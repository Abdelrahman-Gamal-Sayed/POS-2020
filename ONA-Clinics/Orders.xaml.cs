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
using System.Data;

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class Orders : Window
    {

        DataTable test =new DataTable();
        public Orders()
        {
            InitializeComponent();
            clearGrids();
            checkouthotry();
        }
        private void checkouthotry() {

            if(!( User.main_form == "Y" || User.main_form == "y" ))
                btn_main_form.Visibility = Visibility.Collapsed;
            if(!( User.casher_form == "Y" || User.casher_form == "y" ))
                btn_casher_form.Visibility = Visibility.Collapsed;
            if(!( User.kitchen_form == "Y" || User.kitchen_form == "y" ))
                btn_kitchen_form.Visibility = Visibility.Collapsed;
            if(!( User.report_form == "Y" || User.report_form == "y" ))
                btn_report_form.Visibility = Visibility.Collapsed;
            if(!( User.view_form == "Y" || User.view_form == "y" ))
                btn_view_form.Visibility = Visibility.Collapsed;


        }

        private void btn_main_form_Click(object sender, RoutedEventArgs e) {
            new BasicFRM().Show();
            this.Close();
        }

        private void btn_casher_form_Click(object sender, RoutedEventArgs e) {
            new MainFRM().Show();
            this.Close();
        }

        private void btn_kitchen_form_Click(object sender, RoutedEventArgs e) {
            new Orders_DONE().Show();
            this.Close();
        }

        private void btn_view_form_Click(object sender, RoutedEventArgs e) {
            new Orders().Show();
            this.Close();
        }

        private void btn_report_form_Click(object sender, RoutedEventArgs e) {
            new reportFRM().Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e) {

            this.WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRestore.Visibility = Visibility.Visible;

        }

        private void Restore_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Collapsed;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }

        void clearGrids()
        {
            //g_Medicine.Visibility = Visibility.Collapsed;
            g_Users.Visibility = Visibility.Collapsed;
            g_Specialty.Visibility = Visibility.Collapsed;
        }
      
        private void LogOutBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenMenueBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenMenueBTN.Visibility = Visibility.Collapsed;
            CloseMenueBTN.Visibility = Visibility.Visible;
        }

        private void CloseMenueBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenMenueBTN.Visibility = Visibility.Visible;
            CloseMenueBTN.Visibility = Visibility.Collapsed;

        }

      

        private void SwitchBTN_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearGrids();
            switch (mainList.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    g_Users.Visibility = Visibility.Visible;
                    break;
                case 2:

                   // g_Medicine.Visibility = Visibility.Visible;
                    break;
                case 3:
                    g_Specialty.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        DB db = new DB();
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dg_orders.ItemsSource= db.RunReader(@" select distinct  'رقم الطلب ' 
, invoice_id
from POS_RECIEPT_V
where invoice_status ='inprogress' and INVOICE_TYPE='Cash'
order by invoice_id").DefaultView;
        }
    }
}
