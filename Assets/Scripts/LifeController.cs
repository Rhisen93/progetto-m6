using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gestisce i punti vita di un'entità (Player o nemico)
/// Invia eventi quando la vita cambia o l'entità muore
/// </summary>
public class LifeController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _currentHp = 20;
    [SerializeField] private int _maxHp = 20;
    [SerializeField] private bool _fullHpOnAwake = true;
    
    [Header("UI References")]
    [SerializeField] private Canvas _canvasDefeat;
    [SerializeField] private Canvas _canvasGameplay;

    [Header("Events")]
    [SerializeField] private UnityEvent<int, int> _onHpChanged;
    [SerializeField] private UnityEvent _onDeath;

    public int CurrentHp => _currentHp;
    public int MaxHp => _maxHp;

    private void Awake()
    {
        if (_canvasDefeat != null)
        {
            _canvasDefeat.gameObject.SetActive(false);
        }

        if (_fullHpOnAwake)
        {
            SetHp(_maxHp);
        }
    }

    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        _currentHp = hp;


        _onHpChanged?.Invoke(_currentHp, _maxHp);

        if (_currentHp == 0)
        {
            if (ParticleEffectManager.Instance != null)
            {
                ParticleEffectManager.Instance.PlayDeathEffect(transform.position);
            }
            
            Debug.Log($"{gameObject.name} è morto.");
            _onDeath?.Invoke();
            
            if (_canvasDefeat != null)
            {
                _canvasDefeat.gameObject.SetActive(true);
            }
            
            if (_canvasGameplay != null)
            {
                _canvasGameplay.gameObject.SetActive(false);
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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void AddHp(int amount) => SetHp(_currentHp + amount);

    /// <summary>
    /// Infligge danno al LifeController riducendo gli HP
    /// </summary>
    /// <param name="damage">Quantità di danno da infliggere</param>
    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        
        if (ParticleEffectManager.Instance != null)
        {
            ParticleEffectManager.Instance.PlayDamageEffect(transform.position);
        }
        
        SetHp(_currentHp - damage);
        Debug.Log($"{gameObject.name} ha subito {damage} danni. HP rimanenti: {_currentHp}/{_maxHp}");
    }
}