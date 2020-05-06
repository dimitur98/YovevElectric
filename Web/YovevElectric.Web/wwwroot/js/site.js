var defaultProductImg = "http://res.cloudinary.com/daf7wgitd/image/upload/v1588784545/gnnqimydbiytu00fsmzg.png";

function upload() {
    var formData = new FormData();
    formData.append("Img" ,$("#productImg")[0].files[0]);
    var token = $("#form input[name=__RequestVerificationToken]").val();
    console.log(formData);
    $.ajax({
        url: "/api/apiImg/upload",
        type: "POST",
        contentType: false,
        data: formData,
        dataType: "json",
        cache: false,
        processData: false,
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (data) {
                $("#uploadedImg").show().delay(2000).fadeOut();
            } else {
                $("#notUploadedImg").show().delay(2000).fadeOut();
            }

        }
    })
}
function imgValidation(input, num) {
    console.log(input);
    var name = input["name"].split(".");
    var format = name[name.length - 1];
    var type = input["type"];
    var size = input["size"];
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { format: format, size: size, type: type };
    console.log(json)
    $.ajax({
        url: "/api/apiImg/imgValidation",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (!data) {
                addImgNumberToAlert(num);
            }
        }

    });
}
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
        $("#input").hide();
        imgValidation(input.files[0], 11);
    } else {
        deleteImgNumberFromAlert("11");
        deleteProductImg($("#img").attr("src"), id);
        $("#input").val(null);
        $("#img").attr("src", defaultProductImg);
        $("#btn").hide();
        $("#btnSubmit").hide();
        $("#input").show();
    }
}

function loadImg() {
    var img = $("#img");
    if (img.attr("src") != defaultProductImg) {
        $("#btn").show();
        $("#btnSubmit").hide();
        $("#input").hide();
    } else {
        $("#btn").hide();
        $("#btnSubmit").hide();
        $("#input").show();
    }
}

function deleteProductImg(input,id) {
    console.log("asd");
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { imgToDel: input, productId: id};
    console.log(json);
    $.ajax({
        url: "/api/apiImg/deleteProductImg",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (data) {
                $("#imgRemove").show().delay(2000).fadeOut();
            } else {
                $("#imgNotRemoved").show().delay(2000).fadeOut();
            }

        }
    });
}



function addImgNumberToAlert(img) {
    $("#alert").show();
    if ($("#invalidImgs").text() == "") {
        $("#invalidImgs").append(img);
    } else {
        $("#invalidImgs").append(", " + img);
    }
}

function deleteImgNumberFromAlert(num) {
    console.log("s");
    var text = $("#invalidImgs").text().split(",");
    console.log(text);
    var index = text.indexOf(num);
    text.splice(index, 1);
    console.log(text);

    $("#invalidImgs").text(text.join(","));
    if ($("#invalidImgs").text() == "") {
        $("#alert").hide();
    }
}