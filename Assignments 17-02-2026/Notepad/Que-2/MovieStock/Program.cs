using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieStockApp
{
    public class Program
    {
        public static List<Movie> MovieList = new List<Movie>();

        public static void Main(string[] args)
        {
            Program p = new Program();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string details = Console.ReadLine();
                p.AddMovie(details);
            }

            string searchGenre = Console.ReadLine();
            List<Movie> genreResults = p.ViewMoviesByGenre(searchGenre);

            foreach (var m in genreResults)
            {
                Console.WriteLine(m.Title + " " + m.Artist + " " + m.Ratings);
            }

            List<Movie> sortedList = p.ViewMoviesByRatings();
            foreach (var m in sortedList)
            {
                Console.WriteLine(m.Title + " " + m.Ratings);
            }
        }

        public void AddMovie(string MovieDetails)
        {
            string[] parts = MovieDetails.Split(',');
            Movie m = new Movie();
            m.Title = parts[0].Trim();
            m.Artist = parts[1].Trim();
            m.Genre = parts[2].Trim();
            m.Ratings = int.Parse(parts[3].Trim());
            
            MovieList.Add(m);
        }

        public List<Movie> ViewMoviesByGenre(string genre)
        {
            List<Movie> results = new List<Movie>();
            foreach (var movie in MovieList)
            {
                if (movie.Genre.ToLower() == genre.ToLower())
                {
                    results.Add(movie);
                }
            }

            if (results.Count == 0)
            {
                Console.WriteLine("No Movies found in genre '" + genre + "'");
            }
            return results;
        }

        public List<Movie> ViewMoviesByRatings()
        {
            return MovieList.OrderBy(m => m.Ratings).ToList();
        }
    }
}
