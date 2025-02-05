using Microsoft.EntityFrameworkCore;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class UserRepository : IUserRepository{
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context){
            _context = context;
        }

        public async Task<DataResponse<List<User>>> GetAll(){
            var users = await _context.Users
                .Where(i => i.Deleted == false && i.Visible == false)
                .ToListAsync();
            
            // if(users == null){
            //     return new DataResponse<List<User>>{
            //         Data = users,
            //         Message = "User Not Found",
            //         Success = false
            //     };
            // }

            var result = new DataResponse<List<User>>{
                Data = users,
                Message = "Success",
                Success = true
            };

            return result;
        }

        public async Task<DataResponse<User>> GetUserByName(string name){
            var user = await _context.Users.FirstOrDefaultAsync(i=>i.Name == name);
            
            var result = new DataResponse<User>{
                Data = user,
                Message = "Success",
                Success = true
            };

            return result;
        }
    }
}