using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloBooks.Models
{
	public class BookPropertyType
	{
		public int id { get; set; }
		public string PropertyName { get; set; }
	}

	public class BookProperty
	{
		public int id { get; set; }
		public virtual BookPropertyType PropertyType { get; set; }
		public virtual Book Book { get; set; }
	}
}