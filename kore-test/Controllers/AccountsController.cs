using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kore_test.Models;

namespace kore_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly kore_testContext _context;

        public AccountsController(kore_testContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [Route("Get")]
        [HttpGet]
        public IEnumerable<Account> GetAccount()
        {
            return _context.Account;
        }

        [Route("Get/Duplicate")]
        [HttpGet]
        public IEnumerable<Account> GetAccountDetails()
        {
            var accounts = _context.Account;
            Dictionary<int, int> duplicate = new Dictionary<int, int>();

            foreach (var item in accounts)
            {
                if(duplicate.ContainsKey(item.ContactId))
                {
                    duplicate[item.ContactId]++;
                }
                else
                {
                    duplicate.Add(item.ContactId, 1);
                }
            }

            foreach( KeyValuePair<int, int> pair in duplicate)
            {
                if(pair.Value >= 2)
                {
                    foreach(var item in accounts)
                    {
                        if(item.ContactId == pair.Key)
                        {
                            item.ContactId = 0;
                        }
                    }
                }
            }

            return accounts;

        }

        // GET: api/Accounts/5
        [Route("Get/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [Route("/update/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount([FromRoute] long id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(account);
        }

        private bool AccountExists(long id)
        {
            return _context.Account.Any(e => e.Id == id);
        }
    }
}