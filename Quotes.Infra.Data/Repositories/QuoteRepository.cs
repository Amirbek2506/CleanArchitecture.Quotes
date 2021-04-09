using Microsoft.EntityFrameworkCore;
using Quotes.Domain.Entities;
using Quotes.Domain.Interfaces;
using Quotes.Domain.Models;
using Quotes.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Infra.Data.Repositories
{
    public class QuoteRepository:IQuoteRepository
    {
        public ApplicationDbContext _context;
        public QuoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Response AddQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            Save();
            return new Response {Status = "success", Message = "Added successfully!"};
        }

        public async Task<Quote> GetQuoteById(int id)
        {
            return await _context.Quotes.FindAsync(id);
        }

        public async Task<List<Quote>> GetAllQuotes()
        {
            return await _context.Quotes.ToListAsync();
        }

        public async Task<List<Quote>> GetQuotesByCategory(string titleCategory)
        {
            return await _context.Quotes.Where(p=>p.Category.ToLower()==titleCategory.ToLower()).ToListAsync();
        }

        public async Task<Quote> GetRandomQuote()
        {
            var quotes = await _context.Quotes.ToListAsync();
            return quotes.OrderBy(qu => Guid.NewGuid()).FirstOrDefault();
        }

        public Response DeleteQuoteById(int id)
        {
            _context.Quotes.Remove(_context.Quotes.Find(id));
            Save();
            return new Response { Status = "success", Message = "Successfully deleted!" };
        }

      

        public bool IsQuoteExists(int id)
        {
            return _context.Quotes.Find(id) != null;
        }

        public Response UpdateQuote(Quote quote)
        {
            var _quote = _context.Quotes.Find(quote.Id);
            _quote.Author = quote.Author;
            _quote.TextQuote = quote.TextQuote;
            _quote.Category = quote.Category;
            _quote.UpdatedAt = DateTime.Now;
            Save();
            return new Response { Status = "success", Message = "Successfully updated!" };
        }

        private int Save()
        {
            return _context.SaveChanges();
        }
    }
}
