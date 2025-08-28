using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace FFF.Characters
{
  public class CatBehaviour : MonoBehaviour
  {
    #region Data

    private CatData m_cat;

    public CatData Data
    {
      get => m_cat;
    }

    public void Init(int p_staminaValue)
    {
      m_cat = new CatData(p_staminaValue);
    }

    #endregion

    #region Movements and Animations

    private CatAnimationController m_animationController;

    private CatMovementBehaviour m_movementBehaviour;

    #region Walk

    public GameObject m_furnitures;
    public void WalkToFurnitures()
    {
      m_animationController.ToggleWalk(true);
      m_movementBehaviour.WalkTo(new Vector2(m_furnitures.transform.position.x - 1.5f, transform.position.y), OnFurnituresReached);
    }

    public void OnFurnituresReached()
    {
      m_animationController.ToggleWalk(false);
      ClimbToNext();
    }

    #endregion

    #region Climb

    private int m_currentFurnitureIndex;
    private int m_fallingIndex;
    public int FallingIndex
    {
      get => m_fallingIndex;
      set
      {
        m_fallingIndex = value;
        m_currentFurnitureIndex = 0;
      }
    }

    private void ClimbToNext()
    {
      m_currentFurnitureIndex++;
      if (m_currentFurnitureIndex < m_furnitures.transform.childCount)
      {
        m_animationController.ToggleClimb(true);
        m_movementBehaviour.ClimbTo(new Vector2(transform.position.x, m_furnitures.transform.GetChild(m_currentFurnitureIndex).transform.position.y), OnFurnitureClimbed);
      }
      else
      {
        Win();
      }
    }

    public void OnFurnitureClimbed()
    {
      m_animationController.ToggleClimb(false);

      if (m_fallingIndex < 0 || m_currentFurnitureIndex < m_fallingIndex)
      {
        ClimbToNext();
      }
      else
      {
        TriggerFallingAnimation();
      }
    }

    #endregion

    #region Fall

    // Called in animation CatFall
    public void StartFalling()
    {
      m_movementBehaviour.FallToGround(null);
    }

    private void TriggerFallingAnimation()
    {
      m_animationController.TriggerFall();
    }

    #endregion

    #region Win

    public GameObject m_food;

    public void Win()
    {
      m_animationController.TriggerWin();
    }

    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
      m_animationController = GetComponent<CatAnimationController>();
      m_movementBehaviour = GetComponent<CatMovementBehaviour>();
    }
  }
}
