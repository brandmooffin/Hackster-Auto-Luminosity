using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AutoLuminosityIotApp.Models;
using Newtonsoft.Json.Linq;

namespace AutoLuminosityIotApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly AppSettings _appSettings = new AppSettings();
        private readonly DispatcherTimer _timer;

        private bool _isGettingMessages;

        public MainPage()
        {
            InitializeComponent();

            // Setup the timer task that will poll the web service for new messages and status
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(15000) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        
        private async void Timer_Tick(object sender, object e)
        {
            // We don't want to destroy the service we're calling, so if we are in the middle of a call, we should wait until that's done first
            if (!_isGettingMessages)
            {
                try
                {
                    var schedules = new List<Schedule>();

                    // get list of schedules for each light
                    var url = string.Format(App.ServiceBaseUrl + "GetSchedulesForUser?userId={0}", _appSettings.UserId);
                    var request = HttpWebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json; charset=utf-8";
                    var response = await request.GetResponseAsync();
                    var schedulesJsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    schedules.AddRange(JArray.Parse(schedulesJsonResponse).ToObject<List<Schedule>>());

                    var currentTime = DateTime.Now;
                    var currentHour = currentTime.Hour;
                    var currentMinute = currentTime.Minute;
                    // compare list of schedules to current time
                    foreach (var schedule in schedules)
                    {
                        var scheduleHour = Int32.Parse(schedule.ExecuteTime.Split(':')[0]);
                        var scheduleMinute = Int32.Parse(schedule.ExecuteTime.Split(':')[1]);
                        // execute any schedules that are up
                        if (scheduleHour == currentHour && scheduleMinute == currentMinute)
                        {
                            url = string.Format(App.ServiceBaseUrl + "ExecuteSchedule?scheduleId={0}", schedule.Id);
                            request = HttpWebRequest.Create(url);
                            request.Method = "GET";
                            request.ContentType = "application/json; charset=utf-8";
                            response = await request.GetResponseAsync();
                        }
                    }
                    
                    _isGettingMessages = false;

                }
                catch (Exception ex)
                {
                    // If anything happens, we will spit out the error on screen and flag that we are no longer grabbing messages
                    _isGettingMessages = false;
                }
            }
        }
    }
}