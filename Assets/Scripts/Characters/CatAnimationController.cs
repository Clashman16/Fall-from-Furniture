using UnityEngine;

namespace FFF.Characters
{
  public class CatAnimationController : MonoBehaviour
  {
    private Animator m_animator;

    #region Toggle Boolean (Walk, Climb)

    public void ToggleWalk(bool p_isWalking)
    {
      ToggleAnimation("IsWalking", p_isWalking);
    }

    public void ToggleClimb(bool p_isClimbing)
    {
      ToggleAnimation("IsClimbing", p_isClimbing);
    }

    private void ToggleAnimation(string p_animationName, bool p_isAnimation)
    {
      if (m_animator != null)
      {
        m_animator.SetBool(p_animationName, p_isAnimation);
      }
    }

    #endregion

  #region Trigger Animation (Fall, Win, Reset)

    public void TriggerFall()
    {
      TriggerAnimation("TrFall");
    }

    public void TriggerWin()
    {
      TriggerAnimation("TrWin");
    }

  public void TriggerReset()
  {
    TriggerAnimation("TrReset");
  }

  private void TriggerAnimation(string p_animationName)
  {
    if (m_animator != null)
    {
      m_animator.SetTrigger(p_animationName);
    }
  }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
      m_animator = GetComponent<Animator>();
    }
  }
}
