using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestisce la visualizzazione della barra vita e del testo HP nell'UI
/// </summary>
public class UI_LifePanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _lifeFillable;
    [SerializeField] private TextMeshProUGUI _lifeText;
    
    /// <summary>
    /// Aggiorna la grafica della vita in base agli HP correnti e massimi
    /// Questo metodo può essere collegato all'evento OnHpChanged del LifeController
    /// </summary>
    public void UpdateGraphics(int currentHp, int maxHp)
    {
        if (_lifeText != null)
        {
            _lifeText.text = "HP " + currentHp + "/" + maxHp;
        }
        
        if (_lifeFillable != null && maxHp > 0)
        {
            _lifeFillable.fillAmount = (float)currentHp / maxHp;
        }
    }
}
