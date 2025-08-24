using UnityEngine;

namespace FFF.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
   public class ScriptableLevelData : ScriptableObject
   {
      [SerializeField] private ScriptableFurnitureData[] m_lstFurnitures;

      public ScriptableFurnitureData[] FurnitureList
      {
         get => m_lstFurnitures;
      }
   }
}
