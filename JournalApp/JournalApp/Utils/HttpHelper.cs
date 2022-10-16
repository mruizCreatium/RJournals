using System.Net.Http;

namespace JournalApp.Utils
{
    public class HttpHelper
    {
        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
