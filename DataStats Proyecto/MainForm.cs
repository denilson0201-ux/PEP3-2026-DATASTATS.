using System.Globalization;
using System.Text;
using DataStats.Models;
using DataStats.Services;

namespace DataStats;

public class MainForm : Form
{
    private readonly DataStore _store = new();
    private List<SurveyResponse> _data;
    private readonly Color navy = Color.FromArgb(39, 52, 68);
    private readonly Color slate = Color.FromArgb(91, 108, 126);
    private readonly Color accent = Color.FromArgb(48, 98, 140);
    private readonly Color bg = Color.FromArgb(244, 246, 248);
    private readonly Color ink = Color.FromArgb(43, 54, 66);
    private readonly Color border = Color.FromArgb(222, 228, 234);
    private readonly Panel content = new();
    private readonly Label pageTitle = new();
    private readonly Label pageSubtitle = new();
    private readonly Dictionary<string, Button> navButtons = new();
    private string currentPage = "Inicio";
    private DataGridView? grid;
    private TextBox? searchBox;

    // ---------- Escala por DPI: todas las medidas en píxeles pasan por S() ----------
    private int S(int v) => (int)Math.Round(v * DeviceDpi / 96.0);
    private Padding P(int l, int t, int r, int b) => new(S(l), S(t), S(r), S(b));
    private static Font F(float size, FontStyle style = FontStyle.Regular) => new("Segoe UI", size, style);

    public MainForm()
    {
        AutoScaleMode = AutoScaleMode.None;
        _data = _store.Load();
        Text = "DataStats | Análisis de conectividad";
        Font = F(9.5f);
        BackColor = bg;
        var wa = Screen.FromPoint(Cursor.Position).WorkingArea;
        MinimumSize = new Size(Math.Min(S(1000), wa.Width), Math.Min(S(640), wa.Height));
        Size = new Size(Math.Min(S(1360), wa.Width - S(30)), Math.Min(S(850), wa.Height - S(30)));
        StartPosition = FormStartPosition.CenterScreen;
        BuildShell();
        ShowDashboard();
    }

    // En WinForms el control con DOCK que se agrega ÚLTIMO es el que se acomoda PRIMERO.
    // Por eso el panel de relleno (Fill) siempre se agrega primero y los laterales después.
    private void BuildShell()
    {
        // ----- Barra lateral -----
        var sidebar = new Panel { Dock = DockStyle.Left, Width = S(250), BackColor = navy };

        var nav = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown,
            WrapContents = false, Padding = P(12, 6, 12, 0), BackColor = navy };
        sidebar.Controls.Add(nav);

        var brand = new Panel { Dock = DockStyle.Top, Height = S(100), BackColor = navy };
        sidebar.Controls.Add(brand);
        brand.Controls.Add(new Label { Text = "DS", BackColor = accent, ForeColor = Color.White, Font = F(15, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter, Size = new Size(S(46), S(46)), Location = new Point(S(20), S(26)) });
        brand.Controls.Add(new Label { Text = "DataStats", ForeColor = Color.White, Font = F(15, FontStyle.Bold),
            Location = new Point(S(76), S(24)), AutoSize = true });
        brand.Controls.Add(new Label { Text = "ANÁLISIS DE INTERNET", ForeColor = Color.FromArgb(177, 190, 202),
            Font = F(7.5f, FontStyle.Bold), Location = new Point(S(78), S(56)), AutoSize = true });

        AddNavHeader(nav, "GENERAL");
        AddNav(nav, "Inicio", "▦", "Inicio", ShowDashboard);
        AddNav(nav, "Encuestas", "▤", "Encuestas", ShowSurveys);
        AddNav(nav, "Estadísticas", "▥", "Estadísticas", ShowStatistics);
        AddNavHeader(nav, "GESTIÓN");
        AddNav(nav, "", "⇩", "Exportar datos", ExportCsv);
        AddNav(nav, "Info", "⚙", "Información", ShowInfo);

        // ----- Barra superior -----
        var top = new Panel { Dock = DockStyle.Top, Height = S(84), BackColor = Color.White, Padding = P(30, 8, 28, 0) };
        pageTitle.Dock = DockStyle.Fill; pageTitle.Font = F(17, FontStyle.Bold); pageTitle.ForeColor = ink;
        pageTitle.TextAlign = ContentAlignment.MiddleLeft; pageTitle.AutoEllipsis = true;
        pageSubtitle.Dock = DockStyle.Bottom; pageSubtitle.Height = S(28); pageSubtitle.Font = F(9); pageSubtitle.ForeColor = slate;
        pageSubtitle.TextAlign = ContentAlignment.MiddleLeft; pageSubtitle.AutoEllipsis = true;
        var crumb = new Label { Text = "DataStats  /  Sistema estadístico", Dock = DockStyle.Right, Width = S(290),
            TextAlign = ContentAlignment.MiddleRight, ForeColor = slate, Font = F(9) };
        var line = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = border };
        top.Controls.Add(pageTitle);
        top.Controls.Add(pageSubtitle);
        top.Controls.Add(crumb);
        top.Controls.Add(line);

        // ----- Contenido -----
        content.Dock = DockStyle.Fill;
        content.BackColor = bg;
        content.Padding = P(28, 22, 28, 18);
        content.AutoScroll = true;

        Controls.Add(content);   // Fill: se acomoda al final
        Controls.Add(top);
        Controls.Add(sidebar);   // Left: se acomoda primero
    }

    private void AddNavHeader(FlowLayoutPanel nav, string text)
    {
        nav.Controls.Add(new Label { Text = text, Width = S(224), Height = S(38), Margin = Padding.Empty,
            Padding = P(10, 16, 0, 0), ForeColor = Color.FromArgb(143, 160, 177), Font = F(8, FontStyle.Bold) });
    }

    private void AddNav(FlowLayoutPanel nav, string key, string icon, string text, Action action)
    {
        var b = new Button { Text = $"{icon}   {text}", Width = S(224), Height = S(44), FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(S(14), 0, 0, 0), BackColor = navy,
            ForeColor = Color.FromArgb(205, 216, 226), Font = F(9.5f), Cursor = Cursors.Hand,
            Margin = new Padding(0, S(2), 0, S(2)), UseVisualStyleBackColor = false };
        b.FlatAppearance.BorderSize = 0;
        b.FlatAppearance.MouseOverBackColor = Color.FromArgb(53, 70, 89);
        b.Click += (_, _) => action();
        nav.Controls.Add(b);
        if (key != "") navButtons[key] = b;
    }

    private void SetPage(string key, string title, string subtitle)
    {
        currentPage = key; pageTitle.Text = title; pageSubtitle.Text = subtitle;
        grid = null; searchBox = null;
        content.SuspendLayout();
        foreach (var c in content.Controls.Cast<Control>().ToList()) { content.Controls.Remove(c); c.Dispose(); }
        content.AutoScroll = true;
        content.AutoScrollPosition = new Point(0, 0);
        foreach (var kv in navButtons)
        {
            bool on = kv.Key == key;
            kv.Value.BackColor = on ? Color.FromArgb(58, 84, 112) : navy;
            kv.Value.ForeColor = on ? Color.White : Color.FromArgb(205, 216, 226);
        }
        content.ResumeLayout(true);
    }

    // Pila vertical de filas de alto fijo (ya escalado) que ocupa todo el ancho disponible.
    private static TableLayoutPanel MakeStack(params int[] rowHeights)
    {
        var t = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 1, RowCount = rowHeights.Length,
            Margin = Padding.Empty, Padding = Padding.Empty, Height = rowHeights.Sum() };
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        foreach (var h in rowHeights) t.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
        return t;
    }

    // =====================  INICIO  =====================
    private void ShowDashboard()
    {
        SetPage("Inicio", "Panel principal", "Resumen general de las encuestas registradas");
        var count = _data.Count;
        var avgCost = count == 0 ? 0 : _data.Average(x => (double)x.MonthlyCost);
        var avgStability = count == 0 ? 0 : _data.Average(x => x.Stability);
        var satisfied = count == 0 ? 0 : _data.Count(x => x.Satisfaction == "Sí") * 100.0 / count;

        var kpis = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, Margin = Padding.Empty };
        for (int i = 0; i < 4; i++) kpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        kpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        kpis.Controls.Add(Kpi("ENCUESTAS REGISTRADAS", count.ToString(), "Respuestas guardadas", false), 0, 0);
        kpis.Controls.Add(Kpi("GASTO MENSUAL PROMEDIO", count == 0 ? "—" : $"{avgCost:N2} Bs", "Promedio de la muestra", false), 1, 0);
        kpis.Controls.Add(Kpi("ESTABILIDAD PROMEDIO", count == 0 ? "—" : $"{avgStability:N2} / 5", "Escala de valoración", false), 2, 0);
        kpis.Controls.Add(Kpi("SATISFACCIÓN", count == 0 ? "—" : $"{satisfied:N1}%", "Respuestas 'Sí'", true), 3, 0);

        var root = MakeStack(S(138), S(264), S(150));
        root.Controls.Add(kpis, 0, 0);
        root.Controls.Add(BuildWelcomeCard(), 0, 1);
        root.Controls.Add(BuildNoteCard(count), 0, 2);
        content.Controls.Add(root);
    }

    private Card Kpi(string title, string value, string detail, bool last)
    {
        var c = new Card(accent) { Dock = DockStyle.Fill, Margin = new Padding(0, 0, last ? 0 : S(14), S(14)), Padding = P(22, 14, 12, 12) };
        // Orden de agregado: Fill primero, luego Top y Bottom (se acomodan al revés).
        c.Controls.Add(new Label { Text = value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = ink, Font = F(20, FontStyle.Bold), AutoEllipsis = true });
        c.Controls.Add(new Label { Text = title, Dock = DockStyle.Top, Height = S(24), TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = slate, Font = F(8, FontStyle.Bold), AutoEllipsis = true });
        c.Controls.Add(new Label { Text = detail, Dock = DockStyle.Bottom, Height = S(26), TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = slate, Font = F(8.5f), AutoEllipsis = true });
        return c;
    }

    private Card BuildWelcomeCard()
    {
        var card = new Card { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, S(14)), Padding = P(26, 18, 26, 14) };
        card.Controls.Add(new Label { Text = $"Almacenamiento local: {_store.DataLocation}", Dock = DockStyle.Fill,
            ForeColor = Color.Gray, Font = F(8), TextAlign = ContentAlignment.TopLeft });

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = S(60), WrapContents = false, Padding = P(0, 8, 0, 0) };
        var add = MakeButton("+  Registrar encuesta", true, 190);
        add.Click += (_, _) => OpenSurveyEditor(null);
        var view = MakeButton("Ver encuestas", false, 150);
        view.Click += (_, _) => ShowSurveys();
        buttons.Controls.Add(add); buttons.Controls.Add(view);
        card.Controls.Add(buttons);

        card.Controls.Add(new Label { Text = "Registra encuestas reales sobre los proveedores de Internet y utiliza los reportes para analizar la información recopilada.",
            Dock = DockStyle.Top, Height = S(64), ForeColor = slate, Font = F(10) });
        card.Controls.Add(Heading("Bienvenido a DataStats", 18));
        return card;
    }

    private Card BuildNoteCard(int count)
    {
        var card = new Card { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, S(14)), Padding = P(26, 14, 26, 10) };
        card.Controls.Add(new Label { Dock = DockStyle.Fill, ForeColor = slate, Font = F(9.5f), Text = count == 0
            ? "Aún no hay encuestas registradas. Los indicadores aparecerán cuando ingreses respuestas reales. El sistema no incluye resultados de ejemplo."
            : "Los indicadores se calculan exclusivamente con las respuestas almacenadas. La muestra describe únicamente a las personas encuestadas." });
        card.Controls.Add(Heading("Importante: datos auténticos", 13));
        return card;
    }

    // =====================  ENCUESTAS  =====================
    private void ShowSurveys()
    {
        SetPage("Encuestas", "Encuestas", "Registro, consulta y administración de respuestas");
        content.AutoScroll = false;

        var g = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AllowUserToDeleteRows = false, AllowUserToResizeRows = false, AutoGenerateColumns = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None, RowHeadersVisible = false, EnableHeadersVisualStyles = false,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight = S(46), GridColor = Color.FromArgb(232, 236, 240), Font = F(9.5f),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal };
        g.RowTemplate.Height = S(40);
        g.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(237, 241, 245), ForeColor = ink,
            Font = F(9, FontStyle.Bold), SelectionBackColor = Color.FromArgb(237, 241, 245), SelectionForeColor = ink,
            WrapMode = DataGridViewTriState.True, Alignment = DataGridViewContentAlignment.MiddleLeft, Padding = P(8, 0, 0, 0) };
        g.DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.White, ForeColor = ink,
            SelectionBackColor = Color.FromArgb(222, 232, 241), SelectionForeColor = ink, Padding = P(8, 0, 0, 0) };
        grid = g;
        AddCol("Fecha", "SurveyDate", 90, "dd/MM/yyyy");
        AddCol("Zona", "Zone", 130);
        AddCol("Proveedor", "Provider", 110);
        AddCol("Tipo de conexión", "ConnectionType", 135);
        AddCol("Pago (Bs)", "MonthlyCost", 90, "N2");
        AddCol("Velocidad (Mbps)", "SpeedMbps", 110, "N2");
        AddCol("Satisfacción", "Satisfaction", 100);
        AddCol("Estabilidad", "Stability", 90);
        g.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0 && g.Rows[e.RowIndex].DataBoundItem is SurveyResponse s) OpenSurveyEditor(s);
        };

        var holder = new Card { Dock = DockStyle.Fill, Padding = new Padding(1) };
        holder.Controls.Add(g);

        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = S(60), WrapContents = false, Padding = P(0, 0, 0, 10) };
        var add = MakeButton("+  Nueva encuesta", true, 170);
        add.Click += (_, _) => OpenSurveyEditor(null);
        var edit = MakeButton("Editar seleccionada", false, 170);
        edit.Click += (_, _) =>
        {
            if (grid?.CurrentRow?.DataBoundItem is SurveyResponse s) OpenSurveyEditor(s);
            else MessageBox.Show("Selecciona una encuesta.", "DataStats", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };
        var del = MakeButton("Eliminar", false, 110);
        del.Click += (_, _) => DeleteSelected();
        var search = new TextBox { Width = S(260), Font = F(10), PlaceholderText = "Buscar proveedor o zona...",
            Margin = new Padding(S(6), S(7), 0, 0) };
        search.TextChanged += (_, _) => BindGrid(search.Text);
        searchBox = search;
        toolbar.Controls.Add(add); toolbar.Controls.Add(edit); toolbar.Controls.Add(del); toolbar.Controls.Add(search);

        content.Controls.Add(holder);   // Fill primero
        content.Controls.Add(toolbar);  // Top después
        BindGrid();
    }

    private void AddCol(string header, string prop, int weight, string? format = null)
    {
        var c = new DataGridViewTextBoxColumn { HeaderText = header, DataPropertyName = prop, FillWeight = weight, MinimumWidth = S(72) };
        if (format != null) c.DefaultCellStyle.Format = format;
        grid!.Columns.Add(c);
    }

    private void BindGrid(string filter = "")
    {
        if (grid == null) return;
        var rows = _data.Where(x => string.IsNullOrWhiteSpace(filter) ||
            x.Provider.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
            x.Zone.Contains(filter, StringComparison.OrdinalIgnoreCase)).OrderByDescending(x => x.SurveyDate).ToList();
        grid.DataSource = null; grid.DataSource = rows;
    }

    private void DeleteSelected()
    {
        if (grid?.CurrentRow?.DataBoundItem is not SurveyResponse s)
        {
            MessageBox.Show("Selecciona una encuesta.", "DataStats", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show("¿Eliminar la encuesta seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        _data.RemoveAll(x => x.Id == s.Id); _store.Save(_data); BindGrid(searchBox?.Text ?? "");
    }

    private void OpenSurveyEditor(SurveyResponse? existing)
    {
        using var f = new SurveyForm(existing);
        if (f.ShowDialog(this) == DialogResult.OK && f.Result != null)
        {
            if (existing == null) _data.Add(f.Result);
            else
            {
                var i = _data.FindIndex(x => x.Id == existing.Id);
                if (i >= 0) _data[i] = f.Result;
            }
            _store.Save(_data);
            switch (currentPage)
            {
                case "Encuestas": BindGrid(searchBox?.Text ?? ""); break;
                case "Estadísticas": ShowStatistics(); break;
                default: ShowDashboard(); break;
            }
        }
    }

    // =====================  ESTADÍSTICAS  =====================
    private void ShowStatistics()
    {
        SetPage("Estadísticas", "Estadísticas", "Indicadores descriptivos calculados a partir de la muestra registrada");
        if (_data.Count == 0)
        {
            var rootEmpty = MakeStack(S(200));
            var empty = new Card { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, S(14)), Padding = P(26, 18, 26, 14) };
            var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = S(60), WrapContents = false, Padding = P(0, 8, 0, 0) };
            var b = MakeButton("+  Registrar primera encuesta", true, 230);
            b.Click += (_, _) => OpenSurveyEditor(null);
            buttons.Controls.Add(b);
            empty.Controls.Add(buttons);
            empty.Controls.Add(new Label { Text = "Registra encuestas reales para obtener frecuencias, porcentajes, promedios y comparaciones.",
                Dock = DockStyle.Top, Height = S(46), ForeColor = slate, Font = F(10) });
            empty.Controls.Add(Heading("Todavía no hay datos para analizar", 18));
            rootEmpty.Controls.Add(empty, 0, 0);
            content.Controls.Add(rootEmpty);
            return;
        }

        var provs = Freq(x => x.Provider);
        var sats = Freq(x => x.Satisfaction);
        var probs = Freq(x => x.ProblemFrequency);
        var recs = Freq(x => x.WouldRecommend);
        int h1 = DistHeight(Math.Max(provs.Count, sats.Count));
        int h2 = DistHeight(Math.Max(probs.Count, recs.Count));

        var root = MakeStack(S(262), h1 + S(14), h2 + S(14), S(124));
        root.Controls.Add(BuildSummaryCard(), 0, 0);
        root.Controls.Add(DistRow(provs, "Frecuencia por proveedor", sats, "Nivel de satisfacción"), 0, 1);
        root.Controls.Add(DistRow(probs, "Frecuencia de problemas", recs, "Recomendación del proveedor"), 0, 2);

        var caveat = new Card { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, S(14)), Padding = P(26, 16, 26, 12) };
        caveat.Controls.Add(new Label { Dock = DockStyle.Fill, ForeColor = slate, Font = F(9.5f),
            Text = "Interpretación: estos resultados describen exclusivamente a las personas encuestadas. No deben generalizarse a toda la ciudad si la muestra no es probabilística." });
        root.Controls.Add(caveat, 0, 3);
        content.Controls.Add(root);
    }

    private List<(string name, int count)> Freq(Func<SurveyResponse, string> key) =>
        _data.GroupBy(key).OrderByDescending(g => g.Count())
             .Select(g => (g.Key, g.Count())).ToList();

    private int DistHeight(int rows) => S(66) + rows * S(32);

    private TableLayoutPanel DistRow(List<(string name, int count)> left, string leftTitle,
                                     List<(string name, int count)> right, string rightTitle)
    {
        var t = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = Padding.Empty };
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var a = BuildDistributionCard(leftTitle, left); a.Margin = new Padding(0, 0, S(14), S(14));
        var b = BuildDistributionCard(rightTitle, right); b.Margin = new Padding(0, 0, 0, S(14));
        t.Controls.Add(a, 0, 0); t.Controls.Add(b, 1, 0);
        return t;
    }

    private Card BuildSummaryCard()
    {
        var total = _data.Count;
        var avg = _data.Average(x => x.MonthlyCost);
        var median = Median(_data.Select(x => (double)x.MonthlyCost).OrderBy(x => x).ToList());
        var mode = _data.GroupBy(x => x.Provider).OrderByDescending(g => g.Count()).First().Key;
        var lines = string.Join("\n", new[]
        {
            $"Total de encuestas: {total}",
            $"Gasto mensual — media: {avg:N2} Bs   |   mediana: {median:N2} Bs",
            $"Gasto mensual — mínimo: {_data.Min(x => x.MonthlyCost):N2} Bs   |   máximo: {_data.Max(x => x.MonthlyCost):N2} Bs",
            $"Velocidad contratada media: {_data.Average(x => x.SpeedMbps):N2} Mbps",
            $"Proveedor más frecuente en la muestra: {mode}",
            $"Estabilidad media: {_data.Average(x => x.Stability):N2} / 5"
        });
        var card = new Card { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, S(14)), Padding = P(26, 14, 26, 10) };
        card.Controls.Add(new Label { Text = lines, Dock = DockStyle.Fill, ForeColor = ink, Font = F(10), TextAlign = ContentAlignment.TopLeft });
        card.Controls.Add(Heading("Resumen estadístico", 17));
        return card;
    }

    private Card BuildDistributionCard(string title, List<(string name, int count)> values)
    {
        var card = new Card { Dock = DockStyle.Fill, Padding = P(20, 14, 20, 10) };
        // Las filas se agregan en orden inverso porque la última agregada queda arriba.
        for (int i = values.Count - 1; i >= 0; i--)
        {
            var (name, n) = values[i];
            var pct = n * 100.0 / _data.Count;
            var row = new Panel { Dock = DockStyle.Top, Height = S(32) };
            row.Controls.Add(new BarPanel(pct / 100.0, accent, Color.FromArgb(232, 237, 241)) { Dock = DockStyle.Fill });
            row.Controls.Add(new Label { Text = string.IsNullOrWhiteSpace(name) ? "(sin dato)" : name, Dock = DockStyle.Left,
                Width = S(150), AutoEllipsis = true, TextAlign = ContentAlignment.MiddleLeft, ForeColor = ink });
            row.Controls.Add(new Label { Text = $"{n}  ({pct:N1}%)", Dock = DockStyle.Right, Width = S(100),
                TextAlign = ContentAlignment.MiddleRight, ForeColor = slate });
            card.Controls.Add(row);
        }
        card.Controls.Add(Heading(title, 14));
        return card;
    }

    private static double Median(List<double> values)
    {
        if (values.Count == 0) return 0;
        int n = values.Count;
        return n % 2 == 1 ? values[n / 2] : (values[n / 2 - 1] + values[n / 2]) / 2;
    }

    // =====================  EXPORTAR  =====================
    private void ExportCsv()
    {
        if (_data.Count == 0) { MessageBox.Show("No hay encuestas para exportar.", "DataStats", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
        using var sfd = new SaveFileDialog { Filter = "Archivo CSV (*.csv)|*.csv", FileName = "DataStats_encuestas.csv" };
        if (sfd.ShowDialog() != DialogResult.OK) return;
        var sb = new StringBuilder();
        sb.AppendLine("Fecha;Zona;Proveedor;Tipo de conexion;Pago mensual Bs;Velocidad Mbps;Usuarios hogar;Satisfaccion;Estabilidad;Frecuencia problemas;Recomendaria;Mejora");
        foreach (var x in _data)
        {
            string[] vals = { x.SurveyDate.ToString("yyyy-MM-dd"), x.Zone, x.Provider, x.ConnectionType,
                x.MonthlyCost.ToString(CultureInfo.InvariantCulture), x.SpeedMbps.ToString(CultureInfo.InvariantCulture),
                x.HouseholdUsers.ToString(), x.Satisfaction, x.Stability.ToString(), x.ProblemFrequency, x.WouldRecommend, x.Improvement };
            sb.AppendLine(string.Join(";", vals.Select(Csv)));
        }
        try
        {
            File.WriteAllText(sfd.FileName, "\uFEFF" + sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Los datos se exportaron correctamente.", "DataStats", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (IOException)
        {
            MessageBox.Show("No se pudo guardar el archivo. Si está abierto en Excel, ciérralo e inténtalo de nuevo.",
                "DataStats", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    private static string Csv(string value) => "\"" + value.Replace("\"", "\"\"") + "\"";

    // =====================  INFORMACIÓN  =====================
    private void ShowInfo()
    {
        SetPage("Info", "Información del proyecto", "Alcance, metodología y uso responsable de los resultados");
        var root = MakeStack(S(580));
        var p = new Card { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, S(14)), Padding = P(26, 16, 26, 14) };
        string info = "PROPÓSITO\nRecopilar y analizar la percepción de usuarios sobre sus servicios de Internet.\n\n" +
            "METODOLOGÍA SUGERIDA\nInvestigación descriptiva, enfoque cuantitativo y encuesta estructurada. Definir población, muestra, zona y fechas antes de aplicar el instrumento.\n\n" +
            "VARIABLES\nProveedor, zona, tipo de conexión, gasto mensual, velocidad contratada, cantidad de usuarios del hogar, satisfacción, estabilidad, frecuencia de problemas, recomendación y mejora esperada.\n\n" +
            "PRIVACIDAD Y CALIDAD\nNo registrar nombres, correos ni contraseñas. Solicitar participación voluntaria. No inventar ni completar respuestas faltantes. Informar el tamaño y tipo de muestra.\n\n" +
            "ALMACENAMIENTO\nLas respuestas se guardan localmente en un archivo JSON dentro de la carpeta de datos de usuario de Windows.";
        p.Controls.Add(new Label { Text = info, Dock = DockStyle.Fill, ForeColor = ink, Font = F(10), TextAlign = ContentAlignment.TopLeft });
        p.Controls.Add(Heading("DataStats | Sistema de análisis de conectividad", 17));
        root.Controls.Add(p, 0, 0);
        content.Controls.Add(root);
    }

    // =====================  Ayudas de interfaz  =====================
    private Label Heading(string text, float size) => new()
    {
        Text = text, Dock = DockStyle.Top, Height = S((int)(size * 2.4)), AutoEllipsis = true,
        Font = F(size, FontStyle.Bold), ForeColor = ink, TextAlign = ContentAlignment.MiddleLeft
    };

    private Button MakeButton(string text, bool primary, int minWidth)
    {
        var b = new Button { Text = text, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
            MinimumSize = new Size(S(minWidth), S(40)), Padding = P(14, 2, 14, 2), FlatStyle = FlatStyle.Flat,
            BackColor = primary ? accent : Color.White, ForeColor = primary ? Color.White : ink,
            Font = F(9, FontStyle.Bold), Cursor = Cursors.Hand, Margin = new Padding(0, 0, S(10), 0), UseVisualStyleBackColor = false };
        b.FlatAppearance.BorderColor = primary ? accent : Color.FromArgb(205, 213, 221);
        b.FlatAppearance.BorderSize = 1;
        return b;
    }

    // Tarjeta blanca con borde fino (y barra de color opcional a la izquierda).
    private sealed class Card : Panel
    {
        private readonly Color? _accent;
        public Card(Color? accent = null)
        {
            _accent = accent; DoubleBuffered = true; BackColor = Color.White;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var pen = new Pen(Color.FromArgb(222, 228, 234));
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            if (_accent is Color c)
            {
                using var b = new SolidBrush(c);
                e.Graphics.FillRectangle(b, 0, 0, Math.Max(3, (int)Math.Round(4 * DeviceDpi / 96.0)), Height);
            }
        }
        protected override void OnResize(EventArgs e) { base.OnResize(e); Invalidate(); }
    }

    // Barra de porcentaje que se redibuja según el ancho disponible.
    private sealed class BarPanel : Panel
    {
        private readonly double _fraction;
        private readonly Color _fill, _track;
        public BarPanel(double fraction, Color fill, Color track)
        {
            _fraction = Math.Clamp(fraction, 0, 1); _fill = fill; _track = track; DoubleBuffered = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int h = (int)Math.Round(12 * DeviceDpi / 96.0);
            int pad = (int)Math.Round(8 * DeviceDpi / 96.0);
            var track = new Rectangle(pad, (Height - h) / 2, Math.Max(1, Width - 2 * pad), h);
            using var tb = new SolidBrush(_track);
            e.Graphics.FillRectangle(tb, track);
            int w = (int)Math.Round(track.Width * _fraction);
            if (w > 0)
            {
                using var fb = new SolidBrush(_fill);
                e.Graphics.FillRectangle(fb, track.X, track.Y, w, track.Height);
            }
        }
        protected override void OnResize(EventArgs e) { base.OnResize(e); Invalidate(); }
    }
}
