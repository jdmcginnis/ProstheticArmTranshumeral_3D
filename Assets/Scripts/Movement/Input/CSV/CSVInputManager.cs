using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// CSV file should be of format: First row will be discarded so it can be names
//                               Columns in same order of JointTypes.JointNames enum
public class CSVInputManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float cameraFPS;

    private string filePath;
    public float[] jointAngleOffsets = new float[Enum.GetNames(typeof(JointTypes.JointNames)).Length];
    public float[] jointAngles = new float[Enum.GetNames(typeof(JointTypes.JointNames)).Length];


    public void InitializeCSVInput()
    {
        Time.fixedDeltaTime = 1/cameraFPS;

        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Movement/Input/CSV/CSVFiles");
        string fileName = "power_fast_1.csv";
        filePath = Path.Combine(folderPath, fileName);

        try
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError("File Not Found: " + filePath);
            } else
            {
                StartCoroutine(HandleCSVInput());
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occured: " + ex.Message);
        }
    }

    // TODO: Consider initial joint angles of the hand!

    private IEnumerator HandleCSVInput()
    {
        // Wait until everything is initialized
        yield return new WaitForFixedUpdate();

        // float[] jointAngles = new float[Enum.GetNames(typeof(JointTypes.JointNames)).Length];

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            reader.ReadLine(); // read first line and discard
            line = reader.ReadLine(); // read first line and use as the initial joint angles

            SetInitialJointOffsets(line);

            while ((line = reader.ReadLine()) != null)
            {
                string[] entries = line.Split(',');

                for (int i = 0; i < entries.Length; i++)
                {
                    string entry = entries[i];
                    if (float.TryParse(entry, out float value))
                    {
                        jointAngles[i] = value - jointAngleOffsets[i];
                    } else
                    {
                        Debug.LogError($"Could not parse entry '{entry}' as a float!");
                    }
                }

                inputManager.HandleNewAngleInput(jointAngles);
                yield return new WaitForFixedUpdate();

            }
        }
    }

    private void SetInitialJointOffsets(string jointsLine)
    {
        string[] entries = jointsLine.Split(',');

        for (int i = 0; i < entries.Length; i++)
        {
            string entry = entries[i];
            if (float.TryParse(entry, out float value))
            {
                jointAngleOffsets[i] = value;
            } else
            {
                Debug.LogError($"Could not parse entry '{entry}' as a float!");
            }
        }
    }
}
