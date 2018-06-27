using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagment.Model;
using Hotelmanagment.DB;

namespace HotelManagment.API.Controllers {
    [Produces("application/json")]
    [Route("api/Operations")]
    public class OperationsController : Controller {
        private readonly DatabaseContext _context;

        public OperationsController(DatabaseContext context) {
            _context = context;
        }

        // GET: api/Operations
        [HttpGet]
        public IEnumerable<Operation> GetOperations() {
            return _context.Operations;
        }

        // GET: api/Operations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperation([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var operation = await _context.Operations.SingleOrDefaultAsync(m => m.Id == id);

            if (operation == null) {
                return NotFound();
            }

            return Ok(operation);
        }

        // PUT: api/Operations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperation([FromRoute] int id, [FromBody] Operation operation) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != operation.Id) {
                return BadRequest();
            }

            _context.Entry(operation).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!OperationExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Operations
        [HttpPost]
        public async Task<IActionResult> PostOperation([FromBody] Operation operation) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperation", new { id = operation.Id }, operation);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var operation = await _context.Operations.SingleOrDefaultAsync(m => m.Id == id);
            if (operation == null) {
                return NotFound();
            }

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();

            return Ok(operation);
        }

        private bool OperationExists(int id) {
            return _context.Operations.Any(e => e.Id == id);
        }
    }
}