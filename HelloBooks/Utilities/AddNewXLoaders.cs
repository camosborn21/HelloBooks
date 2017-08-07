using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloBooks.Utilities
{
	public class AddNewXLoaders
	{
		public string GetButtonManagementString(string buttonName, string hideTable, string showtable)
		{
			return
				"$('#"+buttonName +"').on('click',function() {$('#"+ showtable + "').show('fade', 1000);$('#"+ hideTable + "').hide();});";
		}
	}
}