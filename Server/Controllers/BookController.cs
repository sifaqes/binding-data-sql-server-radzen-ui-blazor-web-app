using BindingData_SQL_EF.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BindingData_SQL_EF.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		
        public BookController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get ()
        {
			MatelasluxSystemDbContext db = new MatelasluxSystemDbContext();
            return db.Books.ToList();
        }

        [HttpPost]

        public async Task<ActionResult<Book>> Post ( Book value )
        {
            MatelasluxSystemDbContext db = new MatelasluxSystemDbContext();
            db.Books.Add(value);
            db.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put ( long id, Book updatedBook )
        {
            using (MatelasluxSystemDbContext db = new MatelasluxSystemDbContext())
            {
                var existingBook = await db.Books.FindAsync(id);

                if (existingBook == null)
                {
                    return NotFound(); // Return 404 Not Found if the book with the given id is not found
                }

                // Update the properties of the existing book with the values from the updated book
                existingBook.Name = updatedBook.Name;
                existingBook.Author = updatedBook.Author;
                existingBook.Price = updatedBook.Price;
                existingBook.Quantity = updatedBook.Quantity;
                // Update other properties as needed

                await db.SaveChangesAsync(); // Save changes to the database

                return Ok(existingBook); // Return the updated book
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete ( long id )
        {
            MatelasluxSystemDbContext db = new MatelasluxSystemDbContext();
            var book = db.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return NoContent();
        }
	}
}
