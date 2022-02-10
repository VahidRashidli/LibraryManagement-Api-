using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models;
using LibraryManagement.EmailHandler.Configuration;
using LibraryManagement.EmailHandler.Services.Abstraction;
using LibraryManagement.EmailHandler.Services.Implementation;
using LibraryManagement.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Abstarction;

namespace LibraryManagement.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IMailService _iMailService;
        public BooksController(IRepository<Book> repository, IMapper mapper,
            IMailService iMailService)
        {
            _repository = repository;
            _mapper = mapper;
            _iMailService = iMailService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute]int id)
        {
            Book book = await _repository.GetAsync(id);
            if (book == null) return NotFound();
            return Ok(_mapper.Map<BookDto>(await _repository.GetAsync(id)));
        }
        [HttpGet("GetPaginated")]
        public async Task<IActionResult> GetAllAsyncAsNoTracking(
            [FromQuery]int currentPage, [FromQuery] int itemsCount)
        {
            if (currentPage < 1 || itemsCount < 1) return BadRequest();
            PaginationDto<Book> dto =
                new(_repository.GetIQuerable(), currentPage, itemsCount);
            return Ok(dto);
        }
        [HttpGet]
        public async Task<IList<BookDto>> GetAllBooks()
        {
            return _mapper.Map<IList<BookDto>>(await _repository.GetAllAsync());
        }
        [HttpGet("specific")]
        public async Task<IList<BookDto>> GetSpecificBooks()
        {
            return _mapper.Map<IList<BookDto>>(await 
                _repository.GetAllFilteredAsync((x=>!x.IsDeleted),null));
        }
        [HttpPost("Addbook")]
        public async Task<IActionResult> AddBook([FromBody]BookDto bookDto)
        {
            bool isAdded=await _repository.AddAsync(_mapper.Map<Book>(bookDto));
            if (!isAdded) return BadRequest();
            await _iMailService.SendEmailAsync(new MailRequest {
            ToEmail="vrashidli1@gmail.com",
            Subject="Great",
            Body="Hey body how u doing?"
            });
            return Ok("Success!");
        }
        [HttpPost("Addbooks")]
        public async Task<IActionResult> AddBooks([FromBody] IList<BookDto>booksDtos)
        {
            bool areAdded = await _repository.AddRangeAsync(_mapper.Map<IList<Book>>(booksDtos));
            if (!areAdded) return BadRequest();
            return Ok("Success!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            Book book = await _repository.GetAsync(id);
            if(book == null) return NotFound();
            bool isDeleted=await _repository.DeleteAsync(id);
            if (!isDeleted) return BadRequest();
            return Ok("Success!");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>Update([FromRoute] int id,[FromBody]BookDto bookDto)
        {
            Book book = await _repository.GetAsync(id);
            if (book == null) return NotFound();
            book.Name = bookDto.Name;
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            bool isUpdated = await _repository.UpdateAsync(book);
            if (!isUpdated) return BadRequest();
            return Ok("Success!");
        }

    }
}
