using System;
using avt.ActionForm.Core;
using DnnSharp.Common;
using DnnSharp.Common.Licensing.v3;
using System.Web.UI;

namespace avt.ActionForm.RegCore {
    public partial class Activate : ActivatePageBase {
        public Activate() : base(App.Info) {
        }
        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);
            ClientResManager.RegisterCss(Page, App.Info, App.Info.CommonUrl + "/static/bootstrap337/css/bootstrap.min.css?v=" + App.Info.Build);
            ClientResManager.RegisterCss(Page, App.Info, App.Info.CommonUrl + "/static/dnnsf/css/activate.css?v=" + App.Info.Build);
            ClientResManager.RegisterCss(Page, App.Info, App.Info.CommonUrl + "/static/bootstrap337/css/bootstrap.min.css?v=" + App.Info.Build);
            Page.ClientScript.RegisterClientScriptInclude(typeof(Page), "dnnsftoast", App.Info.CommonUrl + "/static/dnnsf/dnnsf.js?v=" + App.Info.Build);
            ClientResManager.RegisterJquery(Page, AppInfo);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "dnnsfjQuery", "if (typeof(dnnsfjQuery) == 'undefined') dnnsfjQuery = jQuery || $;", true);
        }
    }
}
