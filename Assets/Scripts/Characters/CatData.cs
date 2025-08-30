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
         set
         {
           staminaValue = value;
           staminaMaxValue = value;
         }
      }

      public CatData(int p_staminaValue)
      {
         StaminaMax = p_staminaValue;
      }
   }
}
