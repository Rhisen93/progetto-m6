using UnityEngine;

/// <summary>
/// Torre nemica che spara proiettili verso il player quando è nel raggio di rilevamento
/// Utilizza il sistema di pooling per i proiettili
/// </summary>
public class Tower : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float _detectionRange = 10f;
    
    [Header("Combat Settings")]
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private string _bulletPoolTag = "Bullet";
    
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _firePoint;

    private float _fireCooldown = 0f;

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _detectionRange)
        {
            transform.LookAt(_player);

            if (_fireCooldown <= 0f)
            {
                Shoot();
                _fireCooldown = 1f / _fireRate;
            }
        }

        _fireCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (Pooling.Instance != null && _firePoint != null)
        {
            Pooling.Instance.SpawnFromPool(_bulletPoolTag, _firePoint.position, _firePoint.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}
