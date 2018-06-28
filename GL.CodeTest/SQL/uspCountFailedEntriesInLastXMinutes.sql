CREATE PROCEDURE uspCountFailedEntriesInLastXMinutes
@Minutes INT,
@EntryCount INT OUT
AS



SET @EntryCount = (SELECT COUNT(EntryID) FROM [Entry] WHERE [Entry].[DateTime] >= DATEADD(minute, -@Minutes, GETDATE()))