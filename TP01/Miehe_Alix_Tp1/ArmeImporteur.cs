using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    internal static class ArmeImporteur
    {
        private static string projectDir;
        private static string DataFilePath;
        private static string BlacklistFilePath;
        private static string baseDir;
        private static Dictionary<string, int> dict;

        public static int minWordLength { set; get; }

        public static void Init()
        {
            InitPaths();

            InitList();

            minWordLength = 3;
        }




        private static void InitPaths()
        {
            baseDir = AppContext.BaseDirectory;
            // Go up from bin/Debug/netX.XX to the project folder
            projectDir = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;

            // Now go to the folder next to the .sln file
            DataFilePath = Path.Combine(projectDir, "..", "DataFiles", "txt.txt");
            BlacklistFilePath = Path.Combine(projectDir, "..", "DataFiles", "blacklist.txt");
        }




        private static void InitList()
        {
            dict = new Dictionary<string, int>();
            string content = File.ReadAllText(DataFilePath);
            List<string> wordList = content.Split(" ").ToList();

            foreach (string word in wordList)
            {
                string normalizedWord = NormalizeWord(word);

                if (dict.ContainsKey(normalizedWord))
                {
                    dict[normalizedWord]++;
                }
                else if (ValidateWord(normalizedWord))
                {
                    dict[normalizedWord] = 1;
                }
            }
        }






        public static void PrintWords()
        {
            foreach (KeyValuePair<string, int> entry in dict)
            {
                Console.WriteLine($"{entry.Key} : {entry.Value}");
            }
        }




        private static bool ValidateWord(string word)
        {
            string blacklistContent = File.ReadAllText(BlacklistFilePath);
            List<string> blacklist = blacklistContent.Split(" ").ToList();

            bool rtrn = true;

            if (blacklist.Contains(word) && word.Length > minWordLength)
            {
                rtrn = false;
            }

            return rtrn;
        }

        private static string NormalizeWord(string word)
        {
            // Lowercase first
            word = word.ToLower();

            // Keep only letters or digits
            var result = new string(word.Where(char.IsLetterOrDigit).ToArray());

            return result;
        }


        public static void addToBlacklist(string word)
        {
            File.AppendAllText(BlacklistFilePath, " " + NormalizeWord(word));
        }



        public static void removeFromBlacklist(string word)
        {
            string blacklistContent = File.ReadAllText(BlacklistFilePath);
            List<string> blacklist = blacklistContent.Split(" ").ToList();
            if (blacklist.Contains(word))
            {
                blacklist.Remove(word);
                File.WriteAllText(BlacklistFilePath, string.Join(" ", blacklist));
            }
        }

        public static void ImportWeapons()
        {
            foreach (KeyValuePair<string, int> entry in dict)
            {
                // randomly select weapon type from enum
                Random rnd = new Random();
                EWeaponType randomValue = (EWeaponType)rnd.Next(Enum.GetValues(typeof(EWeaponType)).Length);

                Armory.AddWeapon(new Weapon(NormalizeWord(entry.Key), entry.Key.Length, entry.Value, randomValue, 1));
            }
        }


    }
}
