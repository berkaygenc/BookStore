using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entity;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result=query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            
           

            BookDetailViewModel result;
            try
            {
                GetBookByIdQuery query = new GetBookByIdQuery(_context,_mapper);
                GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok(result);
        }


        //[HttpGet]
        //public Book GetByIdd([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newbook)
        {
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context,_mapper);
                command.Model = newbook;
                CreateBookCommandValidator validator=new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                //if(!result.IsValid)
                //    foreach (var item in result.Errors)
                //        Console.WriteLine("Özellik "+item.PropertyName + "- Error Message: "+item.ErrorMessage);
                //else
                //    command.Handle();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedbook)
        {
            try
            {

                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.Model = updatedbook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
            return Ok();
        }
    }
}
