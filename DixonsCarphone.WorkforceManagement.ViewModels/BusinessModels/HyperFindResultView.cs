using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class HyperFindResultView
    {
        public string FullName { get; set; }
        public string PersonNumber { get; set; }

        public PersonDataView PersonData { get; set; }

        public string parsedPerson { get
            {
                return int.Parse(PersonNumber.Replace("UK", "")).ToString();
            } }
    }

    public class PersonDataView
    {
        public PersonView Person { get; set; }
    }

    public class PersonView
    {
        public string BirthDate { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string HireDate { get; set; }
        public string LastName { get; set; }
        public string PersonNumber { get; set; }
        public string ShortName { get; set; }
        public int EmployeeStandardHours { get; set; }
        public int FullTimeStandardHours { get; set; }
        public string AccrualProfileName { get; set; }
        public DateTime ManagerSignoffThruDateTime { get; set; }
        public string PayrollLockoutThruDateTime { get; set; }
        public bool FingerRequiredFlag { get; set; }
        public decimal BaseWageHourly { get; set; }
    }
}
