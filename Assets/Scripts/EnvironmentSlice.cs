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

    public enum LevelGenerated
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
    /// Contains the coordinates where the next slice should be places. Value is
    /// created by using the previous slice's upper bounds.
    /// </summary>
    private Vector3 currMax;

    private LevelGenerated generatedLevel;

    /// <summary>
    /// Holds the grid GameObject where all the slices are generated in.
    /// </summary>
    private Grid grid;

    /// <summary>
    /// Offset used to check when the next slice should be generated. Used with
    /// the camera's upper bound.
    /// </summary>
    private const int kCameraOffset = 2;

    private const int kMaxNumberEnemiesPerSlice = 4;

    private const float kLowMusicLevelYOffset = -7.0f;
    private const float kMidMusicLevelYOffset = 12.0f;
    private const float kHighMusicLevelYOffset = 28.0f;

    private System.Random randomGenerator;

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
        GameObject clone = Instantiate(slice, currMax, Quaternion.identity, grid.transform) as GameObject;
        currMax = clone.GetComponent<TilemapRenderer>().bounds.max;
        AlignSlices(ref currMax);

        var tileMap = clone.GetComponent<Tilemap>();

        GenerateEnemies(tileMap);
    }

    private void CreateLowEnvironmentSlice()
    {
        // Tilemaps were created at the same time, which causes them to have
        // different y values as default. Therefore have to manually set the
        // value. Value was determined by aligning the slices in the game.
        currMax.y = kLowMusicLevelYOffset;

        generatedLevel = LevelGenerated.Low;

        var lowTileMap = Resources.Load("TileMaps/LowLevelTileMap") as GameObject;
        InstantiateSlice(ref lowTileMap);
    }

    private void CreateMediumEnvironmentSlice()
    {
        // Tilemaps were created at the same time, which causes them to have
        // different y values as default. Therefore have to manually set the
        // value. Value was determined by aligning the slices in the game.
        currMax.y = kMidMusicLevelYOffset;

        generatedLevel = LevelGenerated.Medium;

        var medTileMap = Resources.Load("TileMaps/MidLevelTileMap") as GameObject;
        InstantiateSlice(ref medTileMap);
    }

    private void CreateHighEnvironmentSlice()
    {
        // Tilemaps were created at the same time, which causes them to have
        // different y values as default. Therefore have to manually set the
        // value. Value was determined by aligning the slices in the game.
        currMax.y = kHighMusicLevelYOffset;

        generatedLevel = LevelGenerated.High;

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
        // musicLevel is changed here in order to show the different tileMaps.
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

    private bool ShouldGenerateSlice()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.aspect * halfHeight;

        var cameraXUpperBound = Camera.main.transform.position.x + halfWidth;
        if ((cameraXUpperBound + kCameraOffset) < currMax.x)
        {
            return false;
        }

        return true;
    }

    private void GenerateEnemies(Tilemap tileMap)
    {
        List<Vector3> locations = new List<Vector3>();

        for (int x = tileMap.cellBounds.xMin; x < tileMap.cellBounds.xMax; ++x)
        {
            for (int y = tileMap.cellBounds.yMin; y < tileMap.cellBounds.yMax; ++y)
            {
                Vector3Int localPlace = new Vector3Int(x, y, 0);
                if (tileMap.HasTile(localPlace))
                {
                    Vector3Int tileAbovePos = new Vector3Int(x, y + 1, 0);
                    Vector3Int twoTileAbovePos = new Vector3Int(x, y + 1, 0);
                    if (!tileMap.HasTile(tileAbovePos) && !tileMap.HasTile(twoTileAbovePos))
                    {
                        Vector3 emptyTile = tileMap.CellToWorld(tileAbovePos);
                        Debug.Log(emptyTile.ToString());
                        locations.Add(emptyTile);
                    }
                }
            }
        }

        int numberEnemiesToSpawn = randomGenerator.Next(1, kMaxNumberEnemiesPerSlice);
        for (int i = 0; i < numberEnemiesToSpawn; ++i)
        {
            int listIndex = randomGenerator.Next(locations.Count);

            var enemy = Resources.Load("Enemies/Coin") as GameObject;
            Instantiate(enemy, locations[listIndex], Quaternion.identity);

            // Don't instantiate at same place;
            locations.RemoveAt(listIndex);
        }
    }

    private void Start()
    {
        grid = new GameObject("Grid").AddComponent<Grid>();
        musicLevel = MusicLevel.Low;
        currMax = new Vector3(0, 0, 0);
        randomGenerator = new System.Random();

        UpdateEnvironment();
    }


    private void Update()
    {
        if (!ShouldGenerateSlice())
        {
            return;
        }

        UpdateEnvironment();
    }
}
