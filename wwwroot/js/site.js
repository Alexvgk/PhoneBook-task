$(document).ready(function () {
    $("#submitContact").click(async function () {
        let formData = $("#createContactForm").serializeArray();
        let dataObject = {};
        $("input").removeClass("is-invalid");

        formData.forEach(function (item) {
            dataObject[item.name] = item.value;
        });
        if (!dataObject.Name) {
            $("#createContactForm input[name='Name']").addClass("is-invalid");
            return;
        }
        if (!dataObject.JobTitle) {
            dataObject.JobTitle = 'Unemployed';
        }
        try {
            let response = await $.ajax({
                type: "POST",
                url: "/Contact/Create",
                data: JSON.stringify(dataObject),
                contentType: "application/json; charset=UTF-8"
            });

            console.log(response);
            location.reload();
            $('#createContactModal').modal('hide');
        } catch (error) {
            console.error('Error:', error);
        }
    });
});


$(document).ready(function () {
    $(".edit-contact").click(function () {
        let contactId = $(this).data("contact-id");

        $.ajax({
            type: "GET",
            url: "/Contact/GetContactById?id=" + contactId,
            success: function (data) {
                $("#editContactForm #Id").val(data.id);
                $("#editContactForm #Name").val(data.name);
                $("#editContactForm #EditMobilePhone").val(data.mobilePhone);
                $("#editContactForm #JobTitle").val(data.jobTitle);
                $("#editContactForm #Data").val(data.data);

                $("#editContactModal").modal("show");
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });

    $("#submitEditContact").click(function () {
        let formData = $("#editContactForm").serializeArray();

        if (!dataObject.Name) {
            $("#createContactForm input[name='Name']").addClass("is-invalid");
            return;
        }
        if (!dataObject.JobTitle) {
            dataObject.JobTitle = 'Unemployed';
        }

        let dataObject = {};

        formData.forEach(function (item) {
            dataObject[item.name] = item.value;
        });

        $.ajax({
            type: "POST",
            url: "/Contact/UpdateContact?id=" + dataObject.Id,
            data: JSON.stringify(dataObject),
            contentType: "application/json; charset=UTF-8",
            success: function (data) {
                console.log(data);
                $('#editContactModal').modal('hide');
                location.reload();
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
});



    $(document).ready(function () {
        $(".delete-contact").click(function () {
            let contactId = $(this).data("contact-id");

            $.ajax({
                type: "POST",
                url: "/Contact/DeleteContact?id=" + contactId,
                success: function (data) {
                    console.log("Contact deleted successfully");
                    $("#details-" + data.id).remove();
                    $("#contact-" + data.id).remove();
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        });
    });


    $(document).ready(function () {
        var input = document.querySelector("#MobilePhone");

    var iti = window.intlTelInput(input, {
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    initialCountry: "auto", 
    geoIpLookup: function (callback) {
        $.get('https://ipinfo.io', function () { }, 'jsonp').always(function (resp) {
            var countryCode = (resp && resp.country) ? resp.country : "";
            callback(countryCode);
        });
            },
        });

    input.addEventListener('input', function () {
            var isValid = iti.isValidNumber();
    $(this).toggleClass('is-invalid', !isValid);
        });

    $('#submitContact').click(function () {
            var isValid = iti.isValidNumber();
    if (!isValid) {
        $("#MobilePhone").addClass("is-invalid");
    return;
            }
        });
    });

    $(document).ready(function () {
        var input = document.querySelector("#EditMobilePhone");

    var iti = window.intlTelInput(input, {
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
        initialCountry: "auto",
        geoIpLookup: function (callback) {
            $.get('https://ipinfo.io', function () { }, 'jsonp').always(function (resp) {
                var countryCode = (resp && resp.country) ? resp.country : "";
                callback(countryCode);
            });
        },
    });

    input.addEventListener('input', function () {
        var isValid = iti.isValidNumber();
        $(this).toggleClass('is-invalid', !isValid);
    });

        $('#submitEditContact').click(function () {
        var isValid = iti.isValidNumber();
        if (!isValid) {
            $("#EditMobilePhone").addClass("is-invalid");
            return;
        }
    });
    });
