IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'FA25RealEstateDB')
BEGIN
    CREATE DATABASE [FA25RealEstateDB];
END;
GO
USE [FA25RealEstateDB];
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'SystemUser')
BEGIN
    CREATE TABLE SystemUser (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        UserPassword NVARCHAR(255) NOT NULL,
        Username NVARCHAR(255) NOT NULL UNIQUE, -- Cột Username
        UserRole INT NOT NULL,
        RegistrationDate DATETIME DEFAULT GETDATE()
    );
END;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Broker')
BEGIN
    CREATE TABLE Broker (
        BrokerID INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(255) NOT NULL,
        Phone NVARCHAR(20),
        Address NVARCHAR(500)
    );
END;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Contract')
BEGIN
    CREATE TABLE Contract (
        ContractID INT IDENTITY(1,1) PRIMARY KEY,
        ContractTitle NVARCHAR(150) NOT NULL,
        PropertyType NVARCHAR(100) NOT NULL,
        SigningDate DATE NOT NULL,
        ExpirationDate DATE NOT NULL,
        BrokerID INT NOT NULL,
        Value DECIMAL(18, 2) NOT NULL,
        FOREIGN KEY (BrokerID) REFERENCES Broker(BrokerID)
    );
END;
GO

IF NOT EXISTS (SELECT * FROM SystemUser WHERE Username = 'admin')
INSERT INTO SystemUser (UserPassword, Username, UserRole) VALUES
('admin123', 'admin', 1);    
IF NOT EXISTS (SELECT * FROM SystemUser WHERE Username = 'manager') 
INSERT INTO SystemUser (UserPassword, Username, UserRole) VALUES
('manager123', 'manager', 2); 
IF NOT EXISTS (SELECT * FROM SystemUser WHERE Username = 'staff')
INSERT INTO SystemUser (UserPassword, Username, UserRole) VALUES
('staff123', 'staff', 3);   
IF NOT EXISTS (SELECT * FROM SystemUser WHERE Username = 'member')
INSERT INTO SystemUser (UserPassword, Username, UserRole) VALUES
('member123', 'member', 4);
GO

IF NOT EXISTS (SELECT * FROM Broker WHERE FullName = 'Nguyen Thanh Dat')
INSERT INTO Broker (FullName, Phone, Address) VALUES
('Nguyen Thanh Dat', '0901234567', '45 Nguyen Hue, Quan 1, TP.HCM');
IF NOT EXISTS (SELECT * FROM Broker WHERE FullName = 'Tran Thi Hong')
INSERT INTO Broker (FullName, Phone, Address) VALUES
('Tran Thi Hong', '0908765432', '12 Phan Dang Luu, Quan Binh Thanh, TP.HCM');
IF NOT EXISTS (SELECT * FROM Broker WHERE FullName = 'Le Minh Khang')
INSERT INTO Broker (FullName, Phone, Address) VALUES
('Le Minh Khang', '0912345678', '88 Hoang Van Thu, Quan Phu Nhuan, TP.HCM');
IF NOT EXISTS (SELECT * FROM Broker WHERE FullName = 'Pham Ngoc Yen')
INSERT INTO Broker (FullName, Phone, Address) VALUES
('Pham Ngoc Yen', '0919876543', '10 Ton Duc Thang, Quan 1, TP.HCM');
IF NOT EXISTS (SELECT * FROM Broker WHERE FullName = 'Vo Quoc Bao')
INSERT INTO Broker (FullName, Phone, Address) VALUES
('Vo Quoc Bao', '0933445566', '20 Cong Hoa, Quan Tan Binh, TP.HCM');
GO


DECLARE @BrokerID_Dat INT = (SELECT BrokerID FROM Broker WHERE FullName = N'Nguyen Thanh Dat');
DECLARE @BrokerID_Hong INT = (SELECT BrokerID FROM Broker WHERE FullName = N'Tran Thi Hong');
DECLARE @BrokerID_Khang INT = (SELECT BrokerID FROM Broker WHERE FullName = N'Le Minh Khang');
DECLARE @BrokerID_Yen INT = (SELECT BrokerID FROM Broker WHERE FullName = N'Pham Ngoc Yen');
DECLARE @BrokerID_Bao INT = (SELECT BrokerID FROM Broker WHERE FullName = N'Vo Quoc Bao');

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Ban Can Ho Cao Cap Sapphire')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Ban Can Ho Cao Cap Sapphire', N'Apartment', '2024-03-01', '2025-03-01', @BrokerID_Dat, 4500000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Thue Van Phong Dien Tich Lon')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Thue Van Phong Dien Tich Lon', N'Commercial', '2024-04-10', '2024-10-10', @BrokerID_Hong, 75000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Mua Dat Nen Khu Dan Cu Moi')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Mua Dat Nen Khu Dan Cu Moi', N'Land', '2024-01-20', '2026-01-20', @BrokerID_Khang, 2100000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Hop Dong Ban Nha Pho Hai Mat Tien')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Hop Dong Ban Nha Pho Hai Mat Tien', N'House', '2024-02-15', '2025-02-15', @BrokerID_Dat, 3800000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Thue Can Ho Mini Gan Truong Dai Hoc')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Thue Can Ho Mini Gan Truong Dai Hoc', N'Apartment', '2024-05-05', '2024-11-05', @BrokerID_Yen, 15000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Ban Villa Nghi Duong Da Lat')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Ban Villa Nghi Duong Da Lat', N'Villa', '2024-03-25', '2026-03-25', @BrokerID_Hong, 12000000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Thue Mat Bang Kinh Doanh Quan Binh Thanh')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Thue Mat Bang Kinh Doanh Quan Binh Thanh', N'Commercial', '2024-06-01', '2025-06-01', @BrokerID_Bao, 45000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Hop Dong Mua Dat View Bien')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Hop Dong Mua Dat View Bien', N'Land', '2024-04-01', '2027-04-01', @BrokerID_Khang, 5000000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Ban Nha 1 TreT 1 Lau Quan 7')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Ban Nha 1 TreT 1 Lau Quan 7', N'House', '2024-07-10', '2025-07-10', @BrokerID_Yen, 2900000000.00);

IF NOT EXISTS (SELECT * FROM Contract WHERE ContractTitle = N'Cho Thue Can Ho Studio Quan 4')
INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerID, Value) VALUES
(N'Cho Thue Can Ho Studio Quan 4', N'Apartment', '2024-08-01', '2025-02-01', @BrokerID_Bao, 10000000.00);
GO