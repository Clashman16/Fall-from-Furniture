using FFF.Player;
using FFF.Utils;

namespace FFF.Behaviours.UI
{
   public class SettingsButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = EGameScreen.SETTINGS_SCREEN;
      }
   }
}
