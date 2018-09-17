
CREATE TYPE [dbo].[TY_TEXT] FROM [nvarchar](max) NULL
GO

/****** Object:  Table [dbo].[MPO_MPORDER]    Script Date: 2018. 09. 06. 6:03:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
drop table [MPO_MPORDER]
go

CREATE TABLE [dbo].[MPO_MPORDER](
	[ID] [dbo].[TY_ID] IDENTITY(1,1) NOT NULL,
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
	[ConfPlannedQtyX] [dbo].[TY_NVALUE] NULL,
	[PalletOrderQty] [dbo].[TY_NVALUE] NULL,
	[PalletPlannedQty] [dbo].[TY_NVALUE] NULL,
	[PalletBulkQty] [dbo].[TY_NVALUE] NULL,
	[GrossWeightPlanned] [dbo].[TY_NVALUE] NULL,
	[GrossWeightPlannedX] [dbo].[TY_NVALUE] NULL,
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
 CONSTRAINT [PK_MPO_MPORDER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX [IX_MPO_ShippingDate] ON [dbo].[MPO_MPORDER]
(
	[ShippingDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO 
CREATE NONCLUSTERED INDEX [IX_MPO_CustProd] ON [dbo].[MPO_MPORDER]
(
	[CustomerCode] ASC,
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



