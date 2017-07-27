using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloBooks.Models
{
	public class BookCategory
	{
		public int Id { get; set; }

		public int ParentId { get; set; }

		public string Category { get; set; }

		public virtual ICollection<BookCategoryPair> BooksInCategory { get; set; }

		public ICollection<BookCategory> SubCategories { get; set; }
	}

	public class BookCategoryPair
	{
		public int Id { get; set; }

		
		public  virtual Book Book { get; set; }

		public virtual BookCategory Category { get; set; }
	}
}