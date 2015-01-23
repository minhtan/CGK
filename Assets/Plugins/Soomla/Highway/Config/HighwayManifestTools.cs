using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace Soomla.Highway
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public class HighwayManifestTools : ISoomlaManifestTools
    {
#if UNITY_EDITOR
		static HighwayManifestTools instance = new HighwayManifestTools();
		static HighwayManifestTools()
		{
			SoomlaManifestTools.ManTools.Add(instance);
		}

		public void UpdateManifest() {
			SoomlaManifestTools.SetPermission("android.permission.INTERNET");
			SoomlaManifestTools.SetPermission("android.permission.WRITE_EXTERNAL_STORAGE");
			SoomlaManifestTools.SetPermission("android.permission.ACCESS_NETWORK_STATE");
			SoomlaManifestTools.SetPermission("android.permission.ACCESS_WIFI_STATE");
			SoomlaManifestTools.SetPermission("android.permission.READ_PHONE_STATE");

			//google-play-services.jar version
			SoomlaManifestTools.AddMetaDataTag("com.google.android.gms.version", "@integer/google_play_services_version");
		}
#endif
	}
}
