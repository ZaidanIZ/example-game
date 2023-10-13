﻿using UnityEngine;
using System.Collections;

namespace EasyMobile.Editor
{
    public static class EM_Constants
    {
        // Current version
        public const string versionString = "1.1.0";
        public const int versionInt = 0x0110;

        // Folder
        public const string RootPath = "Assets/EasyMobile";
        public const string EditorFolder = RootPath + "/Editor";
        public const string TemplateFolder = EditorFolder + "/Templates";
        public const string GeneratedFolder = RootPath + "/Generated";
        public const string MainPrefabFolder = RootPath;
        public const string MaterialsFolder = RootPath + "/Materials";
        public const string NativeFolder = RootPath + "/Native";
        public const string SkinFolder = RootPath + "/GUISkins";
        public const string SkinTextureFolder = SkinFolder + "/Textures";
        public const string ResourcesFolder = RootPath + "/Resources";
        public const string ScriptsFolder = RootPath + "/Scripts";
        public const string ReceiptValidationFolder = "Assets/Plugins/UnityPurchasing/generated";

        // Asset and stuff
        public const string SettingsAssetName = "EM_Settings";
        public const string SettingsAssetExtension = ".asset";
        public const string SettingsAssetPath = ResourcesFolder + "/EM_Settings.asset";
        public const string MainPrefabName = "EasyMobile";
        public const string PrefabExtension = ".prefab";
        public const string MainPrefabPath = MainPrefabFolder + "/EasyMobile.prefab";
        public const string NativePackagePath = NativeFolder + "/EasyMobileNative.unitypackage";
        public const string ClipPlayerMaterialPath = MaterialsFolder + "/ClipPlayerMat.mat";

        // Generated class names
        public const string RootNameSpace = "EasyMobile";
        public const string AndroidGPGSConstantClassName = "EM_GPGSIds";
        public const string GameServiceConstantsClassName = "EM_GameServiceConstants";
        public const string IAPConstantsClassName = "EM_IAPConstants";

        // URLs
        public const string DocumentationURL = "https://sglibgames.gitbooks.io/easy-mobile-user-guide/content/";
        public const string SupportEmail = "sglib.games@gmail.com";

        // Common symbols
        public const string NoneSymbol = "[None]";
        public const string DeleteSymbol = "-";
        public const string UpSymbol = "↑";
        public const string DownSymbol = "↓";

        // ProjectSettings keys
        public const string PSK_EMVersionString = "VERSION";
        public const string PSK_EMVersionInt = "VERSION_INT";
        public const string PSK_ImportedNativeCode = "IMPORTED_NATIVE_CODE";
    }
}
