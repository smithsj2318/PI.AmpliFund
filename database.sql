create table dbo.Product
(
    ProductId   uniqueidentifier default newsequentialid() not null
        constraint Product_pk
            primary key nonclustered,
    RowVersion  timestamp                                  null,
    ProductSku  nvarchar(100)                              not null,
    Description nvarchar(500)                              not null,
    Price       decimal(18, 2)                             not null
)
go

create unique clustered index Product_ProductSku_uindex
    on dbo.Product (ProductSku)
go

create table dbo.StoreMembership
(
    StoreMembershipId uniqueidentifier default newsequentialid() not null
        constraint StoreMembership_pk
            primary key,
    RowVersion        timestamp                                  null,
    Description       nvarchar(500)                              not null,
    Discount          float                                      not null
)
go

create table dbo.ApplicationUser
(
    ApplicationUserId   uniqueidentifier default newsequentialid() not null
        constraint ApplicationUser_pk
            primary key nonclustered,
    RowVersion          timestamp                                  null,
    ApplicationUserName nvarchar(200)                              not null,
    StoreMembershipId   uniqueidentifier                           not null
        constraint ApplicationUser_StoreMembership_StoreMembershipId_fk
            references dbo.StoreMembership
)
go

create unique clustered index ApplicationUser_ApplicationUserName_uindex
    on dbo.ApplicationUser (ApplicationUserName)
go

create table dbo.ShoppingCart
(
    ShoppingCartId    uniqueidentifier default newsequentialid() not null
        constraint ShoppingCart_pk
            primary key,
    RowVersion        timestamp                                  null,
    ApplicationUserId uniqueidentifier                           not null
        constraint ShoppingCart_ApplicationUser_ApplicationUserId_fk
            references dbo.ApplicationUser
)
go

create table dbo.ShoppingCartItem
(
    ShoppingCartItemId uniqueidentifier default newsequentialid() not null
        constraint ShoppingCartItem_pk
            primary key nonclustered
        constraint ShoppingCartItem_pk
            primary key nonclustered,
    RowVersion         timestamp                                  null,
    ShoppingCartId     uniqueidentifier                           not null
        constraint ShoppingCartItem_ShoppingCart_ShoppingCartId_fk
            references dbo.ShoppingCart
            on delete cascade,
    Quantity           int                                        not null,
    ProductId          uniqueidentifier
        constraint ShoppingCartItem_Product_ProductId_fk
            references dbo.Product
)
go

create unique clustered index ShoppingCartItem_ShoppingCartId_uindex
    on dbo.ShoppingCartItem (ShoppingCartId)
go

INSERT INTO dbo.ApplicationUser (ApplicationUserId, ApplicationUserName, StoreMembershipId) VALUES (N'76E2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'bronze@gmail.com', N'6DE2423E-F36B-1410-8CE9-00A7CCFCEAE3');
INSERT INTO dbo.ApplicationUser (ApplicationUserId, ApplicationUserName, StoreMembershipId) VALUES (N'7EE2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'gold@gmail.com', N'73E2423E-F36B-1410-8CE9-00A7CCFCEAE3');
INSERT INTO dbo.ApplicationUser (ApplicationUserId, ApplicationUserName, StoreMembershipId) VALUES (N'79E2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'silver@gmail.com', N'70E2423E-F36B-1410-8CE9-00A7CCFCEAE3');
GO

INSERT INTO dbo.StoreMembership (StoreMembershipId, Description, Discount) VALUES (N'6DE2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'Bronze', 0.00);
INSERT INTO dbo.StoreMembership (StoreMembershipId, Description, Discount) VALUES (N'70E2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'Silver', 1.00);
INSERT INTO dbo.StoreMembership (StoreMembershipId, Description, Discount) VALUES (N'73E2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'Gold', 5.00);
GO

INSERT INTO dbo.Product (ProductId, ProductSku, Description, Price) VALUES (N'84E2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'1234', N'Widget', 6.99);
INSERT INTO dbo.Product (ProductId, ProductSku, Description, Price) VALUES (N'8AE2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'5678', N'Doohickey', 10.99);
INSERT INTO dbo.Product (ProductId, ProductSku, Description, Price) VALUES (N'90E2423E-F36B-1410-8CE9-00A7CCFCEAE3', N'abcd1234', N'Thingamajig', 20.99);


