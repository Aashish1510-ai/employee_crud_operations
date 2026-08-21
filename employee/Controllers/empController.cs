using employee.data;
using employee.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace employee1.Controllers
{
    [Route("api/[controller]")] //base api
    [ApiController]
    public class empController : ControllerBase //inherits
    {
        private readonly AppDb _context;//to hold AppDb
                                        // readonly

        //Once _context is assigned in the constructor, you don't normally replace it with another AppDb.
        public empController(AppDb context) //constructor
        {
            _context = context;
        }

        // GET: api/emp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees() //do other work instead of wait , IEnumerabl->returns collection of employees
        {
            return await _context.Employees.ToListAsync();//Gets all employees from the database and puts them into a list.
        }
        //ActionResult
        //This represents an HTTP response.
        //_context -->>Your database context.
        // GET: api/emp/1

         [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);//Employees is table name

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // POST: api/emp
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.Id },
                employee
            );
        }

        // PUT: api/emp/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/emp/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}