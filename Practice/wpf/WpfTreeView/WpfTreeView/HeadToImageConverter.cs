﻿using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView
{
	[ValueConversion(typeof(string), typeof(BitmapImage))]
	public  class HeadToImageConverter : IValueConverter
	{
		public static HeadToImageConverter Instance = new HeadToImageConverter();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string path = (string) value;

			if (path == null)
				return null;

			string name = (string)MainWindow.GetFileFolderName(path);

			string image = "Images/file.png";

			if (string.IsNullOrEmpty(name))
				image = "Images/drive.png";
			else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
				image = "Images/folder-closed.png";
				

			

			return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
