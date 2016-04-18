using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;
using AutoLuminosityService.Data;
using AutoLuminosityService.Models;

namespace AutoLuminosityService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AutoLuminosityService : IAutoLuminosityService
    {
        public static string LifxApiKey = "c5d434f24191b225135118ba0f720f30ae29de44ce54bed49be7f04caf5dbe03";

        [WebInvoke(Method = "POST", UriTemplate = "RegisterUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool RegisterUser(Stream userStream)
        {
            try
            {
                StreamReader streamReader = new StreamReader(userStream);
                var streamStr = streamReader.ReadToEnd();
                var user = new JavaScriptSerializer().Deserialize<User>(streamStr);

                // Validate that the password meets the required length; this is my preference and you can totally change it
                if (user.Password.Length < 6) return false;

                // Register user account
                return AutoLuminosityDataManager.RegisterUser(user);
            }
            catch (Exception ex)
            {
                // Log error message
                //Email.ErrorLogEmail("RegisterUser: " + ex.Message);
            }

            return false;
        }

        [WebGet(UriTemplate = "IsEmailRegistered/{email}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool IsEmailRegistered(string email)
        {
            return AutoLuminosityDataManager.IsEmailRegistered(email);
        }

        [WebInvoke(Method = "POST", UriTemplate = "Login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public User Login(Stream userStream)
        {
            User user = null;
            try
            {
                StreamReader streamReader = new StreamReader(userStream);
                var streamStr = streamReader.ReadToEnd();
                user = new JavaScriptSerializer().Deserialize<User>(streamStr);

                user = AutoLuminosityDataManager.LoginUser(user);
            }
            catch (Exception ex)
            {
                //Email.ErrorLogEmail("RegisterUser: " + ex.Message);
            }

            return user;
        }

        [WebInvoke(Method = "POST", UriTemplate = "AddLight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Light AddLight(Stream lightStream)
        {
            var streamReader = new StreamReader(lightStream);
            var streamStr = streamReader.ReadToEnd();
            var light = new JavaScriptSerializer().Deserialize<Light>(streamStr);

            return AutoLuminosityDataManager.AddLight(light);
        }

        [WebInvoke(Method = "POST", UriTemplate = "AddRoom", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Room AddRoom(Stream roomStream)
        {
            var streamReader = new StreamReader(roomStream);
            var streamStr = streamReader.ReadToEnd();
            var room = new JavaScriptSerializer().Deserialize<Room>(streamStr);

            return AutoLuminosityDataManager.AddRoom(room);
        }

        [WebInvoke(Method = "POST", UriTemplate = "AddSchedule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Schedule AddSchedule(Stream scheduleStream)
        {
            var streamReader = new StreamReader(scheduleStream);
            var streamStr = streamReader.ReadToEnd();
            var schedule = new JavaScriptSerializer().Deserialize<Schedule>(streamStr);

            return AutoLuminosityDataManager.AddSchedule(schedule);
        }

        [WebGet(UriTemplate = "GetLights?userId={userId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Light> GetLights(int userId)
        {
            return AutoLuminosityDataManager.GetLights(userId);
        }

        [WebGet(UriTemplate = "GetRooms?userId={userId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Room> GetRooms(int userId)
        {
            return AutoLuminosityDataManager.GetRooms(userId);
        }

        [WebGet(UriTemplate = "GetLight?lightId={lightId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Light GetLight(int lightId)
        {
            return AutoLuminosityDataManager.GetLight(lightId);
        }

        [WebGet(UriTemplate = "GetRoom?roomId={roomId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Room GetRoom(int roomId)
        {
            return AutoLuminosityDataManager.GetRoom(roomId);
        }

        [WebGet(UriTemplate = "GetSchedule?scheduleId={scheduleId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Schedule GetSchedule(int scheduleId)
        {
            return AutoLuminosityDataManager.GetSchedule(scheduleId);
        }

        [WebGet(UriTemplate = "GetLightsForRoom?roomId={roomId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Light> GetLightsForRoom(int roomId)
        {
            return AutoLuminosityDataManager.GetLightsForRoom(roomId);
        }

        [WebGet(UriTemplate = "GetSchedulesForRoom?roomId={roomId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Schedule> GetSchedulesForRoom(int roomId)
        {
            return AutoLuminosityDataManager.GetSchedulesForRoom(roomId);
        }

        [WebGet(UriTemplate = "GetSchedulesForLight?lightId={lightId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Schedule> GetSchedulesForLight(int lightId)
        {
            return AutoLuminosityDataManager.GetSchedulesForLight(lightId);
        }

        [WebGet(UriTemplate = "GetSchedulesForUser?userId={userId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Schedule> GetSchedulesForUser(int userId)
        {
            return AutoLuminosityDataManager.GetSchedulesForUser(userId);
        }

        [WebGet(UriTemplate = "RemoveLight?lightId={lightId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool RemoveLight(int lightId)
        {
            return AutoLuminosityDataManager.RemoveLight(lightId);
        }

        [WebGet(UriTemplate = "RemoveRoom?roomId={roomId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool RemoveRoom(int roomId)
        {
            return AutoLuminosityDataManager.RemoveRoom(roomId);
        }

        [WebGet(UriTemplate = "RemoveSchedule?scheduleId={scheduleId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool RemoveSchedule(int scheduleId)
        {
            return AutoLuminosityDataManager.RemoveSchedule(scheduleId);
        }

        [WebInvoke(Method = "POST", UriTemplate = "UpdateLight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool UpdateLight(Stream lightStream)
        {
            var streamReader = new StreamReader(lightStream);
            var streamStr = streamReader.ReadToEnd();
            var light = new JavaScriptSerializer().Deserialize<Light>(streamStr);

            return AutoLuminosityDataManager.UpdateLight(light);
        }

        [WebInvoke(Method = "POST", UriTemplate = "UpdateRoom", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool UpdateRoom(Stream roomStream)
        {
            var streamReader = new StreamReader(roomStream);
            var streamStr = streamReader.ReadToEnd();
            var room = new JavaScriptSerializer().Deserialize<Room>(streamStr);

            return AutoLuminosityDataManager.UpdateRoom(room);
        }

        [WebInvoke(Method = "POST", UriTemplate = "UpdateSchedule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool UpdateSchedule(Stream scheduleStream)
        {
            var streamReader = new StreamReader(scheduleStream);
            var streamStr = streamReader.ReadToEnd();
            var schedule = new JavaScriptSerializer().Deserialize<Schedule>(streamStr);

            return AutoLuminosityDataManager.UpdateSchedule(schedule);
        }

        [WebGet(UriTemplate = "ExecuteSchedule?scheduleId={scheduleId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ExecuteSchedule(int scheduleId)
        {
            AutoLuminosityDataManager.ExecuteSchedule(scheduleId);
        }

        [WebGet(UriTemplate = "ToggleLight?lightId={lightId}&action={action}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ToggleLight(int lightId, int action)
        {
            AutoLuminosityDataManager.ToggleLight(lightId, action);
        }

        [WebGet(UriTemplate = "ToggleRoom?roomId={roomId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ToggleRoom(int roomId)
        {
            AutoLuminosityDataManager.ToggleRoom(roomId);
        }

        [WebGet(UriTemplate = "AddLightToRoom?roomId={roomId}&lightId={lightId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool AddLightToRoom(int roomId, int lightId)
        {
            return AutoLuminosityDataManager.AddLightToRoom(roomId, lightId);
        }
    }
}
