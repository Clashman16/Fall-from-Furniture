using FFF.Behaviours.Interactable;
using FFF.Managers;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FFF.Behaviours.UI
{
   public class FurnitureDropSlotBehaviour : DashedLineBorderBehaviour, IDropHandler
   {
      static private Gradient s_gradient;

      private float m_currentProbability;

      public float CurrentProbability
      {
         get => m_currentProbability;
         set
         {
            m_currentProbability = value;
        
            m_probabilityDisplay.GetComponentInParent<Image>().color = s_gradient.Evaluate(value);
            m_probabilityDisplay.text = m_currentProbability.ToString("P2", CultureInfo.InvariantCulture);
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

      private bool IsSlotOccupied => FurnitureDisplay.sprite != null;

      public override void Init(LevelManager p_manager)
      {
         base.Init(p_manager);

         IsWaitingSelection = false;

         m_probabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();

        if(s_gradient == null)
        {
            s_gradient = new Gradient();

            var l_lstColors = new GradientColorKey[3];
            l_lstColors[0] = new GradientColorKey(Color.green, 0.0f);
            l_lstColors[1] = new GradientColorKey(Color.yellow, 0.5f);
            l_lstColors[2] = new GradientColorKey(Color.red, 1.0f);

            var l_lstAlphas = new GradientAlphaKey[1];
            l_lstAlphas[0] = new GradientAlphaKey(0.5f, 0.0f);

            s_gradient.SetKeys(l_lstColors, l_lstAlphas);
        }
      }

      public void OnDrop(PointerEventData p_eventData)
      {
         DraggableFurnitureBehaviour l_draggable = p_eventData.pointerDrag.GetComponent<DraggableFurnitureBehaviour>();
         if (l_draggable != null && LevelManager.LastSelectedInteractable == l_draggable
            && IsWaitingSelection && !IsSlotOccupied)
         {
            LevelManager.LastSelectedInteractable = this;
         }
      }
   }
}