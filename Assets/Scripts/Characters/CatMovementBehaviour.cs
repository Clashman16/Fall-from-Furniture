using FFF.Utils;
using UnityEngine;

namespace FFF.Characters
{
  public class CatMovementBehaviour : MonoBehaviour
  {
    private EMovement m_movement = EMovement.STATIC;
    private Vector2 m_targetPosition;
    private System.Action m_callback;

    public Vector2 TargetPosition
    {
      set => m_targetPosition = value;
    }
    private bool MoveTowardsTarget(float p_fSpeed)
    {
      // Move our position a step closer to the target.
      var step = p_fSpeed * Time.deltaTime; // calculate distance to move
      transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, step);

      // Check if the position of the cat and target are approximately equal.
      return Vector2.Distance(transform.position, m_targetPosition) < 0.001f;
    }

    private void InitMovement(Vector2 p_targetPosition, EMovement p_movement, System.Action p_callback)
    {
      m_targetPosition = p_targetPosition;
      m_callback = p_callback;
      m_movement = p_movement;
    }

    private void EndMovement()
    {
      m_movement = EMovement.STATIC;

      if (m_callback != null)
      {
        m_callback.Invoke();
      }
    }

    #region Walk

    public float m_fWalkingSpeed = 3.0f;

    public void WalkTo(Vector2 p_targetPosition, System.Action p_callback)
    {
      InitMovement(p_targetPosition, EMovement.WALK, p_callback);
    }

  private void Walk()
  {
    bool l_bTargetReached = MoveTowardsTarget(m_fWalkingSpeed);

    if (l_bTargetReached)
    {
      EndMovement();
    }
  }

    #endregion

    #region Climb

    public float m_fClimbingSpeed = 1.5f;

    public void ClimbTo(Vector2 p_targetPosition, System.Action p_callback)
    {
      InitMovement(p_targetPosition, EMovement.CLIMB, p_callback);
    }

  public void Climb()
  {
    bool l_bTargetReached = MoveTowardsTarget(m_fClimbingSpeed);

    if (l_bTargetReached)
    {
      EndMovement();
    }
  }

    #endregion

    #region Fall

    public float m_fCurrentFallingSpeed = 5f;
    public float m_fMaxFallingSpeed = 20f;
    public float m_fFallingAcceleration = 5f;

    public void FallTo(Vector2 p_targetPosition, System.Action p_callback)
    {
      InitMovement(p_targetPosition, EMovement.FALL, p_callback);
    }

    private void Fall()
    {
      if (m_fCurrentFallingSpeed < m_fMaxFallingSpeed)
      {
        m_fCurrentFallingSpeed += Mathf.Min(m_fFallingAcceleration * Time.deltaTime, 1);    // limit to 1 for "full speed"
      }
      else
      {
        m_fCurrentFallingSpeed = m_fMaxFallingSpeed;
      }

    bool l_bTargetReached = MoveTowardsTarget(m_fCurrentFallingSpeed);

    if (l_bTargetReached)
    {
      EndMovement();
    }
  }

    #endregion

    // Update is called once per frame
    void Update()
    {
      switch (m_movement)
      {
        case EMovement.WALK:
          Walk();
          break;
        case EMovement.CLIMB:
          Climb();
          break;
        case EMovement.FALL:
          Fall();
          break;
      }
    }
  }
}