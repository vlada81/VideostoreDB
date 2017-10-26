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
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["VideostoreDBContext"].ToString();
            conn = new SqlConnection(conString);
        }

        public bool Create(Movie movie)
        {
            string query = "INSERT INTO Movie (GenreId, MovieName, MoviePrice, MovieReleaseDate) VALUES (@GenreId, @MovieName, @MoviePrice," +
                                                " @MovieReleaseDate);";
            query += " SELECT SCOPE_IDENTITY();";

            connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreId", movie.GenreId);
                cmd.Parameters.AddWithValue("@MovieName", movie.Name);
                cmd.Parameters.AddWithValue("@MoviePrice", movie.Price);
                cmd.Parameters.AddWithValue("@MovieReleaseDate", movie.ReleaseDate);

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
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            string queryMovie = "SELECT * FROM Movie m JOIN Genre g ON m.GenreId = g.GenreId;";
            string queryGenre = "SELECT * FROM Genre;";

            connection();

            DataSet dsM = new DataSet();
            DataTable dtM = new DataTable();

            DataSet dsG = new DataSet();
            DataTable dtG = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = queryMovie;

                SqlDataAdapter dataAdapterM = new SqlDataAdapter();
                dataAdapterM.SelectCommand = cmd;
                dataAdapterM.Fill(dsM, "Movie");
                dtM = dsM.Tables["Movie"];

                cmd.CommandText = queryGenre;

                SqlDataAdapter dataAdapterG = new SqlDataAdapter();
                dataAdapterG.SelectCommand = cmd;
                dataAdapterG.Fill(dsG, "Genre");
                dtG = dsG.Tables["Genre"];

                conn.Close();
            }

            List<Movie> movieList = new List<Movie>();
            List<Genre> genreList = new List<Genre>();

            foreach (DataRow dataRow in dtG.Rows)
            {
                int genreId = int.Parse(dataRow["GenreId"].ToString());
                string genreName = dataRow["GenreName"].ToString();

                genreList.Add(new Genre { GenreId = genreId, Name = genreName});
            }

            foreach (DataRow dataRow in dtM.Rows)
            {
                int movieId = int.Parse(dataRow["MovieId"].ToString());
                int genreId = int.Parse(dataRow["GenreId"].ToString());
                string movieName = dataRow["MovieName"].ToString();
                decimal moviePrice = decimal.Parse(dataRow["MoviePrice"].ToString());
                int movieReleaseDate = int.Parse(dataRow["MovieReleaseDate"].ToString());

                foreach (Genre g in genreList)
                {
                    if (genreId == g.GenreId)
                    {
                        movieList.Add(new Movie { MovieId = movieId, Genre = g, Name = movieName, Price = moviePrice, ReleaseDate = movieReleaseDate });

                    }
                }
            }
            
            return movieList;
        }

        public Movie GetById(int id)
        {
            string queryMovie = "SELECT * FROM Movie m JOIN Genre g ON m.GenreId = g.GenreId WHERE m.MovieId = @MovieId;";
            string queryGenre = "SELECT * FROM Genre;";

            connection();

            DataSet dsM = new DataSet();
            DataTable dtM = new DataTable();

            DataSet dsG = new DataSet();
            DataTable dtG = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = queryMovie;
                cmd.Parameters.AddWithValue("@MovieId", id);

                SqlDataAdapter dataAdapterM = new SqlDataAdapter();
                dataAdapterM.SelectCommand = cmd;
                dataAdapterM.Fill(dsM, "Movie");
                dtM = dsM.Tables["Movie"];

                cmd.CommandText = queryGenre;

                SqlDataAdapter dataAdapterG = new SqlDataAdapter();
                dataAdapterG.SelectCommand = cmd;
                dataAdapterG.Fill(dsG, "Genre");
                dtG = dsG.Tables["Genre"];

                conn.Close();
            }

            Movie movie = null;
            MovieGenreViewModel movieGenreViewModel = null;
            List<Genre> genreList = new List<Genre>();

            foreach (DataRow dataRow in dtG.Rows)
            {
                int genreId = int.Parse(dataRow["GenreId"].ToString());
                string genreName = dataRow["GenreName"].ToString();

                genreList.Add(new Genre { GenreId = genreId, Name = genreName});
            }

            foreach (DataRow dataRow in dtM.Rows)
            {
                int movieId = int.Parse(dataRow["MovieId"].ToString());
                int genreId = int.Parse(dataRow["GenreId"].ToString());
                string movieName = dataRow["MovieName"].ToString();
                decimal moviePrice = decimal.Parse(dataRow["MoviePrice"].ToString());
                int movieReleaseDate = int.Parse(dataRow["MovieReleaseDate"].ToString());

                foreach (Genre g in genreList)
                {
                    if (genreId == g.GenreId)
                    {
                        movie = new Movie { MovieId = movieId, Genre = g, Name = movieName, Price = moviePrice, ReleaseDate = movieReleaseDate };
                        movieGenreViewModel = new MovieGenreViewModel { Movie = movie, Genres = genreList, SelectedGenreId = genreId};
                    }
                }
            }
            
            return movie;
        }

        public void Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}