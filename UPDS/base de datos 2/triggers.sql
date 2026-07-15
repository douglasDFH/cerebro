create database ejercicio_7
use ejercicio_7
drop procedure Pa_Inventario
CREATE PROCEDURE Pa_Inventario(@ciud as char (100))
as
	declare @cprd int,@nomp char(30)
	declare @cant decimal,@prec decimal,@impt decimal,@timp decimal
	declare c_lista cursor for select distinct sumi.cprd,nomp
								from sumi,alma,prod
								where sumi.cprd=prod.cprd and sumi.calm=alma.calm
								and alma.ciud=@ciud
	set @timp=0
	open c_lista
	fetch c_lista into @cprd,@nomp
	while @@FETCH_STATUS=0
		begin
			set @cant=dbo.Cantidad(@cprd,@ciud)  --solo el 50%
			set @prec=dbo.Precio(@cprd,@ciud)  --precio mas bajo
			set @impt=dbo.Importe(@cant,@prec)  --solo el 50%
			print cast (@cprd as char(100))+@nomp+cast(@cant as char(20))+cast(@prec as char (20))+cast(@impt as char (30))
			set @timp=@timp+@impt
			fetch c_lista into @cprd,@nomp
		end
		print @timp
	close c_lista
	deallocate c_lista
return

drop function Cantidad
create function Cantidad(@cprd int,@ciud char(100))
returns decimal (12,3)
as
begin
	declare @cant decimal
	select @cant=ISNULL(sum(cant),0)from sumi,alma,prod
	where sumi.cprd=prod.cprd and sumi.calm=alma.calm
	and sumi.cprd=@cprd and alma.ciud=@ciud
	return (@cant*0.5)
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

EXEC Pa_Inventario @ciud = 'SANTA CRUZ';

select * from sumi
