using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/book")]
    [ApiController]

    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var bookFromDb = await _db.Book.FindAsync(id);
            if(bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }
            else
            {
                _db.Book.Remove(bookFromDb);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Delete successfully!" });
            }
        }
    }
}