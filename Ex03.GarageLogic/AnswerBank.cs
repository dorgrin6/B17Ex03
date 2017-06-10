using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class AnswerBank
    {
        private Type m_AnswerType;
        private readonly List<string> r_UserAnswers = new List<string>();

        public AnswerBank(Type i_AnswerType)
        {
            m_AnswerType = i_AnswerType;
        }

        public Type AnswerType
        {
            get
            {
                return m_AnswerType;
            }
            set
            {
                m_AnswerType = value;
            }
        }
    }
}
