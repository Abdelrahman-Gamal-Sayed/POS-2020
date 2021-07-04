using System.Windows;
using System.Windows.Controls;

namespace ONA_Stores
{
    /// <summary>
    /// Interaction logic for Group.xaml
    /// </summary>
    public partial class Group : UserControl
    {
        public Group()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User.groub_code = Code.Content.ToString();
        }
    }
}
