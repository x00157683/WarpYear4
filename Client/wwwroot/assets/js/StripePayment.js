redirectToCheckout = function (sessionId) {


    var stripe = Stripe("pk_test_gjD3apXghABsLu3tFLHYYcPW00Hi8Qlqkd");
    stripe.redirectToCheckout({ sessionId: sessionId });


}