function showAlert(message, isSuccess) {

    let alertType = isSuccess ? "success" : "danger";

    let html = `
                  <div class="alert alert-${alertType} alert-dismissible fade show" role="alert">
                    ${message}
                  <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                  </div>
                 `;

    $("#alertBox").html(html);

    setTimeout(() => {
        $("#alertBox").html("");
    }, 3000);
}
