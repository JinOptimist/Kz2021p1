using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
	public class BlobService : IBlobService
	{
		private readonly BlobServiceClient _blobServiceClient;
		private const string BlobContainerPhoto = "photo";
		private const string DefaultPic = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/600px-No_image_available.svg.png";

		public BlobService(BlobServiceClient blobServiceClient)
		{
			_blobServiceClient = blobServiceClient;
		}

		public async Task<Uri> GetPhotoAsync(string photoName)
		{
			BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerPhoto);
			BlobClient blobClient = containerClient.GetBlobClient(photoName);

			if (await blobClient.ExistsAsync())
			{
				return blobClient.Uri;
			};

			return new Uri(DefaultPic);
		}

		public async Task<Uri> UploadPhotoAsync(FileUpload fileUpload)
		{
			string fileName = fileUpload.CitizenId.ToString();

			BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerPhoto);
			BlobClient blobClient = containerClient.GetBlobClient(fileName);

			BlobHttpHeaders httpHeaders = new BlobHttpHeaders
			{
				ContentType = fileUpload.File.ContentType
			};

			await blobClient.UploadAsync(fileUpload.File.OpenReadStream(), httpHeaders);

			return blobClient.Uri;
		}
	}
}