using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstBlazorApp.Data
{
    public class FriendInfoService

    {

        
        public Task<FriendInfo[]> GetFriendInfo()
        {
            FriendInfoData fiData = new FriendInfoData();
                    
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(0, 5).Select(index => new FriendInfo(index, fiData.firstNames[index], fiData.lastNames[index], fiData.location[index])
            {
                Id = index + 1,
                FirstName = fiData.firstNames[index],
                LastName = fiData.lastNames[index], 
                Location = fiData.location[index],
            }).ToArray());
        }

    }
}
