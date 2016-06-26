using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using uhttpsharp;
using uhttpsharp.Headers;
using MimeTypes;
using System.Linq;

namespace Cake.UServer {
	public class UFileHandler : IHttpRequestHandler {

		public string HttpRootDirectory { get; set; }

		private List<string> _defaults = new List<string> { "index.html", "default.html" };

		private string GetContentType(string path) {
			var extension = Path.GetExtension(path) ?? string.Empty;
			return MimeTypeMap.GetMimeType(extension);
		}

		private string GetDefault() {
			var match = "";
			var files = new DirectoryInfo(HttpRootDirectory).GetFiles("*.*");
			_defaults.ForEach(def => {
				var ok = files.Where(x => x.Name == def).FirstOrDefault();
				if (ok != null) match = ok.Name;
			});
			return match;
		}

		public async Task Handle(IHttpContext context, System.Func<Task> next) {
			var requestPath = context.Request.Uri.OriginalString.TrimStart('/');

			if (requestPath.Trim() == string.Empty) requestPath = GetDefault();

			var httpRoot = Path.GetFullPath(HttpRootDirectory ?? ".");
			var path = Path.GetFullPath(Path.Combine(httpRoot, requestPath));

			if (!File.Exists(path)) {
				await next().ConfigureAwait(false);

				return;
			}

			context.Response = new HttpResponse(GetContentType(path), File.OpenRead(path), context.Request.Headers.KeepAliveConnection());
		}
	}
}

