using System;

namespace SurgeryHelper.Entities
{
    public class LastServiceComboBoxItem
    {
        public string HiddenValue { get; }

        private string _displayValue;
        
        public LastServiceComboBoxItem(string hiddenValue)
        {
            HiddenValue = hiddenValue;

            var data = hiddenValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            _displayValue = data.Length > 0 ? data[0] : hiddenValue;
        }

        public override string ToString()
        {
            return _displayValue;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Name == "DBNull")
                return false;

            var item = (LastServiceComboBoxItem)obj;

            return string.Equals(HiddenValue, item.HiddenValue);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
