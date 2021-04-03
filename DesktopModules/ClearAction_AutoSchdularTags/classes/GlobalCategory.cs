using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction_AutoSchdularTags
{
    [TableName("Blog_PostCategoryRelation")]
    [PrimaryKey("CategoryId")]
    public class GlobalCategory
    {

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public bool IsActive { get; set; }


        public int ComponentId { get; set; }
        public int OptionOrder { get; set; }
        public string DisplayName { get; set; }

        // public int WeightFactor { get; set; }

    }

    public class GlobalCategoryRelation
    {
        public int CategoryID { get; set; }
        public int iBlogCount { get; set; }

        private List<int> _BlogContentID;
        public List<int> BlogContentID
        {

            get
            {
                if (_BlogContentID == null)
                    _BlogContentID = new List<int>();
                return _BlogContentID;
            }
            set
            {


                _BlogContentID = value;
            }
        }
        public int WeightCount { get; set; }
    }
}