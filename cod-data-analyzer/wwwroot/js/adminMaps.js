document.addEventListener("DOMContentLoaded", () => {
    console.log("DOM loaded");

    const sendRequest = async (method, id, model = { EditMap: {}, AllMaps: [{}] }) => {
        try {
            const url = method === "POST" ? `/Admin/Maps` : `/Admin/Maps?id=${id}`;
            const headers = new Headers({ "Content-Type": "application/json" });

            const apiRequest = new Request(url, {
                method,
                headers,
                body: JSON.stringify(model)
            });

            const response = await fetch(apiRequest);

            if (!response.ok) throw new Error(`Response status: ${response.status}`);

            const contentType = response.headers.get("content-type");
            if (!contentType || !contentType.includes("application/json")) {
                throw new TypeError("Invalid content type response");
            }

            return true;
        } catch (e) {
            console.error(e.message);
            return false;
        }
    };

    const reloadPage = async () => {     
        window.location.replace("/Admin/Maps");
    };

    const getMapUpdateInputs = () => ({
        name: document.getElementById("mapNameUpdate"),
        small: document.getElementById("isSmallUpdate"),
        faceoff: document.getElementById("isFaceoffUpdate"),
        specialty: document.getElementById("isSpecialtyUpdate")
    });

    const getMapInputValues = (inputs) => ({
        name: inputs.name.value,
        small: inputs.small.checked,
        faceoff: inputs.faceoff.checked,
        specialty: inputs.specialty.checked
    });

    const mapUpdateInputDisableVal = (inputs, disabled) => {
        inputs.name.disabled = disabled;
        inputs.small.disabled = disabled;
        inputs.faceoff.disabled = disabled;
        inputs.specialty.disabled = disabled;
    };

    const resetInputs = (inputs) => {
        inputs.name.value = "";
        inputs.small.checked = false;
        inputs.faceoff.checked = false;
        inputs.specialty.checked = false;
    };

    const exitEditMode = (inputs, editIconsContainer, editIcon, doReset = false) => {
        mapUpdateInputDisableVal(inputs, true);
        if (doReset) resetInputs(inputs);
        editIconsContainer.classList.add("d-none");
        editIcon.classList.remove("d-none");
    };

    const watchNewMap = () => {
        const container = document.getElementById("adminMapNewForm");
        container.querySelector("div > i.uploadIcon").addEventListener("click", async () => {
            const map = {
                MapName: document.getElementById("mapNameNew").value,
                IsSmall: document.getElementById("isSmallNew").checked,
                IsFaceoff: document.getElementById("isFaceoffNew").checked,
                IsSpecialty: document.getElementById("isSpecialtyNew").checked,
            };

            resetInputs(getMapUpdateInputs());

            const success = await sendRequest("POST", 0, { EditMap: map, AllMaps: [{}] });

            document.getElementById("actionSuccess").textContent = success
                ? "New map uploaded. Reloading..."
                : "";
            document.getElementById("actionFail").textContent = success
                ? ""
                : "New map upload failed. No changes made.";

            reloadPage();
        });
    };

    const watchTableEdit = () => {
        const rows = Array.from(document.querySelectorAll("table > tbody > tr"));
        rows.forEach(row => {
            const btn = row.querySelector("td > button");
            btn.addEventListener("click", () => {
                const inputs = getMapUpdateInputs();
                const data = Array.from(row.children).map(td => td.textContent);

                inputs.name.value = data[1];
                inputs.small.checked = data[2] === "yes";
                inputs.faceoff.checked = data[3] === "yes";
                inputs.specialty.checked = data[4] === "yes";

                document.getElementById("mapId").value = data[0];

                const form = document.getElementById("adminMapEditForm");
                const iconsContainer = form.querySelector("div > div > div.editIconsContainer");
                const pencilIcon = form.querySelector("div > div > i.editIcon");

                exitEditMode(inputs, iconsContainer, pencilIcon);
            });
        });
    };

    const watchMapEdit = (container) => {
        const editIcons = container.querySelector("div > div.editIconsContainer");
        const editIcon = container.querySelector("div > i.editIcon");
        const saveIcon = container.querySelector("div > i.saveIcon");
        const deleteIcon = container.querySelector("div > i.deleteIcon");
        const cancelIcon = container.querySelector("div > i.cancelIcon");

        editIcon.addEventListener("click", () => {
            const inputs = getMapUpdateInputs();
            const originalValues = getMapInputValues(inputs);

            if (originalValues.name === "" && !originalValues.small
                && !originalValues.faceoff && !originalValues.specialty
                    && document.getElementById("mapId").value == 0) {
                        mapUpdateInputDisableVal(inputs, true);
                        return;
                }

            mapUpdateInputDisableVal(inputs, false);
            editIcon.classList.add("d-none");
            editIcons.classList.remove("d-none");

            cancelIcon.onclick = () => {
                exitEditMode(inputs, editIcons, editIcon, true);
                document.getElementById("mapId").value = 0;
            };

            deleteIcon.onclick = async () => {
                const success = await sendRequest("DELETE", document.getElementById("mapId").value);
                document.getElementById("actionSuccess").textContent = success
                    ? "Map deleted. Reloading..."
                    : "";
                document.getElementById("actionFail").textContent = success
                    ? ""
                    : "Delete failed. No changes made.";

                exitEditMode(inputs, editIcons, editIcon, true);
                document.getElementById("mapId").value = 0;
                reloadPage();
            };

            saveIcon.onclick = async () => {
                const updatedValues = getMapInputValues(inputs);
                if (JSON.stringify(updatedValues) !== JSON.stringify(originalValues)) {
                    const editMap = {
                        MapName: updatedValues.name,
                        IsFaceoff: updatedValues.faceoff,
                        IsSmall: updatedValues.small,
                        IsSpecialty: updatedValues.specialty
                    };

                    const success = await sendRequest("PUT", document.getElementById("mapId").value, {
                        EditMap: editMap,
                        AllMaps: [{}]
                    });

                    document.getElementById("actionSuccess").textContent = success
                        ? "Map updated. Reloading..."
                        : "";
                    document.getElementById("actionFail").textContent = success
                        ? ""
                        : "Update failed. No changes made.";

                    exitEditMode(inputs, editIcons, editIcon);
                    document.getElementById("mapId").value = 0;
                    reloadPage();
                } else {
                    console.log("No changes detected.");
                }
            };
        });
    };

    watchNewMap();
    watchTableEdit();
    document.querySelectorAll("div.mapEditContainer").forEach(watchMapEdit);
});
