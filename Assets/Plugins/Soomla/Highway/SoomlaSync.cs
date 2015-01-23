using UnityEngine;
using System.Collections;
using Soomla.Highway;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Soomla.Sync
{
	public class SoomlaSync {
#if UNITY_IOS  && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern int soomlaSync_initialize(bool metaDataSync, bool stateSync);
		[DllImport ("__Internal")]
		private static extern int soomlaSync_registerUnityConflictResolver();
		[DllImport ("__Internal")]
		private static extern int soomlaSync_resolveConflict(int strategy, string jsonState);
#endif

		public delegate JSONObject StateConflictResolver(JSONObject remoteState, JSONObject currentState, JSONObject stateDiff);
		public static StateConflictResolver CurrentStateConflictResolver = (JSONObject remoteState, JSONObject currentState, JSONObject stateDiff) =>
		{
			return remoteState;
		};

		public static void Initialize(bool metaDataSync, bool stateSync) {
			SoomlaUtils.LogDebug (TAG, "SOOMLA/UNITY Initializing Sync");
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniSoomlaSyncClass = new AndroidJavaClass("com.soomla.sync.SoomlaSync")) {
				
				AndroidJavaObject jniSoomlaSyncInstance = jniSoomlaSyncClass.CallStatic<AndroidJavaObject>("getInstance");
				jniSoomlaSyncInstance.Call("initialize", metaDataSync, stateSync);
			}
			using(AndroidJavaClass jniUnitySoomlaSyncClass = new AndroidJavaClass("com.soomla.highway.unity.SoomlaSync")) 
			{	
				jniUnitySoomlaSyncClass.CallStatic("registerUnityConflictResolver");
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			soomlaSync_initialize(metaDataSync, stateSync);
			soomlaSync_registerUnityConflictResolver();
#endif
		}

		internal static void HandleMetaDataSyncFinised() {
			if (IsStoreAvailable()) {
				try {
					// Call SoomlaStore.initializeFromDB() using reflection to prevent coupling
					// Loads the synced meta-data from the storage
					Type storeInfoType = Type.GetType("Soomla.Store.StoreInfo");
					if (storeInfoType != null) {
						MethodInfo initFromDBMethod = storeInfoType.GetMethod("initializeFromDB", BindingFlags.NonPublic | BindingFlags.Static);
						if (initFromDBMethod != null) {
							initFromDBMethod.Invoke(null, null);
							SoomlaUtils.LogDebug(TAG, "Finished syncing meta-data Unity");
						}
					}
				} catch (Exception ex) {
					// This should not happen since if native succeeded in loading, Unity should too
					String message = "Unable to handle meta-data sync for Store on Unity side, the application is now in " +
						"an inconsistant state between Unity and native: " + ex.Message;
					SoomlaUtils.LogError(TAG, message);
					throw new InvalidOperationException(message);
				}
			}
		}

		internal static void HandleStateSyncFinised ()
		{
			if (IsStoreAvailable()) {
				try {
					// Call StoreInventory.RefreshLocalInventory() using reflection to prevent coupling
					// Loads the synced state from the storage
					Type storeInventoryType = Type.GetType("Soomla.Store.StoreInventory");
					if (storeInventoryType != null) {
						MethodInfo refreshLocalInventoryMethod = storeInventoryType.GetMethod("RefreshLocalInventory", BindingFlags.Public | BindingFlags.Static);
						if (refreshLocalInventoryMethod != null) {
							refreshLocalInventoryMethod.Invoke(null, null);
							SoomlaUtils.LogDebug(TAG, "Finished syncing state Unity");
						}
					}
				} catch (Exception ex) {
					// This should not happen since if native succeeded in loading, Unity should too
					String message = "Unable to handle state sync for Store on Unity side, the application is now in " +
						"an inconsistant state between Unity and native: " + ex.Message;
					SoomlaUtils.LogError(TAG, message);
					throw new InvalidOperationException(message);
				}
			}
		}

		internal static void HandleStateSyncConflict(JSONObject remoteState, JSONObject currentState, JSONObject stateDiff) {
#if !UNITY_EDITOR
			JSONObject resolvedState = CurrentStateConflictResolver(remoteState, currentState, stateDiff);
			int conflictResolveStrategy = 2;
			if (resolvedState == remoteState) {
				conflictResolveStrategy = 0;
			} else if (resolvedState == currentState) {
				conflictResolveStrategy = 1;
			}

#if UNITY_ANDROID
			using(AndroidJavaClass jniUnitySoomlaSyncClass = new AndroidJavaClass("com.soomla.highway.unity.SoomlaSync")) 
			{	
				jniUnitySoomlaSyncClass.CallStatic("resolveConflict", conflictResolveStrategy, resolvedState.print());
			}
#elif UNITY_IOS
			soomlaSync_resolveConflict(conflictResolveStrategy, resolvedState.print());
#endif

#endif
		}

		private static bool IsStoreAvailable() {
			try {
				Type result = Type.GetType("Soomla.Store.SoomlaStore");
				return (result != null);
			} catch (Exception) {
				return false;
			}
		}

		private const string TAG = "SOOMLA SoomlaSync";
	}
}