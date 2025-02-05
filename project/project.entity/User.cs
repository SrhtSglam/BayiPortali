namespace project.entity{
    public class User{
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
    }
}