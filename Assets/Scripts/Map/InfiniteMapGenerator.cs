using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] mapTilePrefabs; // an array to store all the mapTilePrefabs
    public int seed;
    public int mapTileSize = 10;
    public int loadRadius = 2; // reduce the loading radius to reduce the number of map tiles generated
    public float minDistanceBetweenTiles = 30f; // increase the minimum distance between map tiles
    public float maxRandomOffset = 10f; // increase the maximum random offset
    public float fadeDuration = 1f; // fade-in effect duration
    public float stamina = 100f;
    public float staminaConsumptionRate = 1f;
    public float fatigueDamageRate = 1f;
    public float explorationValue = 0f;
    public float explorationThreshold = 100f;

    private Dictionary<Vector2, GameObject> mapTiles;
    private Vector2 playerCurrentTile;
    private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private Vector3? lastTilePosition = null;

    void Start()
    {
        Random.InitState(seed);
        mapTiles = new Dictionary<Vector2, GameObject>();
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerCurrentTile = GetTilePosition(player.transform.position);
        GenerateTilesAround(playerCurrentTile);
    }

    void Update()
    {
        Vector2 playerPosition = GetTilePosition(player.transform.position);

        if (playerPosition != playerCurrentTile)
        {
            playerCurrentTile = playerPosition;
            GenerateTilesAround(playerCurrentTile);
        }

        HandleStamina();
    }

    Vector2 GetTilePosition(Vector3 position)
    {
        return new Vector2(Mathf.Floor(position.x / mapTileSize), Mathf.Floor(position.y / mapTileSize));
    }

    void GenerateTilesAround(Vector2 centerTile)
    {
        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2 tilePosition = new Vector2(centerTile.x + x, centerTile.y + y);
                if (!mapTiles.ContainsKey(tilePosition))
                {
                    GenerateTile(tilePosition);
                }
            }
        }
    }

    void GenerateTile(Vector2 tilePosition)
    {
        Vector3 newPosition;
        int attempts = 0;
        do
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-maxRandomOffset, maxRandomOffset),
                Random.Range(-maxRandomOffset, maxRandomOffset),
                0
            );

            newPosition = new Vector3(tilePosition.x * mapTileSize, tilePosition.y * mapTileSize, 0) + randomOffset;
            attempts++;
        } while (!IsPositionValid(newPosition) && attempts < 10);

        if (attempts < 10) // successfully found a valid position
        {
            int randomIndex = Random.Range(0, mapTilePrefabs.Length); // generate a random index
            GameObject selectedPrefab = mapTilePrefabs[randomIndex]; // select a prefab from the array
            GameObject newTile = Instantiate(selectedPrefab, newPosition, Quaternion.identity);
            mapTiles[tilePosition] = newTile;
            StartCoroutine(FadeInFog(newTile));

            if (lastTilePosition != null)
            {
                CreateLineBetweenPoints((Vector3)lastTilePosition, newPosition);
            }

            lastTilePosition = newPosition;

            if (explorationValue >= explorationThreshold)
            {
                // Trigger Boss Battle
                TriggerBossBattle();
                explorationValue = 0f; // reset exploration value after boss battle
            }
        }
    }

    IEnumerator FadeInFog(GameObject tile)
    {
        // find the child objects tagged as "Fog"
        Transform[] children = tile.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("Fog"))
            {
                SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    Color color = renderer.color;
                    color.a = 1f;
                    renderer.color = color;

                    float elapsedTime = 0f;
                    while (elapsedTime < fadeDuration)
                    {
                        elapsedTime += Time.deltaTime;
                        color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                        renderer.color = color;
                        yield return null;
                    }

                    color.a = 0f;
                    renderer.color = color;
                }
            }
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (var tile in mapTiles.Values)
        {
            if (Vector3.Distance(tile.transform.position, position) < minDistanceBetweenTiles)
            {
                return false;
            }
        }
        return true;
    }

    void HandleStamina()
    {
        if (playerMovement.isMoving)
        {
            float staminaDecrease = staminaConsumptionRate * Time.deltaTime;
            stamina -= staminaDecrease;
            explorationValue += staminaDecrease; // increase exploration value based on stamina decrease

            if (stamina <= 0)
            {
                stamina = 0;
                playerHealth.TakeDamage(fatigueDamageRate * Time.deltaTime);
            }
        }
    }

    void TriggerBossBattle()
    {
        // Implement Boss Battle Logic
        Debug.Log("Boss Battle Triggered!");
    }

    void CreateLineBetweenPoints(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject lineObject = new GameObject("Line");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        // Set the line color
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the order in layer
        lineRenderer.sortingOrder = 1;
    }
}
