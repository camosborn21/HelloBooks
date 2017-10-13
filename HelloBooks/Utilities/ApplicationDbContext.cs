using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HelloBooks.Models;
using Microsoft.Owin.Security;

namespace HelloBooks.Utilities
{
	public interface IApplicationDbContext
	{
		//IDbSet<ReadingAvailability> UserReadingAvailabilities { get; set; }
		IDbSet<Book> Books { get; set; }
		IDbSet<Assignment> Assignments { get; set; }
		IDbSet<RequiredPages> RequiredPages { get; set; }
		IDbSet<ReadingProgress> ReadingProgress { get; set; }
		IDbSet<BookCategoryPair> CategoryPairs { get; set; }
		IDbSet<BookCategory> BookCategories { get; set; }
		IDbSet<BookPropertyType> BookPropertyTypes { get; set; }
		IDbSet<BookProperty> BookProperties { get; set; }
		IDbSet<ReadingDifficulty> ReadingDifficulties { get; set; }
		IDbSet<ReadingAvailability> ReadingAvailabilities { get; set; }

		int SaveChanges();
	}
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
	{
        
		public ApplicationDbContext()
			: base(GetConnectionString(), throwIfV1Schema: false)
		{
		}

	    private static string GetConnectionString()
	    {
	        if (System.Environment.UserDomainName == "OSBORNWEBDEV")
	        {
	            return ("LaptopDevConnection");
	        }
	        return ("SecondaryConnection");
	    }

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
		//public IDbSet<ReadingAvailability> UserReadingAvailabilities { get; set; }
		public IDbSet<Book> Books { get; set; }
		public IDbSet<Assignment> Assignments { get; set; }
		public IDbSet<RequiredPages> RequiredPages { get; set; }
		public IDbSet<ReadingProgress> ReadingProgress { get; set; }
		public IDbSet<BookCategoryPair> CategoryPairs { get; set; }
		public IDbSet<BookCategory> BookCategories { get; set; }
		public IDbSet<BookPropertyType> BookPropertyTypes { get; set; }
		public IDbSet<BookProperty> BookProperties{ get; set; }
		public IDbSet<ReadingDifficulty> ReadingDifficulties { get; set; }

		public IDbSet<ReadingAvailability> ReadingAvailabilities { get; set; }

		//public System.Data.Entity.DbSet<HelloBooks.Models.ApplicationUser> ApplicationUsers { get; set; }

		//public System.Data.Entity.DbSet<HelloBooks.Models.ApplicationUser> ApplicationUsers { get; set; }
		//public System.Data.Entity.DbSet<HelloBooks.Models.ApplicationUser> ApplicationUsers { get; set; }
	}

	public class FakeApplciationDbContext : IApplicationDbContext
	{

		public int SaveChanges()
		{
			return 0;
		}
		//public IDbSet<ReadingAvailability> UserReadingAvailabilities { get; set; }
		public IDbSet<Book> Books { get; set; }
		public IDbSet<Assignment> Assignments { get; set; }
		public IDbSet<RequiredPages> RequiredPages { get; set; }
		public IDbSet<ReadingProgress> ReadingProgress { get; set; }
		public IDbSet<BookCategoryPair> CategoryPairs { get; set; }
		public IDbSet<BookCategory> BookCategories { get; set; }
		public IDbSet<BookPropertyType> BookPropertyTypes { get; set; }
		public IDbSet<BookProperty> BookProperties { get; set; }
		public IDbSet<ReadingDifficulty> ReadingDifficulties { get; set; }
		public IDbSet<HelloBooks.Models.ApplicationUser> ApplicationUsers { get; set; }
		public IDbSet<ReadingAvailability> ReadingAvailabilities { get; set; }
	}
}