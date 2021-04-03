using System;
using System.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
namespace ClearAction.Modules.SolveSpaces.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// 
    /// This concreted data provider class provides the implementation of the abstract methods 
    /// from data dataprovider.cs
    /// 
    /// In most cases you will only modify the Public methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        // used to prefect your database objects (stored procedures, tables, views, etc)
        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private static object GetNull(object field)
        {
            return Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region "Solve-Spaces" Methods

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}

        public override System.Data.IDataReader ListSolveSpaces(int UserID,int CatID, string Option, string SearchTerm,int LoggedInUser,int UptoNDays, bool IncludeDeleted)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListSolveSpaces", UserID, CatID, Option, SearchTerm, LoggedInUser, UptoNDays, IncludeDeleted);
        }

        /// <summary>
        /// return the list of solve spaces completed in last 5 days
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public override System.Data.IDataReader ListRecommendedSolveSpaces(int UserID,int TopN)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListSolveSpacesTopN", UserID, TopN);
        }

        public override System.Data.IDataReader ListRecommendedSolveSpacesSearch(int UserID, int TopN,string search)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListSolveSpacesTopNSearch", UserID,TopN,search);
        }

        public override System.Data.IDataReader ListSolveSpacesWithStatus(int UserID,int CatID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListSolveSpacesWithStatus", UserID, CatID,"",-1,-1);
        }

        public override System.Data.IDataReader ListSolveSpacesWithStatus(int UserID, int CatID, string SearchKey, int PageIndex, int PageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListSolveSpacesWithStatus", UserID, CatID,  SearchKey, PageIndex, PageSize);
        }
        public override int CreateNew(string Title, string ShortDescription, int DurationInMin, int TotalSteps, string TabLink, int TopicId)
        {
            return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, "ClearAction_SolveSpace_CreateNew", Title, ShortDescription, DurationInMin, TotalSteps, TabLink,  TopicId).ToString());
        }

        public override void Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "ClearAction_SolveSpace_Delete", ID);
        }
        public override void UnDelete(int ID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "ClearAction_SolveSpace_UnDelete", ID);
        }
        public override void Update(int ID, string Title, string ShortDescription, int DurationInMin, int TotalSteps, string TabLink,int TopicID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "ClearAction_SolveSpace_Update", ID, Title, ShortDescription, DurationInMin, TotalSteps, TabLink,TopicID);
        }

        public override void Add2Category(int SolveSpaceID, int CategoryID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_AddSSToCategory", SolveSpaceID, CategoryID);
        }

        public override void ClearCategories(int SolveSpaceID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_SSClearCategory", SolveSpaceID);
        }

        public override System.Data.IDataReader ListSSCategories(int SolveSpaceID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListSSCategories", SolveSpaceID);
        }
        public override IDataReader GetSolveSpaceByID(int SolveSpaceID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "ClearAction_SolveSpace_GetByID", SolveSpaceID);
        }

        #endregion

        #region "Global Categories" Method
        public override System.Data.IDataReader ListCategories()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListCategories",-1);
        }

        public override System.Data.IDataReader ListAllCategories(int PI, int PS)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListAllCategories",PI,PS);
        }

        public override void  DeleteCategory(int CategoryID)
        {
          SqlHelper.ExecuteNonQuery(ConnectionString, "CA_DeleteCategory", CategoryID);
        }

        public override void UpdateCategory(int CategoryID, string CategoryName, bool isActive, int CompID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_UpdateCategory", CategoryID, CategoryName, isActive,CompID);
        }

        #endregion
       
        #region UserResponses
        public override IDataReader GetUserResponses(int UserID, int SSID,int StepID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "sp_GetSolveSpaceReportEntrySummary", UserID, SSID,StepID);
        }
        #endregion

        #region "Forum Topic related"
        public override int SaveSolveSpaceTopic(int TopicID, string Title, string Summary)
        {
           return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, "activeforums_Topics_Save", 0, TopicID, 0, 0, false, false, "", -1, true, false, false, false, null, null, Title, Summary, Summary, DateTime.Now, DateTime.Now, -1, "", "", 0, 0, "", "", -99).ToString());
        }
        #endregion
    }
}