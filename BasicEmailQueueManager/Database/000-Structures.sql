CREATE SCHEMA [EmailQueueManager];

GO

CREATE TABLE [EmailQueueManager].[Configuration]
(
    [Key]   VARCHAR(64)     NOT NULL,
    [Value] VARCHAR(MAX)    NOT NULL,

    CONSTRAINT [PK_EmailQueueManager_Configuration_Key] PRIMARY KEY CLUSTERED ([Key])
);

GO

CREATE TABLE [EmailQueueManager].[Statuses]
(
    [StatusId]      TINYINT     NOT NULL,
    [StatusName]    VARCHAR(16) NOT NULL,

    CONSTRAINT [PK_EmailQueueManager_StatusId]      PRIMARY KEY CLUSTERED ([StatusId]),
    CONSTRAINT [UK_EmailQueueManager_StatusName]    UNIQUE ([StatusName])
)

GO

CREATE TABLE [EmailQueueManager].[Email]
(
    [EmailId]           INT                 NOT NULL IDENTITY(1, 1),
    [Status]            TINYINT             NOT NULL,
    [CreationDate]      DATETIMEOFFSET(0)   NOT NULL,
    [LastUpdateDate]    DATETIMEOFFSET(0)   NOT NULL,
    [Body]              NVARCHAR(MAX)       NOT NULL,
    [Subject]           NVARCHAR(128)       NOT NULL,
    [From]              NVARCHAR(64)        NOT NULL,
    [To]                NVARCHAR(MAX)       NOT NULL,
    [Cc]                NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_EmailQueueManager_Email_EmailId] PRIMARY KEY CLUSTERED ([EmailId]),
    CONSTRAINT [FK_EmailQueueManager_Email_Status]  FOREIGN KEY ([Status]) REFERENCES [EmailQueueManager].[Statuses]
);

GO

-- DEQUEUE "FRIENDLY" INDEX
CREATE NONCLUSTERED INDEX [IX_EmailQueueManager_Dequeue] ON [EmailQueueManager].[Email] ([Status]);

GO