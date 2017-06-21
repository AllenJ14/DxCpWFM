using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class HolidayDetail
    {
        public string ID { get; set; }
        public int BalanceRemaining { get; set; }
        public int HolidayTaken { get; set; }
        public int Planned { get; set; }

        public char RAG(float percToTake)
        {
            float takenPerc = (float)HolidayTaken/(BalanceRemaining + HolidayTaken);
            float RAGdelta = Math.Abs(takenPerc - percToTake);
            
            if(RAGdelta >= 0.2)
            {
                return 'R';
            }
            else if(RAGdelta >= 0.1)
            {
                return 'A';
            }
            else
            {
                return 'G';
            }
        }
    }
}