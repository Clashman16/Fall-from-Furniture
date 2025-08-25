using FFF.Managers;
using FFF.ScriptableObjects;
using FFF.Behaviours.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FFF.Behaviours.Interactable
{
   public class DraggableFurnitureBehaviour : DashedLineBorderBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
   {
      #region Data

      private ScriptableFurnitureData m_data;

      public ScriptableFurnitureData Data
      {
         get => m_data;
      }

      private int m_stability;

      public int Stability
      {
         get => m_stability;
         set
         {
            m_stability = value;

            m_stabilityDisplay.text = m_stability.ToString();
         }
      }

      private TextMeshProUGUI m_stabilityDisplay;

      #endregion

      #region Drag n drop

      private Canvas m_canvas;

      private RectTransform m_trf;

      private Vector3 m_vecFirstPosition;

      private CanvasGroup m_canvasGroup;

      #endregion

      public void Init(ScriptableFurnitureData p_data, LevelManager p_manager)
      {
         m_data = p_data;

         GetComponent<Image>().sprite = m_data.Appearance;

         m_stabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();

         Stability = m_data.Stability;

         base.Init(p_manager);

         IsWaitingSelection = true;

         m_canvas = GetComponentInParent<Canvas>();

         m_canvasGroup = GetComponent<CanvasGroup>();

         m_trf = GetComponent<RectTransform>();

         m_vecFirstPosition = transform.position;
      }

      public void OnBeginDrag(PointerEventData p_eventData)
      {
         if(IsWaitingSelection)
         {
            LevelManager.LastSelectedInteractable = this;

            m_canvasGroup.blocksRaycasts = false;
         }
      }

      public void OnDrag(PointerEventData p_eventData)
      {
         m_trf.anchoredPosition += p_eventData.delta / m_canvas.scaleFactor;
      }

      public void OnEndDrag(PointerEventData p_eventData)
      {
         m_trf.anchoredPosition = Vector2.zero;

         m_canvasGroup.blocksRaycasts = true;
      }
   }
}
