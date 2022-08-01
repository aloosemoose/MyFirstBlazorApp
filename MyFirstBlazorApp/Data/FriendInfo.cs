namespace MyFirstBlazorApp.Data
{
    public class FriendInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public FriendInfo(int id, string firstname, string lastname, string location)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            Location = location;
        }
    }
}
