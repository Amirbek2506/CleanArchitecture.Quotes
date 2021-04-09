using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Application.ViewModels
{
    public class QuoteModel
    {
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Author { get; set; }
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string TextQuote { get; set; }
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Category { get; set; }
    }
}
