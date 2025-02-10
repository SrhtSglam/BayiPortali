using project.entity;

namespace project.data.Abstract{
    public interface ICategoryRepository{
        public DataResponse<List<Category>> GetAll();
        public DataResponse<List<Category>> GetAllWithSubCategories();
    }
}