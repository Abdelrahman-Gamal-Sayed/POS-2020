using System;
using System.Windows;
using System.Windows.Controls;

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for order.xaml
    /// </summary>
    public partial class orderback : UserControl
    {
        public orderback()
        {
            InitializeComponent();
        }
    
        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }

        string gettotal()
        {
            return totalprice.Text.ToString();
        }
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int oldqty = 0;
                if (int.TryParse(txtQTY.Text, out oldqty))
                {
                    txtQTY.Text = (oldqty + 1).ToString();
                    totalprice.Text = (Convert.ToDouble(price.Text.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

                }
                else
                {
                    txtQTY.Text = "1";
                    totalprice.Text = price.Text.ToString();
                    totalprice.Text = (Convert.ToDouble(price.Text.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

                }
            }
            catch { }

        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int oldqty = 0;
                if (int.TryParse(txtQTY.Text, out oldqty))
                {
                    if (oldqty - 1 < 0)
                        oldqty = 1;

                    txtQTY.Text = (oldqty - 1).ToString();
                    totalprice.Text = (Convert.ToDouble(price.Text.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

                }
                else
                {
                    txtQTY.Text = "1";
                    totalprice.Text = price.Text.ToString();
                }







            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }




        private void txtQTY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (totalprice == null)
                    return;
                double priceq = 0;
                if (double.TryParse(price.Text.ToString(), out priceq))
                {


                    totalprice.Text = (priceq * Convert.ToUInt32(txtQTY.Text)).ToString();



                }
                else
                {

                    totalprice.Text = priceq.ToString();
                }
            }
            catch { }
        }
       
    }
}
