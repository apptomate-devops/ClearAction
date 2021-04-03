using LogMeIn.GoToCoreLib.Api;
using LogMeIn.GoToWebinar.Api;
using LogMeIn.GoToWebinar.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using DotNetNuke.Web.Api;
using ClearAction.Modules.WebCast.Components;
using ClearAction.Modules.WebCast.Components.Entity;
using System.Configuration;
using JWT.Algorithms;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace ZoomMeetingAPISchedular
{
    public class ZoomMeetingAPISchedular
    {
        public ZoomMeetingAPISchedular()
        {
        }

        static string GenerateJSONWebToken(string secret)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("T7F2hWwJXCfAx5iq5uCumdBYaUQwh8RxCF1g"));
            string appkey = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ZoomAppKey"]) ? ConfigurationManager.AppSettings["ZoomAppKey"] : "dttbFmbgRj6xKbhQu25VUA"; ;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken("dttbFmbgRj6xKbhQu25VUA", "dttbFmbgRj6xKbhQu25VUA", null,
            var token = new JwtSecurityToken(appkey, appkey, null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void ZoomAPICall()
        {
            try
            {
                string secret = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ZoomSecret"]) ? ConfigurationManager.AppSettings["ZoomSecret"] : "T7F2hWwJXCfAx5iq5uCumdBYaUQwh8RxCF1g";
                //const string secret = "T7F2hWwJXCfAx5iq5uCumdBYaUQwh8RxCF1g";

                var token = GenerateJSONWebToken(secret);

                var client = new RestClient("https://api.zoom.us/v2/users/3_iCXSKpTXqmUCAHcy2wPQ/meetings");
                var request = new RestRequest(Method.GET);
                //request.AddQueryParameter("type", "upcoming");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + token);
                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                JObject jObject = JObject.Parse(content);
                var meetings = jObject["meetings"];
                string meeting_url = (string)meetings[0]["start_url"];

            }
            catch (Exception ex)
            { }
        }
        public List<ClearAction.Modules.WebCast.ZoomMeeting> getZoomMeetingList(string UserEmail)
        {
            List<ClearAction.Modules.WebCast.ZoomMeeting> zList = new List<ClearAction.Modules.WebCast.ZoomMeeting>();

            try
            {
                //const string secret = "T7F2hWwJXCfAx5iq5uCumdBYaUQwh8RxCF1g";
                string secret = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ZoomSecret"]) ? ConfigurationManager.AppSettings["ZoomSecret"] : "T7F2hWwJXCfAx5iq5uCumdBYaUQwh8RxCF1g";
                var token = GenerateJSONWebToken(secret);
                string apiURL = "https://api.zoom.us/v2/users/" + UserEmail + "/meetings";
                //var client = new RestClient("https://api.zoom.us/v2/users/3_iCXSKpTXqmUCAHcy2wPQ/meetings");
                //var client = new RestClient("https://api.zoom.us/v2/users/vishalpatel21@gmail.com/meetings");
                var client = new RestClient(apiURL);
                var request = new RestRequest(Method.GET);
                //request.AddQueryParameter("type", "upcoming");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + token);
                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                JObject jObject = JObject.Parse(content);
                var meetings = jObject["meetings"];
                for (int i = 0; i < meetings.Count(); i++)
                {
                    ClearAction.Modules.WebCast.ZoomMeeting zMeeting = new ClearAction.Modules.WebCast.ZoomMeeting();
                    zMeeting.Id = (int)meetings[i]["id"];
                    zMeeting.status = (string)meetings[i]["status"];
                    zMeeting.option_start_type = (string)meetings[i]["option_start_type"];
                    zMeeting.option_audio = (string)meetings[i]["option_audio"];
                    zMeeting.start_time = (string)meetings[i]["start_time"];
                    zMeeting.type = (string)meetings[i]["type"];
                    zMeeting.duration = (string)meetings[i]["duration"];
                    zMeeting.timezone = (string)meetings[i]["timezone"];
                    zMeeting.start_url = (string)meetings[i]["start_url"];
                    zMeeting.join_url = (string)meetings[i]["join_url"];
                    zMeeting.created_at = (string)meetings[i]["created_at"];
                    zMeeting.topic = (string)meetings[i]["topic"];
                    zList.Add(zMeeting);
                }

            }
            catch (Exception ex)
            { }
            return zList;
        }

    }
}