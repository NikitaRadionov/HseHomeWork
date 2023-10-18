using System;
using System.IO;

public class InvalidFileReading : Exception
{
    public InvalidFileReading() : base() { }
    public InvalidFileReading(string message) : base(message) { }
    public InvalidFileReading(string message, Exception inner) : base(message, inner) { }
}


class Program
{
    static string GetSolutionPath()
    {
        // Метод предназначен для получение путя к папке с решением HomeWork
        string exePath = AppDomain.CurrentDomain.BaseDirectory;
        string[] words = exePath.Split("\\");
        string solutionPath = "";
        for (int i = 0; i < words.Length - 5; i++)
        {
            solutionPath += $"{words[i]}\\";
        }
        return solutionPath;
    }

    static void InitStuctB(string data, out double[][]? B)
    {
        // 
        string[] linesOfArray = data.Split("\n");
        int N = linesOfArray.Length;
        int M = linesOfArray[0].Split("  ").Length;
        B = new double[N][];
        for (int i = 0; i < N; i++)
        {
            B[i] = new double[M];
            string[] stringValues = linesOfArray[i].Split("  ");

            if (stringValues.Length != M)
            {
                B = null;
                throw new InvalidFileReading("Incorrect count of values");
            }

            for (int j = 0; j < M; j++)
            {
                double value;
                bool flag = double.TryParse(stringValues[j], out value);
                if (flag)
                {
                    B[i][j] = value;
                }
                else
                {
                    B = null;
                    throw new InvalidFileReading("Invalid data");
                }
            }
        }
    }

    static double[][] CreateStructC(double[][] B)
    {
        int N = B.Length;
        int M = B[0].Length;
        double[][] C = new double[N][];

        for (int i = 0; i < N; i++)
        {
            C[i] = new double[M - 1];
            for (int j = 0; j < M; j++)
            {
                if (i != j)
                {
                    int k = j > i ? j - 1 : j;
                    C[i][k] = B[i][j];
                }
            }
        }
        return C;
    }
    static void Print(double[][] A)
    {
        int N = A.Length;
        int M = A[0].Length;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                Console.Write($"{A[i][j]:0.000} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void Main(string[] args)
    {

        // Считываем имя файла
        string? fileName;
        do
        {
            Console.WriteLine("Input not empty name of txt file");
            fileName = Console.ReadLine();
        } while (fileName == null || fileName.Length < 1);

        // Получаем путь до файла с именем fileName, который расположен на одном уровне с двумя проектами
        string path = GetSolutionPath() + fileName + ".txt";

        // Если файл есть на диске, то работаем с ним, иначе вывод об отсутствии файла
        if (File.Exists(path))
        {
            // Получаем данные из файла в виде большой строки
            string data;
            using (FileStream stream = File.OpenRead(path))
            {
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);

                data = System.Text.Encoding.Default.GetString(array);
            }

            double[][]? B;
            int N, M;
            try
            {
                InitStuctB(data, out B);
                double[][] C = CreateStructC(B);

                Print(B);
                Print(C);
            }
            catch (InvalidFileReading e)
            {
                Console.WriteLine($"Process failed: {e.Message}");
            }

        }
        else
        {
            Console.WriteLine("File does not exist !!!");
        }

    }
}
