using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolSettings
{
    public string tag;
    [field: SerializeField] public FluidParticle prefab;
    public int poolSize;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public FluidParticle prefabForFluidParticle;

    public List<PoolSettings> pools;
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

        Debug.Log(this.enabled);
        foreach (PoolSettings settings in pools)
        {
            Queue<FluidParticle> pool = new Queue<FluidParticle>();

            for (int i = 0; i < settings.poolSize; i++)
            {
                if (settings.prefab == null) settings.prefab = prefabForFluidParticle;
                FluidParticle obj = Instantiate(settings.prefab, transform);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }

            poolDictionary.Add(settings.tag, pool);
        }
        Debug.Log(this.enabled);
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
        pooledObject.gameObject.SetActive(true);
        currentQueue.Enqueue(pooledObject);
        return pooledObject;
    }

}
