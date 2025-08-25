using FFF.Managers;
using FFF.ScriptableObjects;
using FFF.UI;
using TMPro;
using UnityEngine.UI;

namespace FFF.Interactable
{
   public class DraggableFurnitureBehaviour : DashedLineBorderBehaviour
   {
      private ScriptableFurnitureData m_data;

      public ScriptableFurnitureData Data
      {
         get => m_data;
      }

      private int m_currentStability;

      public int CurrentStability
      {
         get => m_currentStability;
         set
         {
            m_currentStability = value;

            m_stabilityDisplay.text = m_currentStability.ToString();
         }
      }
    
      private TextMeshProUGUI m_stabilityDisplay;

      private float m_currentProbability;

      public float CurrentProbability
      {
         get => m_currentProbability;
         set
         {
            m_currentProbability = value;
            // TODO (A3) : Add an UI element next to the future slot
            // m_probabilityDisplay.text = m_currentProbability.ToString();
         }
      }
      
      // TODO (A3) : Add an UI element next to the future slot
      // private TextMeshProUGUI m_probabilityDisplay;

      public void Init(ScriptableFurnitureData p_data, LevelManager p_manager)
      {
         m_data = p_data;

         GetComponent<Image>().sprite = m_data.Appearance;

         m_stabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();

         CurrentStability = m_data.Stability;

         base.Init(p_manager);

         IsWaitingSelection = true;
      }
   }
}
