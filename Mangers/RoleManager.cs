using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TelerivetExample.Models;
using TelerivetExample.Services;

namespace TelerivetExample.Managers
{
    public partial class RoleManager : DBService
    {

        /// <summary>
        /// Saves or updates a record in the Roles table.
        /// returns the id of the inserted/updated record. -1 otherwise
        /// Throw exception with message value 'EXISTS' if the data is duplicate
        /// </summary>
        public static int InsertOrUpdate(Role aRole)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@Id", aRole.Id);
                vParams.Add("@Name", aRole.Name);
                return vConn.QuerySingle<int>("RolesInsertOrUpdate", vParams, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Selects the Single object of Roles table.
        /// </summary>
        public static Role GetRole(int aId)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("@Id", aId);
                return vConn.Query<Role>("RolesSelect", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// Removes a record from the Roles table.
        /// returns True if value was removed successfully else false
        /// Throw exception with message value 'EXISTS' if the data is duplicate
        /// </summary>
        public static bool Delete(int aId)
        {
            using (var vConn = OpenConnection())
            {
                var vParams = new DynamicParameters();
                vParams.Add("Id", aId);
                int iResult = vConn.Execute("RoleDelete", vParams, commandType: CommandType.StoredProcedure);
                return iResult == -1;
            }
        }
        /// <summary>
        /// Selects all records from the Roles table.
        /// </summary>
        public static IEnumerable<Role> SelectAll()
        {
            using (var vConn = OpenConnection())
            {
                return vConn.Query<Role>("RolesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

    }
}
