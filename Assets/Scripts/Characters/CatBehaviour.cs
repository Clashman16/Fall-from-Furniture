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

    public RectTransform m_furnitures;

    public GameObject m_ground;

    #region Walk

    public void WalkToFurnitures()
    {
      m_animationController.ToggleWalk(true);
      m_movementBehaviour.WalkTo(new Vector2(m_furnitures.transform.position.x - RectTransformUtils.GetAbsoluteSize(m_furnitures).x * 0.75f, transform.position.y), OnFurnituresReached);
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
        ClimbToFood();
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
      m_movementBehaviour.FallTo(new Vector2(transform.position.x, m_ground.transform.position.y), null);
    }

    private void TriggerFallingAnimation()
    {
      m_animationController.TriggerFall();
    }

    #endregion

    #region Win

    public GameObject m_food;

    private void ClimbToFood()
    {
      m_animationController.ToggleClimb(true);
      m_movementBehaviour.ClimbTo(new Vector2(transform.position.x, m_food.transform.position.y), OnFoodHeightReached);
    }

    private void OnFoodHeightReached()
    {
      m_animationController.ToggleClimb(false);
      WalkToFood();
    }

    private void WalkToFood()
    {
      m_animationController.ToggleWalk(true);
      m_movementBehaviour.WalkTo(new Vector2(m_food.transform.position.x, transform.position.y), OnFoodReached);
    }

    private void OnFoodReached()
    {
      m_animationController.ToggleWalk(false);
      Win();
    }

    private void Win()
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
