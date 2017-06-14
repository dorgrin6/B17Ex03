using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Owner
    {
        public const string k_Name = "owner's name";

        public const string k_PhoneNumber = "owner's phone number";

        private string m_Name;

        private string m_PhoneNumber;

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
            set
            {
                m_PhoneNumber = value;
            }
        }

        public void GetDetails(Dictionary<string,string> i_Details)
        {
            i_Details.Add(k_Name, m_Name);
            i_Details.Add(k_PhoneNumber, m_PhoneNumber);
        }
    }
}
