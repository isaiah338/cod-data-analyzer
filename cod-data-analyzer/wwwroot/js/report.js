document.addEventListener("DOMContentLoaded", () => {
    // https://stackoverflow.com/questions/35854244/how-can-i-create-a-horizontal-scrolling-chart-js-line-chart-with-a-locked-y-axis


    // Pie Chart - K/D Ratio
    const kills = +document.getElementById("totalKillsOut").textContent;
    const deaths = +document.getElementById("totalDeathsOut").textContent;

    new Chart(document.getElementById('kdChart').getContext('2d'), {
        type: 'pie',
        data: {
            labels: [`Kills (${kills})`, `Deaths (${deaths})`],
            datasets: [{
                label: 'K/D Ratio',
                data: [kills, deaths],
                backgroundColor: ['rgba(255,99,132,0.2)', 'rgba(54,162,235,0.2)'],
                borderColor: ['rgba(255,99,132,1)', 'rgba(54,162,235,1)'],
                borderWidth: 1
            }]
        },
        options: { responsive: true }
    });

    // Pie Chart - E/D Ratio
    const eliminations = +document.getElementById("totalEliminationsOut").textContent;

    new Chart(document.getElementById('edChart').getContext('2d'), {
        type: 'pie',
        data: {
            labels: [`Eliminations (${eliminations})`, `Deaths (${deaths})`],
            datasets: [{
                label: 'E/D Ratio',
                data: [eliminations, deaths],
                backgroundColor: ['rgba(255,99,132,0.2)', 'rgba(54,162,235,0.2)'],
                borderColor: ['rgba(255,99,132,1)', 'rgba(54,162,235,1)'],
                borderWidth: 1
            }]
        },
        options: { responsive: true }
    });
    // Pie Chart - A/K Ratio
    // kdChart
    const a = +document.getElementById("totalAssistOut").textContent;
    const k = +document.getElementById("totalKillsOut").textContent;

    new Chart(document.getElementById('akChart').getContext('2d'), {
        type: 'pie',
        data: {
            labels: [`Assist (${a})`, `Kills (${k})`],
            datasets: [{
                label: 'A/L Ratio',
                data: [a, k],
                backgroundColor: ['rgba(75,192,192,0.2)', 'rgba(255,99,132,0.2)'],
                borderColor: ['rgba(75,192,192,1)', 'rgba(255,99,132,1)'],
                borderWidth: 1
            }]
        },
        options: { responsive: true }
    });

    // Pie Chart - W/L Ratio
    const wins = +document.getElementById("totalWinsOut").textContent;
    const losses = +document.getElementById("totalLossesOut").textContent;

    new Chart(document.getElementById('wlChart').getContext('2d'), {
        type: 'pie',
        data: {
            labels: [`Wins (${wins})`, `Losses (${losses})`],
            datasets: [{
                label: 'W/L Ratio',
                data: [wins, losses],
                backgroundColor: ['rgba(75,192,192,0.2)', 'rgba(255,99,132,0.2)'],
                borderColor: ['rgba(75,192,192,1)', 'rgba(255,99,132,1)'],
                borderWidth: 1
            }]
        },
        options: { responsive: true }
    });

    // Bar Chart - Skill Progress Over Matches
    var skillByMatch = Array.from(document.querySelectorAll(".matchSkill")).map(el => +el.textContent);
    var killsByMatch = Array.from(document.querySelectorAll(".matchKills")).map(el => +el.textContent);
    var matchDates   = Array.from(document.querySelectorAll(".matchStart")).map(el => el.textContent);

    //var maxVal = 125;

    //var delta = Math.floor(killsByMatch.length / maxVal);

    //var tmp = killsByMatch;
    //for (i = 0; i < killsByMatch.length; i = i + delta) {
    //    tmp.push(killsByMatch[i]);
    //}
    //killsByMatch = tmp;

    //tmp = skillByMatch;
    //for (i = 0; i < skillByMatch.length; i = i + delta) {
    //    tmp.push(skillByMatch[i]);
    //}
    //skillByMatch = tmp;

    //tmp = matchDates;
    //for (i = 0; i < matchDates.length; i = i + delta) {
    //    tmp.push(matchDates[i]);
    //}
    //matchDates = tmp;



    var xAxisLabelMinWidth = 15; // Replace this with whatever value you like
    new Chart(document.getElementById('skillChart').getContext('2d'), {
        type: 'bar',
        data: {
            labels: matchDates,
            datasets: [{
                label: 'Skill',
                data: skillByMatch,
                backgroundColor: 'rgba(54,162,235,0.6)',
                borderColor: 'rgba(54,162,235,1)',
                borderWidth: 1
            },
            {
                label: 'kills',
                data: killsByMatch,
                backgroundColor: 'rgba(226,8,8,0.6)',
                borderColor: 'rgba(54,162,235,1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
            },
            scales: {
                x: {
                    ticks: {
                        display: false
                    },
                    gridLines: {
                        display: false
                    },
                    title: {
                        display: true,
                        text: 'Match Date'
                    }
                },
                y: {
                    beginAtZero: false,
                    title: {
                        display: true,
                        text: 'Skill Rating'
                    }
                }
            }
        }
    });
});