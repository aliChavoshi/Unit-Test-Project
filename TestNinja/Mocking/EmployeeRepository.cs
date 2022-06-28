namespace TestNinja.Mocking
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);
        void Remove(Employee employee);
        void Remove(int id);
        void SaveChanges();
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _db;

        public EmployeeRepository()
        {
            _db = new EmployeeContext();
        }

        public Employee Find(int id)
        {
            return _db.Employees.Find(id);
        }

        public void Remove(Employee employee)
        {
            _db.Employees.Remove(employee);
        }

        public void Remove(int id)
        {
            var employee = Find(id);
            Remove(employee);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}