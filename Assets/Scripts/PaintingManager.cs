using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gestisce il conteggio e lo stato dei dipinti raccolti nel livello
/// Singleton pattern per accesso globale
/// </summary>
public class PaintingManager : MonoBehaviour
{
    public static PaintingManager Instance { get; private set; }

    [SerializeField] private int _totalPaintings = 0;
    private int _collectedPaintings = 0;

    [SerializeField] private UnityEvent<int, int> _onPaintingCollected;

    public int CollectedPaintings => _collectedPaintings;
    public int TotalPaintings => _totalPaintings;
    public bool AllPaintingsCollected => _totalPaintings > 0 && _collectedPaintings >= _totalPaintings;

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

        _collectedPaintings = 0;

        if (_totalPaintings == 0)
        {
            Debug.LogWarning("PaintingManager: Total Paintings è 0! Imposta il numero totale di dipinti nell'Inspector.");
        }
        else
        {
            Debug.Log($"PaintingManager inizializzato. Dipinti totali nel livello: {_totalPaintings}");
        }
    }

    /// <summary>
    /// Registra la raccolta di un dipinto
    /// </summary>
    public void CollectPainting()
    {
        _collectedPaintings++;
        _onPaintingCollected?.Invoke(_collectedPaintings, _totalPaintings);
        
        Debug.Log($"Dipinto raccolto! {_collectedPaintings}/{_totalPaintings}");

        if (AllPaintingsCollected)
        {
            Debug.Log("Tutti i dipinti sono stati raccolti! Puoi raggiungere la vittoria!");
        }
    }

    /// <summary>
    /// Resetta il conteggio dei dipinti
    /// </summary>
    public void ResetPaintings()
    {
        _collectedPaintings = 0;
    }
}
