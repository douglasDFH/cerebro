use conchi2
/*Funciones*/

create function f_promedio
(
@val1 float, @val2 float
)
returns float
as
begin declare @resultado float
set @resultado= (@val1+@val2)/2
return @resultado
end
select dbo.f_promedio(50,20)