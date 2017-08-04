using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace HelloBooks.Models
{
	public class ReadingDifficultiesDropDownListViewModel
	{
		public int SelectedReadingDifficultyId { get; set; }
		public IEnumerable<SelectListItem> ReadingDifficulties { get; set; }
	}
}