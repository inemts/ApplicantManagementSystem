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

namespace Student
{
    /// <summary>
    /// Логика взаимодействия для WinStudent.xaml
    /// </summary>
    public partial class WinStudent : Window
    {
        int IdPost;
        public WinStudent(int idPost)
        {
            InitializeComponent();
            IdPost = idPost;
            MainFrame.Content = new SpecialFrame(MainFrame, IdPost);
        }

        private void ShowSpecial(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SpecialFrame(MainFrame, IdPost);
        }
    }
}
