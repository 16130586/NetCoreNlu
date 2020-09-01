$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();
});


$('.promotion-banner').owlCarousel({
    stagePadding: 70,
    loop:true,
    autoplay: true,
    autoplayHoverPause: true,
    margin:10,
    nav:true,
    responsive:{
        0:{
            items:1,
            dots: true
        },
        600:{
            items:3,
            dots: true
        },
        1000:{
            items:4,
            dots: false
        }
    }
});

$('.phone-banner').owlCarousel({
    stagePadding: 20,
    loop:true,
    autoplay: true,
    autoplayHoverPause: true,
    margin: 30,
    nav:true,
    responsive:{
        0:{
            items:1,
            dots: true
        },
        600:{
            items:3,
            dots: true
        },
        1000:{
            items:4,
            dots: true
        }
    }
});


var slideIndex = 1;
showSlides(1, 1);

function showSlides(n, index) {
    slideIndex = n;
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var zoom_img = document.getElementsByClassName("zoom_img");
    var img_isChoose = document.getElementsByClassName("img_choose");

    if (n > slides.length) {slideIndex = 1}
    if (n < 1) {slideIndex = slides.length}
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
        zoom_img[i].className = zoom_img[i].className.replace("drift-demo-trigger", "");
    }
    for (i = 0; i < img_isChoose.length; i++) {
        img_isChoose[i].style.border = "none";
    }
    var det = '.detail_'+ index;

    slides[slideIndex-1].style.display = "block";
    zoom_img[slideIndex-1].className += "drift-demo-trigger";
    img_isChoose[slideIndex-1].style.border = "1px solid red";
    new Drift(document.querySelector('.drift-demo-trigger'), {
        paneContainer: document.querySelector(det),
        inlinePane: 10,
        inlineOffsetY: -20,
        containInline: true,
        hoverBoundingBox: true
    });
}

function ajaxGetInforProduct(id_pro, indexstart) {
    var xhttp;
    if (id_pro == "") {
        document.getElementById("txtDetail").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("txtDetail").innerHTML = this.responseText;
            showSlides(indexstart+1,1);
        }
    };
    xhttp.open("GET", "/Ajax/GetInforProduct?id_product=" + id_pro+"&index="+indexstart, true);
    xhttp.send();

}

function ajaxAddProduct(id_pro, quantity) {
    var xhttp;
    if (id_pro == "") {
        document.getElementById("txtDetail").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("show_miniCart").innerHTML = this.responseText;
        }
    };
    if (quantity == undefined)
        quantity = 1
    xhttp.open("GET", "/Cart/AddCart?id_product=" + id_pro + "&quantity=" + quantity, true);
    xhttp.send();
}




function ajaxDelCartOnHeader(id_pro) {
    var xhttp;
    if (id_pro == "") {
        document.getElementById("txtDetail").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("show_miniCart").innerHTML = this.responseText;
            ajaxGetQty()
        }
    };
    xhttp.open("GET", "/Cart/AjaxDelCartOnHeader?id_product=" + id_pro, true);
    xhttp.send();
}

function ajaxSetQtyOnHeader(id_pro, val) {
    var xhttp;
    if (id_pro == "") {
        document.getElementById("show_miniCart").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("show_miniCart").innerHTML = this.responseText;
        }
    };
    xhttp.open("GET", "/Cart/AjaxSetQtyOnHeader?id_product=" + id_pro +"&value="+val, true);
    xhttp.send();
}

function ajaxUpdateCartHeader() {
    var xhttp;

    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("show_miniCart").innerHTML = this.responseText;
        }
    };
    xhttp.open("GET", "http://localhost:8080/ProjectWeb/AjaxUpdateCart", true);
    xhttp.send();
}


