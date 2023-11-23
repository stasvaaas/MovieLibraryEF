using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryEF
{
    internal class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsAdult { get; set; }
    }
}
