using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Google.Apis.Calendar.v3.Data;
using HelloBooks.Utilities;

namespace HelloBooks.Models
{
	public class ReadingAvailability
	{
		public int Id { get; set; }


		[Display(Name = "Starting Date and Time")]
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime ReadingStartTime { get; set; }

		[Display(Name = "Ending Date and Time")]
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime ReadingEndTime { get; set; }

		public virtual ApplicationUser User { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		[Required]
		public string CalenderEventId { get; set; }



	}
}