using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TetrisOOP
{
    class GameBoard : Board
    {
        private int level = 0;
        public int Level
        {
            get { return level; }
        }
        public string LevelInfo
        {
            get
            {
                return "Уровень: " + level.ToString() + "\n\n1 линия: " + clearRows[0].ToString() +
                  "\n2 линия: " + clearRows[1].ToString() + "\n3 линия: " + clearRows[2].ToString() +
                  "\n4 линия: " + clearRows[3].ToString();
            }
        }
        private string pathSave;
        private string pathLevel;
        private int[] clearRows = new int[4];
        private int localScore;

        public GameBoard(int maxT, int maxW)
            : base(maxT, maxW)
        {
            pathSave = Application.StartupPath + "\\save.sav";
            pathLevel = Application.StartupPath + "\\Levels\\";
            if (!FindSaveFile())
            {
                NextLevel();
            }
        }

        /// <summary>
        /// Начислить очки в зависимости от количества убранных строк
        /// </summary>
        /// <param name="cntRow"></param>
        public override void SetScore(int cntRow)
        {
            cntRow--;
            if (cntRow < 0)
                return;
            if (clearRows[cntRow] > 0)
            {
                clearRows[cntRow]--;
                score += (cntRow + 1) * 10 + (cntRow + 1) * 5;
                localScore += (cntRow + 1) * 10 + (cntRow + 1) * 5;
            }
            else if (score > 0)
            {
                score--;
                localScore--;
            }

            if (localScore >= speed * 1000)
            {
                if (speed < 10)
                {
                    speed++;
                }
                else
                {
                    speed = 10.5;
                }
            }

            int s = 0;
            for (int i = 0; i < clearRows.Length; i++)
            {
                s += clearRows[i];
            }

            if (s == 0)
            {
                score += 100;
                NextLevel();
            }
        }

        /// <summary>
        /// Поиск файла с сохранением предыдущей игры и загрузка данных из него
        /// </summary>
        private bool FindSaveFile()
        {
            FileInfo fi = new FileInfo(pathSave);
            if (!fi.Exists)
            {
                score = 0;
                level = 0;
                return false;
            }

            if (MessageBox.Show("Найдена незаконченная игра. Загрузить?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                score = 0;
                level = 0;
                return false;
            }

            if (!LoadGame())
            {
                score = 0;
                level = 0;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Загрузка сохранённых данных
        /// </summary>
        private bool LoadGame()
        {
            ZLibClass zlibClass = new ZLibClass();
            byte[] unpackInfo;
            using (FileStream fs = new FileStream(pathSave, FileMode.Open))
            {
                byte[] packInfo = new byte[fs.Length];
                fs.Read(packInfo, 0, (int)fs.Length);
                unpackInfo = zlibClass.Unpack(packInfo);
            }

            string fileData = Encoding.GetEncoding("windows-1251").GetString(unpackInfo);
            string[] arrData = fileData.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            int n = 0;
            level = score = maxTop = maxWidth = localScore = -1;
            speed = -1.0;
            while (n < arrData.Length)
            {
                string name = "";
                string value = "";
                try
                {
                    name = arrData[n].Substring(0, arrData[n].IndexOf("="));
                    value = arrData[n].Substring(arrData[n].IndexOf("=") + 1);
                    string[] s;

                    switch (name)
                    {
                        case "level":
                            level = Convert.ToInt32(value);
                            break;
                        case "score":
                            score = Convert.ToInt32(value);
                            break;
                        case "localScore":
                            localScore = Convert.ToInt32(value);
                            break;
                        case "speed":
                            speed = Convert.ToDouble(value);
                            break;
                        case "clearRows":
                            s = value.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            if (s.Length != 4)
                            {
                                return false;
                            }
                            for (int i = 0; i < s.Length; i++)
                            {
                                clearRows[i] = Convert.ToInt32(s[i]);
                            }
                            break;
                        case "fieldSize":
                            s = value.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            if (s.Length != 2)
                            {
                                return false;
                            }
                            maxTop = Convert.ToInt32(s[0]);
                            maxWidth = Convert.ToInt32(s[1]);
                            break;
                        case "field":
                            s = value.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < maxTop; i++)
                            {
                                for (int j = 0; j < maxWidth; j++)
                                {
                                    field[i, j] = Convert.ToInt32(s[i * maxWidth + j]);
                                }
                            }
                            break;
                    }
                    n++;
                }
                catch
                {
                    MessageBox.Show("Файл повреждён или записан с ошибкой", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (level != -1 && score != -1 && speed != -1.0 && maxTop != -1 && maxWidth != -1 && localScore != -1)
            {
                File.Delete(pathSave);
                return true;
            }
            else
            {
                MessageBox.Show("Не все данные найдены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Сохранение текущей позиции
        /// </summary>
        public override void SaveGame()
        {
            ZLibClass zlibClass = new ZLibClass();
            string allInfo = "";
            allInfo += "level=" + level.ToString() + ";";
            allInfo += "score=" + score.ToString() + ";";
            allInfo += "localScore=" + localScore.ToString() + ";";
            allInfo += "speed=" + speed.ToString() + ";";
            allInfo += "clearRows=" + clearRows[0].ToString();
            for (int i = 1; i < 4; i++)
            {
                allInfo += "," + clearRows[i].ToString();
            }
            allInfo += ";";
            allInfo += "fieldSize=" + maxTop.ToString() + "," + maxWidth.ToString() + ";";
            allInfo += "field=";
            for (int i = 0; i < maxTop; i++)
            {
                for (int j = 0; j < maxWidth; j++)
                {
                    if (field[i, j] != 10)
                    {
                        allInfo += field[i, j] + ",";
                    }
                    else
                    {
                        allInfo += "0,";
                    }
                }
            }
            allInfo = allInfo.Substring(0, allInfo.Length - 1);


            byte[] arrInfo = Encoding.GetEncoding("windows-1251").GetBytes(allInfo);
            byte[] packInfo = zlibClass.Pack(arrInfo);
            using (FileStream fs = new FileStream(pathSave, FileMode.Create))
            {
                fs.Write(packInfo, 0, packInfo.Length);
            }
        }

        /// <summary>
        /// Переход на следующий уровень
        /// </summary>
        private bool NextLevel()
        {
            ZLibClass zlibClass = new ZLibClass();
        OneMore:
            level++;
            // загрузка нового уровня
            FileInfo fi = new FileInfo(pathLevel + level.ToString("D3") + ".lvl");
            if (!fi.Exists)
            {
                if (level == 1)
                {
                    level = 0;
                    MessageBox.Show("Не найден первый уровень", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show("Все уровни пройдены. Вы - победитель!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);

                level = 0;
                goto OneMore;
            }

            try
            {
                FileStream fs = fi.OpenRead();
                byte[] lvlInfo = new byte[fs.Length];
                fs.Read(lvlInfo, 0, (int)fs.Length);
                byte[] unpackLvlInfo = zlibClass.Unpack(lvlInfo);

                string lvlStr = Encoding.GetEncoding("windows-1251").GetString(unpackLvlInfo);
                string[] data = lvlStr.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 4; i++)
                    clearRows[i] = Convert.ToInt32(data[i]);
                for (int i = 0; i < maxTop; i++)
                {
                    for (int j = 0; j < maxWidth; j++)
                    {
                        field[i, j] = Convert.ToInt32(data[i * maxWidth + j + 4]);
                    }
                }

                speed = 1;
                localScore = 0;
                return true;
            }
            catch
            {
                level = 0;
                MessageBox.Show("Файл с уровнем " + level.ToString() + " повреждён", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
