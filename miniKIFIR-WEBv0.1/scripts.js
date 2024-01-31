const Search = (result) => {
    const inputAzonosito = document.getElementById("inputOM").value;
    
    if (/^\d{11}$/.test(inputAzonosito)) {
        const table = document.getElementById('dataTable');
        table.textContent = '';
        result.forEach(item => {
            Object.keys(item).forEach(key => {
                const tableCell = document.createElement('th');
                tableCell.textContent = key;
                tableRow.appendChild(tableCell);
            });
        })
        result.forEach(item => {
            if (item.OM_Azonosito === inputAzonosito) {
                const tableRow = document.createElement('tr');
                table.appendChild(tableRow);
                Object.values(item).forEach(value => {
                    const tableCell = document.createElement('td');
                    tableCell.textContent = value;
                    tableRow.appendChild(tableCell);
                });
                table.appendChild(tableRow);
            }
        });
    }
    else {
        alert("A megadott azonosító nem 11 karakter hosszú!")
    }
};
const Process = (result) => {
    const table = document.getElementById('dataTable');
    table.textContent = '';
    result.forEach(item => {
        const tableRow = document.createElement('tr');
        Object.values(item).forEach(value => {
            const tableCell = document.createElement('td');
            tableCell.textContent = value;
            tableRow.appendChild(tableCell);
        });
        table.appendChild(tableRow);
    });
  };

const Display = (item) => {
    const response = fetch("jsonfinal.json");
    response.then((response) => response.json())
        .then((data) => {
            const searchButton = document.getElementById('btnSearch');
            const processButton = document.getElementById('btnGet');
            if (searchButton == item) {
                Search(data);
            } else if (processButton == item) {
                Process(data);
            } else {
                console.error("Hiba!");
            }
        })
        .catch(console.error);
};