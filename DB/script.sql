create table Films(
IdFilm number generated always as identity primary key,
NameFilm varchar(300) not null,
DescriptionFilm clob not null,
Country varchar(70) not null,
YearIssue number not null,
DurationMinutesFilm int not null,
Poster blob not null,
Trailer bfile,
constraint ����������_��������_������ unique(NameFilm)
); 

create table CinemaAddresses(
IdAddress number generated always as identity primary key,
Street varchar(150) not null,
NumberHouse number not null,
constraint ������������_������ unique(Street, NumberHouse)
);

create table MovieTheatres(
IdCinema number generated always as identity primary key,
NameCinema varchar(100) not null,
Address number not null,
constraint �����_���������� foreign key(Address) references CinemaAddresses(IdAddress),
constraint ����������_��������_���������� unique(NameCinema),
constraint ����������_�����_���������� unique(Address)
); 

create table Halls(
IdHall number generated always as identity primary key,
NameHall varchar(150) not null,
Cinema number not null,
constraint ���������_���� foreign key(Cinema) references MovieTheatres(IdCinema),
constraint ����������_���_�_���������� unique(NameHall, Cinema)
); 	

create table Sessions(
IdSession number generated always as identity primary key,
Film number not null,
Hall number not null,
StartSession date not null,
constraint �����_������ foreign key(Film) references Films(IdFilm),
constraint ���_����������_������ foreign key(Hall) references Halls(IdHall)
);

create table RolesOfUsers(
IdRole number primary key,
NameRole varchar(50) unique not null,
NameConnection varchar(50) unique not null,
constraint CheckNameRole_Roles check(NameRole in ('Admin','User')),
constraint CheckNameConnection_Roles check(NameConnection in ('C##Admin', 'C##User'))
);

select * from RolesOfUsers

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
constraint ���_������� foreign key(Hall) references Halls(IdHall),
constraint ����������_������_���� unique(NameSector, Hall)
);

create table Seats(
IdSeat number generated always as identity primary key,
NumberSeat number not null,
SectorHall number not null,
constraint ������_����� foreign key(SectorHall) references SectorsHall(IdSector)
);

create table Tickets(
IdTicket number generated always as identity primary key,
Buyer number not null,
SessionId number not null,
SeatId number not null,
constraint ����������_������ foreign key(Buyer) references Users(IdUser),
constraint �����_������ foreign key(SessionId) references Sessions(IdSession),
constraint �����_������ foreign key(SeatId) references Seats(IdSeat)
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

create or replace procedure DeleteAddress(
idAddr number)
as
begin
delete from CinemaAddresses where IdAddress=idAddr;
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

create or replace procedure DeleteCinema(
idC number)
as
begin
delete from MovieTheatres where IdCinema=idC;
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

create or replace procedure EditHall(
idHl number,
nmHl Halls.NameHall%type,
cnm number
)
as
begin
update Halls set NameHall=nmHl, Cinema=cnm where IdHall=idHl;
commit;
end;

create or replace procedure DeleteHall(
idHl number)
as
begin
delete from Halls where IdHall=idHl;
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

create or replace procedure EditSector(
idS number,
hl number,
nmsector SectorsHall.NameSector%type,
strow number,
enrow number,
countseatsr number,
costs number
)
as
begin
update SectorsHall set Hall=hl, NameSector=nmsector, StartRow=strow, EndRow=enrow,
CountSeatsRow=countseatsr, CostSeat=costs where IdSector=idS;
commit;
end;

create or replace procedure DeleteSector(
idSc number)
as
begin
delete from SectorsHall where IdSector=idSc;
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

create or replace procedure EditSession(
idS number,
flm number,
hl number,
sts Sessions.StartSession%type
)
as
begin
update Sessions set Film=flm, Hall=hl, StartSession=sts where IdSession=idS;
commit;
end;

create or replace procedure DeleteSession(
sId number
)
as
begin
delete from Sessions where IdSession=sId;
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

create or replace procedure EditFilm(
idFlm number,
nameflm Films.NameFilm%type,
descript Films.DescriptionFilm%type,
cntr Films.Country%type,
yr Films.YearIssue%type,
durationflm Films.DurationMinutesFilm%type,
pstr Films.Poster%type,
trailerVideo varchar2
)
as
begin
update Films set NameFilm=nameflm, DescriptionFilm=descript, Country=cntr, YearIssue=yr, DurationMinutesFilm=durationflm,
Poster=pstr, Trailer=BFILENAME('TRAILERDIR', trailerVideo) where IdFilm=idFlm;
commit;
end;

create or replace procedure DeleteFilm(
idFlm number
)
as
begin
delete from Films where IdFilm=idFlm;
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

create or replace procedure AddTicket(
buyer number,
sessionid number,
seatid number
)
as
begin
insert into Tickets(Buyer, SessionId, SeatId) values(buyer, sessionid, seatid);
commit;
end;

create or replace procedure DeleteTicket
(tcktId number)
as
begin
delete from Tickets where IdTicket=tcktId;
commit;
end;
-----------------
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

create or replace function GetFilmsNames
return SYS_REFCURSOR
is
filmsNames SYS_REFCURSOR;
begin
open filmsNames for select IdFilm, NameFilm from Films;
return filmsNames;
end;

create or replace function GetAllCinemas
return SYS_REFCURSOR
is
allCinemas SYS_REFCURSOR;
begin
open allCinemas for select * from MovieTheatres join CinemaAddresses on Address=IdAddress;
return allCinemas;
end;

create or replace function GetCinemaBySectorId(stId number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from SectorsHall join (select * from Halls join MovieTheatres on Cinema=IdCinema) on Hall=IdHall
where IdSector=stId;
return res;
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

create or replace function GetHallById(idHl number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Halls where IdHall=idHl;
return res;
end;

create or replace function GetSessionsByHallId(hlId number)
return SYS_REFCURSOR
is
resultSessions SYS_REFCURSOR;
begin
open resultSessions for select * from Sessions join Films on Film=IdFilm where Hall=hlId order by StartSession;
return resultSessions;
end;

create or replace function GetSessionsByFilmId(filmId number)
return SYS_REFCURSOR
is
resultSessions SYS_REFCURSOR;
begin
open resultSessions for select * from Sessions join Films on Film=IdFilm 
join (select * from MovieTheatres join Halls on IdCinema=Cinema) on Hall=IdHall 
where Film=filmId order by StartSession;
return resultSessions;
end;

create or replace function GetSessionsByStartSession(startDate varchar2)
return SYS_REFCURSOR
is
resultSessions SYS_REFCURSOR;
begin
open resultSessions for select * from Sessions join Films on Film=IdFilm where to_char(StartSession, 'DD.MM.YYYY')=startDate;
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

create or replace function GetFilmByName(
nameFlm Films.NameFilm%type)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select IdFilm, NameFilm, DescriptionFilm, Country, YearIssue, DurationMinutesFilm, Poster, 
Trailer, get_dir_name(Trailer), get_file_name(Trailer) from Films where NameFilm=nameFlm;
return res;
end;

create or replace function GetSeatsOfHall(idHl number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Seats join (select * from SectorsHall join Halls on Hall=IdHall) on SectorHall=IdSector
where Hall=idHl order by SectorHall, NumberSeat;
return res;
end;

create or replace function GetAllTickets
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Tickets;
return res;
end;

create or replace function GetTicketsByUser(us number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Tickets where Buyer=us;
return res;
end;

create or replace function GetSessionById(sId number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Sessions where IdSession=sId;
return res;
end;

create or replace function GetSeatById(stId number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Seats where IdSeat=stId;
return res;
end;

create or replace function GetTicketBySessionId(sId number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from Tickets where SessionId=sId;
return res;
end;

create or replace function GetSectorsByHallId(idHl number)
return SYS_REFCURSOR
is
res SYS_REFCURSOR;
begin
open res for select * from SectorsHall join Halls on Hall=IdHall where Hall=idHl;
return res;
end;
--�������� SELECTS
select * from Sessions join Films on Film=IdFilm where Hall=4
--�������� �����
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
grant execute on EditSession to c##Role_Admin;
grant execute on GetCinema to c##Role_Admin;
grant execute on GetAllCinemas to c##Role_Admin;
grant execute on GetAllFilms to c##Role_Admin;
grant execute on GetAllHallsCinema to c##Role_Admin;
grant execute on GetHallsByCinameName to c##Role_Admin;
grant execute on GetRoleForUser to c##Role_Admin;
grant execute on GetSessionsByHallId to c##Role_Admin;
grant execute on GetUser to c##Role_Admin;
grant execute on AddFilm to c##Role_Admin;
grant execute on EditFilm to c##Role_Admin;
grant execute on DeleteFilm to c##Role_Admin;
grant execute on ChangeUserPassword to c##Role_Admin;
grant execute on GetCinemasByName to c##Role_Admin;
grant execute on EditAddress to c##Role_Admin;
grant execute on EditCinema to c##Role_Admin;
grant execute on GetFilmById to c##Role_Admin;
grant execute on SaveTriggerChanges to c##Role_Admin;
grant execute on get_dir_name to c##Role_Admin;
grant execute on get_file_name to c##Role_Admin;
grant create any directory to c##Role_Admin;
grant execute on GetSessionsByFilmId to c##Role_Admin;
grant execute on GetFilmByName to c##Role_Admin;
grant execute on GetSessionsByStartSession to c##Role_Admin;
grant execute on GetFilmsNames to c##Role_Admin;
grant execute on GetSeatsOfHall to c##Role_Admin;
grant execute on GetAllTickets to c##Role_Admin;
grant execute on GetTicketsByUser to c##Role_Admin;
grant execute on GetSeatById to c##Role_Admin;
grant execute on GetSessionById to c##Role_Admin;
grant execute on GetCinemaBySectorId to c##Role_Admin;
grant execute on DeleteSession to c##Role_Admin;
grant execute on GetTicketBySessionId to c##Role_Admin;
grant execute on DeleteHall to c##Role_Admin;
grant execute on DeleteSector to c##Role_Admin;
grant execute on GetSectorsByHallId to c##Role_Admin;
grant execute on DeleteAddress to c##Role_Admin;
grant execute on DeleteCinema to c##Role_Admin;
grant execute on EditHall to c##Role_Admin;
grant execute on EditSector to c##Role_Admin;
grant execute on GetHallById to c##Role_Admin;

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
grant execute on GetSessionsByFilmId to c##Role_User;
grant execute on GetUser to c##Role_User;
grant execute on ChangeUserPassword to c##Role_User;
grant execute on GetCinemasByName to c##Role_User;
grant execute on GetFilmById to c##Role_User;
grant execute on SaveTriggerChanges to c##Role_User;
grant execute on get_dir_name to c##Role_User;
grant execute on get_file_name to c##Role_User;
grant execute on GetFilmByName to c##Role_User;
grant create any directory to c##Role_User;
grant execute on GetSessionsByStartSession to c##Role_User;
grant execute on GetFilmsNames to c##Role_User;
grant execute on GetSeatsOfHall to c##Role_User;
grant execute on GetAllTickets to c##Role_User;
grant execute on AddTicket to c##Role_User;
grant execute on GetTicketsByUser to c##Role_User;
grant execute on GetSeatById to c##Role_User;
grant execute on GetSessionById to c##Role_User;
grant execute on GetCinemaBySectorId to c##Role_User;
grant execute on DeleteTicket to c##Role_User;
grant execute on GetHallById to c##Role_User;

create user C##User identified by user;
grant C##Role_User to C##User;
--������� ������
insert into RolesOfUsers(IdRole, NameRole, NameConnection) values(1, 'Admin','C##Admin');
insert into RolesOfUsers(IdRole, NameRole, NameConnection) values(2, 'User','C##User');

insert into Users(Login, Password, RoleOfUser) values('Admin',Md5_b64('admin'),1); --������: 'admin'
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

-----DBMS_JOB
create or replace procedure RemoveSessionsAndTickets
as
begin
delete from Tickets where SessionId in (select IdSession from Sessions where StartSession<SYSDATE);
delete from Sessions where StartSession<SYSDATE;
commit;
end;

--������� ����� ��������� ������ ���� � ���� ��� ���� �����
declare
begin
dbms_job.isubmit(100, 'begin RemoveSessionsAndTickets; end;', TO_DATE('29.04.2020 01:05','DD.MM.YYYY HH24:MI'), 'TRUNC(SYSDATE+1)+(1+(5/60))/24');
commit;
end;
--�������� ��� ��������� ������;

-----��������, ��������� � �����������
create or replace trigger BeforeDeleteCinema
before delete on MovieTheatres
for each row
begin
delete from Halls where Cinema=:old.IdCinema;
end;

create or replace trigger AfterDeleteCinema
after delete on MovieTheatres
for each row
begin
delete from CinemaAddresses where IdAddress=:old.Address;
end;

---��������, ��������� � �����
create or replace trigger BeforeDeleteHall
before delete on Halls
for each row
begin
delete from SectorsHall where Hall=:old.IdHall;
end;
-----��������, ��������� � ��������
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
   
---100 000
create or replace procedure InputOneHundredSousan
as
begin
for i in 1..100000 loop
insert into users(Login, Password, RoleOfUser) values('User'||i,Md5_b64('user'||i),2);
end loop;
commit;
end;

begin
InputOneHundredSousan;
end;

create function Md5_b64(i_Inp varchar2) return varchar2 is
  begin
    return Utl_Raw.Cast_To_Varchar2(
               Utl_Encode.Base64_Encode(
                   Dbms_Obfuscation_Toolkit.Md5(Input => Utl_Raw.Cast_To_Raw(i_Inp)
           )));
  end;