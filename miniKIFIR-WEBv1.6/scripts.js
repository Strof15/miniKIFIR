"use strict";
const table = document.getElementById('dataTable');
const inputOM = document.getElementById('inputOM');
const columnsDisplay = ['OM_Azonosito', 'Neve', 'Matematika', 'Magyar'];


const SearchData = async (isSearch) => {
    const response = await fetch("jsonfinal.json");
    const data = await response.json();

    table.textContent = '';
    const headerRow = document.createElement('tr');

    columnsDisplay.forEach(column => {
        const headerCell = document.createElement('th');
        headerCell.textContent = column;
        headerRow.appendChild(headerCell);
    });
    const tableCell = document.createElement('th');
    tableCell.textContent = "Pontszám";
    headerRow.appendChild(tableCell);

    table.appendChild(headerRow);
    data.forEach(item => {
        if (!isSearch || (isSearch && item.OM_Azonosito.startsWith(inputOM.value))) {
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

const ProcessData = async (isSearch) => {
    const response = await fetch("jsonfinal.json");
    const data = await response.json();
    
    if (isSearch && inputOM.value.length !== 11) {
        alert("Az azonosítónak 11 karakter hosszúnak kell lennie!");
        return;
    }
    table.textContent = '';
    const headerRow = document.createElement('tr');
    
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

    data.forEach(item => {
        if (!isSearch || (isSearch && item.OM_Azonosito == inputOM.value)) {
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
