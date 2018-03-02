using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class DatabaseBackupModelViewController
    {
        public string Filename { get; set; }
        public string FileLength { get; set; }
        public DateTime DateCreated { get; set; }
    }
   
}