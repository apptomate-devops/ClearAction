using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction_AutoSchdularTags
{
    public class Controller
    {


        public string UpdatePerference(int iUser)
        {

            string strRecord = string.Format("Blog:{0} ", AutoAssignBlog(iUser).ToString());
            strRecord += string.Format(", Forum {0} and ", AutoAssignForum(iUser).ToString());
            strRecord += string.Format("SolveSpace {0} ", AutoAssignSolveSpace(iUser));
            return strRecord;
        }

        private int AutoAssignBlog(int iUser)
        {
            //PetaPocoHelper.ExecuteNonQuery(GetConnectionstring, System.Data.CommandType.StoredProcedure, "CA_InsertUserPerference", iUser);
            var DBcontroller = new DBController();

            var lstBlog = DBcontroller.GetUserBlogPerference(iUser);
            int RecordCount = 0;
            //Get the category weight
            if (lstBlog != null)
            {

                var lstBlogToAssign = new List<GlobalCategory>();

                var lstCategory = new List<GlobalCategoryRelation>();
                if (lstBlog.Count > 0)
                {
                    foreach (BlogInfo oBlogInfo in lstBlog)
                    {


                        lstCategory.AddRange(MakeGlobalList(oBlogInfo.GetAssoicatedCategory, oBlogInfo.ContentItemid));

                    }

                    RecordCount = new DBController().AddToDatabase(lstCategory, 2, iUser);

                }






            }
            return RecordCount;



        }






        private int AutoAssignForum(int iUser)
        {
            var DBcontroller = new DBController();

            int RecordCount = 0;
            var lstBlog = DBcontroller.GetUserForumPerference(iUser);

            //Get the category weight
            if (lstBlog != null)
            {

                var lstBlogToAssign = new List<GlobalCategory>();

                var lstCategory = new List<GlobalCategoryRelation>();
                if (lstBlog.Count > 0)
                {
                    foreach (ForumInfo oForum in lstBlog)
                    {

                        lstCategory.AddRange(MakeGlobalList(oForum.GetAssoicatedCategory, oForum.ContentID));



                    }

                    RecordCount = DBcontroller.AddToDatabase(lstCategory, 1, iUser);

                }







            }
            return RecordCount;


        }



        private int AutoAssignSolveSpace(int iUser)
        {
            var DBcontroller = new DBController();

            int RecordCount = 0;
            var lstItem = DBcontroller.GetSolveSpaceByUserProfilePerference(iUser).ToList();

            //Get the category weight
            if (lstItem != null)
            {

                var lstBlogToAssign = new List<GlobalCategory>();

                var lstCategory = new List<GlobalCategoryRelation>();
                if (lstItem.Count > 0)
                {
                    foreach (SolveSpaceInfo oSolve in lstItem)
                    {

                        lstCategory.AddRange(MakeGlobalList(oSolve.GetAssoicatedCategory, oSolve.SolveSpaceID));



                    }

                    RecordCount = DBcontroller.AddToDatabase(lstCategory, 3, iUser);

                }







            }
            return RecordCount;


        }


        private List<GlobalCategoryRelation> MakeGlobalList(List<GlobalCategory> oBlogAssociatedCategory, int ContentItemID)
        {
            var lstCategory = new List<GlobalCategoryRelation>();
            //Collect all category and keep in list to find the weight and id of associated blog
            //   var oBlogAssociatedCategory = oForum.GetAssoicatedCategory;
            if (oBlogAssociatedCategory != null)
                foreach (GlobalCategory oGlobal in oBlogAssociatedCategory)
                {

                    bool ISFound = false;
                    if (lstCategory.Count == 0)
                    {
                        ISFound = true;
                    }
                    else
                        foreach (GlobalCategoryRelation oRelation in lstCategory)
                        {

                            if (oRelation.CategoryID == oGlobal.CategoryId)
                            {
                                oRelation.iBlogCount = oRelation.iBlogCount + 1;

                                ISFound = false;
                                oRelation.BlogContentID.Add(ContentItemID);
                            }

                            else
                            {
                                ISFound = true;
                            }

                        }

                    if (ISFound)
                    {

                        var oCategoryRelation = new GlobalCategoryRelation()
                        {
                            CategoryID = oGlobal.CategoryId,
                            iBlogCount = 1 //,
                                           //WeightCount = oGlobal.WeightFactor

                        };
                        oCategoryRelation.BlogContentID.Add(ContentItemID);
                        lstCategory.Add(oCategoryRelation);
                    }




                }
            return lstCategory;

        }
    }
}