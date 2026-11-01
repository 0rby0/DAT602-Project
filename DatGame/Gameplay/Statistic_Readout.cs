using Godot;
using Godot.Collections;

namespace DATGame
{
	public partial class Statistic_Readout : RichTextLabel
	{
		public override void _Process(double delta)
		{
			Dictionary playerinfo = PlayerDAO.global.GetPlayer(Main.get.Account, Main.get.Room);

			Text = $"Position: ({playerinfo["XPos"]}, {playerinfo["YPos"]})\nStatistics:\n\tStength: {playerinfo["Strength"]}\n\tSpeed: {playerinfo["Speed"]}\n\tEnergy: {playerinfo["Energy"]}\n\tHealth: {playerinfo["Health"]}\n\nCurrent Score: {playerinfo["CurrentScore"]}\n\nHP: {playerinfo["CurrentHealth"]}";
		}

	}
}
