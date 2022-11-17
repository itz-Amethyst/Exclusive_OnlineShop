const cookieName = "cart-items";
const cookieExpireDay = 7;

function addToCart(id) {
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
            id , count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { secure: true, expires: cookieExpireDay, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);

   /* $("#cart_items_count").text(products.length)*/;
    
    window.location.reload();


    //const cartItemsWrapper = $("#cart_items_wrapper");
    //cartItemsWrapper.empty();

    //products.forEach(p => {
    //    // Work On This
    //    // <p class="count">
    //    //    <span>قیمت کل: ${totalPrice} </span>
    //    //</p>
    //    //totalPrice = parseFloat(p.price.replace(/,/g, '')) * parseInt(p.count);
    //    let product =
    //        ` <div class="single-cart-item">
    //                <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${p.id}')">
    //                    <i class="ion-android-close"></i>
    //                </a>
    //                <div class="image">
    //                    <a href="single-product.html">
    //                        <img src="/UploadedFiles/${p.picture}"
    //                             class="img-fluid" alt="">
    //                    </a>
    //                </div>
    //                <div class="content">
    //                    <p class="product-title">
    //                        <a href="https://localhost:7018/Product/${p.slug}">محصول: ${p.name}</a>
    //                    </p>
    //                    <p class="count">
    //                        <span>تعداد:‌ ${p.count} </span>
    //                    </p>
    //                    <p class="count">
    //                        <span>قیمت واحد: ${p.unitPrice} </span>
    //                    </p>

    //                </div>
    //            </div>
    //        `;

    //    cartItemsWrapper.append(product);
    //});
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);

    let itemToRemove = products.findIndex(p => p.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { secure: true, expires: cookieExpireDay, path: "/" });

    updateCart();
}

function updateBasketCount(itemId , id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);;
    const productIndex = products.findIndex(x => x.id == id);
    const count = products[productIndex].count;
    $(`#${itemId}`).text(`تعداد: ${count}`);
}

function changeCartItemCount(id, totalId, count , unitPriceId) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = count;

    if (count < 1) {
        products[productIndex].count = 1;
        count = 1;
        $('#cantAccept0-' + id).val(1);
        $.cookie(cookieName, JSON.stringify(products), { secure: true, expires: cookieExpireDay, path: "/" });
    }
    
    //const product = products[productIndex];
    //const unitPrice = $("#UnitPrice").val();
    const unitPrice = $(`#${unitPriceId}`).val();
    const newPrice = parseInt(unitPrice) * parseInt(count);
    $(`#${totalId}`).text(`${newPrice.toLocaleString()} تومان`);
    //products[productIndex].totalPrice = newPrice;
    $.cookie(cookieName, JSON.stringify(products), { secure: true, expires: cookieExpireDay, path: "/" });
    //updateCart();
}