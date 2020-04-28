﻿using Oracle.ManagedDataAccess.Client;
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

        public string ConnectionString
        {
            get { return conString; }
            set
            {
                conString = value == "Admin" ?
                WebConfigurationManager.ConnectionStrings["AdminDbConnection"].ConnectionString
                : WebConfigurationManager.ConnectionStrings["UserDbConnection"].ConnectionString;
            }
        }

        public Genre GetGenre(int idGenre)
        {
            Genre genre=null;
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("system.GetGenre", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@resultGenre",OracleDbType.RefCursor, ParameterDirection.ReturnValue);
                com.Parameters.Add("@gnr", idGenre);
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
                reader.Close();
            }
            return genre;
        }

        public Cinema GetCinema(int idCinema)
        {
            Cinema cinema = null;
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand("system.GetCinema", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@resultCinema", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
            com.Parameters.Add("@cnm", idCinema);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {

                cinema = new Cinema
                {
                    IdCinema = reader.GetInt32(0),
                    NameCinema = reader.GetString(1),
                    AddressId=reader.GetInt32(2)
                };
                cinema.Address = new Address
                {
                    IdAddress = reader.GetInt32(3),
                    Street = reader.GetString(4),
                    NumberHouse = reader.GetInt32(5)
                };
            }
            con.Close();
            return cinema;
        }

        public User GetUser(string login)
        {
            User user = null;
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand("system.GetUser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@resultUser", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
            com.Parameters.Add("@nmUser", login);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                user = new User
                {
                    IdUser=reader.GetInt32(0),
                    Login=reader.GetString(1),
                    Password=reader.GetString(2),
                    RoleOfUserId=reader.GetInt32(3)
                };
            }
            con.Close();
            return user;
        }

        public RoleOfUser GetRoleForUser(int idUser)
        {
            RoleOfUser role = null;
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand("system.GetRoleForUser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@resultRole", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
            com.Parameters.Add("@userId", idUser);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                role = new RoleOfUser
                {
                    IdRole=reader.GetInt32(4),
                    NameRole=reader.GetString(5),
                    NameConnection=reader.GetString(6)
                };
            }
            con.Close();
            return role;
        }

        public List<Film> SelectAllFilms()
        {
            List<Film> films = new List<Film>();
            using (OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("system.GetAllFilms", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@allFilms", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
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
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("system.GetAllCinemas", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@allCinemas", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cinema cinema = new Cinema
                        {
                            IdCinema = reader.GetInt32(0),
                            NameCinema = reader.GetString(1),
                            AddressId = reader.GetInt32(2)
                        };
                        cinema.Address = new Address
                        {
                            IdAddress = reader.GetInt32(3),
                            Street = reader.GetString(4),
                            NumberHouse = reader.GetInt32(5)
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
            using (OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("system.GetAllGenres", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@allGenres", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
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
            List<Sector> sectors = new List<Sector>();
            Hall hall = null;
            int idHall = 0;
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("system.GetAllHallsCinema", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@allHallsCinema", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
                com.Parameters.Add("@cnm", idCinema);
                using (OracleDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int prev = idHall;
                        int hl = reader.GetInt32(0);
                        if (hall != null)
                            halls.Add(hall);
                        List<Hall> hls = halls.Where(h => h.IdHall == hl).ToList();
                        if (hls.Count == 0)
                        {
                            hall = new Hall
                            {
                                IdHall = reader.GetInt32(0),
                                NameHall = reader.GetString(1)
                            };                       
                        }
                        if (idHall == prev || idHall == hl)
                        {
                            hall.Sectors.Add(new Sector
                            {
                                IdSector=reader.GetInt32(3),
                                HallId=reader.GetInt32(4),
                                NameSector=reader.GetString(5),
                                StartRow=reader.GetInt32(6),
                                EndRow=reader.GetInt32(7),
                                CountSeatsRow=reader.GetInt32(8),
                                CostSeat=reader.GetInt32(9)
                            });
                        }
                        idHall = hl;
                    }
                    if(!halls.Contains(hall))
                        halls.Add(hall);
                }
            }
            return halls.Distinct().ToList();
        }

        public List<Hall> GetHallsByCinameName(string nameCinema)
        {
            List<Hall> halls = new List<Hall>();
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("system.GetHallsByCinameName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hallsCinema", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
                cmd.Parameters.Add("@cnmName", nameCinema);
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
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("system.GetSessionsByHallId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@resultSessions", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
                cmd.Parameters.Add("@hlId", hallId);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sessions.Add(new Session
                    {
                        IdSession = reader.GetInt32(0),
                        FilmId = reader.GetInt32(1),
                        Film = new Film
                        {
                            IdFilm = reader.GetInt32(1),
                            NameFilm = reader.GetString(5),
                            DurationMinutesFilm = reader.GetInt32(9)
                        },
                        HallId = reader.GetInt32(2),
                        StartSession = reader.GetDateTime(3)
                    });
                }
            }
            return sessions;
        }

        public Cinema GetCinemaByName(string nameCinema)
        {
            Cinema cinema = null;
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand com = new OracleCommand("system.GetCinemasByName", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@result", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
            com.Parameters.Add("@nmCnm", nameCinema);
            OracleDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {

                cinema = new Cinema
                {
                    IdCinema = reader.GetInt32(0),
                    NameCinema = reader.GetString(1),
                    AddressId = reader.GetInt32(2)
                };
                cinema.Address = new Address
                {
                    IdAddress = reader.GetInt32(3),
                    Street = reader.GetString(4),
                    NumberHouse = reader.GetInt32(5)
                };
            }
            con.Close();
            return cinema;
        }

        public void AddFilm(Film film)
        {  
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("system.AddFilm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@namefilm", film.NameFilm);
                cmd.Parameters.Add("@descriptionfilm", film.DescriptionFilm);
                cmd.Parameters.Add("@country", film.Country);
                cmd.Parameters.Add("@yearissue", film.YearIssue);
                cmd.Parameters.Add("@durationminutesfilm", film.DurationMinutesFilm);
                cmd.Parameters.Add("@poster",OracleDbType.Blob, film.Poster, ParameterDirection.Input);
                cmd.ExecuteNonQuery();
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

        public void EditCinema(Cinema cinema, Address address)
        {
            using (OracleConnection con = new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("SYSTEM.EditAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@idEditAddress", cinema.AddressId);
                com.Parameters.Add("@newStreet", address.Street);
                com.Parameters.Add("@newNumberHouse", address.NumberHouse);
                com.ExecuteNonQuery();
                com = new OracleCommand("SYSTEM.EditCinema", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@idEditCinema", cinema.IdCinema);
                com.Parameters.Add("@newName", cinema.NameCinema);
                com.ExecuteNonQuery();
            }
        }

        public void EditUserPassword(EditUserPassword eup)
        {
            using(OracleConnection con=new OracleConnection(conString))
            {
                con.Open();
                OracleCommand com = new OracleCommand("system.ChangeUserPassword", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@lg", eup.Login);
                com.Parameters.Add("@newPsw", eup.NewPassword);
                com.ExecuteNonQuery();
            }
        }
    }
}