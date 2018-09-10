
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
	[CustomerCode] [dbo].[TY_NAME] NULL,				--Vevõkód, CustomerCode
	[CustomerOrderNumber] [dbo].[TY_NAME] NULL,			--Vevõ rendelés száma, Customer Order date
	[CustomerOrderDate] [dbo].[TY_DATETIME] NULL,		--Vevõ rendelés dátuma, Vevõ rendelés dátuma
	[ShippingDate]  [dbo].[TY_DATETIME] NULL,			--Szállítási dátum, Shipping date
	[WarehouseCode] [dbo].[TY_NAME] NULL,				--Raktár kód,Warehouse code
	[TotalGrossWeightOfOrder] [dbo].[TY_NVALUE] NULL,	--Rendelés Össz bruttó súly, Total gross weight of order
	[NumberOfPalletForDel] [dbo].[TY_NVALUE] NULL,		--Szállítandó raklapok száma, Number of pallet for del.
	[ShippAddressID] [dbo].[TY_NAME] NULL,				--Szállítási Cím ID, Shipp.Address ID
	[ShippAddressCompanyName] [dbo].[TY_NAME] NULL,		--Szállítási cím cégnév, ShippAddre- Company name
	[ShippAddressZipCode] [dbo].[TY_NAME]  NULL,		--Szállítási cím IRSZ,ShippAddress - Zip Code 
	[ShippingAddressCity] [dbo].[TY_TEXT] NULL,			--Szállítási cím város,Shipping address – City
	[ShippingAddressStreetAndNumber] [dbo].[TY_TEXT] NULL,	--Szállítási cím,Shipping address - street and number
	[Note] [dbo].[TY_TEXT] NULL,						--Megjegyzés, NOTE
	[RowNumber] [dbo].[TY_NVALUE] NULL,					--Sor száma (rendelésen belül),ROW NUMBER
	[ProductCode] [dbo].[TY_NAME] NULL,					--Termékkód,Product Code
	[U_M]  [dbo].[TY_NAME] NULL,						--Mennyiségi egység,U.M.
	[ProdDescription] [dbo].[TY_TEXT] NULL,				--Termék megnevezés, Prod Description
	[ConfOrderQty] [dbo].[TY_NVALUE] NULL,				--Rendelt mennyiség bruttó súly,Conf.Order Qty ROW 
	[ConfPlannedQty] [dbo].[TY_NVALUE] NULL,			--!!!szállítandó mennyiség (ret.val.),Conf. Planned Qty (Row)
	[PalletOrderQty] [dbo].[TY_NVALUE]  NULL,			--Rendelt mennyiség raklap,Pallet Order Qty (Row)
	[PalletPlannedQty] [dbo].[TY_NVALUE] NULL,			--Szállítandó mennyiség raklap,Pallet Planned Qty (Row)
	[PalletBulkQty] [dbo].[TY_NVALUE]  NULL,			--,Pallet ‘Bulk’ qty (Row)
	[GrossWeightPlanned] [dbo].[TY_NVALUE]  NULL,		--!!!Szállítandó bruttó súly,Gross Weight Planned (Row)
	[ADR] [dbo].[TY_BVALUE]  NULL,						--ADR,ADR
	[ADRMultiplier] [dbo].[TY_NVALUE]  NULL,			--ADR szorzó,ADR Multiplier
	[ADRLimitedQuantity] [dbo].[TY_NVALUE]  NULL,		--ADR köteles mennyiség, limited_quantity
	[Freeze] [dbo].[TY_BVALUE]  NULL,					--Fagyérzékeny, Freeze
	[Melt] [dbo].[TY_BVALUE] NULL,						--Hõérzékeny,MELT
	[UV] [dbo].[TY_BVALUE] NULL,						--UV érzékeny,UV
	[Bordero] [dbo].[TY_NAME] NULL,						--!!!Fuvarszám,BORDERO
	[Carrier ] [dbo].[TY_NAME] NULL,					--!!!Fuvaros,CARRIER 
	[VehicleType] [dbo].[TY_NAME] NULL,				--!!!Szállítóeszköz típus, VEHICLE TYPE
	[KM] [dbo].[TY_NVALUE]  NULL,						--!!!Teljes Fuvar km, KM
	[Forfait] [dbo].[TY_NVALUE] NULL,					--!!!Teljes Fuvar Útdíj, FORFAIT
	[Currency] [dbo].[TY_NAME] NULL						--!!!Útdíj pénznem (HUF)CURRENCY
) ON [PRIMARY]

GO


