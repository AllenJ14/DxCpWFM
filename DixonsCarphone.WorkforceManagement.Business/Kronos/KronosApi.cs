﻿using DixonsCarphone.WorkforceManagement.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos
{
    public class KronosApi
    {
        public static readonly string URL = ConfigurationManager.AppSettings["KnUrl"];
        private static readonly string XMLheader = "KronosXML=<?xml version='1.0' ?><Kronos_WFC Version='1.0'>";
        private static readonly string XMLfooter = "</Kronos_WFC>";
        private static readonly string userName = ConfigurationManager.AppSettings["KnUser"];
        private static readonly string password = ConfigurationManager.AppSettings["KnP"];
        //private static CookieContainer cookieContainer = new CookieContainer();
        
        public static async Task<bool> Logon(string sessionID = null)
        {
            var xmlString = XMLheader + "<Request Action='logon' Object='system' Username='" + userName + "' Password='" + password + "'/> " + XMLfooter;
            
            var outcome = await postRequestAsync(xmlString, true);
            var success = CheckResponse(outcome);

            await Task.Run(() => logAction("Logon", success, sessionID));

            return success == 1;
        }

        public static async Task<bool> Logoff(string sessionID)
        {
            var xmlString = XMLheader + "<Request Action='logoff' Object='system'/> " + XMLfooter;

            var outcome = await postRequestAsync(xmlString);
            var success = CheckResponse(outcome);

            await Task.Run(() => logAction("Logoff", success, sessionID));

            return success == 1;
        }

        public static async Task<List<HyperFindResult>> HyperfindResult(string queryName, string dateSpan, string sessionID)
        {
            var xmlString = XMLheader + string.Format(
                "<Request Action='RunQuery'><HyperFindQuery HyperFindQueryName='{0}' VisibilityCode='Public' QueryDateSpan='{1}' QueryPersonOrEmployee='Employee' QueryIncludePersonFlag='True'></HyperFindQuery>" +
                "</Request>", queryName, dateSpan) + XMLfooter;

            var outcome = await postRequestAsync(xmlString);
            var success = CheckResponse(outcome);

            if(success == -1)
            {
                var logon = await Logon();
                if (logon)
                {
                    outcome = await postRequestAsync(xmlString);
                    success = CheckResponse(outcome);
                }
            }

            logAction("Hyperfind", success, sessionID, queryName);

            return success == 1 ? await outcome.DeserializeToObjectAsync<HyperFindResult>() : new List<HyperFindResult>();
        }

        public static List<Timesheet> RequestTimesheet(DateTime[] dates, string personNumber, string sessionID)
        {
            var xmlString = XMLheader + "<Transaction>";
            foreach (var item in dates)
            {
                xmlString += "<Request Action='LoadPeriodTotals'><Timesheet><Employee><PersonIdentity PersonNumber='" + personNumber + "'/></Employee><Period><TimeFramePeriod PeriodDateSpan='" + item.ToShortDateString() + "-" + item.AddDays(6).ToShortDateString() + "' TimeFrameName='9'/></Period></Timesheet></Request>";
            }
            xmlString += "</Transaction>" + XMLfooter;

            var outcome = postRequest(xmlString);
            var success = CheckResponse(outcome);

            if (success == -1)
            {
                var logon = Task.Run(() => Logon());
                if (logon.Result)
                {
                    outcome = postRequest(xmlString);
                    success = CheckResponse(outcome);
                }
            }

            Task.Run(() => logAction("Timesheet", success, sessionID, null, personNumber));

            return success == 1 ? outcome.DeserializeToObject<Timesheet>() : new List<Timesheet>();
        }

        public static async Task<List<Timesheet>> RequestTimesheet(DateTime date, string[] personNumber, string sessionID)
        {
            var xmlString = XMLheader;
            foreach(var item in personNumber)
            {
                xmlString += "<Transaction><Request Action='LoadPeriodTotals'><Timesheet><Employee><PersonIdentity PersonNumber='" + item + "'/></Employee><Period><TimeFramePeriod PeriodDateSpan='" + date.ToShortDateString() + "-" + date.AddDays(6).ToShortDateString() + "' TimeFrameName='9'/></Period></Timesheet></Request></Transaction>";
            }
            xmlString += XMLfooter;

            var outcome = await postRequestAsync(xmlString);
            var success = CheckResponse(outcome);

            if (success == -1)
            {
                var logon = await Logon();
                if (logon)
                {
                    outcome = await postRequestAsync(xmlString);
                    success = CheckResponse(outcome);
                }
            }

            logAction("Timesheet", success, sessionID, null, personNumber[0]);

            return success == 1 ? await outcome.DeserializeToObjectAsync<Timesheet>() : new List<Timesheet>();
        }

        //Build Data xml request
        public static async Task<List<ScheduleItems>> RequestScheduleDetail(string startDate, string endDate, List<string> employeeList, string sessionID)
        {
            var xmlString = XMLheader + "<Request Action = 'Load'><Schedule QueryDateSpan='" + startDate + "-" + endDate + "'><Employees>";

            employeeList.ForEach(delegate (string ID)
            {
                xmlString += "<PersonIdentity PersonNumber='" + ID + "'/>";
            });

            xmlString += "</Employees></Schedule></Request>" + XMLfooter;

            var outcome = await postRequestAsync(xmlString);
            var success = CheckResponse(outcome);

            if (success == -1)
            {
                var logon = await Logon();
                if (logon)
                {
                    outcome = await postRequestAsync(xmlString);
                    success = CheckResponse(outcome);
                }
            }

            logAction("Schedule", success, sessionID, null, employeeList[0]);

            return success == 1 ? await outcome.DeserializeToObjectAsync<ScheduleItems>() : new List<ScheduleItems>();
        }       

        public static async Task<List<PunchStatus>> RequestPunchStatus(List<string> employeeList, string sessionID)
        {
            var xmlString = XMLheader;
            foreach(var item in employeeList)
            {
                xmlString += "<Request Action='CheckStatus'><PunchStatus><Employee><PersonIdentity PersonNumber='" + item + "'></PersonIdentity></Employee></PunchStatus></Request>";
            }
            xmlString += "</Transaction>" + XMLfooter;

            var outcome = await postRequestAsync(xmlString);
            var success = CheckResponse(outcome);

            if (success == -1)
            {
                var logon = await Logon();
                if (logon)
                {
                    outcome = await postRequestAsync(xmlString);
                    success = CheckResponse(outcome);
                }
            }

            logAction("PunchStatus", success, sessionID, null, employeeList[0]);

            return success == 1 ? await outcome.DeserializeToObjectAsync<PunchStatus>() : new List<PunchStatus>();
        }

        //Post request to server and save response to file
        static async Task<string> postRequestAsync(string postData, bool newSession = false)
        {
            var result = await postData.PostStringAsync(KronosApi.URL, newSession);

            return result;
        }

        //Post request synch
        static string postRequest(string postData, bool newSession = false)
        {
            return postData.PostString(KronosApi.URL, newSession);
        }

        //Check XML response for value of 'Status' attribute and return
        static short CheckResponse(string xmlString)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("Response");
                reader.MoveToAttribute("Status");

                if (reader.Value == "Success")
                {
                    return 1;
                }
                else
                {
                    short result = 0;
                    reader.ReadToFollowing("Error");
                    reader.MoveToAttribute("ErrorCode");
                    if (reader.Value == "1307" || reader.Value == "1328" || reader.Value == "1201")
                    {
                        result = -1;
                    }
                    var doc = new XmlDocument();
                    doc.LoadXml(xmlString);
#if DEBUG
                    doc.Save("C:\\__USER\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");
#else
                    doc.Save(@"D:\Inetpub\WFM App\App_Data\APIError\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");
#endif
                    return result;
                }
            }

        }

        static bool logAction(string action, short success, string sessionID = null, string branch = null, string empNum = null)
        {
            string fPath = @"D:\Inetpub\WFM App\App_Data\APILog\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

#if DEBUG
            fPath = "C:\\__USER\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
#endif

            if (!File.Exists(fPath))
            {
                using (StreamWriter sw = File.CreateText(fPath))
                {
                    sw.WriteLine("Timestamp, Action, Success, SessionID, Branch, EmpNum");
                }
            }

            using (StreamWriter sw = File.AppendText(fPath))
            {
                string toWrite = DateTime.Now.ToString() + "," + action + "," + success + "," + sessionID + "," + branch + "," + empNum;

                sw.WriteLine(toWrite);
            }
            return true;
        }

        //Write XML Response to File
        static void writeFile(string xmlString)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
            doc.Save("C:\\__USER\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");
            return;
        }

    }

}
