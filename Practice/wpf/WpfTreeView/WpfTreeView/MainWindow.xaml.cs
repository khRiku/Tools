using System;
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
using Path = System.IO.Path;

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
			 ///Get every logical drive on the machine
			foreach (var drive in Directory.GetLogicalDrives())
			{
				// Create a new item for it
				var item = new TreeViewItem()
				{
					// Set the header 
					Header = drive,
					//And the full path
					Tag = drive,
				};



				// Add a dummy item
				item.Items.Add(null);

				// Listen out for item being expanded
				item.Expanded += Folder_Expanded;

				FolderView.Items.Add(item);
			}
		}
		#endregion

		#region folder expanded

		/// <summary>
		///  When a folder is expanded, find the sub folders/files 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Folder_Expanded(object sender, RoutedEventArgs e)
		{
			TreeViewItem item = (TreeViewItem)sender;

			// If the item only contains the dummy data
			if (item.Items.Count != 1 || item.Items[0] != null)
				return;

			// Clear adummy data
			item.Items.Clear();

			// Get full path
			var fullPath = (string) item.Tag;

			#region Get Folders
			//Create a blank list for directories
			var directorys = new List<string>();

			//Try and get directories form the folder
			// ignore any issues doing so
			try
			{
				var diretoryPath = Directory.GetDirectories(fullPath);

				if (diretoryPath.Length > 0)
					directorys.AddRange(diretoryPath);
			}
			catch (Exception pE) { }

			// For each file
			directorys.ForEach(filePath =>
			{
				// Create file item
				var subItem = new TreeViewItem()
				{
					// Set header as folder name
					Header = GetFileFolderName(filePath),
					//And tag as full path
					Tag = filePath,
				};
 
				WpfTreeView.HeadToImageConverter.

				subItem.Items.Add(null);

				subItem.Expanded += Folder_Expanded;

				// Add this item to the parent
				item.Items.Add(subItem);
			});
			#endregion

			#region Get Files

			//Create a blank list for directories
			var files = new List<string>();

			//Try and get directories form the folder
			// ignore any issues doing so
			try
			{
				var fs = Directory.GetFiles(fullPath);

				if(fs.Length > 0)
					files.AddRange(fs);
			}
			catch (Exception pE) { }

			// For each file
			files.ForEach(filePath =>
			{
				// Create file item
				var subItem = new TreeViewItem()
				{
					// Set header as folder name
					Header = GetFileFolderName(filePath),
					//And tag as full path
					Tag = filePath,
				};

				// Add this item to the parent
				item.Items.Add(subItem);
			});

#endregion

		}

		/// <summary>
		/// Find the file or folder name from a full path
		/// </summary>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		private object GetFileFolderName(string path)
		{
			// C:Somethin\a folder
			//c:\Something\a file.png
			// a file file.png

			// If we have no path, return empty
			if (string.IsNullOrEmpty(path))
				return string.Empty;

			var nomalizePath = path.Replace('/', '\\');

			// Find the last backslash in the path
			var lastIndex = nomalizePath.LastIndexOf('\\');

			// If we don't find a backslash, return the path itsel
			if (lastIndex <= 0)
				return path;

			// Return the anme after the last 
			return path.Substring(lastIndex + 1);
		}

		#endregion
	}
}
