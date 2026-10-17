namespace QuanLySinhVien.Views
{
    partial class frmQuanLySV
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblLogo;
        private Label lblAppTitle;
        private Label lblTitle;
        private Label lblSub;
        private Panel pnlThongTin;
        private Panel pnlAccent;
        private Label lblThongTin;
        private Label lblMaSV;
        private TextBox txtMaSV;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblLop;
        private ComboBox cboLop;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private Label lblGioiTinh;
        private Panel pnlGioiTinh;
        private RadioButton rdoNam;
        private RadioButton rdoNu;
        private Label lblDiem;
        private NumericUpDown numDiem;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDienThoai;
        private TextBox txtDienThoai;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private Button butThem;
        private Button butSua;
        private Button butXoa;
        private Button butLamMoi;
        private Panel pnlTim;
        private Label lblTuKhoa;
        private TextBox txtTuKhoa;
        private Label lblLocLop;
        private ComboBox cboLocLop;
        private Label lblDiemTu;
        private NumericUpDown numDiemTu;
        private Button butTimKiem;
        private Button butTatCa;
        private Label lblDanhSach;
        private Label lblTong;
        private DataGridView dgvSinhVien;
        private Label lblGoiY;
        private Label lblBatBuoc;
        private ErrorProvider errPMaSV;
        private ErrorProvider erpHoten;
        private ErrorProvider erpEmail;
        private ErrorProvider erpDienThoai;
        private ErrorProvider erpLopHoc;
        private ErrorProvider erpDiem;
        private ErrorProvider erpNgaySinh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlHeader = new Panel();
            lblLogo = new Label();
            lblAppTitle = new Label();
            lblTitle = new Label();
            lblSub = new Label();
            pnlThongTin = new Panel();
            pnlAccent = new Panel();
            lblThongTin = new Label();
            lblMaSV = new Label();
            txtMaSV = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblLop = new Label();
            cboLop = new ComboBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            lblGioiTinh = new Label();
            pnlGioiTinh = new Panel();
            rdoNam = new RadioButton();
            rdoNu = new RadioButton();
            lblDiem = new Label();
            numDiem = new NumericUpDown();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDienThoai = new Label();
            txtDienThoai = new TextBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            butThem = new Button();
            butSua = new Button();
            butXoa = new Button();
            butLamMoi = new Button();
            pnlTim = new Panel();
            lblTuKhoa = new Label();
            txtTuKhoa = new TextBox();
            lblLocLop = new Label();
            cboLocLop = new ComboBox();
            lblDiemTu = new Label();
            numDiemTu = new NumericUpDown();
            butTimKiem = new Button();
            butTatCa = new Button();
            lblDanhSach = new Label();
            lblTong = new Label();
            dgvSinhVien = new DataGridView();
            lblGoiY = new Label();
            lblBatBuoc = new Label();
            errPMaSV = new ErrorProvider(components);
            erpHoten = new ErrorProvider(components);
            erpEmail = new ErrorProvider(components);
            erpDienThoai = new ErrorProvider(components);
            erpLopHoc = new ErrorProvider(components);
            erpDiem = new ErrorProvider(components);
            erpNgaySinh = new ErrorProvider(components);
            pnlHeader.SuspendLayout();
            pnlThongTin.SuspendLayout();
            pnlGioiTinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDiem).BeginInit();
            pnlTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDiemTu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            SuspendLayout();

            pnlHeader.BackColor = Color.FromArgb(15, 55, 100);
            pnlHeader.Controls.Add(lblLogo);
            pnlHeader.Controls.Add(lblAppTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Size = new Size(1140, 52);

            lblLogo.BackColor = Color.FromArgb(234, 88, 12);
            lblLogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(16, 10);
            lblLogo.Size = new Size(32, 32);
            lblLogo.Text = "S";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(58, 14);
            lblAppTitle.Text = "Ứng dụng quản lý sinh viên";

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 45, 80);
            lblTitle.Location = new Point(20, 64);
            lblTitle.Text = "QUẢN LÝ SINH VIÊN";

            lblSub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblSub.Location = new Point(480, 72);
            lblSub.Size = new Size(640, 24);
            lblSub.Text = "Bài tập Windows Forms  •  Quan hệ Lớp 1 — n Sinh viên";
            lblSub.TextAlign = ContentAlignment.MiddleRight;

            pnlThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.Controls.Add(pnlAccent);
            pnlThongTin.Controls.Add(lblThongTin);
            pnlThongTin.Controls.Add(lblMaSV);
            pnlThongTin.Controls.Add(txtMaSV);
            pnlThongTin.Controls.Add(lblHoTen);
            pnlThongTin.Controls.Add(txtHoTen);
            pnlThongTin.Controls.Add(lblLop);
            pnlThongTin.Controls.Add(cboLop);
            pnlThongTin.Controls.Add(lblNgaySinh);
            pnlThongTin.Controls.Add(dtpNgaySinh);
            pnlThongTin.Controls.Add(lblGioiTinh);
            pnlThongTin.Controls.Add(pnlGioiTinh);
            pnlThongTin.Controls.Add(lblDiem);
            pnlThongTin.Controls.Add(numDiem);
            pnlThongTin.Controls.Add(lblEmail);
            pnlThongTin.Controls.Add(txtEmail);
            pnlThongTin.Controls.Add(lblDienThoai);
            pnlThongTin.Controls.Add(txtDienThoai);
            pnlThongTin.Controls.Add(lblTrangThai);
            pnlThongTin.Controls.Add(cboTrangThai);
            pnlThongTin.Controls.Add(butThem);
            pnlThongTin.Controls.Add(butSua);
            pnlThongTin.Controls.Add(butXoa);
            pnlThongTin.Controls.Add(butLamMoi);
            pnlThongTin.Location = new Point(20, 108);
            pnlThongTin.Size = new Size(1100, 268);

            pnlAccent.BackColor = Color.FromArgb(234, 88, 12);
            pnlAccent.Location = new Point(16, 16);
            pnlAccent.Size = new Size(4, 22);

            lblThongTin.AutoSize = true;
            lblThongTin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblThongTin.ForeColor = Color.FromArgb(15, 45, 80);
            lblThongTin.Location = new Point(28, 16);
            lblThongTin.Text = "Thông tin sinh viên";

            lblMaSV.AutoSize = true;
            lblMaSV.Location = new Point(20, 58);
            lblMaSV.Text = "Mã sinh viên *";
            txtMaSV.Location = new Point(130, 54);
            txtMaSV.MaxLength = 8;
            txtMaSV.CharacterCasing = CharacterCasing.Upper;
            txtMaSV.PlaceholderText = "SV000123";
            txtMaSV.Size = new Size(200, 27);
            txtMaSV.TabIndex = 0;
            txtMaSV.Enter += txtMaSV_Enter;
            txtMaSV.Leave += txtMaSV_Leave;
            txtMaSV.KeyDown += txtMaSV_KeyDown;
            txtMaSV.TextChanged += txtMaSV_TextChanged;

            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(360, 58);
            lblHoTen.Text = "Họ và tên *";
            txtHoTen.Location = new Point(460, 54);
            txtHoTen.Size = new Size(220, 27);
            txtHoTen.TabIndex = 1;
            txtHoTen.KeyDown += txtMaSV_KeyDown;
            txtHoTen.TextChanged += txtHoTen_TextChanged;

            lblLop.AutoSize = true;
            lblLop.Location = new Point(710, 58);
            lblLop.Text = "Lớp học *";
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(800, 54);
            cboLop.Size = new Size(270, 28);
            cboLop.TabIndex = 2;
            cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;

            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(20, 110);
            lblNgaySinh.Text = "Ngày sinh";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Location = new Point(130, 106);
            dtpNgaySinh.Size = new Size(200, 27);
            dtpNgaySinh.TabIndex = 3;

            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Location = new Point(360, 110);
            lblGioiTinh.Text = "Giới tính";
            pnlGioiTinh.Controls.Add(rdoNam);
            pnlGioiTinh.Controls.Add(rdoNu);
            pnlGioiTinh.Location = new Point(460, 104);
            pnlGioiTinh.Size = new Size(220, 32);
            pnlGioiTinh.TabIndex = 4;
            rdoNam.AutoSize = true;
            rdoNam.Checked = true;
            rdoNam.Location = new Point(8, 6);
            rdoNam.TabIndex = 4;
            rdoNam.Text = "Nam";
            rdoNu.AutoSize = true;
            rdoNu.Location = new Point(90, 6);
            rdoNu.TabIndex = 5;
            rdoNu.Text = "Nữ";

            lblDiem.AutoSize = true;
            lblDiem.Location = new Point(710, 110);
            lblDiem.Text = "Điểm *";
            numDiem.DecimalPlaces = 1;
            numDiem.Increment = 0.1M;
            numDiem.Location = new Point(800, 106);
            numDiem.Maximum = 10;
            numDiem.Size = new Size(120, 27);
            numDiem.TabIndex = 6;

            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 162);
            lblEmail.Text = "Email *";
            txtEmail.Location = new Point(130, 158);
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 7;
            txtEmail.KeyDown += txtMaSV_KeyDown;
            txtEmail.TextChanged += txtEmail_TextChanged;

            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(360, 162);
            lblDienThoai.Text = "Điện thoại *";
            txtDienThoai.Location = new Point(460, 158);
            txtDienThoai.MaxLength = 10;
            txtDienThoai.Size = new Size(220, 27);
            txtDienThoai.TabIndex = 8;
            txtDienThoai.KeyDown += txtMaSV_KeyDown;
            txtDienThoai.TextChanged += txtDienThoai_TextChanged;

            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(710, 162);
            lblTrangThai.Text = "Trạng thái";
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Location = new Point(800, 158);
            cboTrangThai.Size = new Size(270, 28);
            cboTrangThai.TabIndex = 9;

            StyleButton(butThem, Color.FromArgb(22, 163, 74));
            butThem.Location = new Point(560, 214);
            butThem.Size = new Size(110, 36);
            butThem.TabIndex = 10;
            butThem.Text = "Thêm";
            butThem.Click += butThem_Click;

            StyleButton(butSua, Color.FromArgb(37, 99, 235));
            butSua.Location = new Point(680, 214);
            butSua.Size = new Size(110, 36);
            butSua.TabIndex = 11;
            butSua.Text = "Sửa";
            butSua.Click += butSua_Click;

            StyleButton(butXoa, Color.FromArgb(220, 68, 68));
            butXoa.Location = new Point(800, 214);
            butXoa.Size = new Size(110, 36);
            butXoa.TabIndex = 12;
            butXoa.Text = "Xóa";
            butXoa.Click += butXoa_Click;

            StyleButton(butLamMoi, Color.FromArgb(100, 116, 139));
            butLamMoi.Location = new Point(920, 214);
            butLamMoi.Size = new Size(150, 36);
            butLamMoi.TabIndex = 13;
            butLamMoi.Text = "Làm mới";
            butLamMoi.Click += butLamMoi_Click;

            pnlTim.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTim.BackColor = Color.White;
            pnlTim.Controls.Add(lblTuKhoa);
            pnlTim.Controls.Add(txtTuKhoa);
            pnlTim.Controls.Add(lblLocLop);
            pnlTim.Controls.Add(cboLocLop);
            pnlTim.Controls.Add(lblDiemTu);
            pnlTim.Controls.Add(numDiemTu);
            pnlTim.Controls.Add(butTimKiem);
            pnlTim.Controls.Add(butTatCa);
            pnlTim.Location = new Point(20, 388);
            pnlTim.Size = new Size(1100, 60);

            lblTuKhoa.AutoSize = true;
            lblTuKhoa.Location = new Point(16, 20);
            lblTuKhoa.Text = "Từ khóa";
            txtTuKhoa.Location = new Point(80, 16);
            txtTuKhoa.PlaceholderText = "Mã, họ tên, email hoặc điện thoại";
            txtTuKhoa.Size = new Size(250, 27);
            txtTuKhoa.TabIndex = 14;

            lblLocLop.AutoSize = true;
            lblLocLop.Location = new Point(348, 20);
            lblLocLop.Text = "Lớp";
            cboLocLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLop.Location = new Point(384, 16);
            cboLocLop.Size = new Size(220, 28);
            cboLocLop.TabIndex = 15;
            cboLocLop.SelectedIndexChanged += cboLocLop_SelectedIndexChanged;

            lblDiemTu.AutoSize = true;
            lblDiemTu.Location = new Point(620, 20);
            lblDiemTu.Text = "Điểm từ";
            numDiemTu.DecimalPlaces = 1;
            numDiemTu.Increment = 0.5M;
            numDiemTu.Location = new Point(684, 16);
            numDiemTu.Maximum = 10;
            numDiemTu.Size = new Size(80, 27);
            numDiemTu.TabIndex = 16;

            StyleButton(butTimKiem, Color.FromArgb(30, 64, 120));
            butTimKiem.Location = new Point(780, 13);
            butTimKiem.Size = new Size(120, 34);
            butTimKiem.TabIndex = 17;
            butTimKiem.Text = "Tìm kiếm";
            butTimKiem.Click += butTimKiem_Click;

            butTatCa.BackColor = Color.White;
            butTatCa.Cursor = Cursors.Hand;
            butTatCa.FlatStyle = FlatStyle.Flat;
            butTatCa.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            butTatCa.FlatAppearance.BorderSize = 1;
            butTatCa.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            butTatCa.ForeColor = Color.FromArgb(30, 64, 100);
            butTatCa.Location = new Point(910, 13);
            butTatCa.Size = new Size(160, 34);
            butTatCa.TabIndex = 18;
            butTatCa.Text = "Hiển thị tất cả";
            butTatCa.Click += butTatCa_Click;

            lblDanhSach.AutoSize = true;
            lblDanhSach.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDanhSach.ForeColor = Color.FromArgb(15, 45, 80);
            lblDanhSach.Location = new Point(20, 460);
            lblDanhSach.Text = "Danh sách sinh viên";

            lblTong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTong.AutoSize = true;
            lblTong.BackColor = Color.FromArgb(220, 242, 228);
            lblTong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTong.ForeColor = Color.FromArgb(22, 120, 70);
            lblTong.Location = new Point(960, 462);
            lblTong.Padding = new Padding(8, 4, 8, 4);
            lblTong.Text = "Tổng số: 0 sinh viên";

            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.BackgroundColor = Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSinhVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSinhVien.ColumnHeadersHeight = 36;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.GridColor = Color.FromArgb(226, 232, 240);
            dgvSinhVien.Location = new Point(20, 492);
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.RowTemplate.Height = 32;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.Size = new Size(1100, 210);
            dgvSinhVien.TabIndex = 19;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 240, 248);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 64, 100);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 248);
            dgvSinhVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSinhVien.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 45, 80);
            dgvSinhVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSinhVien.CellClick += dgvSinhVien_CellClick;
            dgvSinhVien.CellFormatting += dgvSinhVien_CellFormatting;

            lblGoiY.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblGoiY.AutoSize = true;
            lblGoiY.ForeColor = Color.FromArgb(100, 116, 139);
            lblGoiY.Location = new Point(20, 714);
            lblGoiY.Text = "Chọn một dòng để xem, sửa hoặc xóa thông tin sinh viên.";

            lblBatBuoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblBatBuoc.AutoSize = true;
            lblBatBuoc.ForeColor = Color.FromArgb(100, 116, 139);
            lblBatBuoc.Location = new Point(860, 714);
            lblBatBuoc.Text = "Các trường có dấu * là bắt buộc.";

            WireError(errPMaSV);
            WireError(erpHoten);
            WireError(erpEmail);
            WireError(erpDienThoai);
            WireError(erpLopHoc);
            WireError(erpDiem);
            WireError(erpNgaySinh);

            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(1140, 748);
            Controls.Add(pnlHeader);
            Controls.Add(lblTitle);
            Controls.Add(lblSub);
            Controls.Add(pnlThongTin);
            Controls.Add(pnlTim);
            Controls.Add(lblDanhSach);
            Controls.Add(lblTong);
            Controls.Add(dgvSinhVien);
            Controls.Add(lblGoiY);
            Controls.Add(lblBatBuoc);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(1160, 820);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ứng dụng quản lý sinh viên";
            FormClosing += frmQuanLySV_FormClosing;
            Load += frmQuanLySV_Load;
            Shown += frmQuanLySV_Shown;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlThongTin.ResumeLayout(false);
            pnlThongTin.PerformLayout();
            pnlGioiTinh.ResumeLayout(false);
            pnlGioiTinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDiem).EndInit();
            pnlTim.ResumeLayout(false);
            pnlTim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDiemTu).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StyleButton(Button button, Color back)
        {
            button.BackColor = back;
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.ForeColor = Color.White;
        }

        private void WireError(ErrorProvider provider)
        {
            provider.ContainerControl = this;
            provider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }
    }
}
