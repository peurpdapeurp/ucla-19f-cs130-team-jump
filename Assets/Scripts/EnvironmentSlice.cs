using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnvironmentSlice uses the current music level heard by the player in
/// order to create the next slice of the environment. The environment slices
/// that can be created are low, medium, and high environments. The evironment
/// levels will alter the elevation of the player in the game. 
/// </summary>
public class EnvironmentSlice : MonoBehaviour
{
    /// <summary>
    /// Store for the prefab that will be instantiated at runtime to make up the
    /// environment.
    /// </summary>
    public GameObject prefab;

    // Todo: Add more prefabs that can be instantiated in the game environment.

    // Todo: Move MusicLevel enum inside the class that handles the music.
    /// <summary>
    /// MusicLevel will be used to determine the current pitch of the music.
    /// </summary>
    public enum MusicLevel
    {
        Low = 0,
        Medium = 1,
        High = 2,
    };

    /// <summary>
    /// Store for the MusicLevel being played.
    /// </summary>
    private MusicLevel musicLevel;

    /// <summary>
    /// Method used to notify the class of the current music level.
    /// </summary>
    /// <param name="currMusicLevel"> music level heard by the player</param>
    public void ChooseNextSlice(MusicLevel currMusicLevel)
    {
        musicLevel = currMusicLevel;
    }

    private void PopulatePowerups()
    {
        // TODO: Add logic to populate powerups.
    }

    // Todo: Create the ground with elevation depending on music level.
    private void CreateGround()
    {
        int xStartPos = -4;
        int xEndPos = 5;
        int yPos = -4;

        for (int i = xStartPos; i < xEndPos; ++i)
        {
            Instantiate(prefab, new Vector3(i, yPos, 0), Quaternion.identity);
        }
    }

    // Todo: Create platforms if the environment is meant to be high elevation.
    private void CreatePlatforms()
    {
        int platformLength = 2;

        for (int i = 0; i < platformLength; ++i)
        {
            Instantiate(prefab, new Vector3(2 + i, 0, 0), Quaternion.identity);
        }
    }

    private void CreateLowEnvironmentSlice()
    {
        CreateGround();
        CreatePlatforms();
    }

    private void CreateMediumEnvironmentSlice()
    {
    }

    private void CreateHighEnvironmentSlice()
    {
    }

    private void Start()
    {
        musicLevel = MusicLevel.Low;
    }

    private void Update()
    {
        // TODO: Update only when the user is close to the edge of the previous
        //       environment slice.
        if (musicLevel == MusicLevel.High)
        {
            CreateHighEnvironmentSlice();
        } else if (musicLevel == MusicLevel.Medium)
        {
            CreateMediumEnvironmentSlice();
        } else
        {
            CreateLowEnvironmentSlice();
        }
    }
}
