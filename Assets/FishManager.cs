using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class FishManager
{
    
    /// <summary>
    /// This script takes the information from the CSV file with all of the fish
    /// information and creates a list of the Fish class with an instance for
    /// each fish in the csv file!
    /// </summary>
    
    public static List<Fish> allFish = new List<Fish>();

    public static Fish GetRandomFishWeighted() {
        //Picks out a random fish using spoke weights
        var totalSpoke = 0;
        foreach (var fish in allFish) {
            totalSpoke += fish.spokeWeight;
        }
        var valueChosen = Random.Range(0, totalSpoke);
        foreach (var fish in allFish) {
            if (valueChosen < fish.spokeWeight) {
                return fish;
            } else {
                valueChosen -= fish.spokeWeight;
            }
        }
        return null;
    }
}
