using project.entity;

namespace project.data.Abstract{
    public interface ISMTPRepository{
        public SMTP_Login_Info GetSMTPConfiguration();
    }
}