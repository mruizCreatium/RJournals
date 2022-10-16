using Android.Content;
using JournalApp.Droid;
using JournalApp.Views;
using System.ComponentModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace JournalApp.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Settings.JavaScriptEnabled = true;
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.AllowContentAccess = true;
                Control.Settings.AllowFileAccess = true;
                Control.Settings.AllowFileAccessFromFileURLs = true;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;

                var uri = customWebView.Uri.Replace("http://localhost:5045", "https://rjournalsapi.creatium.mx");

                //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file=", WebUtility.UrlEncode(uri)));
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode("BookPreview2-Ch18-Rel0417.pdf"))));

            }
        }


        protected override void OnElementPropertyChanged(object sender,
            PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var _pdfJsWebView = Element as CustomWebView;
            if (e.PropertyName == nameof(CustomWebView.Uri)
                && _pdfJsWebView.Uri != null)
            {
                Control.LoadUrl(
                    $"file:///android_asset/pdfjs/web/viewer.html?" +
                    $"file={WebUtility.UrlEncode(_pdfJsWebView.Uri)}");
            }
        }
    }
}