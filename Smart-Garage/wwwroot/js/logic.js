document.addEventListener('DOMContentLoaded', function () {
    const createResizableTable = function (table) {
        const cols = table.querySelectorAll('th');
        [].forEach.call(cols, function (col) {
            // Add a resizer element to the column
            const resizer = document.createElement('div');
            resizer.classList.add('resizer');

            // Set the height
            resizer.style.height = table.offsetHeight + 'px';

            col.appendChild(resizer);

            createResizableColumn(col, resizer);
        });
    };

    const createResizableColumn = function (col, resizer) {
        let x = 0;
        let w = 0;

        const mouseDownHandler = function (e) {
            x = e.clientX;

            const styles = window.getComputedStyle(col);
            w = parseInt(styles.width, 10);

            document.addEventListener('mousemove', mouseMoveHandler);
            document.addEventListener('mouseup', mouseUpHandler);

            resizer.classList.add('resizing');
        };

        const mouseMoveHandler = function (e) {
            const dx = e.clientX - x;
            col.style.width = (w + dx) + 'px';
        };

        const mouseUpHandler = function () {
            resizer.classList.remove('resizing');
            document.removeEventListener('mousemove', mouseMoveHandler);
            document.removeEventListener('mouseup', mouseUpHandler);
        };

        resizer.addEventListener('mousedown', mouseDownHandler);
    };

    createResizableTable(document.getElementById('resizeMe'));
});
document.addEventListener('DOMContentLoaded', function () {
    // Get the input field for the license plate
    var licensePlateInput = document.getElementById('licensePlateInput');

    // Add event listener for keypress event
    licensePlateInput.addEventListener('keypress', function (event) {
        // Check if the pressed key is 'Enter' (key code 13)
        if (event.which === 13 || event.keyCode === 13) {
            // Prevent the default form submission behavior
            event.preventDefault();
            // Submit the form using JavaScript
            console.log("Enter key pressed"); // Check if this line is logged
            document.getElementById('myForm').submit();
        }
    });
});

// -----------------------------------------------------------------------------------------------------------


// Function to collect data from parts table
function collectPartsData() {
    var partsData = [];
    var partRows = document.querySelectorAll('#itemTableBody tr');
    partRows.forEach(function (row) {
        var partName = row.querySelector('.form-control[name="ServiceInstance.Parts.Name"]').value;
        var partQuantity = parseInt(row.querySelector('.form-control[name="ServiceInstance.Parts.Quantity"]').value);
        var partUnitPrice = parseFloat(row.querySelector('.form-control.item-unit-price').value);
        var partPrice = parseFloat(row.querySelector('.form-control.item-price').value);

        partsData.push({
            Name: partName,
            Quantity: partQuantity,
            UnitPrice: partUnitPrice,
            Price: partPrice
        });
    });
    return partsData;
}

// Function to collect data from services table
function collectServicesData() {
    var servicesData = [];
    var serviceRows = document.querySelectorAll('#serviceTableBody tr');
    serviceRows.forEach(function (row) {
        var serviceName = row.querySelector('.form-control[name="serviceName"]').value;
        var serviceMechanic = row.querySelector('.form-control[name="serviceMechanic"]').value;
        var servicePrice = parseFloat(row.querySelector('.form-control.service-price').value);

        servicesData.push({
            Name: serviceName,
            Mechanic: serviceMechanic,
            Price: servicePrice
        });
    });
    return servicesData;
}

// Function to send data to the controller
function sendDataToController() {
    var partsData = collectPartsData();
    var servicesData = collectServicesData();

    var visitData = {
        PartsTotalPrice: parseFloat(document.getElementById('totalPrice').textContent),
        ServicesTotalPrice: parseFloat(document.getElementById('totalServicePrice').textContent),
        Vehicle: {
            // Populate with vehicle data as needed
        },
        ServiceInstances: servicesData.map(function (service) {
            return {
                Mechanic: { FirstName: service.Mechanic },
                Service: [{ Name: service.Name, Price: service.Price }],
                Parts: partsData
            };
        })
    };

    // Send data via AJAX
    $.ajax({
        type: 'POST',
        url: '/Admin_Visits/Create', // Adjust the URL as needed
        contentType: 'application/json',
        data: JSON.stringify(visitData),
        success: function (response) {
            // Handle success response
        },
        error: function (xhr, status, error) {
            // Handle error
        }
    });
}