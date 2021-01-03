
// Wishlist
Vue.component('wishlist', {
    template: `
        <div v-show="isVisible">
            <ul class="list-group d-flex">
               <h3>Wishlist</h3>
               <li class="list-group-item" v-for="product in wishlist">
                    <div class="col-md-3 product-img"><img class="img-responsive" :src="'/images/' + product.photo"></div>
                    <div class="col-md-3">{{product.productName}}</div>
                    <div class="col-md-3">{{product.description}}</div>
                    <div class="col-md-3">{{product.price}}</div>
                </li>
            </ul>
        </div>`,
    computed: {
        wishlist() {
            return this.$store.state.wishlist;
        },
        isVisible() {
            return this.wishlist.length > 0;
        }
    },
    data() {
        return {
            isOpen: false
        }
    }
});