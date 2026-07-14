use conchi2

Create table prov (
cprv int primary key ,
nombr varchar (50) NOT NULL,
ciud varchar (50) NOT NULL,
)

Create table alma (
calm int primary key,
noma varchar (50) NOT NULL,
ciud varchar (50) NOT NULL,
)
Create table prod (
cprd int primary key,
nomp varchar (50) not null,
colo varchar (50) not null,
)

Create table sumi(
ftra date not null,
cant float (20) not null,
prec float (50) not null,
impt float (50) not null,
cprv int,
calm int,
cprd int,
foreign key (cprv) references prov (cprv),
foreign key (calm) references alma (calm),
foreign key (cprd) references prod(cprd)
)

insert into alma(calm,noma,ciud)values
(1,'pepito','santa cruz'),
(2,'el peto','la paz'),
(3,'balneario','cochabamba');

insert into prod(cprd,nomp,colo)values
(1,'esponja','amarillo'),
(2,'lava-vajillas','rojo'),
(3,'jaboncillo','azul');

insert into prov(cprv,nombr,ciud)values
(1,'jacinto reyes','la paz'),
(2,'alvaro peña','beni'),
(3,'julio mateus','pando');

INSERT INTO sumi (ftra, cant, prec, impt, calm, cprd, cprv) VALUES
('2023-05-10', 100, 15.5, 1550.0, 1, 2, 2),
('2023-06-15', 200, 20.0, 4000.0, 2, 1, 1),
('2023-07-20', 150, 18.0, 2700.0, 3, 2, 2);





/*Funciones*/
Create Function Importe_Pagar(@cprv int,@ciud char(2))
Returns decimal (12,2)
As
Begin 
	Declare @imp_trans decimal (12,5)
	Declare @costo_trans_ex decimal (12,5)@impuesto decimal(12,5),@bono decimal(12,5)
	
	Set @bono=dbo.bono(@cprv,@ciud)
	Set @costo_trans_ex=dbo.costo_trans_ex(@cprv,@ciud)
	Set @impuesto=dbo.impuesto(@cprv,@ciud)

	Set @imp_trans =((@bono + @costo_trans_ex) - @impuesto)

	Return (@imp_trans)
	END
Create Function Impuesto(@crpv int, @ciud char (2))
Returns decimal (12,2)
As 
Begin 
	Return (
			Select isnull(sum(impt),0*(3.0/100)
			From sumi,prov
			Where sumi.cprv=prov.cprv
			AND sumi.cprv=@cprv
			AND prov.ciud=@ciud
			)
End
print dbo.Impuesto(3,'SC')

Create Function bono(@cprv int,@ciud char(2))
Returns decimal (12,2)
As
Begin
	Return(
		Select isnull (avg(impt),0)*(0.5/100)
		From sumi,prov
		Where sumi.cprv= prov.cprv
		And prov.ciud=@ciud
		And sumi.cprv=@cprv
		And sumi.cprv IN(
						Select cprv From sumi
						Group BY cprv
						Having count (distinct ftra)>=2
End

Create FUNCTION (costo_trns_ex(@cprv int,@ciud char(2))
Returns decimal (12,2)
As
Begin
	Return(
			Select isnull(sum(impt),0)*(2.5/100)
			From sumi,prov,alma
			Where sumi.cprv=prov.cprv AND sumi.calm=alma.calm
			AND prov.ciud<>alma.ciud
			AND sumi.cprv=@cprv
			AND prov.ciud=@ciud
			)
END
print dbo.costo_trans_ex(3,'SC')





