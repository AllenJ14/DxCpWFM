using DixonsCarphone.WorkforceManagement.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IPeopleManager
    {
        Task<List<HrFeed>> GetStoreStaff(int storeNum);

        Task<ContractBaseDetail> GetContractBase(int storeNum);

        Task<List<sp_RegionContractBase_Result>> GetRegionContractBase(string regionNumber);

        Task<List<UserAccess>> GetUACList();

        Task<UserAccess> GetUACSingle(int ID);

        Task SubmitNewUAC(UserAccess model);

        Task SubmitUACChange(UserAccess model);

        Task DeleteUACRecord(int id);
    }
}
