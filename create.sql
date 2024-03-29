Build started...
Build succeeded.
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TrustFrameworkPolicies] (
    [DbKey] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TrustFrameworkPolicies] PRIMARY KEY ([DbKey])
);
GO

CREATE TABLE [SubJourneys] (
    [DbKey] uniqueidentifier NOT NULL,
    [PolicyDbKey] uniqueidentifier NOT NULL,
    [Id] nvarchar(max) NULL,
    [Type] int NOT NULL,
    CONSTRAINT [PK_SubJourneys] PRIMARY KEY ([DbKey]),
    CONSTRAINT [FK_SubJourneys_TrustFrameworkPolicies_PolicyDbKey] FOREIGN KEY ([PolicyDbKey]) REFERENCES [TrustFrameworkPolicies] ([DbKey]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserJourneys] (
    [DbKey] uniqueidentifier NOT NULL,
    [PolicyDbKey] uniqueidentifier NOT NULL,
    [AssuranceLevel] nvarchar(max) NULL,
    [Id] nvarchar(max) NULL,
    CONSTRAINT [PK_UserJourneys] PRIMARY KEY ([DbKey]),
    CONSTRAINT [FK_UserJourneys_TrustFrameworkPolicies_PolicyDbKey] FOREIGN KEY ([PolicyDbKey]) REFERENCES [TrustFrameworkPolicies] ([DbKey]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrchestrationSteps] (
    [DbKey] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    [OrchestrationStepJourney] varchar(32) NOT NULL,
    [JourneyDbKey] uniqueidentifier NULL,
    [OrchestrationStepUserJourney_JourneyDbKey] uniqueidentifier NULL,
    CONSTRAINT [PK_OrchestrationSteps] PRIMARY KEY ([DbKey]),
    CONSTRAINT [FK_OrchestrationSteps_SubJourneys_JourneyDbKey] FOREIGN KEY ([JourneyDbKey]) REFERENCES [SubJourneys] ([DbKey]),
    CONSTRAINT [FK_OrchestrationSteps_UserJourneys_OrchestrationStepUserJourney_JourneyDbKey] FOREIGN KEY ([OrchestrationStepUserJourney_JourneyDbKey]) REFERENCES [UserJourneys] ([DbKey])
);
GO

CREATE TABLE [Candidates] (
    [DbKey] uniqueidentifier NOT NULL,
    [SubJourneyDbKey] uniqueidentifier NOT NULL,
    [SubJourneyReferenceId] varchar(128) NOT NULL,
    [OrchestrationStepDbKey] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY ([DbKey]),
    CONSTRAINT [FK_Candidates_OrchestrationSteps_OrchestrationStepDbKey] FOREIGN KEY ([OrchestrationStepDbKey]) REFERENCES [OrchestrationSteps] ([DbKey]) ON DELETE CASCADE,
    CONSTRAINT [FK_Candidates_SubJourneys_SubJourneyDbKey] FOREIGN KEY ([SubJourneyDbKey]) REFERENCES [SubJourneys] ([DbKey]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Candidates_OrchestrationStepDbKey] ON [Candidates] ([OrchestrationStepDbKey]);
GO

CREATE INDEX [IX_Candidates_SubJourneyDbKey] ON [Candidates] ([SubJourneyDbKey]);
GO

CREATE INDEX [IX_OrchestrationSteps_JourneyDbKey] ON [OrchestrationSteps] ([JourneyDbKey]);
GO

CREATE INDEX [IX_OrchestrationSteps_OrchestrationStepUserJourney_JourneyDbKey] ON [OrchestrationSteps] ([OrchestrationStepUserJourney_JourneyDbKey]);
GO

CREATE INDEX [IX_SubJourneys_PolicyDbKey] ON [SubJourneys] ([PolicyDbKey]);
GO

CREATE INDEX [IX_UserJourneys_PolicyDbKey] ON [UserJourneys] ([PolicyDbKey]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230104192645_Init', N'7.0.0');
GO

COMMIT;
GO


