using System;
using System.Net;
using uhttpsharp;
using uhttpsharp.Listeners;
using uhttpsharp.RequestProviders;
using uhttpsharp.Handlers;

namespace Cake.UServer {
	public class UHttpServer {

		private readonly UServerSettings _settings;
		private HttpServer _httpServer = new HttpServer(new HttpRequestProvider());

		public UHttpServer(UServerSettings settings) {
			_settings = settings;
		}

		public void Start() {
			var port = _settings.Port;
			_httpServer.Use(new TcpListenerAdapter(new System.Net.Sockets.TcpListener(IPAddress.Loopback, port)));

			_httpServer.Use((context, next) => {
				Console.WriteLine(context.Request.Uri);
				return next();
			});


			_httpServer.Use(new UFileHandler() { HttpRootDirectory = _settings.Path } );
			_httpServer.Start();
		}
	}
}

