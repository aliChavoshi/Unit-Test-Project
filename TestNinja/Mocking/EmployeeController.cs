using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Find(id);
            if (employee != null) _employeeRepository.Remove(id);
            _employeeRepository.SaveChanges();
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult
    {
    }

    public class RedirectResult : ActionResult
    {
    }

    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}