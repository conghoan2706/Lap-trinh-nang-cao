using System;
using System.Collections.Generic;

class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public Student()
    {
        Id = "";
        Name = "";
        Age = 0;
    }

    public Student(string id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    string[] TachTen()
    {
        return (Name ?? "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public string Ho
    {
        get
        {
            string[] p = TachTen();
            return p.Length == 0 ? "" : p[0];
        }
    }

    public string Ten
    {
        get
        {
            string[] p = TachTen();
            return p.Length == 0 ? "" : p[p.Length - 1];
        }
    }

    public override string ToString()
    {
        return string.Format("MSV: {0}  |  Ho ten: {1}  |  Tuoi: {2}  |  Ho: {3}  |  Ten: {4}",
            Id, Name, Age, Ho, Ten);
    }
}

class StudentDAO
{
    List<Student> ds = new List<Student>();

    public bool Add(Student student)
    {
        if (student == null || string.IsNullOrWhiteSpace(student.Id))
            return false;
        if (GetById(student.Id) != null)
            return false;
        ds.Add(student);
        return true;
    }

    public bool Edit(Student student)
    {
        if (student == null || string.IsNullOrWhiteSpace(student.Id))
            return false;
        Student cu = GetById(student.Id);
        if (cu == null)
            return false;
        cu.Name = student.Name;
        cu.Age = student.Age;
        return true;
    }

    public bool Delete(string id)
    {
        Student sv = GetById(id);
        if (sv == null)
            return false;
        ds.Remove(sv);
        return true;
    }

    public List<Student> GetAlls()
    {
        return new List<Student>(ds);
    }

    public Student GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return null;
        id = id.Trim();
        return ds.Find(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public List<Student> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new List<Student>();
        name = name.Trim();
        return ds.FindAll(s =>
            s.Ten.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0
            || s.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public List<Student> GetByHo(string ho)
    {
        if (string.IsNullOrWhiteSpace(ho))
            return new List<Student>();
        ho = ho.Trim();
        return ds.FindAll(s => s.Ho.IndexOf(ho, StringComparison.OrdinalIgnoreCase) >= 0);
    }
}

class Program
{
    static StudentDAO dao = new StudentDAO();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1.Add  2.Edit  3.Delete  4.GetByName  5.GetByHo  6.GetAlls  7.GetById  0.Thoat");
            Console.Write("Chon: ");
            switch (Console.ReadLine())
            {
                case "1": Them(); break;
                case "2": Sua(); break;
                case "3": Xoa(); break;
                case "4": InDs(dao.GetByName(NhapChuoi("Nhap ten: ")), "--- GetByName ---"); break;
                case "5": InDs(dao.GetByHo(NhapChuoi("Nhap ho: ")), "--- GetByHo ---"); break;
                case "6": InDs(dao.GetAlls(), "--- GetAlls ---"); break;
                case "7": HienThi(dao.GetById(NhapChuoi("Nhap MSV: "))); break;
                case "0": return;
                default: Console.WriteLine("Khong hop le."); break;
            }
        }
    }

    static void Them()
    {
        Student sv = NhapStudent(true);
        Console.WriteLine(dao.Add(sv) ? "Da them." : "Khong them duoc (trung MSV).");
    }

    static void Sua()
    {
        Student sv = NhapStudent(true);
        Console.WriteLine(dao.Edit(sv) ? "Da sua." : "Khong tim thay.");
    }

    static void Xoa()
    {
        Console.WriteLine(dao.Delete(NhapChuoi("Nhap MSV can xoa: ")) ? "Da xoa." : "Khong tim thay.");
    }

    static Student NhapStudent(bool coId)
    {
        Student sv = new Student();
        if (coId)
            sv.Id = NhapChuoi("Nhap MSV: ");
        sv.Name = NhapChuoi("Nhap ho ten: ");
        sv.Age = NhapTuoi();
        return sv;
    }

    static string NhapChuoi(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s))
                return s.Trim();
            Console.WriteLine("Nhap sai, nhap lai!");
        }
    }

    static int NhapTuoi()
    {
        while (true)
        {
            Console.Write("Nhap tuoi: ");
            int t;
            if (int.TryParse(Console.ReadLine(), out t) && t > 0 && t <= 100)
                return t;
            Console.WriteLine("Nhap sai, nhap lai!");
        }
    }

    static void HienThi(Student sv)
    {
        if (sv == null)
            Console.WriteLine("Khong tim thay.");
        else
            Console.WriteLine(sv);
    }

    static void InDs(List<Student> list, string tieuDe)
    {
        Console.WriteLine(tieuDe);
        if (list == null || list.Count == 0)
        {
            Console.WriteLine("Khong co ket qua.");
            return;
        }
        foreach (Student s in list)
            Console.WriteLine(s);
    }
}
