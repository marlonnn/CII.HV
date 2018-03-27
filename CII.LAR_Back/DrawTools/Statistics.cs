using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// circumference and area statistics of graphic object
    /// Author:Zhong Wen 2017/07/31
    /// </summary>
    public class Statistics
    {
        private string circumference;

        public string Circumference
        {
            get
            {
                return circumference;
            }
            set
            {
                circumference = value;
            }
        }

        private string area;

        public string Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }

        public Statistics()
        {

        }

        public Statistics(string circumference, string area)
        {
            this.circumference = circumference;
            this.area = area;
        }
    }
}
