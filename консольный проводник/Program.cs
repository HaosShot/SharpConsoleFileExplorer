namespace ConsoleApp3
{
    public class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            while (true)
            {
                Console.WriteLine($"Текущая директория: {currentDirectory}\n");
                Console.WriteLine("Команды:\nls - чтение текущей директории\ncd <dir> - перейти в директорию\nmkdir <dir> - создать директорию\ntouch <file> - создать новый файл\nedit <file> - открыть файл для редактирования\nexit - завершение работы\n");
                Console.WriteLine();
                Console.WriteLine("Введите команду: ");
                string input = Console.ReadLine();
                string[] parts = input.Split(" ", 2);
                string command = parts[0];
                string argument = parts.Length > 1 ? parts[1] : string.Empty;

                switch (command)
                {
                    case "ls":
                        ListDirectory(currentDirectory);
                        break;

                    case "cd":
                        if (Directory.Exists(Path.Combine(currentDirectory, argument)))
                        {
                            currentDirectory = Path.Combine(currentDirectory, argument);
                        }
                        else
                        {
                            Console.WriteLine("Директории не существует");
                        }
                        break;

                    case "mkdir":
                        Directory.CreateDirectory(Path.Combine(currentDirectory, argument));
                        Console.WriteLine($"Создана директория: {argument}");
                        break;

                    case "touch":
                        File.Create(Path.Combine(currentDirectory, argument)).Close();
                        Console.WriteLine($"Создан файл: {argument}");
                        break;

                    case "edit":
                        EditFile(Path.Combine(currentDirectory, argument));
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
        static void ListDirectory(string path)
        {
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            Console.WriteLine("Директории: ");
            foreach (var dir in directories)
            {
                Console.WriteLine($"{Path.GetFileName(dir)}");
            }

            Console.WriteLine("Файлы: ");
            foreach (var file in files)
            {
                Console.WriteLine($"{Path.GetFileName(file)}");
            }
        }
        static void EditFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine("Текущее содержимое файла: ");
                Console.WriteLine(content);
                Console.WriteLine("Введите новое содержимое файла: ");
                string newContent = Console.ReadLine();
                if (!string.IsNullOrEmpty(newContent))
                {
                    File.WriteAllText(filePath, newContent);
                    Console.WriteLine("Файл обновлен");
                }
                else
                {
                    Console.WriteLine("Редактирование отменено");
                }
            }
            else
            {
                Console.WriteLine("Файл не существует");
            }
        }
    }
}
