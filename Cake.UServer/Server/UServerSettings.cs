using System;
namespace Cake.UServer {

	/// <summary>
	/// Specifies a set of values that are used to start server.
	/// </summary>
	public class UServerSettings {
		/// <summary>
		/// Gets or sets server port.
		/// </summary>
		public int Port { set; get; }
		/// <summary>
		/// Gets or sets root directory of staitc server.
		/// </summary>
		public string Path { set; get; }
	}
}

