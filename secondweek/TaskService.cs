using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondweek
{
    public class TaskService
    {
        private TaskRepository DataBase;
        public TaskService(TaskRepository dataBase) { DataBase = dataBase; }

        public async Task DeleteTask()
        {
            Console.WriteLine("Введите номер записи:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Неверный формат ввода");
            }
            await DataBase.DeleteTask(id);
        }

        public async Task ChangeTaskStatus()
        {
            Console.WriteLine("Введите номер записи:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Неверный формат ввода");
            }
            await DataBase.ChangeStatus(id);
        }

        public async Task TypeTask()
        {
            Console.WriteLine("Введите заголовок: ");
            string title = Console.ReadLine();
            Console.WriteLine("Введите описание: ");
            string description = Console.ReadLine();

            await DataBase.AddTask(title, description, false, DateTime.Now);
        }

        public async Task WriteAllTasks()
        {
            List<TaskEntity> tasks = await DataBase.AllTasks();
            foreach (TaskEntity task in tasks)
            {
                Console.WriteLine($"Id: {task.Id}; Заголовок: {task.Title}; Описание: {task.Description}; Статус: {task.IsCompleted}; Время: {task.CreatedAt};");
            }
        }

    }
}
