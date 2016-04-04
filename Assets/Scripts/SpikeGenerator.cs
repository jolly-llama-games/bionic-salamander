using UnityEngine;
using System.Collections;

public class SpikeGenerator : MonoBehaviour {

    public GameObject spikes;
    [Range(0, 100)]
    public float spikeGenerationChance;
    public ObjectPooler spikePool;

    void OnEnable ()
    {
        PlatformGenerator.OnCreate += GenerateSpikes;
    }
    void OnDisable ()
    {
        PlatformGenerator.OnCreate -= GenerateSpikes;
    }

    void GenerateSpikes (GameObject platform, float platformWidth)
    {
        if (Random.Range(0f, 100f) < spikeGenerationChance)
        {
            // Generate spikes
            Vector3 spikePosition = platform.transform.position + Vector3.up * 0.5f;
            Vector3 spikeOffset = new Vector3(Random.Range(-platformWidth / 2 + 0.5f, platformWidth / 2 - 0.5f), 0, 0);
            PlaceSpikes(spikePosition + spikeOffset);
        }
        else
        {
            // Don't generate spikes
        }
    }

    void PlaceSpikes (Vector3 position)
    {
        GameObject newSpike = spikePool.GetPooledObject();
        newSpike.transform.position = position;
        newSpike.transform.rotation = Quaternion.identity;
        newSpike.SetActive(true);
    }
}
