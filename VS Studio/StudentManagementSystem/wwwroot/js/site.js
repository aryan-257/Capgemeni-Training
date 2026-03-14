// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Confirmation dialog for delete operations
function confirmDelete(message) {
    return confirm(message || 'Are you sure you want to delete this item?');
}

// Auto-hide alerts after 5 seconds
$(document).ready(function () {
    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);
});
