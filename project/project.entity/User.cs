using System.ComponentModel.DataAnnotations;

namespace project.entity{
    public class User{
        public int UserId { get; set; }
        [MaxLength(50)] [MinLength(16)]
        public string Name { get; set; }
        [MaxLength(50)] [MinLength(16)]
        public string Password { get; set; }
    }
}