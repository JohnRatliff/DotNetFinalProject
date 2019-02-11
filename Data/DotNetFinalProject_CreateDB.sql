-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -- ---------------------------------------------------
-- Schema DotNetFinalProject
-- -- ---------------------------------------------------
DROP SCHEMA IF EXISTS DotNetFinalProject;

-- -- ---------------------------------------------------
-- Schema DotNetFinalProject
-- -- ---------------------------------------------------
CREATE SCHEMA IF NOT EXISTS DotNetFinalProject DEFAULT CHARACTER SET utf8;
USE DotNetFinalProject;

-- -- ---------------------------------------------------
-- Table DotNetFinalProject.users
-- -- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.users;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.users (
  UserSk INT NOT NULL AUTO_INCREMENT,
  Email VARCHAR(200) NOT NULL,
  Password VARCHAR(400) NOT NULL,
  FirstName VARCHAR(40) NOT NULL,
  LastName VARCHAR(40) NOT NULL,
  Description TEXT NULL,
  UserLevel INT NOT NULL DEFAULT 2,  -- (1=Admin, 2=Normal)
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (UserSk),
  UNIQUE INDEX Users_Email_UNIQUE (Email ASC));


-- ---------------------------------------------------
-- Table DotNetFinalProject.paymentterms
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.paymentterms ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.paymentterms (
  PaymentTermSk INT NOT NULL AUTO_INCREMENT,
  Name VARCHAR(80) NOT NULL,
  DiscountDays INT NOT NULL DEFAULT 0,
  DueDays INT NOT NULL DEFAULT 0,
  DiscountRate DECIMAL(5,2) NOT NULL DEFAULT 0,
  DiscountDateOption INT NOT NULL DEFAULT 1, -- 1=Order Date, 2=Invoice Date
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (PaymentTermSk),
  UNIQUE INDEX PaymentTerms_Name_UNIQUE (Name ASC));


-- ---------------------------------------------------
-- Table DotNetFinalProject.contacts
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.contacts ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.contacts (
  ContactSk INT NOT NULL AUTO_INCREMENT,
  Name VARCHAR (80) NOT NULL,
  Email VARCHAR(200) NOT NULL,
  Phone VARCHAR(20) NULL,
  AddressLine1 VARCHAR(100) NOT NULL,
  AddressLine2 VARCHAR(100) NULL,
  City VARCHAR(40) NOT NULL,
  State VARCHAR(40) NOT NULL,
  Country VARCHAR(40) NOT NULL,
  Zip VARCHAR(20) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (ContactSk));


-- ---------------------------------------------------
-- Table DotNetFinalProject.uomtypes
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.uomtypes ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.uomtypes (
  UomTypeSk INT NOT NULL AUTO_INCREMENT,
  UomTypeName VARCHAR (10) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (UomTypeSk),
  UNIQUE INDEX uomtypes_UomTypeName_UNIQUE (UomTypeName ASC));


-- ---------------------------------------------------
-- Table DotNetFinalProject.uoms
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.uoms ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.uoms (
  UomSk INT NOT NULL AUTO_INCREMENT,
  UomTypeSk INT NOT NULL, -- 1=Unit, 2=Weight, 3=Volume, 4=Length, 5=Time
  UomId VARCHAR (10) NOT NULL,
  UomName VARCHAR (20) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (UomSk),
  UNIQUE INDEX uoms_UomId_UNIQUE (UomId ASC),
  UNIQUE INDEX uoms_UomName_UNIQUE (UomName ASC),
  INDEX fk_uoms_UomTypeSk_idx (UomTypeSk ASC),
  CONSTRAINT fk_uoms_uomtypes
    FOREIGN KEY (UomTypeSk)
    REFERENCES DotNetFinalProject.uomtypes (UomTypeSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- ---------------------------------------------------
-- Table DotNetFinalProject.uomconversions
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.uomconversions ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.uomconversions (
  UomConversionSk INT NOT NULL AUTO_INCREMENT,
  UomTypeSk INT NOT NULL, -- 1=Unit, 2=Weight, 3=Volume, 4=Length, 5=Time
  ConvertFromUomSk INT NOT NULL,
  ConvertToUomSk INT NOT NULL,
  ConversionFactor DECIMAL(18,9) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (UomConversionSk),
  UNIQUE INDEX uomconversions_ConvertFromPXUomSk_ConvertToUomSk_UNIQUE (ConvertFromUomSk, ConvertToUomSk ASC),
  INDEX fk_uomconversions_UomTypeSK_idx (UomTypeSK ASC),
  INDEX fk_uomconversions_ConvertFromUomSK_idx (ConvertFromUomSk ASC),
  INDEX fk_uomconversions_ConvertToUomSK_idx (ConvertToUomSk ASC),
  CONSTRAINT fk_uomconversions_uomtypes
    FOREIGN KEY (UomTypeSk)
    REFERENCES DotNetFinalProject.uomtypes (UomTypeSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_uomconversions_uoms_ConvertFromUomSK
    FOREIGN KEY (ConvertFromUomSk)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT fk_uomconversions_uoms_ConvertToUomSK
    FOREIGN KEY (ConvertToUomSk)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


-- ---------------------------------------------------
-- Table DotNetFinalProject.products
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.products ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.products (
  ProductSk INT NOT NULL AUTO_INCREMENT,
  ProductId INT NOT NULL,
  ProductName VARCHAR (80) NOT NULL,
  UPC_GTIN NVARCHAR (40) NULL,
  UnitUomSK INT NOT NULL,
  WeightUomSK INT NOT NULL,
  ShipWeightUomSk INT NOT NULL,
  IsWeightPerUnitUniform BIT NOT NULL DEFAULT 0,
  WeightPerUnit DECIMAL (11,2) NOT NULL DEFAULT 0,
  TareWeight DECIMAL (11,2) NOT NULL DEFAULT 0,
  BestBeforeDays INT NOT NULL DEFAULT 0,
  PullByDays INT NOT NULL DEFAULT 0,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (ProductSk),
  UNIQUE INDEX products_ProductId_UNIQUE (ProductId ASC),
  INDEX fk_products_UnitUomSK_idx (UnitUomSK ASC),
  INDEX fk_products_WeightUomSK_idx (WeightUomSk ASC),
  INDEX fk_products_ShipWeightUomSK_idx (ShipWeightUomSk ASC),
  CONSTRAINT fk_products_uoms_UnitUomSK
    FOREIGN KEY (UnitUomSK)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_products_uoms_WeightUomSK
    FOREIGN KEY (WeightUomSK)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_products_uoms_ShipWeightUomSK
    FOREIGN KEY (ShipWeightUomSK)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ---------------------------------------------------
-- Table DotNetFinalProject.pricelists
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.pricelists ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.pricelists (
  PriceListSk INT NOT NULL AUTO_INCREMENT,
  PriceListName VARCHAR (40) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (PriceListSk),
  UNIQUE INDEX pricelists_PriceListName_UNIQUE (PriceListName ASC));


-- ---------------------------------------------------
-- Table DotNetFinalProject.pricelistproducts
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.pricelistproducts ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.pricelistproducts (
  PriceListProductSk INT NOT NULL AUTO_INCREMENT,
  PriceListSk INT NOT NULL,
  ProductSk INT NOT NULL,
  PriceByOption INT NOT NULL DEFAULT 1, -- (1=Units, 2=Weight)
  EffectiveDate DATE NOT NULL,
  Price DECIMAL (18,4) NOT NULL DEFAULT 0,
  PriceUomSk INT NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (PriceListProductSk),
  UNIQUE INDEX pricelistproducts_PriceListSk_ProductSk_PriceUomSK_UNIQUE (PriceListSk, ProductSk, PriceUomSk ASC),
  INDEX fk_pricelistproducts_PriceListSK_idx (PriceListSk ASC),
  INDEX fk_pricelistproducts_ProductSK_idx (ProductSk ASC),
  INDEX fk_pricelistproducts_PriceUomSK_idx (PriceUomSk ASC),
  CONSTRAINT fk_pricelistproducts_pricelists
    FOREIGN KEY (PriceListSk)
    REFERENCES DotNetFinalProject.pricelists (PriceListSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT fk_pricelistproducts_products
    FOREIGN KEY (ProductSk)
    REFERENCES DotNetFinalProject.products (ProductSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT fk_pricelistproducts_uoms
    FOREIGN KEY (PriceUomSk)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ---------------------------------------------------
-- Table DotNetFinalProject.orders
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.orders ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.orders (
  OrderSk INT NOT NULL AUTO_INCREMENT,
  OrderId INT  NOT NULL,
  Ordered DATETIME NOT NULL,
  RequestedDeliveryDate DATE NULL,
  AccountSk INT NOT NULL,
  PaymentTermSk INT NULL,
  PriceListSk INT NULL,
  IsSoldBillShipToSame BIT NOT NULL DEFAULT 0,
  SoldToContactSk INT NOT NULL,
  BillToContactSk INT NOT NULL,
  ShipToContactSk INT NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (OrderSk),
  UNIQUE INDEX orders_OrderId_UNIQUE (orderId ASC),
  INDEX fk_orders_AccountSk_idx (AccountSk ASC),
  INDEX fk_orders_PaymentTermSK_idx (PaymentTermSk ASC),
  INDEX fk_orders_PriceListSK_idx (PriceListSk ASC),
  INDEX fk_orders_SoldToContactSk_idx (SoldToContactSk ASC),
  INDEX fk_orders_BillToContactSk_idx (BillToContactSk ASC),
  INDEX fk_orders_ShipToContactSk_idx (ShipToContactSk ASC),
  CONSTRAINT fk_orders_accounts
    FOREIGN KEY (AccountSk)
    REFERENCES DotNetFinalProject.accounts (AccountSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_orders_paymentterms
    FOREIGN KEY (PaymentTermSk)
    REFERENCES DotNetFinalProject.paymentterms (PaymentTermSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_orders_pricelists
    FOREIGN KEY (PriceListSk)
    REFERENCES DotNetFinalProject.pricelists (PriceListSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_orders_contacts_SoldToContactSk
    FOREIGN KEY (SoldToContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_orders_contacts_BillToContactSk
    FOREIGN KEY (BillToContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_orders_contacts_ShipToContactSk
    FOREIGN KEY (ShipToContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



-- ---------------------------------------------------
-- Table DotNetFinalProject.orderproducts
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.orderproducts ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.orderproducts (
  OrderProductSk INT NOT NULL AUTO_INCREMENT,
  OrderSk INT NOT NULL,
  ProductSk INT NOT NULL,
  Description VARCHAR (80) NULL,
  IsTaxable BIT NOT NULL DEFAULT 1,
  Price DECIMAL (18,4) NOT NULL DEFAULT 0,
  PriceUomSk INT NOT NULL,
  TaxRate DECIMAL (5,2) NOT NULL DEFAULT 0,
  Units INT NOT NULL DEFAULT 0,
  Weight DECIMAL (11,2) NOT NULL DEFAULT 0,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (OrderProductSk),
  INDEX fk_orderproducts_orderSk_idx (OrderSk ASC),
  INDEX fk_orderproducts_ProductSk_idx (ProductSk ASC),
  INDEX fk_orderproducts_PriceUomSk_idx (PriceUomSk ASC),
  CONSTRAINT fk_orderproducts_orders
    FOREIGN KEY (OrderSk)
    REFERENCES DotNetFinalProject.orders (OrderSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT fk_orderproducts_products
    FOREIGN KEY (ProductSk)
    REFERENCES DotNetFinalProject.products (ProductSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_orderproducts_uoms_PriceUomSk
    FOREIGN KEY (PriceUomSk)
    REFERENCES DotNetFinalProject.uoms (UomSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ---------------------------------------------------
-- Table DotNetFinalProject.accounts
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.accounts ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.accounts (
  AccountSk INT NOT NULL AUTO_INCREMENT,
  AccountId INT NOT NULL,
  AccountName VARCHAR (80) NOT NULL,
  PaymentTermSk INT NOT NULL,
  PriceListSk INT NOT NULL,
  IsSoldBillShipToSame BIT NOT NULL DEFAULT 0,
  SoldToContactSk INT NOT NULL,
  BillToContactSk INT NOT NULL,
  ShipToContactSk INT NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (AccountSk),
  UNIQUE INDEX accounts_AccountId_UNIQUE (AccountId ASC),
  UNIQUE INDEX accounts_AccountName_UNIQUE (AccountName ASC),
  INDEX fk_accounts_PriceListSk_idx (PriceListSk ASC),
  INDEX fk_accounts_SoldToContactSk_idx (SoldToContactSk ASC),
  INDEX fk_accounts_BillToContactSk_idx (BillToContactSk ASC),
  INDEX fk_accounts_ShipToContactSk_idx (ShipToContactSk ASC),
  CONSTRAINT fk_accounts_pricelists
    FOREIGN KEY (PriceListSk)
    REFERENCES DotNetFinalProject.pricelists (PriceListSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_accounts_contacts_SoldToContactSk
    FOREIGN KEY (SoldToContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_accounts_contacts_BillToContactSk
    FOREIGN KEY (BillToContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_accounts_contacts_ShipToContactSk
    FOREIGN KEY (ShipToContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ---------------------------------------------------
-- Table DotNetFinalProject.accountcontacts
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.accountcontacts ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.accountcontacts (
  AccountContactSK INT NOT NULL AUTO_INCREMENT,
  AccountSk INT NOT NULL,
  ContactType INT NOT NULL DEFAULT 1, -- 1=SoldTo, 2=BillTo, 3=ShipTo
  ContactSK INT NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (AccountContactSk),
  INDEX fk_accountcontacts_AccountSk_idx (AccountSk ASC),
  INDEX fk_accountcontacts_ContactSk_idx (ContactSk ASC),
  CONSTRAINT fk_accountcontacts_accounts
    FOREIGN KEY (AccountSk)
    REFERENCES DotNetFinalProject.accounts (AccountSK)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_accountcontacts_contacts
    FOREIGN KEY (ContactSk)
    REFERENCES DotNetFinalProject.contacts (ContactSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ---------------------------------------------------
-- Table DotNetFinalProject.settings
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.settings ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.settings (
  SettingSk INT NOT NULL AUTO_INCREMENT,
  LastAccountId INT NULL,
  LastOrderId INT NULL,
  LastProductId INT NULL,
  PriceListSk INT NULL,
  TaxRate DECIMAL (5,2) NOT NULL DEFAULT 0,
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (SettingSk),
  INDEX fk_settings_pricelist_idx (PriceListSk ASC),
  CONSTRAINT settings_pricelists
    FOREIGN KEY (PriceListSk)
    REFERENCES DotNetFinalProject.pricelists (PriceListSk)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- ---------------------------------------------------
-- Table DotNetFinalProject.usersettings
-- ---------------------------------------------------
DROP TABLE IF EXISTS DotNetFinalProject.usersettings ;

CREATE TABLE IF NOT EXISTS DotNetFinalProject.usersettings (
  UserSettingSk INT NOT NULL AUTO_INCREMENT,
  UserSk INT NULL,
  InitialView INT NOT NULL DEFAULT 1,    -- 1=Orders, 2=Accounts, 3=Products, 4=PaymentTerms, 5=PriceLists, 6=UomConversions
  CreatedAt DATETIME NOT NULL DEFAULT NOW(),
  UpdatedAt DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (UserSettingSk),
  INDEX fk_settings_user_idx (UserSk ASC),
  CONSTRAINT fk_settings_users
    FOREIGN KEY (UserSk)
    REFERENCES DotNetFinalProject.users (UserSk)
    ON DELETE CASCADE
    ON UPDATE CASCADE);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
