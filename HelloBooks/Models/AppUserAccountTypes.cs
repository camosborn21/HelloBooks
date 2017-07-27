using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloBooks.Models
{
	public class AppUserAccountTypes
	{
		public int Id { get; set; }

		[Display(Name="Account Type")]
		public string AccountTypeName { get; set; }
	}
}