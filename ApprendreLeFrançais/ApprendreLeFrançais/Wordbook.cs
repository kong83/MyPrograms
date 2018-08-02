using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApprendreLeFrançais
{
    public class Wordbook
    {
        private readonly string _dataFilePath;
        private Dictionary<string, Wordtab> _words;

        public Wordbook()
        {
            _dataFilePath = Path.Combine(Application.StartupPath, "Data.txt");

            LoadWords();
        }

        private void LoadWords()
        {
            _words = new Dictionary<string, Wordtab>();

            if (!File.Exists(_dataFilePath))
            {
                AddTab("Словарь");               
                return;
            }

            string data = File.ReadAllText(_dataFilePath, Encoding.Unicode);

            string[] tabsInfos = data.Split(new [] {"\r\n"}, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string tabInfo in tabsInfos)
            {
                string[] tabInfoArr = tabInfo.Split(new[] { "<^&^>" }, 2, System.StringSplitOptions.None);

                var wordtab = new Wordtab();
                _words.Add(tabInfoArr[0], wordtab);
                wordtab.FillTab(tabInfoArr[1].Split(new[] { "<-&&->" }, System.StringSplitOptions.None));
            }
        }

        private void SaveWords()
        {
            var data = new StringBuilder();

            foreach (string tabName in _words.Keys)
            {
                data.Append(tabName + "<^&^>");

                foreach (WordName key in _words[tabName].Words.Keys)
                {
                    data.Append(key.Name + "<-&->" + _words[tabName].Words[key] + "<-&&->");
                }

                if (data.ToString().EndsWith("<-&&->"))
                {
                    data.Remove(data.Length - 6, 6);
                }

                data.Append("\r\n");
            }

            File.WriteAllText(_dataFilePath, data.ToString(), Encoding.Unicode);
        }

        public IEnumerable<string> GetAllTabs()
        {
            return _words.Keys;
        }

        public void AddTab(string tabName)
        {
            var wordtab = new Wordtab();
            _words.Add(tabName, wordtab);
            wordtab.FillTab(new string[0]);

            SaveWords();
        }

        public void RenameTab(string oldTabName, string newTabName)
        {
            var oneTabWords = _words.FirstOrDefault(x => x.Key == oldTabName);
            _words.Add(newTabName, oneTabWords.Value);
            _words.Remove(oldTabName);

            SaveWords();
        }

        public void DeleteTab(string tabName)
        {
            _words.Remove(tabName);

            SaveWords();
        }

        public void ShowTab(string tabName, DataGridView wordsList)
        {
            _words[tabName].ShowTab(wordsList);
        }

        public void FillTab(string tabName, DataGridView wordsList)
        {
            _words[tabName].FillTab(wordsList);
            SaveWords();
        }

        public void CopyTab(string targetTabName, string destinationTabName)
        {
            _words[destinationTabName].AppendTab(_words[targetTabName].Words);
        }

        public Dictionary<int, KeyValuePair<WordName, string>> GetAllWordsByTab(string tab, bool invert)
        {
            var words = new Dictionary<int, KeyValuePair<WordName, string>>();

            var random = new Random();
            foreach (var wordKeyValue in _words[tab].Words)
            {
                int nextRandom = GetUniqueRandom(random, words);

                // Меняем местами русские и французские слова, если надо 
                if (invert)
                {
                    words.Add(nextRandom, new KeyValuePair<WordName, string>(new WordName(wordKeyValue.Value), wordKeyValue.Key.Name));
                }
                else
                {
                    words.Add(nextRandom, wordKeyValue);
                }
            }

            return SortDictinaty(words);
        }

        private int GetUniqueRandom(Random random, Dictionary<int, KeyValuePair<WordName, string>> words)
        {
            int nextRandom;
            do
            {
                nextRandom = random.Next(10000);
            }
            while (words.ContainsKey(nextRandom));

            return nextRandom;
        }

        private Dictionary<int, KeyValuePair<WordName, string>> SortDictinaty(Dictionary<int, KeyValuePair<WordName, string>> words)
        {
            var sortedWords = new Dictionary<int,KeyValuePair<WordName,string>>();

            int i = 0;
            while (words.Count > 0)
            { 
                int maxKey = -1;
                foreach (int key in words.Keys)
                {
                    if (key > maxKey)
                    {
                        maxKey = key;
                    }
                }

                sortedWords.Add(i++, words[maxKey]);
                words.Remove(maxKey);
            }

            return sortedWords;
        }

        [DebuggerDisplay("{Name}")]
        public class WordName
        {
            public string Name { get; set; }

            public WordName(string name)
            {
                Name = name;
            }

            public WordName(object name)
            {
                Name = name == null ? string.Empty : name.ToString();
            }
        }

        public class Wordtab
        {
            public Dictionary<WordName, string> Words { get; private set; }

            public void FillTab(DataGridView wordsList)
            {
                Words = new Dictionary<WordName, string>();

                foreach (DataGridViewRow row in wordsList.Rows)
                {
                    if (row.Cells[0].Value != null || row.Cells[1].Value != null)
                    {
                        Words.Add(new WordName(row.Cells[0].Value), GetString(row.Cells[1].Value));
                    }
                }
            }

            private string GetString(object value)
            {
                if (value == null)
                {
                    return string.Empty;
                }

                return value.ToString();
            }

            public void FillTab(string[] wordsInfo)
            {
                Words = new Dictionary<WordName, string>();

                foreach (string wordInfo in wordsInfo)
                {
                    string[] words = wordInfo.Split(new[] { "<-&->" }, 2, System.StringSplitOptions.None);
                    if (words.Length == 2)
                    {
                        Words.Add(new WordName(words[0]), words[1]);
                    }
                }
            }

            public void AppendTab(Dictionary<WordName, string> newWords)
            {
                foreach (var wordInfo in newWords)
                {
                    if (!Words.ContainsKey(wordInfo.Key))
                    {
                        Words.Add(wordInfo.Key, wordInfo.Value);
                    }
                }
            }

            public void ShowTab(DataGridView wordsList)
            {
                wordsList.Rows.Clear();

                foreach (WordName key in Words.Keys)
                {
                    wordsList.Rows.Add(key.Name, Words[key]);
                }
            }
        }        
    }
}
