using BasicEmailQueueManager.Domain;
using BasicEmailQueueManager.Infrastructure;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BasicEmailQueueManager.SQLImplementation
{
    public class SQLEmailQueueRepository : IEmailRepository
    {
        private readonly string _connectionString;

        public SQLEmailQueueRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Missing connection string", connectionString);

            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Email>> Dequeue(int count)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                await connection.OpenAsync();
                return await connection.QueryAsync<Email>(
                   @$"  UPDATE EmailQueueManager.Email
                        SET [Status] = @processingStatus, LastUpdateDate = @lastUpdateDate
                        OUTPUT INSERTED.*
                        FROM 
                        (
                            SELECT TOP {count} EmailId 
                            FROM EmailQueueManager.Email 
                            WHERE [Status] = @newStatus
                            ORDER BY EmailId ASC
                        ) AS DequeuedEmails
                        WHERE EmailQueueManager.Email.EmailId = DequeuedEmails.EmailId",
                   new
                   {
                       processingStatus = (byte)Statuses.Processing,
                       newStatus = (byte)Statuses.New,
                       lastUpdateDate = DateTimeOffset.Now
                   });
            }
        }

        public async Task Enqueue(NewEmail newEmail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "INSERT INTO EmailQueueManager.Email " +
                    "([Status], [CreationDate], [LastUpdateDate], [Body], [Subject], [From], [To], [Cc]) " +
                    "VALUES " +
                    "(@statusId, @CreationDate, @LastUpdateDate, @Body, @Subject, @From, @To, @Cc)",
                    new
                    {
                        statusId = Statuses.New,
                        newEmail.CreationDate,
                        newEmail.LastUpdateDate,
                        newEmail.Body,
                        newEmail.Subject,
                        newEmail.From,
                        newEmail.To,
                        newEmail.Cc
                    });
            }
        }

        public async Task SetInError(int emailId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "UPDATE EmailQueueManager.Email " +
                    "SET Status = @statusId, LastUpdateDate = @lastUpdateDate " +
                    "WHERE EmailId = @emailId",
                    new { emailId, statusId = (byte)Statuses.Error, lastUpdateDate = DateTimeOffset.Now });
            }
        }

        public async Task SetSent(int emailId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "UPDATE EmailQueueManager.Email " +
                    "SET Status = @statusId, LastUpdateDate = @lastUpdateDate " +
                    "WHERE EmailId = @emailId",
                    new { emailId, statusId = (byte)Statuses.Sent, lastUpdateDate = DateTimeOffset.Now });
            }
        }
    }
}
