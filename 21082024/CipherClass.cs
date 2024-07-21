namespace LibLesson._21082024;

public class CipherClass
{
    public static void Main(string[] args)
    {
        ICipher cipher = new CaesarCipher(3);

        string originalText = "Hello, World!";
        string encodedText = cipher.Encode(originalText);
        string decodedText = cipher.Decode(encodedText);

        Console.WriteLine($"Original Text: {originalText}");
        Console.WriteLine($"Encoded Text: {encodedText}");
        Console.WriteLine($"Decoded Text: {decodedText}");
    }
}

public interface ICipher
{
    string Encode(string input);
    string Decode(string input);
}

public class CaesarCipher : ICipher
{
    private int shift;

    public CaesarCipher(int shift)
    {
        this.shift = shift;
    }

    public string Encode(string input)
    {
        return Process(input, shift);
    }

    public string Decode(string input)
    {
        return Process(input, 26 - shift);
    }

    private string Process(string input, int shift)
    {
        char[] buffer = input.ToCharArray();

        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];

            if (char.IsLetter(letter))
            {
                char offset = char.IsUpper(letter) ? 'A' : 'a';
                letter = (char)((letter + shift - offset) % 26 + offset);
            }

            buffer[i] = letter;
        }

        return new string(buffer);
    }
}