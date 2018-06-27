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
    [Route("api/AttributeCodes")]
    public class AttributeCodesController : Controller {
        private readonly DatabaseContext _context;

        public AttributeCodesController(DatabaseContext context) {
            _context = context;
        }

        // GET: api/AttributeCodes
        [HttpGet]
        public IEnumerable<AttributeCode> GetAttributeCodes() {
            return _context.AttributeCodes;
        }

        // GET: api/AttributeCodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeCode([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var attributeCode = await _context.AttributeCodes.SingleOrDefaultAsync(m => m.Id == id);

            if (attributeCode == null) {
                return NotFound();
            }

            return Ok(attributeCode);
        }

        // PUT: api/AttributeCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeCode([FromRoute] int id, [FromBody] AttributeCode attributeCode) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != attributeCode.Id) {
                return BadRequest();
            }

            _context.Entry(attributeCode).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!AttributeCodeExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AttributeCodes
        [HttpPost]
        public async Task<IActionResult> PostAttributeCode([FromBody] AttributeCode attributeCode) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.AttributeCodes.Add(attributeCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttributeCode", new { id = attributeCode.Id }, attributeCode);
        }

        // DELETE: api/AttributeCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeCode([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var attributeCode = await _context.AttributeCodes.SingleOrDefaultAsync(m => m.Id == id);
            if (attributeCode == null) {
                return NotFound();
            }

            _context.AttributeCodes.Remove(attributeCode);
            await _context.SaveChangesAsync();

            return Ok(attributeCode);
        }

        private bool AttributeCodeExists(int id) {
            return _context.AttributeCodes.Any(e => e.Id == id);
        }
    }
}