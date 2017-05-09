using DixonsCarphone.WorkforceManagement.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IContentManager
    {
        Task<ContentHeader> GetContentHeaderAndDetails(int contentHeaderId);

        Task<ContentDetail> GetContentDetail(int contentDetailId);

        Task<bool> SaveContentDetail(ContentDetail detail);

        Task<bool> DeleteContentDetail(int contentDetailId);

        Task<List<ContentHeader>> GetAllContentHeaderAndDetails();

    }
}
