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

namespace WpfBinding
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Binding bind = new Binding("Value")
			{
				//	ElementName = slider2.Name,
				Source = this.slider2,
				Mode = BindingMode.TwoWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
			};


			ValidationRule rule = new RangeValidationRule();
			rule.ValidatesOnTargetUpdated = true;
			bind.ValidationRules.Add(rule);
			this.textbox1.SetBinding(TextBox.TextProperty, bind);

		   


		}
	}
}
