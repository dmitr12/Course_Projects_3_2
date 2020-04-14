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

create table Users(
IdUser number generated always as identity primary key,
Login varchar(100) unique not null,
Password clob not null,
Status varchar(100) not null  ////����������� ����
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

insert into Genres(NameGenre, DescriptionGenre) values ('��������������','�������������� ����� � ���� �������������, ������������ � ������ �����-���� ���������, ���������� ��������, ���������� ���� ���� � �������. ��������� �������������� ������ ������� �������� ���� �������� �������� �� ����� �������� �����, ������ �� �������� ����� ������������� � ������� ��� ��������, ����� ��������, ��� ������������ ��� �������� ��� �������� ��� ���������, ��������, ������, ������� ����������, ������, ������ ����� � �.�.');
insert into Genres(NameGenre, DescriptionGenre) values ('������','������ � ������������������� ����, � ������� ������� ����� ��� ����� ������������ � ����� �������, ������ �������, �� ��������� � �������, �� �������. ������ ������� ��������� �������� �� ���� ��� �����������, ��������� ��������, �������������� ������������� � �������� ������������� �������. ������� ����� ����� ����������� �, �������� ��, �����������, ���������� ������� ���������, ��������� ������ �� ������� �� ������� ��������� �� ���������������� ����������, ������������ � �������������. � ���������� ����������� �������� ����� �����������, � ������ �������� ��� ����������� �� ��������. ���� ������� ����� ������������� � ����� ������ ������, ������ ����� ����� ���������� � ���������������� �������� � ����������.');
insert into Genres(NameGenre, DescriptionGenre) values ('����������','�������������� ������ � ������������ �������� �������������, ����� ������� ������������ �� �������������� ����������� � ������� ������������, ������������ � ����������� ����. � ������� �������� ���������� ����� �������������� �� ��� ���� �������, ������� � ����������, ������� ������������ ����� ������������ ��� ������� ��� ���� ���������� � �������. ��������: ��������� ����� �����, ������������ ����, ��������������� �����������, ����������� �� �������, ����������� �����������, �������, ������������� ��������� � ���� ��������. ����� �������� ������� � ����������� �������������� ������� � ��������� ��� ���������� �������.');
insert into Genres(NameGenre, DescriptionGenre) values ('�������','������� � ������������� � ������������������� ����, � ���������� ���������. ����������� � ������������ ������ ��������� �������� ���������� ��� ������� �������, ���������������� (�������), ����������� � ���������. ������� �������� ����� ����� �������� ������ ������������ ����������� ��������� �������� �������, ������ ������ �������� ���� � ���� ������� ����� �������� �����, ���������� ������� ������������ � � ���� ���.������ ���������� ����������� ������� ����� ����� �������� �������� ������ ���������� �� �������, ������ �������, ����������� �������� �������� � �.�. ������������ (����� ������������� �� ����� ����������, ��������� �������). ������ ������ �������� ������ ������� � ���������� �� ���������� ����� ������.');
insert into Genres(NameGenre, DescriptionGenre) values ('�����','������������� ������ � ���� �� �������� ���������������� ������������������� ������. ��� �������, ��� ������ ���������� � ������� ����� � ���������� ���������� ����������, ���������� �������� �� ����������� � �� ��������� � ��������� ���������������� �������������. ����������� ������ ����� �������� ������������ � ���������� ���������� � ������� �����.');

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

select * from Films
