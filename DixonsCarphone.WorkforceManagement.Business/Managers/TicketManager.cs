using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using System.Data.Entity;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class TicketManager : ITicketManager
    {
        public int GetUserGroup(string _userName)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                return dbContext.UserGroups.Where(x => x.UserName == _userName).Select(x => x.GroupId).FirstOrDefault();
            }
        }

        public async Task<int> SubmitTicket(int TicketTypeId, string RaisedBy, int BranchNumber, List<TicketAnswer> QA, int _exception)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                TicketStub _ticketStub = new TicketStub
                {
                    DateTimeCreated = DateTime.Now,
                    Status = "Open",
                    TicketTypeId = TicketTypeId,
                    LastUpdate = DateTime.Now,
                    RaisedBy = RaisedBy,
                    EscalationLevel = 1,
                    BranchNumber = (short)BranchNumber,
                    Exception = _exception
                };

                foreach (var item in QA)
                {
                    _ticketStub.TicketAnswers.Add(item);
                }

                dbContext.TicketStubs.Add(_ticketStub);
                dbContext.SaveChanges();

                return _ticketStub.TicketId;
            }
        }

        public async Task<List<TicketTemplate>> GetFormTemplate(int _TicketTypeId)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                return await dbContext.TicketTemplates.Where(x => x.TicketTypeId == _TicketTypeId && (bool)x.TicketType.Live).OrderBy(x => x.QuestionId).ToListAsync();
            }
        }

        public string GetFormName(int _TicketTypeId)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                return dbContext.TicketTypes.Where(x => x.TicketTypeId == _TicketTypeId).Select(x => x.Title).FirstOrDefault();
            }
        }

        public int ChkInteractLvl(int _level, int _ticketTypeId)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                return (int)dbContext.sp_ChkInteractLvl(_level, _ticketTypeId).FirstOrDefault();
            }
        }

        public async Task<TicketStub> GetSingleTicket(int _TicketID, int _GroupID, string _Username)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                if (dbContext.sp_CheckAccessRight(_GroupID, _TicketID, _Username).First().Value == 1)
                {
                    return await dbContext.TicketStubs.Where(x => x.TicketId == _TicketID)
                    .Include("TicketType")
                    .Include("TicketAnswers.TicketTemplate")
                    .Include("TicketComments")
                    .SingleOrDefaultAsync();
                }

                return new TicketStub();
            }
        }

        public List<sp_GetOpenFormsByUser_Result> GetFormsSelf(string _userName, string _type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_GetOpenFormsByUser(_userName, _type).ToList(); ;
            }
        }

        public List<sp_GetOpenFormsByGroup_Result> GetFormsWithAuth(int _userGroup, string _type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_GetOpenFormsByGroup(_userGroup, _type).ToList();
            }
        }

        public List<sp_GetOpenFormsByTPC_Result> GetFormsWithTPC(string _userName, string _type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_GetOpenFormsByTPC(_userName, _type).ToList();
            }
        }

        public List<sp_GetClosedFormsByUser_Result> GetClosedFormsSelf(string _userName, string _type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_GetClosedFormsByUser(_userName, _type).ToList(); ;
            }
        }

        public List<sp_GetClosedFormsByGroup_Result> GetClosedFormsWithAuth(int _userGroup, string _type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_GetClosedFormsByGroup(_userGroup, _type).ToList();
            }
        }

        public List<sp_GetClosedFormsByTPC_Result> GetClosedFormsWithTPC(string _userName, string _type)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_GetClosedFormsByTPC(_userName, _type).ToList();
            }
        }

        public List<sp_EscalationOptions_Result> GetEscalationOptions(int _ticketType, int _level)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return dbContext.sp_EscalationOptions(_ticketType, _level).OrderBy(x => x.Level).ToList();
            }
        }

        public async Task<TicketComment> AddNewComment(string commentText, string _userName, int _TicketID)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                TicketComment _toAttach = new TicketComment
                {
                    TicketId = _TicketID,
                    User = _userName,
                    Timestamp = DateTime.Now,
                    Comment = commentText
                };

                dbContext.TicketComments.Add(_toAttach);
                await dbContext.SaveChangesAsync();

                return _toAttach;
            }
        }

        public async Task<int> CancelTicket(int TicketID, string _userName)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                var result = dbContext.TicketStubs.Find(TicketID);
                if(result.RaisedBy != _userName)
                {
                    return -5; //Error code if user does not own ticket
                }
                result.Status = "Cancelled";
                result.LastUpdate = DateTime.Now;
                result.LastUpdatedBy = _userName;

                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> CompleteTicket(int _TicketID, string _Username, int _GroupID)
        {
            using (var dbContext = new DxCpWfmContext())
            {        
                if(dbContext.sp_CheckAccessRight(_GroupID, _TicketID, "").First().Value == 1)
                {
                    var result = dbContext.TicketStubs.Find(_TicketID);

                    result.Status = "Completed";
                    result.LastUpdate = DateTime.Now;
                    result.LastUpdatedBy = _Username;

                    return await dbContext.SaveChangesAsync();
                }
                return -5;
            }
        }

        public async Task<int> SendTicket(int _TicketID, string _userName, int lvlAction, int _GroupID)
        {
            using(var dbContext = new DxCpWfmContext())
            {
                if(dbContext.sp_CheckAccessRight(_GroupID, _TicketID, "").First().Value == 1)
                {
                    var result = dbContext.TicketStubs.Find(_TicketID);

                    result.LastUpdate = DateTime.Now;
                    result.LastUpdatedBy = _userName;

                    int toMove = result.EscalationLevel;

                    result.EscalationLevel = (byte)(toMove + lvlAction);

                    return await dbContext.SaveChangesAsync();
                }
                return -5;
            }
        }

        public async Task<List<UserGroup>> GetTPCMenu()
        {
            using(var dbContext = new DxCpWfmContext())
            {
                return await dbContext.UserGroups.Where(x => x.GroupId == 3).OrderBy(x => x.FriendlyName).ToListAsync();
            }
        }

        public async Task<List<TicketType>> GetTypeMenu()
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.TicketTypes.Where(x => (bool)x.Live).ToListAsync();
            }
        }

        public async Task<string> GetRegion(int branchNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.tempRegionLookups.Where(x => x.Store == branchNumber).FirstOrDefaultAsync();
                return result.Area.ToString();
            }
        }
    }
}
