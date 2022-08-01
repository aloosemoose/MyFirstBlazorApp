using System.Collections.Generic;

namespace MyFirstBlazorApp.Data
{
    public class ChampionInfoData
    {
        public List<ChampionInfo> champions = new List<ChampionInfo>();
        public int i = 1;

        public string[] championNames = new string[] { }; 
        public  string[] atkSpeeds = new string[] { };     
        public  string[] atkRanges = new string[] { };

    }
}
