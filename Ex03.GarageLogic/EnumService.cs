namespace Ex03.GarageLogic
{
    using System;
    using System.Text;

    public static class EnumService
    {
        public static string GetAllItems<T>(string i_Title)
        {
            StringBuilder builder = new StringBuilder(string.Format("{0} ", i_Title));
            int index = 1;
            foreach (string item in Enum.GetNames(typeof(T)))
            {
                builder.AppendFormat("{0}) {1} ", index, item);
                ++index;
            }

            return builder.ToString();
        }

        // taken from: https://stackoverflow.com/questions/13615/validate-enum-values
        public static bool TryParseEnum<TEnum>(int i_EnumValue, out TEnum i_RetVal)
        {
            i_RetVal = default(TEnum);
            bool success = Enum.IsDefined(typeof(TEnum), i_EnumValue);
            if (success)
            {
                i_RetVal = (TEnum)Enum.ToObject(typeof(TEnum), i_EnumValue);
            }
            return success;
        }

        
    }
}
