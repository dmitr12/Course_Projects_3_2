using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                        Country = reader.GetString(2),
                        DateIssue = reader.GetDateTime(3),
                        Genre = reader.GetString(4),
                        DurationMinutesFilm = reader.GetInt32(5),
                        Poster = reader.GetOracleBlob(6).Value
                    };
                    films.Add(film);
                }
                return films;
            }
        }
    }
}