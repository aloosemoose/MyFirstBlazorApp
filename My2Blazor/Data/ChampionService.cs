using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My2Blazor.Data
{
    public class ChampionService
    {
        public string BaseUrl = "http://ddragon.leagueoflegends.com";
        public string version = "12.13.1";
   
        public string fixString;

        public string newChamp;
        public string newChampOriginal;
        public string jsonChampName;

        public int NoChampionsSoFar;
        public bool ErrorMessage;

        public List<string> currentChampList = new List<string>();
        public List<string> aattackSpeeds = new List<string>();
        public List<string> aattackRanges = new List<string>();
        public List<string> mmanaPoints = new List<string>();
        public List<string> hhealthPoints = new List<string>();
           
        public ChampionInfoData chData = new ChampionInfoData();

        public void ProcessInput()
        {

            if (PeskyChampions() == false)
            {
                if (newChamp.Contains(" "))
                {
                    string s = newChamp[0].ToString();
                    string s1 = newChamp[newChamp.Length - 1].ToString();


                    char ch = ' ';
                    int freq = newChamp.Split(ch).Length - 1;

                    int countCaps = 0;
                    for (int i = 0; i < newChamp.Length; i++)
                    {
                        if (char.IsUpper(newChamp[i])) countCaps++;
                    }

                    if (s == " " || s1 == " " || (freq >= 1 && countCaps == 1) || (freq > 1 && countCaps >= 2))
                    {

                        ErrorMessage = true;
                    }
                    else
                    {
                        newChamp = newChamp.Replace(" ", string.Empty);
                        ValidateAndGo();
                    }

                }
                else if (newChamp.Contains("'"))
                {
                    string firstLetter = newChamp[0].ToString();
                    string lastLetter = newChamp[newChamp.Length - 1].ToString();

                    string charLookingFor = "'";

                    int freqOfFoundChar = newChamp.Split(charLookingFor.ToCharArray()).Length - 1;

                    if (firstLetter == "'" || lastLetter == "'" || freqOfFoundChar > 1)
                    {
                        ErrorMessage = true;
                    }
                    else
                    {
                        newChamp = newChamp.Replace("'", string.Empty);

                        if (IsItValidButDoNothing() == false)
                        {
                            newChamp = newChamp.ToLower();
                            newChamp = char.ToUpper(newChamp[0]) + newChamp.Substring(1);
                        }
                        ValidateAndGo();
                    }

                }
                else
                {
                    ValidateAndGo();
                }
            }
        }

        public bool PeskyChampions()
        {
            if (newChamp.Length == 0)
            {

                ErrorMessage = true;
                return true;
            }
            else if (newChamp == "Wukong")
            {
                newChamp = "MonkeyKing";
                ValidateAndGo();
                return true;
            }

            else if (newChamp == "Nunu & Willump" || newChamp == "Nunu")
            {

                newChamp = "Nunu";
                ValidateAndGo();
                return true;
            }
            else if (newChamp == "Renata Glasc")
            {
                newChamp = "Renata";
                ValidateAndGo();
                return true;
            }
            else
            {
                return false;
            }

        }

        public void ValidateAndGo()
        {
            if (GetRequest() == false)
            {
                ErrorMessage = true;
            }
            else
            {
                ErrorMessage = false;
                chData.atkRanges = aattackRanges.ToArray();
                chData.atkSpeeds = aattackSpeeds.ToArray();
                chData.championNames = currentChampList.ToArray();
                chData.manaPoints = mmanaPoints.ToArray();
                chData.healthPoints = hhealthPoints.ToArray();
            }

        }
        public bool IsItValidButDoNothing()
        {
            try
            {
                jsonChampName = newChamp;

                string url = "http://ddragon.leagueoflegends.com/cdn/" + version + "/data/en_US/champion/" + jsonChampName + ".json";

                var methodType = Method.Get;

                RestClient client = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(url, methodType);
                RestResponse response = client.ExecuteGet(request);


                string jsonResponse = response.Content;

                fixString = jsonResponse;
                var result = fixString.Replace(newChamp, "Champion");

                ChampionObject.Root jsonIntoObject = JsonConvert.DeserializeObject<ChampionObject.Root>(result);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public bool GetRequest()
        {
            try
            {
                jsonChampName = newChamp;
               
                string url = "http://ddragon.leagueoflegends.com/cdn/" + version + "/data/en_US/champion/" + jsonChampName + ".json";

                var methodType = Method.Get;

                RestClient client = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(url, methodType);
                RestResponse response = client.ExecuteGet(request);

                string jsonResponse = response.Content;
                fixString = jsonResponse;
                var result = fixString.Replace(newChamp, "Champion");

                ChampionObject.Root jsonIntoObject = JsonConvert.DeserializeObject<ChampionObject.Root>(result);

                aattackSpeeds.Add(jsonIntoObject.data.champion.stats.attackspeed.ToString());
                aattackRanges.Add(jsonIntoObject.data.champion.stats.attackrange.ToString());
                mmanaPoints.Add(jsonIntoObject.data.champion.stats.mp.ToString());
                hhealthPoints.Add(jsonIntoObject.data.champion.stats.hp.ToString());
                currentChampList.Add(newChampOriginal);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }


        public Task<ChampionInfo[]> GetChampionInfo()
        {
            return Task.FromResult(Enumerable.Range(0, NoChampionsSoFar).Select(index => new ChampionInfo(chData.championNames[index], chData.atkSpeeds[index], chData.atkRanges[index], chData.manaPoints[index], chData.healthPoints[index])
            {
                Name = chData.championNames[index],
                AttackSpeed = chData.atkSpeeds[index],
                AttackRange = chData.atkRanges[index],
                ManaPoints = chData.manaPoints[index],
                HealthPoints = chData.healthPoints[index]
            }).ToArray());
        }

      

       

       
 
      
    }
}

    
