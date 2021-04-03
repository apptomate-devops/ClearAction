using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace ClearAction.Modules.ComponentManager.Data
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

        #region Public Methods

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}

        public override void AssignItemToUser(int ComponentID, int ItemID, int UserID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_AssignItemToUser", ComponentID, ItemID, UserID,-1);
        }

        public override void AssignItemToUser(int ComponentID, int ItemID, int UserID, int AssignedBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_AssignItemToUser", ComponentID, ItemID, UserID, AssignedBy);
        }

        public override void DeAssignComponent(int ComponentId, int UserID,int ItemID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_DeAssignComponent", ComponentId, UserID, ItemID);
        }

        public override void RemoveFromMyVault(int ComponentID, int UserID, int ItemID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_RemoveFromMyVault", ComponentID, UserID, ItemID);
        }

        public override void SaveUserVisit(int UserID, int ItemID, int ComponentTypeID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_SaveUserVisit", UserID, ItemID, ComponentTypeID);
        }
        #endregion

        #region Blogs Methods

        public override System.Data.IDataReader ListBlogsWithStatus(int UserID, int CatID, string SearchKey, int PI,int PS)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListBlogsWithStatus", UserID, CatID, SearchKey, PI,PS);
        }

        public override System.Data.IDataReader ListForumsWithStatus(int UserID, int CatID, string SearchKey, int PI, int PS)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListForumsWithStatus", UserID, CatID, SearchKey, PI, PS);
        }
        #endregion


        #region "Component Master methods"

        public override System.Data.IDataReader ListComponentMaster()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListComponentMaster");
        }

        #endregion

        #region "Taxonomy Terms methods"

        public override System.Data.IDataReader ListTaxonomyTerms(int PI, int PS)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListTaxonomyTerms",PI, PS);
        }


        public override void DeleteTaxonomyTerm(int TermID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_DeleteTaxonomyTerm", TermID);
        }


        public override void AddTaxonomyTerm(string ParentTerm, string Term)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InsertTaxmonyTerms", ParentTerm, Term);
        }


        public override System.Data.IDataReader ListAllTaxonomyTerms()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListAllTaxonomyTerms");
        }

        public override void AddRelatedTags(int RelatedTagID , int TermID, int UserID,  string Tags)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "CA_UpdateRelatedTags", RelatedTagID, TermID, UserID, Tags );
        }

        public override System.Data.IDataReader GetRelatedTags(int TermID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_GetRelatedTags", TermID);
        }
        public override System.Data.IDataReader GetRelatedTagList(string tag)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_GetRelatedTagList", tag);
        }

        #endregion
        #region "Global Company"
        public override System.Data.IDataReader GetListOfCompany(int iPageNumber, int iPageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "CA_ListCompany", iPageNumber, iPageSize);
        }
        #endregion

    }

}