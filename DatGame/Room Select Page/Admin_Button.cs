using Godot;

namespace DATGame
{
    public partial class Admin_Button : Button
    {
        public void _Admin_Button_Clicked()
        {
            Main.get.SwitchScene("res://Admin Page/Admin Centre.tscn");
        }

    }
}
