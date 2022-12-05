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

            List <GetJob> get = JsonConvert.DeserializeObject<List<GetJob>(response);

            foreach (var item in GetJob){
                Console.WriteLine(item.title);
            }
        }
    }
    class GetJob{
        public int userId{get; set;}
        public string title {get; set;}
    }
}



