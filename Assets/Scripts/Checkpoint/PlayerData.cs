using UnityEngine;

/// <summary>
/// Contiene i dati del player da salvare/caricare
/// </summary>
[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public int CurrentHp;
    public int CollectedPaintings;
    public float PlayTime;

    /// <summary>
    /// Costruttore per creare snapshot dei dati correnti
    /// </summary>
    public PlayerData(Transform playerTransform, int hp, int paintings, float time)
    {
        Position = playerTransform.position;
        Rotation = playerTransform.rotation;
        CurrentHp = hp;
        CollectedPaintings = paintings;
        PlayTime = time;
    }

    /// <summary>
    /// Costruttore vuoto per deserializzazione
    /// </summary>
    public PlayerData() { }
}
