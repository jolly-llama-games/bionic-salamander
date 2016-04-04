using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinGenerator : MonoBehaviour {

    public ObjectPooler coinPool;

    public float coinMargins;

	public void SpawnCoins(int coinAmount, Vector3 origin, float platformWidth)
    {
        if (coinAmount > 0)
        {
            List<GameObject> coins = coinPool.GetPooledObjects(coinAmount);
            if (coins.Count < coinAmount)
            {
                Debug.LogError("Object pooler failed to return requested amount of objects! : " + coinAmount.ToString() + ", " + origin.ToString() + ", " + platformWidth.ToString() + ", " + coins.Count.ToString());
            }
            float adjustedWidth = platformWidth - coinMargins * 2;
            float offset = coinAmount > 1 ? -(adjustedWidth / 2) : 0;
            for (int i = 0; i < coins.Count; i++)
            {
                GameObject coin = coins[i];
                coin.transform.position = new Vector3(origin.x + offset, origin.y, origin.z);
                coin.SetActive(true);
                if (coins.Count > 1)
                {
                    offset += adjustedWidth / (coins.Count - 1);
                }
            }
        }
    }
}
