using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class ResumeButtonBehaviour : OneActionButtonBehaviour
   {
      ScreenManagerBehaviour m_screenManager;

      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.GAME_SCREEN;

         if(m_screenManager == null)
         {
            m_screenManager = FindObjectOfType<ScreenManagerBehaviour>();
         }

         m_screenManager.IsResuming = true;
      }
   }
}
