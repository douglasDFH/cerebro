USE [DBTiendaBlazor]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 29/4/2025 23:42:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DetalleCarrito]') AND type in (N'U'))
DROP TABLE [dbo].[Carrito]
GO
