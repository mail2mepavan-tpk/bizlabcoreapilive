namespace bizlabcoreapi.Models
{
    public class staff_users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public string role { get; set; }
        public string location { get; set; }
        public bool active { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
