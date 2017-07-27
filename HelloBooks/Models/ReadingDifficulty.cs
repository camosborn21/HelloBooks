using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloBooks.Models
{
	public class ReadingDifficulty
	{
		public int Id { get; set; }
		public virtual ApplicationUser User { get; set; }

		[Display(Name = "Pages Per Hour")]
		public int PagesPerHour { get; set; }
	}
}