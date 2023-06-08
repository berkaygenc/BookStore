using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entity;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public BookDetailViewModel Model { get; set; }
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(book => book.Id == id).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException("Kitap Bulunamadı!");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}
