using TMPro;
using UnityEngine;

/// <summary>
/// Gestisce la visualizzazione del contatore dipinti nell'UI
/// </summary>
public class UI_PaintingCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _paintingText;

    private void Start()
    {
        if (PaintingManager.Instance != null)
        {
            UpdateDisplay(0, PaintingManager.Instance.TotalPaintings);
        }
    }

    /// <summary>
    /// Aggiorna il display del contatore dipinti
    /// Questo metodo può essere collegato all'evento OnPaintingCollected del PaintingManager
    /// </summary>
    public void UpdateDisplay(int collected, int total)
    {
        if (_paintingText != null)
        {
            _paintingText.text = $"Paintings: {collected}/{total}";
        }
    }
}
