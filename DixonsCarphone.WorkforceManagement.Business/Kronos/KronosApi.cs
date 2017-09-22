using DixonsCarphone.WorkforceManagement.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        private static CookieContainer cookieContainer = new CookieContainer();


        public static async Task<bool> Logon()
        {
            var xmlString = XMLheader + "<Request Action='logon' Object='system' Username='" + userName + "' Password='" + password + "'/> " + XMLfooter;

            var outcome = await postRequestAsync(xmlString);

            return CheckResponse(outcome);
        }

        public static async Task<bool> Logoff()
        {
            var xmlString = XMLheader + "<Request Action='logoff' Object='system'/> " + XMLfooter;

            var outcome = await postRequestAsync(xmlString);

            return CheckResponse(outcome);
        }

        public static async Task<List<HyperFindResult>> HyperfindResult(string queryName, string dateSpan)
        {
            var xmlString = XMLheader + string.Format(
                "<Request Action='RunQuery'><HyperFindQuery HyperFindQueryName='{0}' VisibilityCode='Public' QueryDateSpan='{1}' QueryPersonOrEmployee='Employee' QueryIncludePersonFlag='True'></HyperFindQuery>" +
                "</Request>", queryName, dateSpan) + XMLfooter;

            var outcome = await postRequestAsync(xmlString);

            return CheckResponse(outcome) ? await outcome.DeserializeToObjectAsync<HyperFindResult>() : new List<HyperFindResult>();
        }

        public static List<Timesheet> RequestTimesheet(DateTime[] dates, string personNumber)
        {
            var xmlString = XMLheader + "<Transaction>";
            foreach(var item in dates)
            {
                xmlString += "<Request Action='LoadPeriodTotals'><Timesheet><Employee><PersonIdentity PersonNumber='" + personNumber + "'/></Employee><Period><TimeFramePeriod PeriodDateSpan='" + item.ToShortDateString() + "-" + item.AddDays(6).ToShortDateString() + "' TimeFrameName='9'/></Period></Timesheet></Request>";
            }
            xmlString += "</Transaction>" + XMLfooter;

            var outcome = postRequest(xmlString);

            return CheckResponse(outcome) ? outcome.DeserializeToObject<Timesheet>() : new List<Timesheet>();
        }

        public static async Task<List<Timesheet>> RequestTimesheet(DateTime date, string[] personNumber)
        {
            var xmlString = XMLheader;
            foreach(var item in personNumber)
            {
                xmlString += "<Transaction><Request Action='LoadPeriodTotals'><Timesheet><Employee><PersonIdentity PersonNumber='" + item + "'/></Employee><Period><TimeFramePeriod PeriodDateSpan='" + date.ToShortDateString() + "-" + date.AddDays(6).ToShortDateString() + "' TimeFrameName='9'/></Period></Timesheet></Request></Transaction>";
            }
            xmlString += XMLfooter;

            var outcome = await postRequestAsync(xmlString);

            return CheckResponse(outcome) ? await outcome.DeserializeToObjectAsync<Timesheet>() : new List<Timesheet>();
        }

        //Build Data xml request
        public static async Task<List<ScheduleItems>> RequestScheduleDetail(string startDate, string endDate, List<string> employeeList)
        {
            var xmlString = XMLheader + "<Request Action = 'Load'><Schedule QueryDateSpan='" + startDate + "-" + endDate + "'><Employees>";

            employeeList.ForEach(delegate (string ID)
            {
                xmlString += "<PersonIdentity PersonNumber='" + ID + "'/>";
            });

            xmlString += "</Employees></Schedule></Request>" + XMLfooter;

            var outcome = await postRequestAsync(xmlString);

            return CheckResponse(outcome) ? await outcome.DeserializeToObjectAsync<ScheduleItems>() : new List<ScheduleItems>();
        }


        //Post request to server and save response to file
        static async Task<string> postRequestAsync(string postData)
        {
            var result = await postData.PostStringAsync(KronosApi.URL);

            return result;
        }

        //Post request synch
        static string postRequest(string postData)
        {
            return postData.PostString(KronosApi.URL);
        }

        //Check XML response for value of 'Status' attribute and return
        static bool CheckResponse(string xmlString)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("Response");
                reader.MoveToAttribute("Status");

                if (reader.Value == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        //Write XML Response to File
        static void writeFile(string xmlString)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
            doc.Save("C:\\__USER\\data.xml");
        }

    }

}
