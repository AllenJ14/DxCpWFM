using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DixonsCarphone.WorkforceManagement.Business.Entities;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class PeopleManager : IPeopleManager
    {
        // Get all colleague records from HrFeed table for specified store
        public async Task<List<HrFeed>> GetStoreStaff(int storeNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.HrFeeds.Where(x => x.STORE_NUM == storeNum && x.DOL == "").OrderByDescending(x => x.CONTRACT_HOURS).ThenBy(x => x.SURNAME).ToListAsync();

                return result;
            }
        }

        public async Task<ContractBaseDetail> GetContractBase(int storeNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.ContractBaseDetails.FindAsync(storeNum);

                return result;
            }
        }

        public async Task<List<sp_RegionContractBase_Result>> GetRegionContractBase(string regionNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_RegionContractBase(regionNumber));

                return data.ToList();
            }
        }

        // Get UAC List
        public async Task<List<UserAccess>> GetUACList()
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.UserAccesses.OrderBy(x => x.Area).ToListAsync();

                return result;
            }
        }

        // Get single UAC entry
        public async Task<UserAccess> GetUACSingle(int ID)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.UserAccesses.FindAsync(ID);

                return result;
            }
        }

        //Submit new UAC record
        public async Task SubmitNewUAC(UserAccess model)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                dbContext.UserAccesses.Add(model);
                await dbContext.SaveChangesAsync();
            }
        }

        //Submit change to UAC record
        public async Task SubmitUACChange(UserAccess model)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var existing = await dbContext.UserAccesses.FindAsync(model.ID);
                if (existing != null)
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(model);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        //Delete UAC record on confirm
        public async Task DeleteUACRecord(int id)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var toDelete = await dbContext.UserAccesses.FindAsync(id);
                if (toDelete != null)
                {
                    dbContext.UserAccesses.Remove(toDelete);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
    
}
