using ClearAction_Chat.EntityDataModel;
using ClearAction_Chat.Models;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClearAction_Chat.Services
{
    public class ClearActionChatServices
    {
        //ClearActionChatEntities _dbContext = new ClearActionChatEntities();
        ClearActionLocalDBEntities _dbContext = new ClearActionLocalDBEntities();
        public List<DNNUserList> GetAllDNNUserList(int PortalId, int LoginUserId)
        {
            List<DNNUserList> objDNNUserList = new List<DNNUserList>();
            try
            {
                var UserList = UserController.GetUsers(PortalId);
                foreach (UserInfo item in UserList)
                {
                    if (LoginUserId != item.UserID)
                    {
                        string fullName = (string.IsNullOrEmpty(item.DisplayName)) ? item.Username : item.DisplayName;
                        objDNNUserList.Add(new DNNUserList
                        {
                            UserId = item.UserID,
                            UserName = item.Username,
                            FullName = fullName,
                            UserPhotoURL = item.Profile.PhotoURL,
                            IsUserOnline = false,
                        });
                    }
                }
            }
            catch
            {
                objDNNUserList = new List<DNNUserList>();
            }
            return objDNNUserList;
        }


        public long SaveConversionMessageDetail(string FromUserProfileId, string ToUserProfileId, string communicationMessage, string stringFormUserMessageTime)
        {
            long longFromUserProfileId = 0;
            long.TryParse(FromUserProfileId, out longFromUserProfileId);
            long longToUserProfileId = 0;
            long.TryParse(ToUserProfileId, out longToUserProfileId);
            long newAddedCommunicationId = 0;
            try
            {
                if (!string.IsNullOrEmpty(FromUserProfileId) && !string.IsNullOrEmpty(ToUserProfileId))
                {
                    ClearActionChat_Conversion objClearActionChat_Conversion = new ClearActionChat_Conversion();
                    objClearActionChat_Conversion.SentByUserId = FromUserProfileId;


                    objClearActionChat_Conversion.ConversionText = communicationMessage;
                    objClearActionChat_Conversion.IsAttachment = false;
                    objClearActionChat_Conversion.SentDateTime = stringFormUserMessageTime;

                    _dbContext.ClearActionChat_Conversion.Add(objClearActionChat_Conversion);
                    _dbContext.SaveChanges();
                    newAddedCommunicationId = objClearActionChat_Conversion.Id;
                }
            }
            catch (Exception ex)
            { newAddedCommunicationId = 0; }
            return newAddedCommunicationId;

        }


        public long SavePersonalConversionSendTo(string FromUserProfileId, string ToUserProfileId, string communicationMessage, long convarsionId)
        {
            long newSendToConversionId = 0;
            try
            {
                if (!string.IsNullOrEmpty(FromUserProfileId) && !string.IsNullOrEmpty(ToUserProfileId))
                {
                    if (convarsionId > 0)
                    {
                        ClearActionChat_Conversion_FromSent objClearActionChat_Conversion_FromSent = new ClearActionChat_Conversion_FromSent();
                        objClearActionChat_Conversion_FromSent.ConversionId = convarsionId;
                        objClearActionChat_Conversion_FromSent.SentByUserId = FromUserProfileId;

                        objClearActionChat_Conversion_FromSent.ReceiveByUserId = ToUserProfileId;

                        objClearActionChat_Conversion_FromSent.IsReadByReceiveByUser = false;


                        _dbContext.ClearActionChat_Conversion_FromSent.Add(objClearActionChat_Conversion_FromSent);
                        _dbContext.SaveChanges();
                        newSendToConversionId = objClearActionChat_Conversion_FromSent.Id;
                    }
                }
            }
            catch (Exception ex)
            { newSendToConversionId = 0; }
            return newSendToConversionId;
        }

        public List<ClearActionChat_Conversion_Model> GetOldPersonalConversion(string FromUserProfileId, string TouserProfileId, string LastConversionId, ref long afterAppandLastconvSendToId, ref long afterRemainingCount)
        {
            List<ClearActionChat_Conversion_Model> objQuick_Chat_Conversion_ModelList = new List<ClearActionChat_Conversion_Model>();
            try
            {

                long longLastConversionId = 0;
                long.TryParse(LastConversionId, out longLastConversionId);
                if (longLastConversionId == 0)
                {
                    int UnreadCount = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => s.ReceiveByUserId == FromUserProfileId && s.SentByUserId == TouserProfileId && s.IsReadByReceiveByUser == false).Count();
                    int TotalCount = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => ((s.ReceiveByUserId == TouserProfileId && s.SentByUserId == FromUserProfileId) || (s.ReceiveByUserId == FromUserProfileId && s.SentByUserId == TouserProfileId))).Count();
                    int takeConversionMessage = 0;
                    if (UnreadCount >= 30)
                    {
                        takeConversionMessage = UnreadCount + 5;
                    }
                    else
                    {
                        takeConversionMessage = 30;
                    }
                    objQuick_Chat_Conversion_ModelList = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => ((s.ReceiveByUserId == TouserProfileId && s.SentByUserId == FromUserProfileId) || (s.ReceiveByUserId == FromUserProfileId && s.SentByUserId == TouserProfileId))).Select(s => new ClearActionChat_Conversion_Model { ConversionId = s.ClearActionChat_Conversion.Id, ConversionSendToId = s.Id, ConversionText = s.ClearActionChat_Conversion.ConversionText, IsReadByReceiveByUserId = s.IsReadByReceiveByUser, ReceiveByUserId = s.ReceiveByUserId, SentByUserId = s.SentByUserId, SentDateTime = s.ClearActionChat_Conversion.SentDateTime, IsAttachment = s.ClearActionChat_Conversion.IsAttachment, OriginalFileName = s.ClearActionChat_Conversion.Attachment_OriginalName, StoreFileName = s.ClearActionChat_Conversion.Attachment_StoreName }).OrderByDescending(s => s.ConversionSendToId).Take(takeConversionMessage).ToList();
                    objQuick_Chat_Conversion_ModelList = objQuick_Chat_Conversion_ModelList.OrderBy(s => s.ConversionSendToId).ToList();
                    if (objQuick_Chat_Conversion_ModelList.Count > 0)
                    {
                        long afterappendLastConversionSendToId = objQuick_Chat_Conversion_ModelList.FirstOrDefault().ConversionSendToId;
                        long RemainingCount = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => ((s.ReceiveByUserId == TouserProfileId && s.SentByUserId == FromUserProfileId) || (s.ReceiveByUserId == FromUserProfileId && s.SentByUserId == TouserProfileId)) && s.Id < afterappendLastConversionSendToId).Count();
                        afterAppandLastconvSendToId = afterappendLastConversionSendToId;
                        afterRemainingCount = RemainingCount;
                    }
                }
                else
                {
                    int takeConversionMessageFirst = 30;
                    objQuick_Chat_Conversion_ModelList = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => s.Id < longLastConversionId && ((s.ReceiveByUserId == TouserProfileId && s.SentByUserId == FromUserProfileId) || (s.ReceiveByUserId == FromUserProfileId && s.SentByUserId == TouserProfileId))).Select(s => new ClearActionChat_Conversion_Model { ConversionId = s.ClearActionChat_Conversion.Id, ConversionSendToId = s.Id, ConversionText = s.ClearActionChat_Conversion.ConversionText, IsReadByReceiveByUserId = s.IsReadByReceiveByUser, ReceiveByUserId = s.ReceiveByUserId, SentByUserId = s.SentByUserId, SentDateTime = s.ClearActionChat_Conversion.SentDateTime, IsAttachment = s.ClearActionChat_Conversion.IsAttachment, OriginalFileName = s.ClearActionChat_Conversion.Attachment_OriginalName, StoreFileName = s.ClearActionChat_Conversion.Attachment_StoreName }).OrderByDescending(s => s.ConversionSendToId).Take(takeConversionMessageFirst).ToList();
                    objQuick_Chat_Conversion_ModelList = objQuick_Chat_Conversion_ModelList.OrderBy(s => s.ConversionSendToId).ToList();
                    if (objQuick_Chat_Conversion_ModelList.Count > 0)
                    {
                        long afterappendLastConversionSendToId = objQuick_Chat_Conversion_ModelList.FirstOrDefault().ConversionSendToId;
                        long RemainingCount = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => ((s.ReceiveByUserId == TouserProfileId && s.SentByUserId == FromUserProfileId) || (s.ReceiveByUserId == FromUserProfileId && s.SentByUserId == TouserProfileId)) && s.Id < afterappendLastConversionSendToId).Count();
                        afterAppandLastconvSendToId = afterappendLastConversionSendToId;
                        afterRemainingCount = RemainingCount;
                    }
                }
            }
            catch (Exception ex)
            {
                objQuick_Chat_Conversion_ModelList = new List<ClearActionChat_Conversion_Model>();
            }
            return objQuick_Chat_Conversion_ModelList;
        }

        public int GetUserUnreadyMessage(string messageSentByUserId, string messageReceiveByUserId)
        {
            int intUnreadCount = 0;
            try
            {
                intUnreadCount = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => s.IsReadByReceiveByUser == false && s.ReceiveByUserId == messageReceiveByUserId && s.SentByUserId == messageSentByUserId).Count();
            }
            catch
            {
                intUnreadCount = 0;
            }
            return intUnreadCount;
        }


        public List<UserUnreadMessageCountModel> GetAllUserUnreadMessageCount(int PortalId, long LoginUserId)
        {
            List<UserUnreadMessageCountModel> objUserUnreadMessageCountModelList = new List<UserUnreadMessageCountModel>();
            try
            {
                var UserList = UserController.GetUsers(PortalId);
                foreach (UserInfo item in UserList)
                {
                    if (LoginUserId != item.UserID)
                    {
                        int count = GetUserUnreadyMessage(item.UserID.ToString(), LoginUserId.ToString());
                        objUserUnreadMessageCountModelList.Add(new UserUnreadMessageCountModel
                        {
                            LoginUserId = LoginUserId,
                            SelectedUserId = item.UserID,
                            UnreadMessageCount = count
                        });
                    }
                }
            }
            catch
            {
                objUserUnreadMessageCountModelList = new List<UserUnreadMessageCountModel>();
            }
            return objUserUnreadMessageCountModelList;
        }

        public bool SetConversionToReadStatus(string conversionFromSentId)
        {
            bool ststus = false;
            try {
                long longconversionFromSentId = 0;
                long.TryParse(conversionFromSentId, out longconversionFromSentId);
                if (longconversionFromSentId != 0)
                {
                    ClearActionChat_Conversion_FromSent objClearActionChat_Conversion_FromSent = new ClearActionChat_Conversion_FromSent();
                    objClearActionChat_Conversion_FromSent = _dbContext.ClearActionChat_Conversion_FromSent.Where(s => s.Id == longconversionFromSentId).FirstOrDefault();
                    if (objClearActionChat_Conversion_FromSent != null)
                    {
                        objClearActionChat_Conversion_FromSent.IsReadByReceiveByUser = true;
                        _dbContext.SaveChanges();
                        ststus = true;
                    }
                }
            }
            catch
            {
                ststus = false;
            }
            return ststus;
        }
    }
}