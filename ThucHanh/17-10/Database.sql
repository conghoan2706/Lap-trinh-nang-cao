IF DB_ID(N'QLSV') IS NULL
    CREATE DATABASE QLSV;
GO

USE QLSV;
GO

IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NOT NULL DROP TABLE dbo.SinhVien;
IF OBJECT_ID(N'dbo.LopHoc', N'U') IS NOT NULL DROP TABLE dbo.LopHoc;
GO

CREATE TABLE dbo.LopHoc
(
    MaLop  NVARCHAR(20)  NOT NULL PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL
);

CREATE TABLE dbo.SinhVien
(
    MaSV        NVARCHAR(20)  NOT NULL PRIMARY KEY,
    HoTen       NVARCHAR(50)  NOT NULL,
    NgaySinh    DATE          NOT NULL,
    GioiTinh    NVARCHAR(10)  NOT NULL,
    Email       NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15)  NOT NULL,
    MaLop       NVARCHAR(20)  NOT NULL,
    Diem        FLOAT         NOT NULL,
    TrangThai   NVARCHAR(20)  NOT NULL,
    CONSTRAINT FK_SinhVien_LopHoc FOREIGN KEY (MaLop) REFERENCES dbo.LopHoc (MaLop)
);
GO

INSERT INTO dbo.LopHoc (MaLop, TenLop) VALUES
(N'CSE0001', N'Kỹ thuật phần mềm 01'),
(N'CSE0002', N'Trí tuệ nhân tạo 01'),
(N'CSE0003', N'Khoa học dữ liệu 01');

INSERT INTO dbo.SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, MaLop, Diem, TrangThai) VALUES
(N'SV000123', N'Nguyễn Văn An',  '2006-08-15', N'Nam', N'an.nv@vju.ac.vn',   N'0912345678', N'CSE0001', 8.5, N'Đang học'),
(N'SV000124', N'Trần Minh Anh',  '2006-01-22', N'Nữ',  N'anh.tm@vju.ac.vn',  N'0987654321', N'CSE0002', 9.0, N'Đang học'),
(N'SV000125', N'Lê Hoàng Bình',  '2006-05-09', N'Nam', N'binh.lh@vju.ac.vn', N'0355556677', N'CSE0001', 7.4, N'Đang học'),
(N'SV000126', N'Đỗ Thị Hồng',    '2006-11-30', N'Nữ',  N'hong.dt@vju.ac.vn', N'0777888999', N'CSE0003', 8.1, N'Đang học');
GO
