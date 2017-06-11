using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class NamesDictionary
    {
        public static Dictionary<string,string> m_Book = new Dictionary<string,string>();

        public static bool AddName(string i_OrgName, string i_DefName)
        {
            if (m_Book.ContainsKey(i_OrgName))
            {
                return false;
            }
            else
            {
                m_Book.Add(i_OrgName, i_DefName);
                return true;
            }
        }

        public static string GetValue(string i_Key)
        {
            string value;
            if (m_Book.TryGetValue(i_Key, out value))
            {
                return value;
            }
            //TODO: need an exception
            return string.Empty;
        }
    }
}
