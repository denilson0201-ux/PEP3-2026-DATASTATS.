using DataStats.Models;

namespace DataStats;

public class SurveyForm : Form
{
    private readonly SurveyResponse _source;
    private readonly bool _editing;
    private readonly DateTimePicker date = new();
    private readonly TextBox zone = new();
    private readonly ComboBox provider = new();
    private readonly ComboBox type = new();
    private readonly NumericUpDown cost = new();
    private readonly NumericUpDown speed = new();
    private readonly NumericUpDown users = new();
    private readonly ComboBox satisfaction = new();
    private readonly NumericUpDown stability = new();
    private readonly ComboBox problems = new();
    private readonly ComboBox recommend = new();
    private readonly ComboBox improvement = new();

    public SurveyResponse? Result { get; private set; }

    public SurveyForm(SurveyResponse? existing)
    {
        _editing = existing != null;
        _source = existing == null ? new SurveyResponse() : new SurveyResponse {
            Id=existing.Id, SurveyDate=existing.SurveyDate, Zone=existing.Zone, Provider=existing.Provider,
            ConnectionType=existing.ConnectionType, MonthlyCost=existing.MonthlyCost, SpeedMbps=existing.SpeedMbps,
            HouseholdUsers=existing.HouseholdUsers, Satisfaction=existing.Satisfaction, Stability=existing.Stability,
            ProblemFrequency=existing.ProblemFrequency, WouldRecommend=existing.WouldRecommend, Improvement=existing.Improvement
        };
        AutoScaleMode = AutoScaleMode.None;
        Text = _editing ? "Editar encuesta | DataStats" : "Nueva encuesta | DataStats";
        Font = new Font("Segoe UI", 9.5f);
        BackColor = Color.FromArgb(244, 246, 248);
        var wa = Screen.FromPoint(Cursor.Position).WorkingArea;
        MinimumSize = new Size(Math.Min(S(640), wa.Width), Math.Min(S(520), wa.Height));
        Size = new Size(Math.Min(S(740), wa.Width - S(30)), Math.Min(S(860), wa.Height - S(30)));
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false; MinimizeBox = false; ShowInTaskbar = false;
        Build();
        LoadValues();
    }

    private TableLayoutPanel table = new();
    private int _row;
    private int S(int v) => (int)Math.Round(v * DeviceDpi / 96.0);
    private Padding P(int l, int t, int r, int b) => new(S(l), S(t), S(r), S(b));

    // Orden de agregado: Fill primero; Bottom y Top después (el último agregado se acomoda primero).
    private void Build()
    {
        var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = P(28, 14, 28, 14), BackColor = Color.White };
        Controls.Add(panel);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = S(70), FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false, Padding = P(24, 14, 24, 10), BackColor = Color.FromArgb(244, 246, 248) };
        Controls.Add(buttons);
        var save = new Button { Text = "Guardar encuesta", Size = new Size(S(170), S(42)), FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(48, 98, 140), ForeColor = Color.White, Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Cursor = Cursors.Hand, Margin = new Padding(0, 0, S(10), 0), UseVisualStyleBackColor = false };
        save.FlatAppearance.BorderColor = Color.FromArgb(48, 98, 140);
        save.Click += (_, _) => Save();
        var cancel = new Button { Text = "Cancelar", Size = new Size(S(120), S(42)), FlatStyle = FlatStyle.Flat,
            BackColor = Color.White, ForeColor = Color.FromArgb(43, 54, 66), Cursor = Cursors.Hand,
            Margin = new Padding(0, 0, S(10), 0), UseVisualStyleBackColor = false };
        cancel.FlatAppearance.BorderColor = Color.FromArgb(205, 213, 221);
        cancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        buttons.Controls.Add(save);    // con RightToLeft el primero queda a la derecha
        buttons.Controls.Add(cancel);
        AcceptButton = save; CancelButton = cancel;

        var head = new Panel { Dock = DockStyle.Top, Height = S(90), BackColor = Color.FromArgb(39, 52, 68), Padding = P(28, 14, 20, 8) };
        head.Controls.Add(new Label { Text = "Completa los datos proporcionados voluntariamente por el encuestado.", Dock = DockStyle.Fill,
            ForeColor = Color.FromArgb(196, 207, 217), TextAlign = ContentAlignment.TopLeft });
        head.Controls.Add(new Label { Text = _editing ? "Editar respuesta" : "Registrar encuesta", Dock = DockStyle.Top, Height = S(40),
            ForeColor = Color.White, Font = new Font("Segoe UI", 17, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft });
        Controls.Add(head);

        table = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 2, Margin = Padding.Empty };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54));
        panel.Controls.Add(table);

        date.Format = DateTimePickerFormat.Short;
        AddField("Fecha de encuesta", date);
        zone.MaxLength = 60;
        AddField("Zona o barrio (sin dirección exacta)", zone);
        SetupCombo(provider, "Entel", "Tigo", "Viva", "Otro");
        AddField("Proveedor de Internet", provider);
        SetupCombo(type, "Fibra óptica", "ADSL / cobre", "Internet inalámbrico", "Datos móviles", "Otro");
        AddField("Tipo de conexión", type);
        cost.DecimalPlaces = 2; cost.Maximum = 100000; cost.Minimum = 0;
        AddField("Pago mensual (Bs)", cost);
        speed.DecimalPlaces = 2; speed.Maximum = 100000; speed.Minimum = 0;
        AddField("Velocidad contratada (Mbps)", speed);
        users.Minimum = 1; users.Maximum = 100;
        AddField("Personas que utilizan la conexión", users);
        SetupCombo(satisfaction, "Sí", "Parcialmente", "No");
        AddField("¿Está satisfecho con el servicio?", satisfaction);
        stability.Minimum = 1; stability.Maximum = 5; stability.Value = 3;
        AddField("Estabilidad (1 = muy inestable, 5 = muy estable)", stability);
        SetupCombo(problems, "Nunca", "Rara vez", "Algunas veces", "Frecuentemente", "Siempre");
        AddField("Frecuencia de problemas", problems);
        SetupCombo(recommend, "Sí", "No", "Tal vez");
        AddField("¿Recomendaría al proveedor?", recommend);
        SetupCombo(improvement, "Precio", "Velocidad", "Atención al cliente", "Estabilidad", "Cobertura", "Otro");
        AddField("¿Qué aspecto debería mejorar?", improvement);

        table.RowCount = _row;
        table.Height = _row * S(56);
    }

    private static void SetupCombo(ComboBox c, params string[] items)
    {
        c.DropDownStyle = ComboBoxStyle.DropDownList; c.Items.AddRange(items);
    }

    private void AddField(string label, Control input)
    {
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, S(56)));
        table.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = Color.FromArgb(65, 79, 94), Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Margin = new Padding(0, 0, S(12), 0) }, 0, _row);
        input.Anchor = AnchorStyles.Left | AnchorStyles.Right;   // ancho flexible, centrado en la fila
        input.Margin = Padding.Empty;
        if (input is TextBox t) { t.BorderStyle = BorderStyle.FixedSingle; t.Font = new Font("Segoe UI", 10); }
        if (input is NumericUpDown n) { n.Font = new Font("Segoe UI", 10); }
        table.Controls.Add(input, 1, _row);
        _row++;
    }

    private void LoadValues()
    {
        date.Value = _source.SurveyDate < date.MinDate ? DateTime.Today : _source.SurveyDate;
        zone.Text = _source.Zone;
        Select(provider, _source.Provider); Select(type, _source.ConnectionType);
        cost.Value = Clamp(_source.MonthlyCost, cost.Minimum, cost.Maximum);
        speed.Value = Clamp(_source.SpeedMbps, speed.Minimum, speed.Maximum);
        users.Value = Math.Clamp(_source.HouseholdUsers, (int)users.Minimum, (int)users.Maximum);
        Select(satisfaction, _source.Satisfaction); stability.Value = Math.Clamp(_source.Stability,1,5);
        Select(problems, _source.ProblemFrequency); Select(recommend, _source.WouldRecommend); Select(improvement, _source.Improvement);
    }

    private static decimal Clamp(decimal value, decimal min, decimal max) => Math.Min(max, Math.Max(min, value));
    private static void Select(ComboBox c, string value)
    {
        int i = c.Items.IndexOf(value);
        if (i >= 0) c.SelectedIndex = i;
        else if (c.Items.Count > 0) c.SelectedIndex = 0;
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(zone.Text) || provider.SelectedIndex < 0 || type.SelectedIndex < 0 ||
            satisfaction.SelectedIndex < 0 || problems.SelectedIndex < 0 || recommend.SelectedIndex < 0 || improvement.SelectedIndex < 0)
        {
            MessageBox.Show("Completa todos los campos antes de guardar.", "Datos incompletos",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
        }
        _source.SurveyDate = date.Value.Date; _source.Zone = zone.Text.Trim();
        _source.Provider = provider.Text; _source.ConnectionType = type.Text;
        _source.MonthlyCost = cost.Value; _source.SpeedMbps = speed.Value; _source.HouseholdUsers = (int)users.Value;
        _source.Satisfaction = satisfaction.Text; _source.Stability = (int)stability.Value;
        _source.ProblemFrequency = problems.Text; _source.WouldRecommend = recommend.Text; _source.Improvement = improvement.Text;
        Result = _source; DialogResult = DialogResult.OK; Close();
    }
}
