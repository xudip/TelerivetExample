using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TelerivetExample.Models;
using TelerivetExample.Services;

namespace TelerivetExample.Managers
{
    public partial class UserManager : DBService
    {

        /// <summary>
        /// Saves or updates a record in the User table.
        /// returns the id of the inserted/updated record. -1 otherwise
        /// Throw exception with message value 'EXISTS' if the data is duplicate
        /// </summary>
        public static int InsertOrUpdate(User aUser)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@UserID", aUser.UserID);
                vParams.Add("@Name", aUser.Name);
                vParams.Add("@Phone", aUser.Phone);
                vParams.Add("@Pin", aUser.Pin);
                vParams.Add("@Status", aUser.Status);
                return vConn.QuerySingle<int>("UserInsertOrUpdate", vParams, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Selects the Single object of User table.
        /// </summary>
        public static User GetUser(int aUserID)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@UserID", aUserID);
                return vConn.Query<User>("UserSelect", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// Removes a record from the User table.
        /// returns True if value was removed successfully else false
        /// Throw exception with message value 'EXISTS' if the data is duplicate
        /// </summary>
        public static bool Delete(int aUserID)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("UserID", aUserID);
                int iResult = vConn.Execute("UserDelete", vParams, commandType: CommandType.StoredProcedure);
                return iResult == -1;
            }
        }
        /// <summary>
        /// Selects all records from the User table.
        /// </summary>
        public static IEnumerable<User> SelectAll()
        {
            using (var vConn = OpenConnection())
            {
                return vConn.Query<User>("UserSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }


        public static int ApproveUser(string fromNumber)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@Phone", fromNumber);
                return vConn.QuerySingle<int>("ApproveUser", vParams, commandType: CommandType.StoredProcedure);
            }
        }

        public static int ChangeUserPin(User user)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@Name", user.Name);
                vParams.Add("@Phone", user.Phone);
                vParams.Add("@Pin", user.Pin);
                return vConn.QuerySingle<int>("ChangeUserPin", vParams, commandType: CommandType.StoredProcedure);
            }
        }

        public static int GetUserByPhoneAndPin(LoginModel user)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@Name", user.Name);
                vParams.Add("@Phone", user.Phone);
                vParams.Add("@Pin", user.Pin);
                return vConn.Query<int>("GetUserByPhoneAndPin", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public static string GetUserRoleByID(int userID)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@UserID", userID);
                return vConn.Query<string>("GetUserRoleByID", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }        
    }
}
