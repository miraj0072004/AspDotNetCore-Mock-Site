function confirmDelete(uniqueId, isDeleteClicked) {

    var confirmDeleteSpan = "confirmDeleteSpan_" + uniqueId;
    var deleteSpan = "deleteSpan_" + uniqueId;
    if (isDeleteClicked) {
        $("#" + confirmDeleteSpan).show();
        $("#" + deleteSpan).hide();
    } else {
        $("#" + confirmDeleteSpan).hide();
        $("#" + deleteSpan).show();
    }
}