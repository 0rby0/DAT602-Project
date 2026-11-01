using Godot;
using Godot.Collections;

namespace DATGame
{
	public partial class Room_Select_Config : Control
	{
		private HBoxContainer _panels;
		private VBoxContainer _infopanel;
		private VBoxContainer _creationpanel;
		private VBoxContainer _killpanel;
		private VBoxContainer _replacepanel;

        public override void _Ready()
		{
			_panels = GetNode<HBoxContainer>("CanvasLayer/Columns");

			_infopanel = _panels.GetNode<VBoxContainer>("Column 2/pnlRoomBox/RoomInfo");
			_creationpanel = _panels.GetNode<VBoxContainer>("Column 2/pnlRoomBox/RoomCreation");
			_killpanel = _panels.GetNode<VBoxContainer>("Column 2/pnlRoomBox/RoomKill");
			_replacepanel = _panels.GetNode<VBoxContainer>("Column 2/pnlRoomBox/RoomReplace");

			Dictionary account_details = UserDAO.global.GetAccount(Main.get.Account);

			if (RoomDAO.global.HasRooms(Main.get.Account))
            {
                Dictionary room_details = RoomDAO.global.GetRoom(Main.get.Account);

				Main.get.Room = (int)room_details["RoomID"];
				Main.get.RoomName = (string)room_details["RoomName"];

				_infopanel.Visible = true;
				_infopanel.GetNode<Label>("lblName").Text = (string)room_details["RoomName"];
				_infopanel.GetNode<Label>("lblPlayers").Text = $"{(string)room_details["PlayerCount"]} Players";
            }

            _panels.GetNode<Button>("Column 1/Options Column/btnAdmin").Visible = (bool)account_details["Admin"];
			_panels.GetNode<Label>("Column 2/lblPlayerName").Text = (string)account_details["Name"];
		}
        public void _Create_Room_Clicked()
        {
			_infopanel.Visible = false;
			_killpanel.Visible = false;

			if (RoomDAO.global.HasRooms(Main.get.Account))
			{
				_creationpanel.Visible = false;
				_replacepanel.Visible = true;
			}
			else
            {
                _replacepanel.Visible = false;
                _creationpanel.Visible = true;
            }
        }

    }
}
