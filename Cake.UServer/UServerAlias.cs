using System;
using System.IO;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.UServer {

	/// <summary>
	/// Contains funtionalyity for serving static files.
	/// </summary>
	[CakeAliasCategory("UServer")]
	public static class UServerUAlias {

        /// <summary>
        /// Start static server in current working directory.
        /// </summary>
        /// <example>
        /// UServer(8080);
        /// </example>
        /// <param name="context"></param>
        /// <param name="port"></param>
		[CakeMethodAlias]
		public static void UServer(this ICakeContext context, int port) {
			var current = new DirectoryInfo("./").FullName;
			var settings = new UServerSettings { Port = port, Path = current };
			UServer(context, settings);

		}


        /// <summary>
        /// Start static server that is specified by settings.
        /// </summary>
        /// <example>
        /// UServer(new UServerSettings {
        ///     Port = 8080,
        ///     Path = "./dist"
        /// });
        /// </example>
        /// <param name="context"></param>
        /// <param name="settings"></param>
		[CakeMethodAlias]
		public static void UServer(this ICakeContext context, UServerSettings settings) {
			var server = new UHttpServer(settings);
			server.Start();

			while (Console.ReadLine() != "q") {}
		}
	}
}

