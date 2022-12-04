using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace digital_ocean{

    class Program{
        HttpClient client = new HttpClient();

        static async Task Main (string[]args){
        Program program = new Program();
        await program.GetTaskAsync();
    }
        private async Task GetTaskAsync(){
            string response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
            Console.WriteLine(response);

            GetJob get = JsonConvert.DeserializeObject<GetJob>(response);
        }
    }
    class GetJob{
        public int userId{get; set;}
        public string WebsiteCheckJob {get; set;}
    }
}



