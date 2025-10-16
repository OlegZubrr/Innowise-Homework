using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TaskManagementProject.Data;
using TaskManagementProject.Models;

namespace TaskManagementProject;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var connectionString = config["ConnectionStrings:SQliteConnection"] ??
                               throw new NullReferenceException("ConnectionString is empty");

        var factory = new DbConnectionFactory(connectionString);
        factory.InitializeDatabase();

        var repo = new TaskRepository(factory);

        while (true)
        {
            Console.WriteLine("\n=== Task Manager ===");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Show all tasks");
            Console.WriteLine("3. Update tasks status");
            Console.WriteLine("4. Delete task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AddTask(repo);
                    break;
                case "2":
                    await ShowTasks(repo);
                    break;
                case "3":
                    await UpdateTaskStatus(repo);
                    break;
                case "4":
                    await DeleteTask(repo);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        static async Task AddTask(TaskRepository taskRepository)
        {
            string? title;
            do
            {
                Console.Write("Enter the task name: ");
                title = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(title))
                    Console.WriteLine("Task name cannot be empty! Please enter a valid name.");
            } while (string.IsNullOrEmpty(title));

            Console.Write("Enter the task description: ");
            var description = Console.ReadLine();

            var task = new TaskItem
            {
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            var id = await taskRepository.AddAsync(task);

            Console.WriteLine($"Task added with id {id}");
        }


        static async Task ShowTasks(TaskRepository taskRepository)
        {
            var tasks = await taskRepository.GetAllAsync();
            foreach (var task in tasks)
                Console.WriteLine(
                    $"Task Id = {task.Id}: {task.Title} | {task.Description} | {(task.IsCompleted ? "completed" : "inProcess")} | {task.CreatedAt}");
        }

        static async Task UpdateTaskStatus(TaskRepository taskRepository)
        {
            Console.Write("Enter the task id: ");

            if (int.TryParse(Console.ReadLine(), out var id))
            {
                Console.Write("Mark as completed? (Y/n): ");

                var answer = Console.ReadLine();

                var isCompleted = answer.ToLower() == "y" || string.IsNullOrEmpty(answer);

                if (await taskRepository.UpdateStatusAsync(id, isCompleted))
                    Console.WriteLine("Task status updated");
                else
                    Console.WriteLine("Task not found");
            }
        }

        static async Task DeleteTask(TaskRepository taskRepository)
        {
            Console.Write("Enter the task id: ");
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                if (await taskRepository.DeleteAsync(id))
                    Console.WriteLine("Task deleted");
                else
                    Console.WriteLine("Task not found");
            }
        }
    }
}