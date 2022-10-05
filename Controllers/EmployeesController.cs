using Employees_API_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Employees_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _dbContext;

        public EmployeesController(EmployeeContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get:api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            return await _dbContext.Employees.ToListAsync();
        }

        //Get: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int EmployeeId)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            var Employee = await _dbContext.Employees.FindAsync(EmployeeId);
            if (Employee == null)
            {
                return NotFound();
            }
            return Employee;
        }

        //Get: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int EmployeeId)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            var Employee = await _dbContext.Employees.FindAsync(EmployeeId);
            if (Employee == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(Employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee Employee)
        {
            _dbContext.Employees.Add(Employee);
            await _dbContext.SaveChangesAsync(true);

            return CreatedAtAction(nameof(GetEmployee), new { id = Employee.EmployeeId }, Employee);
        }

        //POST: api/Employees/5
        [HttpPut]
        public async Task<ActionResult<Employee>> PutEmployee(int EmployeeId, Employee Employee)
        {
            if (EmployeeId != Employee.EmployeeId)
            {
                return BadRequest();
            }
            _dbContext.Entry(Employee).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(EmployeeId))
                {
                    return NotFound();

                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool EmployeeExists(long EmployeeId)
        {
            return (_dbContext.Employees?.Any(e => e.EmployeeId == EmployeeId)).GetValueOrDefault();
        }
    }
}
