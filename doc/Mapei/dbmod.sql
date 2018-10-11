if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'ORD_ORDER' and  syscolumns.name = 'ORD_ADRPOINTS') begin 
   ALTER TABLE ORD_ORDER ADD ORD_ADRPOINTS TY_NVALUE default 0 
End 
GO

IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'TY_TEXT') begin
   CREATE TYPE [dbo].[TY_TEXT] FROM [nvarchar](max) NULL
End
GO

/****** Object:  Table [dbo].[MPO_MPORDER]    Script Date: 2018. 09. 06. 6:03:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MPO_MPORDER]') AND type in (N'U')) BEGIN
CREATE TABLE [dbo].[MPO_MPORDER](
	[ID] [dbo].[TY_ID] IDENTITY(1,1) NOT NULL,
	[SentToCT] [dbo].[TY_BVALUE] NULL,
	[CompanyCode] [dbo].[TY_NAME] NULL,
	[CustomerCode] [dbo].[TY_NAME] NULL,
	[CustomerOrderNumber] [dbo].[TY_NAME] NULL,
	[CustomerOrderDate] [dbo].[TY_DATETIME] NULL,
	[ShippingDate] [dbo].[TY_DATETIME] NULL,
	[WarehouseCode] [dbo].[TY_NAME] NULL,
	[TotalGrossWeightOfOrder] [dbo].[TY_NVALUE] NULL,
	[NumberOfPalletForDel] [dbo].[TY_NVALUE] NULL,
	[ShippAddressID] [dbo].[TY_NAME] NULL,
	[ShippAddressCompanyName] [dbo].[TY_NAME] NULL,
	[ShippAddressZipCode] [dbo].[TY_NAME] NULL,
	[ShippingAddressCity] [dbo].[TY_TEXT] NULL,
	[ShippingAddressStreetAndNumber] [dbo].[TY_TEXT] NULL,
	[Note] [dbo].[TY_TEXT] NULL,
	[RowNumber] [dbo].[TY_NVALUE] NULL,
	[ProductCode] [dbo].[TY_NAME] NULL,
	[U_M] [dbo].[TY_NAME] NULL,
	[ProdDescription] [dbo].[TY_TEXT] NULL,
	[ConfOrderQty] [dbo].[TY_NVALUE] NULL,
	[ConfPlannedQty] [dbo].[TY_NVALUE] NULL,
	[NetWeight] [dbo].[TY_NVALUE] NULL,
	[PalletPlannedQty] [dbo].[TY_NVALUE] NULL,
	[PalletBulkQty] [dbo].[TY_NVALUE] NULL,
	[GrossWeightPlanned] [dbo].[TY_NVALUE] NULL,
	[ADR] [dbo].[TY_BVALUE] NULL,
	[ADRMultiplier] [dbo].[TY_NVALUE] NULL,
	[ADRLimitedQuantity] [dbo].[TY_NVALUE] NULL,
	[Freeze] [dbo].[TY_BVALUE] NULL,
	[Melt] [dbo].[TY_BVALUE] NULL,
	[UV] [dbo].[TY_BVALUE] NULL,
	[Bordero] [dbo].[TY_NAME] NULL,
	[Carrier] [dbo].[TY_NAME] NULL,
	[VehicleType] [dbo].[TY_NAME] NULL,
	[KM] [dbo].[TY_NVALUE] NULL,
	[Forfait] [dbo].[TY_NVALUE] NULL,
	[Currency] [dbo].[TY_NAME] NULL,
	[ADRMultiplierX] [dbo].[TY_NVALUE] NULL,
 CONSTRAINT [PK_MPO_MPORDER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_MPO_ShippingDate] ON [dbo].[MPO_MPORDER]
(
	[ShippingDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
CREATE NONCLUSTERED INDEX [IX_MPO_CustProd] ON [dbo].[MPO_MPORDER]
(
	[CustomerCode] ASC,
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
end
GO
if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'UnitWeight') begin 
   ALTER TABLE MPO_MPORDER ADD UnitWeight TY_NVALUE default 0 
End 


if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'NetWeight') begin 
   ALTER TABLE MPO_MPORDER ADD NetWeight TY_NVALUE default 0 
End 

if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'GrossWeightPlannedX') begin 
   ALTER TABLE MPO_MPORDER ADD GrossWeightPlannedX TY_NVALUE default 0 
End 
     
if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'CSVFileName') begin 
   ALTER TABLE MPO_MPORDER ADD CSVFileName TY_COMMENT default 0 
End 

if not exists (select * from sysindexes  where id=object_id('MPO_MPORDER') and name='IX_MPO_CSVFileName') begin
	CREATE NONCLUSTERED INDEX [IX_MPO_CSVFileName] ON [dbo].[MPO_MPORDER]
	(
		[CSVFileName] ASC,
		[CustomerOrderNumber] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
end
	    
if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'ShippingDateX') begin 
   ALTER TABLE MPO_MPORDER ADD ShippingDateX TY_DATETIME  
End 	

if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'NumberOfPalletForDelX') begin 
   ALTER TABLE MPO_MPORDER ADD NumberOfPalletForDelX TY_NVALUE default 0 
End 	 

if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'PalletPlannedQtyX') begin 
   ALTER TABLE MPO_MPORDER ADD PalletPlannedQtyX TY_NVALUE default 0 
End 	

if not exists(select syscolumns.id from syscolumns join sysobjects on  syscolumns.ID = sysobjects.ID  where  sysobjects.name = 'MPO_MPORDER' and  syscolumns.name = 'PalletBulkQtyX') begin 
   ALTER TABLE MPO_MPORDER ADD PalletBulkQtyX TY_NVALUE default 0 
End 	
truncate table MPO_MPORDER
truncate table MPP_MAPPLANPAR
