using FFF.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FFF.Managers
{
   public class LevelManager : MonoBehaviour
   {
      [SerializeField] private GameObject m_furniturePrefab;

      [SerializeField] private VerticalLayoutGroup m_furnitureLayoutGroup;

      void Start()
      {
         for (int l_i = 0; l_i < 3; l_i++)
         {
            GameObject l_goFurniture = Instantiate(m_furniturePrefab, m_furnitureLayoutGroup.transform);
            l_goFurniture.GetComponent<FurnitureDropSlotBehaviour>().Init();
         }
      }

      void Update()
      {

      }
   }
}

