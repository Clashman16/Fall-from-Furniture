using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class CreditsButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.CREDITS_SCREEN;
      }
   }
}
