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


