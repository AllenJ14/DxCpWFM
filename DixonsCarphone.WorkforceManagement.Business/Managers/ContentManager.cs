using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using System.Data.Entity;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class ContentManager : IContentManager
    {
        //Save new/updated content detail
        public async Task<bool> SaveContentDetail(ContentDetail detail)
        {
            detail.DateModified = DateTime.Now;
            using (var dbContext = new DxCpWfmContext())
            {
                var existing = await dbContext.ContentDetails.FirstOrDefaultAsync(x => x.ContentDetailId == detail.ContentDetailId);
                if (existing != null)
                {
                    detail.DateCreated = existing.DateCreated;
                    dbContext.Entry(existing).CurrentValues.SetValues(detail);
                }
                else
                {
                    detail.DateCreated = DateTime.Now;
                    dbContext.ContentDetails.Add(detail);
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        //Delete content detail
        public async Task<bool> DeleteContentDetail(int contentDetailId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var existing = await dbContext.ContentDetails.FirstOrDefaultAsync(x => x.ContentDetailId == contentDetailId);
                if (existing != null)
                {
                    dbContext.ContentDetails.Remove(existing);
                    await dbContext.SaveChangesAsync();
                }

                return true;
            }
        }


        public async Task<ContentHeader> GetContentHeaderAndDetails(int contentHeaderId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.ContentHeaders
                    .Include(x => x.ContentDetails)
                    .FirstOrDefaultAsync(x => x.ContentHeaderId == contentHeaderId);

                return result;
            }
        }

        public async Task<List<ContentHeader>> GetAllContentHeaderAndDetails()
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.ContentHeaders
                    .Include(x => x.ContentDetails)
                    .ToListAsync();

                return result;
            }
        }

        public async Task<ContentDetail> GetContentDetail(int contentDetailId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.ContentDetails.FirstOrDefaultAsync(x => x.ContentDetailId == contentDetailId);
                return result;
            }

        }
    }
}
