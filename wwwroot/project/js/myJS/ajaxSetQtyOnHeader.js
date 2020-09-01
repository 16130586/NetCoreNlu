function ajaxSetQtyOnHeader(id_pro, val) {
    var xhttp;
    if (id_pro == "") {
        document.getElementById("show_miniCart").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("show_miniCart").innerHTML = this.responseText;
        }
    };
    xhttp.open("GET", "/Cart/AjaxSetQtyOnHeader?id_product=" + id_pro + "&value=" + val, true);
    xhttp.send();
}
