using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class CartPage
    {
        private readonly IPage _page;

        public CartPage(IPage page) => _page = page;

        private ILocator ProductsNav => _page.Locator("ul.nav.navbar-nav li a[href='/products']").First;
        private ILocator FirstAddToCart => _page.Locator("(//a[contains(text(),'Add to cart')])[1]");
        private ILocator ContinueShoppingBtn => _page.Locator("button:has-text('Continue Shopping')");
        private ILocator ViewCartBtn => _page.Locator("a:has-text('View Cart')");
        private ILocator QuantityButton => _page.Locator("td.cart_quantity button.disabled").First;
        private ILocator ViewProductBtn => _page.Locator("(//a[contains(text(),'View Product')])[1]");
        private ILocator QuantityInput => _page.Locator("input[name='quantity']");
        private ILocator AddToCartButton => _page.Locator("button.cart");
        private ILocator DeleteButton => _page.Locator("a.cart_quantity_delete").First;

        public async Task GoToProductsAsync()
        {
            await ProductsNav.ClickAsync();
            await _page.WaitForSelectorAsync(".features_items");
        }

        public async Task AddFirstProductToCartAsync(bool continueShopping)
        {
            await FirstAddToCart.ClickAsync();
            await _page.Locator("div.modal-content").WaitForAsync();

            if (continueShopping)
                await ContinueShoppingBtn.ClickAsync();
            else
                await ViewCartBtn.ClickAsync();
        }

        public async Task ViewFirstProductDetailsAsync()
        {
            await ViewProductBtn.ClickAsync();
            await _page.WaitForSelectorAsync("div.product-information");
        }

        public async Task SetProductQuantityAndAddToCartAsync(int quantity)
        {
            await QuantityInput.FillAsync(quantity.ToString());
            await AddToCartButton.ClickAsync();
            await _page.Locator("div.modal-content").WaitForAsync();
            await ViewCartBtn.ClickAsync();
        }

        public async Task WaitForCartPageAsync()
        {
            await _page.WaitForSelectorAsync("td.cart_quantity");
        }

        public async Task<int> GetQuantityOfFirstItemAsync()
        {
            await QuantityButton.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            var value = await QuantityButton.InnerTextAsync();
            return int.TryParse(value, out var qty) ? qty : 0;
        }

        public async Task RemoveFirstItemFromCartAsync()
        {
            await DeleteButton.ClickAsync();
            await _page.WaitForSelectorAsync("td.cart_quantity", new() { State = WaitForSelectorState.Detached });
        }

        public async Task<bool> IsCartEmptyAsync()
        {
            var cartItems = await _page.Locator("#cart_info_table tbody tr").CountAsync();
            return cartItems == 0;
        }
    }
}
