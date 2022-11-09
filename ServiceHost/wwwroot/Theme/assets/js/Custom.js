const cookieName = "cart-items";
const cookieExpireDay = 7;

function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);

    if (products === undefined) {
        products = [];
    }
    else {
        products = JSON.parse(products); 
    }

    const count = $("#productCount").val();

    const currentProduct = products.find(p => p.id === id);

    if (currentProduct != undefined) {
        currentProduct.count = parseInt(currentProduct.count) + parseInt(count);
    }
    else {
        const product = {
            id , name , price , picture , count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: cookieExpireDay, path: "/" });
}