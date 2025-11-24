using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gestisce il sistema di checkpoint e salvataggio del player
/// Singleton pattern per accesso globale
/// Salva dati tramite PlayerPrefs con serializzazione JSON
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private LifeController _playerLifeController;
    [SerializeField] private Rigidbody _playerRigidbody;

    [Header("Checkpoint Settings")]
    [SerializeField] private bool _persistBetweenSessions = false;
    [SerializeField] private string _saveKey = "PlayerCheckpoint";

    [Header("Events")]
    [SerializeField] private UnityEvent _onCheckpointSaved;
    [SerializeField] private UnityEvent _onCheckpointLoaded;

    private PlayerData _lastCheckpoint;
    private float _playTime = 0f;

    public PlayerData LastCheckpoint => _lastCheckpoint;
    public bool HasCheckpoint => _lastCheckpoint != null;

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

        if (!_persistBetweenSessions && PlayerPrefs.HasKey(_saveKey))
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }

        if (_playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerTransform = player.transform;
                _playerLifeController = player.GetComponent<LifeController>();
                _playerRigidbody = player.GetComponent<Rigidbody>();
            }
        }
    }

    private void Update()
    {
        _playTime += Time.deltaTime;
    }

    /// <summary>
    /// Salva un checkpoint nella posizione corrente del player
    /// </summary>
    public void SaveCheckpoint()
    {
        if (_playerTransform == null)
        {
            Debug.LogError("CheckpointManager: PlayerTransform non assegnato!");
            return;
        }

        int currentHp = GetCurrentHp();
        int collectedPaintings = PaintingManager.Instance != null ? PaintingManager.Instance.CollectedPaintings : 0;

        _lastCheckpoint = new PlayerData(_playerTransform, currentHp, collectedPaintings, _playTime);

        if (_persistBetweenSessions)
        {
            SaveToPlayerPrefs();
        }

        _onCheckpointSaved?.Invoke();
        Debug.Log($"Checkpoint salvato - Posizione: {_lastCheckpoint.Position}, HP: {currentHp}, Dipinti: {collectedPaintings}");
    }

    /// <summary>
    /// Carica l'ultimo checkpoint salvato
    /// </summary>
    public void LoadCheckpoint()
    {
        if (_persistBetweenSessions && _lastCheckpoint == null && PlayerPrefs.HasKey(_saveKey))
        {
            LoadFromPlayerPrefs();
        }

        if (_lastCheckpoint == null)
        {
            Debug.LogWarning("CheckpointManager: Nessun checkpoint disponibile!");
            return;
        }

        if (_playerTransform == null)
        {
            Debug.LogError("CheckpointManager: PlayerTransform non assegnato!");
            return;
        }

        _playerTransform.position = _lastCheckpoint.Position;
        _playerTransform.rotation = _lastCheckpoint.Rotation;

        if (_playerRigidbody != null)
        {
            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.angularVelocity = Vector3.zero;
        }

        if (_playerLifeController != null)
        {
            _playerLifeController.SetHp(_lastCheckpoint.CurrentHp);
        }

        if (PaintingManager.Instance != null)
        {
            PaintingManager.Instance.ResetPaintings();
        }

        _playTime = _lastCheckpoint.PlayTime;

        _onCheckpointLoaded?.Invoke();
        Debug.Log($"Checkpoint caricato - Posizione: {_lastCheckpoint.Position}, HP: {_lastCheckpoint.CurrentHp}");
    }

    /// <summary>
    /// Salva i dati su PlayerPrefs usando JSON
    /// </summary>
    public void SaveToPlayerPrefs()
    {
        if (_lastCheckpoint == null)
        {
            Debug.LogWarning("CheckpointManager: Nessun checkpoint da salvare!");
            return;
        }

        string json = JsonUtility.ToJson(_lastCheckpoint);
        PlayerPrefs.SetString(_saveKey, json);
        PlayerPrefs.Save();
        Debug.Log("Checkpoint salvato su PlayerPrefs");
    }

    /// <summary>
    /// Carica i dati da PlayerPrefs
    /// </summary>
    public void LoadFromPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(_saveKey))
        {
            Debug.LogWarning("CheckpointManager: Nessun salvataggio trovato in PlayerPrefs!");
            return;
        }

        string json = PlayerPrefs.GetString(_saveKey);
        _lastCheckpoint = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Checkpoint caricato da PlayerPrefs");
    }

    /// <summary>
    /// Cancella il checkpoint salvato
    /// </summary>
    public void ClearCheckpoint()
    {
        _lastCheckpoint = null;
        if (PlayerPrefs.HasKey(_saveKey))
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
        Debug.Log("Checkpoint cancellato");
    }

    /// <summary>
    /// Respawn del player all'ultimo checkpoint (usato su morte)
    /// </summary>
    public void RespawnAtCheckpoint()
    {
        if (HasCheckpoint)
        {
            LoadCheckpoint();
        }
        else
        {
            Debug.LogWarning("CheckpointManager: Nessun checkpoint, impossibile fare respawn!");
        }
    }

    /// <summary>
    /// Ottiene gli HP correnti del player
    /// </summary>
    private int GetCurrentHp()
    {
        if (_playerLifeController != null)
        {
            return _playerLifeController.CurrentHp;
        }
        return 20; // Valore default
    }

    /// <summary>
    /// Ottiene il tempo di gioco totale
    /// </summary>
    public float GetPlayTime() => _playTime;
}
