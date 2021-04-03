using ClearAction.Modules.ProfileClearAction_ProfileSetup.Components;
using ClearAction.Modules.ProfileClearAction_ProfileSetup.Services.ViewModels;
using DotNetNuke.Common;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Services
{


    //[SupportedModules("ClearAction_ProfileSetup")]
    //[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Anonymous)]
    [AllowAnonymous]
    public partial class ProfileController : APIControllerBase
    {
        [HttpGet]
       // [ValidateAntiForgeryToken]
        public HttpResponseMessage GetListQuestion()
        {

            List<QuestionViewModel> items;

            if (Globals.IsEditMode())
            {
                items = new DbController().GetQuestion(true)
                       .Select(item => new QuestionViewModel(item, ""))
                       .ToList();
            }
            else
            {
                items = new DbController().GetQuestion(false)
                       .Select(item => new QuestionViewModel(item,""))
                       .ToList();
            }

            JavaScriptSerializer jser = new JavaScriptSerializer();
         
            return Request.CreateResponse(jser.Serialize(items));
        }

    }
}
