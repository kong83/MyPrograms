namespace SurgeryHelper.Entities
{
    public class MKBClass
    {
        /// <summary>
        /// Код МКБ
        /// </summary>
        public string MkbName { get; private set; }

        /// <summary>
        /// Код КСГ с информацией о детях
        /// </summary>
        public string KsgName { get; private set; }

        /// <summary>
        /// Расшифровка кода КСГ
        /// </summary>
        public string KsgDecoding { get; private set; }

        /// <summary>
        /// к/д норма
        /// </summary>
        public string KDNorm { get; private set; }

        /// <summary>
        /// к/д минимум
        /// </summary>
        public string KDMin { get; private set; }

        /// <summary>
        /// к/д максимум
        /// </summary>
        public string KDMax { get; private set; }

        /// <summary>
        /// Специальность
        /// </summary>
        public string Specialiy { get; private set; }

        public MKBClass()
        {
            MkbName = KsgName = KsgDecoding = KDNorm = KDMin = KDMax = Specialiy = string.Empty;
        }

        public MKBClass(string ksgData)
        {
            string[] ksgDataArr = ksgData.Split(';');
            MkbName = ksgDataArr[0];
            KsgName = ksgDataArr[1];
            KsgDecoding = ksgDataArr[2];
            KDNorm = ksgDataArr[3];
            KDMin = ksgDataArr[4];
            KDMax = ksgDataArr[5];
            Specialiy = ksgDataArr[6];
        }

        public override string ToString()
        {
            return string.Format(
                "{0};{1};{2};{3};{4};{5};{6}",
                MkbName,
                KsgName,
                KsgDecoding,
                KDNorm,
                KDMin,
                KDMax,
                Specialiy);
        }
    }    
}
