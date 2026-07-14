create database Bike_Store
use Bike_Store
create table customers(
customer_id int not null primary key,
first_name varchar(255) not null,
last_name varchar(255) not null,
phone varchar(25) null,
email varchar(255) not null,
street varchar(255) null,
city varchar(50) null,
state varchar(25) null,
create_date date not null
)
create table categories(
category_id int not null identity(1,1) primary key,
category_name varchar(255) not null
)
insert into categories values('Mountain Bikes'),
('Road Bikes'),('Electric Bikes'),('Accessories'),('Clothing')
insert into customers (first_name,last_name,phone,email,street,city,state)
values('Juanito Juan','Melgar Soto','77712345',
'jj@gmail.com','Ave. Beni #567','Santa Cruz','Santa Cruz')
select * from customers where first_name = 'Juanito Juan'
alter table products
add imagen image,create_date date
alter table products
add category_id int
alter table products add constraint FK_categoriesProducts 
foreign key (category_id) references categories(category_id)

create table products(
product_id int not null identity(1,1) primary key,
product_name varchar(200) not null,
model_year smallint not null,
price decimal(10,2) not null,
imagen image null,
category_id int not null,
create_date date not null
)

create table users(
usuario_id int not null identity(1,1) primary key,
nombre varchar(50) not null,
clave varchar(250) not null,
email varchar(100) not null
)

create proc spinsertar_products
@product_id int output,
@product_name varchar(200),
@model_year smallint,
@price decimal(10,2),
@imagen image,
@category_id int
as
insert into products(product_name,model_year,price,imagen,category_id)
values (@product_name,@model_year,@price,@imagen,@category_id)
go
create proc speditar_products
@product_id int,
@product_name varchar(200),
@model_year smallint,
@price decimal(10,2),
@imagen image,
@category_id int
as
update products set product_name=@product_name,model_year=@model_year,
price=@price,imagen=@imagen,category_id=@category_id
where product_id=@product_id
go
create proc speliminar_products
@product_id int
as
delete from products
where product_id=@product_id
go
create proc spmostrar_products
as
select p.product_id,p.product_name,p.model_year,p.price,p.imagen,
p.category_id,c.category_name as Category
from products p inner join categories c
on p.category_id = c.category_id
order by p.product_id desc
go
alter proc spbuscar_product_name
@textbuscar varchar(50)
as
select p.product_id,p.product_name,p.model_year,p.price,p.imagen,
p.category_id,c.category_name as Category
from products p inner join categories c
on p.category_id = c.category_id
where product_name like '%' + @textbuscar + '%'
order by p.product_name
go
create proc spinsertar_categories
@category_id int output,
@category_name varchar(255)
as
insert into categories (category_name)
Values (@category_name)
go
create proc speditar_categories
@category_id int,
@category_name varchar(255)
as
update categories set category_name=@category_name
where category_id=@category_id
go
create proc speliminar_categories
@category_id int
as
delete from categories
where category_id=@category_id
go
create proc spmostrar_categories
as
select * from categories
order by category_id desc
go
alter proc spbuscar_category_name
@textbuscar varchar(50)
as
select * from categories
where category_name like '%'+ @textbuscar +'%'
order by category_name
go
create proc spinsertar_users
@usuario_id int output,
@usuario_name varchar(50),
@usuario_clave varchar(250),
@usuario_email varchar(100)
as
insert into users values(@usuario_name,@usuario_clave,@usuario_email)
go
create proc speditar_users
@usuario_id int,
@usuario_name varchar(50),
@usuario_clave varchar(250),
@usuario_email varchar(100)
as
update users set nombre = @usuario_name,
				clave = @usuario_clave,
				email = @usuario_email
where usuario_id = @usuario_id
go
create proc speliminar_users
@usuario_id int
as
delete from users 
where usuario_id = @usuario_id
go
create proc spmostrar_users
as
select * from users
order by usuario_id desc
go
create proc spbuscar_user_name
@textbuscar varchar(50)
as
select * from users
where nombre like '%'+ @textbuscar +'%'
order by nombre
go
create proc splogin
@usuario varchar(50),
@clave varchar(250)
as
select * from users
where nombre = @usuario and clave = @clave
go
