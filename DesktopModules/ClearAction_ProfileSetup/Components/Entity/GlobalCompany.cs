using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{

    [TableName("ClearAction_GlobalCompany")]
    [PrimaryKey("CompanyId")]
    public class CA_GlobalCompany
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }


        public bool IsActive { get; set; }


        /*
         * 
         * These column will map with GlobalCategory Table so mapping and options binding map with other questions
         */
        [ReadOnlyColumn]
        public int QuestionOptionID { get; set; }

        [ReadOnlyColumn]
        public int QuestionID { get; set; }

        [ReadOnlyColumn]
        public int RelatedEntityID { get; set; }


        [ReadOnlyColumn]
        public string OptionText { get; set; }
    }
}