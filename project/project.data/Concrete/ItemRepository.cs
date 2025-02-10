using Microsoft.EntityFrameworkCore;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class ItemRepository : IItemRepository{
        private readonly AppDbContext _context;
        public ItemRepository(AppDbContext context){
            _context = context;
        }

        public DataResponse<List<Item>> GetAll(){
            var items = _context.Items
                .ToList();
                
            if(items == null){
                return new DataResponse<List<Item>>{
                    Data = items,
                    Message = "Items is null",
                    Success = false
                };
            }

            return new DataResponse<List<Item>>{
                Data = items,
                Message = "Items was successful",
                Success = true
            };
        }
    }
}