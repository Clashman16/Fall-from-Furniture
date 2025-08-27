using FFF.Characters;
using FFF.Behaviours.Interactable;
using FFF.Player;
using FFF.ScriptableObjects;
using FFF.Behaviours.UI;
using FFF.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FFF.Managers
{
   public class LevelManager : MonoBehaviour
   {
      #region Level creation

      [SerializeField] private GameObject m_goFurnitureSlotPrefab;

      [SerializeField] private VerticalLayoutGroup m_furnitureSlotLayoutGroup;

      [SerializeField] private GameObject m_goDraggableFurniturePrefab;

      [SerializeField] private HorizontalLayoutGroup m_draggableFurnitureLayoutGroup;

      #endregion

      #region Checking result

      private int m_dFurnitureCount;

      public bool IsCheckingResult => m_dFurnitureCount == m_lstStackedFurniture.Count;

      #endregion

      #region Drag n drop management

      private DashedLineBorderBehaviour m_lastSelectedInteractable;

      public DashedLineBorderBehaviour LastSelectedInteractable
      {
         get => m_lastSelectedInteractable;
         set
         {
            if(value.GetType() == typeof(FurnitureDropSlotBehaviour))
            {
               value.GetComponent<Image>().sprite = ((DraggableFurnitureBehaviour)m_lastSelectedInteractable).Data.Appearance;

               m_lastSelectedInteractable.gameObject.SetActive(false);

               // TODO (A1) : Get previous furniture (Move the furniture object instead of only changing a sprite ?)

               FurnitureDropSlotBehaviour l_dropSlot = (FurnitureDropSlotBehaviour)value;

               l_dropSlot.CurrentStability = ((DraggableFurnitureBehaviour)m_lastSelectedInteractable).Stability;

               m_lstStackedFurniture.Add(l_dropSlot);

               l_dropSlot.CurrentProbability = FurnitureUtils.GetProbabilityOfFalling(m_cat, 
                 m_lstStackedFurniture.GetRange(
                   Mathf.Max(0, m_lstStackedFurniture.Count-2),
                   Mathf.Min(2, m_lstStackedFurniture.Count)
                   ));

               // Decrease stamina
               m_cat.Stamina--;

               foreach (Transform l_draggableTrf in m_draggableFurnitureLayoutGroup.transform)
               {
                  l_draggableTrf.GetComponent<DraggableFurnitureBehaviour>().IsWaitingSelection = true;
               }

               foreach(Transform l_dropSlotTrf in m_furnitureSlotLayoutGroup.transform)
               {
                  l_dropSlotTrf.GetComponent<FurnitureDropSlotBehaviour>().IsWaitingSelection = false;
               }

               m_dCurrentRefillableSlotId += 1;

               if(IsCheckingResult)
               {
                  FurnitureUtils.TryClimbing(m_cat, m_lstStackedFurniture);
               }
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

      private List<FurnitureDropSlotBehaviour> m_lstStackedFurniture;

      #endregion

      CatData m_cat;

      private void Start()
      {
         ScriptableFurnitureData[] l_lstFurniture = PlayerSaveSingleton.Instance.CurentLevelData;

         m_dFurnitureCount = l_lstFurniture.Length;

         for (int l_i = 0; l_i < m_dFurnitureCount; l_i++)
         {
            GameObject l_goSlot = Instantiate(m_goFurnitureSlotPrefab, m_furnitureSlotLayoutGroup.transform);
            l_goSlot.GetComponent<FurnitureDropSlotBehaviour>().Init(this);


            GameObject l_goDraggable = Instantiate(m_goDraggableFurniturePrefab, m_draggableFurnitureLayoutGroup.transform);
            l_goDraggable.GetComponent<DraggableFurnitureBehaviour>().Init(l_lstFurniture[l_i], this);
         }

         m_cat = new CatData(l_lstFurniture.Length);

         m_lstStackedFurniture = new List<FurnitureDropSlotBehaviour>();
      }
   }
}

