using System.Collections.Generic;

namespace MyFirstBlazorApp.Data
{
    public class ChampionInfoData
    {
        public List<ChampionInfo> champions = new List<ChampionInfo>();
        public int i = 1;
        public  string[] championNames = new[] 
        {
            "Aatrox", "Ahri", "Jinx",""
        };
        public  string[] atkSpeeds = new[]
        {
           "","","",""
        };
        public  string[] atkRanges = new[]
        {
           "","","",""
        };

    }
}
