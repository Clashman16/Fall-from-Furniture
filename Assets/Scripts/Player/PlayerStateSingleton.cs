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
         // m_gameScreen is set in ScreenManagerBehaviour.Update
      }
   }
}
