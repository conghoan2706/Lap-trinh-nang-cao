using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Nhap a: ");
        string sa = Console.ReadLine();
        if (!int.TryParse(sa, out int a))
        {
            Console.WriteLine("a khong hop le.");
            return;
        }

        Console.Write("Nhap b: ");
        string sb = Console.ReadLine();
        if (!int.TryParse(sb, out int b))
        {
            Console.WriteLine("b khong hop le.");
            return;
        }

        Console.Write("Nhap c (so thuc): ");
        string sc = Console.ReadLine();
        if (!double.TryParse(sc, out double c))
        {
            Console.WriteLine("c khong hop le.");
            return;
        }

        int cNguyen = (int)c;


        dynamic giaTri = a;
        Console.WriteLine("dynamic luc dau = {0}  (kieu {1})", giaTri, giaTri.GetType().Name);
        giaTri = c;
        Console.WriteLine("dynamic sau khi gan c = {0}  (kieu {1})", giaTri, giaTri.GetType().Name);
        dynamic tong = a + b + giaTri;
        Console.WriteLine("dynamic tong a+b+c = {0}  (kieu {1})", tong, tong.GetType().Name);

        Console.WriteLine();
        Console.WriteLine("--- Vi du OOP ---");
        TamGiac tg = new TamGiac(a, b, c);
        tg.Xuat();

        Console.WriteLine();
        Console.WriteLine("--- Hinh chu nhat (OOP + Data Annotation) ---");
        Console.Write("Nhap chieu dai: ");
        if (!double.TryParse(Console.ReadLine(), out double dai))
        {
            Console.WriteLine("Chieu dai khong hop le.");
            return;
        }
        Console.Write("Nhap chieu rong: ");
        if (!double.TryParse(Console.ReadLine(), out double rong))
        {
            Console.WriteLine("Chieu rong khong hop le.");
            return;
        }

        HinhChuNhat hcn = new HinhChuNhat();
        hcn.ChieuDai = dai;
        hcn.ChieuRong = rong;
        if (dai < 0)
            return;
        if (hcn.HopLe())
            hcn.Xuat();
        else
            hcn.XuatLoi();
    }
}
