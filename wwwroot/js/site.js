$(document).ready(function () {
    $("#submitContact").click(async function () {
        var formData = $("#createContactForm").serializeArray();
        var dataObject = {};
        $("input").removeClass("is-invalid");

        formData.forEach(function (item) {
            dataObject[item.name] = item.value;
        });
        if (!dataObject.Name) {
            $("#createContactForm input[name='Name']").addClass("is-invalid");
            return;
        }
        if (!dataObject.MobilePhone || dataObject.MobilePhone.length < 12) {
            $("#createContactForm input[name='MobilePhone']").addClass("is-invalid");
            return;
        }
        if (!dataObject.JobTitle) {
            dataObject.JobTitle = 'Unemployed';
        }
        try {
            var response = await $.ajax({
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
        var contactId = $(this).data("contact-id");

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
        var formData = $("#editContactForm").serializeArray();

        var dataObject = {};

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
            var contactId = $(this).data("contact-id");

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


