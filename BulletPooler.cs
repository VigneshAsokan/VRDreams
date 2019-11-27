using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour {
     [System.Serializable]
    public class Pool
    {
        public string Type;
        public GameObject bulletPrefab;
        public int MagSize;
    }
    public GameObject Bulletparent;
    public static BulletPooler Instance;
    public static bool MagStatus;
    public int MagTempSize;
    public int resetMag;
    public bool Automatic = false;


    private void Awake()
    {
        Instance = this;
    }


    public List<Pool> pools;  
    public Dictionary<string, Queue<GameObject>> BulletDictionary;

    private void Start()
    {
        BulletDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            
            Queue<GameObject> ObjectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.MagSize; i++)
            {
                    GameObject obj = Instantiate(pool.bulletPrefab,Bulletparent.transform);
                    obj.SetActive(false);
                    ObjectPool.Enqueue(obj);                
            }
            
            BulletDictionary.Add(pool.Type, ObjectPool);
            MagTempSize = pool.MagSize;
        }
        resetMag = MagTempSize;
    }

    public GameObject SpawnfromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (MagTempSize > 0)
        {
            if (!BulletDictionary.ContainsKey(tag))
            {
                return null;
            }
            GameObject BulletToSpawn = BulletDictionary[tag].Dequeue();

            BulletToSpawn.SetActive(true);
            BulletToSpawn.transform.position = position;
            BulletToSpawn.transform.rotation = rotation;
            BulletDictionary[tag].Enqueue(BulletToSpawn);
            MagTempSize--;
            Debug.Log(MagTempSize);
            return BulletToSpawn;
        }
        else
        {
            GetComponent<Animator>().SetBool("InitReload", true);
            return null;
        }
        
    }
}
