
using Quotes.Domain.Entities;
using Quotes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Domain.Interfaces
{
    public interface IQuoteRepository
    {
        Response AddQuote(Quote quote);
        Task<Quote> GetQuoteById(int id);
        Task<List<Quote>> GetAllQuotes();
        Task<List<Quote>> GetQuotesByCategory(string titleCategory);
        Task<Quote> GetRandomQuote();
        Response UpdateQuote(Quote quote);
        Response DeleteQuoteById(int id);
        bool IsQuoteExists(int id);
    }
}
