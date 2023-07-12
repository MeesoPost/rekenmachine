using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reken_Machine
{
    public partial class MainWindow : Window
    {
        private double firstNumber; // Houdt het eerste getal bij dat wordt ingevoerd voor de berekening.
        private string selectedOperator; // Houdt de geselecteerde operator bij (+, -, x, ÷).
        private bool isOperatorClicked = false; // Geeft aan of er al een operator is geselecteerd.

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (isOperatorClicked)
            {
                ResultTextBox.Text = digit; // Als er al een operator is geselecteerd, vervang de inhoud van het tekstvak door het nieuwe cijfer.
                isOperatorClicked = false;
            }
            else
            {
                if (ResultTextBox.Text == "0" && digit != ",") // Als het tekstvak gelijk is aan "0" en het nieuwe cijfer is geen decimaal punt, vervang de inhoud door het nieuwe cijfer.
                {
                    ResultTextBox.Text = digit;
                }
                else
                {
                    ResultTextBox.Text += digit; // Voeg het nieuwe cijfer toe aan de bestaande inhoud van het tekstvak.
                }
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            selectedOperator = button.Content.ToString();

            if (!string.IsNullOrEmpty(ResultTextBox.Text)) // Als het tekstvak niet leeg is
            {
                firstNumber = double.Parse(ResultTextBox.Text); // Sla het eerste getal op
                isOperatorClicked = true; // Stel in dat er een operator is geselecteerd
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double secondNumber = double.Parse(ResultTextBox.Text);
            double result = 0;

            switch (selectedOperator)
            {
                case "+":
                    result = firstNumber + secondNumber; // Optellen
                    break;
                case "-":
                    result = firstNumber - secondNumber; // Aftrekken
                    break;
                case "x":
                    result = firstNumber * secondNumber; // Vermenigvuldigen
                    break;
                case "÷":
                    if (secondNumber != 0)
                    {
                        result = firstNumber / secondNumber; // Delen
                    }
                    else
                    {
                        MessageBox.Show("Error: Division by zero is not allowed."); // Foutmelding bij deling door nul
                    }
                    break;
            }

            ResultTextBox.Text = result.ToString(); // Toon het resultaat in het tekstvak
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "0"; // Reset het tekstvak naar "0"
            firstNumber = 0; // Reset het eerste getal
            isOperatorClicked = false; // Reset de indicator voor de operator
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultTextBox.Text.Contains(",")) // Als het tekstvak geen decimaal punt bevat
            {
                ResultTextBox.Text += ","; // Voeg een decimaal punt toe aan het tekstvak
            }
        }
    }
}
