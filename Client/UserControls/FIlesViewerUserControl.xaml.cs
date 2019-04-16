﻿using Common.Models;
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
    public sealed partial class FIlesViewerUserControl : UserControl
    {
        public FIlesViewerUserControl()
        {
            this.InitializeComponent();            
        }

        

        public List<FileCommon> FilesCollection
        {
            get { return (List<FileCommon>)GetValue(MyPropertyProperty); }
            set {
                SetValue(MyPropertyProperty, value); 
}
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register(nameof(FilesCollection), typeof(List<FileCommon>), typeof(FIlesViewerUserControl), new PropertyMetadata(0));


    }
}
