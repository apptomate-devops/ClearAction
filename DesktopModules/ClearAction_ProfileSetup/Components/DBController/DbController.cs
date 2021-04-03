
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using DotNetNuke.Common.Utilities;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    /// <summary>
    /// Summary description for ProfileResponse
    /// </summary>
    public partial class DbController
    {

        #region "Questions"
        private IQueryable<Question> GetQuestions(bool IsAdmin)
        {
            IQueryable<Question> t = GetQuestion();
            if (IsAdmin)
                return t.OrderBy(x => x.QuestionOrder);
            return t.Where(x => x.IsActive).OrderBy(x => x.QuestionOrder);
        }


        public List<Question> GetQuestion(bool IsAdmin)
        {
            IQueryable<Question> t = GetQuestion();

            return t.ToList();

        }

        private IQueryable<Question> GetQuestion()
        {
            IQueryable<Question> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Question>();

                t = rep.Get().AsQueryable();
            }
            return t;
        }

        public List<Question> GetQuestionByStep(bool isAdmin, int StepID)
        {
            IQueryable<Question> t = GetQuestions(isAdmin);
            return t.Where(x => x.StepID == StepID).ToList();
        }

        public IQueryable<Question> GetAllQuestion(bool isAdmin)
        {
            IQueryable<Question> t = GetQuestions(isAdmin);
            return t;
        }

        public IQueryable<QuestionOption> GetQuestionOption(int iQuestionid)
        {
            IQueryable<QuestionOption> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionOption>();

                t = rep.Find("Where QuestionID=@0", iQuestionid).AsQueryable();
            }
            return t.OrderBy(x => x.OptionOrder);
        }

        public IQueryable<QuestionOption> GetQuestionOption(int iQuestionid, int SubCategoryID)
        {
            IQueryable<QuestionOption> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionOption>();

                t = rep.Find("Where QuestionID=@0 AND SubCategory=@1", iQuestionid, SubCategoryID).AsQueryable();
            }
            return t.OrderBy(x => x.OptionOrder);
        }

        public List<QuestionOption> GetQuestionGlobcalCategory(int iQuestionid)
        {
            IQueryable<QuestionOption> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {

                return ctx.ExecuteQuery<QuestionOption>(System.Data.CommandType.StoredProcedure, "ClearAction_ProfileGlobalCategory", iQuestionid).ToList();
            }

        }
        public List<CA_GlobalCompany> GetCompanyList(int iQuestionid)
        {
            IQueryable<CA_GlobalCompany> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {

                return ctx.ExecuteQuery<CA_GlobalCompany>(System.Data.CommandType.StoredProcedure, "ClearAction_ProfileGlobalCompany", iQuestionid).ToList();
            }

        }



        public List<QuestionCategory> GetMainCategoryByQuestionID(int iQuestionID)
        {


            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionCategory>();

                return ctx.ExecuteQuery<QuestionCategory>(System.Data.CommandType.StoredProcedure, "ClearChoice_GetMainCategoryByQuestionID", iQuestionID).ToList();
            }

        }

        #region "Category Based questions"
        public IQueryable<QuestionCategory> GetTopCategory()
        {
            return GetCategoryByParentID(-1);
        }
        public IQueryable<QuestionCategory> GetChildCategory(int ParentID)
        {
            return GetCategoryByParentID(ParentID);
        }
        /// <summary>
        /// added cpndition to hide
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        private IQueryable<QuestionCategory> GetCategoryByParentID(int ParentID)
        {
            IQueryable<QuestionCategory> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionCategory>();

                t = rep.Find("Where ParentID=@0 AND IsActive=1", ParentID).AsQueryable();
            }
            return t.OrderBy(x => x.CategoryOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        private QuestionCategory GetCategoryByID(int CategoryID)
        {
            QuestionCategory t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionCategory>();

                t = rep.Get().SingleOrDefault();
            }
            return t;
        }

        #endregion 

        #endregion

        #region "Profile Response"

        public void AddUpdateProfile(List<ProfileResponse> oProfileResponseList)
        {

            string CaptchaCacheKey = "Profileresponse";

            DataCache.ClearCache(CaptchaCacheKey);

            foreach (ProfileResponse oProfileResponse in oProfileResponseList)
            {
                AddUpdateProfile(oProfileResponse);

            }
        }

        public void UpdateTagsInformation(int iUser)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {


                    ctx.Execute(System.Data.CommandType.StoredProcedure, "CA_InsertUserPerference", iUser);
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        public void AddUpdateProfile(ProfileResponse oProfileResponse)
        {
            var t = FindProfile(oProfileResponse);
            if (t != null)
            {
                oProfileResponse.ProfileResponseId = t.ProfileResponseId;

                UpdateQuestionResponse(oProfileResponse);

            }
            else

                AddQuestionResponse(oProfileResponse);


            if (oProfileResponse.QuestionOptionId == (int)Utiity.ControlType.RadioButton || oProfileResponse.QuestionOptionId == (int)Utiity.ControlType.DropDown || oProfileResponse.QuestionOptionId == (int)Utiity.ControlType.MultiSelect || oProfileResponse.QuestionOptionId == (int)Utiity.ControlType.GlobalCategory)
            {
                //DeleteResponseByQuestionResponseID(oProfileResponse.ProfileResponseId);
                AddUpdateQuestionOptions(oProfileResponse.LstProfileResponseOption, oProfileResponse.ProfileResponseId);
            }
        }
        private void AddQuestionResponse(ProfileResponse oProfileResponse)
        {
            try
            {


                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponse>();

                    rep.Insert(oProfileResponse);
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void UpdateQuestionResponse(ProfileResponse oProfileResponse)
        {
            try
            {



                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponse>();

                    rep.Update(oProfileResponse);
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void AddUpdateQuestionOptions(List<ProfileResponseOption> lst, int ProfileResponseId)
        {
            if (lst != null)
            {
                // DeleteResponseByQuestionResponseID(ProfileResponseId);

                foreach (ProfileResponseOption p in lst)
                {


                    p.ProfileResponseID = ProfileResponseId;

                    ///   p.ProfileResponseId = ProfileResponseId;
                    AddProfileResponseOption(p);
                }
            }
        }

        private List<ProfileResponseOption> GetQuestionByResponseID(int ProfileResponseId)
        {

            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponseOption>();

                    return rep.Find("Where QuestionOptionID=@0", ProfileResponseId).ToList();
                }
            }
            catch (Exception exc)
            {
            }
            return null;

        }
        private void DeleteResponseByQuestionResponseID(int ProfileResponseId)
        {

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponseOption>();
                    ctx.ExecuteQuery<int>(System.Data.CommandType.StoredProcedure, "ClearAction_DeleteByResponseID", ProfileResponseId);
                }
            }
            catch (Exception exc)
            {


            }


        }

        private ProfileResponseOption GetProfileResponseByOptionID(ProfileResponseOption oitem)
        {
            try
            {
                ProfileResponseOption t;

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponseOption>();

                    t = rep.Find("WHERE QuestionOptionID=@0 AND QuestionID=@1 AND ProfileResponseID=@2", oitem.QuestionOptionID, oitem.QuestionID, oitem.ProfileResponseID).SingleOrDefault();
                }

                return t;
            }
            catch (Exception exc)
            {


            }
            return null;
        }
        private void AddProfileResponseOption(ProfileResponseOption oitem)
        {
            try
            {

                ProfileResponseOption t = GetProfileResponseByOptionID(oitem);
                if (t != null)
                {
                    oitem.ProfileResponseOptionId = t.ProfileResponseOptionId;

                    using (IDataContext ctx = DataContext.Instance())
                    {
                        var rep = ctx.GetRepository<ProfileResponseOption>();

                        rep.Update(oitem);
                    }

                }
                else

                    using (IDataContext ctx = DataContext.Instance())
                    {
                        var rep = ctx.GetRepository<ProfileResponseOption>();

                        rep.Insert(oitem);
                    }

            }
            catch (Exception exc)
            {


            }
        }

        private void UpdateProfileResponseOption(ProfileResponseOption oitem)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponseOption>();

                    rep.Update(oitem);
                }
            }
            catch (Exception exc)
            {


            }
        }

        private ProfileResponse FindProfile(ProfileResponse oProfileResponse)
        {
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponse>();

                    return rep.Find("Where QuestionId=@0 AND UserID=@1", oProfileResponse.QuestionId, oProfileResponse.UserID).SingleOrDefault();

                }

            }
            catch (Exception ex)
            {


            }
            return null;
        }

        //public ProfileResponse GetQuestionResponse(int iUserid, int iQuestionID)
        //{
        //    try
        //    {

        //        return GetQuestionResponse(iUserid).Where(x => x.QuestionId == iQuestionID).SingleOrDefault();

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return null;
        //}

        public ProfileResponse GetQuestionResponse(int iUserid, int iQuestionID)
        {
            try
            {

                var oProfileResponse = GetQuestionResponse(iUserid).Where(x => x.QuestionId == iQuestionID).SingleOrDefault();

                if (oProfileResponse != null)
                {
                    if (oProfileResponse.QuestionOptionId == (int)Components.Utiity.ControlType.DropDown || oProfileResponse.QuestionOptionId == (int)Components.Utiity.ControlType.RadioButton || oProfileResponse.QuestionOptionId == (int)Components.Utiity.ControlType.SocialMedia || oProfileResponse.QuestionOptionId == (int)Components.Utiity.ControlType.MultiSelect || oProfileResponse.QuestionOptionId == (int)Components.Utiity.ControlType.GlobalCategory)
                    {
                        oProfileResponse.LstProfileResponseOption = GetProfileReponseOption(oProfileResponse.ProfileResponseId);

                    }
                }
                return oProfileResponse;

            }
            catch (Exception ex)
            {


            }
            return null;
        }
        private List<ProfileResponseOption> GetProfileReponseOption(int ProfileResponseID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<ProfileResponseOption>();

                    return rep.Find("Where ProfileResponseID=@0", ProfileResponseID).ToList();
                }
            }
            catch (Exception exc)
            {


            }
            return null;

        }
        public IQueryable<ProfileResponse> GetQuestionResponse(int iUserid)
        {
            string CaptchaCacheKey = "Profileresponse";

            var oQuuestioResponse = DataCache.GetCache(CaptchaCacheKey);
            try
            {

                if (oQuuestioResponse == null)
                    using (IDataContext ctx = DataContext.Instance())
                    {
                        var rep = ctx.GetRepository<ProfileResponse>();

                        oQuuestioResponse = rep.Find("Where UserID=@0", iUserid).AsQueryable();

                    }
                
                return (IQueryable<ProfileResponse>)oQuuestioResponse;

            }
            catch (Exception ex)
            {


            }
            return null;
        }
        public List<QuestionOption> GetUserProfileCategories(int UserID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    return ctx.ExecuteQuery<QuestionOption>(System.Data.CommandType.StoredProcedure, "CA_GetUserProfileCategories", UserID).ToList();
                }
            }
            catch (Exception exc)
            {
            }
            return null;
        }

        public string GetCompanyName(int CompanyID)
        {


            string strResponse = string.Empty;
            IQueryable<string> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<string>();

                t = rep.Find("SELECT CompanyName FROM ClearAction_GlobalCompany Where  CompanyId=@0", CompanyID).AsQueryable();

            }
            if (t != null)
                return t.SingleOrDefault();
            return strResponse;



        }
        #endregion




    }
}