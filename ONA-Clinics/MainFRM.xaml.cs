using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data;
using System.IO;

namespace ONA_Stores
{


    public class produ
    {

        public string number { get; set; }
        public string price { get; set; }



    }
    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class MainFRM : Window
    {

        DataTable test = new DataTable();
        public MainFRM()
        {
            InitializeComponent();
            clearGrids();


            fillgroub();

            fillgroub1();

            fillgroub_payee();
            checkouthotry();
        }


        private void checkouthotry()
        {

            if (!(User.main_form == "Y" || User.main_form == "y"))
                btn_main_form.Visibility = Visibility.Collapsed;
            //if(!( User.casher_form == "Y" || User.casher_form == "y" ))
            //    btn_casher_form.Visibility = Visibility.Collapsed;
            //if(!( User.kitchen_form == "Y" || User.kitchen_form == "y" ))
            //    btn_kitchen_form.Visibility = Visibility.Collapsed;
            if (!(User.report_form == "Y" || User.report_form == "y"))
                btn_report_form.Visibility = Visibility.Collapsed;
            //if(!( User.view_form == "Y" || User.view_form == "y" ))
            //    btn_view_form.Visibility = Visibility.Collapsed;


        }
        private void btn_main_form_Click(object sender, RoutedEventArgs e)
        {
            new BasicFRM().Show();
            this.Close();
        }

        private void btn_casher_form_Click(object sender, RoutedEventArgs e)
        {
            new MainFRM().Show();
            this.Close();
        }

        private void btn_kitchen_form_Click(object sender, RoutedEventArgs e)
        {
            new Orders_DONE().Show();
            this.Close();
        }

        private void btn_view_form_Click(object sender, RoutedEventArgs e)
        {
            new Orders().Show();
            this.Close();
        }

        private void btn_report_form_Click(object sender, RoutedEventArgs e)
        {
            new reportFRM().Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRestore.Visibility = Visibility.Visible;

        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Collapsed;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }


        DB db = new DB();


        //        CREATE TABLE PRODUCT (PRODUCT_ID INT,
        //  NAME                 TEXT,
        //  CODE                 TEXT,
        //  PRODUCT_CATEGORY_ID  INT,
        //  QTY                  REAL,
        //  SALES_UNIT_PRICE     REal,
        //  PURCHASE_UNIT_PRICE  REAL ,
        //  CREATEDBY            TEXT ,
        //  CREATEDDATE          TEXT,
        //PRIMARY KEY("PRODUCT_ID")
        //)
        void fillitems1(string groub_code)
        {
            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME      from  PRODUCT where PRODUCT_CATEGORY_ID ='" + groub_code + "'");
            subgroup1.Children.Clear();


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;




                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\items" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));

                temp.btn.Click += (s, e) =>
                {

                    fillorders1(temp.Code.Content.ToString(), temp.pic.Source);
                };
                subgroup1.Children.Add(temp);
            }




        }

        void fillitems(string groub_code)
        {
            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME      from  PRODUCT where PRODUCT_CATEGORY_ID ='" + groub_code + "'");
            subgroup.Children.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;




                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\items" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));

                temp.btn.Click += (s, e) =>
                {

                    fillorders(temp.Code.Content.ToString(), temp.pic.Source);
                };

                subgroup.Children.Add(temp);
            }




        }
        void totalprices1()
        {
            double totalsss = 0;
            foreach (ListBoxItem item in mainstackss1.Children)
            {

                order temp = item.Content as order;

                totalsss += Convert.ToDouble(temp.totalprice.Text.ToString());

            }
            txtAName3.Text = totalsss.ToString();
        }
        void totalprices()
        {
            double totalsss = 0;
            foreach (ListBoxItem item in mainstackss.Children)
            {

                order temp = item.Content as order;

                totalsss += Convert.ToDouble(temp.totalprice.Text.ToString());

            }
            txtAName6.Text = totalsss.ToString();
        }
        private void fillorders1(string p, ImageSource picsource)
        {

            foreach (ListBoxItem item in mainstackss1.Children)
            {

                order temp = item.Content as order;
                if (temp.code.Content.ToString() == p)
                {
                    temp.txtQTY.Text = (Convert.ToInt32(temp.txtQTY.Text) + 1).ToString();
                    return;
                }
            }

            DataTable dt = db.RunReader("select   PRODUCT_ID, NAME,SALES_UNIT_PRICE ,Package_Unit_Price,ProductDiscountPCT, Package_size,QTY,Minqty     from  PRODUCT  where PRODUCT_ID ='" + p + "'");



            for (int i = 0; i < dt.Rows.Count; i++)
            {

                order aa = new order();
                aa.comboBox.SelectedIndex = 1;
                aa.comboBox.IsEnabled = false;
                aa.Name.Content = dt.Rows[i][1].ToString();
                aa.code.Content = dt.Rows[i][0].ToString();
                aa.price.Content = dt.Rows[i][2].ToString();
                aa.pic.Source = picsource;



                aa.txtQTY.Text = "2";

                aa.txtQTY.Text = "1";

                aa.totalQTYstoreDB.Text = dt.Rows[i][6].ToString();
                aa.alarmstoreDB.Text = dt.Rows[i][7].ToString();
                if (!string.IsNullOrEmpty(aa.totalQTYstoreDB.Text))
                    aa.totalQTYstore.Text = (Convert.ToInt32(aa.totalQTYstoreDB.Text) - 1).ToString();
                else
                {
                    aa.totalQTYstore.Text = "0";
                    aa.totalQTYstoreDB.Text = "0";
                }
                if ((Convert.ToInt32(aa.totalQTYstoreDB.Text) - Convert.ToInt32(aa.txtQTY.Text)) < 0)
                {

                    MessageBox.Show("تم نفاذ الكمية");
                    return;

                }
                if ((Convert.ToInt32(aa.totalQTYstoreDB.Text) - Convert.ToInt32(aa.txtQTY.Text)) <= Convert.ToInt32(aa.alarmstoreDB.Text))
                {

                    MessageBox.Show("هذه الكمية على وشك النفاذ");
                    //  txtQTY.Text = ( Convert.ToInt32(txtQTY.Text) + 1 ).ToString();

                }

                aa.totalprice.TextChanged += (s, e) =>
                {

                    totalprices1();
                };

                ListBoxItem cc = new ListBoxItem();
                cc.Margin = new Thickness(2);
                // cc.Background = Brushes.Yellow;
                cc.Content = aa;
                mainstackss1.Children.Add(cc);
                totalprices1();
            }

        }

        private void fillorders(string p, ImageSource picsource)
        {

            foreach (ListBoxItem item in mainstackss.Children)
            {

                order temp = item.Content as order;
                if (temp.code.Content.ToString() == p)
                {
                    temp.txtQTY.Text = (Convert.ToInt32(temp.txtQTY.Text) + 1).ToString();
                    return;
                }
            }
            //    PRODUCT_ID, NAME,PURCHASE_UNIT_PRICE ,Package_size

            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME,SALES_UNIT_PRICE ,Package_Unit_Price,ProductDiscountPCT, Package_size,QTY,Minqty    from  PRODUCT   where PRODUCT_ID ='" + p + "'");



            for (int i = 0; i < dt.Rows.Count; i++)
            {

                order aa = new order();
                aa.Name.Content = dt.Rows[i][1].ToString();
                aa.code.Content = dt.Rows[i][0].ToString();
                aa.price.Content = aa.price_unit.Content = dt.Rows[i][2].ToString();
                aa.price_package.Content = dt.Rows[i][3].ToString();
                aa.disc.Text = dt.Rows[i][4].ToString();
                aa.pic.Source = picsource;

                aa.txtQTY.Text = "2";



                aa.txtQTY.Text = "1";

                aa.package_size.Content = dt.Rows[i][5].ToString();
                aa.totalQTYstoreDB.Text = dt.Rows[i][6].ToString();
                aa.alarmstoreDB.Text = dt.Rows[i][7].ToString();
                if (!string.IsNullOrEmpty(aa.totalQTYstoreDB.Text))
                    aa.totalQTYstore.Text = (Convert.ToInt32(aa.totalQTYstoreDB.Text) - 1).ToString();
                else
                {
                    aa.totalQTYstore.Text = "0";
                    aa.totalQTYstoreDB.Text = "0";
                }



                if ((Convert.ToInt32(aa.totalQTYstoreDB.Text) - Convert.ToInt32(aa.txtQTY.Text)) < 0)
                {

                    MessageBox.Show("تم نفاذ الكمية");
                    return;

                }

                if ((Convert.ToInt32(aa.totalQTYstoreDB.Text) - Convert.ToInt32(aa.txtQTY.Text)) <= Convert.ToInt32(aa.alarmstoreDB.Text))
                {

                    MessageBox.Show("هذه الكمية على وشك النفاذ");
                    //  txtQTY.Text = ( Convert.ToInt32(txtQTY.Text) + 1 ).ToString();

                }
                aa.totalprice.TextChanged += (s, e) =>
                {

                    totalprices();
                };

                ListBoxItem cc = new ListBoxItem();
                cc.Margin = new Thickness(2);
                // cc.Background = Brushes.Yellow;
                cc.Content = aa;

                mainstackss.Children.Add(cc);
                totalprices();
            }

        }
        void fillgroub_payee(string type = "%%", string search = "")
        {
            maingroup2.Children.Clear();
            DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID, NAME      from  PRODUCT_CATEGORY WHERE  NAME like '%" + search.Trim() + "%' ");
            txtAName24.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='V' ").Rows[0][0].ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;

                temp.btn.Click += (s, e) =>
                {

                    fillitems_Payee(temp.Code.Content.ToString());
                };


                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\gropitems" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));


                maingroup2.Children.Add(temp);
            }

        }

        void fillitems_Payee(string groub_code)
        {
            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME      from  PRODUCT where PRODUCT_CATEGORY_ID ='" + groub_code + "'");
            subgroup2.Children.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;




                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\items" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));

                temp.btn.Click += (s, e) =>
                {

                    fillorders_Payee(temp.Code.Content.ToString(), temp.pic.Source);
                };
                subgroup2.Children.Add(temp);
            }




        }

        private void fillorders_Payee(string p, ImageSource picsource)
        {

            foreach (ListBoxItem item in mainstackss2.Children)
            {

                orderback temp = item.Content as orderback;
                if (temp.code.Content.ToString() == p)
                {
                    temp.txtQTY.Text = (Convert.ToInt32(temp.txtQTY.Text) + 1).ToString();
                    return;
                }
            }

            DataTable dt = db.RunReader("select    PRODUCT_ID, NAME,PURCHASE_UNIT_PRICE ,Package_size     from  PRODUCT  where PRODUCT_ID ='" + p + "'");



            for (int i = 0; i < dt.Rows.Count; i++)
            {

                orderback aa = new orderback();
                aa.Name.Content = dt.Rows[i][1].ToString();
                aa.code.Content = dt.Rows[i][0].ToString();
                aa.package_size.Content = dt.Rows[i][3].ToString();
                if (string.IsNullOrWhiteSpace(dt.Rows[i][2].ToString()))
                    aa.price.Text = "0";
                else
                    aa.price.Text = dt.Rows[i][2].ToString();
                aa.pic.Source = picsource;
                aa.txtQTY.Text = "2";
                aa.txtQTY.Text = "1";
                aa.totalprice.TextChanged += (s, e) =>
                {

                    totalprices_Payee();
                };

                ListBoxItem cc = new ListBoxItem();
                cc.Margin = new Thickness(2);
                // cc.Background = Brushes.Yellow;
                cc.Content = aa;
                mainstackss2.Children.Add(cc);
                totalprices_Payee();
            }

        }
        void totalprices_Payee()
        {
            double totalsss = 0;
            foreach (ListBoxItem item in mainstackss2.Children)
            {

                orderback temp = item.Content as orderback;

                totalsss += Convert.ToDouble(temp.totalprice.Text.ToString());

            }
            txtAName13.Text = totalsss.ToString();
        }
        void fillgroub1(string type = "%%", string search = "")
        {
            maingroup1.Children.Clear();
            DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID, NAME      from  PRODUCT_CATEGORY WHERE  NAME like '%" + search.Trim() + "%' ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;

                temp.btn.Click += (s, e) =>
                {

                    fillitems1(temp.Code.Content.ToString());
                };


                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\gropitems" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));


                maingroup1.Children.Add(temp);
            }

        }
        void fillgroub(string type = "%%", string search = "")
        {
            maingroup.Children.Clear();
            DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID, NAME      from  PRODUCT_CATEGORY WHERE  NAME like '%" + search.Trim() + "%' ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Group temp = new Group();

                temp.Margin = new Thickness(2);
                //temp.Background = Brushes.Purple;

                temp.btn.Click += (s, e) =>
                {

                    fillitems(temp.Code.Content.ToString());
                };


                temp.Code.Content = dt.Rows[i][0].ToString();
                temp.Name.Content = dt.Rows[i][1].ToString();
                string filePath = Environment.CurrentDirectory + "\\gropitems" + "\\" + dt.Rows[i][0].ToString() + ".jpg";
                if (File.Exists(filePath))
                    temp.pic.Source = new BitmapImage(new Uri(filePath));


                maingroup.Children.Add(temp);
            }

        }
        void clearGrids()
        {
            g_cash.Visibility = Visibility.Collapsed;
            g_cash1.Visibility = Visibility.Collapsed;

            g_delevry.Visibility = Visibility.Collapsed;
            g_delevry1.Visibility = Visibility.Collapsed;


            g_payee.Visibility = Visibility.Collapsed;
            g_payee1.Visibility = Visibility.Collapsed;

            g_msrofat.Visibility = Visibility.Collapsed;
            g_ageeel.Visibility = Visibility.Collapsed;
            g_mortag3.Visibility = Visibility.Collapsed;
            g_es_check.Visibility = Visibility.Collapsed;
            g_coustmar.Visibility = Visibility.Collapsed;
            g_shra.Visibility = Visibility.Collapsed;
            g_shra_Copy.Visibility = Visibility.Collapsed;
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
                case 1:
                    g_shra.Visibility = Visibility.Visible;
                    cbx_typ_items.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,case when  TYPE=1 then 'حريمي' else 'رجالي'  END TYPE from  PRODUCT_CATEGORY ").DefaultView;
                    //g_cash.Visibility = Visibility.Visible;
                    //g_cash1.Visibility = Visibility.Visible;
                    //cbx_typ_items1.SelectedIndex = 0;
                    //Cbx_typ_items1_DropDownClosed(sender, e);
                    break;
                case 2:
                    //   g_Users.Visibility = Visibility.Visible;

                    //g_delevry.Visibility = Visibility.Visible;
                    //g_delevry1.Visibility = Visibility.Visible;

                    //txtAName9.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='C' ").Rows[0][0].ToString();
                    //cbx_typ_items2.SelectedIndex = 0;
                    //Cbx_typ_items2_DropDownClosed(sender, e);
                    g_shra_Copy.Visibility = Visibility.Visible;
                    break;
                case 0:
                    g_coustmar.Visibility = Visibility.Visible;
                    //g_payee.Visibility = Visibility.Visible;
                    //g_payee1.Visibility = Visibility.Visible;
                    ////  g_Medicine.Visibility = Visibility.Visible;
                    //cbx_typ_items3.SelectedIndex = 0;
                    ////cbx_typ_items3(sender, e);
                    break;
                case 3:
                    txt_price_items2.Text = "";
                    textBox.Text = "";
                    g_msrofat.Visibility = Visibility.Visible;
                    break;

                case 4:
                    txtAName31.Text = "";
                    txtAName33.Text = "";
                    txtAName32.Text = "";
                    txtAName34.Text = "";
                    txtAName35.Text = "";
                    txtAName36.Text = "0";
                    //txtAName37.Text = "0";
                    //txtAName38.Text = "0";
                    g_ageeel.Visibility = Visibility.Visible;
                    break;
                case 5:
                    g_mortag3.Visibility = Visibility.Visible;
                    fillInvoice();
                    break;
                case 6:
                    g_es_check.Visibility = Visibility.Visible;
                    Button_Click_16(sender, e);
                    fillInvoice();
                    break;
                default:
                    break;
            }
        }

        private void fillInvoice(string invoiceID = "")
        {

            if (invoiceID != "")
                dg_items1.ItemsSource = db.RunReader(@"SELECT   i.INVOICE_ID as 'رقم الفاتورة',i.DATEINVOICED as 'تاريخ الفاتورة',i.TIMEINVOICED as 'وقت الفاتورة',i.TOTAL as 'اجمالى الفاتورة',i.PAID as 'المدفوع',i.CreatedBy as 'اسم الكاشير',i.client_id as 'رقم العميل',c.NAME as 'اسم العميل' FROM INVOICE i join CLIENT c on i.client_id=c.client_id and client_type='C'   WHERE i.INVOICE_TYPE!='Purchasing' and (i.INVOICE_ID LIKE '" + invoiceID + "%'  or c.NAME LIKE '" + invoiceID + "%') ORDER BY i.createddate  DESC LIMIT 50;").DefaultView;
            else
                dg_items1.ItemsSource = db.RunReader(@"SELECT   i.INVOICE_ID as 'رقم الفاتورة',i.DATEINVOICED as 'تاريخ الفاتورة',i.TIMEINVOICED as 'وقت الفاتورة',i.TOTAL as 'اجمالى الفاتورة',i.PAID as 'المدفوع',i.CreatedBy as 'اسم الكاشير',i.client_id as 'رقم العميل',c.NAME as 'اسم العميل' FROM INVOICE i join CLIENT c on i.client_id=c.client_id and client_type='C'  WHERE i.INVOICE_TYPE!='Purchasing'  ORDER BY i.createddate  DESC LIMIT 50;").DefaultView;

            dg_items1_Copy.ItemsSource = null;
            txt_search_items3.Text = "";


        }


        //private void btnPlus_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int oldqty = 0;
        //        if (int.TryParse(txtQTY.Text, out oldqty))
        //        {
        //            txtQTY.Text = (oldqty + 1).ToString();
        //            lblTotalItemPrice.Content = (Convert.ToDouble(lblItemPrice.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //        else
        //        {
        //            txtQTY.Text = "1";
        //            lblTotalItemPrice.Content = lblItemPrice.Content;
        //            lblTotalItemPrice.Content = (Convert.ToDouble(lblItemPrice.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //    }
        //    catch { }

        //}

        //private void btnMinus_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int oldqty = 0;
        //        if (int.TryParse(txtQTY.Text, out oldqty))
        //        {
        //            if (oldqty - 1 < 0)
        //                oldqty = 1;

        //            txtQTY.Text = (oldqty - 1).ToString();
        //            lblTotalItemPrice.Content = (Convert.ToDouble(lblItemPrice.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //        else
        //        {
        //            txtQTY.Text = "1";
        //            lblTotalItemPrice.Content = lblItemPrice.Content;
        //        }







        //    }
        //    catch { }
        //}


        //private void txtQTY_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (lblTotalItemPrice == null)
        //            return;
        //        double price = 0;
        //        if (double.TryParse(lblItemPrice.Content.ToString(), out price))
        //        {


        //            lblTotalItemPrice.Content = (price * Convert.ToUInt32(txtQTY.Text)).ToString();



        //        }
        //        else
        //        {

        //            lblTotalItemPrice.Content = price.ToString();
        //        }
        //    }
        //    catch { }
        //}
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

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{


        //    order aa = new order("كشرى");
        //    ListBoxItem cc = new ListBoxItem();
        //    cc.Margin = new Thickness(2);
        //    cc.Background = Brushes.Yellow;
        //    cc.Content = aa;
        // aa.lblTotalItemPrice.Content.ToString();
        //    mainstackss.Children.Add(cc);

        //    foreach (ListBoxItem item in mainstackss.Children)
        //    {

        //        order temp = (order)item.Content;
        //        temp.lblTotalItemPrice.Content.ToString();
        //    }



        //}


        //private void btnMinus_Cliaack(object sender, RoutedEventArgs e,Label test)
        //{
        //    try
        //    {
        //        int oldqty = 0;
        //        if (int.TryParse(test.Content.ToString(), out oldqty))
        //        {
        //            if (oldqty - 1 < 0)
        //                oldqty = 1;

        //            test.Content = (oldqty - 1).ToString();
        //            test.Content = (Convert.ToDouble(test.Content.ToString()) * Convert.ToUInt32(txtQTY.Text)).ToString();

        //        }
        //        else
        //        {
        //            test.Content = "1";
        //            lblTotalItemPrice.Content = lblItemPrice.Content;
        //        }







        //    }
        //    catch { }
        //}

        //private void Button_Click_22(object sender, RoutedEventArgs e)
        //{

        //    foreach (ListBoxItem item in mainstackss.Children)
        //    {

        //        order temp = (order)item.Content;
        //     MessageBox.Show(   temp.lblTotalItemPrice.Content.ToString());
        //    }

        //}

        private void maingroup_TouchUp(object sender, TouchEventArgs e)
        {
            foreach (ListBoxItem item in maingroup.Children)
            {

                if (item.IsSelected == true)
                    MessageBox.Show("ss");
            }
        }

        private void maingroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void maingroup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainstackss2.Children.Clear();
            subgroup2.Children.Clear();
            txtAName13.Text = "0";
            txtAName18.Text = "0";
            txtAName19.Text = "0";
            txtAName17.Text = "0";
            txtAName16.Text = "0";
        }

        private void txtAName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtAName1.Text.Trim() == "")
                {
                    txtAName1.Text = "0";
                }
                if (Convert.ToDouble(txtAName1.Text) > Convert.ToDouble(txtAName15.Text))
                    txtAName1.Text = txtAName15.Text;
                txtAName2.Text = (Convert.ToDouble(txtAName1.Text) - Convert.ToDouble(txtAName15.Text)).ToString();

            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            try
            {

                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName24.Text + "' and client_type='V'");
                if (dt.Rows.Count <= 0)
                {

                    MessageBox.Show("برجاء اختيار العميل");
                    return;
                }



                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,charge,discountpct,totalafterdisount,CLIENT_ID)
values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName13.Text + "','" + txtAName16.Text + "','Purchasing','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Complated','" + txtAName17.Text + "','" + txtAName18.Text + "','" + txtAName19.Text + "','" + txtAName24.Text.Trim() + "')");


                if (Convert.ToDouble(txtAName17.Text) < 0)
                    db.RunNonQuery(@"INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) 
                    values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + (-Convert.ToDouble(txtAName17.Text)).ToString() + "','" + txtAName24.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','V')");

                foreach (ListBoxItem item in mainstackss2.Children)
                {

                    orderback temp = item.Content as orderback;
                    string items_qty = (Convert.ToDouble(temp.txtQTY.Text) * Convert.ToDouble(temp.package_size.Content)).ToString();
                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + items_qty + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','package')");


                    db.RunNonQuery(@"update PRODUCT set QTY=coalesce(QTY, 0)+'" + items_qty.Trim() + "' , PURCHASE_UNIT_PRICE='" + temp.price.Text.Trim() + "' where PRODUCT_ID='" + temp.code.Content.ToString() + "'");

                    DataTable tempdt = db.RunReader("select count(*) from PriceList where PriceList_ID='" + temp.code.Content.ToString() + "' AND Price='" + temp.price.Text.Trim() + "'");
                    if (tempdt.Rows[0][0].ToString() == "0")
                    {
                        db.RunNonQuery("INSERT INTO PriceList(ID,PriceList_ID,Price,QTY,CREATEDBY,CREAREDDATE) VALUES ((select coalesce(max(ID), '0')+1 from PriceList ),'" + temp.code.Content.ToString() + "','" + temp.price.Text.Trim() + "','" + items_qty.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    }
                    else
                    {
                        db.RunReader("UPDATE PriceList SET QTY=coalesce(QTY, 0)+'" + items_qty.Trim() + "' WHERE PriceList_ID='" + temp.code.Content.ToString() + "' AND Price='" + temp.price.Text.Trim() + "'");
                    }


                }



                //try {
                //    DataTable dt2 = db.RunReader("select * from product_v");
                //    // where INVOICE_ID='" + INVOICE_ID + "'");
                //    View_Report showreport = new View_Report();
                //    productqty rpon = new productqty();
                //    rpon.Database.Tables["product_v"].SetDataSource(dt2);
                //    showreport.crystalReportViewer1.ReportSource = rpon;
                //    //  rpon.PrintToPrinter(1, true, 1, 1);

                //    showreport.ShowDialog();
                //    MessageBox.Show("تم عملية ");
                //} catch(Exception ex) { MessageBox.Show(ex.Message); }


                try
                {
                    DataTable dt2 = db.RunReader("select * from pos_MEDICAL_V  where INVOICE_ID='" + INVOICE_ID + "' and Package_size!=0 and client_type='V'  ");
                    View_Report showreport = new View_Report();
                    invoicerep rpon = new invoicerep();
                    rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    //  rpon.PrintToPrinter(1, true, 1, 1);

                    showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


                mainstackss2.Children.Clear();
                subgroup2.Children.Clear();
                txtAName13.Text = "0";
                txtAName16.Text = "0";
                txtAName17.Text = "0";
                txtAName18.Text = "0";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //public static void SetPrinterPort(string printerName, string portName)
        //{
        //    var oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
        //    oManagementScope.Connect();

        //    SelectQuery oSelectQuery = new SelectQuery();
        //    oSelectQuery.Q-ueryString = @"SELECT * FROM Win32_Printer 
        //    WHERE Name = '" + printerName.Replace("\\", "\\\\") + "'";

        //    ManagementObjectSearcher oObjectSearcher =
        //       new ManagementObjectSearcher(oManagementScope, @oSelectQuery);
        //    ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

        //    foreach (ManagementObject oItem in oObjectCollection)
        //    {
        //        oItem.Properties["PortName"].Value = portName;
        //        oItem.Put();
        //    }
        //}

        List<produ> getitems(int id_items, int count_items)
        {
            List<produ> tempList = new List<produ>();
            int insert = 0;
            while (count_items > 0)
            {
                produ temo = new produ();
                DataTable count_db = db.RunReader(@"select min (CREAREDDATE ), QTY,Price,coalesce( ID,-1 ) from PriceList where PriceList_ID='" + id_items + "' and QTY >0   ");
                if (count_db.Rows[0][3].ToString() == "-1")
                {
                    count_db = db.RunReader(@"select max (CREAREDDATE ), QTY,Price,ID from PriceList where PriceList_ID='" + id_items + "'    ");
                    db.RunNonQuery("update  PriceList set QTY=QTY-'" + count_items + "' where ID='" + count_db.Rows[0][3].ToString() + "'");
                    temo.price = count_db.Rows[0][2].ToString();
                    temo.number = count_db.Rows[0][1].ToString();
                    return tempList;
                }
                if (count_db.Rows.Count <= 0)
                    return tempList;

                if (count_items >= Convert.ToInt32(count_db.Rows[0][1].ToString()))
                {
                    insert = 0;
                    temo.number = count_db.Rows[0][1].ToString();
                }
                else
                {
                    insert = Convert.ToInt32(count_db.Rows[0][1].ToString()) - count_items;
                    temo.number = count_items.ToString();

                }
                temo.price = count_db.Rows[0][2].ToString();


                tempList.Add(temo);
                db.RunNonQuery("update  PriceList set QTY='" + insert + "' where ID='" + count_db.Rows[0][3].ToString() + "'");

                if (Convert.ToInt32(count_db.Rows[0][1].ToString()) <= 0)
                {
                    count_items = count_items - Convert.ToInt32(count_db.Rows[0][1].ToString());
                    if (count_items < 0)
                    {
                        db.RunNonQuery("update  PriceList set QTY='" + count_items + "' where ID='" + count_db.Rows[0][3].ToString() + "'");
                    }
                    return tempList;
                }
                else if (count_items <= Convert.ToInt32(count_db.Rows[0][1].ToString()))
                {
                    count_items = 0;
                }
                else if (count_items > Convert.ToInt32(count_db.Rows[0][1].ToString()))
                {
                    count_items = count_items - Convert.ToInt32(count_db.Rows[0][1].ToString());
                }






            }
            return tempList;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataTable dt = db.RunReader("SELECT sum(total) from INVOICE where shiftdelivery='T' ");

            if (MessageBox.Show("اجمالى المبلغ فى الدرج" + Environment.NewLine + dt.Rows[0][0].ToString() + Environment.NewLine + Environment.NewLine + "هل تريد تصفير الدرج ؟", "اجمالى المبلغ فى الدرج", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.RunNonQuery("UPDATE INVOICE set shiftdelivery='N'", "تم تصفير الدرج بنجاح");
            }

            //try
            //{
            //    DataTable dt = db.RunReader("select * from POS_RECIEPT_V ");
            //    View_Report showreport = new View_Report();
            //    crystalrep rpon = new crystalrep();
            //    rpon.Database.Tables["POS_RECIEPT_V"].SetDataSource(dt);
            //    showreport.crystalReportViewer1.ReportSource = rpon;
            //    showreport.ShowDialog();
            //}
            //catch { }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            mainstackss1.Children.Clear();
            subgroup1.Children.Clear();
            txtAName3.Text = "0";
            txtAName11.Text = "10";
            txtAName10.Text = "0";


        }

        void ttt()
        {



            string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
            db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,charge,discountpct,totalafterdisount,TaxAmt)
values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName6.Text + "','" + txtAName1.Text + "','Sales','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Complated','" + txtAName2.Text + "','" + txtAName14.Text + "','" + txtAName15.Text + "','" + txtAName25.Text + "')");





            foreach (ListBoxItem item in mainstackss.Children)
            {
                string TotalQTY = "0";
                order temp = item.Content as order;
                if (temp.comboBox.Text.Trim() == "جملة")
                    TotalQTY = (Convert.ToDouble(temp.txtQTY.Text) * Convert.ToDouble(temp.package_size.Content.ToString())).ToString();
                else
                    TotalQTY = temp.txtQTY.Text;


                List<produ> ts = getitems(Convert.ToInt32(temp.code.Content.ToString()), Convert.ToInt32(TotalQTY));

                if (ts.Count <= 0)
                {
                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','0','" + temp.disc.Text + "')");

                }
                else
                {
                    Double total_price_Shera = 0, Total_Count = 0, AVG_price_shera = 0;
                    foreach (produ it in ts)
                    {
                        total_price_Shera += Convert.ToDouble(it.price);
                        Total_Count += Convert.ToDouble(it.number);

                    }

                    AVG_price_shera = total_price_Shera / Total_Count;

                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + Total_Count + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','" + AVG_price_shera + "','" + temp.disc.Text + "')");

                }

            }


            try
            {
                DataTable dt2 = db.RunReader("select * from pos_MEDICAL_V  where INVOICE_ID='" + INVOICE_ID + "' and Package_size!=0 ");
                View_Report showreport = new View_Report();
                invoicerep rpon = new invoicerep();
                rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
                showreport.crystalReportViewer1.ReportSource = rpon;
                //  rpon.PrintToPrinter(1, true, 1, 1);

                showreport.ShowDialog();
                MessageBox.Show("تم عملية ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


            mainstackss.Children.Clear();
            subgroup.Children.Clear();
            txtAName1.Text = "0";
            txtAName6.Text = "0";
            txtAName2.Text = "0";
            //  txtAName25.Text = "0";


        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where (MOBILE='" + txtAName5.Text + "' or PHONE='" + txtAName5.Text + "') and CLIENT_ID='" + txtAName9.Text + "' and client_type='C'");
            if (dt.Rows.Count <= 0)
            {

                MessageBox.Show("برجاء اختيار العميل");
                return;
            }

            try
            {

                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,CLIENT_ID,discountpct,totalafterdisount,TaxAmt,charge)
                values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName10.Text + "','0','Allocation','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','inprogress','" + txtAName9.Text + "','" + txtAName12.Text + "','" + txtAName3.Text + "','" + txtAName11.Text + "','" + txtAName29.Text + "')");

                db.RunNonQuery("INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',-1*'" + txtAName29.Text + "','" + txtAName9.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')");


                foreach (ListBoxItem item in mainstackss1.Children)
                {
                    string TotalQTY = "0";
                    order temp = item.Content as order;
                    if (temp.comboBox.Text.Trim() == "جملة")
                        TotalQTY = temp.txtQTY.Text;
                    else
                        TotalQTY = temp.txtQTY.Text;


                    List<produ> ts = getitems(Convert.ToInt32(temp.code.Content.ToString()), Convert.ToInt32(TotalQTY));


                    db.RunNonQuery(@"update PRODUCT set QTY=QTY-'" + TotalQTY + "' where PRODUCT_ID='" + temp.code.Content.ToString() + "'");


                    if (ts.Count <= 0)
                    {
                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','0','" + temp.disc.Text + "','package')");

                    }
                    else
                    {
                        Double total_price_Shera = 0, Total_Count = 0, AVG_price_shera = 0;
                        foreach (produ it in ts)
                        {
                            total_price_Shera += Convert.ToDouble(it.price);
                            Total_Count += Convert.ToDouble(it.number);

                        }

                        AVG_price_shera = total_price_Shera / Total_Count;

                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + Total_Count + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','" + AVG_price_shera + "','" + temp.disc.Text + "','package')");

                    }

                }


                try
                {
                    DataTable dt2 = db.RunReader("select * from pos_MEDICAL_V  where INVOICE_ID='" + INVOICE_ID + "' and Package_size!=0 and  client_type='C'  ");
                    View_Report showreport = new View_Report();
                    invoicerep rpon = new invoicerep();
                    rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    //  rpon.PrintToPrinter(1, true, 1, 1);

                    showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


                //   MessageBox.Show("تم عملية ");
                mainstackss1.Children.Clear();
                subgroup1.Children.Clear();
                txtAName3.Text = "0";
                txtAName10.Text = "0";
                txtAName11.Text = "0";
                txtAName30.Text = "0";
                txtAName29.Text = "0";

                txtAName28.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' and  client_type='C'").Rows[0][0].ToString();
                txtAName27.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt>0 and client_type='C'").Rows[0][0].ToString();
                txtAName26.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt<0 and client_type='C'").Rows[0][0].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void savenewpaitient_Click(object sender, RoutedEventArgs e)
        {





            try
            {


                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName9.Text + "' AND client_type='C'");

                if (dt.Rows.Count > 0)
                {

                    db.RunNonQuery("update CLIENT set NAME='" + txtAName4.Text.Trim() + "', MOBILE='" + txtAName5.Text + "' , PHONE='" + txtAName8.Text + "',client_type='C' ,ADDRESS='" + txtAName7.Text + "' where CLIENT_ID='" + txtAName9.Text + "' and client_type='C'", "تم تحديث البيانات");

                    return;
                }
                string CLIENT_ID = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT  where client_type='C'").Rows[0][0].ToString();
                if (txtAName5.Text.Trim() == "" && txtAName8.Text.Trim() == "")
                {

                    MessageBox.Show(" برجاء ادخال رقم الهاتف");
                    return;
                }

                if (txtAName7.Text.Trim() == "")
                {

                    MessageBox.Show(" برجاء ادخال العنوان");
                    return;
                }

                //client_type='C'

                db.RunNonQuery("insert into CLIENT (CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE,client_type) values ('" + CLIENT_ID + "','" + txtAName4.Text.Trim() + "','" + txtAName5.Text.Trim() + "','" + txtAName8.Text.Trim() + "','" + txtAName7.Text.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')", "تم حفظ البيانات");

                txtAName9.Text = CLIENT_ID;
            }
            catch { }

        }



        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            txtAName5.Text = "";


            txtAName9.Text = "";
            txtAName4.Text = "";
            txtAName7.Text = "";
            txtAName8.Text = "";
            txtAName26.Text = "0";
            txtAName27.Text = "0";
            txtAName28.Text = "0";
            txtAName9.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='C' ").Rows[0][0].ToString();
        }



        private void txtAName11_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (txtAName11.Text.Trim() == "")
                    txtAName11.Text = "0";

                if (txtAName12.Text.Trim() == "")
                    txtAName12.Text = "0";
                txtAName10.Text = ((Convert.ToDouble(txtAName3.Text) * ((100 - Convert.ToDouble(txtAName12.Text)) / 100)) + Convert.ToDouble(txtAName11.Text)).ToString();
            }
            catch { }
        }

        private void txtAName12_TextChanged(object sender, TextChangedEventArgs e)
        {


            try
            {
                if (txtAName11.Text.Trim() == "")
                    txtAName11.Text = "0";

                if (txtAName12.Text.Trim() == "")
                    txtAName12.Text = "0";
                txtAName10.Text = ((Convert.ToDouble(txtAName3.Text) * ((100 - Convert.ToDouble(txtAName12.Text)) / 100)) + Convert.ToDouble(txtAName11.Text)).ToString();
            }
            catch { }
        }

        private void txtAName3_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (txtAName11.Text.Trim() == "")
                    txtAName11.Text = "0";

                if (txtAName3.Text.Trim() == "")
                    txtAName3.Text = "0";
                if (txtAName12.Text.Trim() == "")
                    txtAName12.Text = "0";


                double afterDisc = Convert.ToDouble(txtAName3.Text) * (100 - Convert.ToDouble(txtAName12.Text)) / 100;


                txtAName10.Text = (afterDisc * ((100 + Convert.ToDouble(txtAName11.Text)) / 100)).ToString();



            }
            catch { }


        }

        private void txtAName6_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtAName6.Text.Trim() == "")
                    txtAName6.Text = "0";

                if (txtAName14.Text.Trim() == "")
                    txtAName14.Text = "0";
                txtAName15.Text = (Convert.ToDouble(txtAName6.Text) * ((100 - Convert.ToDouble(txtAName14.Text) + Convert.ToDouble(txtAName25.Text)) / 100)).ToString();
            }
            catch { }
        }

        private void txtAName14_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtAName6.Text.Trim() == "")
                    txtAName6.Text = "0";

                if (txtAName14.Text.Trim() == "")
                    txtAName14.Text = "0";

                if (txtAName25.Text.Trim() == "")
                    txtAName25.Text = "0";

                double afterDisc = Convert.ToDouble(txtAName6.Text) * (100 - Convert.ToDouble(txtAName14.Text)) / 100;


                txtAName15.Text = (afterDisc * ((100 + Convert.ToDouble(txtAName25.Text)) / 100)).ToString();
            }
            catch { }



        }

        private void SettingBTN_Click(object sender, RoutedEventArgs e)
        {
            new SettingPage().Show();
        }

        private void TxtAName13_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtAName13.Text.Trim() == "")
                    txtAName13.Text = "0";

                if (txtAName18.Text.Trim() == "")
                    txtAName18.Text = "0";
                txtAName19.Text = (Convert.ToDouble(txtAName13.Text) * ((100 - Convert.ToDouble(txtAName18.Text)) / 100)).ToString();

                if (txtAName16.Text.Trim() == "")
                {
                    txtAName16.Text = "0";
                }
                txtAName17.Text = (Convert.ToDouble(txtAName16.Text) - Convert.ToDouble(txtAName19.Text)).ToString();
            }
            catch { }


        }

        private void TxtAName16_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtAName16.Text.Trim() == "")
                {
                    txtAName16.Text = "0";
                }
                if (Convert.ToDouble(txtAName16.Text) > Convert.ToDouble(txtAName19.Text))
                    txtAName16.Text = txtAName19.Text;
                txtAName17.Text = (Convert.ToDouble(txtAName16.Text) - Convert.ToDouble(txtAName19.Text)).ToString();
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainstackss2.Children.Clear();
            subgroup2.Children.Clear();
            txtAName13.Text = "0";
            txtAName16.Text = "0";
            txtAName17.Text = "0";
            txtAName18.Text = "0";
        }

        private void Savenewpaitient1_Click(object sender, RoutedEventArgs e)
        {


            try
            {




                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  CLIENT_ID='" + txtAName24.Text + "' and client_type='V'");

                if (dt.Rows.Count > 0)
                {

                    db.RunNonQuery("update CLIENT set NAME='" + txtAName21.Text + "',  client_type='V', MOBILE='" + txtAName20.Text.Trim() + "' , PHONE='" + txtAName23.Text.Trim() + "' ,ADDRESS='" + txtAName22.Text + "' where CLIENT_ID='" + txtAName24.Text + "' and client_type='V'", "تم تحديث البيانات");

                    return;
                }
                string CLIENT_ID = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT  where client_type='V'").Rows[0][0].ToString();
                if (txtAName23.Text.Trim() == "" && txtAName20.Text.Trim() == "")
                {

                    MessageBox.Show(" برجاء ادخال رقم الهاتف");
                    return;
                }

                //if(txtAName7.Text.Trim() == "") {

                //    MessageBox.Show(" برجاء ادخال العنوان");
                //    return;
                //}

                // and client_type='V'

                db.RunNonQuery("insert into CLIENT (CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE,client_type) values ('" + CLIENT_ID + "','" + txtAName21.Text.Trim() + "','" + txtAName23.Text.Trim() + "','" + txtAName20.Text.Trim() + "','" + txtAName22.Text.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','V')", "تم حفظ البيانات");

                txtAName24.Text = CLIENT_ID;
            }
            catch { }
        }

        private void TxtAName20_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where MOBILE='" + txtAName20.Text + "' or PHONE='" + txtAName20.Text + "' ");

                if (dt.Rows.Count > 0)
                {
                    // txtAName7.Text = dt.Rows[0][4].ToString();
                    txtAName21.Text = dt.Rows[0][1].ToString();
                    txtAName22.Text = dt.Rows[0][3].ToString();
                    txtAName24.Text = dt.Rows[0][0].ToString();
                    txtAName23.Text = dt.Rows[0][2].ToString();


                }
            }
            catch { }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            txtAName20.Text = "";


            txtAName22.Text = "";
            txtAName21.Text = "";
            txtAName23.Text = "";
            // txtAName24.Text = "";
            txtAName24.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='V' ").Rows[0][0].ToString();
        }

        //        private void Button_Click_8(object sender, RoutedEventArgs e) {

        //            try {

        //                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
        //                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,charge,discountpct,totalafterdisount)
        //values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName6.Text + "','" + txtAName1.Text + "','Cash','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','inprogress','" + txtAName2.Text + "','" + txtAName14.Text + "','" + txtAName15.Text + "')");


        //                foreach(ListBoxItem item in mainstackss.Children) {

        //                    order temp = item.Content as order;

        //                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE) 
        //values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

        //                }


        //                //try {
        //                //    DataTable dt = db.RunReader("select * from POS_RECIEPT_V  where INVOICE_ID='" + INVOICE_ID + "'");
        //                //    View_Report showreport = new View_Report();
        //                //    crystalrep rpon = new crystalrep();
        //                //    rpon.Database.Tables["POS_RECIEPT_V"].SetDataSource(dt);

        //                //    //rpon.Parameter_discountpct.CurrentValues=
        //                //    rpon.SetParameterValue("discountpct", txtAName14.Text.Trim());
        //                //    rpon.SetParameterValue("totalafterdiscount ", txtAName15.Text.Trim());
        //                //    //   rpon.Parameter_discountpct
        //                //    showreport.crystalReportViewer1.ReportSource = rpon;
        //                //    rpon.PrintToPrinter(2, true, 1, 1);
        //                //    showreport.ShowDialog();
        //                MessageBox.Show("تم عملية ");
        //                //} catch(Exception ex) { MessageBox.Show(ex.Message); }


        //                mainstackss.Children.Clear();
        //                subgroup.Children.Clear();
        //                txtAName1.Text = "0";
        //                txtAName6.Text = "0";
        //                txtAName2.Text = "0";
        //            } catch(Exception ex) {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {
                string clintID = txtAName44.Text;
                if (db.RunReader("select  coalesce(CLIENT_ID,0) from CLIENT where CLIENT_ID='" + txtAName44.Text + "' and client_type='C'").Rows.Count <= 0)
                    clintID = "";


                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,charge,discountpct,totalafterdisount,TaxAmt,CLIENT_ID)
values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName6.Text + "','" + txtAName1.Text + "','Sales','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Complated','" + txtAName2.Text + "','" + txtAName14.Text + "','" + txtAName15.Text + "','" + txtAName25.Text + "','" + clintID + "')");





                foreach (ListBoxItem item in mainstackss.Children)
                {
                    string TotalQTY = "0", ProductType = "unit";
                    order temp = item.Content as order;
                    if (temp.comboBox.Text.Trim() == "جملة")
                    {
                        TotalQTY = (Convert.ToDouble(temp.txtQTY.Text) * Convert.ToDouble(temp.package_size.Content.ToString())).ToString();
                        ProductType = "package";
                    }
                    else
                        TotalQTY = temp.txtQTY.Text;

                    db.RunNonQuery(@"update PRODUCT set QTY=QTY-'" + TotalQTY + "' where PRODUCT_ID='" + temp.code.Content.ToString() + "'");
                    List<produ> ts = getitems(Convert.ToInt32(temp.code.Content.ToString()), Convert.ToInt32(TotalQTY));

                    if (ts.Count <= 0)
                    {
                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','0','" + temp.disc.Text + "','" + ProductType + "')");

                    }
                    else
                    {
                        Double total_price_Shera = 0, Total_Count = 0, AVG_price_shera = 0;
                        foreach (produ it in ts)
                        {
                            total_price_Shera += Convert.ToDouble(it.price);
                            Total_Count += Convert.ToDouble(it.number);

                        }

                        AVG_price_shera = total_price_Shera / Total_Count;

                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + Total_Count + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','" + AVG_price_shera + "','" + temp.disc.Text + "','" + ProductType + "')");

                    }

                }


                //try
                //{
                //    DataTable dt2 = db.RunReader("select * from pos_salestotal_v  ");
                //        //where INVOICE_ID='" + INVOICE_ID + "'");
                //    View_Report showreport = new View_Report();
                //   totalsales rpon = new totalsales();
                //    rpon.Database.Tables["pos_salestotal_v"].SetDataSource(dt2);
                //    showreport.crystalReportViewer1.ReportSource = rpon;
                //    //  rpon.PrintToPrinter(1, true, 1, 1);

                //    showreport.ShowDialog();
                //    MessageBox.Show("تم عملية ");
                //}
                //catch (Exception ex) { MessageBox.Show(ex.Message); }


                try
                {

                    DataTable dt2 = new DataTable();
                    //  if (clintID =="")
                    // dt2 = db.RunReader("select  * from pos_MEDICAL_V   where INVOICE_ID='" + INVOICE_ID + "' and Package_size!=0  ");
                    // else
                    dt2 = db.RunReader("select  * from pos_MEDICAL_V   where INVOICE_ID='" + INVOICE_ID + "' and Package_size!=0 and  coalesce(client_type,'C')='C'  ");

                    View_Report showreport = new View_Report();
                    invoicerep rpon = new invoicerep();
                    rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    //  rpon.PrintToPrinter(1, true, 1, 1);

                    showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


                mainstackss.Children.Clear();
                subgroup.Children.Clear();
                txtAName1.Text = "0";
                txtAName6.Text = "0";
                txtAName2.Text = "0";
                //  txtAName25.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtAName20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName20.Text + "' and client_type='V' ");

                    if (dt.Rows.Count > 0)
                    {
                        txtAName24.Text = dt.Rows[0][0].ToString();
                        txtAName21.Text = dt.Rows[0][1].ToString();
                        txtAName23.Text = dt.Rows[0][2].ToString();
                        txtAName20.Text = dt.Rows[0][3].ToString();
                        txtAName22.Text = dt.Rows[0][4].ToString();

                    }
                    else
                    {
                        dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  client_type='V' AND ( MOBILE='" + txtAName20.Text + "' or PHONE='" + txtAName20.Text + "')");
                        if (dt.Rows.Count > 0)
                        {
                            // txtAName7.Text = dt.Rows[0][4].ToString();
                            txtAName24.Text = dt.Rows[0][0].ToString();
                            txtAName21.Text = dt.Rows[0][1].ToString();
                            txtAName23.Text = dt.Rows[0][2].ToString();
                            txtAName20.Text = dt.Rows[0][3].ToString();
                            txtAName22.Text = dt.Rows[0][4].ToString();


                        }
                        else
                        {
                            MessageBox.Show("برجاء التأكد من رقم العميل");
                        }
                    }
                }
                catch { }
            }
        }

        private void TxtAName5_KeyDown(object sender, KeyEventArgs e)
        {



            if (e.Key == Key.Enter)
            {
                try
                {
                    DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName5.Text + "' and client_type='C' ");

                    if (dt.Rows.Count > 0)
                    {
                        txtAName9.Text = dt.Rows[0][0].ToString();
                        txtAName4.Text = dt.Rows[0][1].ToString();
                        txtAName8.Text = dt.Rows[0][2].ToString();
                        txtAName5.Text = dt.Rows[0][3].ToString();
                        txtAName7.Text = dt.Rows[0][4].ToString();

                        txtAName28.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' and client_type='C'").Rows[0][0].ToString();
                        txtAName27.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt>0 and client_type='C'").Rows[0][0].ToString();
                        txtAName26.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt<0 and client_type='C'").Rows[0][0].ToString();

                    }
                    else
                    {
                        dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  client_type='C' and ( MOBILE='" + txtAName5.Text + "' or PHONE='" + txtAName5.Text + "')");
                        if (dt.Rows.Count > 0)
                        {
                            // txtAName7.Text = dt.Rows[0][4].ToString();
                            txtAName9.Text = dt.Rows[0][0].ToString();
                            txtAName4.Text = dt.Rows[0][1].ToString();
                            txtAName8.Text = dt.Rows[0][2].ToString();
                            txtAName5.Text = dt.Rows[0][3].ToString();
                            txtAName7.Text = dt.Rows[0][4].ToString();



                            txtAName28.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' and client_type='C'").Rows[0][0].ToString();
                            txtAName27.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt>0 and client_type='C'").Rows[0][0].ToString();
                            txtAName26.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt<0 and client_type='C'").Rows[0][0].ToString();


                        }
                        else
                        {
                            MessageBox.Show("برجاء التأكد من رقم العميل");
                        }
                    }
                }
                catch { }
            }
        }

        private void TxtAName30_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAName10.Text) || string.IsNullOrWhiteSpace(txtAName30.Text))
                    return;

                if (Convert.ToDouble(txtAName30.Text) > Convert.ToDouble(txtAName10.Text))
                    txtAName30.Text = txtAName10.Text;
                txtAName29.Text = (Convert.ToDouble(txtAName30.Text) - Convert.ToDouble(txtAName10.Text)).ToString();

            }
            catch { }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_price_items2.Text) || string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("اكمل البيانات");
                return;
            }
            if (txt_price_items1.Text == string.Empty)
            {
                txt_price_items1.Text = db.RunReader("SELECT coalesce(max(expense_id), 1) + 1 from Expenses").Rows[0][0].ToString();
                db.RunNonQuery("INSERT INTO Expenses(expense_id,name,Amount,CreatedBy,createddate,user_id) VALUES ('" + txt_price_items1.Text + "','" + textBox.Text.Trim() + "','" + txt_price_items2.Text.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + User.Code + "')");

            }
            else
            {
                db.RunNonQuery(@"update Expenses set name='" + textBox.Text.Trim() + "',Amount='" + txt_price_items2.Text.Trim() + "',CreatedBy='" + User.Name + "',createddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',user_id ='" + User.Code + "' where expense_id='" + txt_price_items1.Text + "'");

            }
            //try
            //{
            //    DataTable dt2 = db.RunReader("select * from pos_MEDICAL_V   where INVOICE_ID='" + INVOICE_ID + "'");
            //    View_Report showreport = new View_Report();
            //    invoicerep rpon = new invoicerep();
            //    rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
            //    showreport.crystalReportViewer1.ReportSource = rpon;
            //    //  rpon.PrintToPrinter(1, true, 1, 1);

            //    showreport.ShowDialog();
            //  //  MessageBox.Show("تم عملية ");
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
            MessageBox.Show("تم حفظ البيانات");
            txt_price_items2.Text = "";
            textBox.Text = "";
            txt_price_items1.Text = "";
            txt_search_items_SelectionChanged(sender, e);

        }

        private void TxtAName31_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.Key == Key.Enter) {
            //    try {
            //        DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName31.Text + "' ");

            //        if(dt.Rows.Count > 0) {
            //            txtAName35.Text = dt.Rows[0][0].ToString();
            //            txtAName32.Text = dt.Rows[0][1].ToString();
            //            txtAName31.Text = dt.Rows[0][2].ToString();
            //            txtAName34.Text = dt.Rows[0][3].ToString();
            //            txtAName33.Text = dt.Rows[0][4].ToString();

            //            txtAName38.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName35.Text.Trim() + "'").Rows[0][0].ToString();
            //            txtAName37.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt>0").Rows[0][0].ToString();
            //            txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt<0").Rows[0][0].ToString();

            //        } else {
            //            dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where MOBILE='" + txtAName31.Text + "' or PHONE='" + txtAName31.Text + "'");
            //            if(dt.Rows.Count > 0) {
            //                // txtAName7.Text = dt.Rows[0][4].ToString();
            //                txtAName35.Text = dt.Rows[0][0].ToString();
            //                txtAName32.Text = dt.Rows[0][1].ToString();
            //                txtAName31.Text = dt.Rows[0][2].ToString();
            //                txtAName34.Text = dt.Rows[0][3].ToString();
            //                txtAName33.Text = dt.Rows[0][4].ToString();


            //                txtAName38.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName35.Text.Trim() + "'").Rows[0][0].ToString();
            //                txtAName37.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt>0").Rows[0][0].ToString();
            //                txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt<0").Rows[0][0].ToString();


            //            } else {
            //                MessageBox.Show("برجاء التأكد من رقم العميل");
            //            }
            //        }
            //    } catch { }
            //}
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            txtAName31.Text = "";


            txtAName33.Text = "";
            txtAName32.Text = "";
            txtAName34.Text = "";
            txtAName35.Text = "";
            txtAName36.Text = "0";
            // txtAName37.Text = "0";
            //txtAName38.Text = "0";
            // txtAName9.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT ").Rows[0][0].ToString();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (cbx_typ_items9.SelectedIndex < 0)
                return;
            string d= "C";
            DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  CLIENT_ID='" + cbx_typ_items9.Text + "' and client_type='" + d + "'");
            if (dt.Rows.Count <= 0)
            {

                MessageBox.Show("برجاء اختيار العميل");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAName39.Text))
            {
                MessageBox.Show("برجاء ادخال المبلغ المطلوب");
                return;
            }



            //  txtAName39.Text = (radioButton_Copy.IsChecked == true) ? (Convert.ToDouble(txtAName39.Text) * -1).ToString() : txtAName39.Text;



            db.RunNonQuery("INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'0','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','-" + txtAName39.Text + "','" + cbx_typ_items9.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + d + "')", "تم الحفظ");


            txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type      where a.client_id='" + cbx_typ_items9.Text.Trim() + "' and a.client_type='" + d + "' ").Rows[0][0].ToString();
            //txtAName37.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt>0 and a.client_type='" + d + "' ").Rows[0][0].ToString();
            //txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt<0 and a.client_type='" + d + "' ").Rows[0][0].ToString();


            txtAName39.Text = "";

        }

        private void TxtAName40_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName40.Text + "' and client_type='C' ");

                    if (dt.Rows.Count > 0)
                    {
                        txtAName44.Text = dt.Rows[0][0].ToString();
                        txtAName41.Text = dt.Rows[0][1].ToString();
                        txtAName43.Text = dt.Rows[0][2].ToString();
                        txtAName40.Text = dt.Rows[0][3].ToString();
                        // txtAName42.Text = dt.Rows[0][4].ToString();

                    }
                    else
                    {
                        dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  client_type='C' and ( MOBILE='" + txtAName40.Text + "' or PHONE='" + txtAName40.Text + "')");
                        if (dt.Rows.Count > 0)
                        {
                            // txtAName7.Text = dt.Rows[0][4].ToString();
                            txtAName44.Text = dt.Rows[0][0].ToString();
                            txtAName41.Text = dt.Rows[0][1].ToString();
                            txtAName43.Text = dt.Rows[0][2].ToString();
                            txtAName40.Text = dt.Rows[0][3].ToString();
                            //txtAName42.Text = dt.Rows[0][4].ToString();


                        }
                        else
                        {
                            MessageBox.Show("برجاء التأكد من رقم العميل");
                        }
                    }
                }
                catch { }
            }
        }

        private void Savenewpaitient2_Click(object sender, RoutedEventArgs e)
        {
            try
            {




                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  CLIENT_ID='" + txtAName44.Text + "' AND client_type='C'");

                if (dt.Rows.Count > 0)
                {

                    db.RunNonQuery("update CLIENT set NAME='" + txtAName41.Text.Trim() + "' ,MOBILE='" + txtAName44.Text.Trim() + "' , PHONE='" + txtAName43.Text.Trim() + "',client_type='C' ,ADDRESS='" + txtAName42.Text + "' where CLIENT_ID='" + txtAName44.Text + "'  AND client_type='C' ", "تم تحديث البيانات");

                    return;
                }
                string CLIENT_ID = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='C' ").Rows[0][0].ToString();
                if (txtAName43.Text.Trim() == "" && txtAName40.Text.Trim() == "")
                {

                    MessageBox.Show(" برجاء ادخال رقم الهاتف");
                    return;
                }

                //if(txtAName7.Text.Trim() == "") {

                //    MessageBox.Show(" برجاء ادخال العنوان");
                //    return;
                //}

                //client_type='C'

                db.RunNonQuery("insert into CLIENT (CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE,client_type) values ('" + CLIENT_ID + "','" + txtAName41.Text.Trim() + "','" + txtAName43.Text.Trim() + "','" + txtAName40.Text.Trim() + "','" + txtAName42.Text.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')", "تم حفظ البيانات");

                txtAName44.Text = CLIENT_ID;
            }
            catch { }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            txtAName40.Text = "";


            txtAName42.Text = "";
            txtAName41.Text = "";
            txtAName43.Text = "";
            // txtAName24.Text = "";
            txtAName44.Text = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='C' ").Rows[0][0].ToString();

        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            mainstackss.Children.Clear();
            subgroup.Children.Clear();
            txtAName25.Text = "0";
            txtAName6.Text = "0";
            txtAName14.Text = "0";
            txtAName15.Text = "0";
            txtAName1.Text = "0";
            txtAName2.Text = "0";
        }

        private void Cbx_typ_items1_DropDownClosed(object sender, EventArgs e)
        {
            fillgroub(cbx_typ_items1.SelectedIndex.ToString(), txtAName45.Text.Trim());
        }

        private void TxtAName45_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fillgroub(cbx_typ_items1.SelectedIndex.ToString(), txtAName45.Text.Trim());
        }

        private void Cbx_typ_items2_DropDownClosed(object sender, EventArgs e)
        {
            fillgroub1(cbx_typ_items2.SelectedIndex.ToString(), txtAName46.Text.Trim());
        }

        private void TxtAName46_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fillgroub1(cbx_typ_items2.SelectedIndex.ToString(), txtAName46.Text.Trim());
        }

        private void TxtAName47_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fillgroub_payee(cbx_typ_items3.SelectedIndex.ToString(), txtAName47.Text.Trim());
        }

        private void txt_search_items_SelectionChanged(object sender, RoutedEventArgs e)
        {
            dg_items.ItemsSource = db.RunReader("select expense_id as 'كود المصروفات',name as 'الوصف',Amount as 'اجمالي المصروفات',CreatedBy as 'بواسطة',createddate as 'بتاريخ' from Expenses where  name like '%" + txt_search_items.Text + "%' ").DefaultView;
        }

        private void dg_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_items.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_items.SelectedItem;
                    txt_price_items1.Text = row[0].ToString();
                    textBox.Text = row[1].ToString();
                    txt_price_items2.Text = row[2].ToString();
                }
            }
            catch { }
        }



        private void dg_items_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_items_Copy.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dg_items_Copy.SelectedItem;
                txtAName31.Text = row[0].ToString();

                try
                {
                    string d = "C";
                    //checkBox.IsChecked = (radioButton.IsChecked == true) ? true : false;
                    DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where CLIENT_ID='" + txtAName31.Text + "' and client_type='" + d + "' ");

                    if (dt.Rows.Count > 0)
                    {
                        txtAName35.Text = dt.Rows[0][0].ToString();
                        txtAName32.Text = dt.Rows[0][1].ToString();
                        txtAName31.Text = dt.Rows[0][2].ToString();
                        // txtAName34.Text = dt.Rows[0][3].ToString();
                        // txtAName33.Text = dt.Rows[0][4].ToString();

                        txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type   and a.client_type=c.client_type   where a.client_id='" + txtAName35.Text.Trim() + "' and a.client_type='" + d + "' ").Rows[0][0].ToString();
                        //txtAName37.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt>0 and a.client_type='" + d + "' ").Rows[0][0].ToString();
                        //txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt<0 and a.client_type='" + d + "' ").Rows[0][0].ToString();

                    }
                    else
                    {
                        dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where MOBILE='" + txtAName31.Text + "' or PHONE='" + txtAName31.Text + "'  and client_type='" + d + "' ");
                        if (dt.Rows.Count > 0)
                        {
                            // txtAName7.Text = dt.Rows[0][4].ToString();
                            txtAName35.Text = dt.Rows[0][0].ToString();
                            txtAName32.Text = dt.Rows[0][1].ToString();
                            txtAName31.Text = dt.Rows[0][2].ToString();
                            // txtAName34.Text = dt.Rows[0][3].ToString();
                            // txtAName33.Text = dt.Rows[0][4].ToString();


                            txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "'  and a.client_type='" + d + "'").Rows[0][0].ToString();
                            //txtAName37.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt>0  and a.client_type='" + d + "'").Rows[0][0].ToString();
                            //txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation a join CLIENT c on  c.client_id=a.client_id and a.client_type=c.client_type    where a.client_id='" + txtAName35.Text.Trim() + "' AND AllocarionAmt<0  and a.client_type='" + d + "'").Rows[0][0].ToString();


                        }
                        else
                        {
                            MessageBox.Show("برجاء التأكد من رقم العميل");
                        }
                    }
                }
                catch { }
            }
        }

        private void Txt_search_items1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = new DataTable();
            //if (radioButton.IsChecked == true)
            //    dt = db.RunReader("select CLIENT_ID as 'كود العميل', NAME as 'اسم العميل' , MOBILE as 'رقم الموبيل'  from CLIENT where CLIENT_ID like '%" + txt_search_items1.Text.Trim() + "%'  and client_type='V'");
            //else
            dt = db.RunReader("select CLIENT_ID as 'كود العميل', NAME as 'اسم العميل' , MOBILE as 'رقم الموبيل'  from CLIENT where CLIENT_ID like '%" + txt_search_items1.Text.Trim() + "%' and client_type='C' ");
            dg_items_Copy.ItemsSource = dt.DefaultView;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                //if (radioButton.IsChecked == true)
                //    dt = db.RunReader("select CLIENT_ID as 'كود العميل', NAME as 'اسم العميل' , MOBILE as 'رقم الموبيل'  from CLIENT where CLIENT_ID like '%" + txt_search_items1.Text.Trim() + "%'  and client_type='V'");
                //else
                dt = db.RunReader("select CLIENT_ID as 'كود العميل', NAME as 'اسم العميل' , MOBILE as 'رقم الموبيل'  from CLIENT where CLIENT_ID like '%" + txt_search_items1.Text.Trim() + "%' and client_type='C' ");
                dg_items_Copy.ItemsSource = dt.DefaultView;
            }
            catch { }
        }



        private void Button_Click_15(object sender, RoutedEventArgs e)
        {

            DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where (MOBILE='" + txtAName5.Text + "' or PHONE='" + txtAName5.Text + "') and CLIENT_ID='" + txtAName9.Text + "' and client_type='C'");
            if (dt.Rows.Count <= 0)
            {

                MessageBox.Show("برجاء اختيار العميل");
                return;
            }


            CHECK ch = new CHECK();
            ch.ShowDialog();


            if (ch.Check == false)
                return;

            string checkNumber = ch.checkNumber;
            DateTime dateCheck = ch.dateCheck;




            try
            {

                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED,TIMEINVOICED,TOTAL,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE,INVOICE_STATUS,CLIENT_ID,discountpct,totalafterdisount,TaxAmt,charge,checkno,dateact)
                values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName10.Text + "','0','Allocation-Check','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','inprogress','" + txtAName9.Text + "','" + txtAName12.Text + "','" + txtAName3.Text + "','" + txtAName11.Text + "','" + txtAName29.Text + "','" + checkNumber + "','" + dateCheck.ToString("yyyy-MM-dd") + "')");

                db.RunNonQuery("INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',-1*'" + txtAName29.Text + "','" + txtAName9.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')");


                foreach (ListBoxItem item in mainstackss1.Children)
                {
                    string TotalQTY = "0";
                    order temp = item.Content as order;
                    if (temp.comboBox.Text.Trim() == "جملة")
                        TotalQTY = temp.txtQTY.Text;
                    else
                        TotalQTY = temp.txtQTY.Text;


                    List<produ> ts = getitems(Convert.ToInt32(temp.code.Content.ToString()), Convert.ToInt32(TotalQTY));


                    db.RunNonQuery(@"update PRODUCT set QTY=QTY-'" + TotalQTY + "' where PRODUCT_ID='" + temp.code.Content.ToString() + "'");


                    if (ts.Count <= 0)
                    {
                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + temp.txtQTY.Text + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','0','" + temp.disc.Text + "','package')");

                    }
                    else
                    {
                        Double total_price_Shera = 0, Total_Count = 0, AVG_price_shera = 0;
                        foreach (produ it in ts)
                        {
                            total_price_Shera += Convert.ToDouble(it.price);
                            Total_Count += Convert.ToDouble(it.number);

                        }

                        AVG_price_shera = total_price_Shera / Total_Count;

                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,Sales_Unit_price,Purchase_Unit_price,DiscountPctLine,ProductType) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp.code.Content.ToString() + "','" + Total_Count + "','" + temp.totalprice.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp.price.Content + "','" + AVG_price_shera + "','" + temp.disc.Text + "','package')");

                    }

                }


                try
                {
                    DataTable dt2 = db.RunReader("select * from pos_MEDICAL_V  where INVOICE_ID='" + INVOICE_ID + "' and Package_size!=0 and client_type='C' ");
                    View_Report showreport = new View_Report();
                    invoicerep rpon = new invoicerep();
                    rpon.Database.Tables["pos_MEDICAL_V"].SetDataSource(dt2);
                    showreport.crystalReportViewer1.ReportSource = rpon;
                    //  rpon.PrintToPrinter(1, true, 1, 1);

                    showreport.ShowDialog();
                    MessageBox.Show("تم عملية ");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


                //   MessageBox.Show("تم عملية ");
                mainstackss1.Children.Clear();
                subgroup1.Children.Clear();
                txtAName3.Text = "0";
                txtAName10.Text = "0";
                txtAName11.Text = "0";
                txtAName30.Text = "0";
                txtAName29.Text = "0";

                txtAName28.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' and client_type='C'").Rows[0][0].ToString();
                txtAName27.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt>0 and client_type='C'").Rows[0][0].ToString();
                txtAName26.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + txtAName9.Text.Trim() + "' AND AllocarionAmt<0 and client_type='C'").Rows[0][0].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد ارجاع الفاتورة", "ارجاع فاتورة", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {



                if (dg_items1.SelectedItem != null)
                {

                    DataRowView row = (DataRowView)dg_items1.SelectedItem;

                    String invoiceID = row[0].ToString();


                    double totalmon = 0;


                    DataTable dttem = db.RunReader("SELECT INVOICE_ID as 'رقم الفاتورة',PRODUCT_ID  as 'رقم المنتج',QTY as 'الكمية',TOTALLINE as 'الاجمالى',Sales_Unit_price as 'سعر القطعة',Purchase_Unit_price as 'سعر الشراء',INVOICE_LINE_ID,coalesce(Bonus,0) FROM INVOICE_LINE WHERE INVOICE_ID='" + invoiceID + "' ");


                    for (int i = 0; i < dttem.Rows.Count; i++)
                    {



                        String invoiceID1 = dttem.Rows[i][0].ToString(), PRODUCT_ID = dttem.Rows[i][1].ToString(), QTY = dttem.Rows[i][2].ToString(), TOTALLINE = dttem.Rows[i][3].ToString(), Sales_Unit_price = dttem.Rows[i][4].ToString(), Purchase_Unit_price = dttem.Rows[i][5].ToString(), INVOICE_LINE_ID = dttem.Rows[i][6].ToString(),bounas= dttem.Rows[i][7].ToString();
                        double rtarn = double.TryParse(TOTALLINE, out rtarn) ? rtarn : 0;
                        totalmon = totalmon + rtarn;

                        db.RunNonQuery(@"update PRODUCT set QTY=coalesce(QTY, 0)+'" + QTY.Trim() + "'+'"+bounas.Trim()+"' , PURCHASE_UNIT_PRICE='" + Purchase_Unit_price.Trim() + "' where PRODUCT_ID='" + PRODUCT_ID + "'");

                        DataTable tempdt = db.RunReader("select count(*) from PriceList where PriceList_ID='" + PRODUCT_ID + "' AND Price='" + Purchase_Unit_price + "'");
                        if (tempdt.Rows[0][0].ToString() == "0")
                            db.RunNonQuery("INSERT INTO PriceList(ID,PriceList_ID,Price,QTY,CREATEDBY,CREAREDDATE) VALUES ((select coalesce(max(ID), '0')+1 from PriceList ),'" + PRODUCT_ID + "','" + Purchase_Unit_price + "','" + QTY.Trim() + "'+'"+bounas.Trim()+"','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                        else
                            db.RunReader("UPDATE PriceList SET QTY=coalesce(QTY, 0)+'" + QTY.Trim() + "'+'"+bounas.Trim()+"' WHERE PriceList_ID='" + PRODUCT_ID + "' AND Price='" + Purchase_Unit_price + "'");


                        db.RunNonQuery("UPDATE INVOICE SET total=total-'" + TOTALLINE + "' ,totalafterdisount=totalafterdisount-'" + TOTALLINE + "' WHERE INVOICE_ID='" + invoiceID1 + "'");
                        db.RunNonQuery("DELETE from INVOICE_LINE where INVOICE_LINE_ID='" + INVOICE_LINE_ID + "'");
                        dg_items1_Copy.ItemsSource = null;//db.RunReader("SELECT i.INVOICE_ID as 'رقم الفاتورة',i.PRODUCT_ID  as 'رقم المنتج',i.QTY as 'الكمية',i.TOTALLINE as 'الاجمالى',i.Sales_Unit_price as 'سعر القطعة',i.Purchase_Unit_price as 'سعر الشراء',i.INVOICE_LINE_ID,v.NAME as 'اسم المنتج' FROM INVOICE_LINE i join PRODUCT v on v.PRODUCT_ID=i.PRODUCT_ID WHERE INVOICE_ID='" + invoiceID1 + "'").DefaultView;


                    }
                    string claint_id = db.RunReader("select max(client_id) from INVOICE where INVOICE_ID='" + invoiceID + "'").Rows[0][0].ToString();


                    db.RunNonQuery(@"INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) 
                       values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'" + invoiceID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + -totalmon + "','" + claint_id + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')");


                    db.RunNonQuery("DELETE from INVOICE where INVOICE_ID='" + invoiceID + "'", "تم ارجاع الفاتورة بنجاح");
                    fillInvoice();

                }
            }
        }

        private void Txt_search_items2_TextChanged(object sender, TextChangedEventArgs e)
        {
            fillInvoice(txt_search_items2.Text.Trim());
        }

        private void Dg_items1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dg_items1_Copy.ItemsSource = null;
                if (dg_items1.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_items1.SelectedItem;
                    //  txt_price_items1.Text = row[0].ToString();
                    dg_items1_Copy.ItemsSource = db.RunReader("SELECT i.INVOICE_ID as 'رقم الفاتورة',i.PRODUCT_ID  as 'رقم المنتج',i.QTY as 'الكمية',i.TOTALLINE as 'الاجمالى',i.Sales_Unit_price as 'سعر القطعة',i.Purchase_Unit_price as 'سعر الشراء',i.INVOICE_LINE_ID,v.NAME as 'اسم المنتج',coalesce(Bonus,0) as 'بونص' FROM INVOICE_LINE i join PRODUCT v on v.PRODUCT_ID=i.PRODUCT_ID  WHERE INVOICE_ID='" + row[0].ToString() + "' and i.QTY !=0").DefaultView;
                }
            }
            catch { }
        }

        private void BtnDeleteInvoice_Click_1(object sender, RoutedEventArgs e)
        {
            //if (txt_search_items3.Text.Trim() == string.Empty)
            //{
            //    System.Windows.Forms.MessageBox.Show("ادخل الكمية المرتجعة");
            //}

            //else 
            if (MessageBox.Show("هل تريد ارجاع المنتج", "ارجاع منتج", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {



                if (dg_items1_Copy.SelectedItem != null)
                {

                    DataRowView row = (DataRowView)dg_items1_Copy.SelectedItem;

                    String invoiceID = row[0].ToString(), PRODUCT_ID = row[1].ToString(), QTY = row[2].ToString(), TOTALLINE = row[3].ToString(), Sales_Unit_price = row[4].ToString(), Purchase_Unit_price = row[5].ToString(), INVOICE_LINE_ID = row[6].ToString(), bounus = row[8].ToString();
                    if (txt_search_items3.Text.Trim() == string.Empty)
                    {
                        txt_search_items3.Text = QTY;
                    }
                        if (Convert.ToUInt32(txt_search_items3.Text) > Convert.ToInt32(QTY))
                    {
                        System.Windows.Forms.MessageBox.Show("الكمية المرتجعة اكبر من المباعاه");
                        return;
                    }
                    string mortag3 = "0";
                        if(QTY.Trim()== txt_search_items3.Text.Trim())
                    {
                        mortag3 = (Convert.ToDouble(QTY) + Convert.ToDouble(bounus)).ToString();
                    }
                        else
                    {
                        mortag3 = txt_search_items3.Text.Trim();
                    }

                    db.RunNonQuery(@"update PRODUCT set QTY=coalesce(QTY, 0)+'" + mortag3 + "' , PURCHASE_UNIT_PRICE='" + Purchase_Unit_price.Trim() + "' where PRODUCT_ID='" + PRODUCT_ID + "'");

                    DataTable tempdt = db.RunReader("select count(*) from PriceList where PriceList_ID='" + PRODUCT_ID + "' AND Price='" + Purchase_Unit_price + "'");
                    if (tempdt.Rows[0][0].ToString() == "0")
                        db.RunNonQuery("INSERT INTO PriceList(ID,PriceList_ID,Price,QTY,CREATEDBY,CREAREDDATE) VALUES ((select coalesce(max(ID), '0')+1 from PriceList ),'" + PRODUCT_ID + "','" + Purchase_Unit_price + "','" + mortag3 + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    else
                        db.RunReader("UPDATE PriceList SET QTY=coalesce(QTY, 0)+'" + mortag3 + "' WHERE PriceList_ID='" + PRODUCT_ID + "' AND Price='" + Purchase_Unit_price + "'");


                    db.RunNonQuery("UPDATE INVOICE SET total=total-'" + TOTALLINE + "' ,totalafterdisount=totalafterdisount-'" + TOTALLINE + "' WHERE INVOICE_ID='" + invoiceID + "'");
                    if (Convert.ToUInt32(txt_search_items3.Text) < Convert.ToInt32(QTY))
                        db.RunNonQuery("update  INVOICE_LINE  set QTY=QTY-'" + txt_search_items3.Text.Trim() + "'  where INVOICE_LINE_ID='" + INVOICE_LINE_ID + "'", "تم ارجاع المنتج بنجاح");
                    else if (Convert.ToUInt32(txt_search_items3.Text) == Convert.ToInt32(QTY))
                        db.RunNonQuery("DELETE   from  INVOICE_LINE    where INVOICE_LINE_ID='" + INVOICE_LINE_ID + "'", "تم ارجاع المنتج بنجاح");
                    dg_items1_Copy.ItemsSource = db.RunReader("SELECT i.INVOICE_ID as 'رقم الفاتورة',i.PRODUCT_ID  as 'رقم المنتج',i.QTY as 'الكمية',i.TOTALLINE as 'الاجمالى',i.Sales_Unit_price as 'سعر القطعة',i.Purchase_Unit_price as 'سعر الشراء',i.INVOICE_LINE_ID,v.NAME as 'اسم المنتج' ,coalesce(Bonus,0) as 'بونص' FROM INVOICE_LINE i join PRODUCT v on v.PRODUCT_ID=i.PRODUCT_ID WHERE INVOICE_ID='" + invoiceID + "'").DefaultView;

                    string claint_id = db.RunReader("select max(client_id) from INVOICE where INVOICE_ID='" + invoiceID + "'").Rows[0][0].ToString();


                    db.RunNonQuery(@"INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) 
                       values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'" + invoiceID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',-1*'" + txt_search_items3.Text.Trim() + "'*'"+ Sales_Unit_price + "','" + claint_id + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')");

                    if(dg_items1_Copy.Items.Count==0)
                    db.RunNonQuery("DELETE from INVOICE where INVOICE_ID='" + invoiceID + "'");
                    fillInvoice();
                }
            }
        }

        private void txt_search_items4_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void txt_search_items4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)


                dg_items2.ItemsSource = db.RunReader(@"select  distinct
 i.dateact as 'تاريخ الاستحقاق',
i.checkno as 'رقم الشيك',
c.CLIENT_Id as 'كود العميل',
c.name as 'اسم العميل'
,i.total as 'القيمة'
from invoice i
inner join CLIENT c on i.CLIENT_Id = c.CLIENT_Id    and  c.client_type='C'   where i.checkno='" + txt_search_items4.Text + "'  ").DefaultView;
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            txt_search_items4.Text = "";
            dg_items2.ItemsSource = db.RunReader(@"select  distinct
 i.dateact as 'تاريخ الاستحقاق',
i.checkno as 'رقم الشيك', 
c.CLIENT_Id as 'كود العميل' ,
c.name as 'اسم العميل'
,i.total  as 'القيمة'
from invoice i
inner join CLIENT c on i.CLIENT_Id  =c.CLIENT_Id   and  c.client_type='C'  where  i.dateact =date('now') ").DefaultView;
        }

        private void txtAName40_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAName43.Text = txtAName40.Text;
        }

        private void txtAName41_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtAName5_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAName8.Text = txtAName5.Text;
        }

        private void txtAName20_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtAName23.Text = txtAName20.Text;
        }

        private void txtAName31_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAName34.Text = txtAName31.Text;
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtAName50.Text.Trim() == "" || txtAName51.Text.Trim() == "" || txtAName49.Text.Trim() == string.Empty)
                {

                    MessageBox.Show(" اكمل البيانات");
                    return;
                }

                string type_c = (moward.IsChecked == true) ? "V" : "C";
                DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  CLIENT_ID='" + txt_code_items.Text + "' AND client_type='" + type_c + "'");

                if (dt.Rows.Count > 0)
                {

                    db.RunNonQuery("update CLIENT set NAME='" + txtAName50.Text.Trim() + "' ,MOBILE='" + txtAName49.Text.Trim() + "' , PHONE='" + txtAName52.Text.Trim() + "',client_type='" + type_c + "' ,ADDRESS='" + txtAName51.Text + "' where CLIENT_ID='" + txt_code_items.Text + "'  AND client_type='C' ", "تم تحديث البيانات");

                    return;
                }
                string CLIENT_ID = db.RunReader("select coalesce(max(CLIENT_ID), '0')+1 from CLIENT where client_type='" + type_c + "' ").Rows[0][0].ToString();


                //if(txtAName7.Text.Trim() == "") {

                //    MessageBox.Show(" برجاء ادخال العنوان");
                //    return;
                //}

                //client_type='C'

                db.RunNonQuery(@"insert into CLIENT (CLIENT_ID,         NAME                        , MOBILE                            ,PHONE                         ,ADDRESS                        ,CREATEDBY                 ,CREATEDDATE                                      ,client_type) values 
                                               ('" + CLIENT_ID + "','" + txtAName50.Text.Trim() + "','" + txtAName49.Text.Trim() + "','" + txtAName52.Text.Trim() + "','" + txtAName51.Text.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + type_c + "')", "تم حفظ البيانات");
                if (txtAName53.Text.Trim() != string.Empty && Convert.ToDouble(txtAName53.Text) > 0)
                    db.RunNonQuery(@"INSERT INTO Allocation (Allocation_id                   ,INVOICE_ID,AllocationDate                                        ,AllocarionAmt             ,client_id           ,CREATEDBY          ,CREATEDDATE                                         ,client_type) 
                    values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'0'      ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txtAName53.Text + "','" + CLIENT_ID + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + type_c + "')");

                txt_code_items.Text = CLIENT_ID;
            }
            catch { }
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            User.CleanAll(g_coustmar);
        }


        private void txt_search_items5_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string type_c = (moward.IsChecked == true) ? "V" : "C";
            dg_items_Copy1.ItemsSource = db.RunReader("select  CLIENT_ID as 'الكود',NAME as 'الاسم' ,client_type as 'النوع', MOBILE as 'الموبيل',PHONE as 'التليفون',ADDRESS as 'العنوان',CREATEDBY as 'اسم المدخل', CREATEDDATE  as 'تاريخ الادخال' from CLIENT where  CLIENT_ID like '%" + txt_search_items5.Text.Trim() + "%' and NAME like '%" + txt_search_items5.Text.Trim() + "%' and  MOBILE like '%" + txt_search_items5.Text.Trim() + "%'  and PHONE like '%" + txt_search_items5.Text.Trim() + "%'and client_type='" + type_c + "'").DefaultView;
        }

        private void coustmar_Click(object sender, RoutedEventArgs e)
        {
            txtAName53.Text = "0";
            if (coustmar.IsChecked == true && txt_code_items.Text.Trim() == string.Empty)
                dfdddd.Visibility = Visibility.Visible;
            else
                dfdddd.Visibility = Visibility.Collapsed;
        }

        private void dg_items_Copy1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dg_items_Copy1.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_items_Copy1.SelectedItem;

                    txt_code_items.Text = row[0].ToString();
                    txtAName50.Text = row[1].ToString();
                    txtAName49.Text = row[3].ToString();
                    txtAName52.Text = row[4].ToString();
                    txtAName51.Text = row[5].ToString();


                    if (row[2].ToString() == "C")
                        coustmar.IsChecked = true;
                    else moward.IsChecked = true;
                    coustmar_Click(sender, e);

                }
            }
            catch { }
        }
        DataTable dt_pay_cash = new DataTable();
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            try
            {
                txtAName62.Focus();
                if (cbx_typ_items.Text.Trim() == string.Empty || cbx_typ_items4.Text.Trim() == string.Empty || txtAName58.Text.Trim() == string.Empty || txtAName54.Text.Trim() == string.Empty ||Convert.ToDouble( txtAName58.Text) == 0 || Convert.ToDouble(txtAName54.Text) == 0)
                {
                    System.Windows.Forms.MessageBox.Show("اكمل البانات"); return;
                }
                if (dg_items_Copy2.ItemsSource == null || dt_pay_cash == null)
                {
                    dt_pay_cash = new DataTable();
                    dt_pay_cash.Columns.Add("النوع");
                    dt_pay_cash.Columns.Add("كود الصنف");
                    dt_pay_cash.Columns.Add("الصنف");
                    dt_pay_cash.Columns.Add("الكمية");
                    dt_pay_cash.Columns.Add("السعر");
                    dt_pay_cash.Columns.Add("الاجمالي");
                }
                string nametepy = db.RunReader("select  NAME from PRODUCT_CATEGORY  where PRODUCT_CATEGORY_ID='" + cbx_typ_items.Text + "' ").Rows[0][0].ToString();
                string nameitem = db.RunReader("select  NAME from  PRODUCT where  PRODUCT_ID='" + cbx_typ_items4.Text + "'").Rows[0][0].ToString();
                dt_pay_cash.Rows.Add(nametepy, cbx_typ_items4.Text, nameitem, txtAName58.Text, txtAName54.Text, txtAName55.Text);

                dg_items_Copy2.ItemsSource = dt_pay_cash.DefaultView;
                txtAName62.Text = txtAName63.Text = cbx_typ_items.Text = cbx_typ_items4.Text = txtAName58.Text = txtAName54.Text = txtAName55.Text = string.Empty;
                txtAName55_TextChanged(sender, et);
            }
            catch { }

        }

        private void cbx_typ_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cbx_typ_items4.SelectedIndex > -1)
            //        cbx_typ_items4.ItemsSource = db.RunReader("select   PRODUCT_ID ,NAME from  PRODUCT  where  PRODUCT_CATEGORY_ID='" + cbx_typ_items.Text + "'").DefaultView;
            //    else
            //        cbx_typ_items4.ItemsSource = null;
        }

        private void cbx_typ_items_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,case when  TYPE=1 then 'حريمي' else 'رجالي'  END TYPE from  PRODUCT_CATEGORY  where  PRODUCT_CATEGORY_ID like '" + txtAName62.Text.Trim() + "'  or NAME like '%" + txtAName62.Text.Trim() + "%'").DefaultView;
        }

        private void cbx_typ_items_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // cbx_typ_items.IsFocused = ;
                //cbx_typ_items.SelectedIndex = -1;
                //cbx_typ_items.IsDropDownOpen = true;
                txtAName63.Focus();
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items4_DropDownOpened(object sender, EventArgs e)
        {

            cbx_typ_items4.ItemsSource = db.RunReader("select   PRODUCT_ID ,NAME from  PRODUCT  where  PRODUCT_CATEGORY_ID like '" + ((cbx_typ_items.Text == string.Empty) ? "%%" : cbx_typ_items.Text).ToString() + "' and (PRODUCT_ID like '" + txtAName63.Text.Trim() + "' or  NAME like '%" + txtAName63.Text.Trim() + "%')").DefaultView;

        }

        private void cbx_typ_items4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAName58.Focus();
                //cbx_typ_items4.SelectedIndex = -1;
                //cbx_typ_items4.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items5_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items5.ItemsSource = db.RunReader("select CLIENT_ID ,NAME from CLIENT where client_type='V' and   (CLIENT_ID =  '" + txtAName64.Text.Trim() + "' or NAME   like  '%" + txtAName64.Text.Trim() + "%'  or MOBILE =  '" + txtAName64.Text.Trim() + "' or PHONE  like  '" + txtAName64.Text.Trim() + "') ").DefaultView;
        }

        private void cbx_typ_items5_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                cbx_typ_items5.SelectedIndex = -1;
                cbx_typ_items5.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items5_DropDownClosed(object sender, EventArgs e)
        {
            txtAName62.Focus();
            DataTable dt = db.RunReader("select CLIENT_ID ,NAME,PHONE,MOBILE,ADDRESS from CLIENT where client_type='V' and   CLIENT_ID =  '" + cbx_typ_items5.Text.Trim() + "'");
            if (dt.Rows.Count > 0)
            {
                txtAName57.Text = dt.Rows[0][1].ToString();
                txtAName56.Text = dt.Rows[0][2].ToString();
                txtAName60.Text = dt.Rows[0][3].ToString();
                txtAName59.Text = dt.Rows[0][4].ToString();
            }
        }

        private void cbx_typ_items4_DropDownClosed(object sender, EventArgs e)
        {
            txtAName58.Focus();
            if (cbx_typ_items4.SelectedIndex > -1 && cbx_typ_items.Text.Trim() == string.Empty)
            {
                cbx_typ_items.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,case when  TYPE=1 then 'حريمي' else 'رجالي'  END TYPE from  PRODUCT_CATEGORY  ").DefaultView;
                DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID from  PRODUCT  where PRODUCT_ID='" + cbx_typ_items4.Text + "'  ");
                if (dt.Rows.Count > 0)
                {
                    cbx_typ_items.Text = dt.Rows[0][0].ToString();
                }

            }
            if (cbx_typ_items4.SelectedIndex > -1)
            {
                if (dt_pay_cash == null)
                    dt_pay_cash = new DataTable();
                foreach (DataRow item in dt_pay_cash.Rows)
                {
                    if (item[1].ToString() == cbx_typ_items4.Text)
                    {
                        if (System.Windows.Forms.MessageBox.Show("تم اضافة هذا العنصر هل تريد تعديل الكمية او السعر", "", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string qy = item[3].ToString();
                            string d = item[4].ToString();

                            dt_pay_cash.Rows.Remove(item);
                            txtAName58.Text = qy;
                            txtAName54.Text = d;


                        }
                        else
                        {
                            cbx_typ_items4.Text = string.Empty;
                        }


                        break;

                    }
                }
            }
        }

        private void txtAName58_TextChanged(object sender, TextChangedEventArgs e)
        {
            double quntaty = 0, price = 0;
            if (double.TryParse(txtAName58.Text.Trim(), out quntaty) == false || double.TryParse(txtAName54.Text.Trim(), out price) == false)
            { txtAName55.Text = ""; return; }
            txtAName55.Text = (quntaty * price).ToString();
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            User.CleanAll(g_shra);
            dt_pay_cash = null;
        }

        private void txtAName58_LostFocus(object sender, RoutedEventArgs e)
        {
            double quntaty = 0;
            if (double.TryParse(txtAName58.Text.Trim(), out quntaty) == false)
                txtAName58.Text = "";
        }

        private void txtAName54_LostFocus(object sender, RoutedEventArgs e)
        {
            double quntaty = 0;
            if (double.TryParse(txtAName54.Text.Trim(), out quntaty) == false)
                txtAName54.Text = "";
        }
        TextChangedEventArgs et;
        private void txtAName55_TextChanged(object sender, TextChangedEventArgs e)
        {
            double sum = 0, sumline = 0;

            sumline = double.TryParse(txtAName55.Text.Trim(), out sumline) ? sumline : 0;
            if (dg_items_Copy2.Items.Count == 0)
            {
                dt_pay_cash = null;
            }
            else
            {
                foreach (DataRow item in dt_pay_cash.Rows)
                {
                    sum = sum + Convert.ToDouble(item[5].ToString());
                }
            }
            txtAName65.Text = sum.ToString();
            txtAName61.Text = (sum + sumline).ToString();

        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            if (dg_items_Copy2.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dg_items_Copy2.SelectedItem;


                foreach (DataRow item in dt_pay_cash.Rows)
                {
                    if (item[1].ToString() == row[1].ToString())
                    {
                        dt_pay_cash.Rows.Remove(item);

                        break;

                    }
                }

            }
            txtAName55_TextChanged(sender, et);
        }

        private void txtAName54_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                dddddd.Focus();
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            if (dg_items_Copy2.Items.Count > 0)
            {
                if (cbx_typ_items5.SelectedIndex < 0)
                {
                    System.Windows.Forms.MessageBox.Show("اختار المورد"); return;
                }

                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID,DATEINVOICED                                                 ,TIMEINVOICED           ,TOTAL                   ,PAID                 ,INVOICE_TYPE     ,USER_ID         ,CREATEDBY       , CREATEDDATE                                         ,INVOICE_STATUS       ,charge                   ,discountpct              ,totalafterdisount    ,CLIENT_ID)
                          values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName65.Text + "','" + txtAName65.Text + "','Purchasing','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Complated',        '0',                         '0',                             '" + txtAName65.Text + "','" + cbx_typ_items5.Text.Trim() + "')");

                foreach (DataRow temp in dt_pay_cash.Rows)
                {

                    string items_qty = temp[3].ToString();
                    db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID                                    ,INVOICE_ID          ,PRODUCT_ID,QTY,TOTALLINE,CREATEDBY,CREATEDDATE,ProductType) 
                                          values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp[1].ToString() + "','" + items_qty + "','" + temp[5].ToString() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','package')");


                    db.RunNonQuery(@"update PRODUCT set QTY=coalesce(QTY, 0)+'" + items_qty.Trim() + "' , PURCHASE_UNIT_PRICE='" + temp[4].ToString().Trim() + "' where PRODUCT_ID='" + temp[1].ToString() + "'");

                    DataTable tempdt = db.RunReader("select count(*) from PriceList where PriceList_ID='" + temp[1].ToString() + "' AND Price='" + temp[4].ToString().Trim() + "'");
                    if (tempdt.Rows[0][0].ToString() == "0")
                    {
                        db.RunNonQuery("INSERT INTO PriceList(ID,PriceList_ID,Price,QTY,CREATEDBY,CREAREDDATE) VALUES ((select coalesce(max(ID), '0')+1 from PriceList ),'" + temp[1].ToString() + "','" + temp[4].ToString().Trim() + "','" + items_qty.Trim() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    }
                    else
                    {
                        db.RunReader("UPDATE PriceList SET QTY=coalesce(QTY, 0)+'" + items_qty.Trim() + "' WHERE PriceList_ID='" + temp[1].ToString() + "' AND Price='" + temp[4].ToString().Trim() + "'");
                    }

                }
                System.Windows.Forms.MessageBox.Show("تم الحفظ"); Button_Click_20(sender, e);
            }
        }

        private void cbx_typ_items8_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items8.ItemsSource = db.RunReader("select CLIENT_ID ,NAME from CLIENT where client_type='C' and   (CLIENT_ID =  '" + txtAName73.Text.Trim() + "' or NAME   like  '%" + txtAName73.Text.Trim() + "%'  or MOBILE =  '" + txtAName73.Text.Trim() + "' or PHONE  like  '" + txtAName73.Text.Trim() + "') ").DefaultView;
        }

        private void txtAName73_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbx_typ_items8.SelectedIndex = -1;
                cbx_typ_items8.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbx_typ_items8.SelectedIndex = -1;
                cbx_typ_items8.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items8_DropDownClosed(object sender, EventArgs e)
        {
            txtAName75.Focus();
            DataTable dt = db.RunReader("select CLIENT_ID ,NAME,PHONE,MOBILE,ADDRESS from CLIENT where client_type='C' and   CLIENT_ID =  '" + cbx_typ_items8.Text.Trim() + "'");
            if (dt.Rows.Count > 0)
            {
                txtAName70.Text = dt.Rows[0][1].ToString();
                txtAName69.Text = dt.Rows[0][2].ToString();
                txtAName72.Text = dt.Rows[0][3].ToString();
                txtAName71.Text = dt.Rows[0][4].ToString();
                txtAName81.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + cbx_typ_items8.Text.Trim() + "' and  client_type='C'").Rows[0][0].ToString();
            }
        }

        private void txtAName75_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // cbx_typ_items.IsFocused = ;
                cbx_typ_items6.SelectedIndex = -1;
                cbx_typ_items6.IsDropDownOpen = true;
                cbx_typ_items6.Focus();

                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // cbx_typ_items.IsFocused = ;
                cbx_typ_items6.SelectedIndex = -1;
                cbx_typ_items6.IsDropDownOpen = true;

                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items6_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items6.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,case when  TYPE=1 then 'حريمي' else 'رجالي'  END TYPE from  PRODUCT_CATEGORY  where  PRODUCT_CATEGORY_ID like '" + txtAName75.Text.Trim() + "'  or NAME like '%" + txtAName75.Text.Trim() + "%'").DefaultView;
        }

        private void cbx_typ_items7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //cbx_typ_items7.SelectedIndex = -1;
                //cbx_typ_items7.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);
                txtAName66.Focus();

            }
        }

        private void cbx_typ_items7_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items7.ItemsSource = db.RunReader("select   PRODUCT_ID ,NAME from  PRODUCT  where  PRODUCT_CATEGORY_ID like '" + ((cbx_typ_items6.Text == string.Empty) ? "%%" : cbx_typ_items6.Text).ToString() + "' and (PRODUCT_ID like '" + txtAName76.Text.Trim() + "' or  NAME like '%" + txtAName76.Text.Trim() + "%')").DefaultView;
        }
        DataTable dt_pay_pay = new DataTable();
        private void cbx_typ_items7_DropDownClosed(object sender, EventArgs e)
        {
            txtAName66.Focus();
            
            DataTable dt = db.RunReader("select    PRODUCT_CATEGORY_ID,Package_Unit_Price from  PRODUCT  where PRODUCT_ID='" + cbx_typ_items7.Text + "'  ");
            if (cbx_typ_items7.SelectedIndex > -1 && cbx_typ_items6.Text.Trim() == string.Empty)
            {

                cbx_typ_items6.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,case when  TYPE=1 then 'حريمي' else 'رجالي'  END TYPE from  PRODUCT_CATEGORY  ").DefaultView;
               
                if (dt.Rows.Count > 0)
                {
                    cbx_typ_items6.Text = dt.Rows[0][0].ToString();
                }

            }
            if (cbx_typ_items7.SelectedIndex > -1)
            {
                if (dt_pay_pay == null)
                    dt_pay_pay = new DataTable();
                foreach (DataRow item in dt_pay_pay.Rows)
                {
                    if (item[1].ToString() == cbx_typ_items7.Text)
                    {
                        if (System.Windows.Forms.MessageBox.Show("تم اضافة هذا العنصر هل تريد تعديل الكمية او السعر", "", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string qy = item[3].ToString();
                            string d = item[4].ToString();
                            string c = item[6].ToString();

                            dt_pay_pay.Rows.Remove(item);
                            txtAName66.Text = qy;
                            txtAName78.Text = d;
                            txtAName67.Text = c;


                        }
                        else
                        {
                            cbx_typ_items7.Text = string.Empty;
                        }


                        break;

                    }
                }
            }
        }
        void countquantatydd()
        {
            double quntaty = 0, ponas = 0;
            if (cbx_typ_items7.SelectedIndex < 0)
            {
                txtAName66.Text = "0";
                txtAName67.Text = "0";
                return;
            }
            txtAName66.Text = (double.TryParse(txtAName66.Text.Trim(), out quntaty) == false) ? "0" : txtAName66.Text;
            txtAName67.Text = (double.TryParse(txtAName67.Text.Trim(), out ponas) == false) ? "0" : txtAName67.Text;
            //update PRODUCT set QTY = coalesce(QTY, 0) + '" + items_qty.Trim() + "', PURCHASE_UNIT_PRICE = '" + temp[4].ToString().Trim() + "' where PRODUCT_ID = '" + temp[1].ToString() + "'
            double qu_items = Convert.ToDouble(db.RunReader("select coalesce(QTY, 0),coalesce(Minqty,0) from PRODUCT where PRODUCT_ID = '" + cbx_typ_items7.Text + "' ").Rows[0][0].ToString());
            double Minqty = Convert.ToDouble(db.RunReader("select coalesce(QTY, 0),coalesce(Minqty,0) from PRODUCT where PRODUCT_ID = '" + cbx_typ_items7.Text + "' ").Rows[0][1].ToString());
            if (qu_items <= (quntaty+ ponas+ Minqty))
            {
                System.Windows.Forms.MessageBox.Show("الكمية اوشكت على النفاذ");
              

            }
        }
        private void txtAName66_LostFocus(object sender, RoutedEventArgs e)
        {
            double quntaty = 0;
            if (double.TryParse(txtAName66.Text.Trim(), out quntaty) == false)
                txtAName66.Text = "0";
            double quntaty2 = 0;
            if (double.TryParse(txtAName67.Text.Trim(), out quntaty2) == false)
                txtAName67.Text = "0";
            countquantatydd();

        }
        void countquantaty()
        {
            double quntaty = 0, ponas = 0;
            if (cbx_typ_items7.SelectedIndex < 0)
            {
                txtAName66.Text = "0";
                txtAName67.Text = "0";
                return;
            }
            txtAName66.Text = (double.TryParse(txtAName66.Text.Trim(), out quntaty) == false) ? "0" : txtAName66.Text;
            txtAName67.Text = (double.TryParse(txtAName67.Text.Trim(), out ponas) == false) ? "0" : txtAName67.Text;
            //update PRODUCT set QTY = coalesce(QTY, 0) + '" + items_qty.Trim() + "', PURCHASE_UNIT_PRICE = '" + temp[4].ToString().Trim() + "' where PRODUCT_ID = '" + temp[1].ToString() + "'
            double qu_items = Convert.ToDouble(db.RunReader("select coalesce(QTY, 0) from PRODUCT where PRODUCT_ID = '" + cbx_typ_items7.Text + "' ").Rows[0][0].ToString());
            if (qu_items < quntaty)
            {
                System.Windows.Forms.MessageBox.Show("الكمية في الخزن لا تسمح");
                txtAName66.Text = qu_items.ToString();
                txtAName67.Text = "0";

            }
            //else if (qu_items < (quntaty + ponas))
            //{
            //    System.Windows.Forms.MessageBox.Show("الكمية في الخزن لا تسمح");

            //    txtAName67.Text = (qu_items - quntaty).ToString();
            //}
        }
        private void txtAName66_TextChanged(object sender, TextChangedEventArgs e)
        {
            countquantaty();
            double quntaty = 0, price = 0,dis=0;
            if (txtAName67.Text == string.Empty)
                txtAName67.Text = "0";
            double.TryParse(txtAName67.Text.Trim(), out dis);
            if (double.TryParse(txtAName66.Text.Trim(), out quntaty) == false || double.TryParse(txtAName78.Text.Trim(), out price) == false)
            { txtAName55.Text = ""; return; }
            txtAName68.Text = (quntaty * price-dis).ToString();
            if(quntaty * price - dis<0)
            {
                txtAName67.Text = (quntaty * price).ToString();
            }

        }

        private void txtAName68_TextChanged(object sender, TextChangedEventArgs e)
        {
            double sum = 0, sumline = 0;

            sumline = double.TryParse(txtAName68.Text.Trim(), out sumline) ? sumline : 0;
            if (dg_items_Copy3.Items.Count == 0)
            {
                dt_pay_pay = null;
            }
            else
            {
                foreach (DataRow item in dt_pay_pay.Rows)
                {
                    sum = sum + Convert.ToDouble(item[5].ToString());
                }
            }
            txtAName77.Text = sum.ToString();
            txtAName74.Text = (sum + sumline).ToString();
        }

        private void txtAName67_LostFocus(object sender, RoutedEventArgs e)
        {
            double quntaty = 0;
            if (double.TryParse(txtAName67.Text.Trim(), out quntaty) == false)
                txtAName67.Text = "0";
            countquantatydd();
        }

        private void txtAName78_LostFocus(object sender, RoutedEventArgs e)
        {
            double quntaty = 0;
            if (double.TryParse(txtAName78.Text.Trim(), out quntaty) == false)
                txtAName78.Text = "";
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            User.CleanAll(g_shra_Copy);
            dt_pay_pay = null;
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            if (dg_items_Copy3.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dg_items_Copy3.SelectedItem;


                foreach (DataRow item in dt_pay_pay.Rows)
                {
                    if (item[1].ToString() == row[1].ToString())
                    {
                        dt_pay_pay.Rows.Remove(item);

                        break;

                    }
                }

            }
            txtAName68_TextChanged(sender, et);
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbx_typ_items6.Text.Trim() == string.Empty || cbx_typ_items7.Text.Trim() == string.Empty || txtAName66.Text.Trim() == string.Empty || txtAName78.Text.Trim() == string.Empty || Convert.ToDouble(txtAName66.Text) == 0 || Convert.ToDouble(txtAName78.Text) == 0 )
                {
                    System.Windows.Forms.MessageBox.Show("اكمل البانات"); return;
                }
                if (dg_items_Copy3.ItemsSource == null || dt_pay_pay == null)
                {
                    dt_pay_pay = new DataTable();
                    dt_pay_pay.Columns.Add("النوع");
                    dt_pay_pay.Columns.Add("كود الصنف");
                    dt_pay_pay.Columns.Add("الصنف");
                    dt_pay_pay.Columns.Add("الكمية");
                    dt_pay_pay.Columns.Add("السعر");
                    dt_pay_pay.Columns.Add("الاجمالي");
                    dt_pay_pay.Columns.Add("خصم");
                }
                string nametepy = db.RunReader("select  NAME from PRODUCT_CATEGORY  where PRODUCT_CATEGORY_ID='" + cbx_typ_items6.Text + "' ").Rows[0][0].ToString();
                string nameitem = db.RunReader("select  NAME from  PRODUCT where  PRODUCT_ID='" + cbx_typ_items7.Text + "'").Rows[0][0].ToString();
                dt_pay_pay.Rows.Add(nametepy, cbx_typ_items7.Text, nameitem, txtAName66.Text, txtAName78.Text, txtAName68.Text, txtAName67.Text);

                dg_items_Copy3.ItemsSource = dt_pay_pay.DefaultView;
                txtAName75.Text = txtAName76.Text = cbx_typ_items6.Text = cbx_typ_items7.Text = txtAName66.Text = txtAName67.Text = txtAName68.Text = txtAName78.Text = string.Empty;
                txtAName66_TextChanged(sender, et);
                txtAName75.Focus();
            }
            catch { }
        }

        private void txtAName79_TextChanged(object sender, TextChangedEventArgs e)
        {
            double total = 0, pay = 0;
            if (double.TryParse(txtAName79.Text, out pay) == false)
            {
                txtAName79.Text = "0";
                txtAName80.Text = txtAName77.Text;
            }
            else if (double.TryParse(txtAName77.Text, out total))
            {
                if (total < pay)
                {
                    pay = total;
                    txtAName79.Text = pay.ToString();
                }
                txtAName80.Text = (total - pay).ToString();
            }
        }

        private void txtAName67_TextChanged(object sender, TextChangedEventArgs e)
        {
            countquantaty();
        }

        private void txtAName66_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
                txtAName78.Focus();
        }

        private void cbx_typ_items6_DropDownClosed(object sender, EventArgs e)
        {
            txtAName76.Focus();
        }

        private void txtAName78_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtAName67.Focus();
        }

        private void txtAName67_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fffff.Focus();
        }

        private void txtAName76_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbx_typ_items7.SelectedIndex = -1;
                cbx_typ_items7.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);
                cbx_typ_items7.Focus();
            }
        }

        private void txtAName62_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // cbx_typ_items.IsFocused = ;
                cbx_typ_items.SelectedIndex = -1;
                cbx_typ_items.IsDropDownOpen = true;
                cbx_typ_items.Focus();
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items_DropDownClosed(object sender, EventArgs e)
        {
            txtAName63.Focus();
        }

        private void txtAName63_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbx_typ_items4.SelectedIndex = -1;
                cbx_typ_items4.IsDropDownOpen = true;
                cbx_typ_items4.Focus();
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void txtAName58_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtAName54.Focus();
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            if (dg_items_Copy3.Items.Count == 0)
            { return; }

            DataTable dt = db.RunReader("select CLIENT_ID, NAME , MOBILE,PHONE ,ADDRESS ,CREATEDBY ,CREATEDDATE from CLIENT where  CLIENT_ID='" + cbx_typ_items8.Text + "' and client_type='C'");
            //if (dt.Rows.Count <= 0)
            //{

            //    MessageBox.Show("برجاء اختيار العميل");
            //    return;
            //}

            try
            {

                txtAName80.Text = (Convert.ToDouble(txtAName79.Text) - Convert.ToDouble(txtAName77.Text)).ToString();
                string INVOICE_ID = db.RunReader("select coalesce(max(INVOICE_ID), '0')+1 from INVOICE").Rows[0][0].ToString();
                db.RunNonQuery(@"insert into INVOICE (INVOICE_ID                  ,DATEINVOICED,TIMEINVOICED                                   ,TOTAL           ,PAID,INVOICE_TYPE,USER_ID   ,CREATEDBY, CREATEDDATE                                                        ,INVOICE_STATUS       ,CLIENT_ID                 ,discountpct              ,totalafterdisount ,TaxAmt                   ,charge)
                values ('" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "','" + txtAName77.Text + "','0','Allocation','" + User.Code + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','inprogress','" + cbx_typ_items8.Text + "'        ,'0'                  ,'" + txtAName77.Text + "','0'            ,'" + txtAName80.Text + "')");
                if (txtAName80.Text != "0" && dt.Rows.Count > 0)
                    db.RunNonQuery("INSERT INTO Allocation (Allocation_id,INVOICE_ID,AllocationDate,AllocarionAmt,client_id,CREATEDBY,CREATEDDATE,client_type) values ((SELECT coalesce( max ( Allocation_id ),0)+1 from Allocation ),'" + INVOICE_ID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',-1*'" + txtAName80.Text + "','" + cbx_typ_items8.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','C')");


                foreach (DataRow temp in dt_pay_pay.Rows)
                {
                    string TotalQTY = temp[3].ToString() ;




                    List<produ> ts = getitems(Convert.ToInt32(temp[1].ToString()), Convert.ToInt32(TotalQTY));


                    db.RunNonQuery(@"update PRODUCT set QTY=QTY-'" + TotalQTY + "' where PRODUCT_ID='" + temp[1].ToString() + "'");


                    if (ts.Count <= 0)
                    {
                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID          ,PRODUCT_ID            ,QTY                                  ,TOTALLINE,CREATEDBY,CREATEDDATE                                                               ,Sales_Unit_price        ,Purchase_Unit_price,DiscountPctLine,ProductType,Bonus) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE )    ,'" + INVOICE_ID + "','" + temp[1].ToString() + "','" + temp[3].ToString() + "','" + temp[5].ToString() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp[4].ToString() + "','0'            ,'" + temp[6].ToString() + "','package','0')");

                    }
                    else
                    {
                        Double total_price_Shera = 0, Total_Count = 0, AVG_price_shera = 0;
                        foreach (produ it in ts)
                        {
                            total_price_Shera += Convert.ToDouble(it.price);
                            Total_Count += Convert.ToDouble(it.number);

                        }

                        AVG_price_shera = total_price_Shera / Total_Count;

                        db.RunNonQuery(@"insert into INVOICE_LINE  (INVOICE_LINE_ID,INVOICE_ID,PRODUCT_ID                  ,QTY                  ,TOTALLINE,CREATEDBY,CREATEDDATE                                                                          ,Sales_Unit_price            ,Purchase_Unit_price     ,DiscountPctLine           ,ProductType,Bonus) 
values ((select coalesce(max(INVOICE_LINE_ID), '0')+1 from INVOICE_LINE ),'" + INVOICE_ID + "','" + temp[1].ToString() + "','" + temp[3].ToString() + "','" + temp[5].ToString() + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + temp[4].ToString() + "','" + AVG_price_shera + "','0','package','" + temp[6].ToString() + "')");

                    }

                }



                MessageBox.Show("تم عملية ");
                Button_Click_23(sender, e);
                //txtAName28.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + cbx_typ_items8.Text.Trim() + "' and  client_type='C'").Rows[0][0].ToString();
                //txtAName27.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + cbx_typ_items8.Text.Trim() + "' AND AllocarionAmt>0 and client_type='C'").Rows[0][0].ToString();
                //txtAName26.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + cbx_typ_items8.Text.Trim() + "' AND AllocarionAmt<0 and client_type='C'").Rows[0][0].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtAName35_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbx_typ_items9.SelectedIndex = -1;
                cbx_typ_items9.IsDropDownOpen = true;
                //  cbx_typ_items_DropDownOpened(sender, e);

            }
        }

        private void cbx_typ_items9_DropDownClosed(object sender, EventArgs e)
        {
            txtAName39.Focus();
            DataTable dt = db.RunReader("select CLIENT_ID ,NAME,PHONE,MOBILE,ADDRESS from CLIENT where client_type='C' and   CLIENT_ID =  '" + cbx_typ_items9.Text.Trim() + "'");
            if (dt.Rows.Count > 0)
            {
                txtAName32.Text = dt.Rows[0][1].ToString();
                txtAName31.Text = dt.Rows[0][2].ToString();
                txtAName34.Text = dt.Rows[0][3].ToString();
                txtAName33.Text = dt.Rows[0][4].ToString();
                txtAName36.Text = db.RunReader("SELECT coalesce( sum( AllocarionAmt ),0) from Allocation where client_id='" + cbx_typ_items9.Text.Trim() + "' and  client_type='C'").Rows[0][0].ToString();
            }
        }

        private void cbx_typ_items9_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items9.ItemsSource = db.RunReader("select CLIENT_ID ,NAME from CLIENT where client_type='C' and   (CLIENT_ID =  '" + txtAName35.Text.Trim() + "' or NAME   like  '%" + txtAName35.Text.Trim() + "%'  or MOBILE =  '" + txtAName35.Text.Trim() + "' or PHONE  like  '" + txtAName35.Text.Trim() + "') ").DefaultView;
        }

        private void txtAName39_TextChanged(object sender, TextChangedEventArgs e)
        { double d=0, s=0;
            if (double.TryParse(txtAName39.Text, out d) == false || double.TryParse(txtAName36.Text, out s) == false || txtAName36.Text == string.Empty)
            {
                return;
            }
            if(d>s)
            {
                txtAName39.Text = txtAName36.Text;
            }
        }

        private void cbx_typ_items6_LostFocus(object sender, RoutedEventArgs e)
        {
            cbx_typ_items7.Text = "";
        }

        private void cbx_typ_items_LostFocus(object sender, RoutedEventArgs e)
        {
            cbx_typ_items4.Text = "";
        }
    }
}
