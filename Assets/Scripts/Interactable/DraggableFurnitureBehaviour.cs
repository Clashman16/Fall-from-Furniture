using FFF.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FFF.Interactable
{
   public class DraggableFurnitureBehaviour : MonoBehaviour
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

      public void Init(ScriptableFurnitureData p_data)
      {
         m_data = p_data;

         GetComponent<Image>().sprite = m_data.Appearance;

         m_stabilityDisplay = GetComponentInChildren<TextMeshProUGUI>();

         CurrentStability = m_data.Stability;
      }
   }
}
