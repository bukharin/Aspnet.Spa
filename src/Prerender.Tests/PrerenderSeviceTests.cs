using NUnit.Framework;
using PrerenderService.Service;

namespace Prerender.Tests
{
    [TestFixture]
    public class PrerenderSeviceTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            _renderer = new SnapshotRenderer(new TestPrerendererConfiguration());
        }

        #endregion

        private SnapshotRenderer _renderer;

        [Test]
        public void TestWithPrerendererIOIntegration()
        {
            PrerenderResult result = _renderer.RenderPage("https://www.google.com").Result;

            Assert.NotNull(result.Content);
            Assert.IsTrue(result.Content.Contains("<title>Google</title>"));
        }
    }
}