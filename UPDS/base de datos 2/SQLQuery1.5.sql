use conchi2
/*Unique*/
create table persona1(
id_per int unique,
nombre varchar(30)
)

insert into persona1 values(14,'Juana')
insert into persona1 values(14,'Juana')

Select * From persona1

/*Check*/
Create table persona2(
id_per int check (id_per>10),
nombre varchar (30)
)
insert into persona2 values (14,'Juana')
insert into persona2 values (11,'Marisol')
Select * from persona2

/*Default*/
Create table persona3(
id_per int default '0',
nombre varchar(30)
)

insert into persona3 values (14,'Juana')
insert into persona3 (nombre) values ('Marisol')
insert into persona3  values (11,'Marisol')

Select * from persona3

