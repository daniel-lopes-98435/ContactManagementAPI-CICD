-- Create database 'ContactManagment'
CREATE DATABASE ContactManagment;
GO

-- Select database 'ContactManagment'
USE ContactManagment;
GO

-- Create table 'Contact'
CREATE TABLE Contacts (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  FirstName NVARCHAR(70) NOT NULL,
  LastName NVARCHAR(70) NOT NULL,
  AreaCode CHAR(2) NOT NULL,
  PhoneNumber INT NOT NULL,
  Email NVARCHAR(100) NOT NULL
);
GO
GO

-- Add sample records in Contact table -- Insere um contato na tabela 'Contatct'
INSERT INTO Contacts (FirstName, LastName, AreaCode, PhoneNumber, Email)
VALUES ('João', 'Silva', '11', '777799999', 'joao.silva@email.com');
GO
INSERT INTO Contacts (FirstName, LastName, AreaCode, PhoneNumber, Email)
VALUES ('Maria', 'Silva', '11', '888899999', 'maria.silva@email.com');
GO
INSERT INTO Contacts (FirstName, LastName, AreaCode, PhoneNumber, Email)
VALUES ('Jose', 'Silva', '11', '999999999', 'jose.silva@email.com');
GO
-- 5 List of contats added
SELECT TOP 10 * FROM Contacts

-- 6 Add sample records in Contact table -- Insere um contato na tabela 'Contatct'

CREATE TABLE [dbo].[Users](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Username] [nvarchar](50) NOT NULL,
    [Password] [nvarchar](100) NOT NULL,
    [SystemPermission] [int] NOT NULL,
    PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    UNIQUE NONCLUSTERED
(
[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]

    INSERT INTO Users (Username, Password, SystemPermission)
    VALUES ('dev', 'admin', 0)

    INSERT INTO Users (Username, Password, SystemPermission)
    VALUES ('user', 'usuario', 1)

-- 7 List of Users added
Select * from Users