using UnityEngine;

/// <summary>
/// Piattaforma distruttibile che si elimina quando il Player ci sale sopra
/// NOTA: Richiede Collider con isTrigger = true sull'oggetto
/// </summary>
public class Platform : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 0.5f;
    
    private bool _isDestroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroyed) return;
        
        if (other.CompareTag("Player"))
        {
            _isDestroyed = true;
            Destroy(gameObject, _destroyDelay);
        }
    }
}
