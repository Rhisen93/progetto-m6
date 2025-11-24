using UnityEngine;

/// <summary>
/// Carica automaticamente il checkpoint all'avvio della scena se disponibile
/// Aggiungere a un GameObject nella scena (es. GameManager)
/// </summary>
public class AutoLoadCheckpoint : MonoBehaviour
{
    [SerializeField] private bool _loadOnStart = true;
    [SerializeField] private float _delaySeconds = 0.5f;

    private void Start()
    {
        if (_loadOnStart && CheckpointManager.Instance != null && CheckpointManager.Instance.HasCheckpoint)
        {
            Invoke(nameof(LoadCheckpoint), _delaySeconds);
        }
    }

    private void LoadCheckpoint()
    {
        CheckpointManager.Instance.LoadFromPlayerPrefs();
        CheckpointManager.Instance.LoadCheckpoint();
        Debug.Log("Checkpoint caricato automaticamente all'avvio!");
    }
}
