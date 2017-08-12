using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace HelloBooks.Utilities
{
	public class UserCalendarService
	{
		public IList<CalendarListEntry> GetUserOwnedCalendars()
		{
			var service = new CalendarService();
			var calendarData = service.CalendarList.List().Execute();
			return calendarData.Items.Where(c=>c.AccessRole=="owner").ToList();
		}
	}
}