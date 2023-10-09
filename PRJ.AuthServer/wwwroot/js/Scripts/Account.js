

function LoadOTP() {
    $("#OTPModal").modal("show");
    $("#authErrorOTPModal").hide();

    $('#OTPButton').click(function (e) {

        loadingSpinner();

        let Id1 = $('#Id1').val();
        let Id2 = $('#Id2').val();
        let Id3 = $('#Id3').val();
        let Id4 = $('#Id4').val();
        let Id5 = $('#Id5').val();
        let Id6 = $('#Id6').val();

        var URL = CureentSiteURL + "/Account/Verification";


        var dataJSON = {
            "Id1": Id1,
            "Id2": Id2,
            "Id3": Id3,
            "Id4": Id4,
            "Id5": Id5,
            "Id6": Id6
        };


        $.ajax({
            type: 'POST',
            data: JSON.stringify(dataJSON),
            url: URL,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (results) {
                if (results.succeeded) {
                    $("#authErrorOTPModal").hide();
                    $("#authErrorMessageOTPModal").html('')
                    window.location.href = results.data;
                }
                else {
                    $("#authErrorOTPModal").show();
                    $("#authErrorMessageOTPModal").html(results.message)
                }
            }
        });

    });
}

function LoadForgetPassword() {
    $("#authErrorPasswordModal").hide();
    $("#authSuccessPasswordModal").hide();

    $('#ResetButton').click(function (e) {

        loadingSpinner();

        let UserName = $('#UserName').val();

        var URL = CureentSiteURL + "/Account/ForgetPassword";


        var dataJSON = {
            "UserName": UserName,
        };


        $.ajax({
            type: 'POST',
            data: JSON.stringify(dataJSON),
            url: URL,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',

            success: function (results) {
                if (results.succeeded) {
                    $("#authSuccessPasswordModal").show();
                    $("#authSuccessMessagePasswordModal").html(results.message);
                    $("#authErrorPasswordModal").hide();
                    $("#authErrorMessagePasswordModal").html('');
                }
                else {
                    $("#authErrorPasswordModal").show();
                    $("#authErrorMessagePasswordModal").html(results.message);
                    $("#authSuccessPasswordModal").hide();
                    $("#authSuccessMessagePasswordModal").html('');
                }
            }
        });
    });
}
function ResetForget() {
    $("#authErrorPasswordModal").hide();
    $("#authSuccessPasswordModal").hide();
    $("#authErrorPasswordModal").hide();
    $("#authSuccessMessagePasswordModal").html('');
    $("#authErrorPasswordModal").hide();
    $("#authErrorMessagePasswordModal").html('');
    $('#UserName').val('');
}