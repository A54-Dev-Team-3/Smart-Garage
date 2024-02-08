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