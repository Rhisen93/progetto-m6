using UnityEngine;

/// <summary>
/// Rileva quando il player cade fuori dal livello
/// Può mostrare UI di Game Over o far respawnare il player all'ultimo checkpoint
/// Posizionare questo script su un GameObject con BoxCollider (isTrigger = true) sotto il livello
/// </summary>
public class FallDetector : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Canvas _fallCanvas;
    [SerializeField] private Canvas _gameplayCanvas;

    [Header("Respawn Settings")]
    [SerializeField] private bool _useCheckpointRespawn = true;
    [SerializeField] private bool _showUIOnFall = true;
    
    private void Awake()
    {
        if (_fallCanvas != null)
        {
            _fallCanvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caduto fuori dal livello!");

            if (_useCheckpointRespawn && CheckpointManager.Instance != null && CheckpointManager.Instance.HasCheckpoint)
            {
                RespawnPlayer(other);
            }
            else
            {
                ShowGameOver(other);
            }
        }
    }

    /// <summary>
    /// Respawn del player all'ultimo checkpoint
    /// </summary>
    private void RespawnPlayer(Collider playerCollider)
    {
        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.isKinematic = true;
        }

        CheckpointManager.Instance.RespawnAtCheckpoint();

        if (playerRb != null)
        {
            playerRb.isKinematic = false;
        }

        Debug.Log("Player respawnato all'ultimo checkpoint!");
    }

    /// <summary>
    /// Mostra schermata Game Over
    /// </summary>
    private void ShowGameOver(Collider playerCollider)
    {
        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.isKinematic = true;
        }

        PlayerController playerController = playerCollider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            CameraOrbit cameraOrbit = mainCamera.GetComponent<CameraOrbit>();
            if (cameraOrbit != null)
            {
                cameraOrbit.enabled = false;
            }
        }

        if (_showUIOnFall)
        {
            if (_fallCanvas != null)
            {
                _fallCanvas.gameObject.SetActive(true);
            }

            if (_gameplayCanvas != null)
            {
                _gameplayCanvas.gameObject.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
