using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView
{
	[ValueConversion(typeof(string), typeof(BitmapImage))]
	class HeadToImageConverter : IValueConverter
	{
		public static HeadToImageConverter Instance = new HeadToImageConverter();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
