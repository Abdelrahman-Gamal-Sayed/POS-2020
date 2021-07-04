using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class BasicFRM : Window
    {

        DataTable test = new DataTable();
        public BasicFRM()
        {
            InitializeComponent();
            clearGrids();
            checkouthotry();
        }
        private void checkouthotry() {

            //if(!( User.main_form == "Y" || User.main_form == "y" ))
            //    btn_main_form.Visibility = Visibility.Collapsed;
            if(!( User.casher_form == "Y" || User.casher_form == "y" ))
                btn_casher_form.Visibility = Visibility.Collapsed;
            //if(!( User.kitchen_form == "Y" || User.kitchen_form == "y" ))
            //    btn_kitchen_form.Visibility = Visibility.Collapsed;
            if(!( User.report_form == "Y" || User.report_form == "y" ))
                btn_report_form.Visibility = Visibility.Collapsed;
            //if(!( User.view_form == "Y" || User.view_form == "y" ))
            //    btn_view_form.Visibility = Visibility.Collapsed;


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
        void clearGrids()
        {
            g_employee.Visibility = Visibility.Collapsed;
            g_employee_essorty.Visibility = Visibility.Collapsed;
            g_typ_food.Visibility = Visibility.Collapsed;
            g_typ_food_Copy.Visibility = Visibility.Collapsed;
            g_Product_tarkeeb.Visibility = Visibility.Collapsed;
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
                    g_employee.Visibility = Visibility.Visible;
                   
                   
                    cbx_wazfa_emp.ItemsSource = db.RunReader("select * from  JOB ").DefaultView;
                    break;
                case 2:

                    g_employee_essorty.Visibility = Visibility.Visible;
                  
                    
                    cbx_osserty_user.ItemsSource = db.RunReader("select USER_ACCESS_ID,ACCESS_TYPE from  USER_ACCESS ").DefaultView;
                    break;
                case 3:
                    g_typ_food.Visibility = Visibility.Visible;


                    cbx_typ_items.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,case when  TYPE=1 then 'حريمي' else 'رجالي'  END TYPE from  PRODUCT_CATEGORY ").DefaultView;
                    break;
                case 4:
                    g_typ_food_Copy.Visibility = Visibility.Visible;


                    DataTable dt = db.RunReader("select NAME,PHONE,PHONE2 from COMPANY");
                    if(dt.Rows.Count!=0)
                    {
                        txt_name_items2.Text = dt.Rows[0][0].ToString();
                        txt_phon_emp1.Text = dt.Rows[0][1].ToString();
                        txt_phon_emp2.Text = dt.Rows[0][2].ToString();

                        string filePath = Environment.CurrentDirectory + "\\items";
                        filePath = filePath + "\\" + txt_name_items2.Text + ".jpg";
                        if (File.Exists(filePath))
                            mage_item_phtoo1.Source = new BitmapImage(new Uri(filePath));
                        else
                            mage_item_phtoo1.Source = null;
                    }
                    break;

                case 5:
                
                    g_Product_tarkeeb.Visibility = Visibility.Visible;
                    cbx_typ_items6.ItemsSource = db.RunReader("select PRODUCT_ID,name from PRODUCT where Type ='ItemTarkeb'").DefaultView;
                    cbx_typ_items7.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID,NAME from PRODUCT_CATEGORY").DefaultView;
                    break;

                default:
                    break;
            }
        }

   

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            g_emp_data.CleanAll();
           
        }
        DB db = new DB();
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            g_emp_data_main.CleanAll();
          
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            g_essort_date.CleanAll();
          
           


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            g_essort_date_main.CleanAll();
           
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            g_items_data.CleanAll();
            mage_item_phtoo.Source = null;
          
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            g_items_data_main.CleanAll();
            mage_item_main_phtoo.Source = null; //select coalesce(max(PRODUCT_CATEGORY_ID) + 1, '1')  from PRODUCT_CATEGORY
           
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (txt_wazefa_name_main_emp1.Text.Trim() == string.Empty)
                MessageBox.Show("اكمل البيانات");
            else
            {
                if (db.RunReader(" select JOB_ID from JOB where JOB_ID='" + txt_wazefa_code_main_emp1.Text + "' ").Rows.Count == 0)
                {
                    txt_wazefa_code_main_emp1.Text = db.RunReader(" select coalesce(max(JOB_ID), '0')+1 from JOB ").Rows[0][0].ToString();
                    db.RunNonQuery("INSERT  INTO   JOB   VALUES ('" + txt_wazefa_code_main_emp1.Text + "','" + txt_wazefa_name_main_emp1.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "تم الحفظ");

                }
                else
                    db.RunNonQuery("UPDATE JOB SET NAME='" + txt_wazefa_name_main_emp1.Text + "'  WHERE JOB_ID='" + txt_wazefa_code_main_emp1.Text + "' ", "تم الحفظ");
            }
            cbx_wazfa_emp.ItemsSource = db.RunReader("select * from  JOB ").DefaultView;
        }

        private void txt_typ_emp_main_seach_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_typ_emp_main.ItemsSource = db.RunReader("select JOB_ID as 'الكود' ,NAME as 'الاسم'  from  JOB  WHERE JOB_ID like '%" + txt_typ_emp_main_seach.Text + "%' or NAME like '%" + txt_typ_emp_main_seach.Text + "%'").DefaultView;
        }

        private void dg_typ_emp_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_typ_emp_main.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_typ_emp_main.SelectedItem;
                    txt_wazefa_code_main_emp1.Text = row[0].ToString();
                    txt_wazefa_name_main_emp1.Text = row[1].ToString();
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (txt_salary_emp.Text.Trim() == string.Empty ||  txt_name_emp.Text.Trim() == string.Empty || txt_phon_emp.Text.Trim() == string.Empty || txt_adders_emp.Text.Trim() == string.Empty || cbx_wazfa_emp.Text.Trim() == string.Empty || txt_id_emp.Text.Trim() == string.Empty )
                MessageBox.Show("اكمل البيانات");
            else
            {


                string gender = (rbMale_emp.IsChecked == true) ? "ذكر" : "انثي";
                if (db.RunReader(" select EMPLOYEE_ID  from EMPLOYEE where EMPLOYEE_ID='" + txt_code_emp.Text + "' ").Rows.Count == 0)

                {
                    txt_code_emp.Text = db.RunReader(" select coalesce(max(EMPLOYEE_ID), '0')+1 from EMPLOYEE  ").Rows[0][0].ToString();
                    db.RunNonQuery(@"INSERT  INTO   EMPLOYEE (EMPLOYEE_ID   , NAME                            , NATIONAL_ID           , DATE_HIRE                                           , MOBILE                   , SALARY,                       GENDER_CODE                                                                                 , JOB_ID              , CREATEDBY           , CREATEDDATE  ,ADDRESS       ) 
                                            VALUES ('" + txt_code_emp.Text + "','" + txt_name_emp.Text + "','" + txt_id_emp.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txt_phon_emp.Text + "','" + txt_salary_emp.Text + "','" + gender + "','" + cbx_wazfa_emp.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txt_adders_emp.Text + "')", "تم الحفظ");
                }
                else
                    db.RunNonQuery(@"UPDATE EMPLOYEE  SET NAME='" + txt_name_emp.Text + "' ,NATIONAL_ID= '" + txt_id_emp.Text + "',MOBILE ='" + txt_phon_emp.Text + "' ,SALARY ='" + txt_salary_emp.Text + "'  ,GENDER_CODE ='" + gender + "',JOB_ID  ='" + cbx_wazfa_emp.Text + "'  ,ADDRESS='" + txt_adders_emp.Text + "'    WHERE EMPLOYEE_ID='" + txt_code_emp.Text + "' ", "تم الحفظ");
            }
        }

        private void txt_search_emp_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_emp.ItemsSource = db.RunReader("select EMPLOYEE_ID as كود__الموظف  ,NAME الاسم , MOBILE     as   'الموبيل'      , NATIONAL_ID    as 'رقم البطاقة'     , date(DATE_HIRE )   as 'تاريخ التعين'        , SALARY     as 'السعر'         , GENDER_CODE      as 'النوع'   , date(DATE_BIRTH )     as 'تاريخ الميلاد'    , JOB_ID      as 'كود الوظيفة'        , CREATEDBY  as     اسم__المدخل      ,  CREATEDDATE as تاريخ__الادخال   ,ADDRESS as العنوان       from  EMPLOYEE  WHERE EMPLOYEE_ID like '%" + txt_search_emp.Text + "%' or NAME like '%" + txt_search_emp.Text + "%' ").DefaultView;
        }

        private void dg_emp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_emp.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_emp.SelectedItem;
                    txt_code_emp.Text = row[0].ToString();
                    txt_name_emp.Text = row[1].ToString();
                    txt_id_emp.Text = row[1].ToString();
                    txt_phon_emp.Text = row[2].ToString();
                    txt_id_emp.Text = row[3].ToString();

                    txt_salary_emp.Text = row[5].ToString();
                    if (row[6].ToString() == "ذكر")
                        rbMale_emp.IsChecked = true;
                    else rbMale_emp.IsChecked = false;

                   // txt_parcedate_emp.Text = row[7].ToString();
                    cbx_wazfa_emp.Text = row[8].ToString();
                    txt_adders_emp.Text = row[11].ToString();

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    mage_item_phtoo.Source = new BitmapImage(new Uri(op.FileName));
                    //path2 = op.FileName;

                    //  MessageBox.Show(path2);
                    //   System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(path2);

                }




            }
            catch { }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
           

            if (
                txt_name_items.Text.Trim() == string.Empty ||
                cbx_typ_items.Text.Trim() == string.Empty
               || txt_price_items.Text.Trim() == string.Empty
              //|| txt_price_items_at3y.Text.Trim() == string.Empty
              // || txt_price_items1.Text.Trim() == string.Empty
              //|| txt_price_items2.Text.Trim() == string.Empty
              // || txt_price_items3.Text.Trim() == string.Empty
              //  || txt_price_items.Text.Trim() == string.Empty
              //  || cbx_typ_items2.Text.Trim() == string.Empty
              //  || cbx_typ_items1.Text.Trim() == string.Empty
              ||  txt_price_items4.Text.Trim() == string.Empty
                    )
                MessageBox.Show("اكمل البيانات");
            else
            {
                string Type = CBType.IsChecked != true ? "NORMAL" : "ItemTarkeb";

                if (db.RunReader(" select PRODUCT_ID  from PRODUCT  where PRODUCT_ID='" + txt_code_items.Text + " ' or NAME = '" + txt_name_items.Text + "'  ").Rows.Count == 0  )
                {

                    if (txt_code_items.Text.Trim() == "")
                        txt_code_items.Text = new Random().NextULong(1000000, 999999999999).ToString();
                    db.RunNonQuery(@"INSERT  INTO   PRODUCT (PRODUCT_ID                , NAME,                    PRODUCT_CATEGORY_ID       ,      SALES_UNIT_PRICE    ,     CREATEDBY                     , CREATEDDATE                               ,Package_Unit_Price                   ,Delay_Unit_price,                Package_size              ,UOM_ID                      ,UOM_PART_ID                 ,ProductDiscountPCT   ,Minqty   ,COLOR  ,Type    ) 
                                            VALUES ('" + txt_code_items.Text + "','" + txt_name_items.Text+  "','" + cbx_typ_items.Text + "','" + txt_price_items_at3y.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" +  txt_price_items.Text + "','"+ txt_price_items2 .Text+ "','"+ txt_price_items1 .Text+ "','"+ cbx_typ_items2 .Text+ "','"+ cbx_typ_items1 .Text+ "','"+ txt_price_items3 .Text+ "','"+ txt_price_items4 .Text+ "','"+ cbx_typ_items4 .Text+ "','"+ Type + "')", "تم الحفظ");
                   // txt_code_items.Text = "";
                    cbx_typ_items4.Text = "";
                }
                else if(txt_code_items.Text!="")
                    db.RunNonQuery(@"UPDATE PRODUCT  SET Type='"+ Type + "', NAME='" + txt_name_items.Text + "' ,PRODUCT_CATEGORY_ID= '" + cbx_typ_items.Text + "' ,SALES_UNIT_PRICE ='" + txt_price_items_at3y.Text + "' ,Package_Unit_Price= '" + txt_price_items.Text + "' ,Delay_Unit_price= '" + txt_price_items2.Text + "', Package_size= '" + txt_price_items1.Text + "',UOM_ID ='" + cbx_typ_items2.Text + "',COLOR='"+ cbx_typ_items4 .Text+ "',  UOM_PART_ID='" + cbx_typ_items1.Text + "' ,ProductDiscountPCT = '" + txt_price_items3.Text + "',Minqty='" + txt_price_items4.Text + "' WHERE PRODUCT_ID='" + txt_code_items.Text + "' ", "تم الحفظ");
            }
            if (mage_item_phtoo.Source != null)
            {
                try
                {
                    string filePath = Environment.CurrentDirectory + "\\items";
                    Directory.CreateDirectory(filePath);
                    filePath = filePath + "\\" + txt_code_items.Text + ".jpg";
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)mage_item_phtoo.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);
                }
                catch { }
            }
            txt_search_items_SelectionChanged(sender, e);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            try
            {

                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    mage_item_main_phtoo.Source = new BitmapImage(new Uri(op.FileName));
                    //path2 = op.FileName;

                    //  MessageBox.Show(path2);
                    //   System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(path2);

                }




            }
            catch { }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            cbx_typ_items3.SelectedIndex = 0;
            if (txt_typ_items_name.Text.Trim() == string.Empty )
                MessageBox.Show("اكمل البيانات");
            else
            {
                if (db.RunReader(" select PRODUCT_CATEGORY_ID  from PRODUCT_CATEGORY  where PRODUCT_CATEGORY_ID ='" + txt_typ_items_main.Text + "' ").Rows.Count == 0)
                {
                    txt_typ_items_main.Text = db.RunReader(" select * from PRODUCT_CATEGORY  ").Rows.Count.ToString();
                    db.RunNonQuery("INSERT  INTO   PRODUCT_CATEGORY    VALUES ('" + txt_typ_items_main.Text + "','" + txt_typ_items_name.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,'"+ cbx_typ_items3 .SelectedIndex+ "')", "تم الحفظ");
                }
                else
                    db.RunNonQuery("UPDATE PRODUCT_CATEGORY  SET NAME='" + txt_typ_items_name.Text + "' ,TYPE='"+ cbx_typ_items3 .SelectedIndex+ "'  WHERE PRODUCT_CATEGORY_ID='" + txt_typ_items_main.Text + "' ", "تم الحفظ");
            }
            cbx_typ_items.ItemsSource = db.RunReader("select PRODUCT_CATEGORY_ID ,NAME,CASE WHEN  TYPE=1 THEN 'حريمي' ELSE 'رجالي' END TYPE from  PRODUCT_CATEGORY  ").DefaultView;
            if (mage_item_main_phtoo.Source != null)
            {
                try
                {
                    string filePath = Environment.CurrentDirectory + "\\gropitems";
                    Directory.CreateDirectory(filePath);
                    filePath = filePath + "\\" + txt_typ_items_main.Text + ".jpg";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)mage_item_main_phtoo.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);
                }
                catch { }
            }
        }

        private void txt_typ_items_main_seach_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  db.RunNonQuery("CREATE TABLE PRODUCT_CATEGORY (PRODUCT_CATEGORY_ID INT,  NAME                 TEXT,  CREATEDBY            TEXT,  CREATEDDATE          TEXT,PRIMARY KEY(" + "PRODUCT_CATEGORY_ID" + ")); " ,"ddd");
            dg_typ_items_main.ItemsSource = db.RunReader("select    PRODUCT_CATEGORY_ID as 'الكود', NAME as 'الاسم'      from  PRODUCT_CATEGORY  WHERE PRODUCT_CATEGORY_ID like '%" + txt_typ_items_main_seach.Text + "%' or NAME like '%" + txt_typ_items_main_seach.Text + "%' ").DefaultView;
        }

        private void dg_typ_items_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_typ_items_main.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_typ_items_main.SelectedItem;
                    txt_typ_items_main.Text = row[0].ToString();
                    txt_typ_items_name.Text = row[1].ToString();
                    cbx_typ_items3.SelectedIndex = 0;//(row[2].ToString() == string.Empty) ? -1 : Convert.ToInt32( row[2].ToString());
                    string filePath = Environment.CurrentDirectory + "\\gropitems";


                    filePath = filePath + "\\" + txt_typ_items_main.Text + ".jpg";
                    if (File.Exists(filePath))
                        mage_item_main_phtoo.Source = new BitmapImage(new Uri(filePath));
                    else
                        mage_item_main_phtoo.Source = null;

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void txt_search_items_SelectionChanged(object sender, RoutedEventArgs e)
        {
            dg_items.ItemsSource = db.RunReader(@" select  PRODUCT_ID  as 'الكود', NAME as 'الاسم' 
                 ,Minqty as 'الحد الادنى'
 ,   CREATEDBY  as 'اسم المدخل', CREATEDDATE  as 'تاريخ الادخال' ,PURCHASE_UNIT_PRICE as 'اخر سعر شراء',Type as 'النوع' from  PRODUCT p 
where PRODUCT_ID like '%" + txt_search_items.Text + "%'  or  NAME like '%" + txt_search_items.Text + "%' ").DefaultView;
        }

        private void dg_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_items.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_items.SelectedItem;

                    txt_code_items.Text = row[0].ToString();
                    DataTable dt = db.RunReader(@" select  PRODUCT_ID  as 'الكود', NAME as 'الاسم',PRODUCT_CATEGORY_ID  as 'كود الصنف' 
                ,SALES_UNIT_PRICE as 'السعر'    ,Package_Unit_Price  as 'سعر الجملة',Delay_Unit_price as 'سعر الاجل',
              Package_size  as 'عدد الوحدات',UOM_ID      as 'كود واحدة الجملة',UOM_PART_ID    as 'كود واحدة'
,ProductDiscountPCT as 'نسبة الخصم' ,Minqty as 'الحد الادنى',COLOR AS 'اللون' ,   CREATEDBY  as 'اسم المدخل', CREATEDDATE  as 'تاريخ الادخال',Type as 'النوع' from  PRODUCT 
where PRODUCT_ID = '" + txt_code_items.Text + "'  ");
                    txt_name_items.Text = ( dt.Rows[0][1].ToString().Split('-').Length>1)? dt.Rows[0][1].ToString().Split('-')[0]: dt.Rows[0][1].ToString();
                    cbx_typ_items.Text = dt.Rows[0][2].ToString();
                    txt_price_items_at3y.Text = dt.Rows[0][3].ToString();
                    txt_price_items.Text = dt.Rows[0][4].ToString();
                    txt_price_items2.Text = dt.Rows[0][5].ToString();
                    txt_price_items1.Text = dt.Rows[0][6].ToString();
                    cbx_typ_items2_DropDownOpened(sender,e);
                    cbx_typ_items2.Text = dt.Rows[0][7].ToString();
                    cbx_typ_items1_DropDownOpened(sender, e);
                    cbx_typ_items1.Text = dt.Rows[0][8].ToString();
                    txt_price_items3.Text = dt.Rows[0][9].ToString();
                    txt_price_items4.Text = dt.Rows[0][10].ToString();
                    //cbx_typ_items4.Text = (dt.Rows[0][1].ToString().Split('-').Length > 1) ? dt.Rows[0][1].ToString().Split('-')[1] : "";
                    CBType.IsChecked = dt.Rows[0][14].ToString().ToUpper() == "NORMAL".ToUpper() ? false : true;
                    string filePath = Environment.CurrentDirectory + "\\items";


                    filePath = filePath + "\\" + txt_code_items.Text + ".jpg";
                    if (File.Exists(filePath))
                        mage_item_phtoo.Source = new BitmapImage(new Uri(filePath));
                    else
                        mage_item_phtoo.Source = null;

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (txt_occerty_name_main_user.Text.Trim() == string.Empty)
                MessageBox.Show("اكمل البيانات");
            else
            {
                string main_form = "N", casher_form = "N", report_form = "N", view_form = "N", kitchen_form = "N";
                if (chb_main.IsChecked == true)
                    main_form = "Y";

                if (chb_cacher.IsChecked == true)
                    casher_form = "Y";


                if (chb_report.IsChecked == true)
                    report_form = "Y";


                if (chb_view.IsChecked == true)
                    view_form = "Y";


                if (chb_katchen.IsChecked == true)
                    kitchen_form = "Y";

                if (db.RunReader(" select USER_ACCESS_ID from USER_ACCESS where USER_ACCESS_ID='" + txt_occerty_code_main_user.Text + "' ").Rows.Count == 0)
                {
                    txt_occerty_code_main_user.Text = db.RunReader(" select coalesce(max(USER_ACCESS_ID), '0')+1 from USER_ACCESS  ").Rows[0][0].ToString();
                    db.RunNonQuery(@"INSERT  INTO   USER_ACCESS  (USER_ACCESS_ID,              ACCESS_TYPE                               ,main_form,casher_form,          report_form,            view_form,kitchen_form,CREATEDBY,CREATEDDATE            )  
                                             VALUES ('" + txt_occerty_code_main_user.Text + "','" + txt_occerty_name_main_user.Text + "','" + main_form + "','" + casher_form + "','" + report_form + "','" + view_form + "','" + kitchen_form + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "تم الحفظ");
                }
                else
                    db.RunNonQuery("UPDATE USER_ACCESS SET ACCESS_TYPE ='" + txt_occerty_name_main_user.Text + "'  ,main_form = '" + main_form + "', casher_form = '" + casher_form + "', report_form ='" + report_form + "', view_form = '" + view_form + "', kitchen_form = '" + kitchen_form + "' WHERE USER_ACCESS_ID='" + txt_occerty_code_main_user.Text + "' ", "تم الحفظ");
            }
            cbx_osserty_user.ItemsSource = db.RunReader("select USER_ACCESS_ID,ACCESS_TYPE from  USER_ACCESS ").DefaultView;
        }

        private void txt_user_main_seach1_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_user_main1.ItemsSource = db.RunReader("select USER_ACCESS_ID as 'الكود',              ACCESS_TYPE    as 'الاسم'                           ,main_form as 'الشاشة الرئسية', casher_form as 'الكاشير',          report_form as 'التقارير',            view_form as 'العرض',kitchen_form as 'المطبخ',CREATEDBY as 'اسم المدخل',CREATEDDATE as 'تاريخ الادخال'     from  USER_ACCESS  WHERE USER_ACCESS_ID like '%" + txt_user_main_seach1.Text + "%' or ACCESS_TYPE     like '%" + txt_user_main_seach1.Text + "%'").DefaultView;
        }

        private void dg_user_main1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_user_main1.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_user_main1.SelectedItem;
                    txt_occerty_code_main_user.Text = row[0].ToString();
                    txt_occerty_name_main_user.Text = row[1].ToString();
                    chb_main.IsChecked = (row[2].ToString() == "Y") ? true : false;
                    chb_cacher.IsChecked = (row[3].ToString() == "Y") ? true : false;
                    chb_report.IsChecked = (row[4].ToString() == "Y") ? true : false;
                    chb_view.IsChecked = (row[5].ToString() == "Y") ? true : false;
                    chb_katchen.IsChecked = (row[6].ToString() == "Y") ? true : false;

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            if (cbx_code_emp.Text.Trim() == string.Empty
               || txt_name_user.Text.Trim() == string.Empty
                || txt_pass_user.Text.Trim() == string.Empty
                || cbx_osserty_user.Text.Trim() == string.Empty)
                MessageBox.Show("اكمل البيانات");
            else
            {
                if (txt_pass_user.Text == txt_re_pass_user.Text)
                {
                    if (db.RunReader(" select USER_ID from USER where USER_ID='" + txt_code_user.Text + "' ").Rows.Count == 0)
                    {
                        txt_code_user.Text = db.RunReader(" select coalesce(max(USER_ID), '0')+1 from USER  ").Rows[0][0].ToString();
                        db.RunNonQuery(@"INSERT  INTO   USER   (USER_ID                   ,EMPLOYEE_ID   ,              USER_PASSWORD             ,USER_NAME,                   ACCESS_ID                   ,CREATEDBY              ,CREATEDDATE            )  
                                             VALUES ('" + txt_code_user.Text + "','" + cbx_code_emp.Text + "','" + txt_pass_user.Text + "','" + txt_name_user.Text + "','" + cbx_osserty_user.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", "تم الحفظ");
                    }
                    else
                        db.RunNonQuery("UPDATE USER  SET EMPLOYEE_ID ='" + cbx_code_emp.Text + "'  ,USER_PASSWORD = '" + txt_pass_user.Text + "', USER_NAME = '" + txt_name_user.Text + "', ACCESS_ID ='" + cbx_osserty_user.Text + "'  WHERE USER_ID='" + txt_occerty_code_main_user.Text + "' ", "تم الحفظ");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("الرقم السري غير متطابق");
                }
            }
        }

        private void dg_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {try
            {
                if (dg_user.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_user.SelectedItem;
                    txt_code_user.Text = row[0].ToString();
                    cbx_code_emp.Text = row[1].ToString();
                    txt_pass_user.Text= txt_re_pass_user.Text = row[2].ToString();
                    txt_name_user.Text = row[3].ToString();
                    cbx_osserty_user.Text = row[4].ToString();
                  

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void txt_search_user_TextChanged(object sender, TextChangedEventArgs e)
        {
dg_user.ItemsSource = db.RunReader(@"select USER_ID as 'الكود',EMPLOYEE_ID as 'كود الموظف',  
                                USER_PASSWORD  as 'الرقم السري',           USER_NAME    as 'الاسم'                          
                                  ,ACCESS_ID as 'الصلاحية',CREATEDBY as 'اسم المدخل',CREATEDDATE as 'تاريخ الادخال'    
from  USER  WHERE USER_ID like '%" + txt_search_user.Text + "%' or USER_NAME     like '%" + txt_search_user.Text + "%'").DefaultView;
        }


        private void cbx_osserty_user_DropDownOpened(object sender, EventArgs e)
        {
            cbx_code_emp.ItemsSource = db.RunReader(" select EMPLOYEE_ID,NAME  from EMPLOYEE ").DefaultView;
        }

        private void txt_name_user_LostFocus(object sender, RoutedEventArgs e)
        {
            if(db.RunReader("select USER_NAME from  USER where USER_NAME='"+ txt_name_user.Text + "'").Rows.Count>0)
            {
                System.Windows.Forms.MessageBox.Show("هذا المستخدم موجود مسبقا"); txt_name_user.Text = "";
            }
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

        private void cbx_typ_items2_DropDownOpened(object sender, EventArgs e)
        {
            if(cbx_typ_items2.ItemsSource==null)
            {
                cbx_typ_items2.ItemsSource = db.RunReader("select UOM_ID,NAME from uom").DefaultView;
            }
        }

        private void cbx_typ_items1_DropDownOpened(object sender, EventArgs e)
        {
            if (cbx_typ_items1.ItemsSource == null)
            {
                cbx_typ_items1.ItemsSource = db.RunReader("select UOM_ID,NAME from uom_part").DefaultView;
            }
            }

        private void SettingBTN_Click(object sender, RoutedEventArgs e) {
            new SettingPage().Show();
        }

        private void txt_price_items_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txt_price_items_at3y.Text = txt_price_items.Text;
        }

        private void txt_name_items1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter && MessageBox.Show("هل تريد اضافة اللون", "Sure", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (db.RunReader("select count(*) from color where name='" + txt_name_items1.Text + "'").Rows[0][0].ToString() == "0")
                {
                    db.RunNonQuery("insert into color (name ) values ('" + txt_name_items1.Text + "') ");
                    gridcolar.Visibility = Visibility.Hidden;
                }
                else System.Windows.Forms.MessageBox.Show("تم ادخال هذا اللون من قبل");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridcolar.Visibility = Visibility.Visible;
            txt_name_items1.Text = "";
        }

        private void cbx_typ_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbx_typ_items4_DropDownOpened(object sender, EventArgs e)
        {
            cbx_typ_items4.ItemsSource = db.RunReader("select NAME from color where  NAME !=''").DefaultView;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {if(txt_name_items2.Text==string.Empty|| txt_phon_emp1.Text==string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("اكمل البيانات");return;
            }

            if(db.RunReader("select count(*) from COMPANY ").Rows[0][0].ToString()=="0")
            {
                db.RunNonQuery("insert into COMPANY (NAME,PHONE,PHONE2) values ('" + txt_name_items2.Text + "','" + txt_phon_emp1.Text + "','" + txt_phon_emp2.Text + "') ");
            }
            else
            {
                db.RunNonQuery("update  COMPANY  set NAME='" + txt_name_items2.Text + "',PHONE='" + txt_phon_emp1.Text + "',PHONE2  ='" + txt_phon_emp2.Text + "'");
            }
           
                if (mage_item_phtoo1.Source != null)
            {
                try
                {
                    string filePath = Environment.CurrentDirectory + "\\items";
                    Directory.CreateDirectory(filePath);
                    filePath = filePath + "\\" + txt_name_items2.Text + ".jpg";
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)mage_item_phtoo1.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);
                }
                catch { }
            }
            System.Windows.Forms.MessageBox.Show("تم الحفظ");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    mage_item_phtoo1.Source = new BitmapImage(new Uri(op.FileName));
                    //path2 = op.FileName;

                    //  MessageBox.Show(path2);
                    //   System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(path2);

                }




            }
            catch { }
        }
        string pathfile()
        {
            try
            {
                using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = fbd.ShowDialog();


                    // string[] files = Directory.GetFiles(fbd.SelectedPath);

                    // System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    return fbd.SelectedPath;
                }
            }
            catch { return ""; }

        }
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            try
            {
                string pathto = pathfile();
                if (pathto == string.Empty)
                    return;

                string filePath = Environment.CurrentDirectory.ToString() + "\\DB_ONA_Stores";
                //  string ff = "";
                //        foreach (string item in filePath.Split('\\'))
                //{
                //    ff = ff + ((ff == string.Empty )? "" : "\\") + item;
                //    if (item == "DB_ONA_Stores")
                //        break;
                //}
                if (File.Exists(filePath))
                    File.Copy(filePath, pathto + "\\DB_ONA_Stores" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                System.Windows.Forms.MessageBox.Show("تم النسخ");
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
        }

        private void txt_search_items_KeyDown(object sender, KeyEventArgs e)
        {
            Int64 x;
            if (e.Key == Key.Enter && txt_search_items.Text.Trim() != "" && db.RunReader("select PRODUCT_ID  from PRODUCT  where PRODUCT_ID='" + txt_search_items.Text.Trim() + "' ").Rows.Count <= 0 && Int64.TryParse(txt_search_items.Text.Trim(), out x) && System.Windows.Forms.MessageBox.Show("لا يوجد منتج بهاذا الكود هل تريد اضافة منتج جديد", "منتج جديد", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                txt_code_items.Text = txt_search_items.Text.Trim();
                txt_search_items.Text = "";
            }
               

        }

        private void Button_Click_18(object sender, RoutedEventArgs e) => txt_code_items.Text = new Random().NextULong(1000000000000, 999999999999999).ToString();

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {

            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
            {
                IncludeLabel = false,
                Alignment = BarcodeLib.AlignmentPositions.CENTER,
                Width = 190,
                Height = 200,
                RotateFlipType = System.Drawing.RotateFlipType.RotateNoneFlipNone,
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.Black,
            };

            string path = @"BarCodeItems\" + txt_code_items.Text.Trim() + ".png";
      
           // var pppp= Directory.GetCurrentDirectory();
            if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + path))
            barcode.Encode(BarcodeLib.TYPE.CODE128B, txt_code_items.Text).Save(path, System.Drawing.Imaging.ImageFormat.Png);

            path = System.AppDomain.CurrentDomain.BaseDirectory + path;
            try
            {
              //  DataTable dt = db.RunReader("select * from POS_RECIEPT_V ");
                View_Report showreport = new View_Report();
                BarCodeReport rpon = new BarCodeReport();

                rpon.SetParameterValue("imgPath", path);
                showreport.crystalReportViewer1.ReportSource = rpon;
                showreport.ShowDialog();



           

          

            }
            catch { }

        }
        static List<Tarkeb> tarkebs = new List<Tarkeb>();
        private void Button_Click_20(object sender, RoutedEventArgs e)
        {

            if(cbx_typ_items6.Text.Trim()==""|| txt_price_items9.Text.Trim()=="" || txt_price_items9.Text.Trim() == "0")
            {
                MessageBox.Show("اكمل البيانات");
                return;
            }

            if(tarkebs.Where(a=>a.TarkebID== Convert.ToDouble(cbx_typ_items6.Text.Trim())).FirstOrDefault()!=null)
            {
                MessageBox.Show("تم اضافتها من قبل");
                return;
            }
            try
            {
                tarkebs.Add(new Tarkeb()
                {
                    TarkebID = Convert.ToDouble(cbx_typ_items6.Text.Trim()),
                    QTY = Convert.ToDouble(txt_price_items9.Text.Trim())
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dg_Tarkeb.ItemsSource = null;
            dg_Tarkeb.ItemsSource = tarkebs;

            cbx_typ_items6.Text = "";
            txt_price_items9.Text = "";

        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            tarkebs.Clear();
            dg_Tarkeb.ItemsSource = null;
            txt_code_items1.Text = "";
            txt_name_items4.Text = "";
            cbx_typ_items6.Text = "";
            txt_price_items9.Text = "";
            txt_search_items1.Text = "";
            dg_items1.ItemsSource = null;
        }

        private void dg_Tarkeb_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            foreach (var item in tarkebs)
            {
                if (db.RunReader("select  PRODUCT_ID,name from PRODUCT where PRODUCT_ID ='" + item.TarkebID + "'").Rows.Count!=1)
                {
                    MessageBox.Show("برجاء التاكد من كود التركيب ");
                }
            }
        }

        private void dg_Tarkeb_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.Key == Key.Delete)
                {

                    tarkebs.RemoveAt(dg_Tarkeb.SelectedIndex);
                    dg_Tarkeb.ItemsSource = null;
                    dg_Tarkeb.ItemsSource = tarkebs;

                }
            }
            catch
            {

            }
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            if(tarkebs.Count<=0 || txt_name_items4.Text.Trim()==""|| txt_price_items5.Text.Trim()=="")
            {
                MessageBox.Show("برجاء اكمل البيانات");
                return;
            }

            

            if (txt_code_items1.Text.Trim() == "")
            {
                txt_code_items1.Text = new Random().NextULong(1000000, 999999999999).ToString();
                db.RunNonQuery(@"INSERT  INTO   PRODUCT (PRODUCT_ID                , NAME,                    PRODUCT_CATEGORY_ID       ,      SALES_UNIT_PRICE    ,     CREATEDBY                     , CREATEDDATE                                              ,Type    ) 
                                            VALUES ('" + txt_code_items1.Text + "','" + txt_name_items4.Text + "','" + cbx_typ_items7.Text + "','" + txt_price_items5.Text + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Tarkeb')");

       

            }else
            {
                //
                db.RunNonQuery("update PRODUCT set name='" + txt_name_items4.Text + "',PRODUCT_CATEGORY_ID='" + cbx_typ_items7.Text + "',SALES_UNIT_PRICE='" + txt_price_items5.Text + "' where PRODUCT_ID='" + txt_code_items1.Text.Trim() + "'");
                db.RunNonQuery("delete from TARKEBDATA where ProductID='" + txt_code_items1.Text.Trim() + "'");
            }

            foreach (var item in tarkebs)
            {
                string ID = db.RunReader(" select coalesce(max(ID), '0')+1 from TARKEBDATA ").Rows[0][0].ToString();
                db.RunNonQuery(@"INSERT INTO TARKEBDATA (ID,ProductID,ProductTarkebID,QTY,CreatedBy,CreatedDate)  VALUES ('" + ID + "','" + txt_code_items1.Text + "','" + item.TarkebID + "','" + item.QTY + "','" + User.Name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }

            MessageBox.Show("تم الحفظ");
            tarkebs.Clear();
            dg_Tarkeb.ItemsSource = null;
            txt_code_items1.Text = "";
            txt_name_items4.Text = "";
            cbx_typ_items6.Text = "";
            txt_price_items9.Text = "";
            txt_search_items1.Text = "";
            dg_items1.ItemsSource = null;
        }

        private void txt_search_items1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            dg_items1.ItemsSource = db.RunReader(@" select  PRODUCT_ID  as 'الكود', NAME as 'الاسم' 
                 ,SALES_UNIT_PRICE as ' سعر ',PRODUCT_CATEGORY_ID
 ,   CREATEDBY  as 'اسم المدخل', CREATEDDATE  as 'تاريخ الادخال' ,Type as 'النوع' from  PRODUCT p 
where Type='Tarkeb' and (PRODUCT_ID like '%" + txt_search_items1.Text + "%'  or  NAME like '%" + txt_search_items1.Text + "%') ").DefaultView;

        }

        private void dg_items1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_items1.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dg_items1.SelectedItem;

                    txt_code_items1.Text = row[0].ToString();
                    txt_name_items4.Text = row[1].ToString();
                    cbx_typ_items7.Text = row[3].ToString();
                    txt_price_items5.Text = row[2].ToString();
                  //  cbx_typ_items7.Text = row[4].ToString();
                    DataTable dt = db.RunReader("select ProductTarkebID,qty from TARKEBDATA where ProductID='" + row[0].ToString() + "'");
                    tarkebs.Clear();
                    dg_Tarkeb.ItemsSource = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tarkebs.Add(new Tarkeb
                        {
                            TarkebID = Convert.ToDouble(dt.Rows[i][0].ToString()),
                            QTY = Convert.ToDouble(dt.Rows[i][1].ToString())
                        });

                    }

                    dg_Tarkeb.ItemsSource = tarkebs;

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }

    public class Tarkeb
    {
        public double TarkebID { get; set; }
        public double QTY { get; set; }
    }

    public static class RandomExt
    {
        public static long NextLong(this Random self, long min, long max)
        {
            // Get a random 64 bit number.

            var buf = new byte[sizeof(ulong)];
            self.NextBytes(buf);
            ulong n = BitConverter.ToUInt64(buf, 0);

            // Scale to between 0 inclusive and 1 exclusive; i.e. [0,1).

            double normalised = n / (ulong.MaxValue + 1.0);

            // Determine result by scaling range and adding minimum.

            double range = (double)max - min;

            return (long)(normalised * range) + min;
        }

        public static ulong NextULong(this Random self, ulong min, ulong max)
        {
            // Get a random 64 bit number.

            var buf = new byte[sizeof(ulong)];
            self.NextBytes(buf);
            ulong n = BitConverter.ToUInt64(buf, 0);

            // Scale to between 0 inclusive and 1 exclusive; i.e. [0,1).

            double normalised = n / (ulong.MaxValue + 1.0);

            // Determine result by scaling range and adding minimum.

            double range = (double)max - min;

            return (ulong)(normalised * range) + min;
        }
    }
}
