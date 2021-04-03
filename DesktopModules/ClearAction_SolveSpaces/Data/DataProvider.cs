using System.Data;
using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;


namespace ClearAction.Modules.SolveSpaces.Data
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
                const string assembly = "ClearAction.Modules.SolveSpaces.Data.SqlDataprovider,ClearAction_SolveSpaces";
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

        #region Solve Spaces Methods

        //public abstract IDataReader GetItems(int userId, int portalId);

        //public abstract IDataReader GetItem(int itemId);        


        public abstract IDataReader ListSolveSpaces(int UserID,int CatID,string Option,string SearchTerm,int LoggedInUser,int UptoNDays,bool IncludeDeleted);
        
        /// <summary>
        /// Return the list of completed solve spaces in last 5 days
        /// </summary>
        /// <param name="UserID">ID of User</param>
        /// <returns></returns>
        public abstract IDataReader ListRecommendedSolveSpaces(int UserID,int TopN);
        public abstract IDataReader ListRecommendedSolveSpacesSearch(int UserID, int TopN,string SearchTerm);

        /// <summary>
        /// List having assigned status Yes/No
        /// </summary>
        /// <param name="UserID">ID of User</param>
        /// <returns></returns>
        public abstract IDataReader ListSolveSpacesWithStatus(int UserID,int CatID);
        public abstract IDataReader ListSolveSpacesWithStatus(int UserID, int CatID, string SearchKey, int PageIndex, int PageSize);

        public abstract int CreateNew(string Title, string ShortDescription, int DurationInMin, int TotalSteps, string TabLink,int TopicId);

        public abstract void Delete(int ID);
        public abstract void UnDelete(int ID);
        public abstract void Update(int ID, string Title, string ShortDescription, int DurationInMin, int TotalSteps, string TabLink,int TopicId);
        public abstract IDataReader GetSolveSpaceByID(int SolveSpaceID);
        public abstract void Add2Category(int SolveSpaceID, int CategoryID);
        public abstract void ClearCategories(int SolveSpaceID);

        public abstract IDataReader ListSSCategories(int SolveSpaceID);
        #endregion

        #region Global Categories
        public abstract IDataReader ListCategories();

        /* added by kusum : 8-Dec-17*/
        public abstract IDataReader ListAllCategories(int PI, int PS);
        public abstract void  DeleteCategory(int CategoryID);
        public abstract void  UpdateCategory(int CategoryID, string CategoryName, bool isActive, int CompID);
        #endregion

        #region User Responses
        public abstract IDataReader GetUserResponses(int UserID, int SSID,int StepID);
        #endregion
        
        #region Forum Topic Related
            public abstract int SaveSolveSpaceTopic(int Topic, string Title, string Summary);
        #endregion


    }

}