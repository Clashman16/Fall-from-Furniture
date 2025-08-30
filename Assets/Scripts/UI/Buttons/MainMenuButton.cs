using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class MainMenuButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.TITLE_SCREEN;
      }
   }
}
