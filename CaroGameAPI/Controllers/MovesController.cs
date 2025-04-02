﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaroGameAPI.Data;

namespace CaroGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        private readonly CaroGameAPIContext _context;

        public MovesController(CaroGameAPIContext context)
        {
            _context = context;
        }

        // GET: api/Moves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moves>>> GetMoves()
        {
            return await _context.Moves.ToListAsync();
        }

        // GET: api/Moves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moves>> GetMoves(Guid id)
        {
            var moves = await _context.Moves.FindAsync(id);

            if (moves == null)
            {
                return NotFound();
            }

            return moves;
        }

        // PUT: api/Moves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoves(Guid id, Moves moves)
        {
            if (id != moves.ID)
            {
                return BadRequest();
            }

            _context.Entry(moves).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovesExists(id))
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

        // POST: api/Moves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Moves>> PostMoves(Moves moves)
        {
            _context.Moves.Add(moves);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoves", new { id = moves.ID }, moves);
        }

        // DELETE: api/Moves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoves(Guid id)
        {
            var moves = await _context.Moves.FindAsync(id);
            if (moves == null)
            {
                return NotFound();
            }

            _context.Moves.Remove(moves);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovesExists(Guid id)
        {
            return _context.Moves.Any(e => e.ID == id);
        }
    }
}
