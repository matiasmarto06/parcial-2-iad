using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class MainView : Form
    {
        private Label lblInstruction;
        private DataGridView inputGrid;
        private Button btnConfirm;
        private Button btnCalculate;
        private DataGridView outputGrid;
        private Label lblRecommendation;

        private const double Capital = 850000.0;

        public MainView()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Investment Analyzer";
            this.Size = new Size(600, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 1. Primary Instruction
            lblInstruction = new Label { Top = 15, Left = 20, Width = 540, Height = 40, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblInstruction.Text = "Ingrese los valores históricos de los plazos fijos anuales de los últimos 3 años para cada banco.";

            // 2. Transposed Input Grid (Banks on X, Years on Y)
            inputGrid = new DataGridView { Top = 60, Left = 20, Width = 540, Height = 135, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            inputGrid.Columns.Add("Period", "Periodo");
            inputGrid.Columns.Add("Provincia", "Banco Provincia (%)");
            inputGrid.Columns.Add("Nacion", "Banco Nación (%)");
            inputGrid.Columns.Add("Hipotecario", "Banco Hipotecario (%)");
            inputGrid.Columns[0].ReadOnly = true;

            inputGrid.Rows.Add("Año 1", "", "", "");
            inputGrid.Rows.Add("Año 2", "", "", "");
            inputGrid.Rows.Add("Año 3", "", "", "");
            inputGrid.Rows.Add("Promedio", "-", "-", "-");

            // Protect and style the Average row
            inputGrid.Rows[3].ReadOnly = true;
            inputGrid.Rows[3].DefaultCellStyle.BackColor = Color.LightGray;

            // 3. Sequential Action Buttons
            btnConfirm = new Button { Top = 210, Left = 20, Width = 260, Height = 40, Text = "Confirm Data", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnConfirm.Click += BtnConfirm_Click;

            btnCalculate = new Button { Top = 210, Left = 300, Width = 260, Height = 40, Text = $"Calculate (${Capital:N0})", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnCalculate.Enabled = false; // Locked until Confirm succeeds
            btnCalculate.Click += BtnCalculate_Click;

            // 4. Output Grid (Disabled until Calculate)
            outputGrid = new DataGridView { Top = 265, Left = 20, Width = 540, Height = 110, AllowUserToAddRows = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            outputGrid.Enabled = false;
            outputGrid.Columns.Add("Bank", "Bank");
            outputGrid.Columns.Add("Annual", "Annual ($)");
            outputGrid.Columns.Add("Quarterly", "Quarterly ($)");
            outputGrid.Columns.Add("Monthly", "Monthly ($)");

            outputGrid.Rows.Add("Provincia", "-", "-", "-");
            outputGrid.Rows.Add("Nacion", "-", "-", "-");
            outputGrid.Rows.Add("Hipotecario", "-", "-", "-");

            // 5. Recommendation Label (Hidden until Calculate)
            lblRecommendation = new Label { Top = 390, Left = 20, Width = 540, Height = 100, Font = new Font("Segoe UI", 11, FontStyle.Regular), ForeColor = Color.DarkGreen };
            lblRecommendation.Visible = false;

            this.Controls.Add(lblInstruction);
            this.Controls.Add(inputGrid);
            this.Controls.Add(btnConfirm);
            this.Controls.Add(btnCalculate);
            this.Controls.Add(outputGrid);
            this.Controls.Add(lblRecommendation);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            // TODO: Parse inputs, calculate averages, write to Promedio row, and enable btnCalculate.
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // TODO: Run the compounding math, populate outputGrid, and display lblRecommendation.
        }
    }
}
