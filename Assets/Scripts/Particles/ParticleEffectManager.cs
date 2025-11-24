using UnityEngine;

/// <summary>
/// Manager centralizzato per spawning effetti particellari
/// Fornisce metodi helper per spawnare effetti comuni
/// </summary>
public class ParticleEffectManager : MonoBehaviour
{
    public static ParticleEffectManager Instance { get; private set; }

    [Header("Particle Tags")]
    [SerializeField] private string _paintingCollectTag = "PaintingCollect";
    [SerializeField] private string _jumpTag = "Jump";
    [SerializeField] private string _damageTag = "Damage";
    [SerializeField] private string _deathTag = "Death";
    [SerializeField] private string _victoryTag = "Victory";

    [Header("Spawn Offsets")]
    [SerializeField] private Vector3 _jumpOffset = new Vector3(0, 0.1f, 0);
    [SerializeField] private Vector3 _damageOffset = new Vector3(0, 1f, 0);
    [SerializeField] private Vector3 _deathOffset = new Vector3(0, 1f, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Spawna effetto raccolta dipinto
    /// </summary>
    /// <param name="position">Posizione del dipinto raccolto</param>
    public void PlayPaintingCollectEffect(Vector3 position)
    {
        if (ParticlePool.Instance != null)
        {
            ParticlePool.Instance.SpawnFromPool(_paintingCollectTag, position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawna effetto salto
    /// </summary>
    /// <param name="position">Posizione ai piedi del player</param>
    public void PlayJumpEffect(Vector3 position)
    {
        if (ParticlePool.Instance != null)
        {
            Vector3 spawnPos = position + _jumpOffset;
            ParticlePool.Instance.SpawnFromPool(_jumpTag, spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawna effetto danno
    /// </summary>
    /// <param name="position">Posizione del danno</param>
    public void PlayDamageEffect(Vector3 position)
    {
        if (ParticlePool.Instance != null)
        {
            Vector3 spawnPos = position + _damageOffset;
            ParticlePool.Instance.SpawnFromPool(_damageTag, spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawna effetto morte
    /// </summary>
    /// <param name="position">Posizione del player morto</param>
    public void PlayDeathEffect(Vector3 position)
    {
        if (ParticlePool.Instance != null)
        {
            Vector3 spawnPos = position + _deathOffset;
            ParticlePool.Instance.SpawnFromPool(_deathTag, spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawna effetto vittoria
    /// </summary>
    /// <param name="position">Posizione del player vittorioso</param>
    public void PlayVictoryEffect(Vector3 position)
    {
        if (ParticlePool.Instance != null)
        {
            ParticlePool.Instance.SpawnFromPool(_victoryTag, position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawna effetto custom dal pool
    /// </summary>
    /// <param name="tag">Tag del pool</param>
    /// <param name="position">Posizione spawn</param>
    /// <param name="rotation">Rotazione spawn</param>
    public void PlayCustomEffect(string tag, Vector3 position, Quaternion rotation)
    {
        if (ParticlePool.Instance != null)
        {
            ParticlePool.Instance.SpawnFromPool(tag, position, rotation);
        }
    }
}
