using LinePutScript.Localization.WPF;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.ShowHeaAndLika
{
    public class ShowHeaAndLika : MainPlugin {
		public ShowHeaAndLika(IMainWindow mainwin) : base(mainwin) {}
		public override string PluginName => "ShowHeaAndLika";

		GdPanelAction healthBar;
		GdPanelAction likabilityBar;
		public override void LoadPlugin() {
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
	}
}
