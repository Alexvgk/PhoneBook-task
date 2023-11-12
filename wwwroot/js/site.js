$(document).ready(function () {
    $("#submitContact").click(async function () {
        var formData = $("#createContactForm").serializeArray();
        var dataObject = {};

        formData.forEach(function (item) {
            dataObject[item.name] = item.value;
        });

        try {
            var response = await $.ajax({
                type: "POST",
                url: "/Contact/Create",
                data: JSON.stringify(dataObject),
                contentType: "application/json; charset=UTF-8"
            });

            console.log(response);
            createContactInUI(response);
            $('#createContactModal').modal('hide');
        } catch (error) {
            console.error('Error:', error);
        }
    });
});

function createContactInUI(newContact) {
    var newContactElement = $("<div>").addClass("contact").attr("id", "contact-" + newContact.Id);
    newContactElement.append("<p>Name: " + newContact.Name + "</p>");
    $("#contactsContainer").append(newContactElement);
}


$(document).ready(function () {
    $(".edit-contact").click(function () {
        var contactId = $(this).data("contact-id");

        $.ajax({
            type: "GET",
            url: "/Contact/GetContactById?id=" + contactId,
            success: function (data) {
                $("#editContactForm #Name").val(data.name);
                $("#editContactForm #MobilePhone").val(data.mobilePhone);
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
            url: "/Contact/EditContact",
            data: JSON.stringify(dataObject),
            contentType: "application/json; charset=UTF-8",
            success: function (data) {
                console.log(data);

                $('#editContactModal').modal('hide');
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