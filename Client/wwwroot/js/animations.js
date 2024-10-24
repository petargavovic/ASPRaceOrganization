console.log('animations.js loaded');
function toggleRowAnimation(rowId, expanded) {
    var row = document.getElementById('row-' + rowId);
    if (row) {
        if (expanded) {
            $(row).fadeIn({ duration: 500 });
        } else {
            $(row).fadeOut({ duration: 500 });
        }
    }
}