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

namespace Student
{
    /// <summary>
    /// Логика взаимодействия для TopListsFrame.xaml
    /// </summary>
    public partial class TopListsFrame : Page
    {
        Frame Frame;
        public TopListsFrame(Frame frame, int id)
        {
            InitializeComponent();
            Frame = frame;

            //рейтинг абитуриентов по выбранной специальности(пеедается айди)
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
