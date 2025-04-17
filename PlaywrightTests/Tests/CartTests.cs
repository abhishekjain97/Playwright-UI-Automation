using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests
{
    public class CartTests : TestBase
    {
        private CartPage _cartPage;

        [SetUp]
        public void LaunchWebsite()
        {
            _cartPage = new CartPage(_page);
        }

        [Test]
        public async Task Cart_ShouldHandleProductQuantityAndDeletionCorrectly()
        {
            // Step 1: Go to product page and add one product to cart
            await _cartPage.GoToProductsAsync();
            await _cartPage.AddFirstProductToCartAsync(continueShopping: false);
            await _cartPage.WaitForCartPageAsync();

            // Step 2: Verify quantity is 1
            int qtyAfterFirstAdd = await _cartPage.GetQuantityOfFirstItemAsync();
            Assert.That(qtyAfterFirstAdd, Is.EqualTo(1), $"Expected quantity to be 1 after first add, got {qtyAfterFirstAdd}");

            // Step 3: Go back to product page and view the same product
            await _cartPage.GoToProductsAsync();
            await _cartPage.ViewFirstProductDetailsAsync();

            // Step 4: Set quantity to 2 and add to cart
            await _cartPage.SetProductQuantityAndAddToCartAsync(2);
            await _cartPage.WaitForCartPageAsync();

            // Step 5: Verify quantity is updated to 2
            int qtyAfterSecondAdd = await _cartPage.GetQuantityOfFirstItemAsync();
            Assert.That(qtyAfterSecondAdd, Is.EqualTo(3), $"Expected quantity to be 3 after second add, got {qtyAfterSecondAdd}");

            // Step 6: Remove the product from cart
            await _cartPage.RemoveFirstItemFromCartAsync();

            // Step 7: Verify the cart is empty
            bool isCartEmpty = await _cartPage.IsCartEmptyAsync();
            Assert.IsTrue(isCartEmpty, "Expected the cart to be empty after deletion.");
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
