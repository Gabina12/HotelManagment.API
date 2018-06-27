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
    [Route("api/RoomAttributes")]
    public class RoomAttributesController : Controller {
        private readonly DatabaseContext _context;

        public RoomAttributesController(DatabaseContext context) {
            _context = context;
        }

        // GET: api/RoomAttributes
        [HttpGet]
        public IEnumerable<RoomAttribute> GetRoomAttributes() {
            return _context.RoomAttributes;
        }

        // GET: api/RoomAttributes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomAttribute([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var roomAttribute = await _context.RoomAttributes.SingleOrDefaultAsync(m => m.Id == id);

            if (roomAttribute == null) {
                return NotFound();
            }

            return Ok(roomAttribute);
        }

        // PUT: api/RoomAttributes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomAttribute([FromRoute] int id, [FromBody] RoomAttribute roomAttribute) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != roomAttribute.Id) {
                return BadRequest();
            }

            _context.Entry(roomAttribute).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!RoomAttributeExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoomAttributes
        [HttpPost]
        public async Task<IActionResult> PostRoomAttribute([FromBody] RoomAttribute roomAttribute) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.RoomAttributes.Add(roomAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomAttribute", new { id = roomAttribute.Id }, roomAttribute);
        }

        // DELETE: api/RoomAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAttribute([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var roomAttribute = await _context.RoomAttributes.SingleOrDefaultAsync(m => m.Id == id);
            if (roomAttribute == null) {
                return NotFound();
            }

            _context.RoomAttributes.Remove(roomAttribute);
            await _context.SaveChangesAsync();

            return Ok(roomAttribute);
        }

        private bool RoomAttributeExists(int id) {
            return _context.RoomAttributes.Any(e => e.Id == id);
        }
    }
}