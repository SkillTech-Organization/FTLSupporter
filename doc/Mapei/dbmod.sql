
CREATE TYPE [dbo].[TY_TEXT] FROM [nvarchar](max) NULL
GO

/****** Object:  Table [dbo].[MPO_MPORDER]    Script Date: 2018. 09. 06. 6:03:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPO_MPORDER](
	[ID] [dbo].[TY_ID] IDENTITY(1,1) NOT NULL,
	[CompanyCode] [dbo].[TY_NAME] NULL,					--,Company code
	[CustomerCode] [dbo].[TY_NAME] NULL,				--Vev�k�d, CustomerCode
	[CustomerOrderNumber] [dbo].[TY_NAME] NULL,			--Vev� rendel�s sz�ma, Customer Order date
	[CustomerOrderDate] [dbo].[TY_DATETIME] NULL,		--Vev� rendel�s d�tuma, Vev� rendel�s d�tuma
	[ShippingDate]  [dbo].[TY_DATETIME] NULL,			--Sz�ll�t�si d�tum, Shipping date
	[WarehouseCode] [dbo].[TY_NAME] NULL,				--Rakt�r k�d,Warehouse code
	[TotalGrossWeightOfOrder] [dbo].[TY_NVALUE] NULL,	--Rendel�s �ssz brutt� s�ly, Total gross weight of order
	[NumberOfPalletForDel] [dbo].[TY_NVALUE] NULL,		--Sz�ll�tand� raklapok sz�ma, Number of pallet for del.
	[ShippAddressID] [dbo].[TY_NAME] NULL,				--Sz�ll�t�si C�m ID, Shipp.Address ID
	[ShippAddressCompanyName] [dbo].[TY_NAME] NULL,		--Sz�ll�t�si c�m c�gn�v, ShippAddre- Company name
	[ShippAddressZipCode] [dbo].[TY_NAME]  NULL,		--Sz�ll�t�si c�m IRSZ,ShippAddress - Zip Code 
	[ShippingAddressCity] [dbo].[TY_TEXT] NULL,			--Sz�ll�t�si c�m v�ros,Shipping address � City
	[ShippingAddressStreetAndNumber] [dbo].[TY_TEXT] NULL,	--Sz�ll�t�si c�m,Shipping address - street and number
	[Note] [dbo].[TY_TEXT] NULL,						--Megjegyz�s, NOTE
	[RowNumber] [dbo].[TY_NVALUE] NULL,					--Sor sz�ma (rendel�sen bel�l),ROW NUMBER
	[ProductCode] [dbo].[TY_NAME] NULL,					--Term�kk�d,Product Code
	[U_M]  [dbo].[TY_NAME] NULL,						--Mennyis�gi egys�g,U.M.
	[ProdDescription] [dbo].[TY_TEXT] NULL,				--Term�k megnevez�s, Prod Description
	[ConfOrderQty] [dbo].[TY_NVALUE] NULL,				--Rendelt mennyis�g brutt� s�ly,Conf.Order Qty ROW 
	[ConfPlannedQty] [dbo].[TY_NVALUE] NULL,			--!!!sz�ll�tand� mennyis�g (ret.val.),Conf. Planned Qty (Row)
	[PalletOrderQty] [dbo].[TY_NVALUE]  NULL,			--Rendelt mennyis�g raklap,Pallet Order Qty (Row)
	[PalletPlannedQty] [dbo].[TY_NVALUE] NULL,			--Sz�ll�tand� mennyis�g raklap,Pallet Planned Qty (Row)
	[PalletBulkQty] [dbo].[TY_NVALUE]  NULL,			--,Pallet �Bulk� qty (Row)
	[GrossWeightPlanned] [dbo].[TY_NVALUE]  NULL,		--!!!Sz�ll�tand� brutt� s�ly,Gross Weight Planned (Row)
	[ADR] [dbo].[TY_BVALUE]  NULL,						--ADR,ADR
	[ADRMultiplier] [dbo].[TY_NVALUE]  NULL,			--ADR szorz�,ADR Multiplier
	[ADRLimitedQuantity] [dbo].[TY_NVALUE]  NULL,		--ADR k�teles mennyis�g, limited_quantity
	[Freeze] [dbo].[TY_BVALUE]  NULL,					--Fagy�rz�keny, Freeze
	[Melt] [dbo].[TY_BVALUE] NULL,						--H��rz�keny,MELT
	[UV] [dbo].[TY_BVALUE] NULL,						--UV �rz�keny,UV
	[Bordero] [dbo].[TY_NAME] NULL,						--!!!Fuvarsz�m,BORDERO
	[Carrier ] [dbo].[TY_NAME] NULL,					--!!!Fuvaros,CARRIER 
	[VehicleType] [dbo].[TY_NAME] NULL,				--!!!Sz�ll�t�eszk�z t�pus, VEHICLE TYPE
	[KM] [dbo].[TY_NVALUE]  NULL,						--!!!Teljes Fuvar km, KM
	[Forfait] [dbo].[TY_NVALUE] NULL,					--!!!Teljes Fuvar �td�j, FORFAIT
	[Currency] [dbo].[TY_NAME] NULL						--!!!�td�j p�nznem (HUF)CURRENCY
) ON [PRIMARY]

GO


