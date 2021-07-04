using System;
using System.IO;
using System.Windows;
using System.Windows.Input;



namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Window
    {
        private static string background { get; set; }
        private static string ColorAccent { get; set; }
        private static string ColorPrimary { get; set; }
        public SettingPage()
        {
            InitializeComponent();

           
            //   ColorAccent = AccentBrush.Background.ToString();
            oldData();

        }

        private void oldData()
        {
            try
            {
                System.IO.Directory.CreateDirectory(@"Setting");

                if (File.Exists(@"Setting\background.txt"))
                {
                    background = File.ReadAllText(@"Setting\background.txt");
                    if (background == "dark")
                        Background.IsChecked = true;
                    else if (background == "light")
                        Background.IsChecked = false;
                }
              //  PasswordBox.Password = File.ReadAllText("pass.txt");
            }
            catch { }
        }

        #region MainRegion

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
        private void LogOutBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }


        private void PackIcon_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void PackIcon_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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






        #endregion

   


    
        private void Background_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Background.IsChecked == true)
                    background = "dark";  
                else
                  background = "light";
                File.WriteAllText(@"Setting\background.txt", background);
                changeTheme();
            }
            catch { }
        }

        private void changeTheme()
        {
            Uri uri0 = System.Windows.Application.Current.Resources.MergedDictionaries[0].Source;

            Uri uri3 = System.Windows.Application.Current.Resources.MergedDictionaries[2].Source;

            Uri uri2 = System.Windows.Application.Current.Resources.MergedDictionaries[3].Source;


            System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
            Uri uri;
            if (!string.IsNullOrWhiteSpace(background))
                uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + background + ".xaml");
            else
                uri = uri0;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });

            uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(1, new ResourceDictionary() { Source = uri });

            if (!string.IsNullOrWhiteSpace(ColorAccent))
            {
                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + ColorAccent + ".xaml");
                File.WriteAllText(@"Setting\ColorAccent.txt", ColorAccent);
            }
            else
                uri = uri3;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri });

            if (!string.IsNullOrWhiteSpace(ColorPrimary))
            {
                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + ColorPrimary + ".xaml");
                File.WriteAllText(@"Setting\ColorPrimary.txt", ColorPrimary);
            }
            else
                uri = uri2;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });

            
           

           // string.IsNullOrWhiteSpace(ColorPrimary)


        }

        private void Btn_Primary_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Yellow";
            changeTheme();
        }

        private void Btn_Accent_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Yellow";
            changeTheme();
        }

        private void Btn_Primary2_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "amber";
            changeTheme();
        }

        private void Btn_Accent2_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "amber";
            changeTheme();
        }

        private void Btn_Primary3_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Orange";
            changeTheme();
        }

        private void Btn_Accent3_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Orange";
            changeTheme();
        }

        private void Btn_Primary4_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "DeepOrange";
            changeTheme();
        }

        private void Btn_Accent4_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "DeepOrange";
            changeTheme();
        }

        private void Btn_Primary5_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "LightBlue";
            changeTheme();
        }

        private void Btn_Accent5_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "LightBlue";
            changeTheme();
        }

        private void Btn_Primary6_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Blue";
            changeTheme();
        }

        private void Btn_Accent6_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Blue";
            changeTheme();
        }

        private void Btn_Primary7_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Teal";
            changeTheme();
        }

        private void Btn_Accent7_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Teal";
            changeTheme();
        }

        private void Btn_Primary8_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Cyan";
            changeTheme();
        }

        private void Btn_Accent8_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Cyan";
            changeTheme();
        }

        private void Btn_Primary9_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Pink";
            changeTheme();
        }

        private void Btn_Accent9_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Pink";
            changeTheme();
        }

        private void Btn_Primary10_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Green";
            changeTheme();
        }

        private void Btn_Accent10_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Green";
            changeTheme();
        }

        private void Btn_Primary11_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "DeepPurple";
            changeTheme();
        }

        private void Btn_Accent11_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "DeepPurple";
            changeTheme();
        }

        private void Btn_Primary12_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Indigo";
            changeTheme();
        }

        private void Btn_Accent12_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Indigo";
            changeTheme();
        }

        private void Btn_Primary13_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "LightGreen";
            changeTheme();
        }

        private void Btn_Accent13_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "LightGreen";
            changeTheme();
        }

        private void Btn_Primary14_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Red";
            changeTheme();
        }

        private void Btn_Accent14_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Red";
            changeTheme();
        }

        private void Btn_Primary15_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Lime";
            changeTheme();
        }

        private void Btn_Accent15_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Lime";
            changeTheme();
        }

        private void Btn_Primary16_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Purple";
            changeTheme();
        }

        private void Btn_Accent16_Click(object sender, RoutedEventArgs e)
        {
            ColorAccent = "Purple";
            changeTheme();
        }

        private void Btn_Primary17_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "BlueGrey";
            changeTheme();
        }

        private void Btn_Primary1_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Grey";
            changeTheme();
        }

        private void Btn_Primary18_Click(object sender, RoutedEventArgs e)
        {
            ColorPrimary = "Brown";
            changeTheme();
        }
    }
}
