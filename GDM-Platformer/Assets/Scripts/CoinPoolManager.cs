using UnityEngine;
using System.Collections.Generic;

public class CoinPoolManager : MonoBehaviour
{
    public static CoinPoolManager Instance { get; private set; }

    public GameObject coinPrefab;

    private ObjectPool coinPool;
    private List<Vector3> coinStartPositions;
    private List<GameObject> activeCoins;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Find all coins placed in the scene and record their positions
        GameObject[] existingCoins = GameObject.FindGameObjectsWithTag("coin");
        coinStartPositions = new List<Vector3>();

        foreach (GameObject coin in existingCoins)
        {
            coinStartPositions.Add(coin.transform.position);
            Destroy(coin);
        }

        // Create pool and spawn coins
        coinPool = new ObjectPool(coinPrefab, coinStartPositions.Count);
        activeCoins = new List<GameObject>();
        SpawnAllCoins();
    }

    void OnEnable()
{
    if (coinPool != null)
    {
        ResetAllCoins();
    }
}

    public void SpawnAllCoins()
    {
        foreach (Vector3 position in coinStartPositions)
        {
            GameObject coin = coinPool.Get();
            coin.transform.position = position;
            activeCoins.Add(coin);
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coinPool.Return(coin);
        activeCoins.Remove(coin);
    }

    public void ResetAllCoins()
    {
        foreach (GameObject coin in activeCoins)
        {
            coinPool.Return(coin);
        }
        activeCoins.Clear();
        SpawnAllCoins();
    }
}