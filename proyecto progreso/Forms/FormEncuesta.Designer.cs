namespace DataStats.Forms
{
    partial class FormEncuesta
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
            pnlEncuesta = new Panel();
            btnVolver = new Button();
            btnGuardar = new Button();
            cmbMejora = new ComboBox();
            lblPregunta8 = new Label();
            cmbRecomendacion = new ComboBox();
            cmbProblemas = new ComboBox();
            lblPregunta7 = new Label();
            lblPregunta6 = new Label();
            lblEscala = new Label();
            numEstabilidad = new NumericUpDown();
            lblPregunta5 = new Label();
            cmbSatisfaccion = new ComboBox();
            lblPregunta4 = new Label();
            lblMbps = new Label();
            numVelocidad = new NumericUpDown();
            lblPregunta3 = new Label();
            lblMoneda = new Label();
            numPagoMensual = new NumericUpDown();
            label1 = new Label();
            cmbProveedor = new ComboBox();
            lblPregunta1 = new Label();
            lblDescripcion = new Label();
            lblTituloEncuesta = new Label();
            pnlEncuesta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEstabilidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVelocidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPagoMensual).BeginInit();
            SuspendLayout();
            // 
            // pnlEncuesta
            // 
            pnlEncuesta.AutoScroll = true;
            pnlEncuesta.Controls.Add(btnVolver);
            pnlEncuesta.Controls.Add(btnGuardar);
            pnlEncuesta.Controls.Add(cmbMejora);
            pnlEncuesta.Controls.Add(lblPregunta8);
            pnlEncuesta.Controls.Add(cmbRecomendacion);
            pnlEncuesta.Controls.Add(cmbProblemas);
            pnlEncuesta.Controls.Add(lblPregunta7);
            pnlEncuesta.Controls.Add(lblPregunta6);
            pnlEncuesta.Controls.Add(lblEscala);
            pnlEncuesta.Controls.Add(numEstabilidad);
            pnlEncuesta.Controls.Add(lblPregunta5);
            pnlEncuesta.Controls.Add(cmbSatisfaccion);
            pnlEncuesta.Controls.Add(lblPregunta4);
            pnlEncuesta.Controls.Add(lblMbps);
            pnlEncuesta.Controls.Add(numVelocidad);
            pnlEncuesta.Controls.Add(lblPregunta3);
            pnlEncuesta.Controls.Add(lblMoneda);
            pnlEncuesta.Controls.Add(numPagoMensual);
            pnlEncuesta.Controls.Add(label1);
            pnlEncuesta.Controls.Add(cmbProveedor);
            pnlEncuesta.Controls.Add(lblPregunta1);
            pnlEncuesta.Controls.Add(lblDescripcion);
            pnlEncuesta.Controls.Add(lblTituloEncuesta);
            pnlEncuesta.Location = new Point(20, 20);
            pnlEncuesta.Name = "pnlEncuesta";
            pnlEncuesta.Size = new Size(840, 610);
            pnlEncuesta.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.LightGray;
            btnVolver.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = Color.Black;
            btnVolver.Location = new Point(250, 870);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(200, 45);
            btnVolver.TabIndex = 22;
            btnVolver.Text = "Volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.SteelBlue;
            btnGuardar.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(30, 870);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(200, 45);
            btnGuardar.TabIndex = 21;
            btnGuardar.Text = "Guardar encuesta";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // cmbMejora
            // 
            cmbMejora.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMejora.FormattingEnabled = true;
            cmbMejora.Items.AddRange(new object[] { "Precio", "Velocidad", "Atención al cliente", "Estabilidad", "Cobertura", "Otro" });
            cmbMejora.Location = new Point(30, 815);
            cmbMejora.Name = "cmbMejora";
            cmbMejora.Size = new Size(300, 23);
            cmbMejora.TabIndex = 20;
            // 
            // lblPregunta8
            // 
            lblPregunta8.AutoSize = true;
            lblPregunta8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta8.Location = new Point(30, 770);
            lblPregunta8.Name = "lblPregunta8";
            lblPregunta8.Size = new Size(481, 20);
            lblPregunta8.TabIndex = 19;
            lblPregunta8.Text = "¿Qué aspecto considera que debería mejorar su proveedor de internet?";
            // 
            // cmbRecomendacion
            // 
            cmbRecomendacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRecomendacion.FormattingEnabled = true;
            cmbRecomendacion.Items.AddRange(new object[] { "Sí", "No", "Tal vez" });
            cmbRecomendacion.Location = new Point(30, 715);
            cmbRecomendacion.Name = "cmbRecomendacion";
            cmbRecomendacion.Size = new Size(250, 23);
            cmbRecomendacion.TabIndex = 18;
            // 
            // cmbProblemas
            // 
            cmbProblemas.FormattingEnabled = true;
            cmbProblemas.Items.AddRange(new object[] { "Sí", "No" });
            cmbProblemas.Location = new Point(30, 625);
            cmbProblemas.Name = "cmbProblemas";
            cmbProblemas.Size = new Size(121, 23);
            cmbProblemas.TabIndex = 1;
            // 
            // lblPregunta7
            // 
            lblPregunta7.AutoSize = true;
            lblPregunta7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta7.Location = new Point(30, 680);
            lblPregunta7.Name = "lblPregunta7";
            lblPregunta7.Size = new Size(398, 20);
            lblPregunta7.TabIndex = 17;
            lblPregunta7.Text = "\t¿Recomendaría su proveedor de internet a otras personas?";
            // 
            // lblPregunta6
            // 
            lblPregunta6.AutoSize = true;
            lblPregunta6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta6.Location = new Point(30, 590);
            lblPregunta6.Name = "lblPregunta6";
            lblPregunta6.Size = new Size(382, 20);
            lblPregunta6.TabIndex = 16;
            lblPregunta6.Text = "¿Tiene problemas frecuentes con su servicio de internet?";
            // 
            // lblEscala
            // 
            lblEscala.AutoSize = true;
            lblEscala.Location = new Point(165, 535);
            lblEscala.Name = "lblEscala";
            lblEscala.Size = new Size(161, 15);
            lblEscala.TabIndex = 15;
            lblEscala.Text = "1 = Muy mala / 5 = Excelente";
            // 
            // numEstabilidad
            // 
            numEstabilidad.Location = new Point(30, 535);
            numEstabilidad.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numEstabilidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEstabilidad.Name = "numEstabilidad";
            numEstabilidad.Size = new Size(120, 23);
            numEstabilidad.TabIndex = 14;
            numEstabilidad.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblPregunta5
            // 
            lblPregunta5.AutoSize = true;
            lblPregunta5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta5.Location = new Point(30, 500);
            lblPregunta5.Name = "lblPregunta5";
            lblPregunta5.Size = new Size(378, 20);
            lblPregunta5.TabIndex = 13;
            lblPregunta5.Text = "¿Cómo califica la estabilidad de su conexión a internet?";
            // 
            // cmbSatisfaccion
            // 
            cmbSatisfaccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSatisfaccion.FormattingEnabled = true;
            cmbSatisfaccion.Items.AddRange(new object[] { "Sí", "Parcialmente", "No" });
            cmbSatisfaccion.Location = new Point(30, 445);
            cmbSatisfaccion.Name = "cmbSatisfaccion";
            cmbSatisfaccion.Size = new Size(300, 23);
            cmbSatisfaccion.TabIndex = 12;
            // 
            // lblPregunta4
            // 
            lblPregunta4.AutoSize = true;
            lblPregunta4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta4.Location = new Point(30, 410);
            lblPregunta4.Name = "lblPregunta4";
            lblPregunta4.Size = new Size(296, 20);
            lblPregunta4.TabIndex = 11;
            lblPregunta4.Text = "¿Está satisfecho con su servicio de internet?";
            // 
            // lblMbps
            // 
            lblMbps.AutoSize = true;
            lblMbps.Location = new Point(240, 355);
            lblMbps.Name = "lblMbps";
            lblMbps.Size = new Size(37, 15);
            lblMbps.TabIndex = 10;
            lblMbps.Text = "Mbps";
            // 
            // numVelocidad
            // 
            numVelocidad.Location = new Point(30, 355);
            numVelocidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numVelocidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numVelocidad.Name = "numVelocidad";
            numVelocidad.Size = new Size(200, 23);
            numVelocidad.TabIndex = 9;
            numVelocidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPregunta3
            // 
            lblPregunta3.AutoSize = true;
            lblPregunta3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta3.Location = new Point(30, 320);
            lblPregunta3.Name = "lblPregunta3";
            lblPregunta3.Size = new Size(308, 20);
            lblPregunta3.TabIndex = 8;
            lblPregunta3.Text = "¿Qué velocidad de internet tiene contratada?";
            // 
            // lblMoneda
            // 
            lblMoneda.AutoSize = true;
            lblMoneda.Location = new Point(236, 267);
            lblMoneda.Name = "lblMoneda";
            lblMoneda.Size = new Size(19, 15);
            lblMoneda.TabIndex = 7;
            lblMoneda.Text = "Bs";
            // 
            // numPagoMensual
            // 
            numPagoMensual.DecimalPlaces = 2;
            numPagoMensual.Location = new Point(30, 265);
            numPagoMensual.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numPagoMensual.Name = "numPagoMensual";
            numPagoMensual.Size = new Size(200, 23);
            numPagoMensual.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(30, 230);
            label1.Name = "label1";
            label1.Size = new Size(384, 20);
            label1.TabIndex = 5;
            label1.Text = "¿Cuánto paga mensualmente por su servicio de internet?";
            label1.Click += label1_Click;
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Items.AddRange(new object[] { "Entel", "Tigo", "Viva", "Otro" });
            cmbProveedor.Location = new Point(30, 175);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(750, 23);
            cmbProveedor.TabIndex = 4;
            // 
            // lblPregunta1
            // 
            lblPregunta1.AutoSize = true;
            lblPregunta1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPregunta1.Location = new Point(30, 140);
            lblPregunta1.Name = "lblPregunta1";
            lblPregunta1.Size = new Size(243, 20);
            lblPregunta1.TabIndex = 3;
            lblPregunta1.Text = "¿Qué proveedor de internet utiliza?";
            // 
            // lblDescripcion
            // 
            lblDescripcion.Font = new Font("Segoe UI Historic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDescripcion.Location = new Point(10, 65);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(800, 55);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Responde las siguientes preguntas. Tus respuestas permitirán analizar la calidad de los servicios de internet.";
            // 
            // lblTituloEncuesta
            // 
            lblTituloEncuesta.Font = new Font("Segoe UI", 17.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloEncuesta.Location = new Point(10, 10);
            lblTituloEncuesta.Name = "lblTituloEncuesta";
            lblTituloEncuesta.Size = new Size(800, 50);
            lblTituloEncuesta.TabIndex = 1;
            lblTituloEncuesta.Text = "Encuesta sobre proveedores de internet";
            lblTituloEncuesta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormEncuesta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(884, 661);
            Controls.Add(pnlEncuesta);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormEncuesta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataStats - Nueva encuesta";
            pnlEncuesta.ResumeLayout(false);
            pnlEncuesta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numEstabilidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVelocidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPagoMensual).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncuesta;
        private Label lblTituloEncuesta;
        private Label lblDescripcion;
        private Label lblPregunta1;
        private ComboBox cmbProveedor;
        private Label label1;
        private NumericUpDown numPagoMensual;
        private Label lblPregunta3;
        private Label lblMoneda;
        private Label lblMbps;
        private NumericUpDown numVelocidad;
        private ComboBox cmbSatisfaccion;
        private Label lblPregunta4;
        private NumericUpDown numEstabilidad;
        private Label lblPregunta5;
        private Label lblPregunta6;
        private Label lblEscala;
        private ComboBox cmbProblemas;
        private Label lblPregunta7;
        private ComboBox cmbRecomendacion;
        private ComboBox cmbMejora;
        private Label lblPregunta8;
        private Button btnGuardar;
        private Button btnVolver;
    }
}