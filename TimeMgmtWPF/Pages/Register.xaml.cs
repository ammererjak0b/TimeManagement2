using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TimeMgmtLib;
using TimeMgmtLib.Models;

namespace TimeMgmtWPF.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBoxDouble(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ",")
            {
                if (!((TextBox)sender).Text.Contains(","))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int personal = Convert.ToInt32(txtPersonal.Text);
            double flexTime = Convert.ToDouble(txtFlexibleHours.Text);

            if(txtPw.Password != txtPwConfirm.Password)
            {

            }

            TimeMgmtFactory.Instance.UserInstance = new User(personal, txtPw.Password);
            TimeMgmtFactory.Instance.UserInstance.Register(txtPrename.Text, txtSurname.Text, DateTime.Today.Date, flexTime);
        }
    }
}
