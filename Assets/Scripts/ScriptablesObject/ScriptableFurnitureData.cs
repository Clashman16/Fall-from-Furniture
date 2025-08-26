using UnityEngine;

namespace FFF.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/FurnitureData")]
   public class ScriptableFurnitureData : ScriptableObject
   {
      [SerializeField, Min(2)] private int m_dStability;

      public int Stability
      {
         get => m_dStability;
      }

      [SerializeField] private Sprite m_imgAppearance;

      public Sprite Appearance
      {
         get => m_imgAppearance;
      }
   }
}
