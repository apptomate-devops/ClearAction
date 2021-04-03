using System.Data;
using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;

namespace ClearAction.Modules.ComponentManager.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// An abstract class for the data access layer
    /// 
    /// The abstract data provider provides the methods that a control data provider (sqldataprovider)
    /// must implement. You'll find two commented out examples in the Abstract methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class DataProvider
    {

        #region Shared/Static Methods

        private static DataProvider provider;

        // return the provider
        public static DataProvider Instance()
        {
            if (provider == null)
            {
                const string assembly = "ClearAction.Modules.ComponentManager.Data.SqlDataprovider,ClearAction_ComponentManager";
                Type objectType = Type.GetType(assembly, true, true);

                provider = (DataProvider)Activator.CreateInstance(objectType);
                DataCache.SetCache(objectType.FullName, provider);
            }

            return provider;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not returning class state information")]
        public static IDbConnection GetConnection()
        {
            const string providerType = "data";
            ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);

            Provider objProvider = ((Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);
            string _connectionString;
            if (!String.IsNullOrEmpty(objProvider.Attributes["connectionStringName"]) && !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]]))
            {
                _connectionString = System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                _connectionString = objProvider.Attributes["connectionString"];
            }

            IDbConnection newConnection = new System.Data.SqlClient.SqlConnection();
            newConnection.ConnectionString = _connectionString.ToString();
            newConnection.Open();
            return newConnection;
        }

        #endregion

        #region Component Management methods

        //public abstract IDataReader GetItems(int userId, int portalId);

        //public abstract IDataReader GetItem(int itemId);        
            public abstract void AssignItemToUser(int ComponentID, int ItemID, int UserID);
        public abstract void AssignItemToUser(int ComponentID, int ItemID, int UserID, int AssignedBy);
            public abstract void DeAssignComponent(int ComponentId, int UserID,int ItemID);
        public abstract void RemoveFromMyVault(int ComponentID, int UserID, int ItemID);

            public abstract IDataReader ListForumsWithStatus(int UserID, int CatID, string SearchKey, int PI,int PS);
        public abstract void SaveUserVisit(int UserID, int ItemID, int ComponentTypeID);
        #endregion

        #region Blogs retlated methods
        public abstract IDataReader ListBlogsWithStatus(int UserID, int CatID,string SearchKey, int PI, int PS);
        #endregion


        #region "Component Manager methods"
        /* added by Kusum on 9-Dec-17 */
        public abstract IDataReader ListComponentMaster();

        #endregion



        #region "Taxonomy Terms methods"
        /* added by Kusum on 9-Dec-17 */
        public abstract IDataReader ListTaxonomyTerms(int PI, int PS);

        public abstract void DeleteTaxonomyTerm(int TermID);

        public abstract void AddTaxonomyTerm(string ParentTerm, string Term);

        public abstract IDataReader ListAllTaxonomyTerms();

        public abstract void AddRelatedTags(int RelatedTagID, int TermID, int UserID, string Tags);

        public abstract IDataReader GetRelatedTags(int TermID);
        public abstract IDataReader GetRelatedTagList(string tag);

        #endregion

        #region "Global Company"

        public abstract IDataReader GetListOfCompany(int iPageNumber, int iPageSize);
        #endregion  

    }
}