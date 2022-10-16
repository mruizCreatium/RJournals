using JournalApp.UWP;
using JournalApp.Views;
using System;
using System.Net;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace JournalApp.UWP
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                //Control.Source = new Uri(string.Format("ms-appx-web:///Assets/pdfjs/web/viewer.html?file={0}", string.Format("ms-appx-web:///Assets/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
                Control.Settings.IsJavaScriptEnabled = true;

                //var pdfId = Guid.NewGuid().ToString();
                //var pdfName = $"ms-appx:///{pdfId}.pdf";
                //var pdfUri = new Uri(pdfName);
                //string lPath = pdfUri.LocalPath;



                ////await StorageFile.CreateStreamedFileFromUriAsync(pdfId + ".pdf", new Uri(pdfName), null);
                ////var targetFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(pdfName));
                ////targetFile.Path


                //string tempFilePath = await StorageHelper.DownloadPdF(customWebView.Uri, lPath.Substring(1));
                //if (!string.IsNullOrEmpty(tempFilePath))
                //{
                //var fileUrl = $"{WebUtility.UrlEncode(tempFilePath)}";
                //var fileUrl1 = WebUtility.UrlEncode($"{tempFilePath}");
                //var fileUrl2 = $"{tempFilePath}";
                //Control.Source = new Uri($"ms-appx-web:///Assets/pdfjs/web/viewer.html?file={fileUrl1}");
                Control.Source = new Uri(string.Format("ms-appx-web:///Assets/pdfjs/web/viewer.html?file={0}", string.Format("ms-appx-web:///Assets/Content/{0}", WebUtility.UrlEncode("BookPreview2-Ch18-Rel0417.pdf"))));
                //}
            }


        }




    }
}
