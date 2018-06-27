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
    [Route("api/SaleTypes")]
    public class SaleTypesController : Controller {
        private readonly DatabaseContext _context;

        public SaleTypesController(DatabaseContext context) {
            _context = context;
        }

        // GET: api/SaleTypes
        [HttpGet]
        public IEnumerable<SaleType> GetSaleTypes() {
            return _context.SaleTypes;
        }

        // GET: api/SaleTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleType([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var saleType = await _context.SaleTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (saleType == null) {
                return NotFound();
            }

            return Ok(saleType);
        }

        // PUT: api/SaleTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleType([FromRoute] int id, [FromBody] SaleType saleType) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != saleType.Id) {
                return BadRequest();
            }

            _context.Entry(saleType).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!SaleTypeExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SaleTypes
        [HttpPost]
        public async Task<IActionResult> PostSaleType([FromBody] SaleType saleType) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.SaleTypes.Add(saleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleType", new { id = saleType.Id }, saleType);
        }

        // DELETE: api/SaleTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleType([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var saleType = await _context.SaleTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (saleType == null) {
                return NotFound();
            }

            _context.SaleTypes.Remove(saleType);
            await _context.SaveChangesAsync();

            return Ok(saleType);
        }

        private bool SaleTypeExists(int id) {
            return _context.SaleTypes.Any(e => e.Id == id);
        }
    }
}