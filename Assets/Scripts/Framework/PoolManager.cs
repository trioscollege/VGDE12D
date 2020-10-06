using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Member class for a prefab entered into the object pool
    [Serializable]
    public class ObjectPoolEntry
    {
        // the object to pre instantiate
        [SerializeField]
        public GameObject prefab;
        // quantity of object to pre-instantiate
        [SerializeField]
        public int count = 3;
    }

    // The object prefabs which the pool can handle.
    public ObjectPoolEntry[] m_entries;

    // The pooled objects currently available. Indexed by the index of the objectPrefabs.
    [HideInInspector]
    public List<GameObject>[] m_pool;
    // container object to store unused items in.
    protected GameObject m_containerObject;
    public static PoolManager Instance { get { return s_instance; } }
    private static PoolManager s_instance = null;

    void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            s_instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        InitializePool();
    }

    // Use this for initialization
    void InitializePool()
    {

        m_containerObject = new GameObject("ObjectPool");
        m_containerObject.transform.parent = transform;
        // Loop through the object prefabs and make a new list for each one.
        m_pool = new List<GameObject>[m_entries.Length];

        for (int i = 0; i < m_entries.Length; i++)
        {
            ObjectPoolEntry objectPrefab = m_entries[i];
            //create the repository and fill it
            m_pool[i] = new List<GameObject>();
            for (int n = 0; n < objectPrefab.count; n++)
            {
                GameObject newObj = Instantiate(objectPrefab.prefab) as GameObject;
                newObj.name = objectPrefab.prefab.name;
                PoolObject(newObj);
            }
        }
    }

    public GameObject GetObjectForType(string objectType, bool onlyPooled)
    {

        for (int i = 0; i < m_entries.Length; i++)
        {
            GameObject prefab = m_entries[i].prefab;

            if (prefab.name == objectType)
            {
                if (m_pool[i].Count > 0)
                {
                    GameObject pooledObject = m_pool[i][0];
                    m_pool[i].RemoveAt(0);
                    pooledObject.transform.parent = null;
                    pooledObject.SetActive(true);
                    return pooledObject;
                }

                if (!onlyPooled)
                {
                    GameObject newObj = Instantiate(m_entries[i].prefab) as GameObject;
                    newObj.name = m_entries[i].prefab.name;
                    return newObj;
                }
            }
        }

        // Otherwise no object of the specified type or none were left in the pool with onlyPooled set to true
        return null;
    }

    // Places the object back into a pool of the appropriate type if one exists.
    public void PoolObject(GameObject obj)
    {

        for (int i = 0; i < m_entries.Length; i++)
        {
            if (m_entries[i].prefab.name == obj.name)
            {
                obj.SetActive(false);
                obj.transform.parent = m_containerObject.transform;
                m_pool[i].Add(obj);
                return;
            }
        }
    }
}