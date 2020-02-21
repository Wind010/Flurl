using System;
using System.Net;
using System.Net.Http;

namespace Flurl.Http.Configuration
{
	/// <summary>
	/// Implementation of a HttClientFactory that allows WebProxy to be configured.
	/// </summary>
	public class ProxyHttpClientFactory : DefaultHttpClientFactory
	{
		private IWebProxy _webProxy;

		/// <summary>
		/// Implementation of a HttClientFactory that allows WebProxy to be configured.
		/// </summary>
		/// <param name="webProxy"><see cref="IWebProxy"></see></param>
		public ProxyHttpClientFactory(IWebProxy webProxy)
		{
			_webProxy = webProxy ?? throw new ArgumentNullException(nameof(webProxy));
		}


		/// <summary>
		/// Overrides the base.CreateMessageHandler with the passed in IWebProxy above.  Ensure that any
		/// modifications of the base.CreateMessageHandler is reflected here in order not to lose Flurl.Http functionality.
		/// </summary>
		/// <returns><see cref="HttpClientHandler"></see></returns>
		public override HttpMessageHandler CreateMessageHandler()
		{
			var httpClientHandler = new HttpClientHandler
			{
				// flurl has its own mechanisms for managing cookies and redirects
				UseCookies = false,
				AllowAutoRedirect = false,
				Proxy = _webProxy,
				UseProxy = true
			};

			ConfigureAutomaticDecompression(httpClientHandler);

			return httpClientHandler;
		}
	}
}