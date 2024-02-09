"use strict";
const table = document.getElementById('dataTable'),
inputOM = document.getElementById('inputOM'),
        inputSum = document.getElementById('inputSum'),
        inputNev = document.getElementById('inputNev'),
        spanSearch = document.getElementById('spanSearch'),
        spanMath = document.getElementById('spanMath'),
        spanHun = document.getElementById('spanHun'),
        spanSum = document.getElementById('spanSum'),
        menuList = document.querySelectorAll(".menu-list li"),
        body = document.body,
        navbar = document.querySelector(".navbar"),
        menuBtn = document.querySelector(".menu-btn"),
        cancelBtn = document.querySelector(".cancel-btn"),
        columnsDisplay = ['OM_Azonosito', 'Neve', 'Matematika', 'Magyar'];
    
/*NAVBAR JS*/  
const ToggleMenu = (show) => {
    navbar.classList.toggle("show", show);
    menuBtn.classList.toggle("hide", show);
    body.classList.toggle("disabled", show);
}
menuBtn.onclick = () => ToggleMenu(true);
cancelBtn.onclick = () => ToggleMenu(false);
window.onscroll = () => scrollFunction;

menuList.forEach((menuItem) => {
    menuItem.onclick = () => {
        body.classList.remove("disabled");
        navbar.classList.remove("show");
        menuBtn.classList.remove("hide");
    };
});

function scrollFunction() {
    if (document.documentElement.scrollTop > 20) {
        navbar.classList.add("sticky");
    }
    else {
        navbar.classList.remove("sticky");
    }
}
/*JSON FILE FETCH*/  
const fetchData = async () => {
    const response = await fetch("jsonfinal.json");
    const data = await response.json();
    return data.sort((a, b) => {
        const nameA = a.Neve.toLowerCase();
        const nameB = b.Neve.toLowerCase();
        if (nameA < nameB) return -1;
        if (nameA > nameB) return 1;
        return 0;
    });
};
/*C-D feladat JS function*/
let currentSortKey;
const sortByKey = (data, key) => {
    return data.sort((a, b) => {
        if (key === "Matematika" || key === "Magyar") {
            return b[key] - a[key]; 
        } else {
            const pointA = a[key].toLowerCase();
            const pointB = b[key].toLowerCase();
            if (pointA < pointB) return -1;
            if (pointA > pointB) return 1;
            return 0;
        }
    });
};

const updateTable = (sortedData) => {
    table.textContent = ''; 
    renderTable(sortedData, true); 
};

const SearchData = async (isSearch) => {
    let data = await fetchData();

    table.textContent = '';
    const headerRow = document.createElement('tr');
    
    columnsDisplay.forEach(column => {
        const headerCell = document.createElement('th');
        headerCell.textContent = column;
        headerCell.addEventListener("click", function() {
            currentSortKey = column; 
            const sortedData = sortByKey(data, currentSortKey); 
            updateTable(sortedData); 
        });
        headerRow.appendChild(headerCell);
    });

    const tableCell = document.createElement('th');
    tableCell.textContent = "Pontszam";
    headerRow.appendChild(tableCell);
    let searchNumber = 0, mathNumber = 0, hunNumber = 0, sumNumber = 0;
    table.appendChild(headerRow);
    data.forEach(item => {
        if (!isSearch || (isSearch && item.OM_Azonosito.startsWith(inputOM.value) && item.Neve.startsWith(inputNev.value.toUpperCase()))) {
            searchNumber++;
            const tableRow = document.createElement('tr');
            tableRow.classList.add('tableRow');
            columnsDisplay.forEach(column => {
                const tableCell = document.createElement('td');
                tableCell.textContent = item[column];
                tableRow.appendChild(tableCell);
            });

            const tableCell = document.createElement('td');
            tableCell.textContent = item.Matematika + item.Magyar;
            tableRow.appendChild(tableCell);

            table.appendChild(tableRow);
            mathNumber += item.Matematika;
            hunNumber += item.Magyar;
            sumNumber += item.Matematika + item.Magyar; 
        }
    });
    spanSearch.textContent = searchNumber;
    spanMath.textContent = (mathNumber / searchNumber).toFixed(2);
    spanHun.textContent = (hunNumber / searchNumber).toFixed(2);
    spanSum.textContent = (sumNumber / searchNumber).toFixed(2);
};
function renderTable(data, isSearch) {
    const headerRow = document.createElement('tr');
    
    columnsDisplay.forEach(column => {
        const headerCell = document.createElement('th');
        headerCell.textContent = column;
        headerCell.addEventListener("click", function() {
            currentSortKey = column; 
            const sortedData = sortByKey(data, currentSortKey); 
            updateTable(sortedData); 
        });
        headerRow.appendChild(headerCell);
    });
    
    const tableCell = document.createElement('th');
    tableCell.textContent = "Pontszam";
    headerRow.appendChild(tableCell);

    table.appendChild(headerRow);

    data.forEach(item => {
        if (!isSearch || (isSearch && item.OM_Azonosito.startsWith(inputOM.value) && item.Neve.startsWith(inputNev.value.toUpperCase()))) {
            const tableRow = document.createElement('tr');
            tableRow.classList.add('tableRow');
            columnsDisplay.forEach(column => {
                const tableCell = document.createElement('td');
                tableCell.textContent = item[column];
                tableRow.appendChild(tableCell);
            });

            const tableCell = document.createElement('td');
            tableCell.textContent = item.Matematika + item.Magyar;
            tableRow.appendChild(tableCell);

            table.appendChild(tableRow);
        }
    });
};
/*A-B feladat JS function*/
const ProcessData = async (isSearch) => {
    const data = await fetchData();
    
    if (isSearch && inputOM.value.length !== 11) {
        alert("Az azonosítónak 11 karakter hosszúnak kell lennie!");
        return;
    }
    table.textContent = '';
    const headerRow = document.createElement('tr');
    if (isSearch) {
        const columnsDisplay = ['OM_Azonosito', 'Neve', 'Email', 'SzuletesiDatum', 'ErtesitesiCime', 'Matematika', 'Magyar'];
        columnsDisplay.forEach(column => {
            const headerCell = document.createElement('th');
            headerCell.textContent = column;
            headerRow.appendChild(headerCell);
        });
    }
    else if (!isSearch) {
        columnsDisplay.forEach(column => {
            const headerCell = document.createElement('th');
            headerCell.textContent = column;
            headerRow.appendChild(headerCell);
        });
    }
    const tableCell = document.createElement('th');
    tableCell.textContent = "Pontszam";
    headerRow.appendChild(tableCell);
    
    table.appendChild(headerRow);
    
    let foundOM_Azonosito = false;

    data.forEach(item => {
        if (!isSearch && (item.Magyar + item.Matematika) >= inputSum.value ) {
            const tableRow = document.createElement('tr');
            columnsDisplay.forEach(column => {
                const tableCell = document.createElement('td');
                tableCell.textContent = item[column];
                tableRow.appendChild(tableCell);
            });

            const tableCell = document.createElement('td');
            tableCell.textContent = item.Matematika + item.Magyar;
            tableRow.appendChild(tableCell);

            table.appendChild(tableRow);

        }
        else if (isSearch && item.OM_Azonosito == inputOM.value) {
            const columnsDisplay = ['OM_Azonosito', 'Neve', 'Email', 'SzuletesiDatum', 'ErtesitesiCime', 'Matematika', 'Magyar'];
            const tableRow = document.createElement('tr');
            columnsDisplay.forEach(column => {
                const tableCell = document.createElement('td');
                tableCell.textContent = item[column];
                tableRow.appendChild(tableCell);
            });

            const tableCell = document.createElement('td');
            tableCell.textContent = item.Matematika + item.Magyar;
            tableRow.appendChild(tableCell);

            table.appendChild(tableRow);

            if (isSearch) {
                foundOM_Azonosito = true;
            }
        }
    });
    if (isSearch && !foundOM_Azonosito) {
        alert("Nincs ilyen azonosítójú felvételiző!");
    }
};
/*C-D feladat JS function*/
const ExportText = async () => {
    const dataTextArea = document.getElementById("dataText");
    dataTextArea.value = ""; 
    dataTextArea.style.display = "block";
    const data = await fetchData();

    const keysRow = Object.keys(data[0]).join(";") + '\n';
    dataTextArea.value += keysRow;
    data.forEach(column => {
        if (column.OM_Azonosito.startsWith(inputOM.value) && column.Neve.startsWith(inputNev.value.toUpperCase())) {
            let rowData = "";
            Object.keys(data[0]).forEach(key => {
                rowData += column[key] + ";";
            });

            rowData = rowData.slice(0, -1) + '\n';
            dataTextArea.value += rowData;
        }
        
    });
};