using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourDeFranceApp.Model
{
    class StageInfo
    {
        private string sport;

        public string Sport
        {
            get { return sport; }
            set { sport = value; }
        }

        private string stageName;

        public string StageName
        {
            get { return stageName; }
            set { stageName = value; }
        }

        private string distance;

        public string Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private string dateOfEvent;

        public string Date
        {
            get { return dateOfEvent; }
            set { dateOfEvent = value; }
        }

        public StageInfo()
        {

        }


        public override string ToString()
        {
            return "Cyclist: " + StageName + " " + Sport + " " + Date;
        }
    }
}
