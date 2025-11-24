using UnityEngine;

/// <summary>
/// Gestore centralizzato degli input del player
/// Utilizza Command Pattern per separare input da esecuzione
/// Permette rebinding e replay degli input
/// </summary>
public class InputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private string _verticalAxis = "Vertical";
    [SerializeField] private string _jumpButton = "Jump";

    private ICommand _moveCommand;
    private ICommand _jumpCommand;

    /// <summary>
    /// Ottiene l'input orizzontale
    /// </summary>
    public float Horizontal => Input.GetAxis(_horizontalAxis);

    /// <summary>
    /// Ottiene l'input verticale
    /// </summary>
    public float Vertical => Input.GetAxis(_verticalAxis);

    /// <summary>
    /// Verifica se il pulsante di salto è stato premuto
    /// </summary>
    public bool JumpButtonDown => Input.GetButtonDown(_jumpButton);

    /// <summary>
    /// Imposta il comando di movimento
    /// </summary>
    /// <param name="command">Comando da eseguire</param>
    public void SetMoveCommand(ICommand command)
    {
        _moveCommand = command;
    }

    /// <summary>
    /// Imposta il comando di salto
    /// </summary>
    /// <param name="command">Comando da eseguire</param>
    public void SetJumpCommand(ICommand command)
    {
        _jumpCommand = command;
    }

    /// <summary>
    /// Esegue il comando di movimento se impostato
    /// </summary>
    public void ExecuteMoveCommand()
    {
        _moveCommand?.Execute();
    }

    /// <summary>
    /// Esegue il comando di salto se impostato
    /// </summary>
    public void ExecuteJumpCommand()
    {
        _jumpCommand?.Execute();
    }

    /// <summary>
    /// Permette di cambiare i binding degli input a runtime
    /// </summary>
    /// <param name="horizontal">Nome asse orizzontale</param>
    /// <param name="vertical">Nome asse verticale</param>
    /// <param name="jump">Nome pulsante salto</param>
    public void RebindInputs(string horizontal, string vertical, string jump)
    {
        _horizontalAxis = horizontal;
        _verticalAxis = vertical;
        _jumpButton = jump;
    }
}
