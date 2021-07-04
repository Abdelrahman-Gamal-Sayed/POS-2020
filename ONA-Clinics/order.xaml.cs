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

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for order.xaml
    /// </summary>
    public partial class order : UserControl
    {
        public order()
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
                    totalprice.Text = (Convert.ToDouble(price.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

                }
                else
                {
                    txtQTY.Text = "1";
                    totalprice.Text = price.Content.ToString();
                    totalprice.Text = (Convert.ToDouble(price.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

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
                    totalprice.Text = (Convert.ToDouble(price.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

                }
                else
                {
                    txtQTY.Text = "1";
                    totalprice.Text = price.Content.ToString();
                }







            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }




        private void txtQTY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(disc.Text.Trim() == "")
                    disc.Text = "0";
                if (totalprice == null)
                    return;





                if(( Convert.ToInt32(totalQTYstoreDB.Text) - Convert.ToInt32(txtQTY.Text) ) < 0) {

                    MessageBox.Show("لا يوجد كمية كافية فى المخزن");
                    txtQTY.Text = totalQTYstoreDB.Text;

                } else 
               if(( Convert.ToInt32(totalQTYstoreDB.Text) - Convert.ToInt32(txtQTY.Text) ) <= Convert.ToInt32(alarmstoreDB.Text) && Convert.ToInt32(totalQTYstore.Text)!=0 ) {

                        MessageBox.Show("هذه الكمية على وشك النفاذ");
                        //  txtQTY.Text = ( Convert.ToInt32(txtQTY.Text) + 1 ).ToString();

                    }
                
                totalQTYstore.Text = ( Convert.ToInt32(totalQTYstoreDB.Text) - Convert.ToInt32(txtQTY.Text) ).ToString();
                double priceq = 0;
                if (double.TryParse(price.Content.ToString(), out priceq))
                {
                    

                    totalprice.Text = (priceq * Convert.ToUInt32(txtQTY.Text) * ( 100 - Convert.ToDouble(disc.Text) ) / 100 ).ToString();



                }
                else
                {

                    totalprice.Text = priceq.ToString();
                }
            }
            catch { }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e) {

            try {
                if(comboBox.Text.Trim() == "جملة") price.Content = price_package.Content;
            else price.Content = price_unit.Content;

         
                if(totalprice == null)
                    return;
                double priceq = 0;
                if(double.TryParse(price.Content.ToString(), out priceq)) {


                    totalprice.Text = ( priceq * Convert.ToUInt32(txtQTY.Text) * ( 100 - Convert.ToDouble(disc.Text) ) / 100 ).ToString();



                } else {

                    totalprice.Text = priceq.ToString();
                }
            } catch { }

        }
    }
}
