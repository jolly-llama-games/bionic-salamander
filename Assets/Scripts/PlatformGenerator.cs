using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public ObjectPooler[] objectPools;
    public Transform generationPoint;

    public float platformGapMin;
    public float platformGapMax;

    private float platformGap;

    private int platformSelector;
    private float[] platformWidths;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float height;

    public float coinSpawnChance = 30;

    private CoinGenerator coinGenerator;

    public delegate void PlatformEvents (GameObject go, float pw);
    public static event PlatformEvents OnCreate;

    // Use this for initialization
    void Start () {
        platformWidths = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++)
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        coinGenerator = FindObjectOfType<CoinGenerator>();
    }
	
	// Update is called once per frame
	void Update () {
	    
        if(transform.position.x < generationPoint.position.x)
        {
            platformGap = Random.Range(platformGapMin, platformGapMax);
            platformSelector = Random.Range(0, objectPools.Length);

            height = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (height > maxHeight)
            {
                height = maxHeight;
            } else if (height < minHeight)
            {
                height = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + platformGap, height, transform.position.z);

            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = Quaternion.identity;
            newPlatform.SetActive(true);

            // Trigger OnCreate event
            if (OnCreate != null)
                OnCreate(newPlatform, platformWidths[platformSelector]);

            if (Random.Range(0f, 100f) < coinSpawnChance)
            {
                int coinAmount = Random.Range(1, Mathf.FloorToInt(platformWidths[platformSelector] / 2));// + 1);

                coinGenerator.SpawnCoins(coinAmount, transform.position + Vector3.up * 1.3f, platformWidths[platformSelector]);
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
        }
	}
}
