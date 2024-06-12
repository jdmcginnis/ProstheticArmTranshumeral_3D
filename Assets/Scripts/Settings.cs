using System;
using System.IO;
using UnityEngine;

// Reflects the changes as made in Settings.json
// Imports settings from Settings.json and stores it in the Settings static class
//      - If you want to make changes to the settings, do it in Settings.json
public static class Settings
{
    public static readonly bool usingController;
    public static readonly float predictionRate;
    public static readonly int[] activeGrasps;

    private static string filePath = Application.persistentDataPath + "/Settings.json";

    static Settings()
    {
        if (File.Exists(filePath))
        {
            string loadSettingsData = File.ReadAllText(filePath);
            SettingsData settingsData = JsonUtility.FromJson<SettingsData>(loadSettingsData);

            usingController = settingsData.UsingController;
            predictionRate = settingsData.PredictionRate;
            activeGrasps = settingsData.ActiveGrasps;

        } else
        {
            throw new Exception("No Settings.json file to load!");
        }
    }

    // Helper class for loading data
    private class SettingsData
    {
        public bool UsingController;
        public float PredictionRate;
        public int[] ActiveGrasps;
    }

}
