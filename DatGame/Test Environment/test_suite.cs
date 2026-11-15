using Godot;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace DATGame
{
    public partial class test_suite : Node
    {
        private List<Action> _tests = new List<Action>();
        private int _currentTest = 0;
        private List<string> _expectedResults = new List<string>();

        public override void _Ready()
        {
            // Initial Reset
            DebugDAO.global.ResetDatabase();

            // 1. Login Error: Account not found
            _tests.Add(() => UserDAO.global.Login("!!--!!NotAnAccount!!==!!", "123"));
            _expectedResults.Add("Error: Account not found");

            // 2. Login Error: Login failed
            _tests.Add(() => UserDAO.global.Login("admin", "321"));
            _expectedResults.Add("Error: Login failed");

            // 3. Login Error: Account locked
            _tests.Add(LockTest);
            _expectedResults.Add("Error: Account locked");

            // 4. Create Account Error: Account name already exists
            _tests.Add(() => UserDAO.global.CreateAccount("admin", "123"));
            _expectedResults.Add("Error: Account name already exists");

            // 5. Create Room Error: Invalid account name
            _tests.Add(() => RoomDAO.global.CreateRoom("!--2mekwf8mmsss", "!!--!!NotAnAccount!!==!!"));
            _expectedResults.Add("Error: Invalid account name");

            // 6. Layout Tiles Error: Room does not exist
            _tests.Add(() => DebugDAO.global.LayoutTiles(4000000, 1, 0));
            _expectedResults.Add("Error: Room does not exist");

            // 7. Ability Placement Error: Tile does not exist
            _tests.Add(() => DebugDAO.global.PlaceAbility("Piston Wreck", 1999999999));
            _expectedResults.Add("Error: Tile does not exist");

            // 8. Move Player Error: Search radius too small
            _tests.Add(RadiusTest);
            _expectedResults.Add("Error: Search radius too small");

            // 9. Ability Placement Error: Ability does not exist
            _tests.Add(() => DebugDAO.global.PlaceAbility("Not Ability", 401));
            _expectedResults.Add("Error: Ability does not exist");

            // 10. Move Player Error: Room not found
            _tests.Add(() => MapDAO.global.MovePlayer("admin", room: 10, x: 0, y: 1, range: 1, diagonal: true));
            _expectedResults.Add("Error: Room not found");

            // 11. Move Player Error: Account not found
            _tests.Add(() => MapDAO.global.MovePlayer("!!_-dsaasdss", room: 2, x: 0, y: 1, range: 1, diagonal: true));
            _expectedResults.Add("Error: Account not found");

            // 12. Move Player Error: Tile already occupied
            _tests.Add(OccupationTest);
            _expectedResults.Add("Error: Tile already occupied");

            // 13. Move Player Error: Diagonal movement on non-diagonal check
            _tests.Add(() => MapDAO.global.MovePlayer("admin", room: 2, x: 1, y: 1, range: 1, diagonal: false));
            _expectedResults.Add("Error: Diagonal movement on non-diagonal check");

            // 14. Move Player Error: Tile out of range
            _tests.Add(() => MapDAO.global.MovePlayer("admin", room: 2, x: 2, y: 0, range: 1, diagonal: true));
            _expectedResults.Add("Error: Tile out of range");

            // 15. Glitch Ability Error: Room does not exist
            _tests.Add(() => MapDAO.global.Glitch(room: 4000));
            _expectedResults.Add("Error: Room does not exist");

            // 16. Item Transfer Error: One or both ability instances not found
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 2, id1: 9999, id2: 6954));
            _expectedResults.Add("Error: One or both ability instances not found");

            // 17. Item Transfer Error: Account not found
            _tests.Add(() => PlayerDAO.global.SwapItems("!!kdofk9", room: 2, id1: 40, id2: 32));
            _expectedResults.Add("Error: Account not found");

            // 18. Item Transfer Error: Room not found
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 590, id1: 40, id2: 32));
            _expectedResults.Add("Error: Room not found");

            // 19. Item Transfer Error: No abilities to swap
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 2, id1: null, id2: null));
            _expectedResults.Add("Error: No abilities to swap");

            // 20. Pickup Error: Invalid player
            _tests.Add(() => DebugDAO.global.PickupAbility(player: 3040, abilityinstance: 1));
            _expectedResults.Add("Error: Invalid player");

            // 21. Pickup Error: Invalid ability instance
            _tests.Add(() => DebugDAO.global.PickupAbility(player: 2, abilityinstance: 5006));
            _expectedResults.Add("Error: Invalid ability instance");

            // 22. Drop Error: Invalid player
            _tests.Add(() => DebugDAO.global.DropAbility(9090, 1));
            _expectedResults.Add("Error: Invalid player");

            // 23. Drop Error: Invalid ability instance
            _tests.Add(() => DebugDAO.global.DropAbility(2, 9090));
            _expectedResults.Add("Error: Invalid ability instance");

            // 24. Drop Error: Player is not holding that ability
            _tests.Add(() => DebugDAO.global.DropAbility(2, 1));
            _expectedResults.Add("Error: Player is not holding that ability");

            // 25. Score Update Error: Invalid player id
            _tests.Add(() => DebugDAO.global.UpdateScore(696969));
            _expectedResults.Add("Error: Invalid player id");

            // 26. Kill Room Error: Room does not exist
            _tests.Add(() => RoomDAO.global.KillRoom(67073));
            _expectedResults.Add("Error: Room does not exist");

            // 27. Account Update Error: Account does not exist
            _tests.Add(() => UserDAO.global.UpdateAccount("&&&hndekfvuysdu", admin: true, locked: false));
            _expectedResults.Add("Error: Account does not exist");

            // 28. Account Deletion Error: Account deletion cancelled
            _tests.Add(() => DebugDAO.global.DeleteAccount("admin", 0));
            _expectedResults.Add("Error: Account deletion cancelled");

            // 29. Account Deletion Error: Account does not exist
            _tests.Add(() => DebugDAO.global.DeleteAccount("^undkfi", 1));
            _expectedResults.Add("Error: Account does not exist");

            // 30. Player Update Error: Player does not exist
            _tests.Add(() => PlayerDAO.global.UpdatePlayer("V", room: 2, highscore: 100, currentscore: 34));
            _expectedResults.Add("Error: Player does not exist");

            // 31. Player Deletion Error: Account not found
            _tests.Add(() => PlayerDAO.global.DeletePlayer("$4fedsf", 2));
            _expectedResults.Add("Error: Account not found");

            // 32. Player Deletion Error: Room not found
            _tests.Add(() => PlayerDAO.global.DeletePlayer("admin", 23));
            _expectedResults.Add("Error: Room not found");
        }

        public override void _Process(double delta)
        {
            if (_currentTest < _tests.Count)
            {
                GD.Print("\n----\n");
                GD.Print($"Running Test {_currentTest + 1}/{_tests.Count}");
                GD.Print("Actual Result:");
                _tests[_currentTest]();
                GD.Print($"Expected Result:\n{_expectedResults[_currentTest]}");
                _currentTest++;
            }
            else
            {
                GetNode<Label>("CanvasLayer/Rows/lblTests").Text = "Tests Complete, Check the Console for Results";
            }
        }

        public void LockTest()
        {
            UserDAO.global.UpdateAccount(username: "admin", admin: true, locked: true);
            UserDAO.global.Login("admin", "admin123");
            UserDAO.global.UpdateAccount(username: "admin", admin: true, locked: false);
        }

        public void RadiusTest()
        {
            RoomDAO.global.CreateRoom("!!--TestRoom!!==!!", "admin");
            PlayerDAO.global.JoinGame("admin", room: 2, "Gorilla");
            MapDAO.global.MovePlayer("admin", room: 2, x: 0, y: 1, range: 0, diagonal: false);
        }

        public void OccupationTest()
        {
            PlayerDAO.global.JoinGame("Ghostie", room: 2, "Eagle");
            MapDAO.global.MovePlayer("Ghostie", room: 2, x: 0, y: 1, range: 1, diagonal: true);
            MapDAO.global.MovePlayer("admin", room: 2, x: 0, y: 1, range: 1, diagonal: true);
        }

        public override void _Notification(int what)
        {
            if (what == NotificationWMCloseRequest)
            {
                // Reset the database before closing
                DebugDAO.global.ResetDatabase();
                GetTree().Quit();
            }
        }

    }
}
