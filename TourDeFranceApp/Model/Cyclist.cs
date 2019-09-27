using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourDeFranceApp.Model
{
    class Cyclist
    {
        public Cyclist()
        {

        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        private string resultTime;

        public string ResultTime
        {
            get { return resultTime; }
            set { resultTime = value; }
        }

        private int endPosition;

        public int EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; }
        }
        

        public override string ToString()
        {
            return "Cyclist: " + Name + " " + Gender+" "+Country+" "+ResultTime+" "+EndPosition;
        }

    }
}
