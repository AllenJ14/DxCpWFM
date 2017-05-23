using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IDashMaintenance
    {
        DashboardErrors GetErrors();

        bool RunBuild();

        bool RunBuildNewData();

        bool RunBudgets();

        bool PushData();

        Task<List<StoreReference>> StoreReferenceList();

        Task<StoreReference> StoreReferenceSingle(int? id);

        Task SubmitNewStoreReference(StoreReference model);

        Task SubmitStoreReferenceChange(StoreReference model);

        Task DeleteStoreReferenceRecord(int id);

        Task<List<RoleReference>> RoleReferenceList();

        Task<RoleReference> RoleReferenceSingle(string id);

        Task SubmitNewRoleReference(RoleReference model);

        Task SubmitRoleReferenceChange(RoleReference model);

        Task DeleteRoleReferenceRecord(string id);

        bool ImportPLSheet(string type);

        Task<List<Entities.FileUploadRecord>> GetFileRecords(string type);
    }
}
