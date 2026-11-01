using Godot;
using Godot.Collections;

namespace DATGame
{
    public partial class Tile_Inventory : VBoxContainer
    {
        private bool _ontoprow = true;

        public override void _Ready()
        {
            base._Ready();

            LoadTileInventory();
        }

        public void LoadTileInventory()
        {

            Array tileabilities = MapDAO.global.GetTileInventory(Main.get.Account, Main.get.Room, 0, 0);

            HBoxContainer toprow = GetNode<HBoxContainer>("Top Row");
            HBoxContainer bottomrow = GetNode<HBoxContainer>("Bottom Row");

            Array<Node> toprowchildren = toprow.GetChildren();
            Array<Node> bottomrowchildren = bottomrow.GetChildren();

            Array<Node> allchildren = toprowchildren + bottomrowchildren;

            for (int rm = allchildren.Count; rm > tileabilities.Count + 1; rm--)
            {
                if (rm > tileabilities.Count + 1)
                {
                    allchildren[rm - 1].QueueFree();
                    _ontoprow = !_ontoprow;
                }
            }

            for (int cr = allchildren.Count; cr < tileabilities.Count + 1; cr++)
            {
                if (cr < tileabilities.Count + 1)
                {
                    Node itembutton = GD.Load<PackedScene>("res://Gameplay/Item Button.tscn").Instantiate();
                    if (_ontoprow == true)
                    {
                        toprow.CallDeferred("add_child", itembutton);
                    }
                    else
                    {
                        bottomrow.CallDeferred("add_child", itembutton);
                    }
                    _ontoprow = !_ontoprow;
                }
            }

            for (int item = 0; item < allchildren.Count; item++)
            {
                Node slot = allchildren[item];

                Item_Button_Config conf = (Item_Button_Config)slot;

                if (item < tileabilities.Count)
                {
                    conf.UpdateContents((Dictionary)tileabilities[item]);
                }
                else
                {
                    conf.UpdateContents(null);
                }
            }
        }
    }

}
