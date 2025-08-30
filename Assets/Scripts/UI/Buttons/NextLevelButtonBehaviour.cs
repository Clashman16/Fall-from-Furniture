using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class NextLevelButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerSaveSingleton.Instance.CurrentLevelId += 1;

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.GAME_SCREEN;
      }
   }
}
