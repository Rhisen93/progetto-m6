using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema di Object Pooling per ottimizzare la creazione/distruzione di oggetti frequenti
/// Utilizza il Singleton pattern per accesso globale
/// </summary>
public class Pooling : MonoBehaviour
{
    public static Pooling Instance { get; private set; }

    /// <summary>
    /// Classe che definisce un pool di oggetti configurabile dall'Inspector
    /// </summary>
    [System.Serializable]
    public class Pool
    {
        [Tooltip("Tag identificativo del pool")]
        public string tag;
        
        [Tooltip("Prefab da istanziare nel pool")]
        public GameObject prefab;
        
        [Tooltip("Numero iniziale di oggetti da pre-istanziare")]
        public int size;
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Più di un'istanza di Pooling trovata! Distruzione duplicato.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            if (string.IsNullOrEmpty(pool.tag))
            {
                Debug.LogError("Pool con tag vuoto trovato! Saltato.");
                continue;
            }

            if (pool.prefab == null)
            {
                Debug.LogError($"Pool '{pool.tag}' non ha un prefab assegnato! Saltato.");
                continue;
            }

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
            Debug.Log($"Pool '{pool.tag}' inizializzato con {pool.size} oggetti.");
        }
    }

    /// <summary>
    /// Ottiene un oggetto dal pool e lo attiva
    /// </summary>
    /// <param name="tag">Tag del pool da cui ottenere l'oggetto</param>
    /// <param name="position">Posizione in cui posizionare l'oggetto</param>
    /// <param name="rotation">Rotazione dell'oggetto</param>
    /// <returns>GameObject dal pool, o null se il pool non esiste</returns>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool con tag '{tag}' non esiste!");
            return null;
        }

        GameObject obj = _poolDictionary[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        _poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    /// <summary>
    /// Ritorna un oggetto al pool disattivandolo
    /// </summary>
    /// <param name="obj">GameObject da ritornare al pool</param>
    public void ReturnToPool(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("Tentativo di ritornare un oggetto null al pool!");
            return;
        }

        obj.SetActive(false);
    }
}
