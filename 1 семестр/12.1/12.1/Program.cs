using System;
using System.Text;

public class CaesarDecryptorRussian
{
    // Шифр Цезаря
    public static string Decrypt(string ciphertext, int shift)
    {
        StringBuilder decryptedText = new StringBuilder();
        string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        foreach (char c in ciphertext.ToLower())
        {
            int index = alphabet.IndexOf(c);
            if (index != -1) 
            {
                int decryptedIndex = (index + shift + alphabet.Length) % alphabet.Length;
                decryptedText.Append(alphabet[decryptedIndex]);
            }
            else
            {
                decryptedText.Append(c); 
            }
        }
        return decryptedText.ToString();
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите зашифрованный текст:");
        string encryptedText = Console.ReadLine();

        Console.WriteLine("Введите сдвиг:");
        if (int.TryParse(Console.ReadLine(), out int shift))
        {
            string decryptedText = Decrypt(encryptedText, shift);
            Console.WriteLine("Расшифрованный текст: " + decryptedText);
        }
        else
        {
            Console.WriteLine("Неверный формат сдвига.");
        }
    }
}