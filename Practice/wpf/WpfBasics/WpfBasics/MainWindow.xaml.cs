using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfBasics
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ApplyButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show($"The description is:{this.DescriptionText.Text}");
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			this.WeldCheckBox.IsChecked = false;
		}

		private void Checkbox_Checked(object sender, RoutedEventArgs e)
		{
			this.LengthText.Text += ((CheckBox) sender).Content;
		}

		private void FinishDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//if (this.NoteText == null)
			//	return;

			ComboBoxItem combo= ((ComboBox)sender).SelectedValue as ComboBoxItem;
			this.NoteText.Text = combo.Content as string;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			FinishDropdown_SelectionChanged(this.FinishDropdown, null);
		}

		private void SupplierNameText_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.MassText.Text = this.SupplierNameText.Text;
		}
	}
}
