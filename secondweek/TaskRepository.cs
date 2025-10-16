using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace secondweek
{
    public class TaskRepository
    {
        readonly string _connectionString;
        public TaskRepository(string connectionString) { _connectionString = connectionString; }

        public async Task DeleteTask(int taskId)
        {
            int result;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    result = await connection.ExecuteAsync(("DELETE FROM Tasks WHERE Id = @id"), new { Id = taskId });

                }
                if (result > 0)
                    Console.WriteLine("Выполнено успешно!");
                else
                    Console.WriteLine("Запись не найдена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении: {ex.Message}");

            }
        }

        public async Task ChangeStatus(int taskId)
        {
            int result;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    result = await connection.ExecuteAsync(("UPDATE Tasks SET IsCompleted = ~IsCompleted WHERE Id = @id"), new { Id = taskId });

                }
                if (result > 0)
                    Console.WriteLine("Выполнено успешно!");
                else
                    Console.WriteLine("Запись не найдена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении: {ex.Message}");

            }
        }

        public async Task AddTask(string title, string description, bool isCompleted, DateTime createTime)
        {
            int result;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    result = await connection.ExecuteAsync("INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) VALUES (@title, @description, @isCompleted, @createdAt )", new { Title = title, Description = description, IsCompleted = isCompleted, CreatedAt = createTime });

                }

                Console.WriteLine("Выполнено успешно!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении: {ex.Message}");

            }
        }

        public async Task<List<TaskEntity>> AllTasks()
        {

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var results = await connection.QueryAsync<TaskEntity>("SELECT * FROM Tasks");
                    if (results.Any())
                        return results.ToList();
                    else
                    {
                        Console.WriteLine("Записей не найдено.");
                        return new List<TaskEntity>();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении: {ex.Message}");
                return new List<TaskEntity>();
            }
        }

    }
}
