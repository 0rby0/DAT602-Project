using Godot;
using Godot.Collections;

namespace DATGame
{
    public partial class Tile_Button_Config : Button
    {
        private Dictionary _info;

        public override void _Ready()
        {
            base._Ready();
        }

        public void UpdateContents(Dictionary Info)
        {
            _info = Info;

            if (_info is null)
            {
                return;
            }

            Text = $"({_info["XPos"]}, {_info["YPos"]})";
        }

        public void _TileSelected()
        {
            PlayerDAO.global.Rejoin(Main.get.Account, Main.get.Room, (int)_info["TileID"]);

            Main.get.SwitchScene("res://Gameplay/Gameplay.tscn");
        }

    }
}
