using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Helper UI per gestire azioni legate ai checkpoint
/// Collegare ai pulsanti UI per Save/Load/Restart
/// </summary>
public class CheckpointUI : MonoBehaviour
{
    /// <summary>
    /// Salva il checkpoint corrente (da pulsante UI)
    /// </summary>
    public void SaveCurrentCheckpoint()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.SaveCheckpoint();
        }
        else
        {
            Debug.LogWarning("CheckpointUI: CheckpointManager non trovato!");
        }
    }

    /// <summary>
    /// Carica l'ultimo checkpoint (da pulsante UI)
    /// </summary>
    public void LoadLastCheckpoint()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.LoadCheckpoint();
        }
        else
        {
            Debug.LogWarning("CheckpointUI: CheckpointManager non trovato!");
        }
    }

    /// <summary>
    /// Ricarica la scena corrente (da pulsante UI)
    /// </summary>
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// Ricarica la scena e carica il checkpoint (da pulsante UI)
    /// </summary>
    public void RestartFromCheckpoint()
    {
        if (CheckpointManager.Instance != null && CheckpointManager.Instance.HasCheckpoint)
        {
            CheckpointManager.Instance.SaveToPlayerPrefs();
            
            RestartLevel();
        }
        else
        {
            Debug.LogWarning("CheckpointUI: Nessun checkpoint disponibile!");
            RestartLevel();
        }
    }

    /// <summary>
    /// Cancella il checkpoint salvato (da pulsante UI)
    /// </summary>
    public void ClearSavedCheckpoint()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.ClearCheckpoint();
            Debug.Log("Checkpoint cancellato!");
        }
    }
}
