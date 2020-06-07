var defaultProductImg = "http://res.cloudinary.com/daf7wgitd/image/upload/v1588784545/gnnqimydbiytu00fsmzg.png";

function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img1")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn1").show();
        $("#btnSubmit").show();
    } else {
        $("#productImg1").val(null);
        $("#img1").attr("src", defaultProductImg);
        $("#btn1").hide();
        $("#btnSubmit").hide();
    }
}
function readURL2(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img2")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn2").show();
        $("#btnSubmit").show();
    } else {
        $("#productImg2").val(null);
        $("#img2").attr("src", defaultProductImg);
        $("#btn2").hide();
        $("#btnSubmit").hide();
    }
}
function readURL3(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img3")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn3").show();
        $("#btnSubmit").show();
    } else {
        $("#productImg3").val(null);
        $("#img3").attr("src", defaultProductImg);
        $("#btn3").hide();
        $("#btnSubmit").hide();
    }
}
function readURL4(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img4")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn4").show();
        $("#btnSubmit").show();
    } else {
        $("#productImg4").val(null);
        $("#img4").attr("src", defaultProductImg);
        $("#btn4").hide();
        $("#btnSubmit").hide();
    }
}
function loadImg(input) {
    for (var i = 1; i <= 4; i++) {
        var img = $("#img" + i);

        if (img.attr("src") != defaultProductImg) {
            $("#btn" + i).show();
            $("#productImg" + i).hide();
        } else {
            $("#btn" + i).hide();
            $("#productImg" + i).show();
        }
        if (input == "editProduct") {
            console.log("1");
            if ($("#img" + i).attr("src") != defaultProductImg) {
                $("#productImg" + i).hide();
            } else {
                $("#productImg" + i).show();
            }
        }
    }
    
}
function loadAllSubCategories() {
    var token = $("#form input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: "/api/category/getCategories/",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                loadSubCategoriesToSideBar(data[i], i);
            }
        }
    });

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
                    $("#subCategories" + num).append("<li><a href='/Administration/Administration/DeleteSubCategory?id=" + item["id"] + "'>" + item["name"] + "</a > <span></span></li > ");
                });
            }
        }
    });
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
function showConfirmOrder() {

    $('#exampleModal').modal()
    var checkBox = document.getElementById("invoiceBtn");
    var invoiceData = document.getElementById("invoiceData");
    $("#city").append($("#cityInput").val());
    $("#adress").append($("#adressInput").val());
    $("#name").append($("#nameInput").val());
    $("#lastName").append($("#lastNameInput").val());
    $("#postCode").append($("#postCodeInput").val());
    $("#telephone").append($("#telephoneInput").val());
    $("#moreInfo").append($("#moreInfoInput").val());
    $("#bulstad").append($("#bulstadInput").val());
    $("#MOL").append($("#MOLInput").val());
    $("#firmName").append($("#firmNameInput").val());
    if (checkBox.checked == true) {

        invoiceData.style.display = "block";
    } else {
        invoiceData.style.display = "none";
    }
}
function addProductToBag(productName, id) {
    $('#addProductToBagModal').modal()
    $("#productId").val(id);
    $("#title").text("Добави " + productName + " към кошницата.");
}

function changeImg(imgNum) {
    $("#product-zoom").attr("src", $("#product-zoom" + imgNum).attr("src"));
    $("#product-zoom").attr("data-zoom-image", $("#product-zoom" + imgNum).attr("src"));
    console.log($("#product-zoom1").attr("src"));
    console.log($("#product-zoom1").attr("src"));

}

function showDiscounts() {
    $("#table").toggle()
}