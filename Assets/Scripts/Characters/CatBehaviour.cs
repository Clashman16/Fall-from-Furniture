using System.Collections;
using System.Collections.Generic;
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

    public void Init(int p_staminaValue)
    {
      m_cat = new CatData(p_staminaValue);
    }

    #endregion

    #region Animation

    private CatAnimationController m_catAnimationController;

    #region Walk
    
    private bool m_bIsWalking;
    public bool IsWalking
    {
      get => m_bIsWalking;
      set
      {
        m_bIsWalking = value;
        m_catAnimationController.ToggleWalk(value);
      }
    }

    public float m_speed = 3.0f;

    public GameObject m_target;

    private Vector2 m_targetPosition;
    private Vector2 TargetPosition
    {
      get
      {
        if(m_targetPosition.Equals(Vector2.zero))
        {
          m_targetPosition = new Vector2(m_target.transform.position.x - 1.5f, transform.position.y);
        }
        return m_targetPosition;
      }
    }

    private void Walk()
    {
        // Move our position a step closer to the target.
        var step =  m_speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector2.MoveTowards(transform.position, TargetPosition, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector2.Distance(transform.position,TargetPosition) < 0.001f)
        {
            IsWalking = false;
            ClimbToNext();
        }
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

    private bool m_bIsClimbing;
    public bool IsClimbing
    {
      get => m_bIsClimbing;
      set
      {
        m_bIsClimbing = value;
        m_catAnimationController.ToggleClimb(value);
      }
    }

    public float m_fSpeedClimbing = 1.5f;

    private void ClimbToNext()
    {
      m_currentFurnitureIndex++;
      if(m_currentFurnitureIndex < m_target.transform.childCount)
      {
        m_targetPosition = new Vector2(transform.position.x, m_target.transform.GetChild(m_currentFurnitureIndex).transform.position.y);
        IsClimbing = true;
      } else
      {
        Win();
      }
    }

    public void Climb()
    {
      // Move our position a step closer to the target.
      var step =  m_fSpeedClimbing * Time.deltaTime; // calculate distance to move
      transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, step);

      // Check if the position of the cube and sphere are approximately equal.
      if (Vector2.Distance(transform.position,m_targetPosition) < 0.001f)
      {
          IsClimbing = false;

          if(m_fallingIndex < 0 || m_currentFurnitureIndex < m_fallingIndex)
          {
            ClimbToNext();
          }
          else
          {
            TriggerFallingAnimation();
          }
      }
    }

    #endregion

    #region Fall

    private bool m_bIsFalling = false;
    
    // Called in animation CatFall
    public void StartFalling()
    {
      m_bIsFalling = true;
    }

    private void TriggerFallingAnimation()
    {
      m_catAnimationController.TriggerFall();
    }

    public float m_fCurrentFallingSpeed = 5f;
    public float m_fMaxSpeed = 20f;
    public float m_fAcceleration = 5f;
    private Vector2 m_vGroundPosition;
    private Vector2 GroundPosition
    {
      get
      {
        if(m_vGroundPosition.Equals(Vector2.zero))
        {
          m_vGroundPosition = new Vector2(transform.position.x, -5);
        }
        return m_vGroundPosition;
      }
      set => m_vGroundPosition = value;
    }

    private void Fall()
    {
      if (m_fCurrentFallingSpeed < m_fMaxSpeed)
      {
          m_fCurrentFallingSpeed += Mathf.Min(m_fAcceleration * Time.deltaTime, 1);    // limit to 1 for "full speed"
      }
      else
      {
            m_fCurrentFallingSpeed = m_fMaxSpeed;
      }

      // Move our position a step closer to the target.
      var step =  m_fCurrentFallingSpeed * Time.deltaTime; // calculate distance to move
      transform.position = Vector2.MoveTowards(transform.position, GroundPosition, step);

      // Check if the position of the cube and sphere are approximately equal.
      if (transform.position.y < -5)
      {
        m_bIsFalling = false;
      }
    }

    #endregion

    #region Win

    public GameObject m_food;

    public void Win()
    {
      m_catAnimationController.TriggerWin();
    }

    #endregion

    #endregion

    private bool MoveTowardsTarget(float p_fSpeed)
    {
      // Move our position a step closer to the target.
      var step =  p_fSpeed * Time.deltaTime; // calculate distance to move
      transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, step);

      return Vector2.Distance(transform.position,m_targetPosition) < 0.001f;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_catAnimationController = GetComponent<CatAnimationController>();
    }

    void Update()
    {
      if(m_bIsWalking)
      {
        Walk();
      }

      if(m_bIsClimbing)
      {
        Climb();
      }

      if(m_bIsFalling)
      {
        Fall();
      }
    }
  }
}
