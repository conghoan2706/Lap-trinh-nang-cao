using System.ComponentModel.DataAnnotations;
using QuanLySinhVien.BUL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Views
{
    public partial class frmQuanLySV : Form
    {
        private readonly SinhVienBUL svb = new SinhVienBUL();
        private readonly LopBus lopBus = new LopBus();
        private readonly List<LopHoc> lopHocs;
        private List<SinhVien> sinhViens = new List<SinhVien>();
        private bool ready;
        private bool showing;
        private string lastLookup = "";

        public frmQuanLySV()
        {
            lopHocs = lopBus.GetAllLopHoc();
            InitializeComponent();
        }

        private void frmQuanLySV_Load(object sender, EventArgs e)
        {
            dtpNgaySinh.Value = DateTime.Today;
            dtpNgaySinh.MinDate = new DateTime(1970, 1, 1);
            dtpNgaySinh.MaxDate = DateTime.Today;

            SetupGrid();

            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = lopHocs;

            List<LopHoc> boLoc = new List<LopHoc>
            {
                new LopHoc { MaLop = "", TenLop = "Tất cả lớp" }
            };
            boLoc.AddRange(lopHocs);
            cboLocLop.DisplayMember = "TenLop";
            cboLocLop.ValueMember = "MaLop";
            cboLocLop.DataSource = boLoc;

            cboTrangThai.Items.AddRange(new object[] { "Đang học", "Bảo lưu", "Nghỉ học" });
            cboTrangThai.SelectedIndex = 0;

            TogAdd(true);
            ready = true;
            ApplyFilter();
            ActiveControl = txtMaSV;
        }

        private void frmQuanLySV_Shown(object sender, EventArgs e)
        {
            txtMaSV.Focus();
        }

        private void SetupGrid()
        {
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.Columns.Clear();
            dgvSinhVien.Columns.Add(TextColumn("colMa", "Mã SV", "MaSV", 80));
            dgvSinhVien.Columns.Add(TextColumn("colTen", "Họ và tên", "HoTen", 140));
            dgvSinhVien.Columns.Add(TextColumn("colNgay", "Ngày sinh", "NgaySinh", 90));
            dgvSinhVien.Columns.Add(TextColumn("colGioi", "Giới tính", "GioiTinh", 70));
            dgvSinhVien.Columns.Add(TextColumn("colEmail", "Email", "Email", 160));
            dgvSinhVien.Columns.Add(TextColumn("colDt", "Điện thoại", "SoDienThoai", 100));
            dgvSinhVien.Columns.Add(TextColumn("colDiem", "Điểm", "Diem", 55));
            dgvSinhVien.Columns.Add(TextColumn("colLop", "Lớp", "MaLop", 150));
            dgvSinhVien.Columns.Add(TextColumn("colTrangThai", "Trạng thái", "TrangThai", 90));
        }

        private static DataGridViewTextBoxColumn TextColumn(string name, string header, string property, float weight)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = property,
                FillWeight = weight,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
        }

        /// <summary>
        /// true = add mode (new student). false = an existing student is on the form.
        /// </summary>
        private void TogAdd(bool isAdding)
        {
            butThem.Enabled = isAdding;
            butSua.Enabled = !isAdding;
            butXoa.Enabled = !isAdding;
            txtMaSV.ReadOnly = !isAdding;
        }

        private void txtMaSV_Enter(object sender, EventArgs e)
        {
            txtMaSV.SelectAll();
        }

        private void txtMaSV_Leave(object sender, EventArgs e)
        {
            if (showing)
                return;

            string ma = txtMaSV.Text.Trim().ToUpperInvariant();
            if (ma == lastLookup)
                return;

            lastLookup = ma;
            if (ma.Length == 0)
            {
                ClearInputsExceptMa();
                TogAdd(true);
                return;
            }

            SinhVien? sv = svb.GetSinhVienByMaSV(ma);
            if (sv == null)
            {
                ClearInputsExceptMa();
                TogAdd(true);
                return;
            }

            ShowSinhVien(sv);
        }

        private void txtMaSV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            SelectNextControl((Control)sender, true, true, true, true);
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgvSinhVien.Rows[e.RowIndex].DataBoundItem is not SinhVien sv)
                return;

            ShowSinhVien(sv);
        }

        private void dgvSinhVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string property = dgvSinhVien.Columns[e.ColumnIndex].DataPropertyName;
            if (property == "NgaySinh" && e.Value is DateTime date)
            {
                e.Value = date.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
            else if (property == "Diem" && e.Value is double score)
            {
                e.Value = score.ToString("0.0");
                e.FormattingApplied = true;
            }
            else if (property == "MaLop" && dgvSinhVien.Rows[e.RowIndex].DataBoundItem is SinhVien sv)
            {
                e.Value = TenLop(sv.MaLop);
                e.FormattingApplied = true;
            }
            else if (property == "TrangThai" && e.CellStyle != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(22, 120, 70);
            }
        }

        private string TenLop(string maLop)
        {
            LopHoc? lop = lopHocs.FirstOrDefault(l => l.MaLop == maLop);
            return lop == null ? maLop : lop.TenLop;
        }

        private void ShowSinhVien(SinhVien sv)
        {
            showing = true;
            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh < dtpNgaySinh.MinDate ? dtpNgaySinh.MinDate : sv.NgaySinh;
            rdoNam.Checked = sv.GioiTinh == "Nam";
            rdoNu.Checked = sv.GioiTinh != "Nam";
            txtEmail.Text = sv.Email;
            txtDienThoai.Text = sv.SoDienThoai;
            cboLop.SelectedValue = sv.MaLop;
            decimal diem = Math.Round((decimal)sv.Diem, 1, MidpointRounding.AwayFromZero);
            if (diem < numDiem.Minimum) diem = numDiem.Minimum;
            if (diem > numDiem.Maximum) diem = numDiem.Maximum;
            numDiem.Value = diem;
            cboTrangThai.SelectedItem = sv.TrangThai;
            ClearErrorProviders();
            lastLookup = sv.MaSV;
            TogAdd(false);
            showing = false;
        }

        private void ClearInputsExceptMa()
        {
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            numDiem.Value = 0;
            rdoNam.Checked = true;
            if (cboLop.Items.Count > 0)
                cboLop.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;
        }

        private void butLamMoi_Click(object sender, EventArgs e)
        {
            ClearErrorProviders();
            txtMaSV.Clear();
            ClearInputsExceptMa();
            lastLookup = "";
            TogAdd(true);
            txtMaSV.Focus();
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            SinhVien sv = ReadForm();
            if (!ShowErrors(sv))
                return;

            try
            {
                svb.AddSinhVien(sv);
                ApplyFilter();
                MessageBox.Show("Đã thêm sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                butLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                ShowActionError(ex);
            }
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            SinhVien sv = ReadForm();
            if (!ShowErrors(sv))
                return;

            try
            {
                svb.UpdateSinhVien(sv);
                ApplyFilter();
                MessageBox.Show("Đã cập nhật sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowActionError(ex);
            }
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            ClearErrorProviders();
            string ma = txtMaSV.Text.Trim();
            if (ma.Length == 0)
            {
                errPMaSV.SetError(txtMaSV, "Chọn sinh viên cần xóa");
                return;
            }

            DialogResult ask = MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên " + ma + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (ask != DialogResult.Yes)
                return;

            try
            {
                svb.DeleteSinhVien(ma);
                ApplyFilter();
                MessageBox.Show("Đã xóa sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                butLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmQuanLySV_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;

            DialogResult ask = MessageBox.Show(
                "Bạn có muốn thoát không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (ask != DialogResult.Yes)
                e.Cancel = true;
        }

        private void cboLocLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void butTimKiem_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void butTatCa_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            numDiemTu.Value = 0;
            if (cboLocLop.Items.Count > 0)
                cboLocLop.SelectedIndex = 0;
            ApplyFilter();
        }

        /// <summary>
        /// Class filter asks the business layer. Keyword and minimum score stay on the form.
        /// </summary>
        private void ApplyFilter()
        {
            if (!ready)
                return;

            List<SinhVien> source;
            if (cboLocLop.SelectedValue is string maLop && maLop.Length > 0)
                source = svb.GetSinhVienByMaLop(maLop);
            else
                source = svb.GetAllSinhVien();

            IEnumerable<SinhVien> query = source;
            string keyword = txtTuKhoa.Text.Trim();
            if (keyword.Length > 0)
            {
                query = query.Where(s =>
                    s.MaSV.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || s.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || s.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || s.SoDienThoai.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            double diemTu = (double)numDiemTu.Value;
            if (diemTu > 0)
                query = query.Where(s => s.Diem >= diemTu);

            sinhViens = query.ToList();
            dgvSinhVien.DataSource = null;
            dgvSinhVien.DataSource = sinhViens;
            lblTong.Text = "Tổng số: " + sinhViens.Count + " sinh viên";
        }

        private SinhVien ReadForm()
        {
            SinhVien sv = new SinhVien
            {
                MaSV = txtMaSV.Text,
                HoTen = txtHoTen.Text,
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
                Email = txtEmail.Text,
                SoDienThoai = txtDienThoai.Text,
                MaLop = cboLop.SelectedValue as string ?? "",
                Diem = (double)numDiem.Value,
                TrangThai = cboTrangThai.SelectedItem as string ?? ""
            };
            sv.ChuanHoa();
            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoTen;
            return sv;
        }

        private bool ShowErrors(SinhVien sv)
        {
            ClearErrorProviders();
            List<ValidationResult> errors = sv.IsInValid();
            if (errors.Count == 0)
                return true;

            Control? first = null;
            foreach (ValidationResult error in errors)
            {
                string field = error.MemberNames.FirstOrDefault() ?? "";
                string message = error.ErrorMessage ?? "Dữ liệu không hợp lệ";
                Control? target = FieldControl(field);
                if (target == null)
                {
                    MessageBox.Show(message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                ErrorProviderOf(field).SetError(target, message);
                first ??= target;
            }

            first?.Focus();
            return false;
        }

        private Control? FieldControl(string field)
        {
            return field switch
            {
                "MaSV" => txtMaSV,
                "HoTen" => txtHoTen,
                "Email" => txtEmail,
                "SoDienThoai" => txtDienThoai,
                "MaLop" => cboLop,
                "Diem" => numDiem,
                "NgaySinh" => dtpNgaySinh,
                "TrangThai" => cboTrangThai,
                "GioiTinh" => rdoNam,
                _ => null
            };
        }

        private ErrorProvider ErrorProviderOf(string field)
        {
            return field switch
            {
                "MaSV" => errPMaSV,
                "HoTen" => erpHoten,
                "Email" => erpEmail,
                "SoDienThoai" => erpDienThoai,
                "MaLop" => erpLopHoc,
                "NgaySinh" => erpNgaySinh,
                _ => erpDiem
            };
        }

        private void ShowActionError(Exception ex)
        {
            if (ex.Message.Contains("Mã sinh viên"))
                errPMaSV.SetError(txtMaSV, ex.Message);
            else
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearErrorProviders()
        {
            errPMaSV.Clear();
            erpHoten.Clear();
            erpEmail.Clear();
            erpDienThoai.Clear();
            erpLopHoc.Clear();
            erpDiem.Clear();
            erpNgaySinh.Clear();
        }

        private void txtMaSV_TextChanged(object sender, EventArgs e) => errPMaSV.Clear();
        private void txtHoTen_TextChanged(object sender, EventArgs e) => erpHoten.Clear();
        private void txtEmail_TextChanged(object sender, EventArgs e) => erpEmail.Clear();
        private void txtDienThoai_TextChanged(object sender, EventArgs e) => erpDienThoai.Clear();
        private void cboLop_SelectedIndexChanged(object sender, EventArgs e) => erpLopHoc.Clear();
    }
}
