using Godot;

namespace DATGame
{
    public partial class Gameplay_Config : Control
    {
        public void _Back_Button_Clicked()
        {
            Main.get.Combatant = null;
            Main.get.Itemselected = false;
            PlayerDAO.global.ExitRoom(Main.get.Account, Main.get.Room);
            Main.get.SwitchScene("res://Room Select Page/Room Select.tscn");
        }

    }
}
