using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoLuminosityService.Connectors
{
    public class LifxConnector
    {
        public static string LifxApiKey = "c5d434f24191b225135118ba0f720f30ae29de44ce54bed49be7f04caf5dbe03";

        public async static void ToggleRoom(string roomRequest, string jsonRequest)
        {
            string url = string.Format("https://api.lifx.com/v1/lights/group_id:{0}/state", roomRequest);

            byte[] data = Encoding.UTF8.GetBytes(jsonRequest);

            var request = HttpWebRequest.Create(url);
            request.Method = "PUT";
            request.Headers.Add("Authorization", "Bearer " + LifxApiKey);
            request.ContentType = "application/json; charset=utf-8";
            using (Stream stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null))
            {
                stream.Write(data, 0, data.Length);
            }
            var response = await request.GetResponseAsync();
        }

        public async static void ToggleLight(string lightRequest, string jsonRequest)
        {
            string url = string.Format("https://api.lifx.com/v1/lights/id:{0}/state", lightRequest);

            byte[] data = Encoding.UTF8.GetBytes(jsonRequest);

            var request = HttpWebRequest.Create(url);
            request.Method = "PUT";
            request.Headers.Add("Authorization", "Bearer " + LifxApiKey);
            request.ContentType = "application/json; charset=utf-8";
            using (Stream stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null))
            {
                stream.Write(data, 0, data.Length);
            }
            var response = await request.GetResponseAsync();
        }
    }
}
