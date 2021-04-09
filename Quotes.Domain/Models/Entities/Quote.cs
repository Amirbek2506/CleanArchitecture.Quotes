using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Domain.Entities
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public string Author { get; set; }
        public string TextQuote { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
