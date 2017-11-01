CREATE TABLE [dbo].[Movie] (
    [MovieId]          INT             IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [GenreId]          INT             NULL,
    [MovieName]        NVARCHAR (50)   NULL,
    [MoviePrice]       DECIMAL (10, 2) NULL,
    [MovieReleaseDate] INT             NULL,
    FOREIGN KEY (GenreId) REFERENCES dbo.Genre (GenreId)
);

