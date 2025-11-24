using UnityEngine;

/// <summary>
/// Rappresenta un singolo dipinto raccoglibile nel gioco
/// Gestisce la rotazione visiva e notifica il PaintingManager quando raccolto
/// </summary>
public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private float _destroyDelay = 0.1f;

    private bool _isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollected) return;

        if (other.CompareTag("Player"))
        {
            _isCollected = true;
            
            if (ParticleEffectManager.Instance != null)
            {
                ParticleEffectManager.Instance.PlayPaintingCollectEffect(transform.position);
            }
            
            if (PaintingManager.Instance != null)
            {
                PaintingManager.Instance.CollectPainting();
            }
            else
            {
                Debug.LogWarning("PaintingManager non trovato nella scena!");
            }

            Destroy(gameObject, _destroyDelay);
        }
    }

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);
    }
}
