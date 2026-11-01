using Godot;

namespace DATGame
{
    public partial class Back_Button : Button
    {
        public void _Back_Button_Clicked()
        {
            Main.get.Account = null;
            Main.get.SwitchScene("res://Login Page/Login.tscn");
        }
    }
}
