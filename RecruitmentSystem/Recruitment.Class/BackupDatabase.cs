using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Recruitment.Class
{
    public class BackupDatabase
    {

        static SqlConnectionInfo sqlConnectionInfo()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            string servername = ""; string username = ""; string password = "";
            foreach (var i in db.Connection.ConnectionString.Split(';'))
            {
                if (i.Contains("Data Source"))
                {
                    servername = i.Split('=')[1];

                }
                else if (i.ToLower().Contains("user id"))
                {
                    username = i.Split('=')[1];
                }
                else if (i.ToLower().Contains("password"))
                {
                    password = i.Split('=')[1];
                }
                else if (i.ToLower().Contains("initial catalog"))
                {

                }
            }
            return new SqlConnectionInfo(servername, username, password);
        }
        static string database()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var retval="";
            foreach (var i in db.Connection.ConnectionString.Split(';'))
            {

                if (i.ToLower().Contains("initial catalog"))
                {
                    retval = i.Split('=')[1];
                }
            }
            return retval;
        }
        static Server svr()
        {
            return new Server(new ServerConnection(sqlConnectionInfo()));
        }
        public static string BackupDb()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var connectionstring = db.Connection.ConnectionString;
            var backup = new Backup()
            {
                Action = BackupActionType.Database,
                Database = BackupDatabase.database(),
                BackupSetDescription = BackupDatabase.database() +" " + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt")
            };
            string path = HttpContext.Current.Request.MapPath("~/DatabaseBackup/") + database() +" "+ DateTime.Now.ToString("MM-dd-yyyy hh-mm-ss tt") + ".bak";
            backup.Devices.AddDevice(path, DeviceType.File);

            backup.SqlBackup(svr());
            return path;
        }
        public static void RestoreDb(string file)
        {
            sqlConnectionInfo();
            var restore = new Restore() { Database = BackupDatabase.database(), Action = RestoreActionType.Database, ReplaceDatabase = true };
            restore.Devices.AddDevice(file, DeviceType.File);
            svr().KillAllProcesses(BackupDatabase.database());
            restore.Wait();

            restore.SqlRestore(svr());
        }
    }
}