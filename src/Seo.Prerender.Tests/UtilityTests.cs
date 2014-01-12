using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Seo.PrerenderService.Web;
using Seo.PrerenderService.Web.Configuration;

namespace Seo.Prerender.Tests
{
    [TestFixture]
    public class UtilityTests
    {
        private PrerenderConfig _config;

        [SetUp]
        public void Init()
        {
            _config = new PrerenderConfig();
            _config.Configuration = new PrerenderSection();
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithoutEscaped()
        {
            var testRequestUri = "http://localhost:4434/path/to/url";
            var url = Utility.GetSnapshotUrl(new Uri(testRequestUri));
            Assert.AreEqual(url, testRequestUri);
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithoutEscapedAndPort()
        {
            var testRequestUri = "http://localhost/path/to/url";
            var url = Utility.GetSnapshotUrl(new Uri(testRequestUri));
            Assert.AreEqual(url, testRequestUri);
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithoutEscapedAndHttps()
        {
            var testRequestUri = "https://localhost/path/to/url";
            var url = Utility.GetSnapshotUrl(new Uri(testRequestUri));
            Assert.AreEqual(url, testRequestUri);
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithEscaped()
        {
            var testRequestUri = "http://localhost:4434/?_escaped_fragment_=/about";
            var url = Utility.GetSnapshotUrl(new Uri(testRequestUri));
            Assert.AreEqual(url, "http://localhost:4434/about");
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithEscapedAndOtherQueryString()
        {
            var testRequestUri = "http://localhost:4434/?_escaped_fragment_=/about&myvalue=3";
            var url = Utility.GetSnapshotUrl(new Uri(testRequestUri));
            Assert.AreEqual(url, "http://localhost:4434/about?myvalue=3");
        }

        //[Test]
        //public void TestGetCorrectSnapshotUrlWithEscapedQueryStringAndRelativePath()
        //{
        //    var testRequestUri = "http://localhost:4434/path/to/resource?_escaped_fragment_=/about?myvalue=3";
        //    var url = Utility.GetSnapshotUrl(new Uri(testRequestUri));
        //    Assert.AreEqual(url, "http://localhost:4434/about?myvalue=3");
        //}

        [Test]
        public void TestCorrectRobotRequestDetection()
        {
            var requestUrl = "http://localhost:4434/path/to/resource?q=2";
            var userAgent = "chrome";

            var requestParams = new RequestParams
            {
                RequestUri = new Uri(requestUrl),
                UserAgent = userAgent
            };

            Assert.IsFalse(Utility.IsRequestShouldBePrerendered(requestParams, _config));

            requestParams.UserAgent = "Googlebot";
            Assert.IsTrue(Utility.IsRequestShouldBePrerendered(requestParams, _config));

            requestParams.UserAgent = "Googlebot-Mobile";
            Assert.IsTrue(Utility.IsRequestShouldBePrerendered(requestParams, _config));
        }

        [Test]
        public void TestCorrectRobotRequestDetectionByFragment()
        {
            var requestUrl = "http://localhost:4434/?q=2&_escaped_fragment_=/about";
            var userAgent = "Googlebot";

            var requestParams = new RequestParams
            {
                RequestUri = new Uri(requestUrl),
                UserAgent = userAgent
            };

            Assert.IsTrue(Utility.IsRequestShouldBePrerendered(requestParams, _config));
        }

        [Test]
        public void TestCorrectPrerenderingIgnoredFile()
        {
            var requestUrl = "http://localhost:4434/myfile.html";
            var userAgent = "Googlebot";

            var requestParams = new RequestParams
            {
                RequestUri = new Uri(requestUrl),
                UserAgent = userAgent
            };

            Assert.IsFalse(Utility.IsRequestShouldBePrerendered(requestParams, _config));
            requestParams.UserAgent = "chrome";
            Assert.IsFalse(Utility.IsRequestShouldBePrerendered(requestParams, _config));
        }
    }
}
