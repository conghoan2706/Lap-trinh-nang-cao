using System;

class Program
{
    static void Main()
    {
        int a;
        int b;
        int c;

        // Nhap a
        while (true)
        {
            Console.Write("Nhap a: ");

            if (int.TryParse(Console.ReadLine(), out a))
            {
                break;
            }

            Console.WriteLine("Nhap sai, nhap lai!");
        }

        // Nhap b
        while (true)
        {
            Console.Write("Nhap b: ");

            if (int.TryParse(Console.ReadLine(), out b))
            {
                break;
            }

            Console.WriteLine("Nhap sai, nhap lai!");
        }

        // Nhap c
        while (true)
        {
            Console.Write("Nhap c: ");

            if (int.TryParse(Console.ReadLine(), out c))
            {
                break;
            }

            Console.WriteLine("Nhap sai, nhap lai!");
        }

        Console.WriteLine("a+b+c=" + (a + b + c));
    }
}
