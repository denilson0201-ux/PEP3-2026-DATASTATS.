namespace DataStats.Forms
{
    partial class FormConsultar
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
            dgvEncuestas = new DataGridView();
            btnActualizar = new Button();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEncuestas).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Emoji", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(240, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Encuestas registradas";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvEncuestas
            // 
            dgvEncuestas.AllowUserToAddRows = false;
            dgvEncuestas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEncuestas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEncuestas.Location = new Point(20, 75);
            dgvEncuestas.MultiSelect = false;
            dgvEncuestas.Name = "dgvEncuestas";
            dgvEncuestas.ReadOnly = true;
            dgvEncuestas.RowHeadersVisible = false;
            dgvEncuestas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEncuestas.Size = new Size(1040, 450);
            dgvEncuestas.TabIndex = 1;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.SteelBlue;
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(650, 550);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(180, 45);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar datos";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.LightGray;
            btnVolver.ForeColor = Color.Black;
            btnVolver.Location = new Point(850, 550);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(180, 45);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            // 
            // FormConsultar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1084, 611);
            Controls.Add(btnVolver);
            Controls.Add(btnActualizar);
            Controls.Add(dgvEncuestas);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormConsultar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataStats - Consultar encuestas";
            ((System.ComponentModel.ISupportInitialize)dgvEncuestas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private DataGridView dgvEncuestas;
        private Button btnActualizar;
        private Button btnVolver;
    }
}