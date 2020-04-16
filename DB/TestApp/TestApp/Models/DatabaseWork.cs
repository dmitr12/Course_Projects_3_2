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
            Genre genre=null;
            string strCommand = $"select * from Genres where IdGenre={idGenre}";
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand(strCommand, con);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                genre = new Genre
                {
                    IdGenre = reader.GetInt32(0),
                    NameGenre = reader.GetString(1),
                    DescriptionGenre = reader.GetString(2)
                };
            }
            con.Close();
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

        public List<Cinema> SelectAllCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>();
            string strCommand = "select * from MovieTheatres";
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand(strCommand, con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cinema cinema = new Cinema()
                        {
                            IdCinema=reader.GetInt32(0),
                            NameCinema=reader.GetString(1)
                        };
                        cinemas.Add(cinema);
                    }
                }
            }
            return cinemas;
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


        public void AddSector(object idHall, Sector sector)
        {
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("AddSector", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hall", idHall);
                cmd.Parameters.Add("@namesector", sector.NameSector);
                cmd.Parameters.Add("@startrow", sector.StartRow);
                cmd.Parameters.Add("@endrow", sector.EndRow);
                cmd.Parameters.Add("@countseatsrow", sector.CountSeatsRow);
                cmd.Parameters.Add("@costseat", sector.CostSeat);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddHall(int idCinema, Hall hall)
        {
            object idHall=null;
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("AddHall", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@namehall", hall.NameHall);
                cmd.Parameters.Add("@cinema", idCinema);
                cmd.Parameters.Add("@idHall", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                idHall = cmd.Parameters["@idHall"].Value;
            }
            foreach (Sector sector in hall.Sectors)
                AddSector(idHall, sector);
        }

        public void AddCinema(Cinema cinema, Address address)
        {
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("AddCinemaAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@street", address.Street);
                com.Parameters.Add("@numberhouse", address.NumberHouse);
                com.Parameters.Add("@idAddr", OracleDbType.Int32).Direction=ParameterDirection.Output;
                com.ExecuteNonQuery();
                var idAddr = com.Parameters["@idAddr"].Value;
                com = new OracleCommand("AddCinema", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@namecinema", cinema.NameCinema);
                com.Parameters.Add("@idAddr", idAddr);
                com.ExecuteNonQuery();
            }
        }

        public List<Hall> GetHallsByCinameName(string nameCinema)
        {
            List<Hall> halls = new List<Hall>();
            string strCom= $"select * from Halls where Cinema=(select IdCinema from MovieTheatres where NameCinema='{nameCinema}')";
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(strCom, con);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    halls.Add(new Hall()
                    {
                        IdHall = reader.GetInt32(0),
                        NameHall = reader.GetString(1),
                        CinemaId = reader.GetInt32(2)
                    });
                }

            }
            return halls;
        }
    }
}