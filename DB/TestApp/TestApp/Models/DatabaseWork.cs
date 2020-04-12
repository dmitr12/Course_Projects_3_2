using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace TestApp.Models
{
    public class DatabaseWork
    {
        string conString;
        public DatabaseWork(string connectionString)
        {
            conString = WebConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }

        public string CheckDb()
        {
            string result="";
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                result += con.ServiceName + "\n" + con.State + "\n" + con.ServerVersion;
            }
            return result;
        }

        public Genre GetGenre(int idGenre)
        {
            Genre genre;
            string strCommand = "select * from Genres where IdGenre=idGenre";
            using(OracleConnection con=new OracleConnection(strCommand))
            {
                con.Open();
                OracleCommand com = new OracleCommand(strCommand, con);
                OracleDataReader reader = com.ExecuteReader();
                genre = new Genre
                {
                    NameGenre = reader.GetString(1),
                    DescriptionGenre = reader.GetString(2)
                };
            }
            return genre;
        }

        public List<Film> SelectAllFilms()
        {
            List<Film> films = new List<Film>();
            string strCommand = "select * from Films";
            using (OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand(strCommand, con);
                OracleDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Film film = new Film
                    {
                        IdFilm = reader.GetInt32(0),
                        NameFilm = reader.GetString(1),
                        DescriptionFilm = reader.GetString(2),
                        Country = reader.GetString(3),
                        YearIssue = reader.GetInt32(4),
                        DurationMinutesFilm = reader.GetInt32(5),
                        Poster = reader.GetOracleBlob(6).Value
                    };
                    films.Add(film);
                }
                return films;
            }
        }

        public List<Genre> SelectAllGenre()
        {
            List<Genre> genres = new List<Genre>();
            string strCommand = "select * from Genres";
            using (OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand(strCommand, con);
                using (var reader=com.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Genre genre = new Genre()
                        {
                            IdGenre=reader.GetInt32(0),
                            NameGenre = reader["NameGenre"].ToString(),
                            DescriptionGenre = reader["DescriptionGenre"].ToString()
                        };
                        genres.Add(genre);
                    }
                }
            }
            return genres;
        }

        public void AddFilm(Film film/*, int[] selectedGenres*/)
        {
            string filmCommand = "insert into Films(NameFilm,DescriptionFilm,Country,YearIssue,DurationMinutesFilm,Poster) " +
                $"values('{film.NameFilm}', '{film.DescriptionFilm}', '{film.Country}', {film.YearIssue}, {film.DurationMinutesFilm}, :Poster)";

            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                using (OracleCommand cmd=new OracleCommand(filmCommand, con))
                {
                    cmd.Parameters.Add("Poster", OracleDbType.Blob, film.Poster, ParameterDirection.Input);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}