using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClearAction.Modules.WebCast.Controls
{
    public partial class TopHeader : ModuleBase
    {



        private int _IsMyVault;

        public int IsMyVault
        {
            get
            {
                if (((GetQueryStringValue("", -99) != -99) | (Session["CA_Blog_IsMyVault"] != null)))
                {
                    _IsMyVault = int.Parse(Session["CA_Blog_IsMyVault"].ToString());
                }
                else
                {
                    _IsMyVault = 1;
                }

                return _IsMyVault;
            }

            set
            {
                _IsMyVault = value;
            }
        }

        public int CurrentStatus
        {
            get
            {
                int iReturn = 0;
                try
                {
                    if (Session["CA_Blog_Status"] != null)
                    {
                        iReturn = int.Parse(Session["CA_Blog_Status"].ToString());
                    }
                    else
                    {
                        iReturn = 0;
                    }
                }
                catch (Exception ex)
                {
                }

                return iReturn;
            }
        }
        private int _Mid;


        public int MId
        {
            get
            {
                return _Mid;
            }
            set
            {
                _Mid = value;

            }
        }

        private int _tID;
        public int tID
        {
            get
            {
                return _tID;
            }

            set
            {
                _tID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //hyperlinkNew.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(tID, "Edit", "mid=" + MId, Components.Util.PostKey + "=-1");
            }
        }

      

        protected void btnMyVault_Click(object sender, EventArgs e)
        {

        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {

        }

        protected void btnAllCat_Click(object sender, EventArgs e)
        {

        }

       
    }
}