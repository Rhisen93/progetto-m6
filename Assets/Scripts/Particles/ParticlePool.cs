using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Pool per Particle Systems
/// Riutilizza particelle invece di istanziare/distruggere continuamente
/// </summary>
public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public ParticleSystem Prefab;
        public int Size = 5;
    }

    [Header("Particle Pools")]
    [SerializeField] private List<Pool> _pools = new List<Pool>();

    private Dictionary<string, Queue<ParticleSystem>> _poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePools();
    }

    /// <summary>
    /// Inizializza tutti i pool di particelle
    /// </summary>
    private void InitializePools()
    {
        _poolDictionary = new Dictionary<string, Queue<ParticleSystem>>();

        foreach (Pool pool in _pools)
        {
            if (pool.Prefab == null)
            {
                Debug.LogWarning($"ParticlePool: Prefab nullo per tag '{pool.Tag}'");
                continue;
            }

            Queue<ParticleSystem> objectPool = new Queue<ParticleSystem>();

            for (int i = 0; i < pool.Size; i++)
            {
                ParticleSystem obj = Instantiate(pool.Prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.Tag, objectPool);
        }

        Debug.Log($"ParticlePool inizializzato con {_pools.Count} pool");
    }

    /// <summary>
    /// Spawna particelle dal pool
    /// </summary>
    /// <param name="tag">Tag del pool</param>
    /// <param name="position">Posizione spawn</param>
    /// <param name="rotation">Rotazione spawn</param>
    /// <returns>Particle System spawnato</returns>
    public ParticleSystem SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"ParticlePool: Pool con tag '{tag}' non esiste!");
            return null;
        }

        ParticleSystem particleToSpawn = _poolDictionary[tag].Dequeue();

        particleToSpawn.transform.position = position;
        particleToSpawn.transform.rotation = rotation;
        particleToSpawn.gameObject.SetActive(true);

        particleToSpawn.Play();
        StartCoroutine(ReturnToPoolAfterDuration(particleToSpawn, tag));

        _poolDictionary[tag].Enqueue(particleToSpawn);

        return particleToSpawn;
    }

    /// <summary>
    /// Ritorna la particella al pool dopo la durata
    /// </summary>
    private System.Collections.IEnumerator ReturnToPoolAfterDuration(ParticleSystem particle, string tag)
    {
        yield return new WaitForSeconds(particle.main.duration + particle.main.startLifetime.constantMax);
        
        if (particle != null && particle.gameObject.activeSelf)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particle.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Ritorna manualmente una particella al pool
    /// </summary>
    /// <param name="particle">Particle System da ritornare</param>
    public void ReturnToPool(ParticleSystem particle)
    {
        if (particle != null)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particle.gameObject.SetActive(false);
        }
    }
}
