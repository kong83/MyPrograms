using System;
using System.IO;
using System.Windows.Forms;
using TestResultMaker;

namespace PsychologicalTests
{
    static class Program
    {
        public static TestGeneralInfo TestGeneralInfo;
        public static AnxietyResults AnxietyResults;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string testSavePath = Properties.Settings.Default.TestSavePath;
            if (!Directory.Exists(testSavePath))
            {
                MessageBox.Show("Директория для сохранения результатов тестирования не найдена. Выполнение программы невозможно. Проверьте правильность директории в конфиг файле.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string testFilePath = Path.Combine(testSavePath, "test.txt");
                File.WriteAllText(testFilePath, "test");
                File.Delete(testFilePath);
            }
            catch
            {
                MessageBox.Show("Создание файлов в директории для сохранения результатов тестирования запрещено. Выполнение программы невозможно. Проверьте правильность директории в конфиг файле и права доступа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new StartForm().ShowDialog();

            if (TestGeneralInfo == null)
            {
                return;
            }

            switch (TestGeneralInfo.TestName)
            {
                case "Test_Phillipsa":
                    new AnxietyTestForm().ShowDialog();

                    if (AnxietyResults != null)
                    {
                        new ResultForm().ShowDialog();
                    }
                    break;
                default:
                    MessageBox.Show("Неизвестный тип теста. Выполнение невозможно. Поддерживаемые типы тестов: Test_Phillipsa", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
        }
    }
}
