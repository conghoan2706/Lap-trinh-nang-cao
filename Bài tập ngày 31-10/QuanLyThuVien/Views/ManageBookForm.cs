using QuanLyThuVienDAO.Business;
using QuanLyThuVienDAO.Entity;

namespace QuanLyThuVienDAO.Views;

public class ManageBookForm : Form
{
    private readonly QLTVDBContext _context;
    private readonly ManageBookBuss _books;
    private readonly BookCopyBuss _copies;
    private readonly CategoryBuss _categories;
    private readonly TextBox _keyword = new() { PlaceholderText = "Mã sách, tên sách, tác giả...", Dock = DockStyle.Fill };
    private readonly ComboBox _filter = Combo();
    private readonly ComboBox _category = Combo();
    private readonly TextBox _id = new();
    private readonly TextBox _title = new();
    private readonly TextBox _author = new();
    private readonly TextBox _publisher = new();
    private readonly NumericUpDown _price = new() { Maximum = 1000000000, ThousandsSeparator = true };
    private readonly NumericUpDown _year = new() { Minimum = 1000, Maximum = DateTime.Today.Year, Value = 2020 };
    private readonly NumericUpDown _count = new() { Maximum = 1000, Value = 0 };
    private readonly DataGridView _bookGrid = Grid();
    private readonly DataGridView _copyGrid = Grid();
    private readonly GroupBox _copyGroup = new() { Text = "Danh sách bản sao", Dock = DockStyle.Fill };
    private readonly ToolStripStatusLabel _status = new();
    private string? _selectedId;
    private bool _loading;

    public ManageBookForm(QLTVDBContext context)
    {
        _context = context;
        _books = new(context); _copies = new(context); _categories = new(context);
        Text = "Quản lý đầu sách và bản sao";
        Size = new Size(1300, 880); MinimumSize = new Size(1100, 760);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10);
        BackColor = Color.FromArgb(247, 249, 252);
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1, Padding = new Padding(12) };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var heading = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 };
        heading.RowStyles.Add(new RowStyle(SizeType.Percent, 62));
        heading.RowStyles.Add(new RowStyle(SizeType.Percent, 38));
        heading.Controls.Add(new Label { Text = "QUẢN LÝ ĐẦU SÁCH VÀ BẢN SAO", Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(20, 95, 125) }, 0, 0);
        heading.Controls.Add(new Label { Text = "Tra cứu, cập nhật thông tin đầu sách và quản lý bản sao",
            Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray }, 0, 1);
        root.Controls.Add(heading, 0, 0);
        var search = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 6, RowCount = 1, Padding = new Padding(0, 8, 0, 8) };
        search.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        search.Controls.Add(new Label { Text = "Từ khóa tra", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        search.Controls.Add(_keyword, 1, 0); search.Controls.Add(new Label { Text = "Thể loại", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 2, 0);
        search.Controls.Add(_filter, 3, 0);
        search.Controls.Add(Button("Tìm kiếm", ReloadBooks), 4, 0);
        search.Controls.Add(Button("Làm mới", () => { _keyword.Clear(); _filter.SelectedIndex = 0; ReloadBooks(); }), 5, 0);
        root.Controls.Add(search, 0, 1);
        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420)); body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        body.Controls.Add(BuildEditor(), 0, 0);
        var lists = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
        lists.RowStyles.Add(new RowStyle(SizeType.Percent, 53)); lists.RowStyles.Add(new RowStyle(SizeType.Percent, 47));
        var bookGroup = new GroupBox { Text = "Danh sách đầu sách", Dock = DockStyle.Fill, Padding = new Padding(8) };
        bookGroup.Controls.Add(_bookGrid); lists.Controls.Add(bookGroup, 0, 0);
        var copyPanel = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, Padding = new Padding(6) };
        copyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        copyPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28)); copyPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        copyPanel.Controls.Add(_copyGrid, 0, 0);
        copyPanel.Controls.Add(new Label { Text = "Chọn một bản sao để cập nhật trạng thái, tình trạng hoặc xóa.", AutoSize = true }, 0, 1);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill };
        actions.Controls.Add(Button("Nhập thêm bản sao", ImportCopies)); actions.Controls.Add(Button("Cập nhật bản", EditCopy));
        actions.Controls.Add(Button("Xóa bản sao", DeleteCopy, Color.Firebrick));
        foreach (Control button in actions.Controls) { button.Dock = DockStyle.None; button.Width = 200; }
        copyPanel.Controls.Add(actions, 0, 2);
        _copyGroup.Controls.Add(copyPanel); lists.Controls.Add(_copyGroup, 0, 1);
        body.Controls.Add(lists, 1, 0); root.Controls.Add(body, 0, 2);
        var statusBar = new StatusStrip(); statusBar.Items.Add(_status);
        Controls.Add(root); Controls.Add(statusBar);
        AddColumn(_bookGrid, "BookId", "Mã sách", 75); AddColumn(_bookGrid, "Title", "Tên đầu sách", 220);
        AddColumn(_bookGrid, "CategoryName", "Thể loại", 130); AddColumn(_bookGrid, "Author", "Tác giả", 145);
        AddColumn(_bookGrid, "Publisher", "Nhà xuất bản", 140); AddColumn(_bookGrid, "Price", "Giá sách", 95);
        AddColumn(_bookGrid, "PublicationYear", "Năm xuất bản", 100); AddColumn(_bookGrid, "NumberOfCopies", "Số bản sao", 90);
        _bookGrid.Columns["Price"].DefaultCellStyle.Format = "N0";
        AddColumn(_copyGrid, "BookCopyId", "Mã bản sao", 220); AddColumn(_copyGrid, "Status", "Trạng thái", 210);
        AddColumn(_copyGrid, "Condition", "Tình trạng vật lý", 220);
        _bookGrid.SelectionChanged += (_, _) => { if (!_loading) Run(SelectBook); };
        _keyword.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Run(ReloadBooks); } };
        Shown += (_, _) => Run(() =>
        {
            var categories = _categories.GetAll();
            _category.DisplayMember = "Description"; _category.ValueMember = "CategoryId"; _category.DataSource = categories;
            _filter.DisplayMember = "Description"; _filter.ValueMember = "CategoryId";
            _filter.DataSource = new[] { new Category { CategoryId = "", Description = "-- Tất cả thể loại --" } }.Concat(categories).ToList();
            ReloadBooks();
        });
    }

    private Control BuildEditor()
    {
        var group = new GroupBox { Text = "Thông tin đầu sách", Dock = DockStyle.Fill, Padding = new Padding(10) };
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 12 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150)); panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Control[] inputs = [_id, _title, _author, _publisher, _category, _price, _year, _count];
        string[] labels = ["Mã sách (*):", "Tên sách (*):", "Tác giả (*):", "Nhà xuất bản (*):", "Thể loại (*):", "Giá sách:", "Năm xuất bản:", "Số bản sao:"];
        for (int i = 0; i < inputs.Length; i++)
        {
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            inputs[i].Dock = DockStyle.Top;
            panel.Controls.Add(new Label { Text = labels[i], AutoSize = true, Padding = new Padding(0, 4, 0, 0) }, 0, i);
            panel.Controls.Add(inputs[i], 1, i);
        }
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        var editActions = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty };
        editActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        editActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        editActions.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        editActions.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        editActions.Controls.Add(Button("Thêm đầu sách", AddBook, Color.SeaGreen), 0, 0);
        editActions.Controls.Add(Button("Lưu chỉnh sửa", UpdateBook, Color.DarkOrange), 1, 0);
        editActions.Controls.Add(Button("Xóa đầu sách", DeleteBook, Color.Firebrick), 0, 1);
        editActions.Controls.Add(Button("Làm trống", ClearEditor), 1, 1);
        panel.Controls.Add(editActions, 0, 9); panel.SetColumnSpan(editActions, 2); panel.SetRowSpan(editActions, 2);
        var import = Button("NHẬP BỔ SUNG BẢN SAO", ImportCopies); panel.Controls.Add(import, 0, 11); panel.SetColumnSpan(import, 2);
        group.Controls.Add(panel); return group;
    }

    private Book ReadBook() => new()
    {
        BookId = _id.Text.Trim(), Title = _title.Text.Trim(), Author = _author.Text.Trim(), Publisher = _publisher.Text.Trim(),
        CategoryId = _category.SelectedValue?.ToString() ?? "", Price = _price.Value,
        PublicationYear = (int)_year.Value, NumberOfCopies = _count.Enabled ? (int)_count.Value : 0
    };
    private void ReloadBooks()
    {
        string? keep = _selectedId;
        _loading = true;
        try
        {
            var books = _books.Search(_keyword.Text, _filter.SelectedValue?.ToString());
            _bookGrid.DataSource = books.Select(b => new { b.BookId, b.Title, CategoryName = b.Category?.Description,
                b.Author, b.Publisher, b.Price, b.PublicationYear, b.NumberOfCopies }).ToList();
            foreach (DataGridViewRow row in _bookGrid.Rows)
                if (row.Cells["BookId"].Value?.ToString() == keep) { _bookGrid.CurrentCell = row.Cells[0]; break; }
            _status.Text = $"Đã tải {books.Count} đầu sách lúc {DateTime.Now:HH:mm:ss}";
        }
        finally { _loading = false; }
        if (_bookGrid.Rows.Count > 0) SelectBook(); else ClearEditor();
    }
    private void SelectBook()
    {
        var id = _bookGrid.CurrentRow?.Cells["BookId"].Value?.ToString();
        if (id == null) return;
        var book = _books.GetById(id); if (book == null) return;
        _selectedId = id; _id.Text = id; _id.ReadOnly = true;
        _title.Text = book.Title; _author.Text = book.Author; _publisher.Text = book.Publisher;
        _category.SelectedValue = book.CategoryId; _price.Value = book.Price; _year.Value = book.PublicationYear;
        _count.Maximum = Math.Max(1000, book.NumberOfCopies); _count.Value = book.NumberOfCopies; _count.Enabled = false;
        _copyGrid.DataSource = _copies.GetByBookId(id);
        _copyGroup.Text = $"Danh sách bản sao của sách {id} ({book.NumberOfCopies} bản sao)";
    }
    private void ClearEditor()
    {
        _selectedId = null; _id.Clear(); _id.ReadOnly = false; _title.Clear(); _author.Clear(); _publisher.Clear();
        _price.Value = 0; _year.Value = 2020; _count.Value = 0; _count.Maximum = 1000; _count.Enabled = true;
        _copyGrid.DataSource = null; _copyGroup.Text = "Danh sách bản sao"; _id.Focus();
    }
    private string RequireBook() => _selectedId ?? throw new InvalidOperationException("Vui lòng chọn đầu sách trong danh sách.");
    private void AddBook() { var book = ReadBook(); _books.Add(book); _selectedId = book.BookId; ReloadBooks(); }
    private void UpdateBook() { RequireBook(); _books.Update(ReadBook()); ReloadBooks(); }
    private void DeleteBook()
    {
        string id = RequireBook();
        if (!Confirm($"Xóa đầu sách {id}?")) return;
        _books.Delete(id); _selectedId = null; ReloadBooks();
    }
    private void ImportCopies()
    {
        string id = RequireBook();
        using var dialog = new Form { Text = $"Nhập thêm bản sao cho {id}", Size = new Size(370, 180),
            StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false };
        var number = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 1, Location = new Point(160, 20), Width = 150 };
        dialog.Controls.Add(new Label { Text = "Số lượng nhập:", Location = new Point(15, 22), AutoSize = true }); dialog.Controls.Add(number);
        var ok = new Button { Text = "Nhập bản sao", DialogResult = DialogResult.OK, Location = new Point(160, 70), Width = 150 };
        dialog.Controls.Add(ok); dialog.AcceptButton = ok;
        if (dialog.ShowDialog(this) == DialogResult.OK) { _books.ImportCopies(id, (int)number.Value); ReloadBooks(); }
    }
    private BookCopy RequireCopy() => _copyGrid.CurrentRow?.DataBoundItem as BookCopy
        ?? throw new InvalidOperationException("Vui lòng chọn bản sao.");
    private void DeleteCopy()
    {
        var copy = RequireCopy();
        if (!Confirm($"Xóa bản sao {copy.BookCopyId}?")) return;
        _copies.Delete(copy.BookCopyId); ReloadBooks();
    }
    private void EditCopy()
    {
        var copy = RequireCopy();
        using var dialog = new Form { Text = $"Cập nhật {copy.BookCopyId}", Size = new Size(410, 230),
            StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false };
        var status = Combo(); status.DataSource = BookCopyBuss.Statuses.ToArray(); status.SelectedItem = copy.Status;
        var condition = Combo(); condition.DataSource = BookCopyBuss.Conditions.ToArray(); condition.SelectedItem = copy.Condition;
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(15), RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.Controls.Add(new Label { Text = "Trạng thái", AutoSize = true }, 0, 0); layout.Controls.Add(status, 1, 0);
        layout.Controls.Add(new Label { Text = "Tình trạng vật lý", AutoSize = true }, 0, 1); layout.Controls.Add(condition, 1, 1);
        var save = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Dock = DockStyle.Fill }; layout.Controls.Add(save, 1, 2);
        dialog.Controls.Add(layout); dialog.AcceptButton = save;
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        _copies.Update(new BookCopy { BookCopyId = copy.BookCopyId, BookId = copy.BookId,
            Status = status.SelectedItem?.ToString() ?? "", Condition = condition.SelectedItem?.ToString() ?? "" });
        ReloadBooks();
    }
    private void Run(Action action)
    {
        try { action(); }
        catch (Exception ex)
        {
            _context.ChangeTracker.Clear();
            MessageBox.Show(this, ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    private static bool Confirm(string text) => MessageBox.Show(text, "Xác nhận", MessageBoxButtons.YesNo,
        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    private Button Button(string text, Action action, Color? color = null)
    {
        var button = new Button { Text = text, AutoSize = false, Size = new Size(180, 38), Dock = DockStyle.Fill,
            BackColor = color ?? Color.FromArgb(0, 133, 180), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        button.Click += (_, _) => Run(action); return button;
    }
    private static ComboBox Combo() => new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
    private static DataGridView Grid() => new() { Dock = DockStyle.Fill, AutoGenerateColumns = false,
        ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, MultiSelect = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect, RowHeadersVisible = false,
        BackgroundColor = Color.White, AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells };
    private static void AddColumn(DataGridView grid, string name, string title, int width) =>
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = name, DataPropertyName = name, HeaderText = title, Width = width });
}
