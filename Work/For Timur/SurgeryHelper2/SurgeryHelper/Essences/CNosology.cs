using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Класс с информацией о нозологии
    /// </summary>
    public class CNosology : CBaseMedical
    {
        /// <summary>
        /// Id родительской операции. -1 - если корневая
        /// </summary>
        public int IdParent;

        /// <summary>
        /// Тип нозологии (содержит ли внутри вложенные нозологии)
        /// </summary>
        public NodeType Type;

        /// <summary>
        /// Состояние узла: открыт или закрыт
        /// </summary>
        public NodeFolderStated NodeFolderStated;

        public CNosology()
        {
            IdParent = -1;
            Type = NodeType.Type;
            NodeFolderStated = NodeFolderStated.Closed;
        }

        public CNosology(CNosology nosologyInfo)
        {
            Id = nosologyInfo.Id;
            Name = nosologyInfo.Name;
            IdParent = nosologyInfo.IdParent;
            Type = nosologyInfo.Type;
            NodeFolderStated = nosologyInfo.NodeFolderStated;
        }

        public static int Compare(CNosology nosologyInfo1, CNosology nosologyInfo2)
        {
            return string.Compare(nosologyInfo1.Name, nosologyInfo2.Name);
        }
    }
}