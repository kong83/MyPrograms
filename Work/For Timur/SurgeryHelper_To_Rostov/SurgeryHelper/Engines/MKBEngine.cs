using System;
using System.Collections.Generic;
using System.Text;
using SurgeryHelper.Entities;

namespace SurgeryHelper.Engines
{
    public class MKBEngine
    {
        private readonly Dictionary<string, List<MKBClass>> _mkb;

        private const string MkbInfoSplitStr = "^";

        public MKBEngine(string mkbData)
        {
            _mkb = new Dictionary<string, List<MKBClass>>();

            string[] mkbs = mkbData.Split(new[] { MkbInfoSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mkb in mkbs)
            {
                var kgs = new MKBClass(mkb);
                if (_mkb.ContainsKey(kgs.MkbName))
                {
                    _mkb[kgs.MkbName].Add(kgs);
                }
                else
                {
                    _mkb.Add(kgs.MkbName, new List<MKBClass> { kgs });
                }
            }
        }

        /// <summary>
        /// Записать все данные в строку для подготовки данных к запаковке
        /// </summary>
        /// <returns></returns>
        public string PrepareDataToPack()
        {
            var mkbData = new StringBuilder();
            foreach (string mkbName in _mkb.Keys)
            {
                foreach (MKBClass ksg in _mkb[mkbName])
                {
                    mkbData.Append(ksg + MkbInfoSplitStr);
                }
            }

            return mkbData.ToString();
        }

        /// <summary>
        /// Получить дополнительные данные по указанным кодам МКБ и КСГ
        /// </summary>
        /// <param name="mkbName">Код МКБ</param>
        /// <param name="ksgName">Код КСГ</param>
        /// <returns></returns>
        public MKBClass GetMkbInfo(string mkbName, string ksgName)
        {
            List<MKBClass> mkbInfo = _mkb[mkbName];
            foreach (MKBClass ksg in mkbInfo)
            {
                if (ksg.KsgName == ksgName)
                {
                    return ksg;
                }
            }

            return new MKBClass();
        }

        /// <summary>
        /// Получить все КСГ данные по указанному коду МКБ
        /// </summary>
        /// <param name="mkbName">Код МКБ</param>
        /// <returns></returns>
        public List<MKBClass> GetMkbList(string mkbName)
        {
            if (_mkb.ContainsKey(mkbName))
            {
                return _mkb[mkbName];
            }

            return new List<MKBClass>();
        }
    }
}
