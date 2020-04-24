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

        public Genre GetGenre(int idGenre)
        {
            Genre genre=null;
            string strCommand = $"select * from system.Genres where IdGenre={idGenre}";
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

        public Cinema GetCinema(int idCinema)
        {
            Cinema cinema = null;
            string strCommand = $"select * from system.MovieTheatres where IdCinema={idCinema}";
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand(strCommand, con);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                cinema = new Cinema
                {
                    IdCinema = reader.GetInt32(0),
                    NameCinema = reader.GetString(1)
                };
            }
            con.Close();
            return cinema;
        }

        public User GetUser(string login)
        {
            User user = null;
            string strCommand = $"select * from system.Users where Login='{login}'";
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand(strCommand, con);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                user = new User
                {
                    IdUser=reader.GetInt32(0),
                    Login=reader.GetString(1),
                    Mail=reader.GetString(2),
                    Password=reader.GetString(3),
                    RoleOfUserId=reader.GetInt32(4)
                };
            }
            con.Close();
            return user;
        }

        public RoleOfUser GetRoleForUser(int idUser)
        {
            RoleOfUser role = null;
            string strCommand = $"select * from system.Users join system.RolesOfUsers on RoleOfUser=IdRole" +
                $" where IdUser={idUser}";
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand(strCommand, con);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                role = new RoleOfUser
                {
                    IdRole=reader.GetInt32(5),
                    NameRole=reader.GetString(6),
                    NameConnection=reader.GetString(7)
                };
            }
            con.Close();
            return role;
        }

        public List<Film> SelectAllFilms()
        {
            List<Film> films = new List<Film>();
            string strCommand = "select * from system.Films";
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
            string strCommand = "select * from system.MovieTheatres";
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
            string strCommand = "select * from system.Genres";
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

        public List<Hall> SelectAllHallsCinema(int idCinema)
        {
            List<Hall> halls = new List<Hall>();
            string strCommand = $"select * from system.Halls where Cinema={idCinema}";
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand(strCommand, con);
                using (OracleDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        halls.Add(new Hall
                        {
                            IdHall = reader.GetInt32(0),
                            NameHall = reader.GetString(1)
                        });
                    }
                }
            }
            return halls;
        }

        public void AddFilm(Film film/*, int[] selectedGenres*/)
        {
            string filmCommand = "insert into system.Films(NameFilm,DescriptionFilm,Country,YearIssue,DurationMinutesFilm,Poster) " +
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

        public void AddSession(Session session)
        {
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("system.AddSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@film", session.FilmId);
                cmd.Parameters.Add("@hall", session.HallId);
                cmd.Parameters.Add("@startsession", session.StartSession);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddUser(RegisterModel user)
        {
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("system.AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@login", user.Login);
                cmd.Parameters.Add("@mail", user.Mail);
                cmd.Parameters.Add("@password", user.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSector(object idHall, Sector sector)
        {
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("system.AddSector", con);
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
                OracleCommand cmd = new OracleCommand("system.AddHall", con);
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
                OracleCommand com = new OracleCommand("SYSTEM.AddCinemaAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@street", address.Street);
                com.Parameters.Add("@numberhouse", address.NumberHouse);
                com.Parameters.Add("@idAddr", OracleDbType.Int32).Direction=ParameterDirection.Output;
                com.ExecuteNonQuery();
                var idAddr = com.Parameters["@idAddr"].Value;
                com = new OracleCommand("SYSTEM.AddCinema", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@namecinema", cinema.NameCinema);
                com.Parameters.Add("@idAddr", idAddr);
                com.ExecuteNonQuery();
            }
        }

        public List<Hall> GetHallsByCinameName(string nameCinema)
        {
            List<Hall> halls = new List<Hall>();
            string strCom= $"select * from system.Halls where Cinema=(select IdCinema from system.MovieTheatres where NameCinema='{nameCinema}')";
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

        public List<Session> GetSessionsByHallId(int hallId)
        {
            List<Session> sessions = new List<Session>();
            string strCom = $"select * from system.Sessions join system.Films on Film=IdFilm where Hall={hallId} order by StartSession";
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(strCom, con);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sessions.Add(new Session
                    {
                        IdSession = reader.GetInt32(0),
                        FilmId=reader.GetInt32(1),
                        Film = new Film
                        {
                            IdFilm = reader.GetInt32(1),
                            NameFilm = reader.GetString(5),
                            DurationMinutesFilm = reader.GetInt32(9)
                        },
                        HallId =reader.GetInt32(2),
                        StartSession=reader.GetDateTime(3)
                    });
                }
            }
            return sessions;
        }
    }
}