using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
