CREATE TABLE [dbo].[Starships]
(
  [Id] INT NOT NULL PRIMARY KEY,
  [RegistryIdentifier] VARCHAR(15) NOT NULL,
  [ClassId] INT NULL
    CONSTRAINT FK_Starships_Classifications
      FOREIGN KEY REFERENCES StarshipClassifications (Id),
  [PrimaryConfiguration] INT NOT NULL
    CONSTRAINT FK_Starships_Configurations
      FOREIGN KEY REFERENCES dbo.StarshipConfigurations (Id)
)
