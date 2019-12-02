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
    public Vector3 currMax;

    private LevelGenerated generatedLevel;

    /// <summary>
    /// Holds the grid GameObject where all the slices are generated in.
    /// </summary>
    private Grid grid;

    /// <summary>
    /// Offset used to check when the next slice should be generated. Used with
    /// the camera's upper bound.
    /// </summary>
    public const int kCameraOffset = 2;

    /// <summary>
    /// Create different number of enemies based on the slice that was generated.
    /// </summary>
    private const int kMinNumberEnemiesPerSlice = 3;
    private const int kMaxNumberEnemiesPerLowLevelSlice = 4;
    private const int kMaxNumberEnemiesPerMediumLevelSlice = 6;
    private const int kMaxNumberEnemiesPerHighLevelSlice = 8;

    /// <summary>
    /// Enemy type strings
    /// </summary>
    private const string enemyPrefabStringAerial = "Enemies/BasicAerialEnemy";
    private const string enemyPrefabStringWallWalking = "Enemies/BasicWallWalkingEnemy";
    private const string enemyPrefabStringJumping = "Enemies/BasicJumpingEnemy";
    private const string enemyPrefabStringTerrain = "Enemies/BasicTerrainEnemy";
    private const string enemyPrefabStringCoin = "Enemies/Coin";

    // The number of slices there are per level.
    private const int kMaxNumOfLevelSlices = 3;

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
        AlignSlices(ref currMax);
        GameObject clone = Instantiate(slice, currMax, Quaternion.identity, grid.transform) as GameObject;
        currMax = clone.GetComponent<TilemapRenderer>().bounds.max;

        var tileMap = clone.GetComponent<Tilemap>();

        GenerateEnemies(tileMap);
    }

    private void CreateLowEnvironmentSlice()
    {
        generatedLevel = LevelGenerated.Low;

        int sliceToSelect = randomGenerator.Next(1, kMaxNumOfLevelSlices);
        GameObject lowTileMap = Resources.Load("TileMaps/LowLevelTileMap" + sliceToSelect.ToString()) as GameObject;
        InstantiateSlice(ref lowTileMap);
    }

    private void CreateMediumEnvironmentSlice()
    {
        generatedLevel = LevelGenerated.Medium;

        int sliceToSelect = randomGenerator.Next(1, kMaxNumOfLevelSlices);
        GameObject medTileMap = Resources.Load("TileMaps/MediumLevelTileMap" + sliceToSelect.ToString()) as GameObject;
        InstantiateSlice(ref medTileMap);
    }

    private void CreateHighEnvironmentSlice()
    {
        generatedLevel = LevelGenerated.High;

        int sliceToSelect = randomGenerator.Next(1, kMaxNumOfLevelSlices);
        GameObject highTileMap = Resources.Load("TileMaps/HighLevelTileMap" + sliceToSelect.ToString()) as GameObject;
        InstantiateSlice(ref highTileMap);
    }

    // Slices need to be aligned. Right now this number is determined by
    // manually aligning the slices. But the value should be changed such that
    // we use information from the game instead of using a pre-determined value.
    private void AlignSlices(ref Vector3 vector)
    {
        currMax.x += 15.5f;
        currMax.y = 0.0f;
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
                    if (aboveIsEmpty(tileMap, localPlace, 2))
                    {
                        Vector3 emptyTile = tileMap.CellToWorld(localPlace + new Vector3Int(0, 2, 0));
                        locations.Add(emptyTile);
                    }
                    if (belowIsEmpty(tileMap, localPlace, 2) && localPlace[1] > -5)
                    {
                        Vector3 emptyTile = tileMap.CellToWorld(localPlace + new Vector3Int(0, -2, 0));
                        locations.Add(emptyTile);
                    }
                    if (rightIsEmpty(tileMap, localPlace, 2))
                    {
                        Vector3 emptyTile = tileMap.CellToWorld(localPlace + new Vector3Int(2, 0, 0));
                        locations.Add(emptyTile);
                    }
                    if (leftIsEmpty(tileMap, localPlace, 2))
                    {
                        Vector3 emptyTile = tileMap.CellToWorld(localPlace + new Vector3Int(-2, 0, 0));
                        locations.Add(emptyTile);
                    }
                }
            }
        }

        int maxNumEnemies = 0;
        if (generatedLevel == LevelGenerated.Low)
        {
            maxNumEnemies = kMaxNumberEnemiesPerLowLevelSlice;
        } else if (generatedLevel == LevelGenerated.Medium)
        {
            maxNumEnemies = kMaxNumberEnemiesPerMediumLevelSlice;
        } else
        {
            maxNumEnemies = kMaxNumberEnemiesPerHighLevelSlice;
        }

        int numberEnemiesToSpawn = randomGenerator.Next(kMinNumberEnemiesPerSlice, maxNumEnemies);
        for (int i = 0; i < numberEnemiesToSpawn; ++i)
        {
            string enemyPrefabString = getRandomEnemyPrefabString();
            var enemy = Resources.Load(enemyPrefabString) as GameObject;

            switch (enemyPrefabString)
            {
                case enemyPrefabStringAerial:
                    {
                        int listIndex = randomGenerator.Next(locations.Count);
                        Instantiate(enemy, locations[listIndex], Quaternion.Euler(0, 0, 0));
                        // Don't instantiate at same place;
                        locations.RemoveAt(listIndex);
                        break;
                    }
                case enemyPrefabStringJumping:
                    {
                        int listIndex = randomGenerator.Next(locations.Count);
                        Instantiate(enemy, locations[listIndex], Quaternion.Euler(0, 0, 0));
                        // Don't instantiate at same place;
                        locations.RemoveAt(listIndex);
                        break;
                    }
                case enemyPrefabStringWallWalking:
                    {
                        int listIndex = randomGenerator.Next(locations.Count);
                        Instantiate(enemy, locations[listIndex], Quaternion.Euler(0, 0, 180));
                        // Don't instantiate at same place;
                        locations.RemoveAt(listIndex);
                        break;
                    }
                case enemyPrefabStringTerrain:
                    {
                        int listIndex = randomGenerator.Next(locations.Count);
                        Instantiate(enemy, locations[listIndex], Quaternion.Euler(0, 0, 0));
                        // Don't instantiate at same place;
                        locations.RemoveAt(listIndex);
                        break;
                    }
                case enemyPrefabStringCoin:
                    {
                        int listIndex = randomGenerator.Next(locations.Count);
                        Instantiate(enemy, locations[listIndex], Quaternion.Euler(0, 0, 0));
                        // Don't instantiate at same place;
                        locations.RemoveAt(listIndex);
                        break;
                    }
            }
        }
    }

    private bool aboveIsEmpty(Tilemap tileMap, Vector3Int localPlace, int distance)
    {
        for (int i = 1; i <= distance; i++)
        {
            if (tileMap.HasTile(localPlace + new Vector3Int(0, i, 0)))
                return false;
        }
        return true;
    }

    private bool belowIsEmpty(Tilemap tileMap, Vector3Int localPlace, int distance)
    {
        for (int i = 1; i <= distance; i++)
        {
            if (tileMap.HasTile(localPlace + new Vector3Int(0, -i, 0)))
                return false;
        }
        return true;
    }

    private bool rightIsEmpty(Tilemap tileMap, Vector3Int localPlace, int distance)
    {
        for (int i = 1; i <= distance; i++)
        {
            if (tileMap.HasTile(localPlace + new Vector3Int(i, 0, 0)))
                return false;
        }
        return true;
    }

    private bool leftIsEmpty(Tilemap tileMap, Vector3Int localPlace, int distance)
    {
        for (int i = 1; i <= distance; i++)
        {
            if (tileMap.HasTile(localPlace + new Vector3Int(-i, 0, 0)))
                return false;
        }
        return true;
    }

    private string getRandomEnemyPrefabString()
    {
        switch (randomGenerator.Next(5))
        {
            case 0:
                {
                    return enemyPrefabStringAerial;
                }
            case 1:
                {
                    return enemyPrefabStringJumping;
                }
            case 2:
                {
                    return enemyPrefabStringTerrain;
                }
            case 3:
                {
                    return enemyPrefabStringWallWalking;
                }
            case 4:
                {
                    return enemyPrefabStringCoin;
                }
            default:
                {
                    return enemyPrefabStringTerrain;
                }
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
