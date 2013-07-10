using NUnit.Framework;
using White.Core.UIItems;
using White.Core.UITests.Testing;

namespace White.Core.UITests.UIItems
{
    [TestFixture, WPFCategory]
    public class WPFHyperlinkTest : ControlsActionTest
    {
        [Fact]
        public void ClickHyperlink()
        {
            WPFLabel labelContainingHyperlink = Window.Get<WPFLabel>("linkLabel");
            labelContainingHyperlink.Hyperlink("Link Text").Click();
            AssertResultLabelText("Link label clicked");
        }

        [Fact]
        public void ClickHyperlinkLlaunchedModal()
        {
            WPFLabel labelContainingHyperlink = Window.Get<WPFLabel>("hyperlinkLaunchesModal");
            labelContainingHyperlink.Hyperlink("Launch Modal").Click();
            CloseModal(Window);
        }
    }
}