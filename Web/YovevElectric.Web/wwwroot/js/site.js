var defaultProductImg = "http://res.cloudinary.com/daf7wgitd/image/upload/v1588784545/gnnqimydbiytu00fsmzg.png";

function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn").show();
        $("#btnSubmit").show();
    } else {
        $("#input").val(null);
        $("#img").attr("src", defaultProductImg);
        $("#btn").hide();
        $("#btnSubmit").hide();
    }
}

function loadImg(input) {
    var img = $("#img");
    if (img.attr("src") != defaultProductImg) {
        $("#btn").show();
        $("#btnSubmit").hide();
        $("#input").hide();

        console.log(input);
    } else {
        $("#btn").hide();
        $("#btnSubmit").hide();
        $("#input").show();
        console.log("2");
    }
    if (input == "editProduct") {
        console.log("1");
        if ($("#img").attr("src") != defaultProductImg) {
            $("#productImg").hide();
            console.log($("#productImg"));
        } else {
            $("#productImg").show();
            console.log("2");
        }
    }
}

function loadSubCategoriesToSideBar(input, num) {
    var token = $("#form input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: "/api/category/getSubCategories/" + input,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if ($('ul#subCategories' + num + ' li').length == 0) {
                data.forEach((item) => {
                    console.log(item["name"]);
                    $("#subCategories" + num).append("<li><a href='/Products/Products?category=" + input + "&subCategory=" + item["name"] + "'>" + item["name"] + "</a><span></span></li>");
                });
            }
        }
    });
}

function loadSubCategoriesToSideBarForAdmin(input, num) {
    var token = $("#form input[name=__RequestVerificationToken]").val();
    console.log("start");
    $.ajax({
        url: "/api/category/getSubCategoriesWithDeleted/" + input,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            console.log(data);
            if ($('ul#subCategories' + num + ' li').length == 0) {
                data.forEach((item) => {
                    var status = item["isDeleted"] ? "Неактивно" : "Активно";
                    $("#subCategories" + num).append("<li><a href='/Administration/Administration/DeleteUndeleteSubCategory?name=" + item["name"] + "'>" + item["name"] + " (" + status + ")</a > <span></span></li > ");
                    console.log("<li><a asp-area='Administration' asp-controller='Administration' asp-action='DeleteUndeleteSubCategory' asp-route-name='" + item + "'>" + item + "</a><span></span></li>");
                });
            }
        }
    });
}
function loadCategoriesToDropDown() {

}

function loadSubCategoriesToDropDown(category) {
    var token = $("#form input[name=__RequestVerificationToken]").val();
    if (category == null) {
        category = $('#categories').find(":selected").text();
    }
    $.ajax({
        url: "/api/category/getSubCategories/" + category,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (data.length != 0) {
                $("#subCategoryInput").show();
                data.forEach((item) => {
                    choosen = item == category ? selected = "selected='selected'" : "";

                    $("#subCategories").append("<option " + choosen + " value=" + item["name"] + ">" +
                        item["name"] +
                        "</option>");
                });
            } else {
                $('#subCategories')
                    .find('option')
                    .remove()
                    .end()
                $("#subCategoryInput").hide();
            }

        }
    });
}

function loadCategories(category) {
    var token = $("#form input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: "/api/category/getCategories/",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {

            data.forEach((item) => {
                choosen = item == category ? selected = "selected='selected'" : "";
                console.log(item);
                $("#categories").append(
                    "<option " + choosen + " value=" + item + ">" +
                    item +
                    "</option>");
                console.log("<option " + choosen + " value=" + item + ">" +
                    item +
                    "</option>");
            })

        }
    });
}


function invoiceFnc() {

    var checkBox = document.getElementById("invoiceBtn");
    var inputs = document.getElementById("invoiceInput");
    if (checkBox.checked == true) {

        inputs.style.display = "block";
    } else {
        inputs.style.display = "none";
    }
}
$(function () {
    moment.locale("bg");
    $("time").each(function (i, e) {
        const dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("llll"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});

function searchByTitle() {
    var input = document.getElementById("titleInput");
    // Execute a function when the user releases a key on the keyboard
    input.addEventListener("keypress", function (event) {
        // Number 13 is the "Enter" key on the keyboard
        if (event.keyCode === 13) {
            // Cancel the default action, if needed
            event.preventDefault();
            // Trigger the button element with a click
            document.getElementById("searchForm").submit();
        }
    });
}



