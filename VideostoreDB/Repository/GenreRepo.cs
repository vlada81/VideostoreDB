using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideostoreDB.Repository.Interfaces;
using VideostoreDB.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace VideostoreDB.Repository
{
    public class GenreRepo : IGenreRepository
    {
        private SqlConnection conn;
        private void Connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["VideostoreDBContext"].ToString();
            conn = new SqlConnection(conString);
        }

        /*  CREATE GENRE    */
        public bool Create(Genre genre)
        {
            string query = "INSERT INTO Genre (GenreName) VALUES (@GenreName);";
            query += " SELECT SCOPE_IDENTITY()";

            Connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreName", genre.GenreName);

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

        /*  GET ALL GENRE    */
        public IEnumerable<Genre> GetAll()
        {
            string query = "SELECT * FROM Genre;";
            Connection();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds, "Genre");
                dt = ds.Tables["Genre"];
                conn.Close();
            }

            List<Genre> genreList = new List<Genre>();

            foreach (DataRow dr in dt.Rows)
            {
                int genreId = int.Parse(dr["GenreId"].ToString());
                string genreName = dr["GenreName"].ToString();

                genreList.Add(new Genre() { GenreId = genreId, GenreName = genreName });
            }

            return genreList;
        }

        /*  GET GENRE BY ID   */
        public Genre GetById(int id)
        {
            string query = "SELECT * FROM Genre g WHERE g.GenreId = @GenreId;";
            Connection();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("GenreId", id);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds, "Genre");
                dt = ds.Tables["Genre"];
                conn.Close();
            }

            Genre genre = null;

            foreach (DataRow dr in dt.Rows)
            {
                int genreId = int.Parse(dr["GenreId"].ToString());
                string genreName = dr["GenreName"].ToString();

                genre = new Genre() { GenreId = genreId, GenreName = genreName };
            }

            return genre;
        }

        /*  EDIT GENRE    */
        public void Update(Genre genre)
        {
            string query = "UPDATE Genre SET GenreName = @Name WHERE GenreId = @GenreId";

            Connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
          
                cmd.Parameters.AddWithValue("Name", genre.GenreName);
                cmd.Parameters.AddWithValue("GenreId", genre.GenreId);

                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();
            }
        }

        /*  DELETE GENRE    */
        public void Delete(int id)
        {
            string query = "DELETE FROM Genre WHERE GenreId = @GenreId;";

            Connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("GenreId", id);

                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();

            }
        }
    }
}