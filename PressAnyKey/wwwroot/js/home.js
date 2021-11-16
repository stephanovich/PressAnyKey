var imgs = ["../images/bg1.jpg", "../images/bg2.jpg", "../images/bg3.jpg", "../images/bg4.png"];
var i = 0;
var v = 0;

var a = $("#Reg-slide")[0];


function SlideLoop() {
    setTimeout(function () {
        v = Math.floor(Math.random() * imgs.length)
        $("#Reg-slide").fadeOut(600, function (event) {
            a.style.backgroundImage = "url(" + imgs[v] + ")";
            $("#Reg-slide").fadeIn(600);
        });
        i++;
        if (i < imgs.length) {
            SlideLoop();
        } else {
            i = 0;
            setTimeout(function () { SlideLoop() }, 5000);
        }
    }, 5000)
};

SlideLoop();
