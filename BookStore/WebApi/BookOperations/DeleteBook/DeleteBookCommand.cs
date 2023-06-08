using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle(int id)
        {
            var book = _dbContext.Books.Where(book => book.Id == id).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Kitap Bulunamadı!");
            else
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }
    }
}
