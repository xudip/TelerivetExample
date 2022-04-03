using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TelerivetExample.Models;
using TelerivetExample.Services;

namespace TelerivetExample.Managers
{
	 public partial class EventManager : DBService
		 {

		/// <summary>
		/// Saves or updates a record in the Event table.
		/// returns the id of the inserted/updated record. -1 otherwise
		/// Throw exception with message value 'EXISTS' if the data is duplicate
		/// </summary>
		public static int InsertOrUpdate(Event aEvent)
		{
			 using (var vConn = OpenConnection())
				 {
				 var vParams = new DynamicParameters();
					 vParams.Add("@EventID",aEvent.EventID);
					 vParams.Add("@Content",aEvent.Content);
					 vParams.Add("@FromNumber",aEvent.FromNumber);
					 vParams.Add("@PhoneID",aEvent.PhoneID);
			 return vConn.QuerySingle<int>("EventInsertOrUpdate", vParams, commandType: CommandType.StoredProcedure);
			 }
		}

		/// <summary>
		/// Selects the Single object of Event table.
		/// </summary>
		public static Event GetEvent(int aEventID)
		{
			 using (var vConn = OpenConnection())
				 {
				 var vParams = new DynamicParameters();
					 vParams.Add("@EventID",aEventID);
					 return vConn.Query<Event>("EventSelect", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
				 }
		}

		/// <summary>
		/// Removes a record from the Event table.
		/// returns True if value was removed successfully else false
		/// Throw exception with message value 'EXISTS' if the data is duplicate
		/// </summary>
		public static bool Delete(int aEventID)
		{
			 using (var vConn = OpenConnection())
				 {
				 var vParams = new DynamicParameters();
					 vParams.Add("EventID",aEventID);
					 int iResult = vConn.Execute("EventDelete", vParams, commandType: CommandType.StoredProcedure);
					 return iResult == -1;
				 }
		}
		/// <summary>
		/// Selects all records from the Event table.
		/// </summary>
		 public static IEnumerable<Event> SelectAll()
		{
			 using (var vConn = OpenConnection())
			{
				 return vConn.Query<Event>("EventSelectAll", commandType: CommandType.StoredProcedure).ToList();
			}
		}

		}
}
