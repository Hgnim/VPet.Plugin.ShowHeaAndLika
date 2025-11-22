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
using System.Windows.Shapes;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.ShowHeaAndLika.window {
	/// <summary>
	/// Setting.xaml 的交互逻辑
	/// </summary>
	public partial class Setting : Window {
		Config config;
		IMainWindow MW;
		bool isSave = false;
		internal delegate void ReturnConfig(Config config, bool isChange);
		ReturnConfig returnConfig;

		/// <param name="config_">当前配置</param>
		/// <param name="returnConfig_">用于返回设置后的配置</param>
		internal Setting(Config config_, IMainWindow MW_, ReturnConfig returnConfig_) {
			config = config_;
			MW = MW_;
			returnConfig = returnConfig_;
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			ShowHealthBar.IsChecked = config.showHealthBar;
			ShowLikabilityBar.IsChecked = config.showLikabilityBar;
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			returnConfig?.Invoke(config,isSave);
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e) {
			isSave = true;
			this.Close();
        }

		private void ShowHealthBar_CheckedChange(object sender, RoutedEventArgs e) {
			bool? v = ShowHealthBar.IsChecked;
			if (v != null)
				config.showHealthBar = (bool)v;
		}
		private void ShowLikabilityBar_CheckedChange(object sender, RoutedEventArgs e) {
			bool? v = ShowLikabilityBar.IsChecked;
			if (v != null)
				config.showLikabilityBar = (bool)v;
		}
	}
}
