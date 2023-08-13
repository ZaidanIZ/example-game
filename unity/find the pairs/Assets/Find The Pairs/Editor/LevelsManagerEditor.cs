using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

namespace IndieSudioFTPEditors
{
		[CustomEditor(typeof(LevelsManager))]
		public class LevelsManagerEditor : Editor
		{
				private int dialogResult = 0;
				private Color tempColor;
				private Color greenColor = Color.green;
				private Color whiteColor = Color.white;
				private Color yellowColor = Color.yellow;
				private Color redColor = new Color (255, 0, 0, 255) / 255.0f;
				private Color cyanColor = new Color (0, 255, 255, 255) / 255.0f;

				public override void OnInspectorGUI ()
				{
						EditorGUILayout.Separator ();
						
						GUILayout.BeginHorizontal ();
						if (GUILayout.Button ("Review Find The Paris", GUILayout.Width (150), GUILayout.Height (25))) {
							Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/40732");
						}
						
						GUI.backgroundColor = greenColor;         
						
						if (GUILayout.Button ("More Assets", GUILayout.Width (110), GUILayout.Height (25))) {
							Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:9268");
						}
						
						GUI.backgroundColor = whiteColor;    

						GUI.backgroundColor = greenColor;         
						
						if (GUILayout.Button ("Visit Our Store", GUILayout.Width (110), GUILayout.Height (25))) {
							Application.OpenURL ("www.indiestd.com");
						}

						GUI.backgroundColor = whiteColor;         
						
						GUILayout.EndHorizontal ();
						
						EditorGUILayout.Separator ();

						LevelsManager attrib = (LevelsManager)target;//get the target


						EditorGUILayout.Separator ();
						EditorGUILayout.LabelField ("Instructions :");
						EditorGUILayout.Separator ();
	
						EditorGUILayout.HelpBox ("* Select number of Rows and Columns to create a Grid of Size equals [Rows x Columns].", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'Create New Level' to create a new Level for the Mission", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'Remove Levels' to remove all Levels in the Mission", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'View Grid' to show the grid of the Level", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'Create New Pair' to create a new pair of elements for the Level ", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'Remove Pairs' to remove all pairs in Level", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'Remove Level' to remove the Level from the Mission", MessageType.None);
						EditorGUILayout.HelpBox ("* Click on 'Remove Pair' to remove the pair from the Level", MessageType.None);

						EditorGUILayout.Separator ();
						EditorGUILayout.HelpBox ("It's recommended to create a grid of even size [Rows x Columns] = [Even Number].", MessageType.Info);
						EditorGUILayout.HelpBox ("To show All levels of Mission make 'Single Level' unchecked.", MessageType.Info);
						EditorGUILayout.HelpBox ("Do not forget to save (ctrl/cmd+s) any change.", MessageType.Info);

						attrib.defaultBackgroundSprite = EditorGUILayout.ObjectField ("Default Background Sprite", attrib.defaultBackgroundSprite, typeof(Sprite), true) as Sprite;
						EditorGUILayout.Separator ();

						attrib.defaultNormalSprite = EditorGUILayout.ObjectField ("Default Normal Sprite", attrib.defaultNormalSprite, typeof(Sprite), true) as Sprite;
						EditorGUILayout.Separator ();

						attrib.defaultOnClickSprite = EditorGUILayout.ObjectField ("Default OnClick Sprite", attrib.defaultOnClickSprite, typeof(Sprite), true) as Sprite;
						EditorGUILayout.Separator ();

						attrib.singleLevel = EditorGUILayout.Toggle ("Single Level", attrib.singleLevel);
						EditorGUILayout.Separator ();

						attrib.randomShuffleOnBuild = EditorGUILayout.Toggle ("Random Shuffle On Build", attrib.randomShuffleOnBuild);
						EditorGUILayout.Separator ();

						attrib.matchGrid = EditorGUILayout.Toggle ("Match Grid", attrib.matchGrid);
						EditorGUILayout.Separator ();

						if (!attrib.matchGrid) {
								attrib.cellSize = EditorGUILayout.Vector2Field ("Cell Size", attrib.cellSize);
								EditorGUILayout.Separator ();
						}
						attrib.spacing = EditorGUILayout.Vector2Field ("Spacing", attrib.spacing);
						EditorGUILayout.Separator ();

						attrib.initialDelay = EditorGUILayout.Slider ("Initial Delay (s)", attrib.initialDelay, 0, 60);
						EditorGUILayout.Separator ();
			
						GUILayout.BeginHorizontal ();

						GUI.backgroundColor = greenColor;         
						if (GUILayout.Button ("Create New Level", GUILayout.Width (150), GUILayout.Height (23))) {
								CreateNewLevel (attrib);
						}

						GUI.backgroundColor = whiteColor;         

						if (attrib.levels.Count != 0) {
								GUI.backgroundColor = redColor;         
								if (GUILayout.Button ("Remove Levels", GUILayout.Width (150), GUILayout.Height (23))) {
										dialogResult = EditorUtility.DisplayDialogComplex ("Removing Levels", "Are you sure you want to remove all levels ?", "yes", "cancel", "close");
										if (dialogResult == 0) {
												RemoveLevels (attrib);
										}
								}
								GUI.backgroundColor = whiteColor;         
						}

						GUILayout.EndHorizontal ();
						GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));
						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();

						for (int i = 0; i < attrib.levels.Count; i++) {
								GUI.contentColor = yellowColor;         
								attrib.levels [i].showLevel = EditorGUILayout.Foldout (attrib.levels [i].showLevel, " [Level " + (i + 1) + "]");
								GUI.contentColor = whiteColor;         

								if (attrib.levels [i].showLevel) {
										EditorGUILayout.Separator ();
					
										EditorGUILayout.Separator ();
										attrib.levels [i].numberOfRows = EditorGUILayout.IntSlider ("Number of Rows", attrib.levels [i].numberOfRows, 2, LevelsManager.rowsLimit);
										EditorGUILayout.Separator ();
										attrib.levels [i].numberOfColumns = EditorGUILayout.IntSlider ("Number of Columns", attrib.levels [i].numberOfColumns, 2, LevelsManager.colsLimit);
										EditorGUILayout.Separator ();

										if (attrib.levels [i].previousNumberOfRows == -1) {
											attrib.levels [i].previousNumberOfRows =attrib.levels [i].numberOfRows;
										}
										
										if (attrib.levels [i].previousNumberOfCols == -1) {
											attrib.levels [i].previousNumberOfCols = attrib.levels [i].numberOfColumns;
										}
										
										if (attrib.levels [i].previousNumberOfCols != attrib.levels [i].numberOfColumns || attrib.levels [i].previousNumberOfRows != attrib.levels [i].numberOfRows) {
											
											if (attrib.levels[i].pairs.Count != 0) {
												dialogResult = EditorUtility.DisplayDialogComplex ("Confirm Message", "Changing grid size leads to reset all pairs", "ok", "cancel", "close");
												if (dialogResult == 0) {
													RemoveLevelPairs (attrib.levels[i],attrib);
												} else {
													attrib.levels [i].numberOfRows = attrib.levels [i].previousNumberOfRows;
													attrib.levels [i].numberOfColumns = attrib.levels [i].previousNumberOfCols;
												}
											} else {
												RemoveLevelPairs (attrib.levels[i],attrib);
											}
										}

										GUI.backgroundColor = cyanColor;         
										GUILayout.BeginHorizontal ();
										if (GUILayout.Button ("View Grid", GUILayout.Width (120), GUILayout.Height (23))) {
											IndieSudioFTPEditors.GridWindowEditor.Init (attrib.levels [i], "Level " + (i + 1) + " Grid", 	attrib.levels [i].numberOfRows, 	attrib.levels [i].numberOfColumns);
										}
										GUI.backgroundColor = whiteColor;         
			
					
										if (GUILayout.Button ("Random Shuffle", GUILayout.Width (150), GUILayout.Height (23))) {
											FPShuffle.RandomPairsShuffle (attrib.levels [i].pairs, 	attrib.levels [i].numberOfRows * 	attrib.levels [i].numberOfColumns);
										}
										GUI.backgroundColor = redColor;         
										if (GUILayout.Button ("Remove Level " + (i + 1), GUILayout.Width (120), GUILayout.Height (23))) {
												dialogResult = EditorUtility.DisplayDialogComplex ("Removing Level", "Are you sure you want to remove level " + (i + 1) + " ?", "yes", "cancel", "close");
												if (dialogResult == 0) {
														RemoveLevel (i, attrib);
														continue;
												}
										}
										GUI.backgroundColor = whiteColor;         

										GUILayout.EndHorizontal ();
										GUILayout.BeginHorizontal();
										GUI.backgroundColor = greenColor;         
										if (GUILayout.Button ("Create New Pair", GUILayout.Width (110), GUILayout.Height (23))) {
											if (attrib.levels [i].pairs.Count < 	attrib.levels [i].numberOfRows * 	attrib.levels [i].numberOfColumns / 2) {
												CreateNewPair (attrib, attrib.levels [i]);
											} else {
												EditorUtility.DisplayDialog ("Limit Reached", "You can't add more pairs", "ok");
											}
										}
										GUI.backgroundColor = whiteColor; 

										GUI.backgroundColor = redColor; 
										if (attrib.levels[i].pairs.Count != 0)
										if (GUILayout.Button ("Remove Pairs", GUILayout.Width (110), GUILayout.Height (23))) {
											bool isOk = EditorUtility.DisplayDialog ("Removing Level Pairs", "Are you sure you want to remove the pairs of Level" + (i + 1) + " ?", "yes", "cancel");
											if (isOk) {
												RemoveLevelPairs (attrib.levels[i], attrib);
												return;
											}
										}
										
										GUI.backgroundColor = whiteColor;
										EditorGUILayout.EndHorizontal();
										EditorGUILayout.Separator ();
										attrib.levels [i].timeLimit = EditorGUILayout.IntSlider ("Time Limit (s)", attrib.levels [i].timeLimit, 0, 500);
										EditorGUILayout.Separator ();

										attrib.levels [i].threeStarsTimePeriod = EditorGUILayout.IntSlider ("Three Stars Time Period (s)", attrib.levels [i].threeStarsTimePeriod, 0, attrib.levels [i].timeLimit);
										EditorGUILayout.Separator ();

										attrib.levels [i].twoStarsTimePeriod = EditorGUILayout.IntSlider ("Two Stars Time Period (s)", attrib.levels [i].twoStarsTimePeriod, 0, attrib.levels [i].threeStarsTimePeriod - (attrib.levels[i].threeStarsTimePeriod >= 1 ?1:0));
										EditorGUILayout.Separator ();

										EditorGUILayout.Separator ();
										GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));

										EditorGUILayout.Separator ();

										for (int j = 0; j < attrib.levels[i].pairs.Count; j++) {
												attrib.levels [i].pairs [j].showPair = EditorGUILayout.Foldout (attrib.levels [i].pairs [j].showPair, "Pair " + (j + 1));

												if (attrib.levels [i].pairs [j].showPair) {

														GUILayout.BeginHorizontal ();
														GUI.backgroundColor = redColor;        
														if (GUILayout.Button ("Remove Pair " + (j + 1), GUILayout.Width (120), GUILayout.Height (25))) {
															bool isOk = EditorUtility.DisplayDialog ("Removing Pair", "Are you sure you want to remove pair" + (j + 1) + " ?", "yes", "cancel");
															if (isOk) {
																RemovePair (j, attrib.levels [i], attrib);
																continue;
															}
														}
														GUI.backgroundColor = whiteColor;         
														GUILayout.EndHorizontal ();
														EditorGUILayout.Separator ();

														attrib.levels [i].pairs [j].backgroundSprite = EditorGUILayout.ObjectField ("Background Sprite", attrib.levels [i].pairs [j].backgroundSprite, typeof(Sprite), true) as Sprite;
														EditorGUILayout.Separator ();

														attrib.levels [i].pairs [j].normalSprite = EditorGUILayout.ObjectField ("Normal Sprite", attrib.levels [i].pairs [j].normalSprite, typeof(Sprite), true) as Sprite;
														EditorGUILayout.Separator ();

														attrib.levels [i].pairs [j].onClickSprite = EditorGUILayout.ObjectField ("On Click Sprite", attrib.levels [i].pairs [j].onClickSprite, typeof(Sprite), true) as Sprite;
														EditorGUILayout.Separator ();
						
														attrib.levels [i].pairs [j].firstElement.index = EditorGUILayout.IntSlider ("First Element Index", attrib.levels [i].pairs [j].firstElement.index, 0, attrib.levels[i].numberOfRows * attrib.levels[i].numberOfColumns - 1);
														EditorGUILayout.Separator ();
														attrib.levels [i].pairs [j].secondElement.index = EditorGUILayout.IntSlider ("Second Element Index", attrib.levels [i].pairs [j].secondElement.index, 0, attrib.levels[i].numberOfRows * attrib.levels[i].numberOfColumns - 1);
												}
										}
								}
						}
					#if UNITY_5
						if (GUI.changed && !EditorApplication.isSceneDirty) 
						{
							EditorApplication.MarkSceneDirty();  
						}
					#endif
				}

				private void CreateNewLevel (LevelsManager attrib)
				{
						if (attrib == null) {
								return;
						}

						Level lvl = new Level ();
						attrib.levels.Add (lvl);
						CreateLevelPairs (attrib, lvl);
						FPShuffle.RandomPairsShuffle (lvl.pairs, lvl.numberOfRows * lvl.numberOfColumns);
				}

				private void CreateLevelPairs (LevelsManager attrib, Level lvl)
				{
						if (lvl == null) {
								return;
						}

						int numberOfPairs = lvl.numberOfRows * lvl.numberOfColumns / 2;
						while (lvl.pairs.Count < numberOfPairs) {
								Level.Pair pair = new Level.Pair ();
								pair.normalSprite = attrib.defaultNormalSprite;
								pair.onClickSprite = attrib.defaultOnClickSprite;
								pair.backgroundSprite = attrib.defaultBackgroundSprite;
								lvl.pairs.Add (pair);
						}
				}

				private void RemoveLevels (LevelsManager attrib)
				{
						if (attrib == null) {
								return;
						}
						attrib.levels.Clear ();
				}

				private void RemoveLevel (int index, LevelsManager attrib)
				{
						if (!(index >= 0 && index < attrib.levels.Count)) {
								return;
						}

						attrib.levels.RemoveAt (index);
				}


			public Level.Pair CreateNewPair (LevelsManager attrib, Level lvl)
			{
				if (lvl == null) {
					return null;
				}

				Level.Pair pair = new Level.Pair ();
				pair.normalSprite = attrib.defaultNormalSprite;
				pair.onClickSprite = attrib.defaultOnClickSprite;
				pair.backgroundSprite = attrib.defaultBackgroundSprite;

				lvl.pairs.Add (pair);
				return pair;
			}

			public static void RemoveLevelPairs (Level level, LevelsManager attrib)
			{
				if (level == null) {
					return;
				}
				level.pairs.Clear ();
				level.previousNumberOfRows = level.numberOfRows;
				level.previousNumberOfCols = level.numberOfColumns;
			}
			
			public static void RemovePair (int index, Level level, LevelsManager attrib)
			{
				if (level == null) {
					return;
				}
				
				if (!(index >= 0 && index < level.pairs.Count)) {
					return;
				}
				
				level.pairs.RemoveAt (index);
			}

		}
}