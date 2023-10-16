using System;
using System.IO;
using System.Reflection;

class Program
{
    static void InitStructA(int N, int M, out double[][] A)
    {
        int n = 0;
        A = new double[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = new double[M];
            for (int j = 0; j < M; j++)
            {
                A[i][j] = Math.Pow(((double)(6 * n * n + 3 * n + 2)) / ((double)(6 * n * n + 3 * n + 3)), n);
                n++;
            }
        }
    }

    static string BuildString(double[][] A, int N, int M)
    {
        // Задача метода: сформировать строку, которая будет записана в файл
        // Метод формирует большую строку, в которой находятся все значения, содержащиеся в структуре данных A
        string[] stringBuilder = new string[N];
        string builder;

        for (int i = 0; i < N; i++)
        {
            builder = "";
            for (int j = 0; j < M - 1; j++)
            {
                builder += $"{A[i][j]:0.000} ";
            }
            if (i == N - 1)
            {
                builder += $"{A[i][M - 1]:0.000}";
            }
            else
            {
                builder += $"{A[i][M - 1]:0.000}\n";
            }
            stringBuilder[i] = builder;
        }

        string str = string.Join("", stringBuilder);
        return str;
    }

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

    static void Main(string[] args)
    {
        // Считываем N и M
        int N, M;
        do
        {
            Console.WriteLine("Put int 0 < N <= 13");
        } while (!int.TryParse(Console.ReadLine(), out N) || N <= 0 || N > 13);

        do
        {
            Console.WriteLine("Put int 0 < M <= 17");
        } while (!int.TryParse(Console.ReadLine(), out M) || M <= 0 || M > 17);

        // Инициализируем структуру A и создаем строку для записи в файл
        double[][] A;
        InitStructA(N, M, out A);
        string str = BuildString(A, N, M);

        // Считываем имя файла
        string fileName;
        do
        {
            Console.WriteLine("Input not empty name of txt file");
            fileName = Console.ReadLine();
        } while (fileName.Length < 1);

        // Получаем нужный путь для записи, и делаем запись в файл
        string path = GetSolutionPath() + fileName + ".txt";

        using (StreamWriter stream = new StreamWriter(path))
        {
            stream.Write(str);
        }

        Console.WriteLine("You file was successfull created !");


    }
}