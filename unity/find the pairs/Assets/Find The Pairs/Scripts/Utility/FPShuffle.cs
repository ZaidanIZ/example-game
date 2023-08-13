using System;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
public class FPShuffle
{
		//For getting random values
		private static Random random = new Random ();
		
		/// <summary>
		/// Shuffles the contents of a list
		/// </summary>
		/// <typeparam name="T">The type of the list to sort</typeparam>
		/// <param name="listToShuffle">The list to shuffle</param>
		/// <param name="numberOfTimesToShuffle">How many times to shuffle the list
		/// by default 3 times</param>
		public static void Shuffle<T> (List<T> listToShuffle, int numberOfTimesToShuffle = 3)
		{
				//Make a new list of the wanted type
				List<T> newList = new List<T> ();
			
				//For each time we want to shuffle
				for (int i = 0; i < numberOfTimesToShuffle; i++) {
						//While there are still items in our list
						while (listToShuffle.Count > 0) {
								//get a random number within the list
								int index = random.Next (listToShuffle.Count);
					
								//Add the item at that position to the new list
								newList.Add (listToShuffle [index]);
					
								//And remove it from the old list
								listToShuffle.RemoveAt (index);
						}
				
						//Then copy all the items back in the old list again
						listToShuffle.AddRange (newList);
				
						//And clear the new list ,to make ready for next shuffling
						newList.Clear ();
				}
		}

		/// <summary>
		/// Random pairs shuffle.
		/// </summary>
		/// <param name="pairs">Pairs.</param>
		/// <param name="maxIndex">Max index.</param>
		public static void RandomPairsShuffle (List<Level.Pair> pairs, int maxIndex)
		{
				List<int> indexes = new List<int> ();
				for (int i = 0; i < maxIndex; i++) {
						indexes.Add (i);
				}
				
				//Shuffle the indexes
				Shuffle (indexes, 2);
		
				int current = 0;

				//Apply shuffle on Pairs
				foreach (Level.Pair pair in pairs) {
						pair.firstElement.index = indexes [current];
						current++;
						pair.secondElement.index = indexes [current];
						current++;
				}
		}
}
