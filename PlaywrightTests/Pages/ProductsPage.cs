using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class ProductsPage
    {
        private readonly IPage _page;

        public ProductsPage(IPage page) => _page = page;

        // Locators
        private ILocator ProductsNav => _page.Locator("a[href='/products']");
        private ILocator SearchBox => _page.Locator("#search_product");
        private ILocator SearchButton => _page.Locator("#submit_search");
        private ILocator SearchResults => _page.Locator(".features_items > .col-sm-4");
        private ILocator CategoryTops => _page.Locator("a[href='#Women']");
        private ILocator SubCategoryTshirts => _page.Locator("a[href='/category_products/1']");
        private ILocator BrandFilter => _page.Locator("a[href='/brand_products/Polo']");

        public async Task NavigateToProductsAsync()
        {
            await ProductsNav.ClickAsync();
            await _page.WaitForSelectorAsync(".features_items");
        }

        public async Task SearchProductAsync(string keyword)
        {
            await SearchBox.FillAsync(keyword);
            await SearchButton.ClickAsync();
            await _page.WaitForSelectorAsync(".features_items");
        }

        public async Task<bool> AreSearchResultsVisibleAsync() =>
            await SearchResults.First.IsVisibleAsync();

        public async Task ApplyCategoryFilterAsync()
        {
            await CategoryTops.ClickAsync(); // Expand 'Women'
            await SubCategoryTshirts.ClickAsync(); // Click 'Tops > Tshirts'
        }

        public async Task ApplyBrandFilterAsync()
        {
            await BrandFilter.ClickAsync(); // Apply Polo brand
        }

        public async Task<bool> DoResultsContainKeywordAsync(string keyword)
        {
            var texts = await _page.Locator(".features_items .productinfo p").AllInnerTextsAsync();
            return texts.Any(text => text.ToLower().Contains(keyword.ToLower()));
        }
    }
}
