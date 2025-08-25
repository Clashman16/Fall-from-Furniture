using FFF.Utils;

namespace FFF.Player
{
   public sealed class PlayerStateSingleton
   {
      private static PlayerStateSingleton m_instance = null;

      private EGameScreen m_gameScreen;

      public EGameScreen GameScreen
      {
         get => m_gameScreen;
         set => m_gameScreen = value;
      }

      public static PlayerStateSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new PlayerStateSingleton();
            }
            return m_instance;
         }
      }

      private PlayerStateSingleton()
      {
         //TODO: When the game will have a menu, use an external class to switch between screens and launch a game
         m_gameScreen = EGameScreen.GAME_SCREEN;
      }
   }
}
