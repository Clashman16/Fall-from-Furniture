using FFF.Behaviours.Interactable;
using FFF.Managers;
using TMPro;
using UnityEngine.EventSystems;

namespace FFF.Behaviours.UI
{
   public class FurnitureDropSlotBehaviour : DashedLineBorderBehaviour, IDropHandler
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

      private int m_dCurrentStability;

      public int CurrentStability
      {
         get => m_dCurrentStability;
         set
         {
            m_dCurrentStability = value;
         }
      }

      private TextMeshProUGUI m_probabilityDisplay;

      public override void Init(LevelManager p_manager)
      {
         base.Init(p_manager);

         IsWaitingSelection = false;

         m_probabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();
      }

      public void OnDrop(PointerEventData p_eventData)
      {
         DraggableFurnitureBehaviour l_draggable = p_eventData.pointerDrag.GetComponent<DraggableFurnitureBehaviour>();
         if (l_draggable != null && LevelManager.LastSelectedInteractable == l_draggable)
         {
            LevelManager.LastSelectedInteractable = this;
         }
      }
   }
}