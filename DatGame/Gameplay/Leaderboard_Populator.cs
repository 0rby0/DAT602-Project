using Godot;
using Godot.Collections;
using System;

namespace DATGame
{
    public partial class Leaderboard_Populator : VBoxContainer
    {
        public override void _Ready()
        {
            LoadLeaders();
        }

        public void LoadLeaders()
        {
            foreach (Node child in GetChildren())
            {
                child.QueueFree();
            }


            Godot.Collections.Array leaderboard = PlayerDAO.global.GetLeaderboard(Main.get.Room);

            for (int i = 0; i < Math.Min(leaderboard.Count, 5); i++)
            {
                Node leaderboardentry = GD.Load<PackedScene>("res://Gameplay/Leaderboard Entry.tscn").Instantiate();
                Leaderboard_Entry_Conf conf = leaderboardentry as Leaderboard_Entry_Conf;

                Dictionary entryinfo = (Dictionary)leaderboard[i];

                conf.Username = (string)entryinfo["AccountName"];
                conf.Score = (int)entryinfo["HighScore"];
                conf.Rank = i + 1;

                CallDeferred("add_child", leaderboardentry);
            }

            VBoxContainer col3 = GetParent<VBoxContainer>();
            Leaderboard_Entry_Conf selfconf = col3.GetNode<Leaderboard_Entry_Conf>("Rank Panel");

            selfconf.Username = Main.get.Account;
            selfconf.Score = PlayerDAO.global.GetPlayerScore(Main.get.Account, Main.get.Room);
        }
    }
}
