using System;
using System.IO;

public class FileCopier
{
    public static void CopyFile(string sourceFileName, string destinationFileName)
    {
        if (!File.Exists(sourceFileName))
        {
            throw new FileNotFoundException("Исходный файл не найден.", sourceFileName);
        }

        if (File.Exists(destinationFileName))
        {
            File.Delete(destinationFileName);
        }

        try
        {
            File.Copy(sourceFileName, destinationFileName);
            Console.WriteLine($"Файл '{sourceFileName}' успешно скопирован в '{destinationFileName}'");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при копировании файла: {ex.Message}", ex);
        }
    }

    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Использование: FileCopier <исходный файл> <файл назначения>");
            return;
        }

        string sourceFile = args[0];
        string destFile = args[1];

        try
        {
            CopyFile(sourceFile, destFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}