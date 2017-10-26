using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VideostoreDB.Models;
using VideostoreDB.ViewModel;
using VideostoreDB.Repository.Interfaces;
using System.Data;

namespace VideostoreDB.Repository
{
    public class MovieGenreVMRepo : IMovieGenreVMRepository
    {
        private SqlConnection conn;

        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["VideostoreDBContext"].ToString();
            conn = new SqlConnection(conString);
        }


        public bool Create(MovieGenreViewModel movieGenreVM)
        {
            string query = "INSERT INTO Movie (GenreId, MovieName, MoviePrice, MovieReleaseDate) VALUES (@GenreId, @MovieName, @MoviePrice," +
                                                " @MovieReleaseDate);";
            query += " SELECT SCOPE_IDENTITY();";

            connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreId", movieGenreVM.SelectedGenreId);
                cmd.Parameters.AddWithValue("@MovieName", movieGenreVM.Movie.Name);
                cmd.Parameters.AddWithValue("@MoviePrice", movieGenreVM.Movie.Price);
                cmd.Parameters.AddWithValue("@MovieReleaseDate", movieGenreVM.Movie.ReleaseDate);

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

        public IEnumerable<MovieGenreViewModel> GetAll()
        {
            //string queryMovie = "SELECT * FROM Movie m JOIN Genre g ON m.GenreId = g.GenreId;";
            //string queryGenre = "SELECT * FROM Genre;";

            string query = "SHAPE {SELECT * FROM Movie} APPEND ({SELECT GenreId, GenreName} AS Genre RELATE GenreId TO GenreId)";
            connection();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                //SqlDataAdapter dataAdapterM = new SqlDataAdapter("SELECT * FROM Movie m JOIN Genre g ON m.GenreId = g.GenreId;", conn);
                //SqlDataAdapter dataAdapterG = new SqlDataAdapter("SELECT * FROM Genre;", conn);

                //dataAdapterM.Fill(ds, "Movie");
                //dataAdapterG.Fill(ds, "Genre");

                //DataRelation relation = ds.Relations.Add("MovieGenre", ds.Tables["Movie"].Columns["GenreId"], ds.Tables["Genre"].Columns["GenreId"]);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                dataAdapter.Fill(ds, "Movie");
                dt = ds.Tables["Movie"];

                conn.Close();
            }

            List<Movie> movieList = new List<Movie>();
            List<MovieGenreViewModel> modelGenreVMList = new List<MovieGenreViewModel>();

            foreach (DataSet dataSet in ds.Tables)
            {
                int movieId = int.Parse(dataSet.Tables["MovieId"].ToString());
                int genreId = int.Parse(dataSet.Tables["GenreId"].ToString());
                //string genreName = dataSet.Tables["GenreName"].ToString();
                string movieName = dataSet.Tables["MovieName"].ToString();
                decimal moviePrice = decimal.Parse(dataSet.Tables["MoviePrice"].ToString());
                int movieReleaseDate = int.Parse(dataSet.Tables["MovieReleasDate"].ToString());

                movieList.Add(new Movie { MovieId = movieId, GenreId = genreId, Name = movieName, Price = moviePrice, ReleaseDate = movieReleaseDate});

            }

            throw new NotImplementedException();
        }

        public MovieGenreViewModel GetById(int id)
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

                genreList.Add(new Genre { GenreId = genreId, Name = genreName });
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
                        movieGenreViewModel = new MovieGenreViewModel { Movie = movie, Genres = genreList, SelectedGenreId = genreId };
                    }
                }
            }

            return movieGenreViewModel;
        }

        public void Update(MovieGenreViewModel movieGenreVM)
        {
            throw new NotImplementedException();
        }

        


    }
}