using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataStats.Models;
using DataStats.Services;

namespace DataStats.Forms
{
    public partial class FormEncuesta : Form
    {
        public FormEncuesta()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Comprobar que todas las preguntas estén respondidas.
            if (cmbProveedor.SelectedIndex == -1 ||
                cmbSatisfaccion.SelectedIndex == -1 ||
                cmbProblemas.SelectedIndex == -1 ||
                cmbRecomendacion.SelectedIndex == -1 ||
                cmbMejora.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Por favor, responde todas las preguntas.",
                    "Encuesta incompleta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // 2. Obtener las respuestas del formulario.
            Encuesta nuevaEncuesta = new Encuesta
            {
                Proveedor = cmbProveedor.SelectedItem!.ToString()!,

                PagoMensual = numPagoMensual.Value,

                Velocidad = (int)numVelocidad.Value,

                Satisfaccion = cmbSatisfaccion.SelectedItem!.ToString()!,

                Estabilidad = (int)numEstabilidad.Value,

                TieneProblemas =
                    cmbProblemas.SelectedItem!.ToString() == "Sí",

                Recomendacion =
                    cmbRecomendacion.SelectedItem!.ToString()!,

                AspectoMejora = cmbMejora.SelectedItem!.ToString()!,

                FechaRegistro = DateTime.Now
            };

            // 3. Guardar la encuesta en el archivo CSV.
            try
            {
                ArchivoDatos.Guardar(nuevaEncuesta);

                MessageBox.Show(
                    "¡Encuesta guardada correctamente!",
                    "DataStats",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar la encuesta.\n\n" + ex.Message,
                    "Error de almacenamiento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
