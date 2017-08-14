using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using HelloBooks.Utilities;

namespace HelloBooks.Models
{
	public class Assignment
	{
		public int Id { get; set; }

		[Required]
		public virtual Book Book { get; set; }

		[Required]
		public int BookId { get; set; }

		[Display(Name =  "Reading Difficulty")]
		public virtual ReadingDifficulty ReadingDifficulty { get; set; }

		public int ReadingDifficultyId { get; set; }

		//public IEnumerable<SelectListItem> GetReadingDifficulties(string UserId)
		//{

		//	IList<ReadingDifficulty> difficulties = Book.User.UserDefinedReadingDifficulties.Where(c => c.ApplicationUserId == UserId).OrderBy(m=>m.Id).ToList();
		//	var list = difficulties.Select(x => new SelectListItem
		//	{
		//		Value = x.Id.ToString(),
		//		Text = x.PagesPerHour.ToString(),
		//		Selected = true
		//	});
		//	return new SelectList(list, "Value", "Text");
		//}

		//public ReadingDifficultiesDropDownListViewModel Difficulties
		//{
		//	get
		//	{
		//		if (Book == null)
		//		{
		//			if (BookId != 0)
		//			{
		//				IApplicationDbContext db = new ApplicationDbContext();
		//				Book = db.Books.First(c => c.Id == BookId);
		//			}
		//		}
		//		return new ReadingDifficultiesDropDownListViewModel
		//		{
		//			ReadingDifficulties = GetReadingDifficulties(Book.ApplicationUserId)
		//		};
		//	}
		//}

		[Display(Name = "Assignment Name")]
		public string AssignmentName { get; set; }

		[Display(Name = "Due Date")]
		[DataType(DataType.Date)]
		public DateTime DueDate { get; set; }

		public int ModifiedPagesPerHour { get; set; }

		public virtual ICollection<RequiredPages> Pages { get; set; }

		public virtual ICollection<ReadingProgress> Reading { get; set; }

		public string GetModifiedDifficultyString()
		{
			return "Modified speed: " + ModifiedPagesPerHour + " pages per hour";
		}
	}

	public class RequiredPages
	{
		public int Id { get; set; }

		[Required]
		public virtual Assignment Assignment{ get; set; }
		[Required]
		public int AssignmentId { get; set; }

		[Required]
		[Display(Name = "Starting Page")]
		public int StartPage { get; set; }

		[Required]
		[Display(Name = "Ending Page")]
		public int LastPage { get; set; }
	}

	public class ReadingProgress
	{
		public int Id { get; set; }

		[Required]
		public virtual Assignment Assignment { get; set; }

		[Required]
		public int AssignmentId { get; set; }


		[Required]
		[Display(Name = "Starting Page")]
		public int StartPage { get; set; }

		[Required]
		[Display(Name = "Ending Page")]
		public int FinishPage { get; set; }

		[Required]
		[Display(Name = "Starting Date and Time")]
		public DateTime StartDateTime { get; set; }

		[Required]
		[Display(Name = "Ending Date and Time")]
		public DateTime EndDateTime { get; set; }


		public string GetTimeSpentReading(ReadingProgress progress)
		{
			string result;
			TimeSpan timeRead = progress.EndDateTime - progress.StartDateTime;
			switch (timeRead.Hours)
			{
				case 1:
					result = "1 hour";
					break;
				default:
					result = timeRead.Hours + " hours";
					break;
			}
			switch (timeRead.Minutes)
			{
				case 0:
					break;
				case 1:
					result = result + " 1 minute";
					break;
				default:
					result = result + " " + timeRead.Minutes + " minutes";
					break;
			}
			return result;
		}

	}
}