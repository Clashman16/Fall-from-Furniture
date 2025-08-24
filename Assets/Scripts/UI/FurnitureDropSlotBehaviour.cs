using UnityEngine;
using UnityEngine.UI;

namespace FFF.UI
{
   public class FurnitureDropSlotBehaviour : DashedLineBorderBehaviour
   {
      private LineRenderer m_border;

      private Image m_imgFurnitureDisplay;

      public void Init()
      {
         DrawBorder();
      }
   }
}


