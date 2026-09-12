using System;

interface Hinh
{
    double GetDienTich();
    double GetChuVi();
    void Nhap();
    void HienThi();
}

class HinhTron : Hinh
{
    private double banKinh;

    public double BanKinh
    {
        get { return banKinh; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Ban kinh phai lon hon 0.");
            else
                banKinh = value;
        }
    }

    public HinhTron()
    {
        banKinh = 0;
    }

    public HinhTron(double banKinh)
    {
        BanKinh = banKinh;
    }

    public double GetDienTich()
    {
        return Math.PI * banKinh * banKinh;
    }

    public double GetChuVi()
    {
        return 2 * Math.PI * banKinh;
    }

    public void Nhap()
    {
        Console.WriteLine("--- Hinh tron ---");
        while (true)
        {
            Console.Write("Nhap ban kinh: ");
            if (double.TryParse(Console.ReadLine(), out double r) && r > 0)
            {
                BanKinh = r;
                break;
            }
            Console.WriteLine("Nhap sai, nhap lai!");
        }
    }

    public void HienThi()
    {
        Console.WriteLine("Hinh tron: R = {0}", BanKinh);
        Console.WriteLine("Dien tich = {0:0.##}", GetDienTich());
        Console.WriteLine("Chu vi    = {0:0.##}", GetChuVi());
    }
}

class HinhChuNhat : Hinh
{
    private double dai;
    private double rong;

    public double ChieuDai
    {
        get { return dai; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Chieu dai phai lon hon 0.");
            else
                dai = value;
        }
    }

    public double ChieuRong
    {
        get { return rong; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Chieu rong phai lon hon 0.");
            else
                rong = value;
        }
    }

    public HinhChuNhat()
    {
        dai = 0;
        rong = 0;
    }

    public HinhChuNhat(double dai, double rong)
    {
        ChieuDai = dai;
        ChieuRong = rong;
    }

    public double GetDienTich()
    {
        return dai * rong;
    }

    public double GetChuVi()
    {
        return 2 * (dai + rong);
    }

    public void Nhap()
    {
        Console.WriteLine("--- Hinh chu nhat ---");
        while (true)
        {
            Console.Write("Nhap chieu dai: ");
            if (double.TryParse(Console.ReadLine(), out double d) && d > 0)
            {
                ChieuDai = d;
                break;
            }
            Console.WriteLine("Nhap sai, nhap lai!");
        }

        while (true)
        {
            Console.Write("Nhap chieu rong: ");
            if (double.TryParse(Console.ReadLine(), out double r) && r > 0)
            {
                ChieuRong = r;
                break;
            }
            Console.WriteLine("Nhap sai, nhap lai!");
        }
    }

    public void HienThi()
    {
        Console.WriteLine("Hinh chu nhat: dai = {0}, rong = {1}", ChieuDai, ChieuRong);
        Console.WriteLine("Dien tich = {0:0.##}", GetDienTich());
        Console.WriteLine("Chu vi    = {0:0.##}", GetChuVi());
    }
}

class HinhTamGiac : Hinh
{
    private double a;
    private double b;
    private double c;

    public double A
    {
        get { return a; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Canh a phai lon hon 0.");
            else
                a = value;
        }
    }

    public double B
    {
        get { return b; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Canh b phai lon hon 0.");
            else
                b = value;
        }
    }

    public double C
    {
        get { return c; }
        set
        {
            if (value <= 0)
                Console.WriteLine("Canh c phai lon hon 0.");
            else
                c = value;
        }
    }

    public HinhTamGiac()
    {
        a = 0;
        b = 0;
        c = 0;
    }

    public HinhTamGiac(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
        if (!IsTamGiac())
            Console.WriteLine("3 canh khong tao thanh tam giac.");
    }

    public bool IsTamGiac()
    {
        return a > 0 && b > 0 && c > 0
            && a + b > c
            && a + c > b
            && b + c > a;
    }

    public double GetDienTich()
    {
        double p = GetChuVi() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public double GetChuVi()
    {
        return a + b + c;
    }

    public void Nhap()
    {
        Console.WriteLine("--- Hinh tam giac ---");
        while (true)
        {
            while (true)
            {
                Console.Write("Nhap canh a: ");
                if (double.TryParse(Console.ReadLine(), out double x) && x > 0)
                {
                    A = x;
                    break;
                }
                Console.WriteLine("Nhap sai, nhap lai!");
            }

            while (true)
            {
                Console.Write("Nhap canh b: ");
                if (double.TryParse(Console.ReadLine(), out double y) && y > 0)
                {
                    B = y;
                    break;
                }
                Console.WriteLine("Nhap sai, nhap lai!");
            }

            while (true)
            {
                Console.Write("Nhap canh c: ");
                if (double.TryParse(Console.ReadLine(), out double z) && z > 0)
                {
                    C = z;
                    break;
                }
                Console.WriteLine("Nhap sai, nhap lai!");
            }

            if (IsTamGiac())
                break;

            Console.WriteLine("3 canh khong tao thanh tam giac, nhap lai!");
        }
    }

    public void HienThi()
    {
        Console.WriteLine("Hinh tam giac: a = {0}, b = {1}, c = {2}", A, B, C);
        if (!IsTamGiac())
        {
            Console.WriteLine("Khong hop le (IsTamGiac = false).");
            return;
        }
        Console.WriteLine("Dien tich = {0:0.##}", GetDienTich());
        Console.WriteLine("Chu vi    = {0:0.##}", GetChuVi());
    }
}

class ChuongTrinhHinh
{
    static void Main()
    {
        Chay();
    }

    public static void Chay()
    {
        Hinh[] dsHinh = new Hinh[3];
        dsHinh[0] = new HinhTron();
        dsHinh[1] = new HinhChuNhat();
        dsHinh[2] = new HinhTamGiac();

        Console.WriteLine("=== Da hinh: Hinh tron, Hinh chu nhat, Hinh tam giac ===");
        for (int i = 0; i < dsHinh.Length; i++)
        {
            dsHinh[i].Nhap();
            Console.WriteLine();
            dsHinh[i].HienThi();
            Console.WriteLine();
        }
    }
}
