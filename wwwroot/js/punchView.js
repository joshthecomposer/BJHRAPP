let isShowing = false;

const viewPunch = (id) => {
    document.querySelectorAll(".edit-punch").forEach(b => b.disabled = !b.disabled);
    if (isShowing) {
        $("#overlay").hide();
        $("#hidden-" + id).hide();
    } else {
        $("#overlay").show();
        $("#hidden-" + id).show();
    }
    isShowing = !isShowing
}