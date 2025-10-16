// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using secondweek;

var _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

var connectionString = _configuration.GetConnectionString("DefaultConnection");

TaskRepository repository = new TaskRepository(connectionString);
TaskService service = new TaskService(repository);

bool exitRequested = false;
while (!exitRequested)
{
    string Message = "Выберите действие:\n1 - Добавить новую запись;\n2 - Изменить статус задачи;\n3 - Получить все задачи;\n4 - Удалить задачу;\n0 - Выход.";
    Console.WriteLine(Message);
    Console.WriteLine("Нажмите клавишу соответствующую операции.");
    ConsoleKey key = Console.ReadKey().Key;
    switch (key)
    {
        case ConsoleKey.D1:
            await service.TypeTask();
            break;
        case ConsoleKey.D2:
            await service.ChangeTaskStatus();
            break;
        case ConsoleKey.D3:
            await service.WriteAllTasks();
            break;
        case ConsoleKey.D4:
            await service.DeleteTask();
            break;
        case ConsoleKey.D0:
            exitRequested = true;
            break;
        default: Console.WriteLine("Такого варианта ответа не существует"); break;
    }

}

