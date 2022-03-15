using System;
using System.Collections.Generic;
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

namespace Lab1_Kalkulator
{
    public partial class MainWindow : Window
    {
        private Boolean shouldClear = false;
        private String firstNumber = "0";
        private String secondNumber = "0";
        private String result = "0";
        private readonly String sysSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public MainWindow()
        {
            InitializeComponent();
            buttonSeparator.Content = sysSeparator;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            var textBox = resultTextBox as TextBox;
            var currentText = textBox.Text;

            if (e.Key == Key.Back)
            {
                textBox.Text = currentText.Length == 1 ? "0" : currentText.Remove(currentText.Length - 1);
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var btnContent = btn.Content as String;

            var textBox = resultTextBox as TextBox;
            var text = textBox.Text as String;

            var signBox = signTextBox as TextBox;
            var sign = signBox.Text as String;            

            if 
                (
                btnContent == "0" ||
                btnContent == "1" ||
                btnContent == "2" ||
                btnContent == "3" ||
                btnContent == "4" ||
                btnContent == "5" ||
                btnContent == "6" ||
                btnContent == "7" ||
                btnContent == "8" ||
                btnContent == "9" ||
                btnContent == sysSeparator ||
                btnContent == "+/-"
                )
            {
                if (btnContent == "+/-") shouldClear = false;
                textBox.Text = shouldClear ? "0" : text;
                UpdateTextBox(btnContent);
                shouldClear = false;
            }

            switch (btnContent)
            {
                case "AC":
                    textBox.Text = "0";
                    signBox.Text = "";
                    break;
                case "x²":
                    textBox.Text = ToSquare(text);
                    break;
                case "%":
                    textBox.Text = OnePercent(text);
                    break;
                case "+":
                case "-":
                case "*":
                case "÷":
                    signBox.Text = btnContent;
                    firstNumber = text;
                    shouldClear = true;
                    break;
                case "=":
                    secondNumber = text;
                    result = !String.IsNullOrEmpty(sign) ? Calculate(firstNumber, secondNumber, sign) : secondNumber;
                    shouldClear = true;
                    signBox.Text = "";
                    textBox.Text = result;
                    break;
            }
        }

        private void UpdateTextBox(string sign)
        {
            var textBox = resultTextBox as TextBox;
            String currentText = textBox.Text;
            String newText = "0";
            if (sign != "+/-" && sign != ",")
            {
                newText = currentText == "0" ? sign : (currentText + sign);
            }
            else
            {
                if (sign == "+/-")
                {
                    if (currentText != "0")
                    {
                        newText = currentText[0] == '-' ? currentText.Remove(0, 1) : ("-" + currentText);
                    }
                }
                else if (sign == ",")
                {
                    newText = currentText.Contains(",") ? currentText : currentText + sign;
                }
            }

            textBox.Text = newText;
        }

        private string ToSquare(String number)
        {
            if (double.TryParse(number, out double x))
            {
                x *= x;
                return x.ToString();
            }
            MessageBox.Show("Błąd konwersji liczby");
            return "Error!";
        }

        private string OnePercent(String number)
        {
            if (double.TryParse(number, out double x))
            {
                x /= 100;
                return x.ToString();
            }
            MessageBox.Show("Błąd konwersji liczby");
            return "Error!";
        }

        private string Calculate(String firstNumber, String secondNumber, String sign)
        {
            if (sign == "+")
            {
                if (double.TryParse(firstNumber, out double x) && double.TryParse(secondNumber, out double y))
                {
                    Double result = x + y;
                    return result.ToString();
                }
                MessageBox.Show("Błąd konwersji liczby");
                return "Error!";
            }

            if (sign == "-")
            {
                if (double.TryParse(firstNumber, out double x) && double.TryParse(secondNumber, out double y))
                {
                    Double result = x - y;
                    return result.ToString();
                }
                MessageBox.Show("Błąd konwersji liczby");
                return "Error!";
            }

            if (sign == "*")
            {
                if (double.TryParse(firstNumber, out double x) && double.TryParse(secondNumber, out double y))
                {
                    Double result = x * y;
                    return result.ToString();
                }
                MessageBox.Show("Błąd konwersji liczby");
                return "Error!";
            }

            if (sign == "÷")
            {
                if (secondNumber != "0")
                {
                    if (double.TryParse(firstNumber, out double x) && double.TryParse(secondNumber, out double y))
                    {
                        Double result = x / y;
                        return result.ToString();
                    }
                    MessageBox.Show("Błąd konwersji liczby");
                    return "Error!";
                }
                MessageBox.Show("Dzielenie przez zero", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Error!";
            }

            MessageBox.Show("Nierozpoznany znak");
            return "Error!";
        }
    }
}
