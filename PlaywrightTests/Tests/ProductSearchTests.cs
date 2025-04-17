using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests
{
    public class ProductSearchTests : TestBase
    {
        private ProductsPage _productsPage;

        [SetUp]
        public void LaunchWebsite()
        {
            _productsPage = new ProductsPage(_page);
        }

        [Test]
        [TestCase("tshirt")]
        [TestCase("jeans")]
        [TestCase("dress")]
        public async Task ProductSearchAndFilter_ShouldShowRelevantResults(string keyword)
        {
            await _productsPage.NavigateToProductsAsync();
            await _productsPage.SearchProductAsync(keyword);

            Assert.IsTrue(await _productsPage.AreSearchResultsVisibleAsync(), "Search results are not visible.");
            Assert.IsTrue(await _productsPage.DoResultsContainKeywordAsync(keyword), "Search results do not match keyword.");

            await _productsPage.ApplyCategoryFilterAsync(); // Simulated category filter
            await _productsPage.ApplyBrandFilterAsync();    // Apply brand filter

            // Optionally, re-validate results after filtering
            Assert.IsTrue(await _productsPage.AreSearchResultsVisibleAsync(), "No products visible after filters.");
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
