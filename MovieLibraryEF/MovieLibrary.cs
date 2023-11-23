using MovieLibraryEF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryEF
{
    internal class MovieLibrary : IEnumerable
    {
        
        private List<Movie> _ordinaryMovies = new List<Movie>();
        private List<Movie> _adultMovies = new List<Movie>();
        

        //get movies from the DB and add movie to the corresonding list(adult/orinary)
        public MovieLibrary()
        {
            using AppDbConext myDb = new AppDbConext();
            List<Movie> movies = myDb.Movies.ToList();
            isAdultMovie(movies);
        }

        //if can parse string to guid and this guid == movie.Id - return this movie
        public string GetMovie(string id)
        {
            Guid guid;
            bool success = Guid.TryParse(id, out guid);
            if (IsNightTime())
            {
                if (success)
                {
                    Movie adultMovie = _adultMovies.FirstOrDefault(movie => movie.Id == guid);
                    if (adultMovie != null)
                    {
                        return adultMovie.Title;
                    }
                }
            }
            else
            {
                if (success)
                {
                    Movie ordinaryMovie = _ordinaryMovies.FirstOrDefault(movie => movie.Id == guid);
                    if (ordinaryMovie != null)
                    {
                        return ordinaryMovie.Title;
                    }
                }
            }

            return "Movie not found or parsing failed.";
        }

        private bool IsNightTime()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;

            return hour <= 7 || hour >= 23;
        }

        //accepting List as a parameter and movie to the corresonding list(adult/orinary)
        private void isAdultMovie(List<Movie> movies)
        {
            foreach(Movie movie in movies)
            {
                if (movie.IsAdult)
                {
                    _adultMovies.Add(movie);
                }
                else
                {
                    _ordinaryMovies.Add(movie);
                }
            }
            
        }

        public IEnumerator GetEnumerator()
        {
            if (IsNightTime())
            {
                List<Movie> values = _ordinaryMovies.Concat(_adultMovies).ToList();
                return new MovieEnum(values);
            }
            else
            {
                return new MovieEnum(_ordinaryMovies);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Movie this[int i]
        {
            get 
            { 
                if(IsNightTime())
                    { return _adultMovies[i]; }
                 else { return _ordinaryMovies[i]; }
            }
        }
    }

    internal class MovieEnum : IEnumerator
    {
        List<Movie> _movies = new List<Movie>();
        int _position = -1;

        public MovieEnum(List<Movie> list)
        {
            _movies = list;
        }
        public object Current
        {
            get
            {
                return _movies.ElementAt(_position);
            }
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _movies.Count);
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
