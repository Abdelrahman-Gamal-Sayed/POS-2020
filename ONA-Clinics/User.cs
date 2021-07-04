using System.Windows.Controls;
using System.Net.NetworkInformation;

namespace ONA_Stores {
    public static class User {

        public static string Code { get; set; }
        public static string Name { get; set; }
        public static string Employee_ID { get; set; }
      

        public static string ACCESS_ID { get; set; }
        public static string ACCESS_TYPE { get; set; }
        public static string main_form { get; set; }
        public static string casher_form { get; set; }
        public static string report_form { get; set; }
        public static string view_form { get; set; }
        public static string kitchen_form { get; set; }


        public static string groub_code { get; set; }
        public static void CleanAll(this Panel s) {

            foreach(System.Windows.UIElement child in s.Children) {


                StackPanel ST = child as StackPanel;
                if(ST != null)
                    ST.CleanAll();
                GroupBox gb = child as GroupBox;
                if(gb != null)
                    ( (Grid)gb.Content ).CleanAll();

                Grid dg = child as Grid;
                if(dg != null)
                    dg.CleanAll();
                CheckBox chb = child as CheckBox;
                if(chb != null)
                    chb.IsChecked = false;
                TextBox txt = child as TextBox;
                if(txt != null)
                    txt.Text = "";
                ComboBox cbx = child as ComboBox;
                if(cbx != null)
                    cbx.Text = "";
                DatePicker dp = child as DatePicker;
                if(dp != null)
                    dp.Text = "";
                DataGrid DG = child as DataGrid;
                if(DG != null)
                    DG.ItemsSource = null;
                Image im = child as Image;
                if(im != null)
                    im.Source = null;


            }

        }

        public static PhysicalAddress GetMacAddress() {
            foreach(NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
                // Only consider Ethernet network interfaces
                if(nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up) {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }


     

        /*
   private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
   {
       System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
       e.Handled = reg.IsMatch(e.Text);

   }
         
   private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
   {
       System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
       e.Handled = reg.IsMatch(e.Text);

   }
   */

    }
}
