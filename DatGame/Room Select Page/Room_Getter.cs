using Godot;
using Godot.Collections;

namespace DATGame
{
    public partial class Room_Getter : VBoxContainer
    {
        public override void _Ready()
        {
            RefreshRooms();
        }

        public void RefreshRooms()
        {
            foreach (Node child in GetChildren())
            {
                child.QueueFree();
            }

            Array rooms = RoomDAO.global.GetRooms();

            foreach (Dictionary room in rooms)
            {
                Node roombox = GD.Load<PackedScene>("res://Room Select Page/Room Entry.tscn").Instantiate();
                Room_Config roomboxconf = roombox as Room_Config;

                roomboxconf.Id = (int)room["RoomID"];
                roomboxconf.Roomname = (string)room["RoomName"];
                roomboxconf.Playercount = (int)room["PlayerCount"];

                AddChild(roombox);
            }
        }
    }
}
