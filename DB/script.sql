create table Films(
IdFilm number generated always as identity primary key,
NameFilm varchar(300) not null,
Country varchar(70) not null,
DateIssue date not null,
Genre varchar(100) not null,
DurationFilm date not null,
Poster blob not null
); 

create or replace directory IMAGES as 'D:\CourseProjects32\Repository\DB';
DECLARE
  V_BLOB      BLOB;
  V_FILE      BFILE;
  V_FILE_SIZE INTEGER;
BEGIN
   insert into Films(NameFilm,Country,DateIssue,Genre,DurationFilm,Poster)
values('aaaa','asdas','25.12.1993','sdsds','25.12.2000',EMPTY_BLOB) returning Poster into V_BLOB;
   V_FILE := BFILENAME('IMAGES','StarWars.jpg');
   V_FILE_SIZE := DBMS_LOB.GETLENGTH(V_FILE);
   DBMS_LOB.FILEOPEN( V_FILE );
   DBMS_LOB.LOADFROMFILE(V_BLOB, V_FILE, V_FILE_SIZE);
   DBMS_LOB.FILECLOSE( V_FILE );
   COMMIT;
END;

select * from Films
