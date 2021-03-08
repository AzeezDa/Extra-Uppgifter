﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class HighScoreItem
    {
        // Name of the player with that highscore
        public string Name { get; protected set; }

        // Amount of bosses defeated
        public int BossesDefeated { get; protected set; }

        // The time it took to defeat those bosses
        public TimeSpan Time { get; protected set; }
        public HighScoreItem(string name, int bossesDefeated, TimeSpan time)
        {
            BossesDefeated = bossesDefeated;
            Time = time;
            Name = name;
        }
    }
    public static class HighScoreManager
    {
        public static List<HighScoreItem> LoadHighScores()
        {
            // List to store the high score items
            List<HighScoreItem> highScoreItems = new List<HighScoreItem>();

            // Load the document
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("../../../Data/HighScore.xml");

            // Loop inside each node and get the items
            foreach (XmlNode node in xmlDocument.SelectSingleNode("Items").ChildNodes)
            {
                highScoreItems.Add(new HighScoreItem(node.Attributes["Name"].Value, int.Parse(node.Attributes["BossesDefeated"].Value), new TimeSpan(long.Parse(node.Attributes["Ticks"].Value))));
            }


            return highScoreItems;
        }

        public static void SaveHighScore(HighScoreItem highScoreItem)
        {
            // Load the document
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("../../../Data/HighScore.xml");

            // Create a new element and add the values to it as attributes
            XmlElement element = xmlDocument.CreateElement("Item");
            element.SetAttribute("Name", highScoreItem.Name);
            element.SetAttribute("BossesDefeated", highScoreItem.BossesDefeated.ToString());
            element.SetAttribute("Ticks", highScoreItem.Time.Ticks.ToString());

            // Append the element to the Items element
            xmlDocument["Items"].AppendChild(element);

            // Save the document
            xmlDocument.Save("../../../Data/HighScore.xml");
        }
    }
}
