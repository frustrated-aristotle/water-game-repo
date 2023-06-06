using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        
        Instance = this;
    }

    #endregion
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolsDictionary;

    // Update is called once per frame
    void Start()
    {
        poolsDictionary =  new Dictionary<string, Queue<GameObject>>(); 
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolsDictionary.Add(pool.tag, objectPool);
            
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolsDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool with the tag : " + tag + " doesn't exist.");
            return null;
        }
        
        GameObject objectToSpawn = poolsDictionary[tag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolsDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
        
        
    }
}
