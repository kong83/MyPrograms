using System;

namespace SampleCreator
{
    public enum VariableType
    {
        Single,
        Numeric,
        Open
    }

    public class Variable
    {
        private readonly string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        private readonly VariableType m_VarType;
        private readonly int m_From;
        private readonly int m_To;
        private readonly string m_Prefix;
        private string m_CurrentValue;
        public string CurrentValue
        {
            get
            {
                return m_CurrentValue;
            }
        }

        public Variable(string name, VariableType varType, int from, int to)
        {
            m_Name = name;
            m_VarType = varType;
            m_From = from;
            m_To = to;
            m_CurrentValue = from.ToString();
        }

        public Variable(string name, VariableType varType, string prefix, int from)
        {
            m_Name = name;
            m_VarType = varType;
            m_Prefix = prefix;
            m_From = from;
            m_CurrentValue = prefix + from;
        }

        public void NextValue()
        {
            int temp;

            if (m_VarType == VariableType.Open)
            {
                temp = Convert.ToInt32(CurrentValue.Substring(m_Prefix.Length));
                temp++;
                m_CurrentValue = m_Prefix + temp;
                return;
            }

            temp = Convert.ToInt32(CurrentValue);
            temp++;
            if (temp > m_To)
                temp = m_From;

            m_CurrentValue = temp.ToString();
        }
    }
}
