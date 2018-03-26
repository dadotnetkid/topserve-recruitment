using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnitOfWorkExtension;

namespace RecruitmentSystem.Recruitment.Data
{
    public class UnitOfWork
    {
        private GenericRepository<tbl_manpower_request> manpowerRepo;
        private TalentAcquisitionEntities context = new TalentAcquisitionEntities();
        public GenericRepository<tbl_manpower_request> ManpowerRepo
        {
            get { return manpowerRepo ?? (manpowerRepo = new GenericRepository<tbl_manpower_request>(context)); }
            set { manpowerRepo = value; }
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Save()
        {
             context.SaveChanges();
        }
    }
}