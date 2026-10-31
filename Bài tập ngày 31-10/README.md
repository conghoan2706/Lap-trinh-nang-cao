# Bài tập ngày 31/10 – Quản lý đầu sách và bản sao

## Mở và chạy

Mở `QuanLyThuVien.sln` bằng Visual Studio 2022 có .NET 8 và Windows Forms. Chọn dự án `QuanLyThuVien` làm Startup Project rồi nhấn F5.

Hoặc mở PowerShell tại thư mục bài ngày 31-10:

```powershell
dotnet run --project .\QuanLyThuVien\QuanLyThuVien.csproj
```

Ứng dụng dùng Entity Framework Core 8.0.8 + SQLite. Lần đầu tự tạo `QuanLyThuVien.db` bên cạnh chương trình, nạp 4 thể loại và sách B006 có 5 bản sao làm dữ liệu minh họa. Dữ liệu lưu qua các lần chạy. Không cần tài khoản hay máy chủ SQL. Khi mang sang máy khác, cần khôi phục gói NuGet lần đầu.

## Cấu trúc theo mẫu đã gửi

Đã đọc các lớp trong Downloads/Entity, Downloads/Business, Downloads/Reposistory và bài quản lý sinh viên ở các buổi trước. Giữ namespace `QuanLyThuVienDAO`, tên Entity và cách chia tầng; giữ cách viết tên thư mục `Reposistory` của mẫu.

Ba thực thể được hiểu là **Book, BookCopy, Category** vì mẫu có đủ ba lớp này, giao diện cần thể loại để lọc và nhập sách.

| Tầng | Tệp/lớp | Trách nhiệm |
|---|---|---|
| Entity | Book, BookCopy, Category | Thuộc tính, khóa, quan hệ, ràng buộc dữ liệu |
| DAL | QLTVDBContext | Kết nối, ánh xạ, khóa ngoại |
| DAL/Reposistory | BookRepository | GetAll, GetById, GetByCategoryId, Search, Add, Update, Delete |
| DAL/Reposistory | BookCopyRepository | GetAll, GetById, GetByBookId, Add, Update, Delete, NextSequence |
| DAL/Reposistory | CategoryRepository | GetAll, GetById, Add, Update, Delete |
| Business | ManageBookBuss | Kiểm tra sách, thêm sách kèm bản sao, nhập bổ sung, phiếu nhập có giao dịch |
| Business | BookCopyBuss | Kiểm tra trạng thái/tình trạng, thêm/sửa/xóa bản sao |
| Business | CategoryBuss | Kiểm tra thể loại, truy xuất/thêm/sửa/xóa |
| Views | ManageBookForm | Giao diện Windows Forms và sự kiện nút bấm |

Repository nhận cùng một `QLTVDBContext` qua hàm khởi tạo. Không giữ `DBReposistory` từ mẫu vì lớp đó tạo context riêng nên giao dịch không bao phủ các repository khác. Giao dịch hiện đặt ở Business, dùng chung context, hoàn tác và ném lỗi lên giao diện.

## Phương thức cho giao diện

| Thao tác | Phương thức Business |
|---|---|
| Tải thể loại | CategoryBuss.GetAll() |
| Tìm theo mã/tên/tác giả và thể loại | ManageBookBuss.Search(keyword, categoryId) |
| Làm mới | Search(null, null) |
| Chọn đầu sách | GetById(id), BookCopyBuss.GetByBookId(id) |
| Thêm đầu sách và bản sao ban đầu | ManageBookBuss.Add(book) / AddBook(book) |
| Lưu chỉnh sửa | ManageBookBuss.Update(book) |
| Xóa đầu sách | ManageBookBuss.Delete(id) |
| Làm trống | Xóa ô nhập trong Views, chuyển sang chế độ thêm |
| Hai nút nhập thêm bản sao | ManageBookBuss.ImportCopies(bookId, quantity) |
| Cập nhật bản sao | BookCopyBuss.Update(copy) |
| Xóa bản sao | BookCopyBuss.Delete(copyId) |
| Nhập phiếu gồm nhiều đầu sách | ManageBookBuss.ImportBill(books) |

## Quy tắc

- Nhấn **Làm trống** trước khi thêm đầu sách mới. Số bản sao được nhập khi thêm mới; khi chọn sách, ô này chỉ đọc. Dùng nút nhập bổ sung để tăng số lượng.
- Mã bản sao bắt đầu từ `B006.001`; lấy hậu tố số lớn nhất hiện có cộng 1, không dùng số lượng dòng và không so sánh chuỗi để tìm số lớn nhất.
- Số bản sao luôn cập nhật cùng thao tác thêm/xóa bản sao. Sửa thông tin đầu sách không được tùy ý ghi đè số lượng.
- Xóa đầu sách phải xóa hết bản sao trước, theo quy tắc của mẫu. Bản sao đang mượn không được xóa. Thể loại còn sách không được xóa.
- Trạng thái: Sẵn sàng, Đang mượn, Ngừng phục vụ. Tình trạng: Mới, Tốt, Cũ, Hỏng, Mất. Bản hỏng/mất không thể sẵn sàng.
- Mỗi lần nhập tối đa 1000 bản sao. Mã sách tối đa 20 ký tự; khi sinh bản sao, tổng độ dài mã bản sao cũng phải không quá 20 ký tự, nếu vượt thì hoàn tác.
- Đã sửa lỗi kiểm tra mã trùng/thể loại bị đảo điều kiện, thêm sách hai lần, thiếu Condition, nuốt ngoại lệ và giao dịch khác context trong mẫu.

## Kiểm tra

```powershell
dotnet build .\QuanLyThuVien.sln -p:NuGetAudit=false
dotnet run --project .\KiemThu\KiemThu.csproj -p:NuGetAudit=false
```

Kiểm tra dùng cơ sở dữ liệu SQLite tạm trong bộ nhớ, không thay đổi dữ liệu ứng dụng. Bao gồm thêm/sửa/xóa, tìm kiếm kết hợp, mã trùng, sinh mã sau khi xóa, số bản sao, trạng thái không hợp lệ, hoàn tác toàn bộ phiếu nhập khi lỗi và khởi tạo giao diện.
