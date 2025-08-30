using FFF.Characters;
using FFF.Behaviours.Interactable;
using FFF.Player;
using FFF.ScriptableObjects;
using FFF.Behaviours.UI;
using FFF.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FFF.Managers
{
   public class LevelManager : MonoBehaviour
   {
      #region Level creation

      [SerializeField] private GameObject m_goFurnitureSlotPrefab;

      [SerializeField] private VerticalLayoutGroup m_furnitureSlotLayoutGroup;

      [SerializeField] private GameObject m_goDraggableFurniturePrefab;

      [SerializeField] private HorizontalLayoutGroup m_draggableFurnitureLayoutGroup;

      private int m_levelId;

      #endregion

      #region Checking result

      private int m_dFurnitureCount = 0;

      public bool IsCheckingResult => m_lstStackedFurniture != null && m_dFurnitureCount == m_lstStackedFurniture.Count;

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
               m_cat.Data.Stamina--;

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
                  m_cat.IsWalking = true;

                  m_cat.FallingIndex = FurnitureUtils.TryClimbing(m_cat, m_lstStackedFurniture);  
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

      public CatController m_cat;

      public void LaunchLevel()
      {
         if(PlayerSaveSingleton.Instance.CurrentLevelId == m_levelId)
         {
            if(m_dFurnitureCount == 0)
            {
               InitLevel();
            }
            else
            {
               ResetLevel();
            }
         }
         else
         {
            ClearLevel();

            InitLevel();

            m_levelId = PlayerSaveSingleton.Instance.CurrentLevelId;
         }        
      }

      private void InitLevel()
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

         m_cat.Init(l_lstFurniture.Length);

         m_lstStackedFurniture = new List<FurnitureDropSlotBehaviour>();
      }

      private void ResetLevel()
      {
         for (int l_i = 0; l_i < m_dFurnitureCount; l_i++)
         {
            Transform l_trfSlotLayoutGroup = m_furnitureSlotLayoutGroup.transform;
            if (l_i < l_trfSlotLayoutGroup.childCount)
            {
               Destroy(l_trfSlotLayoutGroup.GetChild(l_i).gameObject);
            }

            m_draggableFurnitureLayoutGroup.transform.GetChild(l_i).GetComponent<DraggableFurnitureBehaviour>().Reset();
         }

         m_cat.Init(m_dFurnitureCount);
      }

      private void ClearLevel()
      {
         for(int l_i = 0; l_i < m_dFurnitureCount; l_i++)
         {
            Transform l_trfSlotLayoutGroup = m_furnitureSlotLayoutGroup.transform;
            if (l_i < l_trfSlotLayoutGroup.childCount)
            {
               Destroy(l_trfSlotLayoutGroup.GetChild(l_i).gameObject);
            }
            
            Destroy(m_draggableFurnitureLayoutGroup.transform.GetChild(l_i).gameObject);
         }

         m_lstStackedFurniture.Clear();
      }
   }
}

