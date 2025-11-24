using UnityEngine;

/// <summary>
/// Gestisce il canvas di vittoria, mostrandolo solo se tutti i dipinti sono stati raccolti
/// </summary>
public class CanvasPanel : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPanel;
    [SerializeField] private GameObject _warningPanel;
    
    private void Awake()
    {
        _canvasPanel.SetActive(false);
        
        if (_warningPanel != null)
        {
            _warningPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PaintingManager.Instance != null && PaintingManager.Instance.AllPaintingsCollected)
            {
                if (ParticleEffectManager.Instance != null)
                {
                    ParticleEffectManager.Instance.PlayVictoryEffect(other.transform.position);
                }
                
                _canvasPanel.SetActive(true);
                Debug.Log("Vittoria! Tutti i dipinti raccolti!");
            }
            else
            {
                if (_warningPanel != null)
                {
                    _warningPanel.SetActive(true);
                    
                    int remaining = PaintingManager.Instance != null ? 
                        PaintingManager.Instance.TotalPaintings - PaintingManager.Instance.CollectedPaintings : 0;
                    
                    Debug.Log($"Non puoi ancora vincere! Mancano {remaining} dipinti.");
                    
                    Invoke(nameof(HideWarning), 3f);
                }
                else
                {
                    Debug.Log("Raccogli tutti i dipinti prima di vincere!");
                }
            }
        }
    }

    private void HideWarning()
    {
        if (_warningPanel != null)
        {
            _warningPanel.SetActive(false);
        }
    }
}
