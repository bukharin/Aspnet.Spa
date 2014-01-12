using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Seo.PrerenderService;

namespace Seo.Prerender.Tests
{
    [TestFixture]
    public class PrerenderSeviceTests
    {
        private SnapshotRenderer _renderer;
        [SetUp]
        public void Init()
        {
            _renderer = new SnapshotRenderer(new TestPrerendererConfiguration());
        }
        [Test]
        public void TestRenderer()
        {
            var result = _renderer.RenderPage(new Uri("https://www.google.com")).Result;

            Assert.NotNull(result.Content);
            Assert.IsTrue(result.Content.Contains("<title>Google</title>"));
        }
    }
}
