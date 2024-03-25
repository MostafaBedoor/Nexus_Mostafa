namespace Nexus_Mostafa.Server
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public bool active { get; set; }
        public DateOnly last_login { get; set; }
    }
}
