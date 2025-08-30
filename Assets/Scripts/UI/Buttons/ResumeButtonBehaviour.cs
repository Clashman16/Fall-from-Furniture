using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class ResumeButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.GAME_SCREEN;
      }
   }
}
