using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Trigger che salva un checkpoint quando il player lo attraversa
/// Può essere usato anche per attivare effetti visivi/sonori
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] private bool _activateOnce = true;
    [SerializeField] private bool _saveOnActivate = true;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject _activeVisual;
    [SerializeField] private GameObject _inactiveVisual;
    [SerializeField] private ParticleSystem _activationParticles;

    [Header("Events")]
    [SerializeField] private UnityEvent _onCheckpointActivated;

    private bool _isActivated = false;
    private BoxCollider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider>();
        _trigger.isTrigger = true;

        UpdateVisuals();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_isActivated && _activateOnce)
            return;

        ActivateCheckpoint();
    }

    /// <summary>
    /// Attiva il checkpoint
    /// </summary>
    private void ActivateCheckpoint()
    {
        _isActivated = true;

        if (_saveOnActivate && CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.SaveCheckpoint();
        }

        if (_activationParticles != null)
        {
            _activationParticles.Play();
        }

        UpdateVisuals();

        _onCheckpointActivated?.Invoke();

        Debug.Log($"Checkpoint '{gameObject.name}' attivato!");
    }

    /// <summary>
    /// Aggiorna i visual in base allo stato
    /// </summary>
    private void UpdateVisuals()
    {
        if (_activeVisual != null)
        {
            _activeVisual.SetActive(_isActivated);
        }

        if (_inactiveVisual != null)
        {
            _inactiveVisual.SetActive(!_isActivated);
        }
    }

    /// <summary>
    /// Resetta lo stato del checkpoint
    /// </summary>
    public void ResetCheckpoint()
    {
        _isActivated = false;
        UpdateVisuals();
    }

    /// <summary>
    /// Forza l'attivazione del checkpoint da codice
    /// </summary>
    public void ForceActivate()
    {
        ActivateCheckpoint();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = _isActivated ? Color.green : Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
#endif
}
