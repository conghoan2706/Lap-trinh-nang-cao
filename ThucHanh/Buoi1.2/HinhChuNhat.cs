using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

class HinhChuNhat
{
    private double dai;

    [Range(0, double.MaxValue, ErrorMessage = "Chieu rong phai lon hon hoac bang 0")]
    public double ChieuRong { get; set; }

    public double ChieuDai
    {
        get { return dai; }
        set
        {
            if (value < 0)
                Console.WriteLine("Chieu dai phai lon hon hoac bang 0");
            else
                dai = value;
        }
    }

    public double DienTich()
    {
        return ChieuDai * ChieuRong;
    }

    public bool HopLe()
    {
        var ketQua = new List<ValidationResult>();
        var ctx = new ValidationContext(this);
        return Validator.TryValidateObject(this, ctx, ketQua, true);
    }

    public void XuatLoi()
    {
        var ketQua = new List<ValidationResult>();
        var ctx = new ValidationContext(this);
        if (!Validator.TryValidateObject(this, ctx, ketQua, true))
        {
            foreach (var loi in ketQua)
                Console.WriteLine(loi.ErrorMessage);
        }
    }

    public void Xuat()
    {
        Console.WriteLine("Hinh chu nhat: dai={0}, rong={1}, dien tich={2}",
            ChieuDai, ChieuRong, DienTich());
    }
}
