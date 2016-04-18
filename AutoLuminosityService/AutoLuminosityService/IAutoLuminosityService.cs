using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using AutoLuminosityService.Models;

namespace AutoLuminosityService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAutoLuminosityService" in both code and config file together.
    [ServiceContract]
    public interface IAutoLuminosityService
    {
        [OperationContract]
        bool RegisterUser(Stream userStream);

        [OperationContract]
        bool IsEmailRegistered(string email);

        [OperationContract]
        User Login(Stream userStream);

        [OperationContract]
        Light AddLight(Stream lightStream);

        [OperationContract]
        Room AddRoom(Stream roomStream);

        [OperationContract]
        Schedule AddSchedule(Stream scheduleStream);

        [OperationContract]
        List<Light> GetLights(int userId);

        [OperationContract]
        List<Room> GetRooms(int userId);

        [OperationContract]
        Light GetLight(int lightId);

        [OperationContract]
        Room GetRoom(int roomId);

        [OperationContract]
        Schedule GetSchedule(int scheduleId);

        [OperationContract]
        List<Light> GetLightsForRoom(int roomId);

        [OperationContract]
        List<Schedule> GetSchedulesForRoom(int roomId);

        [OperationContract]
        List<Schedule> GetSchedulesForLight(int lightId);

        [OperationContract]
        List<Schedule> GetSchedulesForUser(int userId);

        [OperationContract]
        bool RemoveLight(int lightId);

        [OperationContract]
        bool RemoveRoom(int roomId);

        [OperationContract]
        bool RemoveSchedule(int scheduleId);

        [OperationContract]
        bool UpdateLight(Stream lightStream);

        [OperationContract]
        bool UpdateRoom(Stream roomStream);

        [OperationContract]
        bool UpdateSchedule(Stream scheduleStream);

        [OperationContract]
        void ExecuteSchedule(int scheduleId);

        [OperationContract]
        void ToggleLight(int lightId, int action);

        [OperationContract]
        void ToggleRoom(int roomId);

        [OperationContract]
        bool AddLightToRoom(int roomId, int lightId);
    }
}
