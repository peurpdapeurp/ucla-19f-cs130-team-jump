using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayModeTests
    {
        private EnvironmentSlice environmentSlice;
        private Camera camera;
        private GameObject player;

        private void IncreaseCameraSpeed()
        {
            camera.GetComponent<CameraMover>().movementSpeed = 1.0f;
        }

        private Vector3 GetLatestXMaxOfEnvironmentSlices()
        {
            Vector3 newPos = new Vector3();
            EnvironmentSlice script = environmentSlice.GetComponent<EnvironmentSlice>();
            newPos.x = script.currMax.x - EnvironmentSlice.kCameraOffset;
            newPos.y = camera.transform.position.y;
            newPos.z = camera.transform.position.z;
            return newPos;
        }

        [SetUp]
        public void SetUp()
        {
            GameObject cameraObject =
                Object.Instantiate(Resources.Load<GameObject>("GameObjects/MainCamera"));
            camera = cameraObject.GetComponent<Camera>();
            camera.name = "MainCamera";

            GameObject environmentObject =
                Object.Instantiate(Resources.Load<GameObject>("GameObjects/EnvironmentSliceGenerator"));
            environmentSlice = environmentObject.GetComponent<EnvironmentSlice>();

            GameObject playerObject =
                Object.Instantiate(Resources.Load<GameObject>("GameObjects/PlayerObject"));
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            player = players[0];
        }

        private void DeallocateTileMap(string tileMapsTag)
        {
            foreach (GameObject tilemap in GameObject.FindGameObjectsWithTag(tileMapsTag))
            {
                Object.Destroy(tilemap);
            }
        }

        void DeallocateAllTileMaps()
        {
            DeallocateTileMap("LowLevelTileMap");
            DeallocateTileMap("MediumLevelTileMap");
            DeallocateTileMap("HighLevelTileMap");
        }

        void DeallocateEnemies()
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Object.Destroy(enemy.gameObject);
            }
        }

        void DeallocatePowerups()
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Powerup"))
            {
                Object.Destroy(enemy.gameObject);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (player != null)
            {
                Object.Destroy(player.gameObject);
            }

            DeallocateAllTileMaps();
            DeallocateEnemies();
            DeallocatePowerups();

            Object.Destroy(environmentSlice.gameObject);
            Object.Destroy(camera.gameObject);
        }

        [UnityTest]
        public IEnumerator CameraMovesAtStartOfGame()
        {
            var initialXPosition = camera.transform.position.x;
            yield return new WaitForSeconds(0.3f);

            Assert.Greater(camera.transform.position.x, initialXPosition);
        }

        [UnityTest]
        public IEnumerator CameraStopsMovingIfPlayerLeavesCameraBounds()
        {
            IncreaseCameraSpeed();

            yield return new WaitForSeconds(2.0f);

            Assert.AreEqual(0.0f, camera.GetComponent<CameraMover>().movementSpeed);
        }

        [UnityTest]
        public IEnumerator WastedTextIsDisplayedIfPlayerDies()
        {
            IncreaseCameraSpeed();

            yield return new WaitForSeconds(0.5f);

            Assert.True(camera.GetComponent<CameraTriggerEvent>().loseText.GetComponent<UnityEngine.UI.Text>().enabled);
        }

        [UnityTest]
        public IEnumerator EnvironmentSliceGeneratesNewSliceIfCameraGetsClose()
        {
            // Destroy player so test does not terminate early.
            Object.Destroy(player.gameObject);
            float initialXMaxOfTileMaps =
                environmentSlice.GetComponent<EnvironmentSlice>().currMax.x;

            Assert.Greater(initialXMaxOfTileMaps, 0.0f);

            camera.transform.position = GetLatestXMaxOfEnvironmentSlices();

            yield return new WaitForSeconds(0.2f);

            // The new currMax indicates a new slice was generated.
            Assert.Greater(environmentSlice.GetComponent<EnvironmentSlice>().currMax.x, initialXMaxOfTileMaps);
        }

        [UnityTest]
        public IEnumerator EnvironmentSlicePopulatesRandomNumberOfEnemies()
        {
            yield return new WaitForSeconds(0.2f);

            Assert.Greater(GameObject.FindGameObjectsWithTag("Enemy").Length, 0);
        }

        [UnityTest]
        public IEnumerator EnvironmentSliceCreatesLowLevelSlice()
        {
            Object.Destroy(player.gameObject);
            camera.transform.position = GetLatestXMaxOfEnvironmentSlices();
            environmentSlice.GetComponent<EnvironmentSlice>().ChooseNextSlice(EnvironmentSlice.MusicLevel.Low);

            yield return new WaitForSeconds(0.2f);

            Assert.AreEqual(GameObject.FindGameObjectsWithTag("LowLevelTileMap").Length, 2);
        }

        [UnityTest]
        public IEnumerator EnvironmentSliceCreatesMediumLevelSlice()
        {
            Object.Destroy(player.gameObject);
            camera.transform.position = GetLatestXMaxOfEnvironmentSlices();
            environmentSlice.GetComponent<EnvironmentSlice>().ChooseNextSlice(EnvironmentSlice.MusicLevel.Medium);

            yield return new WaitForSeconds(0.2f);

            Assert.AreEqual(1, GameObject.FindGameObjectsWithTag("MediumLevelTileMap").Length);
        }

        [UnityTest]
        public IEnumerator EnvironmentSliceCreatesHighLevelSlice()
        {
            Object.Destroy(player.gameObject);
            camera.transform.position = GetLatestXMaxOfEnvironmentSlices();
            environmentSlice.GetComponent<EnvironmentSlice>().ChooseNextSlice(EnvironmentSlice.MusicLevel.High);

            yield return new WaitForSeconds(0.2f);

            Assert.AreEqual(1, GameObject.FindGameObjectsWithTag("HighLevelTileMap").Length);
        }

        [UnityTest]
        public IEnumerator PlayerLosesHealthIfContactWithEnemy()
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            Assert.AreEqual(playerHealth.getCurrentHealth(), playerHealth.maxHealth);

            // Create new enemy and position near player.
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicTerrainEnemy"));
            enemy.transform.position = player.transform.position;

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(playerHealth.getCurrentHealth(), playerHealth.maxHealth - 1);

            Object.Destroy(enemy);
        }

        [UnityTest]
        public IEnumerator PlayerDoesNotLoseHealthForASecond()
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            Assert.AreEqual(playerHealth.getCurrentHealth(), playerHealth.maxHealth);

            // Mock enemy behavior
            playerHealth.applyDamage();

            // Create new enemy and position near player.
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicTerrainEnemy"));
            enemy.transform.position = player.transform.position;

            yield return new WaitForSeconds(0.1f);

            // Player only lost one health.
            Assert.AreEqual(playerHealth.getCurrentHealth(), playerHealth.maxHealth - 1);

            Object.Destroy(enemy);
        }

        [UnityTest]
        public IEnumerator PlayerLoseHealthRadiusChanges()
        {
            PostProcessScript postProcessScript = camera.GetComponent<PostProcessScript>();

            Assert.AreEqual(postProcessScript.GetRadius(), PostProcessScript.kFullHealthRadius);

            // Create new enemy and position near player.
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicTerrainEnemy"));
            enemy.transform.position = player.transform.position;

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(postProcessScript.GetRadius(), PostProcessScript.kTwoHealthRadius);
        }

        [UnityTest]
        public IEnumerator BasicTerrainEnemyMoves()
        {
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicTerrainEnemy"));
            enemy.transform.position = camera.transform.position;

            Vector3 oldPosition = enemy.transform.position;

            yield return new WaitForSeconds(1.0f);

            Assert.AreNotEqual(enemy.transform.position, oldPosition);
        }

        [UnityTest]
        public IEnumerator BasicJumpingEnemyMoves()
        {
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicJumpingEnemy"));
            enemy.transform.position = camera.transform.position;
            player.transform.position = camera.transform.position;

            Vector3 oldPosition = enemy.transform.position;

            yield return new WaitForSeconds(3.0f);

            Assert.AreNotEqual(enemy.transform.position, oldPosition);
        }

        [UnityTest]
        public IEnumerator BasicAerialEnemyMoves()
        {
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicAerialEnemy"));
            enemy.transform.position = camera.transform.position;

            Vector3 oldPosition = enemy.transform.position;

            yield return new WaitForSeconds(1.0f);

            Assert.AreNotEqual(enemy.transform.position, oldPosition);
        }

        [UnityTest]
        public IEnumerator BasicWallWalkingEnemyMoves()
        {
            GameObject enemy =
                Object.Instantiate(Resources.Load<GameObject>("Enemies/BasicWallWalkingEnemy"));
            enemy.transform.position = camera.transform.position;

            Vector3 oldPosition = enemy.transform.position;

            yield return new WaitForSeconds(1.0f);

            Assert.AreNotEqual(enemy.transform.position, oldPosition);
        }
    }
}
