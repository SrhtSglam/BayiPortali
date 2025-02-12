// using Microsoft.EntityFrameworkCore;
// using project.data.Abstract;
// using project.entity;

// namespace project.data.Concrete{
//     public class CategoryRepository : ICategoryRepository{
//         private readonly AppDbContext _context;
//         public CategoryRepository(AppDbContext context){
//             _context = context;
//         }

//         public DataResponse<List<Category>> GetAll(){
//             var categories = _context.Categories
//                 .Where(i => i.Deleted == false && i.Visible == false)
//                 .ToList();
                
//             if(categories == null){
//                 return new DataResponse<List<Category>>{
//                     Data = categories,
//                     Message = "Categories is null",
//                     Success = false
//                 };
//             }

//             return new DataResponse<List<Category>>{
//                 Data = categories,
//                 Message = "Categories was successful",
//                 Success = true
//             };
//         }

//         public DataResponse<List<Category>> GetAllWithSubCategories(){
//             var categories = _context.Categories
//                 .Include(s => s.subCategories)
//                 .Where(i => i.Deleted == false && i.Visible == false)
//                 .ToList();
            
//             return new DataResponse<List<Category>>{
//                 Data = categories,
//                 Message = "Operation was successful",
//                 Success = true
//             };
//         }
//     }
// }