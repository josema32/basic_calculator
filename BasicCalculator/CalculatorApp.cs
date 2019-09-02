using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicCalculator;
using BasicCalculatorLibrary;
using System.IO;

namespace BasicCalculator
{
    public partial class CalculatorApp : Form
    {
        private string filePath = @"C:\Temp\Operations.txt";
        OperationsLog finalOperations = new OperationsLog();
        List<OperationsLog> operations = new List<OperationsLog>();
        BindingSource ListBinding = new BindingSource();

        double ResultValue = 0;
        string OperationPerformed = "";
        //bool isOperationPerformed = false;

        public CalculatorApp()
        {
            InitializeComponent();
            SetUpBinding();
            LoadDataFromFile();
        }

        private void numberButton_Click(object sender, EventArgs e)
        {
            Button CalcNumberButton = (Button)sender;

            if ((ResultsDisplayBox.Text == "0"))
            {
                ResultsDisplayBox.Clear();
            }
            
           // isOperationPerformed = false;
            ResultsDisplayBox.Text += CalcNumberButton.Text;
            ChainOperationLabel.Text += CalcNumberButton.Text;
        }

        private void ResultsDisplayBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void ButtonAllClear_Click(object sender, EventArgs e)
        {
            ResultsDisplayBox.Clear();
            ResultsDisplayBox.Text = "0";
            ResultValue = 0;
            ChainOperationLabel.Text = "";
        }

        private void operationButton_Click(object sender, EventArgs e)
        {
            Button CalcOperationButton = (Button)sender;
            OperationPerformed = CalcOperationButton.Text;
            ChainOperationLabel.Text += CalcOperationButton.Text;
            ResultValue = Convert.ToDouble(ResultsDisplayBox.Text);
            ResultsDisplayBox.Text = "";
            //isOperationPerformed = true;
        }

        private void OperationData()
        {
            operations.Add(new  OperationsLog { FinishedOperation = ChainOperationLabel.Text});
        }

        private void SetUpBinding()
        {
            ListBinding.DataSource = operations;
            OperationsListBox.DataSource = ListBinding;
            OperationsListBox.DisplayMember = "DisplayText";
        }
        private void ResetAllBindings()
        {
            SaveDataToFile();
            ListBinding.ResetBindings(false);
            
        }

        private void SaveDataToFile()
        {
            List<string> output = new List<string>();

            foreach (OperationsLog o in operations)
            {
                output.Add(o.DisplayText);
            }
            File.WriteAllLines(filePath, output);
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(filePath) == false)
            {
                return;
            }

            List<string> rows = File.ReadAllLines(filePath).ToList();

            foreach (string row in rows)
            {
                operations.Add(new OperationsLog { FinishedOperation = row });
            }

        }
        private void ManageOperation()
        {
            ChainOperationLabel.Text += " = " + ResultsDisplayBox.Text;
            OperationData();
            ChainOperationLabel.Text = "";
            ChainOperationLabel.Text += ResultsDisplayBox.Text;
            ResetAllBindings();
        }
        private void ButtonEqual_Click(object sender, EventArgs e)
            {
                
                switch (OperationPerformed)
                {
                    case "+":
                        ResultsDisplayBox.Text = Convert.ToString(ResultValue + Convert.ToDouble(ResultsDisplayBox.Text));
                        ManageOperation();
                    break;
                    case "-":
                        ResultsDisplayBox.Text = Convert.ToString(ResultValue - Convert.ToDouble(ResultsDisplayBox.Text));
                        ManageOperation();
                    break;
                    case "X":
                        ResultsDisplayBox.Text = Convert.ToString(ResultValue * Convert.ToDouble(ResultsDisplayBox.Text));
                        ManageOperation();
                    break;
                    case "/":
                        ResultsDisplayBox.Text = Convert.ToString(ResultValue / Convert.ToDouble(ResultsDisplayBox.Text));
                        ManageOperation();
                        break;
                    default:
                        break;
                }
            }

        private void ButtonSquare_Click(object sender, EventArgs e)
            {
                ResultsDisplayBox.Text = Convert.ToString(Convert.ToDouble(ResultsDisplayBox.Text) * 2);
            }

        private void ButtonCube_Click(object sender, EventArgs e)
            {
                ResultsDisplayBox.Text = Convert.ToString(Convert.ToDouble(ResultsDisplayBox.Text) * 3);
            }

        private void CalculatorApp_Load(object sender, EventArgs e)
        {
           LoadDataFromFile();
        }
    }
}
