using Force.DeepCloner;
using LinePutScript.Localization.WPF;
using System.Windows;
using VPet.Plugin.ShowHeaAndLika.window;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.ShowHeaAndLika
{
    public class ShowHeaAndLika : MainPlugin {
		public ShowHeaAndLika(IMainWindow mainwin) : base(mainwin) {}
		public override string PluginName => "ShowHeaAndLika";

		Config config;

		GdPanelAction healthBar;
		GdPanelAction likabilityBar;
		public override void LoadPlugin() {
			config = Config.ConfigSave.Get(MW);

			if(config.showHealthBar)
			healthBar = new(MW, new GdPanelAction.GdPanelItem() {
					text = new() {
						Text = "ui_healthBar_title".Translate(),
					},
					progressBar = new() {
						Value = MW.Core.Save.Health,
						Maximum = 100,
					},
				},
				MW.Main.ToolBar.gdPanel
			);
			if(config.showLikabilityBar)
			likabilityBar = new(MW, new GdPanelAction.GdPanelItem() {
					text = new() {
						Text = "ui_likabilityBar_title".Translate(),
					},
					progressBar = new() {
						Value = MW.Core.Save.Likability,
						Maximum = MW.Core.Save.LikabilityMax,
					},
				},
				MW.Main.ToolBar.gdPanel
			);

			MW.Main.TimeHandle += TimeHandle;
			MW.Event_TakeItem += TakeItem;
		}
		void FlushBar() => 
			MW.Dispatcher.Invoke(() => {
				healthBar.ProgressBar_Change(MW, MW.Core.Save.Health);
				likabilityBar.ProgressBar_Change(MW, MW.Core.Save.Likability);
			});
		void TimeHandle(Main main) => FlushBar();
		void TakeItem(Food food) => FlushBar();

		internal Setting windowSetting = null;
		public override void Setting() {
			if (windowSetting == null || windowSetting.IsVisible == false) {
				windowSetting = new(config.DeepClone(), MW, 
					(c, isChange) => {
						if (isChange)
							config = c;
					}
				);
				windowSetting.Show();
			}
			windowSetting.Topmost = true;
			windowSetting.Topmost = false;
		}
		void SaveConfig() {
			Config.ConfigSave.Save(MW, config);
		}
		/*public override void Save() {
			SaveConfig();
			base.Save();
		}*/
		public override void EndGame() {
			SaveConfig();
			base.EndGame();
		}
	}
}
