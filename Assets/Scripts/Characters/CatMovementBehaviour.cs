using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovementBehaviour : MonoBehaviour
{
  public enum Movement { STATIC, WALK, CLIMB, FALL };

  private Movement m_movement = Movement.STATIC;
  private Vector2 m_targetPosition;
  private System.Action m_callback;

  public Vector2 TargetPosition
  {
    set => m_targetPosition = value;
  }
  private bool MoveTowardsTarget(float p_fSpeed)
  {
    return MoveTowardsTarget(p_fSpeed, true);
  }

  private bool MoveTowardsTarget(float p_fSpeed, bool p_bCheckDistance)
  {
    // Move our position a step closer to the target.
    var step = p_fSpeed * Time.deltaTime; // calculate distance to move
    transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, step);

    if (p_bCheckDistance)
    {
      // Check if the position of the cat and target are approximately equal.
      return Vector2.Distance(transform.position, m_targetPosition) < 0.001f;
    }

    return false;
  }

  private void InitMovement(Vector2 p_targetPosition, Movement p_movement, System.Action p_callback)
  {
    m_targetPosition = p_targetPosition;
    m_callback = p_callback;
    m_movement = p_movement;
  }

  private void EndMovement()
  {
    m_movement = Movement.STATIC;

    if (m_callback != null)
    {
      m_callback.Invoke();
    }
  }

  #region Walk

  public float m_fWalkingSpeed = 3.0f;

  public void WalkTo(Vector2 p_targetPosition, System.Action p_callback)
  {
    InitMovement(p_targetPosition, Movement.WALK, p_callback);
  }

  private void Walk()
  {
    bool l_targetReached = MoveTowardsTarget(m_fWalkingSpeed);

    if (l_targetReached)
    {
      EndMovement();
    }
  }

  #endregion

  #region Climb

  public float m_fClimbingSpeed = 1.5f;

  public void ClimbTo(Vector2 p_targetPosition, System.Action p_callback)
  {
    InitMovement(p_targetPosition, Movement.CLIMB, p_callback);
  }

  public void Climb()
  {
    bool l_targetReached = MoveTowardsTarget(m_fClimbingSpeed);

    if (l_targetReached)
    {
      EndMovement();
    }
  }

  #endregion

  #region Fall

  // Arbitrary value defined as 'aproximately the ground'
  private readonly float m_fGroundY = -5f;

  public float m_fCurrentFallingSpeed = 5f;
  public float m_fMaxFallingSpeed = 20f;
  public float m_fFallingAcceleration = 5f;

  public void FallToGround(System.Action p_callback)
  {
    InitMovement(new Vector2(transform.position.x, m_fGroundY), Movement.FALL, p_callback);
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

    MoveTowardsTarget(m_fCurrentFallingSpeed, false);

    // Check if the cat y position is under the ground
    if (transform.position.y < m_fGroundY)
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
      case Movement.WALK:
        Walk();
        break;
      case Movement.CLIMB:
        Climb();
        break;
      case Movement.FALL:
        Fall();
        break;
    }
  }
}
