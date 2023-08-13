using UnityEditor;
using UnityEngine;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieSudioFTPEditors
{
		[CustomEditor(typeof(DataManager))]
		public class DataManagerEditor : Editor
		{
			public override void OnInspectorGUI()
			{
				if (Application.isPlaying) {
					return;
				}
				DataManager attrib = (DataManager)target;//get the target
				EditorGUILayout.Separator ();
				attrib.fileName = EditorGUILayout.TextField ("File Name",attrib.fileName);
				attrib.serilizationMethod = (DataManager.SerilizationMethod)EditorGUILayout.EnumPopup ("Serilization Method",attrib.serilizationMethod);
		
				EditorGUILayout.Separator ();

				if (GUILayout.Button ("Explore File Folder", GUILayout.Width (120), GUILayout.Height (25))) {
					string path = null;
					#if UNITY_ANDROID
						path = DataManager.GetAndroidFileFolder();
					#elif UNITY_IPHONE
						path = DataManager.GetIPhoneFileFolder();
					#elif UNITY_WP8 || UNITY_WP8_1
						path = DataManager.GetWP8FileFolder();
					#else
						path = DataManager.GetOthersFileFolder();
					#endif
					if(path!=null){
						EditorUtility.RevealInFinder(path);
					}
				}
			}
		}
}