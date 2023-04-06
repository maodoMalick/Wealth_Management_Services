setInterval(SlideShow, 3000);

function SlideShow() {
    //SetOpacity();
    // Get the image's Url
    var imgUrl = document.getElementById("img-Animation").getAttribute("src");
    // Extract the image's name
    var imgName = imgUrl.substring(0, imgUrl.lastIndexOf("_") + 1);
    // Extract the image's id then convert it to a number
    var imgNumber = Number(imgUrl.substring(imgUrl.lastIndexOf("_") + 1, imgUrl.lastIndexOf(".")));
    // Extract the image's extension
    var imgExtension = imgUrl.substring(imgUrl.lastIndexOf("."));

    // If end is reached, return to beginning
    if (imgNumber == 4) {
        imgNumber = 0;
    }

    // Now select the next image in the sequence
    document.getElementById("img-Animation").setAttribute("src", imgName + (imgNumber + 1) + imgExtension);
};

//SetOpacity();
function SetOpacity() {
    // Get the image's Url
    var image = document.getElementById("img-Animation");
    // Control the image opacity
    for (var i = 0; i <= 1; i += .001) {
        image.style.opacity = i;
    }
}
