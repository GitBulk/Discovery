using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMarkupMin.AspNet.Common;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNet4.Common;
using WebMarkupMin.Core;

namespace AzureOne
{
    public class WebMarkupMinConfig
    {
        public static void Configure(WebMarkupMinConfiguration configuration)
        {
            configuration.AllowMinificationInDebugMode = true;
            configuration.AllowCompressionInDebugMode = true;

            //DefaultCssMinifierFactory.Current = new MsAjaxCssMinifierFactory();
            //DefaultJsMinifierFactory.Current = new MsAjaxJsMinifierFactory();

            IHtmlMinificationManager htmlMinificationManager = HtmlMinificationManager.Current;
            HtmlMinificationSettings htmlMinificationSettings = htmlMinificationManager.MinificationSettings;
            htmlMinificationSettings.RemoveRedundantAttributes = true;
            htmlMinificationSettings.RemoveHttpProtocolFromAttributes = true;
            htmlMinificationSettings.RemoveHttpsProtocolFromAttributes = true;

            IXhtmlMinificationManager xhtmlMinificationManager = XhtmlMinificationManager.Current;
            XhtmlMinificationSettings xhtmlMinificationSettings = xhtmlMinificationManager.MinificationSettings;
            xhtmlMinificationSettings.RemoveRedundantAttributes = true;
            xhtmlMinificationSettings.RemoveHttpProtocolFromAttributes = true;
            xhtmlMinificationSettings.RemoveHttpsProtocolFromAttributes = true;

            IXmlMinificationManager xmlMinificationManager = XmlMinificationManager.Current;
            XmlMinificationSettings xmlMinificationSettings = xmlMinificationManager.MinificationSettings;
            xmlMinificationSettings.CollapseTagsWithoutContent = true;

            IHttpCompressionManager httpCompressionManager = HttpCompressionManager.Current;
            httpCompressionManager.CompressorFactories = new List<ICompressorFactory>
            {
                new DeflateCompressorFactory(),
                new GZipCompressorFactory()
            };
        }
    }
}