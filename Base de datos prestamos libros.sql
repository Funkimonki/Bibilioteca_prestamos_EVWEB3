create database prestamos_biblioteca
use prestamos_biblioteca

Create table TipoUsuario(
id_tipo int primary key identity(1,1),
Nombre Varchar(50)
)

create table Editorial(
id_editorial int primary key identity (1,1),
Nombre Varchar(50)
)

create table Autor(
id_Autor int primary key identity (1,1),
Nombre Varchar(60))

create table Usuario(
id_Usuario int primary key identity (1,1),
Rut Varchar (10) not null unique,
Nombre Varchar(60) not null,
Telefono Varchar(9),
Direccion varchar(100),
id_tipo int,
constraint FK_Usuario_TipoUsuario foreign key (id_tipo)
references TipoUsuario(id_tipo)
)

ALTER TABLE Usuario
ADD 
email varchar(100),
pass varchar(8)

create table Libro(
id_Libro int primary key identity (1,1),
Titulo Varchar(60),
ISBN Varchar(13) not null,
id_Editorial int,
id_Autor int,
constraint FK_Libro_Editorial foreign key (id_Editorial)
references Editorial(id_Editorial),
constraint FK_Libro_Autor foreign key (id_Autor)
references Autor(id_Autor)
)

Create Table Ejemplar(
id_Ejemplar int primary key identity (1,1),
Localizacion Varchar(100),
id_Libro int,
constraint FK_Ejemplar_Libro foreign key (id_Libro)
references Libro(id_Libro)
)
---Por si la tabla tiene datos----
DELETE FROM Prestamo WHERE id_prestamo=1
DELETE FROM Prestamo WHERE id_prestamo=2
DELETE FROM Prestamo WHERE id_prestamo=3
DELETE FROM Prestamo WHERE id_prestamo=4
---- eliminar tabla------
drop table Ejemplar
--------------actaulizar tabla---------------
ALTER TABLE Libro ADD archivo varchar(300);
--------------Nueva tabla-----------------
Create table Prestamo(
id_prestamo int primary key identity (1,1),
FechaPres date,
FechaDev date,
id_Libro int,
id_Usuario int,
constraint FK_Prestamo_Libro foreign key (id_Libro)
references Libro(id_Libro),
constraint FK_Prestamo_Usuario foreign key (id_Usuario)
references Usuario(id_Usuario)
)
select * from Prestamo
-------insert para iniciar sesion-----
INSERT INTO TipoUsuario
VALUES ('Jefatura');
insert into Usuario (Rut,Nombre,Telefono,Direccion,id_tipo,email,pass)
values ('20119147-5','Jonh Cena','912546538','Avenida Michael Escobar #654',1,'a@a.a','123')
select * from Usuario
select * from Libro


