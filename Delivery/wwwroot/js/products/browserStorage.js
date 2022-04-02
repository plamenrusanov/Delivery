function getCart() {
    if (!window.localStorage.getItem('cart')) {
        return new Array();
    }
    var cart = window.localStorage.getItem('cart');
    return JSON.parse(cart);
}

function colectProduct() {
    var pp = document.getElementById('packagePrice').innerText;
    var PP = document.getElementById('productPrice').innerText;
    var ST = document.getElementById('subTotal').innerText;
    var product = {
        'SubTotal': ST.substring(0, ST.indexOf(' ')),
        'ProductPrice': PP.substring(0, PP.indexOf(' ')),
        'PackagePrice': pp.substring(0, pp.indexOf(' ')),
        'MaxProducts': document.getElementById('maxProducts').innerText,
        'PName': document.getElementById('productName').innerText,
        'PId': document.getElementById('Id').value,
        "Qty": document.getElementById('theInput').value,
        "Description": document.getElementById('ShoppingCartItemDescription').value,
        "Extras": []
    };

    if (document.getElementById('content')) {
        for (var i = 0; i < document.getElementById('content').childElementCount; i++) {
            var id = $(`#id${i}`).val();
            var name = document.getElementById(`exName${id}`).innerText;
            var ex = document.getElementById(`quantity${id}`);
            if (ex.value > 0) {
                var e = { 'name': `${name}`, 'id': `${id}`, 'qty': `${ex.value}` };
                product.Extras.push(e);
            }
        }
    }  
    return product;
}

function goTo() {
    window.location.href = "/Guest/ShoppingCart/Index";
}

function set(){
    var c = getCart();
    c.push(colectProduct());
    window.localStorage.setItem('cart', JSON.stringify(c));
    goTo();
}


