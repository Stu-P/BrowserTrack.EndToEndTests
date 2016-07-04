using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SpecflowUI.Framework.Util
{
    /// <summary>
    /// solution for exception
    /// System.Net.WebException: 
    /// The underlying connection was closed: Could not establish trust relationship for the SSL/TLS secure channel. ---> System.Security.Authentication.AuthenticationException: The remote certificate is invalid according to the validation procedure.
    /// </summary>

    public static class SSLCertificateUtils
    {

        public static void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=

                delegate(
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }
    }
}