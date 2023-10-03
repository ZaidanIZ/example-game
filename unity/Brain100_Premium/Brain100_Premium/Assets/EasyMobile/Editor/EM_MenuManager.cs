using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

namespace EasyMobile.Editor
{
    public class EM_MenuManager : MonoBehaviour
    {

        #region Menu items

        [MenuItem("Window/Easy Mobile/Settings", false)]
        public static void MenuOpenSettings()
        {
            EM_Settings instance = EM_Settings.LoadSettingsAsset();

            if (instance == null)
            {
                instance = EM_BuiltinObjectCreator.CreateEMSettingsAsset();
            }

            Selection.activeObject = instance;
        }

        [MenuItem("Window/Easy Mobile/Create EasyMobile.prefab", false)]
        public static void MenuCreateMainPrefab()
        {
            EM_BuiltinObjectCreator.CreateEasyMobilePrefab(true);
            EM_Manager.CheckModules();
        }

        [MenuItem("Window/Easy Mobile/Reimport Native Package", false)]
        public static void ReimportNativePackage()
        {
            EM_Manager.ImportNativeCode();
        }

        [MenuItem("Window/Easy Mobile/Documentation", false)]
        public static void OpenDocumentation()
        {
            Application.OpenURL(EM_Constants.DocumentationURL);
        }

        #endregion

        #region Context menu items

        [MenuItem("GameObject/Easy Mobile/EasyMobile Instance", false, 10)]
        public static void CreateEasyMobilePrefabInstance(MenuCommand menuCommand)
        {
            GameObject prefab = EM_EditorUtil.GetMainPrefab();
        
            if (prefab == null)
                prefab = EM_BuiltinObjectCreator.CreateEasyMobilePrefab();
        
            // Stop if another instance already exists as a root object in this scene
            GameObject existingInstance = EM_EditorUtil.FindPrefabInstanceInScene(prefab, EditorSceneManager.GetActiveScene());
            if (existingInstance != null)
            {
                Selection.activeObject = existingInstance;
                return;
            }
        
            // Instantiate an EasyMobile prefab at scene root (parentless) because it's a singleton
            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            AddGameObjectToScene(go);
        }

        [MenuItem("GameObject/Easy Mobile/Clip Player", false, 10)]
        public static void CreateClipPlayer(MenuCommand menuCommand)
        {
            GameObject go = EM_BuiltinObjectCreator.CreateClipPlayer(menuCommand.context as GameObject);
            AddGameObjectToScene(go);
        }

        [MenuItem("GameObject/Easy Mobile/Clip Player (UI)", false, 10)]
        public static void CreateClipPlayerUI(MenuCommand menuCommand)
        {
            GameObject go = EM_BuiltinObjectCreator.CreateClipPlayerUI(menuCommand.context as GameObject);
            AddGameObjectToScene(go);
        }

        #endregion

        #region Private Stuff

        // Register undo action for the game object and make it active selection.
        private static void AddGameObjectToScene(GameObject go)
        {
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }

        #endregion
    }
}