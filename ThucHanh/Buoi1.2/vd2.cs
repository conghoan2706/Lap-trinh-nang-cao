using System;

class TamGiac
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public TamGiac(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public bool HopLe()
    {
        return A + B > C && A + C > B && B + C > A;
    }

    public double ChuVi()
    {
        return A + B + C;
    }

    public void Xuat()
    {
        Console.WriteLine("Tam giac: a={0}, b={1}, c={2}", A, B, C);
        if (HopLe())
            Console.WriteLine("Hop le. Chu vi = {0}", ChuVi());
        else
            Console.WriteLine("Khong tao thanh tam giac.");
    }
}
