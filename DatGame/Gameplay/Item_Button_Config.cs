using Godot;
using Godot.Collections;
using System.IO;

namespace DATGame
{
    public partial class Item_Button_Config : PanelContainer
    {
        private Dictionary _info;

        public void UpdateContents(Dictionary info)
        {
            _info = info;

            if (_info is not null)
            {
                string _path = $"res://Gameplay/Game Sprites/{_info["Sprite"]}.png";

                string tooltip = $"{_info["AbilityName"]}\n";

                if ((bool)_info["Combat"] == true)
                {
                    tooltip += $"{_info["Damage"]} DMG | ";
                }

                if ((int)_info["Cost"] > 0)
                {
                    tooltip += $"{_info["Cost"]} | ";
                }

                tooltip += $"{_info["Value"]} Points\n{_info["Description"]}";

                GetNode<Button>("Clickbox").TooltipText = tooltip;

                if (ResourceLoader.Exists(_path))
                {
                    GetNode<TextureRect>("sprIcon").Texture = GD.Load<Texture2D>(_path);
                }
                else
                {
                    GetNode<TextureRect>("sprIcon").Texture = GD.Load<Texture2D>("res://Gameplay/Game Sprites/Missing Texture.png");
                }
            }
            else
            {
                GetNode<Button>("Clickbox").TooltipText = null;
                GetNode<TextureRect>("sprIcon").Texture = null;
            }
        }

        public void _ItemButtonClicked()
        {
            GD.Print("Clicked");

            if (Main.get.Itemselected == false)
            {
                if (_info is not null)
                {
                    Main.get.Swapitemid1 = (int)_info["AbilityID"];
                    Main.get.Itemselected = true;
                }
            }
            else
            {
                if (_info is not null)
                {
                    Main.get.Swapitemid2 = (int)_info["AbilityID"];
                }

                PlayerDAO.global.SwapItems(Main.get.Account, Main.get.Room, Main.get.Swapitemid1, Main.get.Swapitemid2);

                Main.get.Swapitemid1 = null;
                Main.get.Swapitemid2 = null;
                Main.get.Itemselected = false;
            }
        }

    }
}
