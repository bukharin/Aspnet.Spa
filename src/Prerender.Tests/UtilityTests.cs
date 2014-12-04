using System;
using NUnit.Framework;
using PrerenderService;
using PrerenderService.Configuration;

namespace Prerender.Tests
{
    [TestFixture]
    public class UtilityTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            _config = new PrerenderConfig();
            _config.Configuration = new PrerenderSection();
        }

        #endregion

        private PrerenderConfig _config;

        [Test]
        public void TestCorrectPrerenderingIgnoredFile()
        {
            string requestUrl = "http://localhost:4434/myfile.html";
            string userAgent = "Googlebot";

            var requestParams = new RequestParams
                                    {
                                        RequestUri = new Uri(requestUrl),
                                        UserAgent = userAgent
                                    };

            Assert.IsFalse(Utility.IsRequestShouldBePrerendered(requestParams, _config));
            requestParams.UserAgent = "chrome";
            Assert.IsFalse(Utility.IsRequestShouldBePrerendered(requestParams, _config));
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
            string requestUrl = "http://localhost:4434/path/to/resource?q=2";
            string userAgent = "chrome";

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
            string requestUrl = "http://localhost:4434/?q=2&_escaped_fragment_=/about";
            string userAgent = "Googlebot";

            var requestParams = new RequestParams
                                    {
                                        RequestUri = new Uri(requestUrl),
                                        UserAgent = userAgent
                                    };

            Assert.IsTrue(Utility.IsRequestShouldBePrerendered(requestParams, _config));
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithEscaped()
        {
            string testRequestUri = "http://localhost:4434/?_escaped_fragment_=/about";
            string url = Utility.GetSnapshotUrl(new Uri(testRequestUri), true);
            Assert.AreEqual(url, "http://localhost:4434/about");
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithEscapedAndOtherQueryString()
        {
            string testRequestUri = "http://localhost:4434/?_escaped_fragment_=/about&myvalue=3";
            string url = Utility.GetSnapshotUrl(new Uri(testRequestUri), true);
            Assert.AreEqual(url, "http://localhost:4434/about?myvalue=3");
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithoutEscaped()
        {
            string testRequestUri = "http://localhost:4434/path/to/url";
            string url = Utility.GetSnapshotUrl(new Uri(testRequestUri), true);
            Assert.AreEqual(url, testRequestUri);
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithoutEscapedAndHttps()
        {
            string testRequestUri = "https://localhost/path/to/url";
            string url = Utility.GetSnapshotUrl(new Uri(testRequestUri), true);
            Assert.AreEqual(url, testRequestUri);
        }

        [Test]
        public void TestGetCorrectSnapshotUrlWithoutEscapedAndPort()
        {
            string testRequestUri = "http://localhost/path/to/url";
            string url = Utility.GetSnapshotUrl(new Uri(testRequestUri), true);
            Assert.AreEqual(url, testRequestUri);
        }
    }
}