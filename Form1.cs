using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataStats
{
    public class Form1 : Form
    {
        private readonly string[] preguntas =
        {
            "¿Qué proveedor de Internet utiliza?",
            "¿Cuánto paga mensualmente por el servicio? (Bs)",
            "¿Qué velocidad tiene contratada? (Mbps)",
            "¿Está satisfecho con el servicio?",
            "¿Cómo califica la estabilidad de su conexión? (1 a 5)",
            "¿Tiene problemas frecuentes de conexión?",
            "¿Recomendaría su proveedor a otras personas?",
            "¿Qué aspecto debería mejorar su proveedor?"
        };

        private readonly string[][] opciones =
        {
            new[] { "Entel", "Tigo", "Viva", "Otro" },
            new[] { "Ingresar monto en Bs" },
            new[] { "Menos de 10 Mbps", "10 - 30 Mbps", "31 - 60 Mbps", "Más de 60 Mbps" },
            new[] { "Sí", "Parcialmente", "No" },
            new[] { "1", "2", "3", "4", "5" },
            new[] { "Sí", "No" },
            new[] { "Sí", "No", "Tal vez" },
            new[] { "Precio", "Velocidad", "Atención", "Estabilidad", "Cobertura", "Otro" }
        };

        private readonly string[] respuestas = new string[8];
        private Label lblPregunta;
        private Label lblNumero;
        private Panel panelRespuesta;
        private TrackBar barraPreguntas;
        private Button btnAnterior, btnSiguiente, btnGuardar, btnLimpiar, btnGrafico;
        private TextBox txtMonto;
        private ComboBox cmbRespuesta;
        private readonly string archivoCsv = Path.Combine(Application.StartupPath, "Encuestas.csv");

        public Form1()
        {
            Text = "DataStats - Encuesta de Internet";
            Width = 950;
            Height = 650;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new System.Drawing.Size(800, 550);
            ConstruirInterfaz();
            MostrarPregunta(0);
        }

        private void ConstruirInterfaz()
        {
            var encabezado = new Label
            {
                Text = "DataStats  |  Encuesta sobre proveedores de Internet en Bolivia",
                Dock = DockStyle.Top,
                Height = 75,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 17, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(35, 91, 160),
                ForeColor = System.Drawing.Color.White
            };
            var principal = new Panel { Dock = DockStyle.Fill, Padding = new Padding(25), AutoScroll = true };
            Controls.Add(principal);
            Controls.Add(encabezado);
            encabezado.BringToFront();

            lblNumero = new Label
            {
                Left = 30, Top = 25, Width = 800, Height = 30,
                Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
            };
            principal.Controls.Add(lblNumero);

            lblPregunta = new Label
            {
                Left = 30, Top = 75, Width = 820, Height = 65,
                Font = new System.Drawing.Font("Segoe UI", 15, System.Drawing.FontStyle.Regular)
            };
            principal.Controls.Add(lblPregunta);

            panelRespuesta = new Panel { Left = 30, Top = 155, Width = 820, Height = 115 };
            principal.Controls.Add(panelRespuesta);

            barraPreguntas = new TrackBar
            {
                Left = 30, Top = 290, Width = 500, Minimum = 1, Maximum = 8,
                TickFrequency = 1, Value = 1
            };
            barraPreguntas.ValueChanged += BarraPreguntas_ValueChanged;
            principal.Controls.Add(barraPreguntas);

            btnAnterior = CrearBoton("Anterior", 30, 355, 130, 42);
            btnAnterior.Click += (s, e) => CambiarPregunta(-1);
            principal.Controls.Add(btnAnterior);

            btnSiguiente = CrearBoton("Siguiente", 175, 355, 130, 42);
            btnSiguiente.Click += (s, e) => CambiarPregunta(1);
            principal.Controls.Add(btnSiguiente);

            btnGuardar = CrearBoton("Guardar encuesta", 30, 425, 180, 45);
            btnGuardar.Click += (s, e) => GuardarEncuesta();
            principal.Controls.Add(btnGuardar);

            btnLimpiar = CrearBoton("Limpiar respuestas", 225, 425, 180, 45);
            btnLimpiar.Click += (s, e) => LimpiarRespuestas();
            principal.Controls.Add(btnLimpiar);

            btnGrafico = CrearBoton("Ver gráfica de proveedores", 420, 425, 240, 45);
            btnGrafico.Click += (s, e) => new FormGrafico(archivoCsv).ShowDialog(this);
            principal.Controls.Add(btnGrafico);

            var nota = new Label
            {
                Left = 30, Top = 490, Width = 800, Height = 35,
                Text = "Las respuestas se guardan localmente en Encuestas.csv.",
                ForeColor = System.Drawing.Color.DimGray
            };
            principal.Controls.Add(nota);
        }

        private Button CrearBoton(string texto, int x, int y, int ancho, int alto)
        {
            return new Button
            {
                Text = texto, Left = x, Top = y, Width = ancho, Height = alto,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
        }

        private void MostrarPregunta(int indice)
        {
            if (indice < 0 || indice >= preguntas.Length) return;
            if (barraPreguntas.Value != indice + 1)
                barraPreguntas.Value = indice + 1;

            lblNumero.Text = "Pregunta " + (indice + 1) + " de 8";
            lblPregunta.Text = preguntas[indice];
            panelRespuesta.Controls.Clear();
            txtMonto = null;
            cmbRespuesta = null;

            if (indice == 1)
            {
                txtMonto = new TextBox { Left = 5, Top = 15, Width = 250, Font = new System.Drawing.Font("Segoe UI", 12) };
                txtMonto.Text = respuestas[indice] ?? "";
                panelRespuesta.Controls.Add(txtMonto);
            }
            else
            {
                cmbRespuesta = new ComboBox
                {
                    Left = 5, Top = 15, Width = 350, DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new System.Drawing.Font("Segoe UI", 12)
                };
                cmbRespuesta.Items.AddRange(opciones[indice]);
                if (!string.IsNullOrWhiteSpace(respuestas[indice]))
                    cmbRespuesta.SelectedItem = respuestas[indice];
                panelRespuesta.Controls.Add(cmbRespuesta);
            }

            btnAnterior.Enabled = indice > 0;
            btnSiguiente.Enabled = indice < 7;
        }

        private void BarraPreguntas_ValueChanged(object sender, EventArgs e)
        {
            MostrarPregunta(barraPreguntas.Value - 1);
        }

        private void CambiarPregunta(int paso)
        {
            GuardarRespuestaActual();
            int nuevo = Math.Max(0, Math.Min(7, barraPreguntas.Value - 1 + paso));
            MostrarPregunta(nuevo);
        }

        private void GuardarRespuestaActual()
        {
            int i = barraPreguntas.Value - 1;
            if (i == 1)
            {
                respuestas[i] = txtMonto == null ? "" : txtMonto.Text.Trim();
            }
            else
            {
                respuestas[i] = cmbRespuesta == null || cmbRespuesta.SelectedItem == null
                    ? "" : cmbRespuesta.SelectedItem.ToString();
            }
        }

        private void GuardarEncuesta()
        {
            GuardarRespuestaActual();
            for (int i = 0; i < respuestas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(respuestas[i]))
                {
                    MessageBox.Show("Falta responder la pregunta " + (i + 1) + ".", "DataStats",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MostrarPregunta(i);
                    return;
                }
            }

            decimal monto;
            if (!decimal.TryParse(respuestas[1], out monto) || monto < 0)
            {
                MessageBox.Show("Ingresa un monto válido en bolivianos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MostrarPregunta(1);
                return;
            }

            string encabezado = "Fecha,Proveedor,PrecioBs,Velocidad,Satisfaccion,Estabilidad,Problemas,Recomendaria,Mejora";
            bool existe = File.Exists(archivoCsv);
            using (var writer = new StreamWriter(archivoCsv, true, Encoding.UTF8))
            {
                if (!existe) writer.WriteLine(encabezado);
                string[] fila = new string[9];
                fila[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                for (int i = 0; i < 8; i++) fila[i + 1] = respuestas[i];
                writer.WriteLine(string.Join(",", fila.Select(CsvEscapar)));
            }

            MessageBox.Show("Encuesta guardada correctamente.", "DataStats",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Array.Clear(respuestas, 0, respuestas.Length);
            barraPreguntas.Value = 1;
            MostrarPregunta(0);
        }

        private string CsvEscapar(string valor)
        {
            valor = valor ?? "";
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }

        private void LimpiarRespuestas()
        {
            Array.Clear(respuestas, 0, respuestas.Length);
            MostrarPregunta(barraPreguntas.Value - 1);
        }
    }
}
