using Common.Models;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Client.UserControls
{
    public sealed partial class FilesViewerUserControl : UserControl
    {
        public FilesViewerUserControl()
        {
            this.InitializeComponent();

        }



        public ObservableCollection<FileCommon> SelectedFiles
        {
            get { return (ObservableCollection<FileCommon>)GetValue(SelectedFilesProperty); }
            set { SetValue(SelectedFilesProperty, value);
                
            }
        }

        // Using a DependencyProperty as the backing store for SelectedFiles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFilesProperty =
            DependencyProperty.Register("SelectedFiles", typeof(List<FileCommon>), typeof(FilesViewerUserControl), new PropertyMetadata(null));



        public List<FileCommon> FilesCollection
        {
            get { return (List<FileCommon>)GetValue(FilesCollectionProperty); }
            set
            {
                SetValue(FilesCollectionProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilesCollectionProperty =
            DependencyProperty.Register(nameof(FilesCollection), typeof(List<FileCommon>), typeof(FilesViewerUserControl), new PropertyMetadata(null));

        private void FilesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFiles = new ObservableCollection<FileCommon>();
            foreach (object item in FilesDataGrid.SelectedItems)
            {
                SelectedFiles.Add(item as FileCommon);
            }
        }
    }
}
