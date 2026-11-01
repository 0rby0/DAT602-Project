using Godot;

namespace DATGame
{
    public partial class Class_Select_Config : Control
    {
        public override void _Ready()
        {
            GetNode<Label>("CanvasLayer/Rows/lblRoomName").Text = Main.get.RoomName;
        }

        public void _Back_Button_Clicked()
        {
            Main.get.Room = -1;
            Main.get.RoomName = "";
            Main.get.SwitchScene("res://Room Select Page/Room Select.tscn");
        }
    }
}
