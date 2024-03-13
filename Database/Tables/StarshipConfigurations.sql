CREATE TABLE [dbo].[StarshipConfigurations]
(
  [Id] INT NOT NULL PRIMARY KEY,
  [Description] NVARCHAR(150) NOT NULL,
  [Remarks] NTEXT NULL,
  [RemarkFormat] CHAR(1) NULL
    CONSTRAINT FK_StarshipConfigurations_Formats
      FOREIGN KEY REFERENCES Formats (Code)
)
