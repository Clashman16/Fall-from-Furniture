

using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class ResetButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.GAME_SCREEN;

         //GameResetter.Reset();
      }
   }
}
