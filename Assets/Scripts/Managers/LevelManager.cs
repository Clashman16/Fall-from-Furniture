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

      #region Drag n drop management

      private DashedLineBorderBehaviour m_lastSelectedInteractable;

      public DashedLineBorderBehaviour LastSelectedInteractable
      {
         set
         {
            if(value.GetType() == typeof(FurnitureDropSlotBehaviour))
            {
               value.GetComponent<Image>().sprite = ((DraggableFurnitureBehaviour)m_lastSelectedInteractable).Data.Appearance;

               m_lastSelectedInteractable.gameObject.SetActive(false);

               foreach (Transform l_draggableTrf in m_draggableFurnitureLayoutGroup.transform)
               {
                  l_draggableTrf.GetComponent<DraggableFurnitureBehaviour>().IsWaitingSelection = true;
               }

               foreach(Transform l_dropSlotTrf in m_furnitureSlotLayoutGroup.transform)
               {
                  l_dropSlotTrf.GetComponent<FurnitureDropSlotBehaviour>().IsWaitingSelection = false;
               }

               m_dCurrentRefillableSlotId += 1;
            }
            else
            {
               DashedLineBorderBehaviour[] l_lstInteractable = GetComponentsInChildren<DashedLineBorderBehaviour>();
               foreach (DashedLineBorderBehaviour l_interactable in l_lstInteractable)
               {
                  l_interactable.IsWaitingSelection = false;
               }

               m_furnitureSlotLayoutGroup.transform.GetChild(m_dCurrentRefillableSlotId).GetComponent<FurnitureDropSlotBehaviour>().IsWaitingSelection = true;
            }

            m_lastSelectedInteractable = value;
         }
      }

      private int m_dCurrentRefillableSlotId = 0;

      #endregion



      private void Start()
      {
         ScriptableFurnitureData[] l_lstFurniture = PlayerSaveSingleton.Instance.CurentLevelData;

         for (int l_i = 0; l_i < l_lstFurniture.Length; l_i++)
         {
            GameObject l_goSlot = Instantiate(m_furnitureSlotPrefab, m_furnitureSlotLayoutGroup.transform);
            l_goSlot.GetComponent<FurnitureDropSlotBehaviour>().Init(this);


            GameObject l_goDraggable = Instantiate(m_draggableFurniturePrefab, m_draggableFurnitureLayoutGroup.transform);
            l_goDraggable.GetComponent<DraggableFurnitureBehaviour>().Init(l_lstFurniture[l_i], this);
         }
      }
   }
}

