Create database FStore
create table Member
(
	MemberId int primary key,
	Email varchar(100) not null,
	CompanyName varchar(40) not null,
	City varchar(15) not null,
	Country varchar(15) not null,
	[Password] varchar(30) not null
)
create table [Order]
(
	OrderId int primary key,
	MemberId int not null foreign key references Member(MemberId),
	OrderDate datetime not null ,
	RequireDate datetime,
	ShippedDate datetime,
	Freight money
)
create table Product
(
	ProductId int primary key,
	CategoryId int not null,
	ProductName varchar(40) not null,
	[Weight] varchar(20) not null,
	UnitPrice money not null,
	UnitsInstock int not null
)
create table OrderDetail
(
	OrderId int not null foreign key references [Order](OrderId),
	ProductId int not null foreign key references Product(ProductId),
	UnitPrice money not null,
	Quantity int not null,
	Discount float not null
)
-- Sample records for the Member table
INSERT INTO Member (MemberId, Email, CompanyName, City, Country, [Password])
VALUES
(1, 'john@email.com', 'ABC Corporation', 'New York', 'USA', 'password123'),
(2, 'mary@email.com', 'XYZ Inc.', 'Los Angeles', 'USA', 'securepassword'),
(3, 'jane@email.com', 'Tech Solutions', 'London', 'UK', 'mypassword456'),
(4, 'peter@email.com', 'Global Services', 'Sydney', 'Australia', 'topsecret789'),
(5, 'susan@email.com', 'Tech Innovators', 'Toronto', 'Canada', 'letmein2022');

-- Sample records for the Order table
INSERT INTO [Order] (OrderId, MemberId, OrderDate, RequireDate, ShippedDate, Freight)
VALUES
(1, 1, '2023-01-15', '2023-01-20', '2023-01-17', 25.50),
(2, 2, '2023-02-10', '2023-02-15', '2023-02-12', 18.75),
(3, 3, '2023-03-05', '2023-03-10', '2023-03-08', 12.00),
(4, 4, '2023-04-20', '2023-04-25', '2023-04-22', 35.25),
(5, 5, '2023-05-12', '2023-05-17', '2023-05-15', 21.30);

-- Sample records for the Product table
INSERT INTO Product (ProductId, CategoryId, ProductName, [Weight], UnitPrice, UnitsInstock)
VALUES
(1, 101, 'Laptop', '2.5 kg', 799.99, 50),
(2, 102, 'Smartphone', '0.2 kg', 399.99, 100),
(3, 103, 'Printer', '7.0 kg', 249.99, 30),
(4, 101, 'Tablet', '0.7 kg', 299.99, 40),
(5, 102, 'Headphones', '0.3 kg', 49.99, 150);

-- Sample records for the OrderDetail table
INSERT INTO OrderDetail (OrderId, ProductId, UnitPrice, Quantity, Discount)
VALUES
(1, 1, 799.99, 2, 0.05),
(1, 2, 399.99, 3, 0.1),
(2, 3, 249.99, 1, 0),
(3, 4, 299.99, 2, 0.05),
(4, 5, 49.99, 5, 0.15);
