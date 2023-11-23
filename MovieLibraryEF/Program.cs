namespace MovieLibraryEF
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //creating movies
            Movie movie1 = new Movie()
            {
                Title = "movie1",
                IsAdult = false
            };
            Movie movie2 = new Movie()
            {
                Title = "movie2",
                IsAdult = true
            };
            //setting conection to the db
            using AppDbConext myDb = new AppDbConext();

            //CRUD operations

            //Create
            myDb.Movies.Add(movie1);
            myDb.Movies.Add(movie2);
            myDb.SaveChanges();


            List<Movie> movies = myDb.Movies.ToList();
            
            
            //Read
            //creating a movie library
            

            //Update
            Update("1928c25d-e4a1-4dc8-8720-79495e8d405a", "newName");
            myDb.SaveChanges();

            //Delete
            //enter Id to delete
            //Delete("93a61abf-87bb-46ce-abbc-a51e0dc7cc0a");
            myDb.SaveChanges();


            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie.Title);
                Console.WriteLine(movie.Id);
            }

            Console.WriteLine("-----------------");
            //making sure movie library working properly
            MovieLibrary movieLibrary = new MovieLibrary();
            Console.WriteLine("movie library:");
            foreach (Movie movie in movieLibrary)
            {
                Console.WriteLine(movie.Title);
            }



            Console.WriteLine($"movie by the index is: {movieLibrary[0].Title}");
            string name = movieLibrary.GetMovie("220ac03b-39a8-4262-81e0-9a17cc1bc6d2");
            Console.WriteLine($"your movie is {name}");
        }
        public static void Update(string id, string newName)
        {
            using AppDbConext myDb = new AppDbConext();
            Guid guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                Movie m = myDb.Movies.FirstOrDefault(movie => movie.Id.Equals(guid));
                m.Title = newName;
                myDb.SaveChanges();
            }
        }

        public static void Delete(string id)
        {
            using AppDbConext myDb = new AppDbConext();
            Guid guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                Movie m = myDb.Movies.FirstOrDefault(movie => movie.Id.Equals(guid));
                myDb.Remove(m);
                myDb.SaveChanges();
            }
        }
    }
}