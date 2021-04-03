using ClearAction_Chat.Models;
using ClearAction_Chat.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClearAction_Chat.Components
{
    //[HubName("ClearAction_Chat")]
    public class ChatHub : Hub
    {
        static ConcurrentDictionary<string, string> userDictionary = new ConcurrentDictionary<string, string>();
        ClearActionChatServices objClearActionChatServices = new ClearActionChatServices();

        public void Notify(string userId, string oldConnectionHubId, string id)
        {
            string oldContext = Context.ConnectionId.ToString();
            if (!string.IsNullOrEmpty(oldConnectionHubId))
            {
                var name = userDictionary.FirstOrDefault(x => x.Value == oldConnectionHubId);
                string s;
                if (name.Key != null)
                {
                    userDictionary.TryRemove(name.Key, out s);
                }
            }
            if (!string.IsNullOrEmpty(userId))
            {
                if (userDictionary.ContainsKey(userId.ToLower()))
                {
                    string s;
                    userDictionary.TryRemove(userId.ToLower(), out s);
                    userDictionary.TryAdd(userId, id);
                    foreach (KeyValuePair<String, String> entry in userDictionary)
                    {
                        Clients.Caller.UpdateOnlineOfflineSet(entry.Key);
                        Clients.All.UpdateOnlineOfflineSet(entry.Key);
                    }
                    Clients.All.UserOnline(userId);
                }
                else
                {
                    userDictionary.TryAdd(userId, id);
                    foreach (KeyValuePair<String, String> entry in userDictionary)
                    {
                        Clients.Caller.UpdateOnlineOfflineSet(entry.Key);
                        Clients.All.UpdateOnlineOfflineSet(entry.Key);
                    }
                    Clients.All.UserOnline(userId);
                }
            }

        }


        public void getAttachmentFile()
        {

        }



            public void GetAndSetUserUnreadyCount(string PortalId, string LoginUserId)
        {
            int intPortalId = 0;
            int.TryParse(PortalId, out intPortalId);

            long longLoginUserId = 0;
            long.TryParse(LoginUserId, out longLoginUserId);


            List<UserUnreadMessageCountModel> objUserUnreadMessageCountModelList = new List<UserUnreadMessageCountModel>();
            objUserUnreadMessageCountModelList = objClearActionChatServices.GetAllUserUnreadMessageCount(intPortalId, longLoginUserId);

            Clients.Caller.SetUserUnreadCount(objUserUnreadMessageCountModelList);
        }

        public void Send(string messageSentByUserId, string messageReceiveByUserId, string message)
        {
            string messageTimeText = DateTime.Now.ToString();
            long conversionId = objClearActionChatServices.SaveConversionMessageDetail(messageSentByUserId, messageReceiveByUserId, message, messageTimeText);

            long newSendToConversionId = objClearActionChatServices.SavePersonalConversionSendTo(messageSentByUserId, messageReceiveByUserId, message, conversionId);
            if (userDictionary.ContainsKey(messageReceiveByUserId.ToLower()))
            {
                if (Context != null)
                {
                    //Clients.Client(userDictionary[messageReceiveByUserId.ToLower()]).AppendMessage(messageSentByUserId, messageReceiveByUserId, message, messageTimeText);
                    Clients.Client(userDictionary[messageReceiveByUserId.ToLower()]).broadcastMessage(messageSentByUserId.ToLower(), "", messageReceiveByUserId.ToLower(), "", message, messageTimeText, newSendToConversionId, "");
                }
            }

            //Clients.Caller.AppendMessage(messageSentByUserId, messageReceiveByUserId, message, messageTimeText);
            Clients.Caller.broadcastMessage(messageSentByUserId.ToLower(), "", messageReceiveByUserId.ToLower(), "", message, messageTimeText, 0, "");


            //int intCount = objClearActionChatServices.GetUserUnreadyMessage(messageSentByUserId, messageReceiveByUserId);
            //Clients.Client(userDictionary[messageReceiveByUserId.ToLower()]).SetUserUnreadCount(messageSentByUserId, messageReceiveByUserId, intCount);
        }

        public void setConversionToRead(string ConversionSendToId, string SelectedUserid)
        {
            bool status = objClearActionChatServices.SetConversionToReadStatus(ConversionSendToId);
            Clients.Caller.responceConversionToRead(status, ConversionSendToId, SelectedUserid);
        }


        public void checkAndGetPersonalConversion(string FromUserProfileId, string TouserProfileId, string LastConversionId)
        {
            long afterAppandLastconvSendToId = 0;
            long afterRemainingCount = 0;
            List<ClearActionChat_Conversion_Model> objmma_personal_chat_conversionModelLIst = new List<ClearActionChat_Conversion_Model>();
            try
            {
                objmma_personal_chat_conversionModelLIst = objClearActionChatServices.GetOldPersonalConversion(FromUserProfileId, TouserProfileId, LastConversionId, ref afterAppandLastconvSendToId, ref afterRemainingCount);
            }
            catch
            {
                objmma_personal_chat_conversionModelLIst = new List<ClearActionChat_Conversion_Model>();
            }
            if (LastConversionId == "0")
            {
                Clients.Caller.AppandUserPersonalConversion(objmma_personal_chat_conversionModelLIst, afterAppandLastconvSendToId, afterRemainingCount);
            }
            else
            {
                objmma_personal_chat_conversionModelLIst = objmma_personal_chat_conversionModelLIst.OrderByDescending(s => s.ConversionSendToId).ToList();
                Clients.Caller.AppandLoadedUserPersonalConversion(objmma_personal_chat_conversionModelLIst, afterAppandLastconvSendToId, afterRemainingCount);
            }

            foreach (KeyValuePair<String, String> entry in userDictionary)
            {
                Clients.Caller.UpdateOnlineOfflineSet(entry.Key);
                Clients.All.UpdateOnlineOfflineSet(entry.Key);

            }



        }

        public virtual Task OnDisconnected(bool stopCalled)
        {
            var name = userDictionary.FirstOrDefault(x => x.Value == Context.ConnectionId.ToString());
            string s;
            if (name.Key != null)
            {
                userDictionary.TryRemove(name.Key, out s);
            }
            return Clients.All.disconnected(name.Key);
        }

    }
}