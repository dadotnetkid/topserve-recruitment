using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Models
{
    public class UserLogs : ILog
    {
        //TODO: Your Logic Here
        public void AddLogs(Report report)
        {

        }
    }
    //TODO: this is calling class;
    public class Logs
    {
        public ILog ilog;
        public Logs(ILog ilog)
        {
            this.ilog = ilog;
        }
        public Logs()
        {

        }
        public void AddLogs(Report report)
        {
            this.ilog.AddLogs(report);
        }
    }
    //TODO: this is your virtual table or model
    public class Report
    {
        public string user_id { get; set; }
        public string table_name { get; set; }
        public string type { get; set; }
        public string logs { get; set; }
    }
    //your interface 
    public interface ILog
    {
        void AddLogs(Report report);
    }
}
