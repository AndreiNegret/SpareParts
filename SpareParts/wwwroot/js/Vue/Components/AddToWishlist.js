
// Add to wishlist button
Vue.component('add-to-wishlist', {
    props: {
        product: {
            type: Object,
            default: () => null
        },
        userId: {
            type: String,
            default: ''
        }
    },
    template: `
        <div>
            <a class="btn btn-primary add-to-wishlist" @click="addToWishList">
                <i class="fa fa-heart"></i>
                Adauga in wishlist
            </a>
        </div>`,
    methods: {
        addToWishList() {
            const vm = this;

            // Make a request for a user with a given ID
            axios.post('/ProductWishList/AddProduct', {
                productId: this.product.ProductId,
                userId: this.userId
            })
            .then(function () {
                // handle success
                vm.$store.commit('addToWishList', vm.product);
            })
            .catch(function (error) {
                // handle error
                console.error(error);
            });
        }
    }
});