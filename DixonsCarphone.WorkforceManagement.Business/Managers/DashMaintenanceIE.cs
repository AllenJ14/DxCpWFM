using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class DashMaintenanceIE : IDashMaintenance
    {
        // Return summary of current errors 
        public DashboardErrors GetErrors()
        {
            using(var dbContext = new DxCpIEReportingModel())
            {
                DashboardErrors _dashboardErrors = new DashboardErrors();
                _dashboardErrors.StoreErrors = dbContext.fn_UnmatchedStores().ToList();
                _dashboardErrors.RoleErrors = dbContext.fn_UnmatchedRoles().ToList();
                return _dashboardErrors;
            }
        }

        //Run sp to build dashboard with existing timecard data file
        public bool RunBuild()
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                dbContext.Database.ExecuteSqlCommand("reBuildDashboard");
                return true;
            }
        }

        //Run sp to build dashboard with new timecard data file
        public bool RunBuildNewData()
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                dbContext.Database.ExecuteSqlCommand("reBuildDashboardNewData");
                return true;
            }
        }

        //Run sp to import and update budgets from file
        public bool RunBudgets()
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                dbContext.Database.ExecuteSqlCommand("sp_LoadBudgets");
                return true;
            }
        }

        //Run sp to import and update budgets from file
        public bool PushData()
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                dbContext.Database.ExecuteSqlCommand("sp_PushLWDataToSite");
                return true;
            }
        }

        //Get all store reference records
        public async Task<List<StoreReference>> StoreReferenceList()
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                return await dbContext.StoreReferences.OrderBy(x => x.Br_).ToListAsync();
            }
        }

        //Get single store reference record by id
        public async Task<StoreReference> StoreReferenceSingle(int? id)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                return await dbContext.StoreReferences.FindAsync(id);
            }
        }

        //Submit new store reference record
        public async Task SubmitNewStoreReference(StoreReference model)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                dbContext.StoreReferences.Add(model);
                await dbContext.SaveChangesAsync();
            }
        }

        //Submit change to store reference record
        public async Task SubmitStoreReferenceChange(StoreReference model)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                var existing = await dbContext.StoreReferences.FindAsync(model.Br_);
                if (existing != null)
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(model);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        //Delete store reference record on confirm
        public async Task DeleteStoreReferenceRecord(int id)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                var toDelete = await dbContext.StoreReferences.FindAsync(id);
                if (toDelete != null)
                {
                    dbContext.StoreReferences.Remove(toDelete);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        //Get all role reference records
        public async Task<List<RoleReference>> RoleReferenceList()
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                return await dbContext.RoleReferences.OrderBy(x => x.Role).ToListAsync();
            }
        }

        //Get single role reference record by id
        public async Task<RoleReference> RoleReferenceSingle(string id)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                return await dbContext.RoleReferences.FindAsync(id);
            }
        }

        //Submit new role reference record
        public async Task SubmitNewRoleReference(RoleReference model)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                dbContext.RoleReferences.Add(model);
                await dbContext.SaveChangesAsync();
            }
        }

        //Submit change to role reference record
        public async Task SubmitRoleReferenceChange(RoleReference model)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                var existing = await dbContext.RoleReferences.FindAsync(model.Role);
                if (existing != null)
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(model);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        //Delete role reference record on confirm
        public async Task DeleteRoleReferenceRecord(string id)
        {
            using (var dbContext = new DxCpIEReportingModel())
            {
                var toDelete = await dbContext.RoleReferences.FindAsync(id);
                if (toDelete != null)
                {
                    dbContext.RoleReferences.Remove(toDelete);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        //Import PL_Import
        public bool ImportPLSheet(string type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                dbContext.Database.ExecuteSqlCommand("sp_PL_Import" + type);
                return true;
            }
        }
    }
}
