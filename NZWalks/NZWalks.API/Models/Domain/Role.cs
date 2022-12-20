namespace NZWalks.API.Models.Domain
{
    public class Role
    {
        public Guid id  { get; set; }
        public string Name { get; set; }
        //navigation prop
        public List<User_Role> userRole { get; set; }

    }
}
