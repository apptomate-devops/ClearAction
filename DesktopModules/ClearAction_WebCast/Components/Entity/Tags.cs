using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction.Modules.WebCast.Components
{

    [TableName("WCC_Tags")]
    [PrimaryKey("TagId")]
    public class Tags
    {
        public int TagId { get; set; }

        public int Portalid { get; set; }

        public int ModuleId { get; set; }

        public string TagName { get; set; }
        public int Clicks { get; set; }


        public int Items { get; set; }

        public int Priority { get; set; }

        [ReadOnlyColumn]
        public int WCCID { get; set; }

    }

    public class CA_TaxonomyTerms
    {
        public int TermID { get; set; }
        public string Name { get; set; }
        public int ParentTermID { get; set; }
        public string VocabularyName { get; set; }
    }
}