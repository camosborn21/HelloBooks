using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Books.v1;
using Google.Apis.Services;

namespace HelloBooks.Utilities
{
	public class IsbnLookup
	{
		public string Title { get; set; }
		public string ResultIsbn { get; set; }
		public bool ReturnedOneVolume { get; private set; }
		public int? PageCount { get; set; }
		public string ThumbnailLink { get; set; }
		public string ReturnIsbn { get; set; }

		public IsbnLookup()
		{
			
		}

		public IsbnLookup(string searchString)
		{
			SearchIsbn(searchString);
		}

		public void SearchIsbn(string searchString)
		{
			ResultIsbn = GetNumbers(searchString);
			var service = new BooksService(new BaseClientService.Initializer());
			var bookData = service.Volumes.List("isbn:" + ResultIsbn).ExecuteAsync();
			if (bookData.Result.TotalItems == 1)
			{
				ReturnedOneVolume = true;
				Title= bookData.Result.Items.First().VolumeInfo.Title;
				PageCount = bookData.Result.Items.First().VolumeInfo.PageCount;
				ThumbnailLink = bookData.Result.Items.First().VolumeInfo.ImageLinks.SmallThumbnail;
			}
			else
			{
				ReturnedOneVolume = false;
				ReturnIsbn = searchString;
			}

		}

		private static string GetNumbers(string input)
		{
			return new string(input.Where(c => char.IsDigit(c)).ToArray());
		}
	}
}