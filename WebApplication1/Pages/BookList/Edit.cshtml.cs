using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Model;

namespace WebApplication1.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
            // Tìm 1 Book thích hợp với id nhận vào. Bên Edit.cshtml sẽ dùng Book này cho người dùng update
            // Trang Edit cần có thẻ hidden input cho attribute "Id" để nhận đủ data từ dòng trên 
            // Và khi gửi sẽ gửi đầy đủ thông tin, trường hợp thiếu thẻ Id thì hàm Task bên dưới sẽ bị lỗi
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Nhận tất cả data từ form của trang Edit
                /*var BookFromDb = await _db.Book.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author = Book.Author;
                BookFromDb.ISBN = Book.ISBN;*/

                _db.Book.Update(Book);

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

    }
}