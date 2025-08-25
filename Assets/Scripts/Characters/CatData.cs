namespace FFF.Characters
{
   public class CatData
   {
      private int staminaValue;

      public int Stamina
      {
         get => staminaValue;
         set => staminaValue = value;
      }

      private int staminaMaxValue;

      public int StaminaMax
      {
         get => staminaMaxValue;
         set => staminaMaxValue = value;
      }

      public CatData(int p_staminaValue)
      {
         staminaValue = p_staminaValue;
         staminaMaxValue = p_staminaValue;
      }
   }
}
