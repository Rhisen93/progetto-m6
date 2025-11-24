using TMPro;
using UnityEngine;

/// <summary>
/// Gestisce un timer countdown visualizzato nell'UI
/// </summary>
public class Timer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _timerText;
    
    [Header("Timer Settings")]
    [SerializeField] private float _timeLeft = 180f;

    private void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }
        else
        {
            _timeLeft = 0;
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (_timerText == null) return;
        
        int minutes = Mathf.FloorToInt(_timeLeft / 60f);
        int seconds = Mathf.FloorToInt(_timeLeft - minutes * 60);
        _timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
