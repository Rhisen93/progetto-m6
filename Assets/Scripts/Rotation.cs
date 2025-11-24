using UnityEngine;

/// <summary>
/// Fa ruotare continuamente l'oggetto attorno all'asse X
/// </summary>
public class Rotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);
    }
}
