using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TelerivetExample.Models;
using TelerivetExample.Services;

namespace TelerivetExample.Managers
{
    public partial class UserProjectManager : DBService
    {

        /// <summary>
        /// Saves or updates a record in the UserProject table.
        /// returns the id of the inserted/updated record. -1 otherwise
        /// Throw exception with message value 'EXISTS' if the data is duplicate
        /// </summary>
        public static int InsertOrUpdate(UserProject aUserProject)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@UserProjectID", aUserProject.UserProjectID);
                vParams.Add("@ProjectCode", aUserProject.ProjectCode);
                vParams.Add("@Amount", aUserProject.Amount);
                vParams.Add("@Pin", aUserProject.Pin);
                vParams.Add("@Status", aUserProject.Status);
                vParams.Add("@FromNumber", aUserProject.FromNumber);
                return vConn.QuerySingle<int>("UserProjectInsertOrUpdate", vParams, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Selects the Single object of UserProject table.
        /// </summary>
        public static UserProject GetUserProject(int aUserProjectID)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@UserProjectID", aUserProjectID);
                return vConn.Query<UserProject>("UserProjectSelect", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// Removes a record from the UserProject table.
        /// returns True if value was removed successfully else false
        /// Throw exception with message value 'EXISTS' if the data is duplicate
        /// </summary>
        public static bool Delete(int aUserProjectID)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("UserProjectID", aUserProjectID);
                int iResult = vConn.Execute("UserProjectDelete", vParams, commandType: CommandType.StoredProcedure);
                return iResult == -1;
            }
        }
        /// <summary>
        /// Selects all records from the UserProject table.
        /// </summary>
        public static IEnumerable<UserProject> SelectAll()
        {
            using (var vConn = OpenConnection())
            {
                return vConn.Query<UserProject>("UserProjectSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }
        /// <summary>
        /// Selects all records from the UserProject table by a foreign key.
        /// </summary>
        public static IEnumerable<UserProject> SelectAllByUserID(int userID)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@UserID", userID);
                return vConn.Query<UserProject>("UserProjectSelectAll", vParams, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static int ApproveProject(int requestID, string projectCode)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@ProjectCode", projectCode);
                vParams.Add("@RequestID", requestID);
                return vConn.QuerySingle<int>("ApproveProject", vParams, commandType: CommandType.StoredProcedure);
            }
        }

        public static int ChangeProjectPin(UserProject user)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@ProjectCode", user.ProjectCode);
                vParams.Add("@RequestID", user.RequestID);
                vParams.Add("@Pin", user.Pin);
                return vConn.QuerySingle<int>("ChangeProjectPin", vParams, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
