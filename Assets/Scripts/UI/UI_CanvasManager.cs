using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestisce le azioni dei pulsanti UI per navigazione scene e quit
/// </summary>
public class UI_CanvasManager : MonoBehaviour
{
    /// <summary>
    /// Avvia il gioco caricando la scena di gioco (indice 1)
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    /// <summary>
    /// Esce dall'applicazione
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Uscita dal gioco...");
        Application.Quit();
    }
    
    /// <summary>
    /// Ritorna al menu principale (scena 0)
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
