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
using System.Windows.Threading;
using ProjectMS2.BusinessLayer;

namespace ProjectMS2.PresentationLayer
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Message message;
        private ChatRoom ch;
        private ChatroomWindow chWindow;

        public EditWindow()
        {
            InitializeComponent();
        }
        public EditWindow(Message msg, ChatRoom ch,ChatroomWindow chWindow)
        {
            this.message = msg;
            this.chWindow = chWindow;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
         new Action(() =>
         {
             txt_editBox.Text = msg.MessageContent;

         }));

            this.ch = ch;
            InitializeComponent();
            txt_editBox.MaxLength = 100;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            message.MessageContent = txt_editBox.Text;
            ch.editMessage(message,chWindow.sortType,chWindow.txtBox_IdFilter.Text,chWindow.txtBox_uNameFilter.Text);
            Close();


        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
