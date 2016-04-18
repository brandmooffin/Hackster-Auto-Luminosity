using System;
using System.Collections.Generic;
using System.Linq;
using AutoLuminosityService.Connectors;
using AutoLuminosityService.Models;
using static System.Web.Security.FormsAuthentication;

namespace AutoLuminosityService.Data
{
    public class AutoLuminosityDataManager
    {
        public static User LoginUser(User user)
        {
            using (var dbContext = new AutoLuminosityDataDataContext())
            {
                var hashedPassword = HashPasswordForStoringInConfigFile(user.Password, "MD5");
                var userDb = dbContext.AutoLuminosity_Users.ToList()
                    .FirstOrDefault(
                        g =>
                            string.Equals(g.UserEmail.ToLower(), user.Email.ToLower()) &&
                            string.Equals(g.UserPassword, hashedPassword));
                if (userDb != null)
                {
                    user.Id = userDb.UserId;
                    user.CreateDate = userDb.UserCreateDate.ToString();
                }
            }
            return user;
        }

        public static bool RegisterUser(User user)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var userDb = new AutoLuminosity_User()
                    {
                        UserEmail = user.Email,
                        UserPassword = HashPasswordForStoringInConfigFile(user.Password, "MD5"),
                        UserCreateDate = DateTime.UtcNow
                    };

                    dbContext.AutoLuminosity_Users.InsertOnSubmit(userDb);
                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return false;
        }

        public static bool IsEmailRegistered(string email)
        {
            bool result = false;
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    result = dbContext.AutoLuminosity_Users.ToList().FirstOrDefault(g => string.Equals(g.UserEmail.ToLower(), email.ToLower())) != null;
                }
            }
            catch (Exception ex)
            {
                // Log error message
                //Email.ErrorLogEmail($"IsEmailRegistered {ex.Message} - {ex.StackTrace}");
            }
            return result;
        }

        public static Light AddLight(Light light)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var lightDb = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => string.Equals(i.LightExternalId, light.ExternalId));
                    if (lightDb == null)
                    {
                        // Create a new db Item object to store into the queue
                        lightDb = new AutoLuminosity_Light()
                        {
                            LightIsOn = light.IsOn,
                            UserId = light.UserId,
                            LightName = light.Name,
                            LightCreateDate = DateTime.UtcNow,
                            LightExternalId = light.ExternalId
                        };

                        // Insert our newly created Item and Submit the change to the db
                        dbContext.AutoLuminosity_Lights.InsertOnSubmit(lightDb);
                        dbContext.SubmitChanges();
                    }

                    light.Id = lightDb.LightId;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return light;
        }

        public static Room AddRoom(Room room)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var roomDb = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => string.Equals(i.RoomExternalId, room.ExternalId));
                    if (roomDb == null)
                    {
                        // Create a new db Item object to store into the queue
                        roomDb = new AutoLuminosity_Room()
                        {
                            RoomName = room.Name,
                            UserId = room.UserId,
                            RoomCreateDate = DateTime.UtcNow,
                            RoomExternalId = room.ExternalId
                        };

                        // Insert our newly created Item and Submit the change to the db
                        dbContext.AutoLuminosity_Rooms.InsertOnSubmit(roomDb);
                        dbContext.SubmitChanges();
                    }
                    room.Id = roomDb.RoomId;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return room;
        }

        public static Schedule AddSchedule(Schedule schedule)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    // Create a new db Item object to store into the queue
                    var itemDb = new AutoLuminosity_Schedule
                    {
                        ScheduleEntityId = schedule.EntityId,
                        ScheduleAction = schedule.Action,
                        ScheduleExecuteTime = schedule.ExecuteTime,
                        ScheduleType = schedule.Type
                    };

                    // Insert our newly created Item and Submit the change to the db
                    dbContext.AutoLuminosity_Schedules.InsertOnSubmit(itemDb);
                    dbContext.SubmitChanges();
                    schedule.Id = itemDb.ScheduleId;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return schedule;
        }

        public static bool UpdateLight(Light light)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    // Create a new db Item object to store into the queue
                    var lightDb = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => i.LightId == light.Id);

                    if (lightDb != null)
                    {
                        lightDb.LightName = light.Name;
                        lightDb.LightIsOn = light.IsOn;
                        lightDb.LightExternalId = light.ExternalId;
                    }

                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return false;
        }

        public static bool UpdateRoom(Room room)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    // Create a new db Item object to store into the queue
                    var roomDb = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => i.RoomId == room.Id);

                    if (roomDb != null)
                    {
                        roomDb.RoomName = room.Name;
                        roomDb.RoomExternalId = room.ExternalId;
                    }

                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return false;
        }

        public static bool UpdateSchedule(Schedule schedule)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    // Create a new db Item object to store into the queue
                    var scheduleDb = dbContext.AutoLuminosity_Schedules.FirstOrDefault(i => i.ScheduleId == schedule.Id);

                    if (scheduleDb != null)
                    {
                        scheduleDb.ScheduleAction = schedule.Action;
                        scheduleDb.ScheduleEntityId = schedule.EntityId;
                        scheduleDb.ScheduleExecuteTime = schedule.ExecuteTime;
                        scheduleDb.ScheduleType = schedule.Type;
                    }

                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return false;
        }

        public static Light GetLight(int lightId)
        {
            try
            {
                var light = new Light();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var lightDb = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => i.LightId == lightId);

                    if (lightDb != null)
                    {
                        light = new Light()
                        {
                            UserId = lightDb.UserId,
                            Name = lightDb.LightName,
                            Id = lightDb.LightId,
                            CreateDate = lightDb.LightCreateDate.ToString(),
                            IsOn = lightDb.LightIsOn,
                            ExternalId = lightDb.LightExternalId
                        };
                    }
                }
                return light;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static Room GetRoom(int roomId)
        {
            try
            {
                var room = new Room();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var roomDb = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => i.RoomId == roomId);

                    if (roomDb != null)
                    {
                        room = new Room
                        {
                            UserId = roomDb.UserId,
                            Name = roomDb.RoomName,
                            Id = roomDb.RoomId,
                            CreateDate = roomDb.RoomCreateDate.ToString(),
                            ExternalId = roomDb.RoomExternalId
                        };
                    }
                }
                return room;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static Schedule GetSchedule(int scheduleId)
        {
            try
            {
                var schedule = new Schedule();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var scheduleDb = dbContext.AutoLuminosity_Schedules.FirstOrDefault(i => i.ScheduleId == scheduleId);

                    if (scheduleDb != null)
                    {
                        schedule = new Schedule
                        {
                            Action = scheduleDb.ScheduleAction,
                            EntityId = scheduleDb.ScheduleEntityId,
                            Id = scheduleDb.ScheduleId,
                            Type = scheduleDb.ScheduleType,
                            ExecuteTime = scheduleDb.ScheduleExecuteTime
                        };
                    }
                }
                return schedule;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static List<Light> GetLights(int userId)
        {
            try
            {
                var lights = new List<Light>();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    dbContext.AutoLuminosity_Lights.Where(i => i.UserId == userId).ToList().ForEach(lightDb =>
                    {
                        if (lightDb != null)
                        {
                            lights.Add(new Light()
                            {
                                UserId = lightDb.UserId,
                                Name = lightDb.LightName,
                                Id = lightDb.LightId,
                                CreateDate = lightDb.LightCreateDate.ToString(),
                                IsOn = lightDb.LightIsOn,
                                ExternalId = lightDb.LightExternalId
                            });
                        }
                    });
                }
                return lights;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static List<Room> GetRooms(int userId)
        {
            try
            {
                var rooms = new List<Room>();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    dbContext.AutoLuminosity_Rooms.Where(i => i.UserId == userId).ToList().ForEach(roomDb =>
                    {
                        if (roomDb != null)
                        {
                            rooms.Add(new Room()
                            {
                                UserId = roomDb.UserId,
                                Name = roomDb.RoomName,
                                Id = roomDb.RoomId,
                                CreateDate = roomDb.RoomCreateDate.ToString(),
                                ExternalId = roomDb.RoomExternalId
                            });
                        }
                    });
                }
                return rooms;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static List<Light> GetLightsForRoom(int roomId)
        {
            try
            {
                var lights = new List<Light>();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    dbContext.AutoLuminosity_RoomLightLinks.Where(i => i.RoomId == roomId).ToList().ForEach(roomLightLink =>
                    {
                        if (roomLightLink == null) return;
                        var lightDb = dbContext.AutoLuminosity_Lights.FirstOrDefault(light => light.LightId == roomLightLink.LightId);
                        if (lightDb != null)
                        {
                            lights.Add(new Light()
                            {
                                UserId = lightDb.UserId,
                                Name = lightDb.LightName,
                                Id = lightDb.LightId,
                                CreateDate = lightDb.LightCreateDate.ToString(),
                                IsOn = lightDb.LightIsOn,
                                ExternalId = lightDb.LightExternalId
                            });
                        }
                    });
                }
                return lights;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static List<Schedule> GetSchedulesForRoom(int roomId)
        {
            try
            {
                var schedules = new List<Schedule>();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    dbContext.AutoLuminosity_Schedules.Where(i => i.ScheduleEntityId == roomId && i.ScheduleType == ScheduleType.Room).ToList().ForEach(schedule =>
                    {
                        if (schedule != null)
                        {
                            schedules.Add(new Schedule
                            {
                                Id = schedule.ScheduleId,
                                Action = schedule.ScheduleAction,
                                EntityId = schedule.ScheduleEntityId,
                                ExecuteTime = schedule.ScheduleExecuteTime,
                                Type = schedule.ScheduleType
                            });
                        }
                    });
                }
                return schedules;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static List<Schedule> GetSchedulesForLight(int lightId)
        {
            try
            {
                var schedules = new List<Schedule>();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    dbContext.AutoLuminosity_Schedules.Where(i => i.ScheduleEntityId == lightId && i.ScheduleType == ScheduleType.Light).ToList().ForEach(schedule =>
                    {
                        if (schedule != null)
                        {
                            schedules.Add(new Schedule
                            {
                                Id = schedule.ScheduleId,
                                Action = schedule.ScheduleAction,
                                EntityId = schedule.ScheduleEntityId,
                                ExecuteTime = schedule.ScheduleExecuteTime,
                                Type = schedule.ScheduleType
                            });
                        }
                    });
                }
                return schedules;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static List<Schedule> GetSchedulesForUser(int userId)
        {
            try
            {
                var schedules = new List<Schedule>();
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    dbContext.AutoLuminosity_Schedules.Where(i => i.UserId == userId).ToList().ForEach(schedule =>
                    {
                        if (schedule != null)
                        {
                            var entityName = string.Empty;
                            if (schedule.ScheduleType == ScheduleType.Light)
                            {
                                var light = dbContext.AutoLuminosity_Lights.FirstOrDefault(l => l.LightId == schedule.ScheduleEntityId);
                                entityName = light.LightName;
                            }
                            else
                            {
                                var room = dbContext.AutoLuminosity_Rooms.FirstOrDefault(l => l.RoomId == schedule.ScheduleEntityId);
                                entityName = room.RoomName;
                            }

                            schedules.Add(new Schedule
                            {
                                Id = schedule.ScheduleId,
                                Action = schedule.ScheduleAction,
                                EntityId = schedule.ScheduleEntityId,
                                ExecuteTime = schedule.ScheduleExecuteTime,
                                Type = schedule.ScheduleType,
                                EntityName = entityName
                            });
                        }
                    });
                }
                return schedules;
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return null;
        }

        public static bool RemoveLight(int lightId)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var lights = dbContext.AutoLuminosity_Lights.Where(i => i.LightId == lightId);
                    if (lights.Any())
                    {
                        dbContext.AutoLuminosity_Lights.DeleteAllOnSubmit(lights);
                        dbContext.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool RemoveRoom(int roomId)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var rooms = dbContext.AutoLuminosity_Rooms.Where(i => i.RoomId == roomId);
                    if (rooms.Any())
                    {
                        dbContext.AutoLuminosity_Rooms.DeleteAllOnSubmit(rooms);
                        dbContext.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool RemoveSchedule(int scheduleId)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var schedule = dbContext.AutoLuminosity_Schedules.Where(i => i.ScheduleId == scheduleId);
                    if (schedule.Any())
                    {
                        dbContext.AutoLuminosity_Schedules.DeleteAllOnSubmit(schedule);
                        dbContext.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static void ExecuteSchedule(int scheduleId)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var schedule = dbContext.AutoLuminosity_Schedules.FirstOrDefault(i => i.ScheduleId == scheduleId);
                    if (schedule != null)
                    {
                        string action = string.Empty;
                        if (schedule.ScheduleAction == 1)
                        {
                            action = "on";
                        }
                        else if (schedule.ScheduleAction == 2)
                        {
                            action = "off";
                        }
                        string jsonRequest = "{ \"power\": \"" + action + "\" }";

                        if (schedule.ScheduleType == ScheduleType.Light)
                        {
                            var light = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => i.LightId == schedule.ScheduleEntityId);
                            if (light != null)
                            {
                                LifxConnector.ToggleLight(light.LightExternalId, jsonRequest);
                            }
                        }
                        else if (schedule.ScheduleType == ScheduleType.Room)
                        {
                            var room = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => i.RoomId == schedule.ScheduleEntityId);
                            if (room != null)
                            {
                                LifxConnector.ToggleRoom(room.RoomExternalId, jsonRequest);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ToggleLight(int lightId, int action)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    string lightAction = string.Empty;
                    if (action == 1)
                    {
                        lightAction = "on";
                    }
                    else if (action == 2)
                    {
                        lightAction = "off";
                    }
                    string jsonRequest = "{ \"power\": \"" + lightAction + "\" }";

                    var light = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => i.LightId == lightId);
                    if (light != null)
                    {
                        LifxConnector.ToggleLight(light.LightExternalId, jsonRequest);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ToggleRoom(int roomId)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    string lightAction = "on";
                    bool turnLightsOn = true;
                    var roomLightLinks = dbContext.AutoLuminosity_RoomLightLinks.Where(i => i.RoomId == roomId);
                    roomLightLinks.ToList().ForEach(l =>
                    {
                        var light = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => l.LightId == i.LightId);
                        if (light.LightIsOn)
                        {
                            turnLightsOn = false;
                            lightAction = "off";
                        }
                    });

                    roomLightLinks.ToList().ForEach(l =>
                    {
                        var light = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => l.LightId == i.LightId);
                        light.LightIsOn = turnLightsOn;
                    });

                    dbContext.SubmitChanges();

                    string jsonRequest = "{ \"power\": \"" + lightAction + "\" }";

                    var room = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => i.RoomId == roomId);
                    if (room != null)
                    {
                        LifxConnector.ToggleRoom(room.RoomExternalId, jsonRequest);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ToggleRoom(int roomId, int action)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    string lightAction = string.Empty;
                    if (action == 1)
                    {
                        lightAction = "on";
                    }
                    else if (action == 2)
                    {
                        lightAction = "off";
                    }
                    string jsonRequest = "{ \"power\": \"" + lightAction + "\" }";

                    var room = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => i.RoomId == roomId);
                    if (room != null)
                    {
                        LifxConnector.ToggleRoom(room.RoomExternalId, jsonRequest);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static bool AddLightToRoom(int roomId, int lightId)
        {
            try
            {
                using (var dbContext = new AutoLuminosityDataDataContext())
                {
                    var roomDb = dbContext.AutoLuminosity_Rooms.FirstOrDefault(i => i.RoomId == roomId);
                    var lightDb = dbContext.AutoLuminosity_Lights.FirstOrDefault(i => i.LightId == lightId);

                    if (roomDb == null || lightDb == null) return false;

                    var rommLightLinkDb = dbContext.AutoLuminosity_RoomLightLinks.FirstOrDefault(i => i.RoomId == roomId && i.LightId == lightId);
                    if (rommLightLinkDb == null)
                    {
                        // Create a new db Item object to store into the queue
                        rommLightLinkDb = new AutoLuminosity_RoomLightLink()
                        {
                            RoomId = roomId,
                            LightId = lightId
                        };

                        // Insert our newly created Item and Submit the change to the db
                        dbContext.AutoLuminosity_RoomLightLinks.InsertOnSubmit(rommLightLinkDb);
                        dbContext.SubmitChanges();
                    }
                    return rommLightLinkDb.RoomLightLinkId > 0;
                }
            }
            catch (Exception ex)
            {
                // Log error message
            }
            return false;
        }
    }
}