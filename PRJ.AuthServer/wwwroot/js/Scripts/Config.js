function loadingSpinner() {
    $(document).ajaxStart(function () {
        $("#loaderSpinner").show();
    });
    $(document).ajaxComplete(function () {
        $("#loaderSpinner").hide();
    });
}

var CureentSiteURL = "http://localhost:5075";