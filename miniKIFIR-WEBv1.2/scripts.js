"use strict";

const table = document.getElementById('dataTable');
const Display = (item) => {
    const response = fetch("jsonfinal.json");
    response.then((response) => response.json())
        .then((data) => {
            const searchButton = document.getElementById('btnSearch');
            const processButton = document.getElementById('btnGet');

            if (searchButton == item) {
                ProcessData(data, true);
            } else if (processButton == item) {
                ProcessData(data, false);
            } else {
                console.error("Hiba!");
            }
        })
        .catch(console.error);
};

const ProcessData = (result, isSearch) => {
    table.textContent = '';
    const headerRow = document.createElement('tr');
    const columnsDisplay = ['OM_Azonosito', 'Neve', 'Matematika', 'Magyar'];
    
    if (isSearch && document.getElementById("inputOM").value.length !== 11) {
        alert("Az azonosítónak 11 karakter hosszúnak kell lennie!");
        return;
    }

    columnsDisplay.forEach(column => {
        const headerCell = document.createElement('th');
        headerCell.textContent = column;
        headerRow.appendChild(headerCell);
    });
    const tableCell = document.createElement('th');
    tableCell.textContent = "Pontszám";
    headerRow.appendChild(tableCell);

    table.appendChild(headerRow);

    let foundOM_Azonosito = false;

    result.forEach(item => {
        if (!isSearch || (isSearch && item.OM_Azonosito == document.getElementById("inputOM").value)) {
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