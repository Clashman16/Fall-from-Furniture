using UnityEngine;

namespace FFF.Behaviours.UI
{
   public class QuitButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         Application.Quit();
      }
   }
}
