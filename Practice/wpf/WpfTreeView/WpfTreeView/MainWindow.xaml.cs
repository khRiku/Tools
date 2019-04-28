﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfTreeView
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Constructor

		public MainWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region On Loaded
		/// <summary>
		/// When the application first opens
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			foreach (var drive in Directory.GetLogicalDrives())
			{
				var item = new TreeViewItem();
				item.Header = drive;
				FolderView.Items.Add(item);
			}
		}

		#endregion
	}
}
