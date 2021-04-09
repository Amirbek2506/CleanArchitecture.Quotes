using Quotes.Application.ViewModels;
using Quotes.Domain.Entities;
using Quotes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Application.Interfaces
{
    public interface IQuoteService
    {
        Response AddQuote(QuoteModel quoteModel);
        Task<Quote> GetQuoteById(int id);
        Task<List<Quote>> GetAllQuotes();
        Task<List<Quote>> GetQuotesByCategory(string titleCategory);
        Task<Quote> GetRandomQuote();
        Response UpdateQuote(int id, QuoteModel quoteModel);
        Response DeleteQuoteById(int id);
    }
}
