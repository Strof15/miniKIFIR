"use strict";

const table = document.getElementById('dataTable');
const inputOM = document.getElementById('inputOM');

const SearchData = async () => {
    const response = await fetch("jsonfinal.json");
    const data = await response.json();

    table.textContent = '';
    const headerRow = document.createElement('tr');
    const columnsDisplay = ['OM_Azonosito', 'Neve', 'Matematika', 'Magyar'];

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
        if (item.OM_Azonosito.startsWith(inputOM.value)) {
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
const ListData = async () => {
    const response = await fetch("jsonfinal.json");
    const data = await response.json();

    table.textContent = '';
    const headerRow = document.createElement('tr');
    const columnsDisplay = ['OM_Azonosito', 'Neve', 'Matematika', 'Magyar'];

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

    });
};