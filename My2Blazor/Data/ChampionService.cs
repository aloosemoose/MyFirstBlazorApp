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
        // public string[] championName = { "Aatrox", "Ahri", "Jinx", "" };
        // public string[] championName = new string[] { };
        public string fixString;
        public int championCounter = 0;
        public string newChamp;
        public int NoChampionsSoFar;
        public List<string> currentChampList = new List<string>();
        public List<string> aattackSpeeds = new List<string>();
        public List<string> aattackRanges = new List<string>();
        public bool ErrorMessage;

        public ChampionInfoData chData = new ChampionInfoData();

        public bool GetRequest()
        {
            try
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

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }


        public Task<ChampionInfo[]> GetChampionInfo()
        {
           // ProcessInput();


            return Task.FromResult(Enumerable.Range(0, NoChampionsSoFar).Select(index => new ChampionInfo(chData.championNames[index], chData.atkSpeeds[index], chData.atkRanges[index])
            {
                Name = chData.championNames[index],
                AttackSpeed = chData.atkSpeeds[index],
                AttackRange = chData.atkRanges[index],

            }).ToArray());



        }

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
                    string s = newChamp[0].ToString();
                    string s1 = newChamp[newChamp.Length - 1].ToString();

                    string ch = "'";
                    int freq = newChamp.Split(ch.ToCharArray()).Length - 1;

                    if (s == "'" || s1 == "'" || freq > 1)
                    {
                        ErrorMessage = true;
                  
                    }
                    else
                    {
                        newChamp = newChamp.Replace("'", string.Empty);

                        IsItValid();

                        if (IsItValid() == false)
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
        public bool IsItValid()
        {
            if (GetRequest())
            {
             
                return true;
            }
            else
            {
       
                return false;
            }
        }

        public void ValidateAndGo()
        {
            if (IsItValid() == false)
            {
                ErrorMessage = true;
            }
            else
            {
                ErrorMessage = false;
                chData.atkRanges = aattackRanges.ToArray();
                chData.atkSpeeds = aattackSpeeds.ToArray();
                chData.championNames = currentChampList.ToArray();
            }

        }
    }
}

    
