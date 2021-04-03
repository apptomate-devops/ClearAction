/*
' Copyright (c) 2018 ClearAction
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace ClearAction.Modules.WebCastClearAction_WebCast.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for ClearAction_WebCast
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<ClearAction_WebCastInfo> colClearAction_WebCasts = GetClearAction_WebCasts(ModuleID);
        //if (colClearAction_WebCasts.Count != 0)
        //{
        //    strXML += "<ClearAction_WebCasts>";

        //    foreach (ClearAction_WebCastInfo objClearAction_WebCast in colClearAction_WebCasts)
        //    {
        //        strXML += "<ClearAction_WebCast>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objClearAction_WebCast.Content) + "</content>";
        //        strXML += "</ClearAction_WebCast>";
        //    }
        //    strXML += "</ClearAction_WebCasts>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlClearAction_WebCasts = DotNetNuke.Common.Globals.GetContent(Content, "ClearAction_WebCasts");
        //foreach (XmlNode xmlClearAction_WebCast in xmlClearAction_WebCasts.SelectNodes("ClearAction_WebCast"))
        //{
        //    ClearAction_WebCastInfo objClearAction_WebCast = new ClearAction_WebCastInfo();
        //    objClearAction_WebCast.ModuleId = ModuleID;
        //    objClearAction_WebCast.Content = xmlClearAction_WebCast.SelectSingleNode("content").InnerText;
        //    objClearAction_WebCast.CreatedByUser = UserID;
        //    AddClearAction_WebCast(objClearAction_WebCast);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<ClearAction_WebCastInfo> colClearAction_WebCasts = GetClearAction_WebCasts(ModInfo.ModuleID);

        //foreach (ClearAction_WebCastInfo objClearAction_WebCast in colClearAction_WebCasts)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objClearAction_WebCast.Content, objClearAction_WebCast.CreatedByUser, objClearAction_WebCast.CreatedDate, ModInfo.ModuleID, objClearAction_WebCast.ItemId.ToString(), objClearAction_WebCast.Content, "ItemId=" + objClearAction_WebCast.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

    }

}
