using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;
using System;
using Soomla.Sync;
using System.Runtime.InteropServices;

namespace Soomla.Highway {

	public class HighwayEvents : MonoBehaviour {

		#if UNITY_IOS 
		//&& !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern int unityHighwayEventDispatcher_Init();
		#endif

		private const string TAG = "SOOMLA HighwayEvents";
		
		private static HighwayEvents instance = null;
		
		/// <summary>
		/// Initializes the game state before the game starts.
		/// </summary>
		void Awake(){
			if(instance == null){ 	// making sure we only initialize one instance.
				SoomlaUtils.LogDebug(TAG, "Initializing HighwayEvents (Awake)");
				instance = this;
				GameObject.DontDestroyOnLoad(this.gameObject);
				Initialize();
			} else {				// Destroying unused instances.
				GameObject.Destroy(this.gameObject);
			}
		}

		/// <summary>
		/// Initializes the different native event handlers in Android / iOS
		/// </summary>
		public static void Initialize() {
			SoomlaUtils.LogDebug (TAG, "Initializing StoreEvents ...");
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniEventHandler = new AndroidJavaClass("com.soomla.highway.unity.HighwayEventHandler")) {
				jniEventHandler.CallStatic("initialize");
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);

			#elif UNITY_IOS && !UNITY_EDITOR
			unityHighwayEventDispatcher_Init();
			#endif
		}

		public void onSoomlsSyncInitialized() {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onSoomlsSyncInitialized");
			HighwayEvents.OnSoomlsSyncInitialized();
		}

		public void onMetaDataSyncStarted() {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onMetaDataSyncStarted");
			HighwayEvents.OnMetaDataSyncStarted();
		}

		public void onMetaDataSyncFinished(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onMetaDataSyncFinished: " + message);

			JSONObject eventJSON = new JSONObject(message);
			List<JSONObject> changedComponentsJSON = eventJSON["changedComponents"].list;

			List<String> changedComponents = new List<String>();
			foreach (var changedComponentJSON in changedComponentsJSON) {
				changedComponents.Add(changedComponentJSON.str);
			}

			SoomlaSync.HandleMetaDataSyncFinised();

			HighwayEvents.OnMetaDataSyncFinished(changedComponents);
		}

		public void onMetaDataSyncFailed(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onMetaDataSyncFailed:" + message);
			
			JSONObject eventJSON = new JSONObject(message);
			int errorCode = (int)eventJSON["errorCode"].n;
			string errorMessage = eventJSON["errorMessage"].str;
			
			HighwayEvents.OnMetaDataSyncFailed((MetaDataSyncErrorCode)errorCode, errorMessage);
		}

		public void onStateSyncStarted() {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onStateSyncStarted");
			HighwayEvents.OnStateSyncStarted();
		}
		
		public void onStateSyncFinished(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onStateSyncFinished: " + message);
			
			JSONObject eventJSON = new JSONObject(message);

			List<JSONObject> changedComponentsJSON = eventJSON["changedComponents"].list;
			List<String> changedComponents = new List<String>();
			foreach (var changedComponentJSON in changedComponentsJSON) {
				changedComponents.Add(changedComponentJSON.str);
			}

			List<JSONObject> failedComponentsJSON = eventJSON["failedComponents"].list;
			List<String> failedComponents = new List<String>();
			foreach (var failedComponentJSON in failedComponentsJSON) {
				failedComponents.Add(failedComponentJSON.str);
			}
			
			SoomlaSync.HandleStateSyncFinised();
			
			HighwayEvents.OnStateSyncFinished(changedComponents, failedComponents);
		}
		
		public void onStateSyncFailed(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onStateSyncFailed:" + message);
			
			JSONObject eventJSON = new JSONObject(message);
			int errorCode = (int)eventJSON["errorCode"].n;
			string errorMessage = eventJSON["errorMessage"].str;
			
			HighwayEvents.OnStateSyncFailed((StateSyncErrorCode)errorCode, errorMessage);
		}

		public static Action OnSoomlsSyncInitialized = delegate {};
		public static Action OnMetaDataSyncStarted = delegate {};
		public static Action<IList<String>> OnMetaDataSyncFinished = delegate {};
		public static Action<MetaDataSyncErrorCode, String> OnMetaDataSyncFailed = delegate {};
		public static Action OnStateSyncStarted = delegate {};
		public static Action<IList<String>, IList<String>> OnStateSyncFinished = delegate {};
		public static Action<StateSyncErrorCode, String> OnStateSyncFailed = delegate {};




		/* Internal SOOMLA events ... Not meant for public use */

		public void onConflict(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onConflict:" + message);

			JSONObject eventJSON = new JSONObject(message);
			string remoteStateStr = eventJSON["remoteState"].str;
			string currentStateStr = eventJSON["currentState"].str;
			string stateDiffStr = eventJSON["stateDiff"].str;

			JSONObject remoteState = new JSONObject(remoteStateStr);
			JSONObject currentState = new JSONObject(currentStateStr);
			JSONObject stateDiff = new JSONObject(stateDiffStr);

			SoomlaSync.HandleStateSyncConflict(remoteState, currentState, stateDiff);
		}
	}

	public enum MetaDataSyncErrorCode
	{
		GeneralError = 0,
		ServerError = 1,
		UpdateModelError = 2
	}

	public enum StateSyncErrorCode
	{
		GeneralError = 0,
		ServerError = 1,
		UpdateStateError = 2
	}
}