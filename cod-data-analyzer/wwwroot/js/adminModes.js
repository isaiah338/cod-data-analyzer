document.addEventListener("DOMContentLoaded", () => {
    console.log("DOM loaded");
    // sort gamemode select
    sortSelect(document.getElementById("gamemodeTitleNew"));
    sortSelect(document.getElementById("gameModeTypeNew"));
   

    const reloadPage = async () => {     
        window.location.replace("/Admin/Modes");
    }; 

    const getModeTypeUpdateInput = () => (document.getElementById("modeTypeUpdate"));
    const getModeTitleUpdateInputs = () => ({
        name: document.getElementById("modeTitleUpdateName"),
        code: document.getElementById("modeTitleUpdateCode")
    });

    const setInputDisabled = (input, val) => { input.disabled = val };

    const exitEditMode = (inputs, editIconsContainer, editIcon, doReset = false) => {
        if (Array.isArray(inputs)) {
            inputs.forEach(input => {
                setInputDisabled(input, true)
                if (doReset) {
                    input.value = "";
                }
            });
        } else if (isJsonObject(inputs)) {
            disableJsonInputs(inputs, true);
            if (doReset) {
                resetJsonInputs(inputs);
            }
        } else {
            setInputDisabled(inputs, true);
            if (doReset) {
                inputs.value = "";
            }
        }

        
        editIconsContainer.classList.add("d-none");
        editIcon.classList.remove("d-none");
    };
    const resetJsonInputs = (inputs) => {
        for (var key in inputs) {
            inputs[key].value = "";
        }
    };
    const disableJsonInputs = (inputs, val=true) => {
        for (var key in inputs) {
            setInputDisabled(inputs[key], val);
        }
    };

    const isJsonObject = (val) => {
        return (
            typeof val === "object" &&
            val !== null &&
            !Array.isArray(val)
        );
    }
    

    const watchNewModeType = () => {
        const container = document.getElementById("modeTypeNewContainer");
        container.querySelector("div > i.uploadIcon").addEventListener("click", async () => {
            const input = document.getElementById("modeTypeNewName");
            const type = {
                GameTypeName: input.value
            };

            input.value = "";

            const success = await sendRequest("POST", 0, `/admin/ModeType`, { EditType: type });

            document.getElementById("actionSuccess").textContent = success
                ? "New map uploaded. Reloading..."
                : "";
            document.getElementById("actionFail").textContent = success
                ? ""
                : "New map upload failed. No changes made.";

            reloadPage();
        });
    };

    const watchTypeTableEdit = () => {
        const rows = Array.from(document.querySelectorAll("tr.typeTableRow"));
        rows.forEach(row => {
            const btn = row.querySelector("td > button");
            btn.addEventListener("click", () => {
                const input = getModeTypeUpdateInput();
                const data = Array.from(row.children).map(td => td.textContent);
                const hiddenIdInput = document.getElementById("modeTypeId");
                input.value = data[1];
                hiddenIdInput.value = data[0];

                const form = document.getElementById("adminModeTypeUpdateForm");
                const iconsContainer = form.querySelector("div > div > div.editIconsContainer");
                const pencilIcon = form.querySelector("div > div > i.editIcon");
                
                if (input != null) {
                    exitEditMode(input, iconsContainer, pencilIcon);
                } else {
                    console.log("null input");
                }
                
            });
        });
    };
    const watchGameTypeEdit = () => {
        const container = document.getElementById("modeTypeEditContainer");
        const editIcons = container.querySelector("div > div.editIconsContainer");
        const editIcon = container.querySelector("div > i.editIcon");
        const saveIcon = container.querySelector("div > i.saveIcon");
        const deleteIcon = container.querySelector("div > i.deleteIcon");
        const cancelIcon = container.querySelector("div > i.cancelIcon");
        const modeTypeIdInput = document.getElementById("modeTypeId");

        editIcon.addEventListener("click", () => {
            const input = getModeTypeUpdateInput();
            const originalValue = input.value;

            if (originalValue === "") {
                setInputDisabled({ input }, true);
                return;
            }

            setInputDisabled(input, false);
            editIcon.classList.add("d-none");
            editIcons.classList.remove("d-none");

            cancelIcon.onclick = () => {
                exitEditMode(input, editIcons, editIcon, true);
                modeTypeIdInput.value = 0;
            };

            deleteIcon.onclick = async () => {
                const success = await sendRequest("DELETE", modeTypeIdInput.value, `/admin/ModesType?id=${modeTypeIdInput.value}`);
                exitEditMode(input, editIcons, editIcon, true);
                modeTypeIdInput.value = 0;
                reloadPage();
            };

            saveIcon.onclick = async () => {
                const updatedVal = getModeTypeUpdateInput().value;
                if (updatedVal !== originalValue) {
                    const editType = {
                        GameTypeName: updatedVal,
                        GameTypeId: modeTypeIdInput.value
                    };

                    const success = await sendRequest("PUT", modeTypeIdInput.value, `/admin/ModesType?id=${modeTypeIdInput.value}`, {
                        EditType: editType
                    });

                    exitEditMode(input, editIcons, editIcon);
                    modeTypeIdInput.value = 0;
                    reloadPage();
                } else {
                    console.log("No changes detected.");
                }
            };
        });
    };
    watchNewModeType();
    watchTypeTableEdit();
    watchGameTypeEdit(document.getElementById("adminModeTypeUpdateForm"));

    const watchNewModeTitle = () => {
        const container = document.getElementById("modeTitleNewContainer");
        container.querySelector("div > i.uploadIcon").addEventListener("click", async () => {
            const titleInput = document.getElementById("modeTitleNewTitle");
            const codeInput  = document.getElementById("modeTitleNewCode");
            const title = {
                GameTitleCode: codeInput.value,
                GameTitleName: titleInput.value,
            };

            titleInput.value = "";
            codeInput.value = "";

            const success = await sendRequest("POST", 0, `/admin/ModeTitle`, { EditTitle: title });

            reloadPage();
        });
    };

    const watchTitleTableEdit = () => {
        let rows = Array.from(document.querySelectorAll("tr.titleTableRow"));
        rows = rows.splice(1, rows.length);

        if (!rows || rows.length === 0) {
            console.warn("No rows found.");
            return;
        }

        rows.forEach(row => {
            console.log(`row:\n${row}`);
            // added delay due to button not being found initially
            // 4 attempts then it fails
            let attempts = 0;
            const maxAttempts = 4;
            const retryDelay = 200; 

            const tryAttach = () => {
                const btn = row.querySelector(".titleRowEdit");

                if (btn) {
                    btn.addEventListener("click", () => {
                        const inputs = getModeTitleUpdateInputs();
                        const data = Array.from(row.children).map(td => td.textContent?.trim());
                        const hiddenIdInput = document.getElementById("modeTitleId");

                        inputs.name.value = data[2];
                        inputs.code.value = data[1];
                        hiddenIdInput.value = data[0];

                        const form = document.getElementById("adminModeTitleUpdateForm");
                        const iconsContainer = form?.querySelector("div > div > div.editIconsContainer");
                        const pencilIcon = form?.querySelector("div > div > i.editIcon");

                        exitEditMode(inputs, iconsContainer, pencilIcon);
                    });
                } else if (++attempts < maxAttempts) {
                    setTimeout(tryAttach, retryDelay);
                } else {
                    console.warn("Button not found after multiple attempts in row:", row);
                }
            };

            tryAttach();
        });
    };

    const watchGameTitleEdit = () => {
        const container = document.getElementById("modeTitleEditContainer");
        const editIcons = container.querySelector("div > div.editIconsContainer");
        const editIcon = container.querySelector("div > i.editIcon");
        const saveIcon = container.querySelector("div > i.saveIcon");
        const deleteIcon = container.querySelector("div > i.deleteIcon");
        const cancelIcon = container.querySelector("div > i.cancelIcon");
        const modeTitleIdInput = document.getElementById("modeTitleId");

        editIcon.addEventListener("click", () => {
            const inputs = getModeTitleUpdateInputs();
            const originalNameVal = inputs.name.value;
            const originalCodeVal = inputs.code.value;

            if (originalNameVal === "" && originalCodeVal === "") {
                disableJsonInputs(inputs, true); // fixed
                return;
            }
            disableJsonInputs(inputs, false);
            editIcon.classList.add("d-none");
            editIcons.classList.remove("d-none");

            cancelIcon.onclick = () => {
                if (inputs) {
                    exitEditMode(inputs, editIcons, editIcon, true);
                    modeTitleIdInput.value = 0;
                }
            };

            deleteIcon.onclick = async () => {
                const success = await sendRequest("DELETE", modeTitleIdInput.value, `/admin/ModesTitle?id=${modeTitleIdInput.value}`);
                exitEditMode(inputs, editIcons, editIcon, true); // fixed
                modeTitleIdInput.value = 0;
                reloadPage();
            };

            saveIcon.onclick = async () => {
                const updatedInputs = getModeTitleUpdateInputs();
                const updatedValName = updatedInputs.name.value;
                const updatedValCode = updatedInputs.code.value;

                console.log(`${originalNameVal}\n${originalCodeVal}\n${updatedValName}\n${updatedValCode}`);

                if (updatedValName !== originalNameVal || updatedValCode !== originalCodeVal) {
                    const editTitle = {
                        GameTitleName: updatedValName,
                        GameTitleCode: updatedValCode,
                        GameTitleId: modeTitleIdInput.value
                    };

                    const success = await sendRequest("PUT", modeTitleIdInput.value, `/admin/ModesTitle?id=${modeTitleIdInput.value}`, {
                        EditTitle: editTitle
                    });

                    exitEditMode(updatedInputs, editIcons, editIcon); // fixed
                    modeTitleIdInput.value = 0;
                    reloadPage();
                } else {
                    console.log("No changes detected.");
                }
            };
        });
    };

    watchNewModeTitle();
    watchTitleTableEdit();
    watchGameTitleEdit(document.getElementById("adminModesNewForm"));
    //document.querySelectorAll("div.mapEditContainer").forEach(watchMapEdit);
    const watchNewGameMode = () => {
        const container = document.getElementById("adminModesNewForm");
        container.querySelector("div > i.uploadIcon").addEventListener("click", async () => {
            const gameTitle = document.getElementById("gamemodeTitleNew");
            const gameType = document.getElementById("gameModeTypeNew");
            const editGameMode = {
                GameTitleId: gameTitle.value,
                GameTypeId: gameType.value
            };

            const success = await sendRequest("POST", 0, `/admin/Modes`, { EditGameMode: editGameMode });
            
            reloadPage();
        });
    };

    const watchGameModeTableEdit = () => {
        let rows = Array.from(document.querySelectorAll("tr.gameModeTableRow"));
        rows = rows.splice(1, rows.length);

        if (!rows || rows.length === 0) {
            console.warn("No rows found.");
            return;
        }

        rows.forEach(row => {
            console.log(`row:\n${row}`);
            // added delay due to button not being found initially
            // 4 attempts then it fails
            let attempts = 0;
            const maxAttempts = 4;
            const retryDelay = 200;

            const tryAttach = () => {
                const btn = row.querySelector("td > button");

                if (btn) {
                    btn.addEventListener("click", () => {
                        const input = document.getElementById("gameModeDescriptionUpdate");
                        const editModeTitleInput = document.getElementById("gameModeTitleEditOut");
                        const editModeTypeInput = document.getElementById("gameModeTypeEditOut");
                        const titleIdOut = document.getElementById("gameModeUpdateTitleId");
                        const typeIdOut = document.getElementById("gameModeUpdateTypeId");
                        const modeTitleIdInput = document.getElementById("gameModeId");
                        const data = Array.from(row.children).map(td => td.textContent?.trim());
                        //alert(JSON.stringify(data))
                        input.value  = data[3];
                        editModeTitleInput.textContent  = data[1];
                        editModeTypeInput.textContent = data[2];
                        titleIdOut.value = data[4];
                        typeIdOut.value = data[5];
                        modeTitleIdInput.value = data[0];

                        const form = document.getElementById("adminModeUpdateForm");
                        const iconsContainer = form?.querySelector("div > div > div.editIconsContainer");
                        const pencilIcon = form?.querySelector("div > div > i.editIcon");

                        input.disabled = true;

                        exitEditMode(input, iconsContainer, pencilIcon);
                    });
                } else if (++attempts < maxAttempts) {
                    setTimeout(tryAttach, retryDelay);
                } else {
                    console.warn("Button not found after multiple attempts in row:", row);
                }
            };

            tryAttach();
        });
    };
    const watchGameModeEdit = () => {
        const container = document.getElementById("modeEditContainer");
        const editIcons = container.querySelector("div > div.editIconsContainer");
        const editIcon = container.querySelector("div > i.editIcon");
        const saveIcon = container.querySelector("div > i.saveIcon");
        const deleteIcon = container.querySelector("div > i.deleteIcon");
        const cancelIcon = container.querySelector("div > i.cancelIcon");
        const modeTitleIdInput = document.getElementById("gameModeId");

        editIcon.addEventListener("click", () => {
            //alert("click")
            const input = document.getElementById("gameModeDescriptionUpdate");
            const originalVal = input.value;

            const gameTitleOut = document.getElementById("gameModeTitleEditOut");
            const gameTypeOut = document.getElementById("gameModeTypeEditOut");
            if (gameTitleOut.textContent == "" || gameTypeOut.textContent == "") {
                exitEditMode(input, editIcons, editIcon, true);
            }
            cancelIcon.onclick = () => {
                gameTitleOut.textContent = "";
                gameTypeOut.textContent = "";
                exitEditMode(inputtextContents, editIcons, editIcon, true);
                modeTitleIdInput.value = 0;

            };

            input.disabled = false;
            editIcon.classList.add("d-none");
            editIcons.classList.remove("d-none");

            cancelIcon.onclick = () => {
                exitEditMode({ input }, editIcons, editIcon, true);
                gameTitleOut.textContent = "";
                gameTypeOut.textContent = "";
                modeTitleIdInput.value = 0;
            };

            deleteIcon.onclick = async () => {
                const success = await sendRequest("DELETE", modeTitleIdInput.value, `/admin/Modes?id=${modeTitleIdInput.value}`);
                exitEditMode(input, editIcons, editIcon, true); // fixed
                modeTitleIdInput.value = 0;
                reloadPage();
            };

            saveIcon.onclick = async () => {
                const updatedInput = document.getElementById("gameModeDescriptionUpdate");
                const updatedVal = updatedInput.value;

                const gameTitleOut = document.getElementById("gameModeUpdateTitleId");
                const gameTypeOut = document.getElementById("gameModeUpdateTypeId");
                //alert(gameTitleOut.value)
                //alert(gameTypeOut.value)
                //console.log(`${originalNameVal}\n${originalCodeVal}\n${updatedValName}\n${updatedValCode}`);

                if (updatedVal !== originalVal) {
                    const editGameMode = {
                        GameModeDescription: updatedVal,
                        GameModeId: modeTitleIdInput.value,
                        GameTypeId: gameTypeOut.value,
                        GameTitleId: gameTitleOut.value
                    };
                    //alert(JSON.stringify(editGameMode))
                    const success = await sendRequest("PUT", modeTitleIdInput.value, `/admin/Modes?id=${modeTitleIdInput.value}`, {
                        EditGameMode: editGameMode,
                        
                    });

                    exitEditMode(updatedInput, editIcons, editIcon); // fixed
                    modeTitleIdInput.value = 0;
                    reloadPage();
                } else {
                    console.log("No changes detected.");
                }
            };
        });
    };
    watchNewGameMode();

    watchGameModeTableEdit();
    watchGameModeEdit(document.getElementById("adminModesNewForm"));

    sortGameModeTable();
    
});

const sortGameModeTable = () => {
    // sort game mode tables by type
    let rows = Array.from(document.querySelectorAll("tr.gameModeTableRow"));
    const tableBody = document.getElementById("gamemodeTableBody");
    console.log(rows)
    // first sort by game title
    rows = rows.sort((a, b) => {
        let columnA = Array.from(a.querySelectorAll("td"))[1].textContent;
        let columnB = Array.from(b.querySelectorAll("td"))[1].textContent;
        return columnA.trim().localeCompare(columnB.trim());
    });
    // then sort by game type
    rows = rows.sort((a, b) => {
        let columnA = Array.from(a.querySelectorAll("td"))[2].textContent;
        let columnB = Array.from(b.querySelectorAll("td"))[2].textContent;
        return columnA.trim().localeCompare(columnB.trim());
    });
    // Remove all existing rows and re-append in sorted order
    rows.innerHTML = "";
    rows.forEach(row => tableBody.appendChild(row));
};
const sendRequest = async (method, id, url, model = null) => {
    try {
        const headers = new Headers({ "Content-Type": "application/json" });
        method = method.toUpperCase();
        if (model != null) model = JSON.stringify(model);
        const apiRequest = new Request(url, {
            method,
            headers,
            body: model
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
const sortSelect = (select) => {
    const options = Array.from(select.children);
    options.sort((a, b) => a.textContent.trim().localeCompare(b.textContent.trim()));
    select.innerHTML = "";
    options.forEach(option => select.appendChild(option));


    // Remove all existing options and re-append in sorted order
    select.innerHTML = "";
    options.forEach(option => select.appendChild(option));
};