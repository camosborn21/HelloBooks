using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Threading;
using System.Threading.Tasks;
using HelloBooks.Models;


namespace HelloBooks.Utilities
{
	public class UserCalendarService
	{

		private ApplicationUser _user;
		private IApplicationDbContext db;
		private CalendarService _service;

		public CalendarService Service
		{
			get
			{
				if (_service == null)
				{
					Task<UserCredential> credential = GetUserCredential();
					_service = new CalendarService(new BaseClientService.Initializer()
					{
						HttpClientInitializer = credential.Result
					}
					);
				}
				return _service;
			}
		}

		
		public UserCalendarService(ApplicationUser user)
		{
			_user = user;
			db = new ApplicationDbContext();
		}

		public UserCalendarService(ApplicationUser user, IApplicationDbContext dbContext)
		{
			_user = user;
			db = dbContext;
		}

		public Calendar GetCalendarById(string id)
		{
			return Service.Calendars.Get(id).Execute();

		}

		public string CreateNewCalendarWithId()
		{
			Calendar newCalendar = new Calendar()
			{
				Description = "The HelloBooks.io reading calendar where availability can be established by creating reading events. Because this calendar is exclusive to HelloBooks.io any event created on this calendar will be treated as reading availability.",
				Summary = "HelloBooks.io Reading Calendar"
			};
			Calendar createdCalendar = Service.Calendars.Insert(newCalendar).Execute();
			return createdCalendar.Id;
		}

		public IList<CalendarListEntry> GetUserOwnedCalendars()
		{
			
			var calendarData = Service.CalendarList.List().Execute();
			return calendarData.Items.Where(c => c.AccessRole == "owner").ToList();
		}

		public IList<Event> GetUserEvents()
		{
			//[9/8/2017 00:30] Cameron Osborn: Get user's events from calendar
			Events userEvents = Service.Events.List(_user.GoogleCalendarId).Execute();
			return userEvents.Items;

		}
		public void SyncEventsFromGoogle()
		{
			if (_user.GoogleCalendarId == null) return;
			
			
			IList<Event> userEvents = GetUserEvents();
			foreach (var userEvent in userEvents)
			{
				if (db.ReadingAvailabilities.Where(av => av.ApplicationUserId == _user.Id).ToList().Count != 0)
				{
					if (db.ReadingAvailabilities.Count(r => (r.CalenderEventId == userEvent.Id) &&
					                                        (r.User.GoogleCalendarId == _user.GoogleCalendarId)) != 0 ) continue;
				}
				if (_user.GoogleCalendarIsUnique == true ){
					if (userEvent.Start.DateTime == null) continue;
					if (userEvent.End.DateTime != null)
					{
						ReadingAvailability newAvailability = new ReadingAvailability()
						{
							ApplicationUserId = _user.Id,
							CalenderEventId = userEvent.Id,
							ReadingStartTime = (DateTime) userEvent.Start.DateTime,
							ReadingEndTime = (DateTime) userEvent.End.DateTime

						};
						db.ReadingAvailabilities.Add(newAvailability);
						db.SaveChanges();
					}
				}
				else
				{
					
				}
			}
			
		}

		private async Task<UserCredential> GetUserCredential()
		{
			string googleUserId = _user.Logins.First(c => c.LoginProvider == "Google").ProviderKey;
			
			var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets()
			{
				ClientId = "80693274979-1ia591l1g7cu48dc91j4drbngjocl4m4.apps.googleusercontent.com",
				ClientSecret = "r9yvhLhl7wznmF4UgL04yDNv"
			}, new[] { CalendarService.Scope.Calendar }, googleUserId, CancellationToken.None);

			return credential;
		}

	}
}