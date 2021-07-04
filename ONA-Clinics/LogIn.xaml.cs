using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            oldData();

            validTest();
        }
        private static string background { get; set; }
        private static string ColorAccent { get; set; }
        private static string ColorPrimary { get; set; }
        private void oldData() {
            try {
                System.IO.Directory.CreateDirectory(@"Setting");

                if(File.Exists(@"Setting\background.txt")) {
                    background = File.ReadAllText(@"Setting\background.txt");

                    changeTheme();

                }
                //  PasswordBox.Password = File.ReadAllText("pass.txt");
            } catch { }
        }
        private void changeTheme() {
            Uri uri0 = System.Windows.Application.Current.Resources.MergedDictionaries[0].Source;

            Uri uri2 = System.Windows.Application.Current.Resources.MergedDictionaries[2].Source;

            Uri uri3 = System.Windows.Application.Current.Resources.MergedDictionaries[3].Source;


            System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
            Uri uri;
            if(background != null && background.Trim() != "")
                uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + background + ".xaml");
            else
                uri = uri0;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });

            uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(1, new ResourceDictionary() { Source = uri });

            if(ColorAccent != null && ColorAccent.Trim() != "")

                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + ColorAccent + ".xaml");
            else
                uri = uri2;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri });

            if(ColorPrimary != null && ColorPrimary.Trim() != "")
                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + ColorPrimary + ".xaml");
            else
                uri = uri3;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });




        }
        private void validTest() {

            string path = @"impor_×64.txt";


            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "abdo")
                        {

                            sr.Close();

                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(Environment.MachineName);
                                return;
                            }
                        }
                        if (s != Environment.MachineName)
                        {
                            MessageBox.Show("غير مسموح بتشغيل هذا البرنامج على هذا الجهاز" + Environment.NewLine + "للمزيد من المعلومات برجاء الاتصال ب 01211771638");

                            Application.Current.Shutdown();
                        }

                        return;
                    }
                }

            }

              

                if(!File.Exists(path)) {
                    MessageBox.Show("بعض الملفات محذوفة برجاء التأكد من الملفات كاملة");
                }
        

         
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }

        DB db = new DB();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            validationFun();


        }
        void validationFun() {

            try {


                DataTable s = db.RunReader("select USER_ID,EMPLOYEE_ID,USER_PASSWORD,USER_NAME,ACCESS_ID from USER where USER_NAME = '" + txtEName.Text + "'");
                if(s.Rows.Count <= 0) {
                    MessageBox.Show("Please check your Name");
                    return;
                }

                if(s.Rows[0][2].ToString() != PasswordBox.Password.ToString()) {
                    MessageBox.Show("Please check your Password");
                    return;
                }

                User.Code = s.Rows[0][0].ToString();
                User.Employee_ID = s.Rows[0][1].ToString();
                User.Name = s.Rows[0][3].ToString();
                User.ACCESS_ID = s.Rows[0][4].ToString();

                DataTable ss = db.RunReader("SELECT ACCESS_TYPE,main_form,casher_form,report_form,view_form,kitchen_form from USER_ACCESS WHERE USER_ACCESS_ID ='" + User.ACCESS_ID + "'");

                User.ACCESS_TYPE = ss.Rows[0][0].ToString();
                User.main_form = ss.Rows[0][1].ToString();
                User.casher_form = ss.Rows[0][2].ToString();
                User.report_form = ss.Rows[0][3].ToString();
                User.view_form = ss.Rows[0][4].ToString();
                User.kitchen_form = ss.Rows[0][5].ToString();



                if(User.main_form == "Y" || User.main_form == "y")
                    new BasicFRM().Show();
                else if(User.casher_form == "Y" || User.casher_form == "y")
                    new MainFRM().Show();

                else if(User.kitchen_form == "Y" || User.kitchen_form == "y")
                    new Orders_DONE().Show();
                else if(User.report_form == "Y" || User.report_form == "y")
                    new reportFRM().Show();
                else if(User.view_form == "Y" || User.view_form == "y")
                    new Orders().Show();

                else MessageBox.Show("برجاء المحاولة مرة اخرى");

                this.Close();
            } catch(Exception ex) { MessageBox.Show(ex.Message); }


        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key==Key.Enter)
            validationFun();
        }
    }
}
