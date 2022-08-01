using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstBlazorApp.Data
{
    public class ChampionService
    {
        public string BaseUrl = "http://ddragon.leagueoflegends.com";
        public string version = "12.13.1";
        // public string[] championName = { "Aatrox", "Ahri", "Jinx", "" };
       // public string[] championName = new string[] { };
        public string fixString;
        public int championCounter = 0;
        public string newChamp;
        public int NoChampionsSoFar;
        public List<string> currentChampList = new List<string>();
        public List<string> aattackSpeeds = new List<string>();
        public List<string> aattackRanges = new List<string>();

        public ChampionInfoData chData = new ChampionInfoData();

        public void GetRequest()
        {            

            while (championCounter <= NoChampionsSoFar - 1)
            {

                string url = "http://ddragon.leagueoflegends.com/cdn/" + version + "/data/en_US/champion/" + newChamp + ".json";

                var methodType = Method.Get;

                RestClient client = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(url, methodType);
                RestResponse response = client.ExecuteGet(request);

                var statusCode = response.StatusCode.ToString();
                string jsonResponse = response.Content;

                fixString = jsonResponse;
                var result = fixString.Replace(newChamp, "Champion");

                ChampionObject.Root jsonIntoObject = JsonConvert.DeserializeObject<ChampionObject.Root>(result);


                aattackSpeeds.Add(jsonIntoObject.data.champion.stats.attackspeed.ToString());
                aattackRanges.Add(jsonIntoObject.data.champion.stats.attackrange.ToString());
                currentChampList.Add(newChamp);

                championCounter++;
         
            }
            chData.atkRanges = aattackRanges.ToArray();
            chData.atkSpeeds = aattackSpeeds.ToArray();
            chData.championNames = currentChampList.ToArray();
        }


        public Task<ChampionInfo[]> GetChampionInfo()
        {                     
            GetRequest();

            return Task.FromResult(Enumerable.Range(0, NoChampionsSoFar).Select(index => new ChampionInfo(chData.championNames[index], chData.atkSpeeds[index], chData.atkRanges[index])
            {
                Name = chData.championNames[index],
                AttackSpeed = chData.atkSpeeds[index],
                AttackRange = chData.atkRanges[index],

            }).ToArray());



        }










    }
}

    
