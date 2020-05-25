using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADEBT.Api.Data;
using ADEBT.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ADEBT.Api.Filters;
using AutoMapper;
using ADEBT.Api.Models;

namespace ADEBT.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiValidationFilter]
    public class DebtsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DebtsController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/Debts
        [HttpGet]

        public async Task<ActionResult<IEnumerable<DebtDto>>> GetDebts()
        {
            User user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest();
            }

            return await _mapper.ProjectTo<DebtDto>(_context.Entry(user).Collection(s => s.Debts).Query()).ToListAsync();           
        }

        // GET: api/Debts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DebtDto>> GetDebt(int id)
        {
            User user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest();
            }
            Debt debt = await _context.Debts.SingleAsync(s => s.Id == id && s.User == user);
            
            if (debt == null)
            {
                return NotFound();
            }

            return _mapper.Map<DebtDto>(debt);
        }

        // PUT: api/Debts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDebt(int id, Debt debt)
        {
            User user = await GetCurrentUserAsync();
            if (id != debt.Id || user == null || debt.User != user)
            {
                return BadRequest();
            }

            _context.Entry(debt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DebtExists(id))
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

        // POST: api/Debts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Debt>> PostDebt(Debt debt)
        {
            User user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest();
            }
            debt.User = user;
            _context.Debts.Add(debt);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetDebt", new { id = debt.Id }, debt);
        }

        // DELETE: api/Debts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DebtDto>> DeleteDebt(int id)
        {
            User user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest();
            }
            var debt = await _context.Debts.FindAsync(id);
            if (debt == null)
            {
                return NotFound();
            }
            if (debt.User != user)
            {
                return BadRequest();
            }

            _context.Debts.Remove(debt);
            await _context.SaveChangesAsync();

            return _mapper.Map<DebtDto>(debt);
        }

        private bool DebtExists(int id)
        {
            return _context.Debts.Any(e => e.Id == id);
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
