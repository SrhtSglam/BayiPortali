namespace project.entity{
    public class SMTP_Login_Info{
        public string? PrimaryKey { get; set; }
        public string SMTP_Server { get; set; }
        public int Authentication { get; set; }
        public string User_Id { get; set; }
        public string Password { get; set; }
        public int SMTP_Server_Port { get; set; }
        public byte Secure_Connection { get; set; }
        public string Sender_Name { get; set; }
        public string Sender_Address { get; set; }
    }
}