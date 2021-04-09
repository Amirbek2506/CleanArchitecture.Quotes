using Quotes.Application.Interfaces;
using Quotes.Application.ViewModels;
using Quotes.Domain.Entities;
using Quotes.Domain.Interfaces;
using Quotes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Application.Services
{
    public class QuoteService : IQuoteService
    {
        public IQuoteRepository _quoteRepository;
        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }


        public Response AddQuote(QuoteModel quoteModel)
        {
            return _quoteRepository.AddQuote(
                new Quote
                {
                    Author = quoteModel.Author,
                    TextQuote = quoteModel.TextQuote,
                    Category = quoteModel.Category,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
        }

        public Response DeleteQuoteById(int id)
        {
            if (_quoteRepository.IsQuoteExists(id))
            {
                return _quoteRepository.DeleteQuoteById(id);
            }
            return new Response { Status = "error", Message = "Does not exist!" };
        }

        public async Task<Quote> GetQuoteById(int id)
        {
            return await _quoteRepository.GetQuoteById(id);
        }

        public async Task<List<Quote>> GetAllQuotes()
        {
            return await _quoteRepository.GetAllQuotes();
        }

        public async Task<List<Quote>> GetQuotesByCategory(string titleCategory)
        {
            return await _quoteRepository.GetQuotesByCategory(titleCategory);
        }

        public async Task<Quote> GetRandomQuote()
        {
            return await _quoteRepository.GetRandomQuote();
        }

        public Response UpdateQuote(int id, QuoteModel quoteModel)
        {
            if (_quoteRepository.IsQuoteExists(id))
            {
                return _quoteRepository.UpdateQuote(
                    new Quote
                    {
                        Id = id,
                        Author = quoteModel.Author,
                        Category = quoteModel.Category,
                        TextQuote = quoteModel.TextQuote
                    });
            }
            return new Response { Status = "error", Message = "Does not exist!" };
        }
    }
}
