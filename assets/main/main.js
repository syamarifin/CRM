


function showModalAddInventory() {
    $('#ModalAddInventory').modal('show');
}
function showModalDelete() {
    $('#modalDelete').modal('show');
}
function modalDetail() {
    $('#modalDetail').modal('show');
}
function hideDisplayBlock() {
    $('#myModal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}
function showAlert() {
    $('#showAlert').modal('show');
    $('.modal-backdrop.in').hide();
}
function showModalLogin() {
    $('#myModalLogin').modal({ backdrop: 'static', keyboard: false });
}

