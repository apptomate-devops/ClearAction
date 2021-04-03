using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using ClearAction.Modules.ComponentManager.Data;
using DotNetNuke.Common.Utilities;

using System.Linq;
using DotNetNuke.Data;

namespace ClearAction.Modules.ComponentManager.Components
{
    //uncomment the interfaces to add the support.
    public class CompanyController //: IPortable, ISearchable, IUpgradeable
    {
        #region "Global Company Method"

        public void AddCompany(CA_GlobalCompany company)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<CA_GlobalCompany>();
                    rep.Insert(company);
                }
            }
            catch (System.Exception ex)
            {


            }
        }

        public void UpdateCompany(CA_GlobalCompany company)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<CA_GlobalCompany>();
                    rep.Update(company);
                }
            }
            catch (System.Exception ex)
            {


            }
        }


        public CA_GlobalCompany GetCompanyByID(int CompanyID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<CA_GlobalCompany>();
                    return rep.GetById(CompanyID);
                }
            }
            catch (System.Exception ex)
            {


            }
            return null;
        }


        public int DeleteCompany(int CompanyID)
        {
            try
            {

                var company = GetCompanyByID(CompanyID);
                if (company != null)
                {
                    using (IDataContext ctx = DataContext.Instance())
                    {
                        var rep = ctx.GetRepository<CA_GlobalCompany>();
                        rep.Delete(company);
                    }
                }
                return 1;
            }
            catch (System.Exception ex)
            {


            }
            return 0;
        }
        public List<CA_GlobalCompany> GetCompanys(int CurrentPage, int PageSize)
        {
            var olist = new List<CA_GlobalCompany>();
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<CA_GlobalCompany>();
                    olist = rep.Get().ToList();
                }
            }
            catch (System.Exception ex)
            {


            }

            if (olist != null && olist.Count > 0)
                olist[0].TotalRecords = olist.Count;
            return olist.Skip((CurrentPage - 1) * PageSize).Take(PageSize).OrderBy(x => x.CompanyName).ToList();

        }


        public List<CA_GlobalComp> GetListofGlobalCompany(int iCurrentPage, int iPageSize)
        {   
            return CBO.FillCollection<CA_GlobalComp>(ClearAction.Modules.ComponentManager.Data.DataProvider.Instance().GetListOfCompany(iCurrentPage, iPageSize));
        }
         
        public CA_GlobalCompany SearchCompany(string term)
        {
            IQueryable<CA_GlobalCompany> iresult = null;
            try
            {


                string strQuery = "SELECT * FROM ClearAction_GlobalCompany Where CompanyName ='" + term + "'";
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<CA_GlobalCompany>();
                    iresult = ctx.ExecuteQuery<CA_GlobalCompany>(System.Data.CommandType.Text, strQuery).AsQueryable();
                }
            }
            catch (System.Exception ex)
            {


            }

            if (iresult != null)
            {
                if (iresult.ToList().Count > 0) return iresult.ToList()[0];

            }
            return null;


        }


        #endregion
    }

}
