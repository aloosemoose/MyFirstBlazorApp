using System.Collections.Generic;

namespace My2Blazor.Data
{
    public class ChampionInfoData
    {
        public List<ChampionInfo> champions = new List<ChampionInfo>();
        public int i = 1;

        public string[] championNames = new string[] { }; 
        public  string[] atkSpeeds = new string[] { };     
        public  string[] atkRanges = new string[] { };
        public string[] manaPoints = new string[] { };
        public string[] healthPoints = new string[] {};
    }
}
