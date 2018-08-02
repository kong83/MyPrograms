using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    public class COperationType : CBaseMedical
    {
        /// <summary>
        /// Id родительской операции. 0 - если корневая
        /// </summary>
        public  int IdParent;

        /// <summary>
        /// Тип операции (папка или непосредственно тип)
        /// Папки не могут быть выбраны
        /// </summary>
        public NodeType Type;

        /// <summary>
        /// Состояние узла: открыт или закрыт
        /// </summary>
        public NodeFolderStated NodeFolderStated;

        public COperationType()
        {
            IdParent = -1;
            Type = NodeType.Type;
            NodeFolderStated = NodeFolderStated.Closed;
        }

        public COperationType(COperationType operationTypeInfo)
        {
            Id = operationTypeInfo.Id;
            Name = operationTypeInfo.Name;
            IdParent = operationTypeInfo.IdParent;
            Type = operationTypeInfo.Type;
            NodeFolderStated = operationTypeInfo.NodeFolderStated;
        }

        public static int Compare(COperationType operationTypeInfo1, COperationType operationTypeInfo2)
        {
            return string.Compare(operationTypeInfo1.Name, operationTypeInfo2.Name);
        }
    }
}
