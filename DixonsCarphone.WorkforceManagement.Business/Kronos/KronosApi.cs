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
        public static bool isOffice;
        private static readonly string XMLheader = "KronosXML=<?xml version='1.0' ?><Kronos_WFC Version='1.0'>";
        private static readonly string XMLfooter = "</Kronos_WFC>";
        private static readonly string userName = ConfigurationManager.AppSettings["KnUser"];
        private static readonly string password = ConfigurationManager.AppSettings["KnP"];
        private static CookieContainer cookieContainer = new CookieContainer();


        public static async Task<bool> Logon()
        {
            var xmlString = XMLheader + "<Request Action='logon' Object='system' Username='" + userName + "' Password='" + password + "'/> " + XMLfooter;

            var outcome = await postRequest(xmlString);

            return CheckResponse(outcome);
        }

        public static async Task<bool> Logoff()
        {
            var xmlString = XMLheader + "<Request Action='logoff' Object='system'/> " + XMLfooter;

            var outcome = await postRequest(xmlString);

            return CheckResponse(outcome);
        }

        public static async Task<List<HyperFindResult>> HyperfindResult(string queryName, string dateSpan)
        {
            var xmlString = XMLheader + string.Format(
                "<Request Action='RunQuery'><HyperFindQuery HyperFindQueryName='{0}' VisibilityCode='Public' QueryDateSpan='{1}' QueryPersonOrEmployee='Employee' QueryIncludePersonFlag='True'></HyperFindQuery>" +
                "</Request>", queryName, dateSpan) + XMLfooter;

            var outcome = await postRequest(xmlString);

            return CheckResponse(outcome) ? await outcome.DeserializeToObject<HyperFindResult>() : new List<HyperFindResult>();
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

            var outcome = await postRequest(xmlString);

            return CheckResponse(outcome) ? await outcome.DeserializeToObject<ScheduleItems>() : new List<ScheduleItems>();
        }


        //Post request to server and save response to file
        static async Task<string> postRequest(string postData)
        {
            if (isOffice)
            {
                var result = await postData.PostStringAsync(KronosApi.URL);

                return result;
            }

            return string.Empty;
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
