using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

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
        private Label lblCapital;
        private TextBox txtCapital;
        private Button btnEditCapital;

        private const double Capital = 850000.0;
        private double[] bankAverages = new double[3]; // Provincia, Nacion, Hipotecario
        public MainView()
        {
            InitializeComponent();
            BuildUI();
        }
        private void BuildUI()
        {
            this.Text = "Analizador de Inversiones - Parcial 2";
            this.Size = new Size(620, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = SystemColors.Control;

            lblInstruction = new Label { Top = 15, Left = 20, Width = 560, Height = 20, Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular) };
            lblInstruction.Text = "Ingrese los valores históricos de los plazos fijos anuales de los últimos 3 años (en porcentaje):";

            lblCapital = new Label { Top = 45, Left = 20, Width = 120, Height = 25, Text = "Capital a invertir: $", Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold) };
            txtCapital = new TextBox { Top = 42, Left = 140, Width = 120, Text = "850000", ReadOnly = true }; 
            btnEditCapital = new Button { Top = 40, Left = 270, Width = 80, Height = 25, Text = "Editar", Cursor = Cursors.Hand };

            inputGrid = new DataGridView
            {
                Top = 80,
                Left = 20,
                Width = 560,
                Height = 130,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                BorderStyle = BorderStyle.Fixed3D,
                RowHeadersVisible = false
            };

            inputGrid.Columns.Add("Period", "Periodo");
            inputGrid.Columns.Add("Provincia", "Banco Provincia (%)");
            inputGrid.Columns.Add("Nacion", "Banco Nación (%)");
            inputGrid.Columns.Add("Hipotecario", "B. Hipotecario (%)");

            inputGrid.Columns[0].ReadOnly = true;
            inputGrid.Columns[0].DefaultCellStyle.BackColor = SystemColors.ControlLight;

            inputGrid.Rows.Add("Año 1", "", "", "");
            inputGrid.Rows.Add("Año 2", "", "", "");
            inputGrid.Rows.Add("Año 3", "", "", "");
            inputGrid.Rows.Add("Promedio", "-", "-", "-");

            inputGrid.Rows[3].ReadOnly = true;
            inputGrid.Rows[3].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            inputGrid.Rows[3].DefaultCellStyle.Font = new Font(inputGrid.Font, FontStyle.Bold);

            inputGrid.CellValueChanged += InputGrid_CellValueChanged;

            btnConfirm = new Button { Text = "1. Confirmar Datos", Top = 210, Left = 20, Width = 270, Height = 35, Cursor = Cursors.Hand };
            btnConfirm.Click += BtnConfirm_Click;

            btnCalculate = new Button { Text = $"2. Calcular (Capital: ${Capital:N0})", Top = 210, Left = 310, Width = 270, Height = 35, Cursor = Cursors.Hand };
            btnCalculate.Enabled = false;
            btnCalculate.Click += BtnCalculate_Click;

            outputGrid = new DataGridView
            {
                Top = 270,
                Left = 20,
                Width = 560,
                Height = 110,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                Enabled = false,
                BorderStyle = BorderStyle.Fixed3D,
                RowHeadersVisible = false
            };

            outputGrid.Columns.Add("Modality", "Modalidad");
            outputGrid.Columns.Add("Provincia", "Banco Provincia");
            outputGrid.Columns.Add("Nacion", "Banco Nación");
            outputGrid.Columns.Add("Hipotecario", "B. Hipotecario");

            outputGrid.Columns[0].ReadOnly = true;
            outputGrid.Columns[0].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            outputGrid.Columns[0].DefaultCellStyle.Font = new Font(outputGrid.Font, FontStyle.Bold);

            outputGrid.Rows.Add("Anual ($)", "-", "-", "-");
            outputGrid.Rows.Add("Trimestral ($)", "-", "-", "-");
            outputGrid.Rows.Add("Mensual ($)", "-", "-", "-");

            lblRecommendation = new Label { Top = 400, Left = 20, Width = 560, Height = 100, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            lblRecommendation.Visible = false;

            btnConfirm.Top = 230;
            btnCalculate.Top = 230;
            outputGrid.Top = 290;
            lblRecommendation.Top = 420;

            this.Controls.Add(lblInstruction);
            this.Controls.Add(inputGrid);
            this.Controls.Add(btnConfirm);
            this.Controls.Add(btnCalculate);
            this.Controls.Add(outputGrid);
            this.Controls.Add(lblRecommendation);
            this.Controls.Add(lblCapital);
            this.Controls.Add(txtCapital);
            this.Controls.Add(btnEditCapital);
        }
        private void InputGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (btnCalculate != null)
            {
                btnCalculate.Enabled = false;
            }
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateAndCalculateAverages())
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos y positivos para todos los años.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCalculate.Enabled = false;
                return;
            }

            for (int col = 1; col <= 3; col++)
            {
                inputGrid.Rows[3].Cells[col].Value = bankAverages[col - 1].ToString("F2");
            }

            btnCalculate.Enabled = true;
            MessageBox.Show("Datos confirmados. Promedios calculados con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool ValidateAndCalculateAverages()
        {
            bool isGridValid = true;
            ResetGridColors();

            for (int col = 1; col <= 3; col++)
            {
                double sum = 0;
                bool isColumnValid = true;

                for (int row = 0; row < 3; row++)
                {
                    var cell = inputGrid.Rows[row].Cells[col];

                    if (cell.Value == null)
                    {
                        cell.Style.BackColor = Color.LightCoral;
                        isGridValid = false;
                        isColumnValid = false;
                        continue;
                    }

                    string rawValue = cell.Value.ToString().Replace(',', '.');

                    if (!double.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double rate) || rate <= 0 || rate > 1000)
                    {
                        cell.Style.BackColor = Color.LightCoral;
                        isGridValid = false;
                        isColumnValid = false;
                    }
                    else
                    {
                        sum += rate;
                    }
                }

               
                if (isColumnValid)
                {
                    bankAverages[col - 1] = sum / 3.0;
                }
            }

            return isGridValid;
        }
        private void ResetGridColors()
        {
            for (int col = 1; col <= 3; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    inputGrid.Rows[row].Cells[col].Style.BackColor = Color.White;
                }
            }   
        }
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            outputGrid.Enabled = true;

            double maxReturn = 0;
            string bestBank = "";
            string bestModality = "";

            string[] bankNames = { "Banco Provincia", "Banco Nación", "Banco Hipotecario" };

            for (int i = 0; i < 3; i++)
            {
                double annualRateDecimal = bankAverages[i] / 100.0;

                double totalAnnual = Capital * (1 + annualRateDecimal);
                double totalQuarterly = Capital * Math.Pow(1 + (annualRateDecimal / 4.0), 4);
                double totalMonthly = Capital * Math.Pow(1 + (annualRateDecimal / 12.0), 12);

                double yieldAnnual = totalAnnual - Capital;
                double yieldQuarterly = totalQuarterly - Capital;
                double yieldMonthly = totalMonthly - Capital;

                outputGrid.Rows[0].Cells[i + 1].Value = yieldAnnual.ToString("C2");
                outputGrid.Rows[1].Cells[i + 1].Value = yieldQuarterly.ToString("C2");
                outputGrid.Rows[2].Cells[i + 1].Value = yieldMonthly.ToString("C2");

                if (yieldMonthly > maxReturn) { maxReturn = yieldMonthly; bestBank = bankNames[i]; bestModality = "Mensual"; }
                if (yieldQuarterly > maxReturn) { maxReturn = yieldQuarterly; bestBank = bankNames[i]; bestModality = "Trimestral"; }
                if (yieldAnnual > maxReturn) { maxReturn = yieldAnnual; bestBank = bankNames[i]; bestModality = "Anual"; }
            }

            lblRecommendation.Text = $"CONCLUSIÓN:\n\nLa opción más rentable es invertir en {bestBank} bajo la modalidad {bestModality}.\nRendimientos: {maxReturn:C2}";
            lblRecommendation.Visible = true;
        }
    }
}