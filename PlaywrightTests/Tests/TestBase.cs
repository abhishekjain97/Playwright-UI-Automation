using Microsoft.Playwright;

namespace PlaywrightTests
{
    public abstract class TestBase
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IBrowserContext _context;
        protected IPage _page;

        [SetUp]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            await _page.GotoAsync("https://automationexercise.com");

            // Accept cookies if the dialog appears
            await AcceptCookiesAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        private async Task AcceptCookiesAsync()
        {
            var cookieDialog = _page.Locator("div.fc-dialog-container");
            if (await cookieDialog.IsVisibleAsync())
            {
                var acceptButton = cookieDialog.Locator("button.fc-button.fc-cta-consent.fc-primary-button");
                if (await acceptButton.IsVisibleAsync())
                {
                    await acceptButton.ClickAsync();
                }
            }
        }
    }
}
