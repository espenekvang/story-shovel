using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoryShovel.VsTs
{
    internal class VsTsBoard
    {
        private readonly string _credetialsBase64String;
        private readonly Uri _baseAddress;
        private readonly string _project;

        public VsTsBoard(string accessToken, string baseAddress, string project)
        {
            _credetialsBase64String =
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", accessToken)));
            _baseAddress = new Uri(baseAddress);
            _project = project;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description">Supports html encoding</param>
        /// <returns></returns>
        public async Task AddUserStory(string title, string description)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credetialsBase64String);
                
                var pathUri = new Uri(_baseAddress, $"DefaultCollection/{_project}/_apis/wit/workitems/$User%20story?api-version=1.0");
                var method = new HttpMethod("PATCH");
                var payload = new List<VsTsWorkObject>
                {
                    new VsTsWorkObject("add", "/fields/System.Title", title),
                    new VsTsWorkObject("add", "/fields/System.Description", description)
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var request = new HttpRequestMessage(method, pathUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json-patch+json")
                };

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.Write(result);
                }
                else
                {
                    Console.Write($"Failed due to statuscode {response.StatusCode}.");
                }
            }
        }
    }
}