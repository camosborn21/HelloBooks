using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloBooks.Models
{
	public class Book
	{
		public int Id { get; set; }

		public virtual ApplicationUser User { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		[Required]
		public string Title { get; set; }

		public string Isbn { get; set; }

		[Display(Name = "Page Count")]
		public int TotalPageCount { get; set; }

		public bool DoneWithBook { get; set; }

		[Display(Name = "Thumbnail")]
		public string ThumbnailLink { get; set; }
	
		public virtual ICollection<Assignment> Assignments { get; set; }

		public virtual ICollection<BookCategoryPair> Categories { get; set; }
	}
}