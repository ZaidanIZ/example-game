/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script controla la administracion de coins, palabras, imagenes y traducciones del objeto Words_Database.
/// 
///----------- ENGLISH -----------
/// 
/// This script controls the administration of coins, words, images and translations of "Words_Database" object.
/// 
/// </summary>
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Word_Database))]
public class Words_Database_Editor : Editor
{

	int numberWordError;
	Word_Database wordDb;
	string[] allPathImages;
	bool showAutomaticSystemOfAdd = false;
	//variable for add all images automatically
	string wordWithoutNumber;
	string wordWithoutHyphen;
    string nameFolderOfImages;

	public override void OnInspectorGUI ()
	{

        if (!EditorPrefs.HasKey ("showWords")) {
			EditorPrefs.SetBool ("showWords", false);
		}
		if (!EditorPrefs.HasKey ("showConfigs")) {
			EditorPrefs.SetBool ("showConfigs", false);
		}
		if (!EditorPrefs.HasKey ("showTranslations")) {
			EditorPrefs.SetBool ("showTranslations", false);
		}
		if (!EditorPrefs.HasKey ("showChangeTextsUI")) {
			EditorPrefs.SetBool ("showChangeTextsUI", false);
		}

		wordDb = (Word_Database)target;

        EditorGUILayout.LabelField("How many quiz solved need to unlock this category?");
        wordDb.howManyQuizSolvedNeedToUnlock = EditorGUILayout.IntField(wordDb.howManyQuizSolvedNeedToUnlock);

        EditorGUILayout.LabelField("Name of this category - Example: Level X");
        for (int i = 0; i < wordDb.languagesName.Length; i++)
        {
            wordDb.nameOfCategory[i] = EditorGUILayout.TextField("Name in " + wordDb.languagesName[i], wordDb.nameOfCategory[i]);
        }

        if (GUILayout.Button ("CONFIGURATIONS")) {
			if (EditorPrefs.GetBool ("showConfigs")) {
				EditorPrefs.SetBool ("showConfigs", false);
			} else {
				EditorPrefs.SetBool ("showConfigs", true);
			}
		}


		if (EditorPrefs.GetBool ("showConfigs")) {

			EditorGUILayout.LabelField ("-------------------");
			EditorGUILayout.LabelField ("Coins options", EditorStyles.boldLabel);
			EditorGUILayout.LabelField ("-------------------");
			EditorGUILayout.LabelField ("With how many coins the player will start?:");
			wordDb.startWithCoins = EditorGUILayout.IntField ("", wordDb.startWithCoins, GUILayout.Width (85));
			EditorGUILayout.LabelField ("-------------------");
			EditorGUILayout.LabelField ("How many coins the player earn by solved word?:");
			wordDb.coinsWinedByWord = EditorGUILayout.IntField ("", wordDb.coinsWinedByWord, GUILayout.Width (85));
			EditorGUILayout.LabelField ("-------------------");
			EditorGUILayout.LabelField ("(HELP) Cost to show one letter of the solution:");
			wordDb.coinsToShowOneLetter = EditorGUILayout.IntField ("", wordDb.coinsToShowOneLetter, GUILayout.Width (85));
			EditorGUILayout.LabelField ("-------------------");
			EditorGUILayout.LabelField ("(HELP) Cost to solve the word:");
			wordDb.coinsToSolveWord = EditorGUILayout.IntField ("", wordDb.coinsToSolveWord, GUILayout.Width (85));
            EditorGUILayout.LabelField("-------------------");
            EditorGUILayout.LabelField("Show information later of solve one word?");
            wordDb.showInfoLaterOfSolveWord = EditorGUILayout.Toggle("Yes/No", wordDb.showInfoLaterOfSolveWord);

            EditorGUILayout.LabelField ("__________________________________________________________________________________________________________________________");
		}

		if (GUILayout.Button ("MANAGE THE WORDS/LEVELS AND IMAGES")) {
			if (EditorPrefs.GetBool ("showWords")) {
				EditorPrefs.SetBool ("showWords", false);
			} else {
				EditorPrefs.SetBool ("showWords", true);
			}
		}

		if (EditorPrefs.GetBool ("showWords")) {



			EditorGUILayout.LabelField ("-------------------");
			EditorGUILayout.LabelField ("Note: Use the slash '/' to do a new line.");
			EditorGUILayout.LabelField ("-------------------");
			showAutomaticSystemOfAdd = EditorGUILayout.Toggle ("Automatic System", showAutomaticSystemOfAdd);
			EditorGUILayout.LabelField ("-------------------");
			if (showAutomaticSystemOfAdd) {

				EditorGUILayout.LabelField ("How to add the images automatically?", EditorStyles.boldLabel);
				EditorGUILayout.LabelField ("1. The folder with the images should be in the folder \"Images\"");
				EditorGUILayout.LabelField ("2. The names of the images should be orderer by numbers, followed");
				EditorGUILayout.LabelField ("an underscore \"_\" followed by the name of the image.");
				EditorGUILayout.LabelField ("Examples: 1_LION, 2_TIGER, 3_DOLPHIN");
				EditorGUILayout.LabelField ("3. Clic in the button to add automatically. (All levels added will be replaced)");
                EditorGUILayout.LabelField("-------------------");
                EditorGUILayout.LabelField("Name of the folder with the images (Inside of folder images)");
                nameFolderOfImages = EditorGUILayout.TextField("", nameFolderOfImages);
                string[] folder = { "Assets/Pics Quiz Maker With Categories/Images/" + nameFolderOfImages };
                EditorGUILayout.LabelField("-------------------");
                if (GUILayout.Button ("ADD ALL IMAGES AUTOMATICALLY", GUILayout.Width (300))) {
                    
					allPathImages = AssetDatabase.FindAssets ("", folder);

					System.Array.Resize (ref wordDb.words_List, allPathImages.Length / 2);
					System.Array.Resize (ref wordDb.image, allPathImages.Length / 2);

                    for (int i = 0; i < allPathImages.Length; i = i+2) {

						wordDb.image [i / 2] = AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (allPathImages [i]), typeof(Sprite)) as Sprite;
						wordDb.words_List [i / 2] = wordDb.image [i / 2].name;
						wordDb.words_List [i / 2] = wordDb.words_List [i / 2].ToUpper ();

						for (int b = 0; b < wordDb.words_List[i/2].Length; b++) {

							if (wordDb.words_List [i / 2].Length > 1) {

								if (wordDb.words_List [i / 2] [1].ToString () == "_") {
	
									for (int d = 0; d < wordDb.words_List[i/2].Length; d++) {
	
										if (wordDb.words_List [i / 2].Length > d + 2) {
											wordWithoutNumber = wordWithoutNumber + wordDb.words_List [i / 2] [d + 2].ToString ();
										}
	
									}
	
									wordDb.words_List [i / 2] = wordWithoutNumber;
									wordWithoutNumber = "";

								} 

							}

							if (wordDb.words_List [i / 2].Length > 2) {

								if (wordDb.words_List [i / 2] [2].ToString () == "_") {

									for (int d = 0; d < wordDb.words_List[i/2].Length; d++) {
									
										if (wordDb.words_List [i / 2].Length > d + 3) {
											wordWithoutNumber = wordWithoutNumber + wordDb.words_List [i / 2] [d + 3].ToString ();
										}
									
									}
								
									wordDb.words_List [i / 2] = wordWithoutNumber;
									wordWithoutNumber = "";
	
								} 
							}

							if (wordDb.words_List [i / 2].Length > 3) {

								if (wordDb.words_List [i / 2] [3].ToString () == "_") {
								
									for (int d = 0; d < wordDb.words_List[i/2].Length; d++) {
									
										if (wordDb.words_List [i / 2].Length > d + 4) {
											wordWithoutNumber = wordWithoutNumber + wordDb.words_List [i / 2] [d + 4].ToString ();
										}
									
									}
							
									wordDb.words_List [i / 2] = wordWithoutNumber;
									wordWithoutNumber = "";
								
								} 

							}

							if (wordDb.words_List [i / 2].Length > 4) {
							
								if (wordDb.words_List [i / 2] [4].ToString () == "_") {
								
									for (int d = 0; d < wordDb.words_List[i/2].Length; d++) {
									
										if (wordDb.words_List [i / 2].Length > d + 5) {
											wordWithoutNumber = wordWithoutNumber + wordDb.words_List [i / 2] [d + 5].ToString ();
										}
									
									}
								
									wordDb.words_List [i / 2] = wordWithoutNumber;
									wordWithoutNumber = "";
								
								} 
							
							}

							if (wordDb.words_List [i / 2].Length > 5) {

								if (wordDb.words_List [i / 2] [5].ToString () == "_") {
								
									for (int d = 0; d < wordDb.words_List[i/2].Length; d++) {
									
										if (wordDb.words_List [i / 2].Length > d + 6) {
											wordWithoutNumber = wordWithoutNumber + wordDb.words_List [i / 2] [d + 6].ToString ();
										}
									
									}
								
									wordDb.words_List [i / 2] = wordWithoutNumber;
									wordWithoutNumber = "";
							
								}
							}

						}
						
					}

				}


				if (GUILayout.Button ("CONVERT HYPHEN ( - ) IN SPACE", GUILayout.Width (300))) {

					for (int i = 0; i < wordDb.words_List.Length; i++) {

						if (wordDb.words_List [i].Contains ("-")) {

							for (int b = 0; b < wordDb.words_List[i].Length; b++) {
							
								if (wordDb.words_List [i] [b].ToString () != "-") {
									wordWithoutHyphen = wordWithoutHyphen + wordDb.words_List [i] [b];
								} else {
									wordWithoutHyphen = wordWithoutHyphen + " ";
								}
							
							}

							wordDb.words_List [i] = wordWithoutHyphen;
							wordWithoutHyphen = "";

						}

					}

				}

				EditorGUILayout.LabelField ("-------------------");
				EditorGUILayout.LabelField ("This button detect where should have the slash each word to do a new line.");

				if (GUILayout.Button ("PUT THE SLASHES AUTOMATICALLY", GUILayout.Width (300))) {

					int numberSpaces = 0;
					string wordConvertedWithSlahs = "";

					for (int i = 0; i < wordDb.words_List.Length; i++) {
					
						if (wordDb.words_List [i].Length >= 11 && wordDb.words_List [i].Contains (" ")) {
				
							for (int b = 0; b < wordDb.words_List[i].Length; b++) {
													
								if (wordDb.words_List [i] [b].ToString () == " ") {

									numberSpaces += 1;

								}
													
							}

							if (numberSpaces == 1) {

								for (int b = 0; b < wordDb.words_List[i].Length; b++) {
								
									if (wordDb.words_List [i] [b].ToString () != " ") {
									
										wordConvertedWithSlahs = wordConvertedWithSlahs + wordDb.words_List [i] [b];
									
									} else {

										wordConvertedWithSlahs = wordConvertedWithSlahs + "/";

									}
								
								}

								wordDb.words_List [i] = wordConvertedWithSlahs;
								wordConvertedWithSlahs = "";


							} else if (numberSpaces == 2) {

								int counterSpaces = 0;

								for (int b = 0; b < wordDb.words_List[i].Length; b++) {
								
									if (wordDb.words_List [i] [b].ToString () != " ") {
									
										wordConvertedWithSlahs = wordConvertedWithSlahs + wordDb.words_List [i] [b];
									
									} else {
									
										counterSpaces++;
										if (counterSpaces == 2) {
											wordConvertedWithSlahs = wordConvertedWithSlahs + "/";
										} else {
											wordConvertedWithSlahs = wordConvertedWithSlahs + " ";
										}
									
									}
								
								}
							
								wordDb.words_List [i] = wordConvertedWithSlahs;
								wordConvertedWithSlahs = "";
							
							
							} else if (numberSpaces == 3) {
							
								int counterSpaces = 0;
							
								for (int b = 0; b < wordDb.words_List[i].Length; b++) {
								
									if (wordDb.words_List [i] [b].ToString () != " ") {
									
										wordConvertedWithSlahs = wordConvertedWithSlahs + wordDb.words_List [i] [b];
									
									} else {
									
										counterSpaces++;
										if (counterSpaces == 3) {
											wordConvertedWithSlahs = wordConvertedWithSlahs + "/";
										} else {
											wordConvertedWithSlahs = wordConvertedWithSlahs + " ";
										}
									
									}
								
								}
							
								wordDb.words_List [i] = wordConvertedWithSlahs;
								wordConvertedWithSlahs = "";
							
							
							} else if (numberSpaces == 4) {
							
								int counterSpaces = 0;
							
								for (int b = 0; b < wordDb.words_List[i].Length; b++) {
								
									if (wordDb.words_List [i] [b].ToString () != " ") {
									
										wordConvertedWithSlahs = wordConvertedWithSlahs + wordDb.words_List [i] [b];
									
									} else {
									
										counterSpaces++;
										if (counterSpaces == 4) {
											wordConvertedWithSlahs = wordConvertedWithSlahs + "/";
										} else {
											wordConvertedWithSlahs = wordConvertedWithSlahs + " ";
										}
									
									}
								
								}
							
								wordDb.words_List [i] = wordConvertedWithSlahs;
								wordConvertedWithSlahs = "";
							
							
							} else if (numberSpaces == 5) {
							
								int counterSpaces = 0;
							
								for (int b = 0; b < wordDb.words_List[i].Length; b++) {
								
									if (wordDb.words_List [i] [b].ToString () != " ") {
									
										wordConvertedWithSlahs = wordConvertedWithSlahs + wordDb.words_List [i] [b];
									
									} else {
									
										counterSpaces++;
										if (counterSpaces == 5) {
											wordConvertedWithSlahs = wordConvertedWithSlahs + "/";
										} else {
											wordConvertedWithSlahs = wordConvertedWithSlahs + " ";
										}
									
									}
								
								}
							
								wordDb.words_List [i] = wordConvertedWithSlahs;
								wordConvertedWithSlahs = "";
							
							
							} else if (numberSpaces == 6) {
							
								int counterSpaces = 0;
							
								for (int b = 0; b < wordDb.words_List[i].Length; b++) {
								
									if (wordDb.words_List [i] [b].ToString () != " ") {
									
										wordConvertedWithSlahs = wordConvertedWithSlahs + wordDb.words_List [i] [b];
									
									} else {
									
										counterSpaces++;
										if (counterSpaces == 6) {
											wordConvertedWithSlahs = wordConvertedWithSlahs + "/";
										} else {
											wordConvertedWithSlahs = wordConvertedWithSlahs + " ";
										}
									
									}
								
								}
							
								wordDb.words_List [i] = wordConvertedWithSlahs;
								wordConvertedWithSlahs = "";
							
							
							}

						}

						numberSpaces = 0;
					
					}



				}

				if (GUILayout.Button ("FIND ERRORS", GUILayout.Width (300))) {

					int positionSlash = 0;

					for (int i = 0; i < wordDb.words_List.Length; i++) {

						if (wordDb.words_List [i] != "" && wordDb.words_List [i] != null) {

							for (int b = 0; b < wordDb.words_List[i].Length; b++) {

								positionSlash++;
								if (wordDb.words_List [i] [b].ToString () == "/") {

									if (positionSlash >= 13) {
										Debug.LogWarning ("SLASH IN BAD POSITION, ERROR IN LEVEL : " + (i + 1));
									}

								}

							}
						} else {

							Debug.LogError ("Level without word: #" + (i + 1));

						}

						positionSlash = 0;

					}

			


					int numberSlashes = 0;
			
					for (int i = 0; i < wordDb.words_List.Length; i++) {
				
						if (wordDb.words_List [i] != "" && wordDb.words_List [i] != null) {

							for (int b = 0; b < wordDb.words_List[i].Length; b++) {

								if (wordDb.words_List [i] [b].ToString () == "/") {
						
									numberSlashes++;

									if (numberSlashes >= 2) {
										Debug.LogError ("MORE THAN 2 SLASHES IN LEVEL: " + (i + 1));
									}
						
								}
							}
					
						} else {
						
							Debug.LogError ("Level without word: #" + (i + 1));
							
						}
				
						numberSlashes = 0;
				
					}



					for (int i = 0; i < wordDb.words_List.Length; i++) {

						if (wordDb.words_List [i] != null && wordDb.words_List [i] != "") {
							if (wordDb.words_List [i].Length > 22) {
								Debug.LogError ("Word with more than 22 characters: #" + (i + 1));
							}
						}
					
					
					}

				}
				
				EditorGUILayout.LabelField ("-------------------");
			}
			//EditorGUIUtility.labelWidth = 110;
			EditorGUILayout.LabelField ("Default language (Example: ENGLISH)", EditorStyles.boldLabel);
			wordDb.languagesName [0] = EditorGUILayout.TextField ("", wordDb.languagesName [0], GUILayout.Width (85));
			EditorGUILayout.LabelField ("-------------------");

					
			bool moreThan22Letters = false;
		
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.BeginVertical ();

		
			for (int i = 0; i < wordDb.words_List.Length; i++) {
				if (GUILayout.Button ("X", GUILayout.Width (18), GUILayout.Height (15))) {
	
					for (int b = i; b < wordDb.words_List.Length - 1; b++) {
	
						wordDb.words_List [b] = wordDb.words_List [b + 1];
						wordDb.image [b] = wordDb.image [b + 1];

						//IF EXIST TRANSLATIONS
						if (wordDb.languagesName.Length >= 2) {
							wordDb.language1 [b] = wordDb.language1 [b + 1];
						}
						if (wordDb.languagesName.Length >= 3) {
							wordDb.language2 [b] = wordDb.language2 [b + 1];
						}
						if (wordDb.languagesName.Length >= 4) {
							wordDb.language3 [b] = wordDb.language3 [b + 1];
						}
						if (wordDb.languagesName.Length >= 5) {
							wordDb.language4 [b] = wordDb.language4 [b + 1];
						}
						if (wordDb.languagesName.Length >= 6) {
							wordDb.language5 [b] = wordDb.language5 [b + 1];
						}
						if (wordDb.languagesName.Length >= 7) {
							wordDb.language6 [b] = wordDb.language6 [b + 1];
						}
						if (wordDb.languagesName.Length >= 8) {
							wordDb.language7 [b] = wordDb.language7 [b + 1];
						}
						if (wordDb.languagesName.Length >= 9) {
							wordDb.language8 [b] = wordDb.language8 [b + 1];
						}
						if (wordDb.languagesName.Length >= 10) {
							wordDb.language9 [b] = wordDb.language9 [b + 1];
						}
						if (wordDb.languagesName.Length >= 11) {
							wordDb.language10 [b] = wordDb.language10 [b + 1];
						}
						if (wordDb.languagesName.Length >= 12) {
							wordDb.language11 [b] = wordDb.language11 [b + 1];
						}
						if (wordDb.languagesName.Length >= 13) {
							wordDb.language12 [b] = wordDb.language12 [b + 1];
						}
						if (wordDb.languagesName.Length == 14) {
							wordDb.language13 [b] = wordDb.language13 [b + 1];
						}
							
					}
	
					//IF EXIST TRANSLATIONS
					if (wordDb.languagesName.Length >= 2) {
						System.Array.Resize <string> (ref wordDb.language1, wordDb.language1.Length - 1);
					}
					if (wordDb.languagesName.Length >= 3) {
						System.Array.Resize (ref wordDb.language2, wordDb.language2.Length - 1);
					}
					if (wordDb.languagesName.Length >= 4) {
						System.Array.Resize (ref wordDb.language3, wordDb.language3.Length - 1);
					}
					if (wordDb.languagesName.Length >= 5) {
						System.Array.Resize (ref wordDb.language4, wordDb.language4.Length - 1);
					}
					if (wordDb.languagesName.Length >= 6) {
						System.Array.Resize (ref wordDb.language5, wordDb.language5.Length - 1);
					}
					if (wordDb.languagesName.Length >= 7) {
						System.Array.Resize (ref wordDb.language6, wordDb.language6.Length - 1);
					}
					if (wordDb.languagesName.Length >= 8) {
						System.Array.Resize (ref wordDb.language7, wordDb.language7.Length - 1);
					}
					if (wordDb.languagesName.Length >= 9) {
						System.Array.Resize (ref wordDb.language8, wordDb.language8.Length - 1);
					}
					if (wordDb.languagesName.Length >= 10) {
						System.Array.Resize (ref wordDb.language9, wordDb.language9.Length - 1);
					}
					if (wordDb.languagesName.Length >= 11) {
						System.Array.Resize (ref wordDb.language10, wordDb.language10.Length - 1);
					}
					if (wordDb.languagesName.Length >= 12) {
						System.Array.Resize (ref wordDb.language11, wordDb.language11.Length - 1);
					}
					if (wordDb.languagesName.Length >= 13) {
						System.Array.Resize (ref wordDb.language12, wordDb.language12.Length - 1);
					}
					if (wordDb.languagesName.Length == 14) {
						System.Array.Resize (ref wordDb.language13, wordDb.language13.Length - 1);
					}

					System.Array.Resize (ref wordDb.words_List, wordDb.words_List.Length - 1);
					System.Array.Resize (ref wordDb.image, wordDb.image.Length - 1);

                }
				
			}
			
			EditorGUILayout.EndVertical ();
	
			EditorGUILayout.BeginVertical ();
	
			for (int i = 0; i < wordDb.words_List.Length; i++) {
	
				EditorGUIUtility.labelWidth = 70;
				wordDb.words_List [i] = EditorGUILayout.TextField ("Word #" + (i + 1), wordDb.words_List [i]);
	
				if (wordDb.words_List [i] != null) {
					if (wordDb.words_List [i].Length > 23) {
						moreThan22Letters = true;
						numberWordError = i;
					}
				}
	
	
			}
	
			EditorGUILayout.EndVertical ();
			EditorGUILayout.BeginVertical ();
	
			for (int i = 0; i < wordDb.image.Length; i++) {

				wordDb.image [i] = EditorGUILayout.ObjectField ("", wordDb.image [i], typeof(Object), false) as Sprite;

			}
	
			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();
	
			if (moreThan22Letters) {
				EditorGUILayout.LabelField ("<----- WARNING ----->");
				EditorGUILayout.LabelField ("Error in word #" + (numberWordError + 1) + ":");
				EditorGUILayout.LabelField ("You cant have more than 22 letters in a word.");
				EditorGUILayout.LabelField ("<----------------------->");
			}
	
	
			GUILayout.BeginHorizontal ();
			
			GUILayout.FlexibleSpace ();

			if (GUILayout.Button ("ADD NEW WORD", GUILayout.Width (200))) {
				AddWord ();
			}


			GUILayout.FlexibleSpace ();
			
			GUILayout.EndHorizontal ();

			EditorGUILayout.LabelField ("__________________________________________________________________________________________________________________________");
			

			
			
		}


		


		if (GUILayout.Button ("TRANSLATION OF WORDS")) {
			if (EditorPrefs.GetBool ("showTranslations")) {
				EditorPrefs.SetBool ("showTranslations", false);
			} else {
				EditorPrefs.SetBool ("showTranslations", true);
			}
		}

		if (EditorPrefs.GetBool ("showTranslations")) {

			EditorGUILayout.LabelField ("Default language: " + wordDb.languagesName [0]);


	

			//LANGUAGES NAME
			
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.BeginVertical ();


			for (int i = 1; i < wordDb.languagesName.Length; i++) {

			

				if (GUILayout.Button ("X", GUILayout.Width (18), GUILayout.Height (15))) {
					
					for (int u = i; u < wordDb.languagesName.Length - 1; u++) {
						
						wordDb.languagesName [u] = wordDb.languagesName [u + 1];
						
					}


					if (i == 1) {

						System.Array.Copy (wordDb.language2, wordDb.language1, wordDb.language2.Length);
						System.Array.Copy (wordDb.language3, wordDb.language2, wordDb.language3.Length);
						System.Array.Copy (wordDb.language4, wordDb.language3, wordDb.language4.Length);
						System.Array.Copy (wordDb.language5, wordDb.language4, wordDb.language5.Length);
						System.Array.Copy (wordDb.language6, wordDb.language5, wordDb.language6.Length);
						System.Array.Copy (wordDb.language7, wordDb.language6, wordDb.language7.Length);
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);
												
					} else

					if (i == 2) {

						System.Array.Copy (wordDb.language3, wordDb.language2, wordDb.language3.Length);
						System.Array.Copy (wordDb.language4, wordDb.language3, wordDb.language4.Length);
						System.Array.Copy (wordDb.language5, wordDb.language4, wordDb.language5.Length);
						System.Array.Copy (wordDb.language6, wordDb.language5, wordDb.language6.Length);
						System.Array.Copy (wordDb.language7, wordDb.language6, wordDb.language7.Length);
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);


					} else

					if (i == 3) {

						System.Array.Copy (wordDb.language4, wordDb.language3, wordDb.language4.Length);
						System.Array.Copy (wordDb.language5, wordDb.language4, wordDb.language5.Length);
						System.Array.Copy (wordDb.language6, wordDb.language5, wordDb.language6.Length);
						System.Array.Copy (wordDb.language7, wordDb.language6, wordDb.language7.Length);
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);

					} else

					if (i == 4) {


						System.Array.Copy (wordDb.language5, wordDb.language4, wordDb.language5.Length);
						System.Array.Copy (wordDb.language6, wordDb.language5, wordDb.language6.Length);
						System.Array.Copy (wordDb.language7, wordDb.language6, wordDb.language7.Length);
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);

						
					} else

					if (i == 5) {


						System.Array.Copy (wordDb.language6, wordDb.language5, wordDb.language6.Length);
						System.Array.Copy (wordDb.language7, wordDb.language6, wordDb.language7.Length);
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);

						
					} else

					if (i == 6) {
						

						System.Array.Copy (wordDb.language7, wordDb.language6, wordDb.language7.Length);
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);

						
					} else

					if (i == 7) {
						
						System.Array.Copy (wordDb.language8, wordDb.language7, wordDb.language8.Length);
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);

						
					} else

					if (i == 8) {
						
						System.Array.Copy (wordDb.language9, wordDb.language8, wordDb.language9.Length);
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);

						
					} else

					if (i == 9) {
						
						System.Array.Copy (wordDb.language10, wordDb.language9, wordDb.language10.Length);
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);
					
						
					} else

					if (i == 10) {
						
						System.Array.Copy (wordDb.language11, wordDb.language10, wordDb.language11.Length);
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);
					
						
					} else

					if (i == 11) {
						
						System.Array.Copy (wordDb.language12, wordDb.language11, wordDb.language12.Length);
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);
					
						
					} else

					if (i == 12) {
						
						System.Array.Copy (wordDb.language13, wordDb.language12, wordDb.language13.Length);
				
						
					}


					System.Array.Resize (ref wordDb.languagesName, wordDb.languagesName.Length - 1);

					if (wordDb.languagesName.Length == 1) {
						
						System.Array.Resize (ref wordDb.language1, 0);
						
					} else if (wordDb.languagesName.Length == 2) {

						System.Array.Resize (ref wordDb.language2, 0);

					} else if (wordDb.languagesName.Length == 3) {
						
						System.Array.Resize (ref wordDb.language3, 0);
						
					} else if (wordDb.languagesName.Length == 4) {
						
						System.Array.Resize (ref wordDb.language4, 0);
						
					} else if (wordDb.languagesName.Length == 5) {
						
						System.Array.Resize (ref wordDb.language5, 0);
						
					} else if (wordDb.languagesName.Length == 6) {
						
						System.Array.Resize (ref wordDb.language6, 0);
						
					} else if (wordDb.languagesName.Length == 7) {
						
						System.Array.Resize (ref wordDb.language7, 0);
						
					} else if (wordDb.languagesName.Length == 8) {
						
						System.Array.Resize (ref wordDb.language8, 0);
						
					} else if (wordDb.languagesName.Length == 9) {
						
						System.Array.Resize (ref wordDb.language9, 0);
						
					} else if (wordDb.languagesName.Length == 10) {
						
						System.Array.Resize (ref wordDb.language10, 0);
						
					} else if (wordDb.languagesName.Length == 11) {
						
						System.Array.Resize (ref wordDb.language11, 0);
						
					} else if (wordDb.languagesName.Length == 12) {
						
						System.Array.Resize (ref wordDb.language12, 0);
						
					} else if (wordDb.languagesName.Length == 13) {
						
						System.Array.Resize (ref wordDb.language13, 0);
						
					}
						
	
				}


				for (int h = 0; h < wordDb.words_List.Length; h++) {
					EditorGUILayout.LabelField ("", GUILayout.Width (18), GUILayout.Height (15.994f));
				}
				
				
			}
			
			EditorGUILayout.EndVertical ();
			EditorGUILayout.BeginVertical ();

			if (wordDb.languagesName.Length >= 2) {

				EditorGUIUtility.labelWidth = 125;
		
				wordDb.languagesName [1] = EditorGUILayout.TextField ("Language name #1", wordDb.languagesName [1]);

				for (int b = 0; b < wordDb.words_List.Length; b++) {

					System.Array.Resize (ref wordDb.language1, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language1 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language1 [b]);

				}


			}


			if (wordDb.languagesName.Length >= 3) {

				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [2] = EditorGUILayout.TextField ("Language name #2", wordDb.languagesName [2]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
				
					System.Array.Resize (ref wordDb.language2, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language2 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language2 [b]);

				}
				
			}



			if (wordDb.languagesName.Length >= 4) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [3] = EditorGUILayout.TextField ("Language name #3", wordDb.languagesName [3]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language3, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language3 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language3 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 5) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [4] = EditorGUILayout.TextField ("Language name #4", wordDb.languagesName [4]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language4, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language4 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language4 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 6) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [5] = EditorGUILayout.TextField ("Language name #5", wordDb.languagesName [5]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language5, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language5 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language5 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 7) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [6] = EditorGUILayout.TextField ("Language name #6", wordDb.languagesName [6]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language6, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language6 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language6 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 8) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [7] = EditorGUILayout.TextField ("Language name #7", wordDb.languagesName [7]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language7, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language7 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language7 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 9) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [8] = EditorGUILayout.TextField ("Language name #8", wordDb.languagesName [8]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language8, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language8 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language8 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 10) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [9] = EditorGUILayout.TextField ("Language name #9", wordDb.languagesName [9]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language9, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language9 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language9 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 11) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [10] = EditorGUILayout.TextField ("Language name #10", wordDb.languagesName [10]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language10, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language10 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language10 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 12) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [11] = EditorGUILayout.TextField ("Language name #11", wordDb.languagesName [11]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language11, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language11 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language11 [b]);
					
				}
			}

			if (wordDb.languagesName.Length >= 13) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [12] = EditorGUILayout.TextField ("Language name #12", wordDb.languagesName [12]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language12, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language12 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language12 [b]);
					
				}

			}

			if (wordDb.languagesName.Length >= 14) {
				
				EditorGUIUtility.labelWidth = 125;
				wordDb.languagesName [13] = EditorGUILayout.TextField ("Language name #13", wordDb.languagesName [13]);
				
				for (int b = 0; b < wordDb.words_List.Length; b++) {
					
					System.Array.Resize (ref wordDb.language13, wordDb.words_List.Length);
					EditorGUIUtility.labelWidth = 140;
					wordDb.language13 [b] = EditorGUILayout.TextField (wordDb.words_List [b], wordDb.language13 [b]);
					
				}
			}
			
			
			
			
			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();
			
			//END LANGUAGES NAME

			if (wordDb.languagesName.Length > 14) {
				
				System.Array.Resize (ref wordDb.languagesName, 14);
				
			}
			
			if (wordDb.languagesName.Length == 14) {
				
				EditorGUILayout.LabelField ("You can't have more than 13 translations.");
							
			}

	

			GUILayout.BeginHorizontal ();
			
			GUILayout.FlexibleSpace ();
			
			
			if (GUILayout.Button ("ADD NEW LANGUAGE", GUILayout.Width (200))) {
				AddLanguage ();
				Debug.LogWarning ("REMEMBER THAT FOR EACH LANGUAGE YOU NEED ADD THE TRANSLATIONS OF THE UI.");
			}
			
			GUILayout.FlexibleSpace ();
			
			GUILayout.EndHorizontal ();

			EditorGUILayout.LabelField ("__________________________________________________________________________________________________________________________");
		
			
		}


		if (GUILayout.Button ("HOW TO CHANGE THE TRANSLATIONS OF THE UI?"))
        {
			if (EditorPrefs.GetBool ("showChangeTextsUI")) {
				EditorPrefs.SetBool ("showChangeTextsUI", false);
			} else {
				EditorPrefs.SetBool ("showChangeTextsUI", true);
			}
		}
		
		if (EditorPrefs.GetBool ("showChangeTextsUI"))
        {

			EditorGUILayout.LabelField ("STEP 1: Open the Scripts folder.");
			EditorGUILayout.LabelField ("STEP 2: Open Word_Database script.");
			EditorGUILayout.LabelField ("STEP 3: Read the instructions of the commented lines.");

		}

        if (wordDb.showInfoLaterOfSolveWord)
        {
            if (GUILayout.Button("QUIZ INFORMATIONS"))
            {
                if (EditorPrefs.GetBool("showInfos"))
                {
                    EditorPrefs.SetBool("showInfos", false);
                }
                else
                {
                    EditorPrefs.SetBool("showInfos", true);
                }

            }

            if (EditorPrefs.GetBool("showInfos"))
            {
                System.Array.Resize(ref wordDb.words_List, wordDb.words_List.Length);
                EditorGUILayout.LabelField("Information that will appear when the player finished one quiz.");
                EditorGUILayout.LabelField("(Write in the different languages)");
                for (int i = 0; i < wordDb.words_List.Length; i++)
                {
                   
                    switch (wordDb.languagesName.Length)
                    {
                        case 1:

                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            break;
                        case 2:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            break;
                        case 3:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            break;
                        case 4:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            break;
                        case 5:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            break;
                        case 6:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            break;
                        case 7:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            break;
                        case 8:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            break;
                        case 9:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord8, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            wordDb.languageInfoWord8[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[8] + ")", wordDb.languageInfoWord8[i]);
                            break;
                        case 10:
                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord8, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord9, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            wordDb.languageInfoWord8[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[8] + ")", wordDb.languageInfoWord8[i]);
                            wordDb.languageInfoWord9[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[9] + ")", wordDb.languageInfoWord9[i]);
                            break;
                        case 11:

                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord8, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord9, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord10, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            wordDb.languageInfoWord8[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[8] + ")", wordDb.languageInfoWord8[i]);
                            wordDb.languageInfoWord9[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[9] + ")", wordDb.languageInfoWord9[i]);
                            wordDb.languageInfoWord10[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[10] + ")", wordDb.languageInfoWord10[i]);
                            break;
                        case 12:

                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord8, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord9, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord10, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord11, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            wordDb.languageInfoWord8[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[8] + ")", wordDb.languageInfoWord8[i]);
                            wordDb.languageInfoWord9[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[9] + ")", wordDb.languageInfoWord9[i]);
                            wordDb.languageInfoWord10[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[10] + ")", wordDb.languageInfoWord10[i]);
                            wordDb.languageInfoWord11[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[11] + ")", wordDb.languageInfoWord11[i]);
                            break;
                        case 13:

                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord8, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord9, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord10, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord11, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord12, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            wordDb.languageInfoWord8[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[8] + ")", wordDb.languageInfoWord8[i]);
                            wordDb.languageInfoWord9[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[9] + ")", wordDb.languageInfoWord9[i]);
                            wordDb.languageInfoWord10[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[10] + ")", wordDb.languageInfoWord10[i]);
                            wordDb.languageInfoWord11[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[11] + ")", wordDb.languageInfoWord11[i]);
                            wordDb.languageInfoWord12[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[12] + ")", wordDb.languageInfoWord12[i]);
                            break;
                        case 14:

                            System.Array.Resize(ref wordDb.languageInfoWord0, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord1, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord2, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord3, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord4, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord5, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord6, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord7, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord8, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord9, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord10, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord11, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord12, wordDb.words_List.Length);
                            System.Array.Resize(ref wordDb.languageInfoWord13, wordDb.words_List.Length);

                            wordDb.languageInfoWord0[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[0] + ")", wordDb.languageInfoWord0[i]);
                            wordDb.languageInfoWord1[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[1] + ")", wordDb.languageInfoWord1[i]);
                            wordDb.languageInfoWord2[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[2] + ")", wordDb.languageInfoWord2[i]);
                            wordDb.languageInfoWord3[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[3] + ")", wordDb.languageInfoWord3[i]);
                            wordDb.languageInfoWord4[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[4] + ")", wordDb.languageInfoWord4[i]);
                            wordDb.languageInfoWord5[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[5] + ")", wordDb.languageInfoWord5[i]);
                            wordDb.languageInfoWord6[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[6] + ")", wordDb.languageInfoWord6[i]);
                            wordDb.languageInfoWord7[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[7] + ")", wordDb.languageInfoWord7[i]);
                            wordDb.languageInfoWord8[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[8] + ")", wordDb.languageInfoWord8[i]);
                            wordDb.languageInfoWord9[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[9] + ")", wordDb.languageInfoWord9[i]);
                            wordDb.languageInfoWord10[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[10] + ")", wordDb.languageInfoWord10[i]);
                            wordDb.languageInfoWord11[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[11] + ")", wordDb.languageInfoWord11[i]);
                            wordDb.languageInfoWord12[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[12] + ")", wordDb.languageInfoWord12[i]);
                            wordDb.languageInfoWord13[i] = EditorGUILayout.TextField("Info of " + wordDb.words_List[i] + " (" + wordDb.languagesName[13] + ")", wordDb.languageInfoWord13[i]);
                            break;
                    }
                 
                }
            }
        }

        EditorUtility.SetDirty(wordDb);
        Undo.RecordObject(wordDb, "Undo wordDb");

    }

	void AddWord ()
	{
		System.Array.Resize (ref wordDb.words_List, wordDb.words_List.Length + 1);
		System.Array.Resize (ref wordDb.image, wordDb.image.Length + 1);
    }
	
	void AddLanguage ()
	{
		System.Array.Resize (ref wordDb.languagesName, wordDb.languagesName.Length + 1);
	}

}
