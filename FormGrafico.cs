using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataStats
{
    public class FormGrafico : Form
    {
        private readonly string archivoCsv;
        private readonly Chart grafico;

        public FormGrafico(string rutaCsv)
        {
            archivoCsv = rutaCsv;
            Text = "DataStats - Distribución de proveedores";
            Width = 780;
            Height = 570;
            StartPosition = FormStartPosition.CenterParent;

            grafico = new Chart { Dock = DockStyle.Fill };
            var area = new ChartArea("AreaPrincipal");
            area.AxisX.Title = "Proveedor";
            area.AxisY.Title = "Cantidad de respuestas";
            grafico.ChartAreas.Add(area);
            grafico.Legends.Add(new Legend("Leyenda"));
            Controls.Add(grafico);

            Shown += (s, e) => CargarGrafica();
        }

        private void CargarGrafica()
        {
            grafico.Series.Clear();
            var serie = new Series("Proveedores")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#PERCENT{P0}",
                Legend = "Leyenda"
            };

            var conteos = new Dictionary<string, int>
            {
                { "Entel", 0 }, { "Tigo", 0 }, { "Viva", 0 }, { "Otro", 0 }
            };

            if (File.Exists(archivoCsv))
            {
                foreach (string linea in File.ReadAllLines(archivoCsv).Skip(1))
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    var campos = SepararCsv(linea);
                    if (campos.Count < 2) continue;
                    string proveedor = campos[1].Trim();
                    if (!conteos.ContainsKey(proveedor)) proveedor = "Otro";
                    conteos[proveedor]++;
                }
            }

            int total = conteos.Values.Sum();
            if (total == 0)
            {
                MessageBox.Show("Todavía no hay encuestas guardadas. Registra al menos una respuesta completa.",
                    "DataStats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            foreach (var item in conteos)
            {
                if (item.Value > 0)
                {
                    int punto = serie.Points.AddY(item.Value);
                    serie.Points[punto].LegendText = item.Key;
                    serie.Points[punto].Label = item.Key + " #PERCENT{P0}";
                }
            }

            grafico.Series.Add(serie);
            grafico.Titles.Add("Distribución de proveedores de Internet");
        }

        // Lee campos CSV entrecomillados, incluidos los que contienen comas.
        private List<string> SepararCsv(string linea)
        {
            var resultado = new List<string>();
            bool dentro = false;
            var campo = "";
            for (int i = 0; i < linea.Length; i++)
            {
                char c = linea[i];
                if (c == '"')
                {
                    if (dentro && i + 1 < linea.Length && linea[i + 1] == '"')
                    {
                        campo += '"';
                        i++;
                    }
                    else dentro = !dentro;
                }
                else if (c == ',' && !dentro)
                {
                    resultado.Add(campo);
                    campo = "";
                }
                else campo += c;
            }
            resultado.Add(campo);
            return resultado;
        }
    }
}
