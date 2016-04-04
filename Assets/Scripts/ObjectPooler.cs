using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour, IResettable {

    public GameObject pooledObjectHolder;
    public GameObject pooledObject;

    public int poolSize;

    List<GameObject> pooledObjects;

	// Use this for initialization
	void Start () {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            AddToPool();
        }
	}

    GameObject AddToPool ()
    {
        GameObject obj = (GameObject)Instantiate(pooledObject);
        if(pooledObjectHolder != null)
        {
            obj.transform.parent = pooledObjectHolder.transform;
        }
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
	
	public GameObject GetPooledObject ()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = AddToPool();
        return obj;
    }

    public List<GameObject> GetPooledObjects (int amount)
    {
        List<GameObject> r = new List<GameObject>();
        if (amount == 0)
        {
            return r;
        }

        foreach (var obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                r.Add(obj);
                if(r.Count == amount)
                {
                    return r;
                }
            }
        }

        int left = amount - r.Count;
        int before = r.Count;
        int after;
        for (int i = 0; i < left; i++)
        {
            r.Add(AddToPool());
        }
        after = r.Count;
        if (r.Count < amount)
        {
            Debug.LogError("Needed another " + left + " objects. Created another " + (after - before) + "objects. " + (amount - after) + " objects short.");
        }
        return r;
    }

    public void RecallAllPooledObjects ()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(false);
            }
        }
    }

    public void Reset ()
    {
        RecallAllPooledObjects();
    }
}
