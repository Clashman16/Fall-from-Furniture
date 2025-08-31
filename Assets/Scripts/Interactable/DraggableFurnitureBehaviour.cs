using FFF.Managers;
using FFF.ScriptableObjects;
using FFF.Behaviours.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FFF.Player;

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

      private int m_dStability;

      public int Stability
      {
         get => m_dStability;
         set
         {
            m_dStability = value;

            m_stabilityDisplay.text = m_dStability.ToString();
         }
      }

      private TextMeshProUGUI m_stabilityDisplay;

      #endregion

      #region Drag n drop

      private Canvas m_canvas;

      private RectTransform m_trf;

      private CanvasGroup m_canvasGroup;
    
      private Vector2 m_originalPosition;

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
      }

      public void OnBeginDrag(PointerEventData p_eventData)
      {
         if(IsWaitingSelection && PlayerStateSingleton.Instance.GameScreen == Utils.EGameScreen.GAME_SCREEN)
         {
            LevelManager.LastSelectedInteractable = this;

            m_originalPosition = m_trf.anchoredPosition;

            m_canvasGroup.blocksRaycasts = false;
         } else
         {
           p_eventData.pointerDrag = null;
         }
      }

      public void OnDrag(PointerEventData p_eventData)
      {
        // Check is done on OnBeginDrag
         m_trf.anchoredPosition += p_eventData.delta / m_canvas.scaleFactor;
      }

      public void OnEndDrag(PointerEventData p_eventData)
      {
        // Check is done on OnBeginDrag
         m_trf.anchoredPosition = m_originalPosition;
         m_canvasGroup.blocksRaycasts = true;
         IsWaitingSelection = true; // To keep drag available
      }

      public void ResetData()
      {
         gameObject.SetActive(true);
         m_canvasGroup.blocksRaycasts = true;
         IsWaitingSelection = true;
      }
   }
}
