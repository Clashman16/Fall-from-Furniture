using UnityEngine;

namespace FFF.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/LevelListData")]
   public class ScriptableLevelListData : ScriptableObject
   {
      [SerializeField] private ScriptableLevelData[] m_lstLevels;

      public ScriptableLevelData[] LevelList
      {
         get => m_lstLevels;
      }
   }
}