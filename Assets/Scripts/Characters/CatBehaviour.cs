using FFF.Utils;
using UnityEngine;

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

    #endregion

    #region Movements and Animations

    private CatAnimationController m_animationController;

    private CatMovementBehaviour m_movementBehaviour;

    private System.Action m_onFallCallback;
    private System.Action m_onWinCallback;

    public RectTransform m_furnitures;

    public GameObject m_ground;

    public void TryToGetFood(System.Action p_onFallCallback, System.Action p_onWinCallback)
    {
      m_onFallCallback = p_onFallCallback;
      m_onWinCallback = p_onWinCallback;

      WalkToFurnitures();
    }

    #region Walk

    private void WalkToFurnitures()
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
      m_movementBehaviour.FallTo(new Vector2(transform.position.x, m_ground.transform.position.y), m_onFallCallback);
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

      if (m_onWinCallback != null)
      {
        m_onWinCallback.Invoke();
      }
    }

    #endregion

    #endregion

    private Vector2 m_originalPosition;

    // Start is called before the first frame update
    void Start()
    {
      m_originalPosition = transform.position;
      m_animationController = GetComponent<CatAnimationController>();
      m_movementBehaviour = GetComponent<CatMovementBehaviour>();
    }

    public void Init(int p_staminaValue)
    {
      if(m_cat == null)
      {
        m_cat = new CatData(p_staminaValue);
      } else
      {
        m_cat.StaminaMax = p_staminaValue;
      }
      transform.position = m_originalPosition;
    }

    public void ResetData()
    {
      ResetData(m_cat.StaminaMax);
    }

    public void ResetData(int p_staminaValue)
    {
      Init(p_staminaValue);
      m_animationController.TriggerReset();
    }
  }
}
