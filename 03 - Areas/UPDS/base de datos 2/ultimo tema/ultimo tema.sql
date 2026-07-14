USE [AdventureWorks2012]
GO

ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductSubcategory_ProductSubcategoryID] FOREIGN KEY([ProductSubcategoryID])
REFERENCES [Production].[ProductSubcategory] ([ProductSubcategoryID])
GO

ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_ProductSubcategory_ProductSubcategoryID]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign key constraint referencing ProductSubcategory.ProductSubcategoryID.' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'CONSTRAINT',@level2name=N'FK_Product_ProductSubcategory_ProductSubcategoryID'
GO


--Consulta 1: Listar productos con su categoría y subcategoría
--Enunciado: Obtener una lista de productos, incluyendo sus nombres, la categoría y la subcategoría a la que pertenecen.

 
SELECT 
    p.Name AS ProductName,
    sc.Name AS SubcategoryName,
    c.Name AS CategoryName
FROM 
    Production.Product p
JOIN 
    Production.ProductSubcategory sc ON p.ProductSubcategoryID = sc.ProductSubcategoryID
JOIN 
    Production.ProductCategory c ON sc.ProductCategoryID = c.ProductCategoryID;

--Consulta 2: Ventas por vendedor
--Enunciado: Obtener un resumen de las ventas realizadas por cada vendedor, incluyendo el nombre del vendedor, 
--la cantidad total vendida y el total de ventas en dólares.
	SELECT 
    s.SalesPersonID,
    p.FirstName + ' ' + p.LastName AS SalesPersonName,
    COUNT(s.SalesOrderID) AS TotalOrders,
    SUM(s.TotalDue) AS TotalSales
FROM 
    Sales.SalesOrderHeader s
JOIN 
    HumanResources.Employee e ON s.SalesPersonID = e.BusinessEntityID
JOIN 
    Person.Person p ON e.BusinessEntityID = p.BusinessEntityID
GROUP BY 
    s.SalesPersonID, p.FirstName, p.LastName
ORDER BY 
    TotalSales DESC;

--Consulta 3: Detalles de órdenes de compra con información de proveedores y productos
--Enunciado: Obtener detalles de las órdenes de compra, incluyendo la ID de la orden, el nombre del proveedor,
--el nombre del producto y la cantidad ordenada.
Select *
From
	Purchasing.PurchaseOrderDetail pod
	where PurchaseOrderID =7

Select *
From
	Purchasing.PurchaseOrderHeader
	where PurchaseOrderID =7


SELECT 
    poh.PurchaseOrderID,
    v.Name AS VendorName,
    p.Name AS ProductName,
    pod.OrderQty
FROM 
    Purchasing.PurchaseOrderDetail pod
JOIN 
    Purchasing.PurchaseOrderHeader poh ON pod.PurchaseOrderID = poh.PurchaseOrderID
JOIN 
    Purchasing.Vendor v ON poh.VendorID = v.BusinessEntityID
JOIN 
    Production.Product p ON pod.ProductID = p.ProductID
ORDER BY 
    poh.PurchaseOrderID;