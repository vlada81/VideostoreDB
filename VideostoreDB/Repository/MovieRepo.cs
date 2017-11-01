using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideostoreDB.Repository.Interfaces;
using VideostoreDB.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using VideostoreDB.ViewModel;

namespace VideostoreDB.Repository.Interfaces
{
    public class MovieRepo : IMovieRepository
    {
        private SqlConnection conn;
        private IGenreRepository genreRepo = new GenreRepo();

        private void Connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["VideostoreDBContext"].ToString();
            conn = new SqlConnection(conString);
        }

        public bool Create(Movie movie)
        {
            string query = "INSERT INTO Movie (GenreId, MovieName, MoviePrice, MovieReleaseDate) VALUES (@GenreId, @MovieName, @MoviePrice," +
                                                " @MovieReleaseDate);";
            query += " SELECT SCOPE_IDENTITY();";

            Connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("GenreId", movie.Genre.GenreId);
                cmd.Parameters.AddWithValue("MovieName", movie.Name);
                cmd.Parameters.AddWithValue("MoviePrice", movie.Price);
                cmd.Parameters.AddWithValue("MovieReleaseDate", movie.ReleaseDate);
                

                conn.Open();
                var newFormedId = cmd.ExecuteScalar();
                conn.Close();

                if (newFormedId != null)
                {
                    return true;
                }
            }

            return false;
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Movie WHERE MovieId = @MovieId";

            Connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("MovieId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            string query = "SELECT * FROM Movie m, Genre g WHERE m.GenreId = g.GenreId";
            Connection();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds, "Movie");
                dt = ds.Tables["Movie"];

                conn.Close();
            }

            List<Movie> movieList = new List<Movie>();

            foreach (DataRow dataRow in dt.Rows)
            {
                int movieId = int.Parse(dataRow["MovieId"].ToString());
                string movieName = dataRow["MovieName"].ToString();
                decimal moviePrice = decimal.Parse(dataRow["MoviePrice"].ToString());
                int genreId = int.Parse(dataRow["GenreId"].ToString());
                string genreName = dataRow["GenreName"].ToString();
                int movieReleaseDate = int.Parse(dataRow["MovieReleaseDate"].ToString());

                Genre genre = new Genre()
                {
                    GenreId = genreId,
                    GenreName = genreName
                };

                movieList.Add(new Movie { MovieId = movieId, Genre = genre, Name = movieName, Price = moviePrice, ReleaseDate = movieReleaseDate });
            }

            return movieList;
        }

        public Movie GetById(int id)
        {
            string query = "SELECT * FROM Movie m, Genre g WHERE m.MovieId = @Id AND m.GenreId = g.GenreId";
            Connection();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("Id", id);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds, "Movie");
                dt = ds.Tables["Movie"];

                conn.Close();
            }

            Movie movie = null;

            foreach (DataRow dataRow in dt.Rows)
            {
                int movieId = int.Parse(dataRow["MovieId"].ToString());
                string movieName = dataRow["MovieName"].ToString();
                decimal moviePrice = decimal.Parse(dataRow["MoviePrice"].ToString());
                int genreId = int.Parse(dataRow["GenreId"].ToString());
                string genreName = dataRow["GenreName"].ToString();
                int movieReleaseDate = int.Parse(dataRow["MovieReleaseDate"].ToString());

                Genre genre = new Genre()
                {
                    GenreId = genreId,
                    GenreName = genreName
                };

                movie = new Movie { MovieId = movieId, Genre = genre, Name = movieName, Price = moviePrice, ReleaseDate = movieReleaseDate };

            }

            return movie;
        }

        public void Update(Movie movie)
        {
            string query = "UPDATE Movie SET MovieName = @Name, MoviePrice = @Price, GenreId = @GenreId, MovieReleaseDate = @ReleaseDate" +
                                " WHERE MovieId = @Id";
            Connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("Name", movie.Name);
                cmd.Parameters.AddWithValue("Price", movie.Price);
                cmd.Parameters.AddWithValue("GenreId", movie.Genre.GenreId);
                cmd.Parameters.AddWithValue("ReleaseDate", movie.ReleaseDate);
                cmd.Parameters.AddWithValue("Id", movie.MovieId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}