namespace My2Blazor.Data
{
    public class ChampionInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AttackSpeed { get; set; }
        public string AttackRange { get; set; }

        public ChampionInfo(string name, string attackspeed, string attackrange)
        {

            Name = name;
            AttackSpeed = attackspeed;
            AttackRange = attackrange;
        }

    }
}
/*}
   protected override async Task OnInitializedAsync()
{
    ChampionService cService = new ChampionService();
    champions = await cService.GetChampionInfo();
}*/
