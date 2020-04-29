
create table Films(
IdFilm number generated always as identity primary key,
NameFilm varchar(300) not null,
DescriptionFilm clob not null,
Country varchar(70) not null,
YearIssue number not null,
DurationMinutesFilm int not null,
Poster blob not null,
Trailer bfile,
constraint Уникальное_Название_Фильма unique(NameFilm)
); 

create table CinemaAddresses(
IdAddress number generated always as identity primary key,
Street varchar(150) not null,
NumberHouse number not null,
constraint Уникальность_Адреса unique(Street, NumberHouse)
);

create table MovieTheatres(
IdCinema number generated always as identity primary key,
NameCinema varchar(100) not null,
Address number not null,
constraint Адрес_Кинотеатра foreign key(Address) references CinemaAddresses(IdAddress),
constraint Уникальное_Название_Кинотеатра unique(NameCinema),
constraint Уникальный_Адрес_Кинотеатра unique(Address)
); 

create table Halls(
IdHall number generated always as identity primary key,
NameHall varchar(150) not null,
Cinema number not null,
constraint Кинотеатр_Зала foreign key(Cinema) references MovieTheatres(IdCinema),
constraint Уникальный_Зал_В_Кинотеатре unique(NameHall, Cinema)
); 	

create table Sessions(
IdSession number generated always as identity primary key,
Film number not null,
Hall number not null,
StartSession date not null,
constraint Фильм_Сеанса foreign key(Film) references Films(IdFilm),
constraint Зал_Проведения_Сеанса foreign key(Hall) references Halls(IdHall)
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
constraint Зал_сектора foreign key(Hall) references Halls(IdHall),
constraint Уникальный_сектор_зала unique(NameSector, Hall)
);

create table Seats(
IdSeat number generated always as identity primary key,
NumberSeat number not null,
SectorHall number not null,
constraint Сектор_Места foreign key(SectorHall) references SectorsHall(IdSector)
);

create table Tickets(
IdTicket number generated always as identity primary key,
Buyer number not null,
SessionId number not null,
SeatId number not null,
constraint Покупатель_Билета foreign key(Buyer) references Users(IdUser),
constraint Сеанс_Билета foreign key(SessionId) references Sessions(IdSession),
constraint Место_Билета foreign key(SeatId) references Seats(IdSeat)
); 
--Процедуры и функции
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

create or replace procedure EditCinema(
idEditCinema number,
newName MovieTheatres.NameCinema%type
)
as
begin
update MovieTheatres set NameCinema=newName where IdCinema=idEditCinema;
commit;
end;

create or replace procedure EditAddress(
idEditAddress number,
newStreet CinemaAddresses.Street%type,
newNumberHouse CinemaAddresses.NumberHouse%type
)
as
begin
update CinemaAddresses set street=newStreet, NumberHouse=newNumberHouse where IdAddress=idEditAddress;
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
password Users.Password%type
)
as
begin
insert into Users(Login, Password, RoleOfUser) values (login, password, 2);
commit;
end;

create or replace procedure AddFilm(
namefilm Films.NameFilm%type,
descriptionfilm Films.DescriptionFilm%type,
country Films.Country%type,
yearissue Films.YearIssue%type,
durationminutesfilm Films.DurationMinutesFilm%type,
poster Films.Poster%type,
trailerVideo varchar2
)
as
begin
insert into system.Films(NameFilm,DescriptionFilm,Country,YearIssue,DurationMinutesFilm,Poster,Trailer)
values(namefilm, descriptionfilm, country, yearissue, durationminutesfilm, poster, BFILENAME('TRAILERDIR',trailerVideo));
commit;
end;

create or replace procedure ChangeUserPassword(
lg Users.Login%type,
newPsw Users.Password%type
)
as
begin
update Users set Password=newPsw where Login=lg;
commit;
end;

--------------
create or replace function GetCinema(cnm number)
return SYS_REFCURSOR
is
resultCinema SYS_REFCURSOR;
begin
open resultCinema for select * from MovieTheatres join CinemaAddresses on Address=IdAddress where IdCinema=cnm;
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
open allFilms for select IdFilm, NameFilm, DescriptionFilm, Country,
YearIssue, DurationMinutesFilm, Poster, Trailer, get_dir_name(Trailer), get_file_name(Trailer) from Films;
return allFilms;
end;

create or replace function GetAllCinemas
return SYS_REFCURSOR
is
allCinemas SYS_REFCURSOR;
begin
open allCinemas for select * from MovieTheatres join CinemaAddresses on Address=IdAddress;
return allCinemas;
end;

create or replace function GetAllHallsCinema(cnm number)
return SYS_REFCURSOR
is
allHallsCinema SYS_REFCURSOR;
begin
open allHallsCinema for select * from Halls join SectorsHall on IdHall=Hall where Cinema=cnm order by IdHall;
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

create or replace function GetCinemasByName(
nmCnm MovieTheatres.NameCinema%type)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from MovieTheatres join CinemaAddresses on Address=IdAddress where NameCinema=nmCnm;
return res;
end;

create or replace function GetFilmById(
idFlm number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select IdFilm, NameFilm, DescriptionFilm, Country, YearIssue, DurationMinutesFilm, Poster, 
Trailer, get_dir_name(Trailer), get_file_name(Trailer) from Films where IdFilm=idFlm;
return res;
end;

--Проверка SELECTS
select * from Sessions join Films on Film=IdFilm where Hall=4
--Создание ролей
create role c##Role_Admin;

grant create session to c##Role_Admin;
grant all on Films to c##Role_Admin;
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
grant execute on GetCinema to c##Role_Admin;
grant execute on GetAllCinemas to c##Role_Admin;
grant execute on GetAllFilms to c##Role_Admin;
grant execute on GetAllHallsCinema to c##Role_Admin;
grant execute on GetHallsByCinameName to c##Role_Admin;
grant execute on GetRoleForUser to c##Role_Admin;
grant execute on GetSessionsByHallId to c##Role_Admin;
grant execute on GetUser to c##Role_Admin;
grant execute on AddFilm to c##Role_Admin;
grant execute on ChangeUserPassword to c##Role_Admin;
grant execute on GetCinemasByName to c##Role_Admin;
grant execute on EditAddress to c##Role_Admin;
grant execute on EditCinema to c##Role_Admin;
grant execute on GetFilmById to c##Role_Admin;
grant execute on SaveTriggerChanges to c##Role_Admin;
grant execute on get_dir_name to c##Role_Admin;
grant execute on get_file_name to c##Role_Admin;
grant create any directory to c##Role_Admin;

create user c##Admin identified by admin;
grant c##Role_Admin to c##Admin;

create role C##Role_User;

grant create session to c##Role_User;
grant select on Films to c##Role_User;
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
grant execute on GetCinema to c##Role_User;
grant execute on GetAllCinemas to c##Role_User;
grant execute on GetAllFilms to c##Role_User;
grant execute on GetAllHallsCinema to c##Role_User;
grant execute on GetHallsByCinameName to c##Role_User;
grant execute on GetRoleForUser to c##Role_User;
grant execute on GetSessionsByHallId to c##Role_User;
grant execute on GetUser to c##Role_User;
grant execute on ChangeUserPassword to c##Role_User;
grant execute on GetCinemasByName to c##Role_User;
grant execute on GetFilmById to c##Role_User;
grant execute on SaveTriggerChanges to c##Role_User;
grant execute on get_dir_name to c##Role_User;
grant execute on get_file_name to c##Role_User;

create user C##User identified by user;
grant C##Role_User to C##User;
--Вставка данных
insert into RolesOfUsers(IdRole, NameRole, NameConnection) values(1, 'Admin','C##Admin');
insert into RolesOfUsers(IdRole, NameRole, NameConnection) values(2, 'User','C##User');

insert into Users(Login, Password, RoleOfUser) values('Admin','GaKFQUS2Oo92F6byJQGbEg==',1); --Пароль: 'admin'
--Тема курсового
create or replace directory IMAGES as 'D:\CourseProjects32\Repository\DB';
DECLARE
  V_BLOB      BLOB; 
  V_FILE      BFILE;
  V_FILE_SIZE INTEGER;
BEGIN
   insert into Films(NameFilm,Country,DateIssue,Genre,DurationMinutesFilm,Poster)
values('Я-легенда','США','25.12.2007','фантастика',96,EMPTY_BLOB) returning Poster into V_BLOB;
   V_FILE := BFILENAME('IMAGES','Legend.jpg');
   V_FILE_SIZE := DBMS_LOB.GETLENGTH(V_FILE);
   DBMS_LOB.FILEOPEN( V_FILE );
   DBMS_LOB.LOADFROMFILE(V_BLOB, V_FILE, V_FILE_SIZE);
   DBMS_LOB.FILECLOSE( V_FILE );
   COMMIT;
END;

-----DBMS_JOB!!!!!
create or replace procedure RemoveSessionsAndTickets
as
begin
delete from Tickets where SessionId in (select IdSession from Sessions where StartSession<SYSDATE);
delete from Sessions where StartSession<SYSDATE;
commit;
end;

--Таблицы будут очищаться каждый день в один час пять минут
declare
begin
dbms_job.isubmit(100, 'begin RemoveSessionsAndTickets; end;', TO_DATE('29.04.2020 01:05','DD.MM.YYYY HH24:MI'), 'TRUNC(SYSDATE+1)+(1+(5/60))/24');
commit;
end;
--Триггер на заполенение таблицы Seats и процедура, сохраняющая изменения триггера;
create or replace trigger AfterInsertUpdateRowSector
after insert or update on SectorsHall
for each row
declare
i seats.numberseat%type:=1;
begin
if inserting then
while(i<=((:new.EndRow-:new.StartRow+1)*:new.CountSeatsRow))
loop
insert into Seats(NumberSeat, SectorHall) values(i, :new.IdSector);
i:=i+1;
end loop;
end if;
if updating then
delete from Seats where SectorHall=:old.IdSector;
while(i<=((:new.EndRow-:new.StartRow+1)*:new.CountSeatsRow))
loop
insert into Seats(NumberSeat, SectorHall) values(i, :new.IdSector);
i:=i+1;
end loop;
end if;
end;

create or replace trigger BeforeDeleteSector
before delete on SectorsHall
for each row
begin
delete from Seats where SectorHall=:old.IdSector;
end;

create or replace procedure SaveTriggerChanges
as
begin
commit;
end;
---CHECK VIDEO BFILE
CREATE FUNCTION get_dir_name (bf BFILE) RETURN VARCHAR2 IS
    DIR_ALIAS VARCHAR2(255);
   FILE_NAME VARCHAR2(255);
    BEGIN
      IF bf is NULL
      THEN
        RETURN NULL;
      ELSE
        DBMS_LOB.FILEGETNAME (bf, dir_alias, file_name);
       RETURN dir_alias;
     END IF;
   END;
   
   CREATE FUNCTION get_file_name (bf BFILE) RETURN VARCHAR2 is
    dir_alias VARCHAR2(255);
    file_name VARCHAR2(255);
    BEGIN
      IF bf is NULL
      THEN
        RETURN NULL;
      ELSE
        DBMS_LOB.FILEGETNAME (bf, dir_alias, file_name);
       RETURN file_name;
     END IF;
   END;
   
