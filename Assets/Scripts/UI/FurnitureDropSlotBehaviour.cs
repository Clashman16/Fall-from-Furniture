using FFF.Managers;
using TMPro;

namespace FFF.UI
{
   public class FurnitureDropSlotBehaviour : DashedLineBorderBehaviour
   {
      private float m_currentProbability;

      public float CurrentProbability
      {
         get => m_currentProbability;
         set
         {
            m_currentProbability = value;

            m_probabilityDisplay.text = m_currentProbability.ToString();
         }
      }

      private int m_currentStability;

      public int CurrentStability
      {
         get => m_currentStability;
         set
         {
            m_currentStability = value;
         }
      }

      private TextMeshProUGUI m_probabilityDisplay;

      public override void Init(LevelManager p_manager)
      {
         base.Init(p_manager);

         IsWaitingSelection = false;

         m_probabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();
      }
   }
}

