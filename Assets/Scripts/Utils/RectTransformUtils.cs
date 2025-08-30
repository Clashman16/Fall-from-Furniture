using UnityEngine;

namespace FFF.Utils
{
  public static class RectTransformUtils
  {
    public static Vector2 GetAbsoluteSize(RectTransform rectTransform)
    {
      Vector3[] corners = new Vector3[4];
      rectTransform.GetWorldCorners(corners);

      var width = Mathf.Abs(corners[2].x - corners[0].x);
      var height = Mathf.Abs(corners[2].y - corners[0].y);

      return new Vector2(width, height);
    }
  }
}
