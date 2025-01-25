using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolSettings
{
    public string tag;
    public int poolSize;
    public int fluidID;
}

public class FluidPooler : MonoBehaviour
{
    public static FluidPooler Instance;

    public List<PoolSettings> pools;
    public FluidParticle prefabForFluidParticle;

    private Dictionary<string, Queue<FluidParticle>> poolDictionary = new Dictionary<string, Queue<FluidParticle>>();

    private FluidParticle pooledObject; // To avoid creation of a new object each time GetPoolObject() is called
    private Queue<FluidParticle> currentQueue = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        foreach (PoolSettings settings in pools)
        {
            Queue<FluidParticle> pool = new Queue<FluidParticle>();

            for (int i = 0; i < settings.poolSize; i++)
            {
                FluidParticle obj = Instantiate(prefabForFluidParticle, transform);
                obj.ID = settings.fluidID;
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }

            poolDictionary.Add(settings.tag, pool);
        }
    }

    public FluidParticle GetPoolObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("poolDictionary doesn't contain " + tag + " tag!");
            return null;
        }

        currentQueue = poolDictionary[tag];
        pooledObject = currentQueue.Dequeue();
        pooledObject.Initialize();
        pooledObject.gameObject.SetActive(true);
        currentQueue.Enqueue(pooledObject);
        return pooledObject;
    }

}
