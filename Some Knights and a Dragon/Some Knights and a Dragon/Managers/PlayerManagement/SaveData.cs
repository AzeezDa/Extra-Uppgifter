using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Some_Knights_and_a_Dragon.Managers.PlayerManagement
{
    public class SaveData
    {
       public Dictionary<string, string> DataSaveValues { get; private set; }

        // The items in the player inventory
        public InventoryItem[] InventoryItems { get; private set; }

        public SaveData(string filePath) : this()
        {
            XmlDocument saveDataXml = new XmlDocument();
            saveDataXml.Load(filePath);
            XmlElement root = saveDataXml.DocumentElement;

            // Load the player names, character, level and health
            DataSaveValues["PlayerName"] = root["PlayerName"].InnerText;
            DataSaveValues["Character"] = root["Character"].InnerText;
            DataSaveValues["Level"] = root["Level"].InnerText;
            DataSaveValues["CurrentHealth"] = root["CurrentHealth"].InnerText;
            DataSaveValues["MaxHealth"] = root["MaxHealth"].InnerText;

            // Load the inventory
            InventoryItems = new InventoryItem[5];
            int i = 0;
            foreach (XmlNode xmlNode in root.SelectNodes("InventoryItem"))
            {
                if (xmlNode.Attributes["ItemTypeName"].Value != "null")
                    InventoryItems[i] = new InventoryItem((Items.Item)Activator.CreateInstance(null, xmlNode.Attributes["ItemTypeName"].Value).Unwrap(), int.Parse(xmlNode.Attributes["Amount"].Value));
                i++;
            }
        }

        public SaveData()
        {
            DataSaveValues = new Dictionary<string, string>();
            DataSaveValues.Add("PlayerName", "");
            DataSaveValues.Add("Character", "");
            DataSaveValues.Add("Level", "");
            DataSaveValues.Add("CurrentHealth", "");
            DataSaveValues.Add("MaxHealth", "");
        }

        public void Save()
        {
            XmlDocument saveDataxml = new XmlDocument();
            XmlElement root = saveDataxml.CreateElement("SaveData");
            saveDataxml.AppendChild(root);

            foreach (string item in DataSaveValues.Keys)
            {
                XmlElement element = saveDataxml.CreateElement(item);
                element.InnerText = DataSaveValues[item];

                root.AppendChild(element);
            }

            for (int i = 0; i < 5; i++)
            {
                XmlElement element = saveDataxml.CreateElement("InventoryItem");
                if (InventoryItems[i] != null)
                {
                    element.SetAttribute("ItemTypeName", InventoryItems[i].Item.GetType().ToString());
                    element.SetAttribute("Amount", InventoryItems[i].Amount.ToString());
                }
                else
                {
                    element.SetAttribute("ItemTypeName", "null");
                    element.SetAttribute("Amount", "0");
                }
                root.AppendChild(element);
            }

            saveDataxml.Save("../../../Saves/" + DataSaveValues["PlayerName"] + ".save");
        }

        public void LoadCurrentData()
        {
            DataSaveValues["PlayerName"] = Game1.WindowManager.GetGameplayWindow().PlayerName;
            DataSaveValues["Character"] = Game1.WindowManager.GetGameplayWindow().Player.Creature.GetType().FullName;
            DataSaveValues["Level"] = Game1.WindowManager.GetGameplayWindow().CurrentLevel.Name + ".xml";
            DataSaveValues["CurrentHealth"] = Game1.WindowManager.GetGameplayWindow().Player.Creature.CurrentHealth.ToString();
            DataSaveValues["MaxHealth"] = Game1.WindowManager.GetGameplayWindow().Player.Creature.MaxHealth.ToString();
            InventoryItems = Game1.WindowManager.GetGameplayWindow().Player.Inventory.Inventory;
        }

        public void ChangeValue(string valueName, string value)
        {
            DataSaveValues[valueName] = value;
        }
    }
}
