"use strict";
const menuList = document.querySelectorAll(".menu-list li");
const body = document.querySelector("body");
const navbar = document.querySelector(".navbar");
const menuBtn = document.querySelector(".menu-btn");
const cancelBtn = document.querySelector(".cancel-btn");
menuBtn.onclick = ()=>{
    navbar.classList.add("show");
    menuBtn.classList.add("hide");
    body.classList.add("disabled");
}
cancelBtn.onclick = ()=>{
    body.classList.remove("disabled");
    navbar.classList.remove("show");
    menuBtn.classList.remove("hide");
}
menuList.forEach((menuItem) => {
    menuItem.onclick = () => {
        body.classList.remove("disabled");
        navbar.classList.remove("show");
        menuBtn.classList.remove("hide");
    };
});
window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (document.documentElement.scrollTop > 20) {
        navbar.classList.add("sticky");
    }
    else {
        navbar.classList.remove("sticky");
    }
}

const table = document.getElementById('dataTable'),
        inputOM = document.getElementById('inputOM'),
        inputSum = document.getElementById('inputSum'),
        inputNev = document.getElementById('inputNev'),
        columnsDisplay = ['OM_Azonosito', 'Neve', 'Matematika', 'Magyar'];

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

const SearchData = async (isSearch) => {
    const data = await fetchData();

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
    const spanSearch = document.getElementById('spanSearch'),
        spanMath = document.getElementById('spanMath'),
        spanHun = document.getElementById('spanHun'),
        spanSum = document.getElementById('spanSum');
    let searchNumber = 0, mathNumber = 0, hunNumber = 0, sumNumber = 0;
    table.appendChild(headerRow);
    data.forEach(item => {
        if (!isSearch || (isSearch && item.OM_Azonosito.startsWith(inputOM.value) && item.Neve.startsWith(inputNev.value))) {
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
    tableCell.textContent = "Pontszám";
    headerRow.appendChild(tableCell);
    
    table.appendChild(headerRow);
    
    let foundOM_Azonosito = false;

    data.forEach(item => {
        if (!isSearch && (item.Magyar + item.Matematika) == inputSum.value ) {
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
