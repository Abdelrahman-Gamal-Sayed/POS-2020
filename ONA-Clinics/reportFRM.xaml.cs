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
    public partial class reportFRM : Window
    {

        DataTable test =new DataTable();
        public reportFRM()
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
            //if(!( User.kitchen_form == "Y" || User.kitchen_form == "y" ))
            //    btn_kitchen_form.Visibility = Visibility.Collapsed;
            //if(!( User.report_form == "Y" || User.report_form == "y" ))
            //    btn_report_form.Visibility = Visibility.Collapsed;
            //if(!( User.view_form == "Y" || User.view_form == "y" ))
            //    btn_view_form.Visibility = Visibility.Collapsed;


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
            g_mior_date_all.Visibility = Visibility.Collapsed;
            g_mior_date.Visibility = Visibility.Collapsed;
            g_mor_date_main.Visibility = Visibility.Collapsed;
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

                    g_mior_date.Visibility = Visibility.Visible;
                    g_mor_date_main.Visibility = Visibility.Visible;
                    break;
                case 2:

                    g_mior_date_all.Visibility = Visibility.Visible;
                    break;
                case 3:
                    // g_Specialty.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            g_mior_date.CleanAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            g_mor_date_main.CleanAll();
        }
        DB db = new DB();
        private void cbx_osserty_user_DropDownOpened(object sender, EventArgs e)
        {
            try {
                cbx_osserty_user.ItemsSource = db.RunReader("select USER_ID,USER_NAME       from USER  ").DefaultView;

            }
            catch { }
               }

        private void cbx_osserty_user2_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user2.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID,NAME from PRODUCT_CATEGORY  ").DefaultView;
        }

        private void cbx_osserty_user1_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user1.ItemsSource = db.RunReader("select PRODUCT_ID ,NAME from PRODUCT  where  PRODUCT_CATEGORY_ID='"+ cbx_osserty_user2 .Text+ "'   ").DefaultView;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            String wherequn = "";
            //string datatfrom = (txt_parcedate_emp2.Text.Trim() == string.Empty) ? "" : Convert.ToDateTime( txt_parcedate_emp2.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            //string datatto = (txt_parcedate_emp2.Text.Trim() == string.Empty) ? "" : Convert.ToDateTime(txt_parcedate_emp2.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            ////db.RunNonQuery("CREATE VIEW POS_RECIEPT_V AS SELECT INV.INVOICE_ID,      INV.DATEINVOICED,      inv.TIMEINVOICED,     inv.PAID,      INV.total,      inv.charge,      inv.USER_ID,      inv.INVOICE_STATUS,      INVOICE_TYPE,      u.user_name cashir,      inv.CLIENT_ID,      cl.name,       cl.NAME,      cl.MOBILE,      cl.PHONE,      cl.ADDRESS CLIENTaddress,      invl.INVOICE_LINE_ID,      invl.PRODUCT_ID,      invl.QTY,      invl.TOTALLINE,      p.name productname,       p.PRODUCT_CATEGORY_ID,      p.SALES_UNIT_PRICE,      p.PURCHASE_UNIT_PRICE,      invl.QTY * p.SALES_UNIT_PRICE linetotalamt,      pc.name categoryname FROM invoice inv      inner join INVOICE_LINE invl on invl.invoice_id = inv.INVOICE_id      inner join PRODUCT p on p.PRODUCT_id = invl.PRODUCT_id      inner join PRODUCT_CATEGORY pc on pc.PRODUCT_CATEGORY_id = p.PRODUCT_CATEGORY_id      LEFT JOIN USER u ON u.user_id = inv.user_id      LEFT JOIN CLIENT cl ON cl.client_id = inv.CLIENT_id       ");

            if (txt_parcedate_emp2.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( dateinvoiced)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp2.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),dateinvoiced)";
            if (txt_parcedate_e2mp2.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( dateinvoiced)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_e2mp2.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
            if (cbx_osserty_user1.Text.Trim() != string.Empty)
                wherequn = wherequn + " and  p.product_id  ='" + cbx_osserty_user1.Text + "'";
            if (cbx_osserty_user2.Text.Trim() != string.Empty)
                wherequn = wherequn + " and  PRODUCT_CATEGORY_ID  ='" + cbx_osserty_user2.Text + "'";
            if(seraaredio.IsChecked==true)
            {
                wherequn = wherequn + " and  invoice_type='Purchasing' ";
            }
            else
            {
                wherequn = wherequn + " and  invoice_type!='Purchasing' ";
            }

            dg_user_main1.ItemsSource= db.RunReader(@"select ROW_NUMBER() OVER(ORDER BY round(sum(il.qty)/p.package_size,2) desc) AS 'الترتيب', p.name AS 'الاسم' , p.product_id AS 'كود الصنف', round(sum(il.qty)/p.package_size,2)  AS 'الكمية' ,i.total AS 'الاجمالي'
from INVOICE_LINE il
inner join INVOICE i on i.INVOICE_id=il.INVOICE_id 
inner join PRODUCT p on p.product_id=il.product_id
where ''=''      " + wherequn + " group by p.name , p.product_id , p.package_size").DefaultView;

            // and issotrx = coalesce('', issotrx) and product_id = coalesce('" + cbx_osserty_user1.Text + "', product_id)   and invoice_type = coalesce('', invoice_type) and PRODUCT_CATEGORY_id = coalesce('" + cbx_osserty_user2.Text + "', PRODUCT_CATEGORY_id)
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string wherequn = " ";
            if (cbx_osserty_user.Text.Trim() != string.Empty)
                wherequn = wherequn + " and  i.CREATEDBY  ='" + cbx_osserty_user.Text + "'";
            if (cbx_osserty_user9.Text.Trim() != string.Empty)
                wherequn = wherequn + " and  i.CLIENT_ID  ='" + cbx_osserty_user9.Text + "'";
            if (txt_parcedate_emp.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( DATEINVOICED)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),DATEINVOICED)";
            if (txt_parcedate_emp1.Text.Trim() != string.Empty)
                wherequn = wherequn + " and DATE( DATEINVOICED)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp1.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
            if (seraaredio_Copy.IsChecked == true)
            {
                wherequn = wherequn + " and  invoice_type='Purchasing' ";
            }
            else
            {
                wherequn = wherequn + " and  invoice_type!='Purchasing' ";
            }

            DataTable dt = db.RunReader("select  i.invoice_id as 'رقم الفاتورة',i.CLIENT_ID  as 'كود العميل', c.name as 'اسم العميل',total as 'الاجمالي', dateinvoiced as 'تاريخ الفاتورة' , i.CREATEDBY as 'المحاسب' from INVOICE i left outer join CLIENT c on i.CLIENT_ID=c.CLIENT_ID  where coalesce( c.client_type,'C')=case when INVOICE_TYPE='Purchasing' then 'V' else 'C'  end  " + wherequn + " order by dateinvoiced desc");
            dg_user.ItemsSource=dt.DefaultView;
        }



        private void dg_user_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (dg_user.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_user.SelectedItem;
                    dg_user_Copy.ItemsSource = db.RunReader("select il.PRODUCT_ID as 'كود الصنف',p. name  as 'الاسم',  il.qty   as 'الكمية'    ,il.totalline as 'الاجمالي',Bonus as 'بونص'    from INVOICE_LINE il join PRODUCT p  on il.PRODUCT_ID=p.PRODUCT_ID  where INVOICE_ID='" + row[0].ToString() + "'  ").DefaultView;
                }
            }
            catch { }
        }

        private void dg_user_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            g_mior_date_all.CleanAll();
        }

        private void SettingBTN_Click(object sender, RoutedEventArgs e) {
            new SettingPage().Show();
        }

        private void cbx_osserty_user3_DropDownClosed(object sender, EventArgs e)
        {
            hidencom_rep();
            if (cbx_osserty_user3.SelectedIndex == 0)
            {
                seraaredio_Copy1.Visibility = Visibility.Visible;
                dddd.Visibility = Visibility.Visible;
                esreketogary.Visibility = Visibility.Visible;
            }
            else if (cbx_osserty_user3.SelectedIndex == 1)
                esreketogary_Copy.Visibility = Visibility.Visible;
            else if (cbx_osserty_user3.SelectedIndex == 2 || cbx_osserty_user3.SelectedIndex == 3)
            {
                typ.Visibility = Visibility.Visible;
                typuu.Visibility = Visibility.Visible;

            }
            else if (cbx_osserty_user3.SelectedIndex == 4 || cbx_osserty_user3.SelectedIndex == 5)
            {

                esreketogary_Copy1.Visibility = Visibility.Visible;
                //  typ.Visibility = Visibility.Visible;
                // typuu.Visibility = Visibility.Visible;
            }

        }
        void hidencom_rep()
        {
            seraaredio_Copy1.Visibility = Visibility.Hidden;
            dddd.Visibility = Visibility.Hidden;
            esreketogary.Visibility = Visibility.Hidden;
            esreketogary_Copy.Visibility = Visibility.Hidden;
            typ.Visibility = Visibility.Hidden;
            typuu.Visibility = Visibility.Hidden;
            esreketogary_Copy1.Visibility = Visibility.Hidden;

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
           
            if (cbx_osserty_user3.SelectedIndex == 0)
            {
                string dd = (seraaredio_Copy1.IsChecked == true) ? "C" : "V"; 
                string wherequn = "";
                if (txt_parcedate_emp3.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( AllocationDate)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp3.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),AllocationDate)";
                if (txt_parcedate_emp4.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( AllocationDate)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp4.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                if (cbx_osserty_user4.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  client_id  ='" + cbx_osserty_user4.Text + "'";
                try
                {
                    DataTable dt2 = db.RunReader("select * from allocation_v  where CLIENT_type = '" + dd + "'  " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        allocationrep rpon = new allocationrep();
                        rpon.Database.Tables["allocation_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();
                       
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        else    if (cbx_osserty_user3.SelectedIndex == 1)
            {
                string wherequn = "";
                if (txt_parcedate_emp3.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( createddate)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp3.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),createddate)";
                if (txt_parcedate_emp4.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( createddate)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp4.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                if (cbx_osserty_user5.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  USER_ID  ='" + cbx_osserty_user5.Text + "'";
                try
                {
                    DataTable dt2 = db.RunReader("select * from Expenses_v  where ''='' " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        expensesrep rpon = new expensesrep();
                        rpon.Database.Tables["Expenses_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();
                       
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (cbx_osserty_user3.SelectedIndex == 2)
            {
                string wherequn = "";
                if (cbx_osserty_user6.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  PRODUCT_CATEGORY_ID  ='" + cbx_osserty_user6.Text + "'";
                if(cbx_osserty_user7.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  PRODUCT_ID  ='" + cbx_osserty_user7.Text + "'";
                try
                {
                    DataTable dt2 = db.RunReader("select * from product_v  where ''='' " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        productqty rpon = new productqty();
                        rpon.Database.Tables["product_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();
                       
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (cbx_osserty_user3.SelectedIndex == 3)
            {
                string wherequn = "";
                if (cbx_osserty_user6.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  PRODUCT_CATEGORY_ID  ='" + cbx_osserty_user6.Text + "'";
                if (cbx_osserty_user7.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  PRODUCT_ID  ='" + cbx_osserty_user7.Text + "'";
                try
                {
                    DataTable dt2 = db.RunReader("select * from product_v  where (QTY<=Minqty or QTY is null ) " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        productqty rpon = new productqty();
                        rpon.Database.Tables["product_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (cbx_osserty_user3.SelectedIndex == 4)
            {
                string wherequn = "";
                if (txt_parcedate_emp3.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( DATEINVOICED)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp3.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),DATEINVOICED)";
                if (txt_parcedate_emp4.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( DATEINVOICED)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp4.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                if (cbx_osserty_user8.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  INVOICE_ID  ='" + cbx_osserty_user8.Text + "'";
  

                try
                {
                    DataTable dt2 = db.RunReader("select * from pos_purchasetotal_v    where ''=='' " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        totalpurchasing rpon = new totalpurchasing();
                        rpon.Database.Tables["pos_purchasetotal_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (cbx_osserty_user3.SelectedIndex == 5)
            {
                string wherequn = "";
                if (txt_parcedate_emp3.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( DATEINVOICED)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp3.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),DATEINVOICED)";
                if (txt_parcedate_emp4.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( DATEINVOICED)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp4.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                if (cbx_osserty_user8.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  INVOICE_ID  ='" + cbx_osserty_user8.Text + "'";
                if (cbx_osserty_user6.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  PRODUCT_CATEGORY_ID  ='" + cbx_osserty_user6.Text + "'";
                if (cbx_osserty_user7.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  PRODUCT_ID  ='" + cbx_osserty_user7.Text + "'";


                try
                {
                    DataTable dt2 = db.RunReader("select * from pos_salestotal_v    where ''=='' " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        totalsales rpon = new totalsales();
                        rpon.Database.Tables["pos_salestotal_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (cbx_osserty_user3.SelectedIndex == 6)
            {
                string wherequn = "";
                if (txt_parcedate_emp3.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( DATEINVOICED)  >=coalesce( DATE(  '" + Convert.ToDateTime(txt_parcedate_emp3.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'),DATEINVOICED)";
                if (txt_parcedate_emp4.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and DATE( DATEINVOICED)  <=  DATE('" + Convert.ToDateTime(txt_parcedate_emp4.Text).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                if (cbx_osserty_user8.Text.Trim() != string.Empty)
                    wherequn = wherequn + " and  INVOICE_ID  ='" + cbx_osserty_user8.Text + "'";
                //if (cbx_osserty_user6.Text.Trim() != string.Empty)
                //    wherequn = wherequn + " and  PRODUCT_CATEGORY_ID  ='" + cbx_osserty_user6.Text + "'";
                //if (cbx_osserty_user7.Text.Trim() != string.Empty)
                //    wherequn = wherequn + " and  PRODUCT_ID  ='" + cbx_osserty_user7.Text + "'";

                try
                {
                    DataTable dt2 = db.RunReader("select * from pos_salestotal_v    where ''=='' " + wherequn);
                    if (dt2.Rows.Count > 0)
                    {
                        View_Report showreport = new View_Report();
                        totalsales rpon = new totalsales();
                        rpon.Database.Tables["pos_salestotal_v"].SetDataSource(dt2);
                        showreport.crystalReportViewer1.ReportSource = rpon;
                        //  rpon.PrintToPrinter(1, true, 1, 1);

                        showreport.ShowDialog();

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("لا يوجد بيانات");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void cbx_osserty_user4_DropDownOpened(object sender, EventArgs e)
        {
            string dd = (seraaredio_Copy1.IsChecked == true) ? "C" : "V";
            cbx_osserty_user4.ItemsSource = db.RunReader(" select CLIENT_ID ,NAME from CLIENT where CLIENT_type='"+dd+"' ").DefaultView;
        }
   
        private void cbx_osserty_user5_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user5.ItemsSource = db.RunReader(" select USER_NAME  from USER").DefaultView;
        }

        private void cbx_osserty_user6_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user6.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID,NAME from PRODUCT_CATEGORY  ").DefaultView;
        }

        private void cbx_osserty_user7_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user7.ItemsSource = db.RunReader("select PRODUCT_ID ,NAME from PRODUCT  where  PRODUCT_CATEGORY_ID='" + cbx_osserty_user6.Text + "'   ").DefaultView;
        }

        private void cbx_osserty_user8_DropDownOpened(object sender, EventArgs e)
        {
            if (cbx_osserty_user3.SelectedIndex == 4)
                cbx_osserty_user8.ItemsSource = db.RunReader(" select INVOICE_ID from pos_purchasetotal_v  ").DefaultView;
            else if (cbx_osserty_user3.SelectedIndex == 5)
                cbx_osserty_user8.ItemsSource = db.RunReader(" select INVOICE_ID from pos_salestotal_v  ").DefaultView;
            else
            {
                cbx_osserty_user8.ItemsSource = null;
            }

            
       }

        private void cbx_osserty_user9_DropDownOpened(object sender, EventArgs e)
        {
            cbx_osserty_user9.ItemsSource = db.RunReader("select CLIENT_ID,NAME from CLIENT  ").DefaultView;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_user.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_user.SelectedItem;

                    DataTable dt2 = db.RunReader("select * from pos_MEDICAL_V  where INVOICE_ID='" + row[0].ToString() + "' and Package_size!=0 and  client_type=case when lower(INVOICE_TYPE)='purchasing' then 'V'  else 'C' end ");
                    View_Report showreport = new View_Report();
                    invoicerep rpon = new invoicerep();
                    rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    //  rpon.PrintToPrinter(1, true, 1, 1);

                    showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
