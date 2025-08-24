using FFF.Interactable;
using FFF.Player;
using FFF.ScriptableObjects;
using FFF.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FFF.Managers
{
   public class LevelManager : MonoBehaviour
   {
      #region Level creation

      [SerializeField] private GameObject m_furnitureSlotPrefab;

      [SerializeField] private VerticalLayoutGroup m_furnitureSlotLayoutGroup;

      [SerializeField] private GameObject m_draggableFurniturePrefab;

      [SerializeField] private HorizontalLayoutGroup m_draggableFurnitureLayoutGroup;

      #endregion

      private void Start()
      {
         ScriptableFurnitureData[] l_lstFurniture = PlayerSaveSingleton.Instance.CurentLevelData;

         for (int l_i = 0; l_i < l_lstFurniture.Length; l_i++)
         {
            GameObject l_goSlot = Instantiate(m_furnitureSlotPrefab, m_furnitureSlotLayoutGroup.transform);
            l_goSlot.GetComponent<FurnitureDropSlotBehaviour>().Init();


            GameObject l_goDraggable = Instantiate(m_draggableFurniturePrefab, m_draggableFurnitureLayoutGroup.transform);
            l_goDraggable.GetComponent<DraggableFurnitureBehaviour>().Init(l_lstFurniture[l_i]);
         }
      }
   }
}

