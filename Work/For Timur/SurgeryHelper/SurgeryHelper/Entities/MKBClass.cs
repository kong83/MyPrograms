namespace SurgeryHelper.Entities
{
    public class MkbClass
    {
        /// <summary>
        /// Код МКБ
        /// </summary>
        public string MkbCode { get; set; }

        /// <summary>
        /// Название МКБ
        /// </summary>
        public string MkbName { get; set; }

        public MkbClass()
        {
            MkbCode = MkbName = string.Empty;
        }
    }
}
