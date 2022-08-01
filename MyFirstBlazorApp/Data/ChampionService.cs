using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstBlazorApp.Data
{
    public class ChampionService
    {
        public string BaseUrl = "http://ddragon.leagueoflegends.com";
        public string version = "12.13.1";
        public string[] championName = { "Aatrox", "Ahri", "Jinx", "" };
        public string fixString;
        public int championCounter = 0;
        public string newChamp;



        public ChampionInfoData chData = new ChampionInfoData();

        public void GetRequest()
        {

            championName[3] = newChamp;

            while (championCounter <= 3)
            {

                string url = "http://ddragon.leagueoflegends.com/cdn/" + version + "/data/en_US/champion/" + championName[championCounter] + ".json";

                var methodType = Method.Get;

                RestClient client = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(url, methodType);
                RestResponse response = client.ExecuteGet(request);

                var statusCode = response.StatusCode.ToString();
                string jsonResponse = response.Content;

                fixString = jsonResponse;
                var result = fixString.Replace(championName[championCounter], "Champion");

                ChampionObject.Root jsonIntoObject = JsonConvert.DeserializeObject<ChampionObject.Root>(result);


                chData.atkRanges[championCounter] = jsonIntoObject.data.champion.stats.attackrange.ToString();
                chData.atkSpeeds[championCounter] = jsonIntoObject.data.champion.stats.attackspeed.ToString();
                chData.championNames[championCounter] = championName[championCounter];

                championCounter++;
            }
        }


        public Task<ChampionInfo[]> GetChampionInfo()
        {
            GetRequest();

            return Task.FromResult(Enumerable.Range(0, 4).Select(index => new ChampionInfo(chData.championNames[index], chData.atkSpeeds[index], chData.atkRanges[index])
            {
                Name = chData.championNames[index],
                AttackSpeed = chData.atkSpeeds[index],
                AttackRange = chData.atkRanges[index],

            }).ToArray());



        }










    }
}

    
