using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// EnvironmentSlice uses the current music level heard by the player in
/// order to create the next slice of the environment. The environment slices
/// that can be created are low, medium, and high environments. The evironment
/// levels will alter the elevation of the player in the game. 
/// </summary>
public class EnvironmentSlice : MonoBehaviour
{
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

    private Vector3 currMax;
    private Grid grid;

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

    private void InstantiateSlice(ref GameObject slice)
    {
        var clone = Instantiate(slice, currMax, Quaternion.identity, grid.transform);
        currMax = clone.GetComponent<TilemapRenderer>().bounds.max;
        AlignSlices(ref currMax);
    }

    private void CreateLowEnvironmentSlice()
    {
        // Tilemaps were created at the same time, which causes them to have
        // different y values as default. Therefore have to manually set the
        // value. Value was determined by aligning the slices in the game.
        currMax.y = 0;

        var lowTileMap = Resources.Load("TileMaps/LowLevelTileMap") as GameObject;
        InstantiateSlice(ref lowTileMap);
    }

    private void CreateMediumEnvironmentSlice()
    {
        // Tilemaps were created at the same time, which causes them to have
        // different y values as default. Therefore have to manually set the
        // value. Value was determined by aligning the slices in the game.
        currMax.y = 19.0f;

        var medTileMap = Resources.Load("TileMaps/MidLevelTileMap") as GameObject;
        InstantiateSlice(ref medTileMap);
    }

    private void CreateHighEnvironmentSlice()
    {
        // Tilemaps were created at the same time, which causes them to have
        // different y values as default. Therefore have to manually set the
        // value. Value was determined by aligning the slices in the game.
        currMax.y = 35.0f;

        var highTileMap = Resources.Load("TileMaps/HighLevelTileMap") as GameObject;
        InstantiateSlice(ref highTileMap);
    }

    // Slices need to be aligned. Right now this number is determined by
    // manually aligning the slices. But the value should be changed such that
    // we use information from the game instead of using a pre-determined value.
    private void AlignSlices(ref Vector3 vector)
    {
        currMax.x -= 0.5f;
    }

    private void UpdateEnvironment()
    {
        // musicLevel is changed here in order to showcase the different
        // tileMaps.
        if (musicLevel == MusicLevel.High)
        {
            CreateHighEnvironmentSlice();
            musicLevel = MusicLevel.Low;
        }
        else if (musicLevel == MusicLevel.Medium)
        {
            CreateMediumEnvironmentSlice();
            musicLevel = MusicLevel.High;
        }
        else
        {
            CreateLowEnvironmentSlice();
            musicLevel = MusicLevel.Medium;
        }
    }

    private void Start()
    {
        grid = new GameObject("Grid").AddComponent<Grid>();
        musicLevel = MusicLevel.Low;
        currMax = new Vector3(0, 0, 0);

        InvokeRepeating("UpdateEnvironment", 0.0f, 1.5f);
    }
}
