using DixonsCarphone.WorkforceManagement.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface ITicketManager
    {
        Task<int> SubmitTicket(int TicketTypeId, string RaisedBy, int BranchNumber, List<TicketAnswer> QA, int _exception);

        Task<List<TicketTemplate>> GetFormTemplate(int _TicketTypeId);

        string GetFormName(int TicketTypeId);

        List<sp_GetOpenFormsByUser_Result> GetFormsSelf(string _userName, string _type);

        List<sp_GetOpenFormsByGroup_Result> GetFormsWithAuth(int _userGroup, string _type);

        List<sp_GetOpenFormsByTPC_Result> GetFormsWithTPC(string _userName, string _type);

        Task<TicketStub> GetSingleTicket(int _TicketID, int _GroupID, string _Username);

        Task<TicketComment> AddNewComment(string commentText, string _userName, int _TicketID);

        Task<int> CancelTicket(int TicketID, string _userName);

        Task<int> CompleteTicket(int _TicketID, string _Username, int _GroupID);

        int GetUserGroup(string _userName);

        List<sp_EscalationOptions_Result> GetEscalationOptions(int _ticketType, int _level);

        int ChkInteractLvl(int _level, int _ticketTypeId);

        Task<int> SendTicket(int _TicketID, string _userName, int lvlAction, int _GroupID);

        Task<List<UserGroup>> GetTPCMenu();

        List<sp_GetClosedFormsByUser_Result> GetClosedFormsSelf(string _userName, string _type);

        List<sp_GetClosedFormsByGroup_Result> GetClosedFormsWithAuth(int _userGroup, string _type);

        List<sp_GetClosedFormsByTPC_Result> GetClosedFormsWithTPC(string _userName, string _type);

        Task<List<TicketType>> GetTypeMenu();

        Task<string> GetRegion(int branchNumber);
    }
}
