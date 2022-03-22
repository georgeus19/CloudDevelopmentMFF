using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApplication.Pages
{
	public class IndexModel : PageModel
	{

		public List<BlobItem>? Blobs { get; set; }

		public void OnGet()
		{
			//Blobs = new List<BlobItem>();
            Blobs = CreateClient().GetBlobs().ToList();
        }

		public FileStreamResult OnGetDownloadBlob(string blobName)
		{
			var blobClient = CreateClient().GetBlobClient(blobName);
			var contentType = blobClient.GetProperties().Value.ContentType;
			var stream = blobClient.OpenRead();
			return new FileStreamResult(stream, contentType);
		}

		private BlobContainerClient CreateClient()
		{
			Uri containerEndpoint = new Uri("https://cloudmffslab3storageacc.blob.core.windows.net/lab3container");
			
			return new Azure.Storage.Blobs.BlobContainerClient(containerEndpoint, new Azure.Identity.ManagedIdentityCredential("977e9dcc-5e1f-4dec-be7b-1da636ca0146"));
			return new Azure.Storage.Blobs.BlobContainerClient(containerEndpoint, new Azure.Identity.DefaultAzureCredential());
		}
	}
}