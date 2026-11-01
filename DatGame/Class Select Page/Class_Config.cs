using Godot;

namespace DATGame
{
    public partial class Class_Config : Panel
    {
        private string _sprite;
        private string _classname;
        private string _ability;

        public string Sprite { get => _sprite; set => _sprite = value; }
        public string Classname { get => _classname; set => _classname = value; }
        public string Ability { get => _ability; set => _ability = value; }

        public override void _Ready()
        {
            string spritepath = $"res://Gameplay/Game Sprites/{_sprite}.png";

            GetNode<TextureRect>("Stack/sprIcon").Texture = (Texture2D)GD.Load(spritepath);
            GetNode<Label>("Stack/lblClassName").Text = _classname;
            GetNode<Label>("Stack/lblStartingAbility").Text = _ability;
        }

        public void _Class_Clicked()
        {
            if (PlayerDAO.global.JoinGame(Main.get.Account, Main.get.Room, _classname) == "Tile occupied")
            {
                Main.get.SwitchScene("res://Respawn Menu/Respawn Menu.tscn");
            }
            else
            {
                Main.get.SwitchScene("res://Gameplay/Gameplay.tscn");
            }

        }

    }
}
