using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApplication.Pages
{
	public class IndexModel : PageModel
	{

		// Add connection string if you want to run it locally!!!
		private const string AzureStorageConnectionString = ":))))";
		private const string AzureStorageContainerName = "lab3container";

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
			return new Azure.Storage.Blobs.BlobContainerClient(AzureStorageConnectionString, AzureStorageContainerName);
		}
	}
}