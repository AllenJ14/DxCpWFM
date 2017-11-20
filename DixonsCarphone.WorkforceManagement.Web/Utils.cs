using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Kronos;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DixonsCarphone.WorkforceManagement.Web
{
    public class Utils
    {
        public static async Task<List<ScheduledCollegues>> GetKronosData(DateTime startDate, DateTime endDate, string kronosStoreName, bool isOffice)
        {
            IKronosManager _kronosManager = new KronosManager();

            if (!isOffice)
            {
                var testhyperResult = Helpers.DeSerializeObject<List<HyperFindResult>>(@"C:\Dev\TestData\KronosHyperResultTestData.xml");
                var testschedule = Helpers.DeSerializeObject<ScheduleItems>(@"C:\Dev\TestData\KronosScheduleTestData.xml");
                var testscheduleWithAbsentees = Helpers.DeSerializeObject<ScheduleItems>(@"C:\Dev\TestData\KronosScheduleTestDataWithAbsenteesFormatted.xml");

                var testVm = Helpers.DeSerializeObject<List<ScheduledCollegues>>(@"C:\Dev\TestData\newScheduleTestData.xml");

                return testVm;
            }

            var toRtn = new List<ScheduledCollegues>();
            var hyperResult = await _kronosManager.GetKronosHyperFind(kronosStoreName, startDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture), endDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
            if (hyperResult != null && hyperResult.Count > 0)
            {
                var personNumbers = hyperResult.Select(x => x.PersonNumber).ToList();
                var schedule = await _kronosManager.GetStoreScheduleForWeek(startDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture), endDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                    personNumbers);

                if (schedule != null)
                {
                    hyperResult.SerializeObject(@"C:\Dev\TestData\KronosHyperResultTestData.xml"); //todo remove
                    schedule.SerializeObject(@"C:\Dev\TestData\KronosScheduleTestData.xml"); //todo remove

                    var itms = schedule.ScheduleShift.OrderBy(x => x.StartDate).ToList();
                    AddScheduledCollegues(toRtn, hyperResult, itms);

                    AddAbsentCollegues(toRtn, hyperResult, schedule.Absentees.OrderBy(x => x.StartDate).ToList());
                }
            }

            toRtn.SerializeObject(@"C:\Dev\TestData\ScheduleTestData.xml"); //todo remove
            await _kronosManager.LogOff();

            return toRtn.OrderBy(x => x.ScheduledDate).ToList();
        }

        public static NonScheduledColleguesViewModel GetNonScheduledCollegues(List<ScheduledCollegues> kronosData, List<HrFeed> staff)
        {
            if (kronosData == null) return new NonScheduledColleguesViewModel { NonScheduledColleagues = new List<NonScheduledColleagues>() };

            var finYr = ConfigurationManager.AppSettings["FinancialYearStart"];
            var toRtn = new NonScheduledColleguesViewModel { NonScheduledColleagues = new List<NonScheduledColleagues>() };
            foreach (var colleague in kronosData.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault())) // distinct on person
            {
                var wkNum = string.Empty;
                DateTime startDt;

                var schedules = kronosData.Where(x => x.PersonNumber == colleague.PersonNumber).ToList();
                double totalHours = 0;
                foreach (var schedule in schedules)
                {
                    DateTime stTime;
                    DateTime enTime;
                    wkNum = schedule.ScheduledDate.GetWeekOfYear(finYr).ToString().Substring(4);
                    if (DateTime.TryParse(schedule.EndTime, out enTime) && DateTime.TryParse(schedule.StartTime, out stTime))
                    {
                        var timeDiff = enTime.Subtract(stTime).Hours;
                        totalHours += timeDiff;
                    }
                }

                var ppl = staff.FirstOrDefault(x => colleague.PersonNumber == string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0')));

                if (ppl != null && totalHours < ppl.CONTRACT_HOURS)
                {
                    toRtn.NonScheduledColleagues.Add(new NonScheduledColleagues
                    {
                        PersonNumber = ppl.EMP_NUM.GetValueOrDefault().ToString(),
                        TotalHoursForWeek = totalHours,
                        ContractedHours = ppl.CONTRACT_HOURS.GetValueOrDefault(),
                        FullName = colleague.FullName,
                        WeekNumber = wkNum
                    });
                }
            }

            return toRtn;
        }

        public static double CalculateHours(string endTime, string startTime)
        {
            var x = 0.0;
            DateTime endDate, startDate;

            if (DateTime.TryParse(endTime, out endDate) && DateTime.TryParse(startTime, out startDate))
            {
                x = endDate.Subtract(startDate).TotalHours;
            }

            return x;
        }

        private static void AddScheduledCollegues(List<ScheduledCollegues> toRtn, List<HyperFindResult> hyperResult, List<ScheduleShift> itms)
        {
            foreach (var itm in itms)
            {
                DateTime dt;
                toRtn.Add(new ScheduledCollegues
                {
                    FullName = hyperResult.Where(x => x.PersonNumber == itm.Employee?.PersonIdentity?.PersonNumber).FirstOrDefault().FullName,
                    EndTime = itm?.ShiftSegments?.ShiftSegment?.EndTime,
                    StartTime = itm?.ShiftSegments?.ShiftSegment.StartTime,
                    ScheduledDate = DateTime.TryParse(itm.StartDate, out dt) ? dt : DateTime.MinValue,
                    PersonNumber = itm.Employee?.PersonIdentity?.PersonNumber,
                    BranchAssigned = itm?.ShiftSegments?.ShiftSegment?.SegmentTypeName?.ToLower() == "transfer" && !string.IsNullOrEmpty(itm?.ShiftSegments?.ShiftSegment?.LaborAccountName) ?
                    string.Format("(Transfered. Store #: {0})", itm?.ShiftSegments?.ShiftSegment?.LaborAccountName?.Replace("/", "")) : string.Empty
                });
            }
        }

        private static void AddAbsentCollegues(List<ScheduledCollegues> toRtn, List<HyperFindResult> hyperResult, List<SchedulePayCodeEdit> itms)
        {
            foreach (var itm in itms)
            {
                DateTime dt;
                toRtn.Add(new ScheduledCollegues
                {
                    FullName = hyperResult.Where(x => x.PersonNumber == itm.Employee?.PersonIdentity?.PersonNumber).FirstOrDefault().FullName,
                    StartTime = itm.PayCodeName.Contains("Holiday") ? "Holiday" : "Absence", // string.Format("{0}", itm.PayCodeName),
                    EndTime = string.Format("{0}Hrs", itm.AmountInTime),
                    ScheduledDate = DateTime.TryParse(itm.StartDate, out dt) ? dt : DateTime.MinValue,
                    PersonNumber = itm.Employee?.PersonIdentity?.PersonNumber,
                    IsAbsent = true,
                    AbscenceAmountInTime = itm.AmountInTime
                });
            }
        }

    }
}