
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2019 08:31:08
-- Generated from EDMX file: C:\Users\calixto.gelarzo\Documents\Visual Studio 2013\Projects\HaviOnlineOrdering2018 (SUBWAY)\HaviOnlineOrdering2018\HaviEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SUBWAY];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tbl_Cart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Cart];
GO
IF OBJECT_ID(N'[dbo].[tbl_mcc_sub_cat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_mcc_sub_cat];
GO
IF OBJECT_ID(N'[dbo].[tbl_store]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_store];
GO
IF OBJECT_ID(N'[dbo].[tbl_sup_leadtime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_sup_leadtime];
GO
IF OBJECT_ID(N'[dbo].[tbl_Transaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Transaction];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[tbl_Category]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[tbl_Category];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[tbl_Customer]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[tbl_Customer];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[tbl_item_mstr]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[tbl_item_mstr];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[tbl_items]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[tbl_items];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[tbl_items_sl]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[tbl_items_sl];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[vw_AllowedItemsOnStore]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[vw_AllowedItemsOnStore];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[vw_Subcat]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[vw_Subcat];
GO
IF OBJECT_ID(N'[HAVIModelStoreContainer].[vw_SupplierLeadtime]', 'U') IS NOT NULL
    DROP TABLE [HAVIModelStoreContainer].[vw_SupplierLeadtime];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_Cart'
CREATE TABLE [dbo].[tbl_Cart] (
    [cart_id] char(15)  NOT NULL,
    [wrin] varchar(12)  NOT NULL,
    [price] decimal(18,0)  NOT NULL,
    [qty] int  NOT NULL,
    [category_id] int  NOT NULL,
    [delivered_qty] int  NOT NULL,
    [downloaded] char(1)  NULL,
    [po_stamp] datetime  NOT NULL,
    [datedownloaded] datetime  NULL,
    [TextFileName] varchar(100)  NULL,
    [CreatedDateTime] datetime  NULL
);
GO

-- Creating table 'tbl_store'
CREATE TABLE [dbo].[tbl_store] (
    [owner_id] int  NOT NULL,
    [store_no] char(8)  NOT NULL,
    [store_name] varchar(50)  NOT NULL,
    [delday1] char(1)  NOT NULL,
    [delday2] char(1)  NOT NULL,
    [delday3] char(1)  NOT NULL,
    [delday4] char(1)  NOT NULL,
    [delday5] char(1)  NOT NULL,
    [delday6] char(1)  NOT NULL,
    [delday7] char(1)  NOT NULL,
    [deltime1] char(1)  NULL,
    [deltime2] char(1)  NULL,
    [deltime3] char(1)  NULL,
    [deltime4] char(1)  NULL,
    [deltime5] char(1)  NULL,
    [deltime6] char(1)  NULL,
    [deltime7] char(1)  NULL,
    [toggle_prc] char(1)  NULL,
    [bill_to] char(10)  NULL,
    [ship_to] char(10)  NULL,
    [cust_type] char(1)  NULL,
    [corp_no] char(15)  NULL,
    [area_code] char(5)  NULL,
    [gl_acct_code] char(15)  NULL,
    [charge_service] char(3)  NULL,
    [status] char(10)  NULL,
    [updatedby] char(10)  NULL,
    [updatedate] datetime  NULL,
    [cia_no] int  NULL,
    [dwnldtime] char(2)  NULL,
    [pc_code] varchar(10)  NULL,
    [group_id] varchar(10)  NULL,
    [PortalID] varchar(10)  NULL,
    [zcode] char(3)  NULL,
    [WUtility_StoreCatID] int  NULL,
    [Wutility_UpdateBy] int  NULL,
    [Wutility_UpdateDateTime] datetime  NULL
);
GO

-- Creating table 'tbl_Category'
CREATE TABLE [dbo].[tbl_Category] (
    [category_id] nvarchar(10)  NOT NULL,
    [category_name] varchar(30)  NOT NULL,
    [category_code] char(2)  NOT NULL,
    [owner_id] int  NOT NULL,
    [is_direct] bit  NULL,
    [ctrs] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'tbl_Customer'
CREATE TABLE [dbo].[tbl_Customer] (
    [custid] int IDENTITY(1,1) NOT NULL,
    [login_email] varchar(50)  NOT NULL,
    [password] char(15)  NOT NULL,
    [first_name] varchar(50)  NOT NULL,
    [middle_name] varchar(30)  NULL,
    [last_name] varchar(30)  NULL,
    [phoneno] nvarchar(100)  NULL,
    [faxno] varchar(30)  NULL,
    [address] nvarchar(200)  NULL,
    [owner_id] int  NULL,
    [access_type] varchar(30)  NULL,
    [store_no] char(10)  NULL,
    [cust_rights] varchar(31)  NULL,
    [cstatus] char(1)  NULL,
    [dateopened] datetime  NULL,
    [lastaccess] int  NULL,
    [dateaccess] datetime  NULL,
    [toggle_prc] char(1)  NULL,
    [DwnldTime] char(1)  NULL,
    [cust_rights2] char(10)  NULL,
    [pc_code] char(10)  NULL
);
GO

-- Creating table 'tbl_item_mstr'
CREATE TABLE [dbo].[tbl_item_mstr] (
    [Wrin] char(15)  NOT NULL,
    [Item_desc] char(100)  NULL,
    [Entity_code] char(4)  NULL,
    [split_code] char(2)  NULL,
    [abc_class] char(2)  NULL,
    [uom] char(5)  NULL,
    [item_cat] char(5)  NULL,
    [item_type] char(5)  NULL,
    [item_source] char(5)  NULL,
    [shelf_life] char(10)  NULL,
    [Min_Stock_level] int  NULL,
    [item_stat] char(10)  NULL,
    [pia_no] int  NULL,
    [safety_level] char(10)  NULL,
    [gl_acct_code] char(10)  NULL,
    [item_sub_cat] char(10)  NULL,
    [english_desc] char(50)  NULL,
    [english_uom] char(10)  NULL,
    [ret_wrin] char(15)  NULL,
    [ret_desc] char(50)  NULL,
    [ret_case] char(10)  NULL,
    [charge_service] char(3)  NULL,
    [ret_deposit] decimal(18,0)  NULL,
    [vat_ind] char(3)  NULL,
    [case_del_unit] decimal(18,0)  NULL,
    [dep_wrin] char(15)  NULL,
    [charge_to] char(3)  NULL,
    [Effect_date] datetime  NULL,
    [msi_item] char(3)  NULL,
    [msm_cat] char(3)  NULL,
    [msm_subcat] char(10)  NULL,
    [Pur_UOM] char(10)  NULL,
    [dep_case] char(10)  NULL,
    [hs_code] char(10)  NULL,
    [whse_code] char(10)  NULL,
    [updated_by] char(10)  NULL,
    [update_date] datetime  NULL,
    [status] char(5)  NULL,
    [notes] char(200)  NULL,
    [case_pk] char(15)  NULL,
    [case_cu] float  NULL,
    [case_we] float  NULL
);
GO

-- Creating table 'tbl_items'
CREATE TABLE [dbo].[tbl_items] (
    [owner_id] int  NOT NULL,
    [category_id] int  NOT NULL,
    [wrin] char(15)  NOT NULL,
    [item_desc] char(100)  NOT NULL,
    [case_pk] char(15)  NULL,
    [case_cu] char(15)  NULL,
    [case_we] char(15)  NULL,
    [std_price] decimal(18,2)  NOT NULL,
    [qty] int  NULL,
    [sup_price] decimal(18,0)  NULL,
    [uom] char(10)  NULL,
    [multiplier] int  NULL,
    [WUtility_ItemCatID] int  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'tbl_items_sl'
CREATE TABLE [dbo].[tbl_items_sl] (
    [store_no] varchar(50)  NOT NULL,
    [wrin] varchar(50)  NOT NULL,
    [Modified_by] int  NULL
);
GO

-- Creating table 'vw_Subcat'
CREATE TABLE [dbo].[vw_Subcat] (
    [subcatid] varchar(10)  NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'vw_SupplierLeadtime'
CREATE TABLE [dbo].[vw_SupplierLeadtime] (
    [Subcatid] char(10)  NOT NULL,
    [Description] char(50)  NULL,
    [store_no] varchar(10)  NULL
);
GO

-- Creating table 'vw_AllowedItemsOnStore'
CREATE TABLE [dbo].[vw_AllowedItemsOnStore] (
    [store_no] char(8)  NULL,
    [wrin] char(15)  NOT NULL,
    [owner_id] int  NULL,
    [item_desc] char(100)  NOT NULL,
    [case_pk] char(15)  NULL,
    [case_cu] char(15)  NULL,
    [case_we] char(15)  NULL,
    [uom] char(10)  NULL,
    [std_price] decimal(18,2)  NOT NULL,
    [category_id] int  NOT NULL
);
GO

-- Creating table 'tbl_sup_leadtime'
CREATE TABLE [dbo].[tbl_sup_leadtime] (
    [store_no] varchar(10)  NOT NULL,
    [wsi] varchar(10)  NOT NULL,
    [wrin] varchar(10)  NOT NULL,
    [leadtime] int  NULL,
    [ltadd] int  NULL,
    [proctype] varchar(10)  NULL,
    [fFlat] bit  NULL,
    [type] char(2)  NULL,
    [accepted_delday] varchar(20)  NULL,
    [po_stamp] varchar(20)  NULL
);
GO

-- Creating table 'tbl_mcc_sub_cat'
CREATE TABLE [dbo].[tbl_mcc_sub_cat] (
    [catid] varchar(10)  NOT NULL,
    [subcatid] varchar(10)  NOT NULL,
    [wrin] varchar(10)  NOT NULL
);
GO

-- Creating table 'tbl_Transaction'
CREATE TABLE [dbo].[tbl_Transaction] (
    [Order_no] int IDENTITY(1,1) NOT NULL,
    [Custid] int  NOT NULL,
    [Store_no] char(8)  NOT NULL,
    [refpo] char(15)  NOT NULL,
    [Trans_type] char(1)  NOT NULL,
    [Trans_date] datetime  NOT NULL,
    [Delivery_date] datetime  NOT NULL,
    [Cart_id] char(15)  NOT NULL,
    [Total] decimal(19,4)  NOT NULL,
    [Trans_status] char(1)  NOT NULL,
    [owner_id] int  NOT NULL,
    [update_date] datetime  NULL,
    [careof] char(5)  NULL,
    [so_time] char(1)  NULL,
    [cc_stat] char(3)  NULL,
    [r_stat] char(1)  NULL,
    [freight_mode] nchar(5)  NULL,
    [WebUtilityID] int  NULL,
    [DateUnprocess] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [wrin], [po_stamp], [cart_id], [qty] in table 'tbl_Cart'
ALTER TABLE [dbo].[tbl_Cart]
ADD CONSTRAINT [PK_tbl_Cart]
    PRIMARY KEY CLUSTERED ([wrin], [po_stamp], [cart_id], [qty] ASC);
GO

-- Creating primary key on [store_no] in table 'tbl_store'
ALTER TABLE [dbo].[tbl_store]
ADD CONSTRAINT [PK_tbl_store]
    PRIMARY KEY CLUSTERED ([store_no] ASC);
GO

-- Creating primary key on [category_id], [category_name], [category_code], [owner_id], [ctrs] in table 'tbl_Category'
ALTER TABLE [dbo].[tbl_Category]
ADD CONSTRAINT [PK_tbl_Category]
    PRIMARY KEY CLUSTERED ([category_id], [category_name], [category_code], [owner_id], [ctrs] ASC);
GO

-- Creating primary key on [custid], [login_email], [password], [first_name] in table 'tbl_Customer'
ALTER TABLE [dbo].[tbl_Customer]
ADD CONSTRAINT [PK_tbl_Customer]
    PRIMARY KEY CLUSTERED ([custid], [login_email], [password], [first_name] ASC);
GO

-- Creating primary key on [Wrin] in table 'tbl_item_mstr'
ALTER TABLE [dbo].[tbl_item_mstr]
ADD CONSTRAINT [PK_tbl_item_mstr]
    PRIMARY KEY CLUSTERED ([Wrin] ASC);
GO

-- Creating primary key on [owner_id], [category_id], [wrin], [item_desc], [std_price], [CreatedDate] in table 'tbl_items'
ALTER TABLE [dbo].[tbl_items]
ADD CONSTRAINT [PK_tbl_items]
    PRIMARY KEY CLUSTERED ([owner_id], [category_id], [wrin], [item_desc], [std_price], [CreatedDate] ASC);
GO

-- Creating primary key on [store_no], [wrin] in table 'tbl_items_sl'
ALTER TABLE [dbo].[tbl_items_sl]
ADD CONSTRAINT [PK_tbl_items_sl]
    PRIMARY KEY CLUSTERED ([store_no], [wrin] ASC);
GO

-- Creating primary key on [subcatid] in table 'vw_Subcat'
ALTER TABLE [dbo].[vw_Subcat]
ADD CONSTRAINT [PK_vw_Subcat]
    PRIMARY KEY CLUSTERED ([subcatid] ASC);
GO

-- Creating primary key on [Subcatid] in table 'vw_SupplierLeadtime'
ALTER TABLE [dbo].[vw_SupplierLeadtime]
ADD CONSTRAINT [PK_vw_SupplierLeadtime]
    PRIMARY KEY CLUSTERED ([Subcatid] ASC);
GO

-- Creating primary key on [wrin], [item_desc], [std_price], [category_id] in table 'vw_AllowedItemsOnStore'
ALTER TABLE [dbo].[vw_AllowedItemsOnStore]
ADD CONSTRAINT [PK_vw_AllowedItemsOnStore]
    PRIMARY KEY CLUSTERED ([wrin], [item_desc], [std_price], [category_id] ASC);
GO

-- Creating primary key on [store_no], [wsi], [wrin] in table 'tbl_sup_leadtime'
ALTER TABLE [dbo].[tbl_sup_leadtime]
ADD CONSTRAINT [PK_tbl_sup_leadtime]
    PRIMARY KEY CLUSTERED ([store_no], [wsi], [wrin] ASC);
GO

-- Creating primary key on [catid], [wrin] in table 'tbl_mcc_sub_cat'
ALTER TABLE [dbo].[tbl_mcc_sub_cat]
ADD CONSTRAINT [PK_tbl_mcc_sub_cat]
    PRIMARY KEY CLUSTERED ([catid], [wrin] ASC);
GO

-- Creating primary key on [Store_no], [Trans_date] in table 'tbl_Transaction'
ALTER TABLE [dbo].[tbl_Transaction]
ADD CONSTRAINT [PK_tbl_Transaction]
    PRIMARY KEY CLUSTERED ([Store_no], [Trans_date] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------