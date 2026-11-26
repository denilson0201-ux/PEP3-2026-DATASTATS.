namespace DataStats.Forms
{
    partial class FormInicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            btnNuevaEncuesta = new Button();
            btnResultados = new Button();
            btnConsultar = new Button();
            btnSalir = new Button();
            lblPie = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI Symbol", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.MidnightBlue;
            lblTitulo.Location = new Point(40, 35);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(800, 55);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "DataStats";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.Font = new Font("Segoe UI Emoji", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitulo.ForeColor = Color.DimGray;
            lblSubtitulo.Location = new Point(40, 95);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(800, 35);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Sistema de encuestas y análisis estadístico";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNuevaEncuesta
            // 
            btnNuevaEncuesta.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNuevaEncuesta.Location = new Point(320, 165);
            btnNuevaEncuesta.Name = "btnNuevaEncuesta";
            btnNuevaEncuesta.Size = new Size(260, 55);
            btnNuevaEncuesta.TabIndex = 2;
            btnNuevaEncuesta.Text = "Nueva encuesta";
            btnNuevaEncuesta.UseVisualStyleBackColor = true;
            btnNuevaEncuesta.Click += btnNuevaEncuesta_Click;
            // 
            // btnResultados
            // 
            btnResultados.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnResultados.Location = new Point(320, 235);
            btnResultados.Name = "btnResultados";
            btnResultados.Size = new Size(260, 55);
            btnResultados.TabIndex = 6;
            btnResultados.Text = "Ver resultados";
            btnResultados.UseVisualStyleBackColor = true;
            // 
            // btnConsultar
            // 
            btnConsultar.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConsultar.Location = new Point(320, 305);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(260, 55);
            btnConsultar.TabIndex = 7;
            btnConsultar.Text = "Consultar encuestas";
            btnConsultar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.Location = new Point(320, 390);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(260, 45);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblPie
            // 
            lblPie.ForeColor = Color.Gray;
            lblPie.Location = new Point(40, 475);
            lblPie.Name = "lblPie";
            lblPie.Size = new Size(800, 25);
            lblPie.TabIndex = 9;
            lblPie.Text = "DataStats - Proyecto académico 2026";
            lblPie.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormInicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(884, 511);
            Controls.Add(lblPie);
            Controls.Add(btnSalir);
            Controls.Add(btnConsultar);
            Controls.Add(btnResultados);
            Controls.Add(btnNuevaEncuesta);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormInicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataStats - Menú principal";
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Button btnNuevaEncuesta;
        private Button btnResultados;
        private Button btnConsultar;
        private Button btnSalir;
        private Label lblPie;
    }
}