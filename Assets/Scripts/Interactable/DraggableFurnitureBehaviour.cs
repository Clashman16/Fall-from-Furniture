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

      public void Init(ScriptableFurnitureData p_data, LevelManager p_manager)
      {
         m_data = p_data;

         GetComponent<Image>().sprite = m_data.Appearance;

         m_stabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();

         Stability = m_data.Stability;

         base.Init(p_manager);

         IsWaitingSelection = true;
      }
   }
}
