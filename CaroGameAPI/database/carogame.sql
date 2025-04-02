USE master;
GO
DROP DATABASE IF EXISTS CaroGameDB;
GO
CREATE DATABASE CaroGameDB;
GO
USE CaroGameDB;
GO
DROP TABLE IF EXISTS Users;
GO
-- Người chơi
CREATE TABLE Users (
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    EloRating INT DEFAULT 1000,
    CreateAt DATETIME DEFAULT GETDATE(),
);
GO
DROP TABLE IF EXISTS Rooms;
GO
-- Phòng chơi
CREATE TABLE Rooms (
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RoomCode NVARCHAR(10) UNIQUE NOT NULL,
    OwnerID UNIQUEIDENTIFIER NOT NULL,
    PasswordRoom NVARCHAR(50) NULL,
    IsPublic BIT NOT NULL DEFAULT 1,
    Status VARCHAR(50) NOT NULL DEFAULT 'Waiting',
    CreateAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OwnerID) REFERENCES Users(ID) ON DELETE CASCADE
);
GO
DROP TABLE IF EXISTS Matches;
GO
-- Trận đấu 
CREATE TABLE Matches (
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RoomId UNIQUEIDENTIFIER NOT NULL,
    Player1ID UNIQUEIDENTIFIER NOT NULL,
    Player2ID UNIQUEIDENTIFIER NOT NULL,
    WinnerID UNIQUEIDENTIFIER NULL,
    CreateAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoomId) REFERENCES Rooms(Id),
    FOREIGN KEY (Player1ID) REFERENCES Users(Id),
    FOREIGN KEY (Player2ID) REFERENCES Users(Id),
    FOREIGN KEY (WinnerID) REFERENCES Users(Id) ON DELETE SET NULL
)
GO
DROP TABLE IF EXISTS Moves;
GO
-- Lưu các nước đi của người chơi
CREATE TABLE Moves (
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),    
    MatchID UNIQUEIDENTIFIER NOT NULL,
    PlayerID UNIQUEIDENTIFIER NOT NULL,
    X INT NOT NULL,
    Y INT NOT NULL,
    CreateAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_MatchID FOREIGN KEY (MatchID) REFERENCES Matches(ID),
    CONSTRAINT FK_PlayerID FOREIGN KEY (PlayerID) REFERENCES Users(ID)
)
GO
DROP TABLE IF EXISTS Ranking;
GO
-- Bảng xếp hạng
CREATE TABLE Ranking (
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER NOT NULL,
    Win INT NOT NULL,
    Lose INT NOT NULL,
    Draw INT NOT NULL,
    EloRating INT NOT NULL,
    CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES Users(ID)
)

GO
-- IDX_RoomCode dùng để tìm kiếm phòng theo RoomCode
-- IDX_UserRanking dùng để sắp xếp xếp hạng người chơi theo EloRating
GO
DROP INDEX IF EXISTS IDX_RoomCode ON Rooms;
DROP INDEX IF EXISTS IDX_UserRanking ON Ranking;
GO
CREATE INDEX IDX_RoomCode ON Rooms(RoomCode);
CREATE INDEX IDX_UserRanking ON Ranking(EloRating DESC);
GO
-- Insert dữ liệu mẫu
INSERT INTO Users (Username, PasswordHash, Email) 
VALUES ('Alice', 'hash_alice', 'alice@example.com'),
    ('Bob', 'hash_bob', 'bob@example.com'),
    ('Charlie', 'hash_charlie', 'charlie@example.com');

DECLARE @AliceID UNIQUEIDENTIFIER = (SELECT ID FROM Users WHERE Username = 'Alice');
DECLARE @BobID UNIQUEIDENTIFIER = (SELECT ID FROM Users WHERE Username = 'Bob');
DECLARE @CharlieID UNIQUEIDENTIFIER = (SELECT ID FROM Users WHERE Username = 'Charlie');

INSERT INTO Rooms (RoomCode, OwnerID, PasswordRoom, IsPublic, Status) 
VALUES ('ROOM123', @AliceID, NULL, 1, 'Waiting');

DECLARE @RoomID UNIQUEIDENTIFIER = (SELECT ID FROM Rooms WHERE RoomCode = 'ROOM123');

INSERT INTO Matches (RoomId, Player1ID, Player2ID, WinnerID) 
VALUES (@RoomID, @AliceID, @BobID, @AliceID);

DECLARE @MatchID UNIQUEIDENTIFIER = (SELECT ID FROM Matches WHERE RoomId = @RoomID);

INSERT INTO Moves (MatchID, PlayerID, X, Y) 
VALUES (@MatchID, @AliceID, 0, 0),
    (@MatchID, @BobID, 0, 1),
    (@MatchID, @AliceID, 1, 1),
    (@MatchID, @BobID, 1, 0);

INSERT INTO Ranking (UserID, Win, Lose, Draw, EloRating)
VALUES (@AliceID, 10, 2, 1, 1200),
    (@BobID, 8, 4, 1, 1100),
    (@CharlieID, 5, 5, 2, 1000);
