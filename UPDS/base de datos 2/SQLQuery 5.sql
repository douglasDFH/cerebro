create database conchi_3
use conchi_3

 --Creacion del Pa_Inventario
Drop procedure Pa_Inventario
Create procedure Pa_Inventario(@ciud As Char(2))
as
	declare @crpd Int, @nomp Char (30)
	declare @cant decimal,@impt Decimal, @timp decimal
	declare c_lista cursor for select distinct sumi.cprd,nomp
				from sumi,alma,prod
				where sumi.cprd=prod.cprd and sumi.calm=alma.calm
				and alma.ciud=@ciud
	Set @timp=0
	Open c_lista
	Fetch c_lista into @cprd,@nomp
	while @@FETCH_STATUS=0
		Begin 
			Set @cant=dbo.Cantidad(@cprd,@ciud) --solo el 50%
			Set @prec=dbo.Precio(@cprd,@ciud) --Precio mas bajo
			Set @impt=dbo.Importe(@cant,@prec)
			print cast (@cprd as char (4)) +@nomp +cast (@cant as char (6)) + cast (@prec as char (6)) +cast(@impt as char(6))
			set @timp=@timp+@impt
			fetch c_lista into @cprd,@nomp
		end
		print @timp
	close c_lista
	deallocate c_lista
return

--Funcion que devuelve el 50% de la cantidad existente de un producto en los almacenes de una ciudad
Drop function Cantidad
Create Function Cantidad(@cprd Int,@ciud char((2)
returns decimal (12,3)
as
begin
	declare @cant decimal
	select @cant=isnull (sum(cant),0)from sumi,alma,prod
	where sumi.cprd=prod.cprd and sumi.calm=alma.calm
	and sumi.cprd=@cprd and alma.ciud=@ciud
	return(@cant*0.5)
end
drop function Precio
create function Precio(@cprd int, @ciud char (100))
returns decimal (12,5)
as
begin
	declare @precbajo decimal

	select @precbajo=min(prec)
	from sumi,alma,prod
	where sumi.cprd=prod.cprd and sumi.calm=alma.calm
	and sumi.cprd=@cprd and alma.ciud=@ciud
	return @precbajo
end

drop function Importe
create function Importe(@cant decimal,@prec decimal)
returns decimal (12,5)
as
begin
	return(@cant*@prec)
end

EXEC Pa_Inventario @ciud = '';

select * from prov




