using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;

namespace Exam
{

    public class Country
    {
         HttpClient client = new HttpClient();
         List<State> state = null;
        

        public async Task<State> GetStateDetailsAsync(string path, string query)
        {
            //Avoid Http call every time.
            if (state == null)
            {
                //Make HTTP Call here 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var jobject = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    JArray result =(JArray)jobject.SelectToken("$.RestResponse.result");
                    state = result.ToObject<List<State>>();
                }
            }
            var output =
                       from c in state
                       where c.name == query || c.abbr == query
                       select c;
            if (output == null)
                return null;
            return output.FirstOrDefault();
        }

    }
}
