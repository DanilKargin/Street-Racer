using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace StreetRacer {
    public static class HttpClient
    {
        public static async Task<T> Get<T>(string endpoint)
        {
            var getRequest = CreateRequest(endpoint);
            getRequest.SendWebRequest();

            while (!getRequest.isDone) await Task.Delay(10);
            return JsonConvert.DeserializeObject<T>(getRequest.downloadHandler.text);
        }

        public static async Task<T> Post<T>(string endpoint, object payload)
        {
            var postRequest = CreateRequest(endpoint, RequestType.POST, payload);
            postRequest.SendWebRequest();

            while (!postRequest.isDone) await Task.Delay(10);
            return JsonConvert.DeserializeObject<T>(postRequest.downloadHandler.text);
        }
		private static UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
        {
            var request = new UnityWebRequest(path, type.ToString());
            
            if (data != null)
            {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");


            return request;
        }

	}
    public enum RequestType
    {
        GET = 0,
        POST = 1
    }
}

