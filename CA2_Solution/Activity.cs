using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2_Solution
{
    //This creates the enum outside of class
    public enum ActivityType { Air, Water, Land }

    public class Activity : IComparable
    {
        //Properties
        public string Name { get; set; }
        public DateTime ActivityDate { get; set; }
        public ActivityType TypeOfActivity { get; set; }
        public decimal Cost { get; set; }

        private string _description;

        //Long hand approach
        public string Description
        {
            get
            {
                return string.Format("{0}  Cost - {1:C}", _description, Cost);
            }
            set
            {
                _description = value;
            }
        }

        //For sorting by activity date
        public int CompareTo(object obj)
        {
            Activity that = obj as Activity;
            return this.ActivityDate.CompareTo(that.ActivityDate);
        }

        //For string display
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, ActivityDate.ToShortDateString());
        }
    }
}
