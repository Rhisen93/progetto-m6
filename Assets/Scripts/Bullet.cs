using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _lifeTime = 5f;

    private Transform _target;
    private static Transform _cachedPlayerTransform;

    private void Awake()
    {
        if (_cachedPlayerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _cachedPlayerTransform = playerObj.transform;
            }
        }
        
        _target = _cachedPlayerTransform;
    }

    private void OnEnable()
    {
        Invoke("ReturnToPool", _lifeTime);
    }

    private void Update()
    {
        if (_target == null) return;

        Vector3 direction = (_target.position + Vector3.up - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController lifeController = other.GetComponent<LifeController>();
            
            if (lifeController != null)
            {
                lifeController.TakeDamage((int)_damage);
            }
            
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        this.gameObject.SetActive(false);
        Pooling.Instance.ReturnToPool(this.gameObject);
    }

}