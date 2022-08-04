namespace My2Blazor.Data
{
    public class ChampionInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AttackSpeed { get; set; }
        public string AttackRange { get; set; }
        public string ManaPoints { get; set; }
        public string HealthPoints { get; set; }
       
        public ChampionInfo(string name, string attackspeed, string attackrange, string manapoints, string healthpoints)
        {

            Name = name;
            AttackSpeed = attackspeed;
            AttackRange = attackrange;
            HealthPoints = healthpoints;
            ManaPoints = manapoints;
        }

    }
}
/*}
   protected override async Task OnInitializedAsync()
{
    ChampionService cService = new ChampionService();
    champions = await cService.GetChampionInfo();
}*/
