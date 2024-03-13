CREATE TABLE [dbo].[StarshipClassifications]
(
  [Id] INT NOT NULL PRIMARY KEY,
  [Description] NVARCHAR(150) NOT NULL,
  [Remarks] NTEXT NULL,
  [RemarkFormat] CHAR(1) NULL,
  CONSTRAINT FK_StarshipClassifications_Formats
    FOREIGN KEY ([RemarkFormat]) REFERENCES Formats (Code)
)
