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
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["VideostoreDBContext"].ToString();
            conn = new SqlConnection(conString);
        }

        /*  CREATE GENRE    */
        public bool Create(Genre genre)
        {
            string query = "INSERT INTO Genre (GenreName) VALUES (@GenreName);";
            query += " SELECT SCOPE_IDENTITY()";

            connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreName", genre.Name);

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
            List<Genre> genreList = new List<Genre>();


            string query = "SELECT * FROM Genre;";
            connection();

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

            foreach (DataRow dr in dt.Rows)
            {
                int genreId = int.Parse(dr["GenreId"].ToString());
                string genreName = dr["GenreName"].ToString();

                genreList.Add(new Genre() { GenreId = genreId, Name = genreName });
            }

            return genreList;
        }

        /*  GET GENRE BY ID   */
        public Genre GetById(int id)
        {
            Genre genre = null;

            string query = "SELECT * FROM Genre g WHERE g.GenreId = @GenreId;";
            connection();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreID", id);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.Fill(ds, "Genre");
                dt = ds.Tables["Genre"];
                conn.Close();
            }

            foreach (DataRow dr in dt.Rows)
            {
                int genreId = int.Parse(dr["GenreId"].ToString());
                string genreName = dr["GenreName"].ToString();

                genre = new Genre() { GenreId = genreId, Name = genreName };
            }

            return genre;
        }

        /*  EDIT GENRE    */
        public void Update(Genre genre)
        {
            throw new NotImplementedException();
        }

        /*  DELETE GENRE    */
        public void Delete(int id)
        {
            string query = "DELETE FROM Genre WHERE GenreId = @GenreId;";

            connection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@GenreId", id);

                conn.Open();
                cmd.ExecuteScalar();
                //cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}