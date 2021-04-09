using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quotes.Application.Interfaces;
using Quotes.Application.ViewModels;
using Quotes.Domain.Entities;
using Quotes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private IQuoteService _quoteService;
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<Quote>>> GetAll()
        {
            return Ok(await _quoteService.GetAllQuotes());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Quote>> GetById(int id)
        {
            var quote = await _quoteService.GetQuoteById(id);
            if (quote!=null)
                return Ok(quote);
            return NotFound(new Response { Status = "error", Message = "Does not exist!" });
        }

        [HttpGet("GetByCategory/{titleCategory}")]
        public async Task<ActionResult<List<Quote>>> GetByCategory(string titleCategory)
        {
            return await _quoteService.GetQuotesByCategory(titleCategory);
        }

        
        [HttpGet("GetRandom")]
        public async Task<ActionResult<Quote>> GetRandom()
        {
            return await _quoteService.GetRandomQuote();
        }

        [HttpPost("Add")]
        [Authorize(Roles = "custom")]
        public ActionResult<Response> Add(QuoteModel quote)
        {
            return _quoteService.AddQuote(quote);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "custom")]
        public ActionResult<Response> Update(int id, QuoteModel quoteModel)
        {
            return _quoteService.UpdateQuote(id,quoteModel);

        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "custom")]
        public ActionResult<Response> Delete(int id)
        {
            return _quoteService.DeleteQuoteById(id);
        }

        [HttpDelete("clear")]
        public async Task DeleteByOffset()
        {
            foreach(var quote in await _quoteService.GetAllQuotes())
            {
                if(IsHourElapsed(quote))
                {
                    _quoteService.DeleteQuoteById(quote.Id);
                }
            }
        }

        private bool IsHourElapsed(Quote quote)
        {
            return DateTime.Now.Subtract(quote.CreatedAt).TotalMinutes > 60;
        }
    }
}
