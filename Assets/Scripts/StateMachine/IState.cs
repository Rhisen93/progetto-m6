/// <summary>
/// Interface base per il State Pattern
/// Ogni stato gestisce il comportamento del player in una condizione specifica
/// </summary>
public interface IState
{
    /// <summary>
    /// Chiamato quando si entra nello stato
    /// </summary>
    void Enter();
    
    /// <summary>
    /// Chiamato ogni frame mentre si è nello stato
    /// </summary>
    void Update();
    
    /// <summary>
    /// Chiamato ogni physics frame mentre si è nello stato
    /// </summary>
    void FixedUpdate();
    
    /// <summary>
    /// Chiamato quando si esce dallo stato
    /// </summary>
    void Exit();
}
