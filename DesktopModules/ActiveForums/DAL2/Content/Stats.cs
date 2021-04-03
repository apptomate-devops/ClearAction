using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    public class Stats
    {

        public int StatID { get; set; }

        public int TotalFileToday { get; set; }

        public int ActiveMember { get; set; }
        public int TotalTopics { get; set; }

    }
}
