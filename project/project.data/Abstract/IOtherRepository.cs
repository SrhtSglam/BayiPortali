using project.entity;

namespace project.data.Abstract{
    public interface IOtherRepository
    {
        public int GetCount(string table_name);
        public int GetCountPerPage(string table_name, int perPage);
    }
}