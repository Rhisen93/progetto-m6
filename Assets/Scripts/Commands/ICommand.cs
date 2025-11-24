/// <summary>
/// Interface base per il Command Pattern
/// Ogni comando incapsula un'azione che può essere eseguita
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Esegue il comando
    /// </summary>
    void Execute();
    
    /// <summary>
    /// Annulla il comando (opzionale, per replay/undo systems)
    /// </summary>
    void Undo();
}
