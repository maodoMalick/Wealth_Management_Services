setInterval(SlideShow, 2000);

function SlideShow() {
    var imgUrl = document.getElementById("imgDisplay").getAttribute("src");
    var imgName = imgUrl.substring(0, imgUrl.lastIndexOf("_") + 1);
    var imgNumber = Number(imgUrl.substring(imgUrl.lastIndexOf("_") + 1, imgUrl.lastIndexOf(".")));
    var imgExtension = imgUrl.substring(imgUrl.lastIndexOf("."));

    if (imgNumber == 6) {
        imgNumber = 0;
    }

    document.getElementById("imgDisplay").setAttribute("src", imgName + (imgNumber + 1) + imgExtension);
};

