using System;

namespace Cake.UServer.Console {
	class MainClass {
		public static void Main(string[] args) {

			var settings = new UServerSettings{ Port = 8000, Path = "/Users/wk/Source/project/practika/sale-tracking-admin" };
			UServerUAlias.UServer(null, settings);

			while(System.Console.ReadLine() != "q") { }
		}
	}
}
