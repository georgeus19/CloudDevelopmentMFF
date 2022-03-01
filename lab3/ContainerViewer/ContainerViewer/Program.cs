using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using StreamReader connectionStringReader = new StreamReader("./../../../connectionString.txt");
string conString = connectionStringReader.ReadLine();
BlobContainerClient client = new BlobContainerClient(conString, "lab3container");
List<BlobItem> blobs = client.GetBlobs().ToList();

Console.WriteLine("Names of all blobs in the container:");
foreach(BlobItem blob in blobs) {
    Console.WriteLine(blob.Name);
}

string blobName = "readme.txt";
BlobClient blobClient = client.GetBlobClient(blobName);

Console.WriteLine($"Content of blob {blobName}:");
using (Stream stream = blobClient.OpenRead()) {
    using (StreamReader sr = new StreamReader(stream)) {
        string? line = sr.ReadLine();
        while (line != null) {
            Console.WriteLine(line);
            line = sr.ReadLine();
        }
    }
    

}


