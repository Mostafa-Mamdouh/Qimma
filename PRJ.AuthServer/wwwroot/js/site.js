// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/********************************** Form Validation **********************************/

// Validate Email
let emailError = true;
const email = document.getElementById("email");
email.addEventListener("blur", () => {
    let regex = /^([_\-\.0-9a-zA-Z]+)@([_\-\.0-9a-zA-Z]+)\.([a-zA-Z]){2,7}$/;
    let s = email.value;
    if (regex.test(s)) {
        email.classList.remove("is-invalid");
        emailError = true;
    } else {
        $("#emailcheck").show();
        email.classList.add("is-invalid");
        emailError = false;
    }
});

// Validate Password
let passwordError = true;
function validatePassword() {
    let passwordValue = $("#password").val();
    if (passwordValue.length == "") {
        $("#passcheck").show();
        passwordError = false;
        return false;
    }
    if (passwordValue.length < 6 || passwordValue.length > 12) {
        $("#passcheck").show();
        $("#passcheck").html(
            "**تأكد من إدخال كلمة السر بشكل صحيح**"
        );
        passwordError = false;
        return false;
    } else {
        $("#passcheck").hide();
    }
}


// Submit button
//$("#submit").click(function () {
//    validatePassword();
//    if (
//        passwordError == true &&
//        emailError == true
//    ) {
//        return true;
//    } else {
//        return false;
//    }
//});

