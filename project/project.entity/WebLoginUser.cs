namespace project.entity{
    public class WebLoginUser
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public int WebVisibility { get; set; }
        public static string Company { get; set; }

        public static string AuthId {get; set;}
        public static string? CustomerNo {get; set;}
        public static int Visibility {get; set;}

        public static string? EMail {get; set;}
    }
}

