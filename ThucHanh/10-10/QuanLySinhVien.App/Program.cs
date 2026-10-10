using QuanLySinhVien.Views;

namespace QuanLySinhVien.App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmQuanLySV());
        }
    }
}
