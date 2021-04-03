using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.ComponentModel.DataAnnotations;
namespace ClearAction.Modules.ComponentManager.Components
{
    [TableName("ClearAction_GlobalCompany")]
    [PrimaryKey("CompanyId")]
    public class CA_GlobalCompany
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        public bool IsActive { get; set; }

        [ReadOnlyColumn]
        public int TotalRecords { get; set; }


    }
}