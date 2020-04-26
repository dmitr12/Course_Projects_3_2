create table Genres(
IdGenre number generated always as identity primary key,
NameGenre varchar(100) unique not null,
DescriptionGenre clob not null
); 

create table Films(
IdFilm number generated always as identity primary key,
NameFilm varchar(300) not null,
DescriptionFilm clob not null,
Country varchar(70) not null,
YearIssue number not null,
DurationMinutesFilm int not null,
Poster blob not null
); 

create table FilmsGenres(
IdF number not null,
IdG number not null,
constraint Film_Id_FilmsGenres foreign key(IdF) references Films(IdFilm),
constraint Genre_Id_FilmsGenres foreign key(IdG) references Genres(IdGenre)
); 

create table CinemaAddresses(
IdAddress number generated always as identity primary key,
Street varchar(150) not null,
NumberHouse number not null
);

create table MovieTheatres(
IdCinema number generated always as identity primary key,
NameCinema varchar(100) not null,
Address number not null,
constraint Address_Id_MovieTheatres foreign key(Address) references CinemaAddresses(IdAddress)
); 

create table Halls(
IdHall number generated always as identity primary key,
NameHall varchar(150) not null,
Cinema number not null,
constraint Cinema_Id_Halls foreign key(Cinema) references MovieTheatres(IdCinema)
); 	

create table Sessions(
IdSession number generated always as identity primary key,
Film number not null,
Hall number not null,
StartSession date not null,
constraint Film_Id_Sessions foreign key(Film) references Films(IdFilm),
constraint Hall_Id_Sessions foreign key(Hall) references Halls(IdHall)
);

create table RolesOfUsers(
IdRole number primary key,
NameRole varchar(50) unique not null,
NameConnection varchar(50) unique not null,
constraint CheckNameRole_Roles check(NameRole in ('Admin','User')),
constraint CheckNameConnection_Roles check(NameConnection in ('C##Admin', 'C##User'))
);

create table Users(
IdUser number generated always as identity primary key,
Login varchar(100) unique not null,
Mail varchar(300) unique not null,
Password clob not null,
RoleOfUser number not null,
constraint RoleOfUser_Users foreign key(RoleOfUser) references RolesOfUsers(IdRole)
);

create table SectorsHall(
IdSector number generated always as identity primary key,
Hall number not null,
NameSector varchar(70) not null,
StartRow number not null,
EndRow number not null,
CountSeatsRow number not null,
CostSeat number not null,
constraint Hall_Id_SectorsHall foreign key(Hall) references Halls(IdHall)
);

create table Seats(
IdSeat number generated always as identity primary key,
NumberSeat number not null,
NumberRow number not null,
SectorHall number not null,
constraint SectorHall_Id_Seats foreign key(SectorHall) references SectorsHall(IdSector)
);

create table Tickets(
IdTicket number generated always as identity primary key,
Buyer number not null,
SessionId number not null,
SeatId number not null,
constraint Buyer_Id_Tickets foreign key(Buyer) references Users(IdUser),
constraint Session_Id_Tickets foreign key(SessionId) references Sessions(IdSession),
constraint Seat_Id_Tickets foreign key(SeatId) references Seats(IdSeat)
); 

--��������� � �������
create or replace procedure AddCinemaAddress(
street CinemaAddresses.Street%type,
numberhouse CinemaAddresses.NumberHouse%type, 
idAddr out number
)
as
begin
insert into CinemaAddresses(Street, NumberHouse) values(street, numberhouse)
returning IdAddress into idAddr;
commit;
end; 

create or replace procedure AddCinema(
namecinema MovieTheatres.NameCinema%type,
idAddr number
)
as
begin
insert into MovieTheatres(NameCinema, Address) values(namecinema, idAddr);
commit;
end;

create or replace procedure AddHall(
namehall Halls.NameHall%type,
cinema number,
idHall out number
)
as
begin
insert into Halls(NameHall, Cinema) values(namehall, cinema)
returning IdHall into idHall;
commit;
end; 

create or replace procedure AddSector(
hall number,
namesector SectorsHall.NameSector%type,
startrow number,
endrow number,
countseatsrow number,
costseat number
)
as
begin
insert into SectorsHall(Hall, NameSector , StartRow, EndRow, CountSeatsRow, CostSeat) 
values(hall, namesector, startrow, endrow, countseatsrow, costseat);
commit;
end;

create or replace procedure AddSession(
film number,
hall number,
startsession Sessions.StartSession%type
)
as
begin
insert into Sessions(Film, Hall, StartSession) values(film, hall, startsession);
commit;
end;

create or replace procedure AddUser(
login Users.Login%type,
mail Users.Mail%type,
password Users.Password%type
)
as
begin
insert into Users(Login, Mail, Password, RoleOfUser) values (login, mail, password, 2);
commit;
end;

create or replace procedure AddFilm(
namefilm Films.NameFilm%type,
descriptionfilm Films.DescriptionFilm%type,
country Films.Country%type,
yearissue Films.YearIssue%type,
durationminutesfilm Films.DurationMinutesFilm%type,
poster Films.Poster%type
)
as
begin
insert into system.Films(NameFilm,DescriptionFilm,Country,YearIssue,DurationMinutesFilm,Poster)
values(namefilm, descriptionfilm, country, yearissue, durationminutesfilm, poster);
commit;
end;

--------------

create or replace function GetGenre(gnr number)
return SYS_REFCURSOR
is
resultGenre SYS_REFCURSOR;
begin
open resultGenre for select * from Genres where IdGenre=gnr;
return resultGenre;
end GetGenre;

create or replace function GetCinema(cnm number)
return SYS_REFCURSOR
is
resultCinema SYS_REFCURSOR;
begin
open resultCinema for select * from MovieTheatres where IdCinema=cnm;
return resultCinema;
end GetCinema;

create or replace function GetUser(nmUser varchar)
return SYS_REFCURSOR
is
resultUser SYS_REFCURSOR;
begin
open resultUser for select * from Users where Login=nmUser;
return resultUser;
end;

create or replace function GetRoleForUser(userId number)
return SYS_REFCURSOR
is
resultRole SYS_REFCURSOR;
begin
open resultRole for select * from Users join RolesOfUsers on RoleOfUser=IdRole where IdUser=userId;
return resultRole;
end;

create or replace function GetAllFilms
return SYS_REFCURSOR
is
allFilms SYS_REFCURSOR;
begin
open allFilms for select * from Films;
return allFilms;
end;

create or replace function GetAllCinemas
return SYS_REFCURSOR
is
allCinemas SYS_REFCURSOR;
begin
open allCinemas for select * from MovieTheatres;
return allCinemas;
end;

create or replace function GetAllGenres
return SYS_REFCURSOR
is
allGenres SYS_REFCURSOR;
begin
open allGenres for select * from Genres;
return allGenres;
end;

create or replace function GetAllHallsCinema(cnm number)
return SYS_REFCURSOR
is
allHallsCinema SYS_REFCURSOR;
begin
open allHallsCinema for select * from Halls where Cinema=cnm;
return allHallsCinema;
end;

create or replace function GetHallsByCinameName(cnmName varchar)
return SYS_REFCURSOR
is
hallsCinema SYS_REFCURSOR;
begin
open hallsCinema for select * from Halls where Cinema=(select IdCinema from MovieTheatres where NameCinema=cnmName);
return hallsCinema;
end;

create or replace function GetSessionsByHallId(hlId number)
return SYS_REFCURSOR
is
resultSessions SYS_REFCURSOR;
begin
open resultSessions for select * from Sessions join Films on Film=IdFilm where Hall=hlId order by StartSession;
return resultSessions;
end;

select GetSessionsByHallId(4) from dual
--�������� SELECTS
select * from Sessions join Films on Film=IdFilm where Hall=4
--�������� �����
create role c##Role_Admin;

grant create session to c##Role_Admin;
grant all on Films to c##Role_Admin;
grant all on FilmsGenres to c##Role_Admin;
grant select on Genres to c##Role_Admin;
grant all on CinemaAddresses to c##Role_Admin;
grant all on MovieTheatres to c##Role_Admin;
grant all on Halls to c##Role_Admin;
grant all on Sessions to c##Role_Admin;
grant all on SectorsHall to c##Role_Admin;
grant all on Seats to c##Role_Admin;
grant all on Users to c##Role_Admin;
grant select on RolesOfUsers to c##Role_Admin;
grant select on Tickets to c##Role_Admin;
grant execute on AddCinema to c##Role_Admin;
grant execute on AddCinemaAddress to c##Role_Admin;
grant execute on AddHall to c##Role_Admin;
grant execute on AddSector to c##Role_Admin;
grant execute on AddSession to c##Role_Admin;
grant execute on GetGenre to c##Role_Admin;
grant execute on GetCinema to c##Role_Admin;
grant execute on GetAllCinemas to c##Role_Admin;
grant execute on GetAllFilms to c##Role_Admin;
grant execute on GetAllGenres to c##Role_Admin;
grant execute on GetAllHallsCinema to c##Role_Admin;
grant execute on GetHallsByCinameName to c##Role_Admin;
grant execute on GetRoleForUser to c##Role_Admin;
grant execute on GetSessionsByHallId to c##Role_Admin;
grant execute on GetUser to c##Role_Admin;
grant execute on AddFilm to c##Role_Admin;

create user c##Admin identified by admin;
grant c##Role_Admin to c##Admin;

create role C##Role_User;
grant create session to c##Role_User;
grant select on Films to c##Role_User;
grant select on FilmsGenres to c##Role_User;
grant select on Genres to c##Role_User;
grant select on CinemaAddresses to c##Role_User;
grant select on MovieTheatres to c##Role_User;
grant select on Halls to c##Role_User;
grant select on Sessions to c##Role_User;
grant select on RolesOfUsers to c##Role_User;
grant all on Users to c##Role_User;
grant select on SectorsHall to c##Role_User;
grant select on Seats to c##Role_User;
grant all on Tickets to c##Role_User;
grant execute on AddUser to c##Role_User;
grant execute on GetGenre to c##Role_User;
grant execute on GetCinema to c##Role_User;
grant execute on GetAllCinemas to c##Role_User;
grant execute on GetAllFilms to c##Role_User;
grant execute on GetAllGenres to c##Role_User;
grant execute on GetAllHallsCinema to c##Role_User;
grant execute on GetHallsByCinameName to c##Role_User;
grant execute on GetRoleForUser to c##Role_User;
grant execute on GetSessionsByHallId to c##Role_User;
grant execute on GetUser to c##Role_User;

create user C##User identified by user;
grant C##Role_User to C##User;
--������� ������
insert into Genres(NameGenre, DescriptionGenre) values ('��������������','�������������� ����� � ���� �������������, ������������ � ������ �����-���� ���������, ���������� ��������, ���������� ���� ���� � �������. ��������� �������������� ������ ������� �������� ���� �������� �������� �� ����� �������� �����, ������ �� �������� ����� ������������� � ������� ��� ��������, ����� ��������, ��� ������������ ��� �������� ��� �������� ��� ���������, ��������, ������, ������� ����������, ������, ������ ����� � �.�.');
insert into Genres(NameGenre, DescriptionGenre) values ('������','������ � ������������������� ����, � ������� ������� ����� ��� ����� ������������ � ����� �������, ������ �������, �� ��������� � �������, �� �������. ������ ������� ��������� �������� �� ���� ��� �����������, ��������� ��������, �������������� ������������� � �������� ������������� �������. ������� ����� ����� ����������� �, �������� ��, �����������, ���������� ������� ���������, ��������� ������ �� ������� �� ������� ��������� �� ���������������� ����������, ������������ � �������������. � ���������� ����������� �������� ����� �����������, � ������ �������� ��� ����������� �� ��������. ���� ������� ����� ������������� � ����� ������ ������, ������ ����� ����� ���������� � ���������������� �������� � ����������.');
insert into Genres(NameGenre, DescriptionGenre) values ('����������','�������������� ������ � ������������ �������� �������������, ����� ������� ������������ �� �������������� ����������� � ������� ������������, ������������ � ����������� ����. � ������� �������� ���������� ����� �������������� �� ��� ���� �������, ������� � ����������, ������� ������������ ����� ������������ ��� ������� ��� ���� ���������� � �������. ��������: ��������� ����� �����, ������������ ����, ��������������� �����������, ����������� �� �������, ����������� �����������, �������, ������������� ��������� � ���� ��������. ����� �������� ������� � ����������� �������������� ������� � ��������� ��� ���������� �������.');
insert into Genres(NameGenre, DescriptionGenre) values ('�������','������� � ������������� � ������������������� ����, � ���������� ���������. ����������� � ������������ ������ ��������� �������� ���������� ��� ������� �������, ���������������� (�������), ����������� � ���������. ������� �������� ����� ����� �������� ������ ������������ ����������� ��������� �������� �������, ������ ������ �������� ���� � ���� ������� ����� �������� �����, ���������� ������� ������������ � � ���� ���.������ ���������� ����������� ������� ����� ����� �������� �������� ������ ���������� �� �������, ������ �������, ����������� �������� �������� � �.�. ������������ (����� ������������� �� ����� ����������, ��������� �������). ������ ������ �������� ������ ������� � ���������� �� ���������� ����� ������.');
insert into Genres(NameGenre, DescriptionGenre) values ('�����','������������� ������ � ���� �� �������� ���������������� ������������������� ������. ��� �������, ��� ������ ���������� � ������� ����� � ���������� ���������� ����������, ���������� �������� �� ����������� � �� ��������� � ��������� ���������������� �������������. ����������� ������ ����� �������� ������������ � ���������� ���������� � ������� �����.');

insert into RolesOfUsers(IdRole, NameRole, NameConnection) values(1, 'Admin','C##Admin');
insert into RolesOfUsers(IdRole, NameRole, NameConnection) values(2, 'User','C##User');

insert into Users(Login, Mail, Password, RoleOfUser) values('Admin','dyrda.dmitrij@mail.ru','GaKFQUS2Oo92F6byJQGbEg==',1); --������: 'admin'
--���� ���������
create or replace directory IMAGES as 'D:\CourseProjects32\Repository\DB';
DECLARE
  V_BLOB      BLOB; 
  V_FILE      BFILE;
  V_FILE_SIZE INTEGER;
BEGIN
   insert into Films(NameFilm,Country,DateIssue,Genre,DurationMinutesFilm,Poster)
values('�-�������','���','25.12.2007','����������',96,EMPTY_BLOB) returning Poster into V_BLOB;
   V_FILE := BFILENAME('IMAGES','Legend.jpg');
   V_FILE_SIZE := DBMS_LOB.GETLENGTH(V_FILE);
   DBMS_LOB.FILEOPEN( V_FILE );
   DBMS_LOB.LOADFROMFILE(V_BLOB, V_FILE, V_FILE_SIZE);
   DBMS_LOB.FILECLOSE( V_FILE );
   COMMIT;
END;
