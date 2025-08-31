using FFF.Player;

namespace FFF.Behaviours.UI
{
   public class ClearDataButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerSaveSingleton.Instance.CurrentLevelId = 0;
      }
   }
}
