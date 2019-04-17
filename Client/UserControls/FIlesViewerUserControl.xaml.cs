using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Client.UserControls
{
    public sealed partial class FilesViewerUserControl : UserControl
    {
        public FilesViewerUserConStrol()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<FileCommon> FileCollection
        {
            get { return (ObservableCollection<FileCommon>)GetValue(FileCommonsProperty); }
            set { SetValue(FileCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileCommons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileCollectionProperty =
            DependencyProperty.Register("FileCollection", typeof(ObservableCollection<FileCommon>), typeof(FilesViewerUserControl), new PropertyMetadata(0));


    }
}
