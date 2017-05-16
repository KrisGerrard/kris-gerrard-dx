SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[LastName] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Password] [nchar](8) COLLATE Latin1_General_CI_AS NULL,
	[Username] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[EmployeeImage] [varbinary](max) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](20) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRoles](
	[EmployeeID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeRoles] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE addEmployeeToRole (@EmployeeID int, @RoleID int)
AS 
INSERT INTO EmployeeRoles VALUES (@EmployeeID, @RoleID)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE createEmployeePassword (@EmployeeID int, @Password nchar(10))
AS
UPDATE Employee SET Password = @Password WHERE EmployeeID = @EmployeeID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[FirstName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[LastName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE deleteCustomer (@CID int)
AS
DELETE Customer WHERE CustomerID = @CID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](15) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[AddressType] [int] NOT NULL,
	[Street] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Suburb] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[City] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[PostalCode] [nchar](10) COLLATE Latin1_General_CI_AS NULL,
	[Country] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAddress](
	[CustomerID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
 CONSTRAINT [PK_CustomerAddress] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE deleteCustomerAddress (@AddressID int)
AS
DELETE CustomerAddress WHERE AddressID = @AddressID
DELETE Address WHERE AddressID = @AddressID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactType](
	[ContactTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ContactType] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[ContactType] [int] NOT NULL,
	[Contact] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerContact](
	[CustomerID] [int] NOT NULL,
	[ContactID] [int] NOT NULL,
 CONSTRAINT [PK_CustomerContact] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE deleteCustomerContact(@ContactID int)
AS
DELETE CustomerContact WHERE ContactID = @ContactID
DELETE Contact WHERE ContactID = @ContactID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE deleteEmployee (@EmpID int)
AS 
DELETE Employee WHERE EmployeeID = @EmpID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAddress](
	[EmployeeID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeAddress] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE deleteEmployeeAddress (@AddressID int)
AS
DELETE EmployeeAddress WHERE AddressID = @AddressID
DELETE Address WHERE AddressID = @AddressID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeContact](
	[EmployeeID] [int] NOT NULL,
	[ContactID] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeContact] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE deleteEmployeeContact(@ContactID int)
AS
DELETE EmployeeContact WHERE ContactID = @ContactID
DELETE Contact WHERE ContactID = @ContactID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Complete] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE deleteOrder
	-- Add the parameters for the stored procedure here
(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from [Order] where OrderID=@id
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSubCategory](
	[SubCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[SubCategory] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_ProductSubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Retailer](
	[RetailerID] [int] IDENTITY(1,1) NOT NULL,
	[Retailer] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Retailer] PRIMARY KEY CLUSTERED 
(
	[RetailerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[RetailerID] [int] NOT NULL,
	[SubCategoryID] [int] NOT NULL,
	[ProductName] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[ProductDescription] [nvarchar](255) COLLATE Latin1_General_CI_AS NOT NULL,
	[Price] [money] NOT NULL,
	[UnitsInStock] [int] NOT NULL,
	[Picture] [varbinary](max) NULL,
	[IsDiscontinued] [bit] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Packaged] [bit] NOT NULL,
	[PackagedBy] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE deleteOrderDetail
	-- Add the parameters for the stored procedure here
(@orderID int, @productID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from OrderDetail where OrderID=@orderID 
		and ProductID=@productID
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE deleteProduct
	-- Add the parameters for the stored procedure here
(@productID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from Product where ProductID=@productID
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE deleteRetailer
	-- Add the parameters for the stored procedure here
(@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from Retailer where RetailerID=@id
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc insertCustomer
(@First nvarchar(50), @Last nvarchar(50), @CustomerID int out)
as
INSERT INTO Customer (FirstName, LastName) VALUES (@First, @Last)
set @CustomerID = SCOPE_IDENTITY()
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc insertCustomerAddress
(@CustomerID int, @AddressType int, @Street nvarchar(50), @Suburb nvarchar(50), 
@City nvarchar(50), @Postal nvarchar(15), @Country nvarchar(50)) 
as
DECLARE @AddressID int
INSERT INTO [Address] VALUES (@AddressType, @Street, @Suburb, @City, @Postal, @Country)
SET @AddressID = SCOPE_IDENTITY()
INSERT INTO CustomerAddress VALUES (@CustomerID, @AddressID)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc insertCustomerContact
(@CustomerID int, @ContactType int, @Contact nvarchar(50)) 
as
DECLARE @ContactID int
INSERT INTO Contact VALUES (@ContactType, @Contact)
SET @ContactID = SCOPE_IDENTITY()
INSERT INTO CustomerContact VALUES (@CustomerID, @ContactID)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc insertEmployee
(@First nvarchar(50), @Last nvarchar(50), @EmployID int out)
as
INSERT INTO Employee (FirstName, LastName) VALUES (@First, @Last)
set @EmployID = SCOPE_IDENTITY()
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc insertEmployeeAddress
(@EmployeeID int, @AddressType int, @Street nvarchar(50), @Suburb nvarchar(50), 
@City nvarchar(50), @Postal nvarchar(15), @Country nvarchar(50)) 
as
DECLARE @AddressID int
INSERT INTO [Address] VALUES (@AddressType, @Street, @Suburb, @City, @Postal, @Country)
SET @AddressID = SCOPE_IDENTITY()
INSERT INTO EmployeeAddress VALUES (@EmployeeID, @AddressID)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc insertEmployeeContact
(@EmployeeID int, @ContactType int, @Contact nvarchar(50)) 
as
DECLARE @ContactID int
INSERT INTO Contact VALUES (@ContactType, @Contact)
SET @ContactID = SCOPE_IDENTITY()
INSERT INTO EmployeeContact VALUES (@EmployeeID, @ContactID)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingLocation](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_ShippingLocations] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingMethod](
	[MethodID] [int] IDENTITY(1,1) NOT NULL,
	[Method] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_ShippingMethod] PRIMARY KEY CLUSTERED 
(
	[MethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipper](
	[ShipperID] [int] IDENTITY(1,1) NOT NULL,
	[ShipperName] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Address] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Country] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Phone] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Shipper] PRIMARY KEY CLUSTERED 
(
	[ShipperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipperOptions](
	[ShippingID] [int] IDENTITY(1,1) NOT NULL,
	[ShipperID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[MethodID] [int] NOT NULL,
 CONSTRAINT [PK_ShipperOptions] PRIMARY KEY CLUSTERED 
(
	[ShippingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoiced](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[ShippingOption] [int] NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoiced] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertInvoiced] 
	-- Add the parameters for the stored procedure here
(
	@orderID int,
	@employeeID int,
	@shipping int,
	@date datetime
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if exists (select * from Invoiced where OrderID=@orderID and
		EmployeeID=@employeeID)
		return 0
	else
		insert into Invoiced(OrderID,EmployeeID,ShippingOption,
			InvoiceDate)
		values(@orderID,@employeeID,@shipping,@date)
		return scope_identity()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertOrder]
	-- Add the parameters for the stored procedure here
(@customerID int, @addressID int, @orderDate datetime, @complete bit)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into [Order](CustomerID,AddressID,OrderDate,Complete)
	values(@customerID,@addressID,@orderDate,@complete)
	return scope_identity()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE insertOrderDetail
	-- Add the parameters for the stored procedure here
(@orderID int, @productID int, @quantity int, 
	@packaged bit, @packagedBy int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if exists (select * from OrderDetail where OrderID=@orderID and
		ProductID=@productID)
		update OrderDetail
		set
			Quantity=@quantity,
			Packaged=@packaged,
			PackagedBy=@packagedBy
		where OrderID=@orderID and ProductID=@productID
	else
		insert into OrderDetail(OrderID,ProductID,Quantity,
			Packaged,PackagedBy)
		values(@orderID,@productID,@quantity,@packaged,@packagedBy)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE insertProduct
	-- Add the parameters for the stored procedure here
(@retailerID int, @subcategoryID int, @name nvarchar(50), 
	@description nvarchar(255), @price money, @stock int,
	@picture varbinary(max))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Product(RetailerID,SubCategoryID,ProductName,
		ProductDescription,Price,UnitsInStock,Picture)
	values(@retailerID,@subcategoryID,@name,@description,
		@price,@stock,@picture)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE removeEmployeeFromRole (@EmployeeID int, @RoleID int)
AS
DELETE EmployeeRoles WHERE @EmployeeID = EmployeeID and RoleID = @RoleID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectCustomerAddresses (@CustomerID int)
AS
SELECT a.AddressID, a.AddressType, a.Street, a.Suburb, a.City, a.PostalCode, a.Country 
    FROM Address a inner join CustomerAddress ca on a.AddressID = ca.AddressID WHERE ca.CustomerID = @CustomerID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectCustomerContacts	(@CustomerID int)
AS
SELECT c.ContactID, c.Contact, c.ContactType FROM Contact c inner join  
    ContactType ct on c.ContactType = ct.ContactTypeID inner join CustomerContact cc on
    c.ContactID = cc.ContactID where cc.CustomerID = @CustomerID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectCustomers
AS
SELECT CustomerID, FirstName, LastName FROM Customer
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectEmployeeAddresses (@EmployeeID int)
AS
SELECT a.AddressID, a.AddressType, a.Street, a.Suburb, a.City, a.PostalCode, a.Country 
    FROM Address a inner join EmployeeAddress ea on a.AddressID = ea.AddressID WHERE ea.EmployeeID = @EmployeeID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectEmployeeContacts	(@EmployeeID int)
AS
SELECT c.ContactID, c.Contact, c.ContactType FROM Contact c inner join  
    ContactType ct on c.ContactType = ct.ContactTypeID inner join EmployeeContact ec on
    c.ContactID = ec.ContactID where ec.EmployeeID = @EmployeeID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectEmployeeRoles (@EmployeeID int)
AS
SELECT Role from Roles r inner join EmployeeRoles er on r.RoleID = er.RoleID
WHERE er.EmployeeID = @EmployeeID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE selectEmployees
AS
SELECT EmployeeID, FirstName, lastName FROM Employee
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE updateAddress 
(@AddressID int, @AddressType int, @Street nvarchar(50), @Suburb nvarchar(50), 
@City nvarchar(50), @Postal nvarchar(15), @Country nvarchar(50)) 	
AS
UPDATE Address SET AddressType = @AddressType, Street = @Street, Suburb = @Suburb, 
City = @City, PostalCode = @Postal, Country = @Country
WHERE AddressID = @AddressID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE updateContact (@ContactID int, @ContactTypeID int, @Contact nvarchar(50))
AS
UPDATE Contact SET ContactType = @ContactTypeID, Contact = @Contact WHERE ContactID = @ContactID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE updateCustomer (@CID int, @First nvarchar(50), @Last nvarchar(50))
AS
UPDATE Customer SET FirstName = @First, LastName = @Last WHERE CustomerID = @CID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE updateEmployee (@EmployeeID int, @First nvarchar(50), @Last nvarchar(50))
AS
UPDATE Employee SET FirstName = @First, LastName = @Last WHERE EmployeeID = @EmployeeID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE updateInvoiced 
	-- Add the parameters for the stored procedure here
(
	@invoiceID int,
	@orderID int,
	@employeeID int,
	@shipping int,
	@date datetime
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Invoiced
	set
		OrderID=@orderID,
		EmployeeID=@employeeID,
		ShippingOption=@shipping,
		InvoiceDate=@date
	where invoiceid=@invoiceid
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updateOrder] 
	-- Add the parameters for the stored procedure here
(
	@orderID int,
	@customerID int,
	@addID int,
	@orderDate datetime,
	@complete bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update [Order]
	set
		CustomerID=@customerID,
		AddressID=@addID,
		OrderDate=@orderDate,
		Complete=@complete
	where OrderId=@orderID
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE updateProduct
	-- Add the parameters for the stored procedure here
(@productID int,@retailerID int, @subcategoryID int, @name nvarchar(50), 
	@description nvarchar(255), @price money, @stock int,
	@picture varbinary(max))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Product
	set
		RetailerID=@retailerID,
		SubCategoryID=@subcategoryID,
		ProductName=@name,
		ProductDescription=@description,
		Price=@price,
		UnitsInStock=@stock,
		Picture=@picture
	where ProductID=@productID
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE verifyEmployee (@First nvarchar(50), @Password nvarchar(15))
AS
SELECT EmployeeID FROM Employee WHERE FirstName = @First AND Password = @Password
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishList](
	[WishListID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
 CONSTRAINT [PK_WishList] PRIMARY KEY CLUSTERED 
(
	[WishListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishListItems](
	[WishListID] [int] NOT NULL,
	[ProductID] [int] NOT NULL
)
GO
ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRoles_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeRoles] CHECK CONSTRAINT [FK_EmployeeRoles_Employee]
GO
ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[EmployeeRoles] CHECK CONSTRAINT [FK_EmployeeRoles_Roles]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_AddressType] FOREIGN KEY([AddressType])
REFERENCES [dbo].[AddressType] ([TypeID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_AddressType]
GO
ALTER TABLE [dbo].[CustomerAddress]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[CustomerAddress] CHECK CONSTRAINT [FK_CustomerAddress_Address]
GO
ALTER TABLE [dbo].[CustomerAddress]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddress_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerAddress] CHECK CONSTRAINT [FK_CustomerAddress_Customer]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_ContactType] FOREIGN KEY([ContactType])
REFERENCES [dbo].[ContactType] ([ContactTypeID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_ContactType]
GO
ALTER TABLE [dbo].[CustomerContact]  WITH CHECK ADD  CONSTRAINT [FK_CustomerContact_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ContactID])
GO
ALTER TABLE [dbo].[CustomerContact] CHECK CONSTRAINT [FK_CustomerContact_Contact]
GO
ALTER TABLE [dbo].[CustomerContact]  WITH CHECK ADD  CONSTRAINT [FK_CustomerContact_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerContact] CHECK CONSTRAINT [FK_CustomerContact_Customer]
GO
ALTER TABLE [dbo].[EmployeeAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[EmployeeAddress] CHECK CONSTRAINT [FK_EmployeeAddress_Address]
GO
ALTER TABLE [dbo].[EmployeeAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAddress_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeAddress] CHECK CONSTRAINT [FK_EmployeeAddress_Employee]
GO
ALTER TABLE [dbo].[EmployeeContact]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeContact_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ContactID])
GO
ALTER TABLE [dbo].[EmployeeContact] CHECK CONSTRAINT [FK_EmployeeContact_Contact]
GO
ALTER TABLE [dbo].[EmployeeContact]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeContact_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeContact] CHECK CONSTRAINT [FK_EmployeeContact_Employee]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Address]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[ProductSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubCategory_ProductCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[ProductCategory] ([CategoryID])
GO
ALTER TABLE [dbo].[ProductSubCategory] CHECK CONSTRAINT [FK_ProductSubCategory_ProductCategory]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductSubCategory] FOREIGN KEY([SubCategoryID])
REFERENCES [dbo].[ProductSubCategory] ([SubCategoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductSubCategory]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Retailer] FOREIGN KEY([RetailerID])
REFERENCES [dbo].[Retailer] ([RetailerID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Retailer]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Employee] FOREIGN KEY([PackagedBy])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Employee]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[ShipperOptions]  WITH CHECK ADD  CONSTRAINT [FK_ShipperOptions_Shipper] FOREIGN KEY([ShipperID])
REFERENCES [dbo].[Shipper] ([ShipperID])
GO
ALTER TABLE [dbo].[ShipperOptions] CHECK CONSTRAINT [FK_ShipperOptions_Shipper]
GO
ALTER TABLE [dbo].[ShipperOptions]  WITH CHECK ADD  CONSTRAINT [FK_ShipperOptions_ShippingLocation] FOREIGN KEY([LocationID])
REFERENCES [dbo].[ShippingLocation] ([LocationID])
GO
ALTER TABLE [dbo].[ShipperOptions] CHECK CONSTRAINT [FK_ShipperOptions_ShippingLocation]
GO
ALTER TABLE [dbo].[ShipperOptions]  WITH CHECK ADD  CONSTRAINT [FK_ShipperOptions_ShippingMethod] FOREIGN KEY([MethodID])
REFERENCES [dbo].[ShippingMethod] ([MethodID])
GO
ALTER TABLE [dbo].[ShipperOptions] CHECK CONSTRAINT [FK_ShipperOptions_ShippingMethod]
GO
ALTER TABLE [dbo].[Invoiced]  WITH CHECK ADD  CONSTRAINT [FK_Invoiced_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Invoiced] CHECK CONSTRAINT [FK_Invoiced_Employee]
GO
ALTER TABLE [dbo].[Invoiced]  WITH CHECK ADD  CONSTRAINT [FK_Invoiced_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[Invoiced] CHECK CONSTRAINT [FK_Invoiced_Order]
GO
ALTER TABLE [dbo].[Invoiced]  WITH CHECK ADD  CONSTRAINT [FK_Invoiced_ShipperOptions] FOREIGN KEY([ShippingOption])
REFERENCES [dbo].[ShipperOptions] ([ShippingID])
GO
ALTER TABLE [dbo].[Invoiced] CHECK CONSTRAINT [FK_Invoiced_ShipperOptions]
GO
ALTER TABLE [dbo].[WishList]  WITH CHECK ADD  CONSTRAINT [FK_WishList_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[WishList] CHECK CONSTRAINT [FK_WishList_Customer]
GO
ALTER TABLE [dbo].[WishListItems]  WITH CHECK ADD  CONSTRAINT [FK_WishListItems_WishList] FOREIGN KEY([WishListID])
REFERENCES [dbo].[WishList] ([WishListID])
GO
ALTER TABLE [dbo].[WishListItems] CHECK CONSTRAINT [FK_WishListItems_WishList]
GO
-- BCPArgs:3:[dbo].[Employee] in "c:\SQLAzureMW\BCPData\dbo.Employee.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:6:[dbo].[Roles] in "c:\SQLAzureMW\BCPData\dbo.Roles.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:6:[dbo].[EmployeeRoles] in "c:\SQLAzureMW\BCPData\dbo.EmployeeRoles.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:4:[dbo].[Customer] in "c:\SQLAzureMW\BCPData\dbo.Customer.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:3:[dbo].[AddressType] in "c:\SQLAzureMW\BCPData\dbo.AddressType.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:7:[dbo].[Address] in "c:\SQLAzureMW\BCPData\dbo.Address.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:4:[dbo].[CustomerAddress] in "c:\SQLAzureMW\BCPData\dbo.CustomerAddress.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:4:[dbo].[ContactType] in "c:\SQLAzureMW\BCPData\dbo.ContactType.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:13:[dbo].[Contact] in "c:\SQLAzureMW\BCPData\dbo.Contact.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:7:[dbo].[CustomerContact] in "c:\SQLAzureMW\BCPData\dbo.CustomerContact.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:3:[dbo].[EmployeeAddress] in "c:\SQLAzureMW\BCPData\dbo.EmployeeAddress.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:6:[dbo].[EmployeeContact] in "c:\SQLAzureMW\BCPData\dbo.EmployeeContact.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:1:[dbo].[Order] in "c:\SQLAzureMW\BCPData\dbo.Order.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:6:[dbo].[ProductCategory] in "c:\SQLAzureMW\BCPData\dbo.ProductCategory.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:26:[dbo].[ProductSubCategory] in "c:\SQLAzureMW\BCPData\dbo.ProductSubCategory.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:10:[dbo].[Retailer] in "c:\SQLAzureMW\BCPData\dbo.Retailer.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:49:[dbo].[Product] in "c:\SQLAzureMW\BCPData\dbo.Product.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:6:[dbo].[ShippingLocation] in "c:\SQLAzureMW\BCPData\dbo.ShippingLocation.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:4:[dbo].[ShippingMethod] in "c:\SQLAzureMW\BCPData\dbo.ShippingMethod.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:1:[dbo].[Shipper] in "c:\SQLAzureMW\BCPData\dbo.Shipper.dat" -E -n -C RAW -b 1000 -a 4096
GO
-- BCPArgs:1:[dbo].[ShipperOptions] in "c:\SQLAzureMW\BCPData\dbo.ShipperOptions.dat" -E -n -C RAW -b 1000 -a 4096
GO

