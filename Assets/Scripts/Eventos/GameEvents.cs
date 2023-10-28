using System;

public static class GameEvents
{
    public static event Action OnPause;         // evento pausa del juego
    public static event Action OnResume;        // evento reanuda el juego
    public static event Action OnGameOver;      // evento game over
    public static event Action OnVictory;       // evento victoria

    public static void TriggerPause() => OnPause?.Invoke();         // invoca al evento pausa
    public static void TriggerResume() => OnResume?.Invoke();       // invoca al evento reanudar
    public static void TriggerGameOver() => OnGameOver?.Invoke();   // invoca al evento game over
    public static void TriggerVictory() => OnVictory?.Invoke();     // invoca al evento victoria



}
