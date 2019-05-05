using System;
using System.CodeDom;
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

            //Slider2
            Binding bind = new Binding("Value")
            {
                //	ElementName = slider2.Name,
                Source = this.slider2,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            
            ValidationRule rule = new RangeValidationRule();
            rule.ValidatesOnTargetUpdated = false;
            bind.ValidationRules.Add(rule);

            this.textbox1.SetBinding(TextBox.TextProperty, bind);

            //Slider3 非UI元素绑定， 显示同步处理方式 1
            this.Slider3.SetBinding(Slider.ValueProperty, new Binding("changeValue") {Source = this, Mode = BindingMode.TwoWay});

            //TextBox1 非UI元素绑定， 显示同步处理方式 2
            bind = new Binding("Myproperty")
            {
                Source = mModel,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            this.TextBox1.SetBinding(TextBox.TextProperty, bind);

        }

        private Model mModel = new Model();

        #region 非UI元素绑定， 显示同步处理方式 1
        private DependencyProperty changeValueProperty = DependencyProperty.Register(
            "changeValue",
            typeof(int),
            typeof(MainWindow),
            new PropertyMetadata(10)// 默认值
            );

        private int changeValue
        {
            get { return (int)GetValue(changeValueProperty); }
            set
            {
                SetValue(changeValueProperty, value);
            }
        }

        #endregion

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            changeValue += 10;
        }

        private void OnClick_TextBox1ChangeContent(object sender, RoutedEventArgs e)
        {

            mModel.Myproperty += "a";
        }
    }
}
