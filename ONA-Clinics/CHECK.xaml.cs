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
using System.Windows.Shapes;

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for CHECK.xaml
    /// </summary>
    public partial class CHECK :Window
    {
        public CHECK() {
            InitializeComponent();
        }

        public  string checkNumber;
        public  DateTime dateCheck;
        public  bool Check=false;

        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e) {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            try {
                if(string.IsNullOrWhiteSpace(txt_price_items.Text)) {
                    MessageBox.Show("برجاء ادخال رقم الشيك");
                    return;
                }

                if(string.IsNullOrWhiteSpace(txt_parcedate_emp3.Text)) {
                    MessageBox.Show("برجاء ادخال تاريخ الاستحقاق");
                    return;
                }
                Check = true;
                checkNumber = txt_price_items.Text;
                dateCheck = Convert.ToDateTime(txt_parcedate_emp3.Text);
                this.Close();
            } catch { Check = false; }

        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e) {
            Check = false;
            this.Close();
        }
    }
}
