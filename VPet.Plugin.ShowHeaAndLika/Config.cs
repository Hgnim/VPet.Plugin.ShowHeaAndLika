using System.Windows.Input;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.ShowHeaAndLika {
	internal class Config {
		internal bool showHealthBar;
		internal bool showLikabilityBar;

		internal struct ConfigSave {
			const string mainKey = "ShowHeaAndLika";
			const string shbKey = "showHealthBar";
			const string slbKey = "showLikabilityBar";

			internal static Config Get(IMainWindow MW,bool getDef=false) =>
				new() {
#pragma warning disable IDE0075
					showHealthBar = !string.IsNullOrEmpty(MW.GameSavesData[mainKey].GetString(shbKey)) && !getDef ? 
										MW.GameSavesData[mainKey][(LinePutScript.gbol)shbKey] : true,
					showLikabilityBar= !string.IsNullOrEmpty(MW.GameSavesData[mainKey].GetString(slbKey)) && !getDef ?
										MW.GameSavesData[mainKey][(LinePutScript.gbol)slbKey] : true
#pragma warning restore IDE0075
				};

			internal static void Save(IMainWindow MW, Config config) {
				MW.GameSavesData[mainKey][(LinePutScript.gbol)shbKey] = config.showHealthBar;
				MW.GameSavesData[mainKey][(LinePutScript.gbol)slbKey] = config.showLikabilityBar;
			}
		}
	}
}
