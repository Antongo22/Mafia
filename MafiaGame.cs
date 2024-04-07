using System;
using System.Collections.Generic;
using System.IO;

namespace Mafia
{
    public static class MafiaGame
    {
        static string[] players;
        static string[] roles;

        public static Dictionary<string, string> playerRoles = new Dictionary<string, string>();

        static void GetPlayerss(string filepath)
        {
            players = File.ReadAllLines(filepath);
        }

        static void GetRoles(string filepath)
        {
            roles = File.ReadAllLines(filepath);
        }

        static void isCorrectData()
        {
            if (players.Length != roles.Length)
            {
                throw new Exception("Количество игроков и ролей не совпадает!");
            }
        }

        static void ShufflePlayersAndRoles()
        {
            List<string> shuffledRoles = new List<string>(roles);

            Random rng = new Random();
            int n = shuffledRoles.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);

                string tempRole = shuffledRoles[k];
                shuffledRoles[k] = shuffledRoles[n];
                shuffledRoles[n] = tempRole;
            }

            for (int i = 0; i < players.Length; i++)
            {
                playerRoles[players[i]] = shuffledRoles[i];
            }
        }


        static string GetAllRoles()
        {
            string rolseText = "";

            foreach(var role in playerRoles)
            {
                rolseText += role.Key + " - " + role.Value + "\n";
            }

            return rolseText;
        }

        static void SaveRoles(string fileToSave)
        {
            File.WriteAllText(fileToSave, GetAllRoles());
        }



        public static void Game(string fileplayers, string fileroles, string fileSave)
        {
            GetPlayerss(fileplayers);
            GetRoles(fileroles);

            isCorrectData();

            ShufflePlayersAndRoles();

            SaveRoles(fileSave);
        }

    }
}
