using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class DatabaseController : Controller
    {
        public ActionResult Index()
        {
            var dir = Directory.EnumerateFiles(Server.MapPath("~/DatabaseBackup"));
            string path = Server.MapPath("~/DatabaseBackup/");
            List<DatabaseBackupModelViewController> list = new List<DatabaseBackupModelViewController>();
            foreach (var i in dir)
            {
                if (Path.GetExtension(i).Contains("bak"))
                {
                    list.Add(new DatabaseBackupModelViewController()
                    {
                        Filename = Path.GetFileName(i),
                        FileLength = (Math.Round((new FileInfo(i).Length / 1024) / 1024.0, 2)).ToString() + " mb",
                        DateCreated = System.IO.File.GetCreationTime(i)

                    });
                }

            }
            return View(list.OrderByDescending(m => m.DateCreated));
        }
        public ActionResult Backup()
        {
            var res = BackupDatabase.BackupDb();
            return RedirectToAction("index");
        }
        public ActionResult Download(string filename)
        {
            string path = Server.MapPath("~/DatabaseBackup/") + filename;
            return File(path, "applicant/binary", filename);
        }
        public ActionResult Delete(string filename)
        {
            string path = Server.MapPath("~/DatabaseBackup/") + filename;
            System.IO.File.Delete(path);

            return RedirectToAction("index");
        }
        public ActionResult RestoreDb(string filename)
        {
            string path = Server.MapPath("~/DatabaseBackup/") + filename;
            BackupDatabase.RestoreDb(path);
            return RedirectToAction("index");
        }
    }
}