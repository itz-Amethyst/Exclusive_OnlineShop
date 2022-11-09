const cookieName = "cart-items";
const cookieExpireDay = 7;

function addToCart(id, name, price, picture , slug) {
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
            id , name , price , picture , count , slug
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: cookieExpireDay, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);

    $("#cart_items_count").text(products.length);

    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.empty();

    products.forEach(p => {
        // Work On This
        // <p class="count">
        //    <span>قیمت کل: ${totalPrice} </span>
        //</p>
        //totalPrice = parseFloat(p.price.replace(/,/g, '')) * parseInt(p.count);
        let product =
            ` <div class="single-cart-item">
                    <a href="javascript:void(0)" class="remove-icon">
                        <i class="ion-android-close"></i>
                    </a>
                    <div class="image">
                        <a href="single-product.html">
                            <img src="/UploadedFiles/${p.picture}"
                                 class="img-fluid" alt="">
                        </a>
                    </div>
                    <div class="content">
                        <p class="product-title">
                            <a href="https://localhost:7018/Product/${p.slug}">محصول: ${p.name}</a>
                        </p>
                        <p class="count">
                            <span>تعداد:‌ ${p.count} </span>
                        </p>
                        <p class="count">
                            <span>قیمت واحد: ${p.price} </span>
                        </p>
                      
                    </div>
                </div>
            `;

        cartItemsWrapper.append(product);
    });
}