using System.Collections.Generic;

namespace MyFirstBlazorApp.Data
{
    public class FriendInfoData
    {
        public List<FriendInfo> friends = new List<FriendInfo>();
        public int i = 1;
        public readonly string[] firstNames = new[]
        {
            "Kindson", "Oleander", "Saffron", "Jadon", "Solace",
        };
        public readonly string[] lastNames = new[]
        {
            "Munonye", "Yuba", "Lawrence", "Munonye", "Okeke"
        };
        public readonly string[] location = new[]
        {
            "Budapest", "Nigeria", "Lagos", "Asaba", "Oko"
        };
    }
}
