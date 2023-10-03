using UnityEngine;
using UnityEditor;
using System.Collections;
using EasyMobile;

namespace EasyMobile.Editor
{
    // Partial editor class for Utility module.
    public partial class EM_SettingsEditor
    {
        const string UtilityModuleLabel = "UTILITY";

        void UtilityModuleGUI()
        {
            EditorGUILayout.BeginVertical(EM_GUIStyleManager.GetCustomStyle("Module Box"));

            EM_EditorGUI.ModuleLabel(UtilityModuleLabel);

            // Rating Request settings
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("RATING REQUEST SETUP", EditorStyles.boldLabel);

            // Appearance
            EditorGUILayout.LabelField("Appearance", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("All instances of " + RatingRequestSettings.PRODUCT_NAME_PLACEHOLDER + " in titles and messages will be replaced by the actual Product Name given in PlayerSettings.", MessageType.Info);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(RatingRequestProperties.iosDialogContent.property, RatingRequestProperties.iosDialogContent.content, true);
            EditorGUILayout.PropertyField(RatingRequestProperties.androidDialogContent.property, RatingRequestProperties.androidDialogContent.content, true);   
            EditorGUI.indentLevel--;

            // Behaviour
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Behaviour", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(RatingRequestProperties.minimumAcceptedStars.property, RatingRequestProperties.minimumAcceptedStars.content);
            EditorGUILayout.PropertyField(RatingRequestProperties.supportEmail.property, RatingRequestProperties.supportEmail.content);
            EditorGUILayout.PropertyField(RatingRequestProperties.iosAppId.property, RatingRequestProperties.iosAppId.content);
            EditorGUILayout.PropertyField(RatingRequestProperties.annualCap.property, RatingRequestProperties.annualCap.content);

            EditorGUILayout.EndVertical();
        }
    }
}
