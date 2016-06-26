using System;
using System.IO;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.UServer {
	public static class Alias {

		[CakeMethodAlias]
		public static void UServer(this ICakeContext context, int port) {
			var current = new DirectoryInfo("./").FullName;
			var settings = new UServerSettings { Port = port, Path = current };
			UServer(context, settings);

		}

		[CakeMethodAlias]
		public static void UServer(this ICakeContext context, UServerSettings settings) {
			var server = new UHttpServer(settings);
			server.Start();
		}
	}
}

