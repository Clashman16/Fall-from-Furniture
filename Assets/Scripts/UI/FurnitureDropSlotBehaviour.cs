using FFF.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace FFF.UI
{
   public class FurnitureDropSlotBehaviour : DashedLineBorderBehaviour
   {
      public override void Init(LevelManager p_manager)
      {
         base.Init(p_manager);

         IsWaitingSelection = false;
      }
   }
}

