using UnityEngine;

/// <summary>
/// Cancella checkpoint salvati all'avvio della scena
/// Usare quando vuoi che i checkpoint siano solo per sessione corrente
/// </summary>
public class ClearCheckpointOnStart : MonoBehaviour
{
    [SerializeField] private bool _clearOnAwake = true;

    private void Awake()
    {
        if (_clearOnAwake && CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.ClearCheckpoint();
            Debug.Log("Checkpoint cancellati all'avvio della scena");
        }
    }
}
