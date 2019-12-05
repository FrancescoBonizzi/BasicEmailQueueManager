INSERT INTO [EmailQueueManager].[Configuration] 
    ([Key], [Value])
VALUES
    ('EnableSsl', '1'),
    ('UseDefaultCredentials', '1'),
    ('Host', 'localhost'),
    ('Port', '25'),
    ('Username', ''),
    ('Password', ''),
    ('RunIntervalSeconds', '60')

GO

INSERT INTO [EmailQueueManager].[Statuses] 
    ([StatusId], [StatusName])
VALUES
    (0, 'New'),
    (1, 'Processing'),
    (2, 'Sent'),
    (3, 'Error')

GO