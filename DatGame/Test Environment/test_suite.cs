using Godot;
using System;
using System.Collections.Generic;

namespace DATGame
{
    public partial class test_suite : Node
    {
        private List<Action> _tests = new List<Action>();
        private int _currentTest = 0;

        public override void _Ready()
        {
            // Initial Reset
            DebugDAO.global.ResetDatabase();

            // Login Error: Account not found
            _tests.Add(() => UserDAO.global.Login("!!--!!NotAnAccount!!==!!", "123"));

            // Login Error: Login failed
            _tests.Add(() => UserDAO.global.Login("admin", "321"));

            _tests.Add(LockTest);

            // Create Account Error: Account name already exists
            _tests.Add(() => UserDAO.global.CreateAccount("admin", "123"));

            // Create Room Error: Invalid account name
            _tests.Add(() => RoomDAO.global.CreateRoom("!--2mekwf8mmsss", "!!--!!NotAnAccount!!==!!"));

            // Layout Tiles Error: Room does not exist
            _tests.Add(() => DebugDAO.global.LayoutTiles(4000000, 0, 0));

            // Ability Placement Error: Tile does not exist
            _tests.Add(() => DebugDAO.global.PlaceAbility("Piston Wreck", 1999999999));

            // Ability Placement Error: Ability does not exist
            _tests.Add(() => DebugDAO.global.PlaceAbility("Not Ability", 0));

            _tests.Add(RadiusTest);

            // Move Player Error: Room not found
            _tests.Add(() => MapDAO.global.MovePlayer("admin", room: 10, x: 0, y: 1, range: 1, diagonal: true));

            // Move Player Error: Account not found
            _tests.Add(() => MapDAO.global.MovePlayer("!!_-dsaasdss", room: 0, x: 0, y: 1, range: 1, diagonal: true));

            _tests.Add(OccupationTest);

            // Move Player Error: Diagonal movement on non-diagonal check
            _tests.Add(() => MapDAO.global.MovePlayer("admin", room: 0, x: 1, y: 1, range: 1, diagonal: false));

            // Move Player Error: Tile out of range
            _tests.Add(() => MapDAO.global.MovePlayer("admin", room: 0, x: 2, y: 0, range: 1, diagonal: true));

            // Glitch Ability Error: Room does not exist
            _tests.Add(() => MapDAO.global.Glitch(room: 4000));

            // Item Transfer Error: One or both ability instances not found
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 0, id1: 40, id2: 32));

            // Item Transfer Error: Account not found
            _tests.Add(() => PlayerDAO.global.SwapItems("!!kdofk9", room: 0, id1: null, id2: null));

            // Item Transfer Error: Room not found
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 590, id1: null, id2: null));

            // Item Transfer Error: No abilities to swap
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 0, id1: null, id2: null));

            // Pickup Error: Invalid player
            _tests.Add(() => PlayerDAO.global.SwapItems("!--3fdsfa", room: 0, id1: null, id2: null));

            // Pickup Error: Invalid ability instance
            _tests.Add(() => PlayerDAO.global.SwapItems("admin", room: 0, id1: null, id2: 50));

            // Drop Error: Invalid player
            _tests.Add(() => DebugDAO.global.DropAbility(9090, 1));

            // Drop Error: Invalid ability instance
            _tests.Add(() => DebugDAO.global.DropAbility(1, 9090));

            // Drop Error: Player is not holding that ability
            _tests.Add(() => DebugDAO.global.DropAbility(1, 1));

            // Score Update Error: Invalid player id
            _tests.Add(() => DebugDAO.global.UpdateScore(696969));

            // Kill Room Error: Room does not exist
            _tests.Add(() => RoomDAO.global.KillRoom(67073));

            // Account Update Error: Account does not exist
            _tests.Add(() => UserDAO.global.UpdateAccount("&&&hndekfvuysdu", admin: true, locked: false));

            // Account Deletion Error: Account deletion cancelled
            _tests.Add(() => DebugDAO.global.DeleteAccount("admin", 0));

            // Account Deletion Error: Account does not exist
            _tests.Add(() => DebugDAO.global.DeleteAccount("^undkfi", 1));

            // Player Update Error: Player does not exist
            _tests.Add(() => PlayerDAO.global.UpdatePlayer("V", room: 0, highscore: 100, currentscore: 34));

            // Player Deletion Error: Account not found
            _tests.Add(() => PlayerDAO.global.DeletePlayer("$4fedsf", 0));

            // Player Deletion Error: Room not found
            _tests.Add(() => PlayerDAO.global.DeletePlayer("admin", 23));
        }

        public void _NextTestClicked()
        {
            if (_currentTest < _tests.Count)
            {
                _tests[_currentTest]();
            }

            _currentTest++;
            GetNode<Label>("CanvasLayer/Rows/lblCurrentTest").Text = $"Current Test: {_currentTest}";
        }

        public void LockTest()
        {
            // Login Error: Account locked
            UserDAO.global.UpdateAccount(username: "admin", admin: true, locked: true);
            UserDAO.global.Login("admin", "123");
            UserDAO.global.UpdateAccount(username: "admin", admin: true, locked: false);
        }

        public void RadiusTest()
        {
            // Move Player Error: Search radius too small
            RoomDAO.global.CreateRoom("!!--TestRoom!!==!!", "admin");
            PlayerDAO.global.JoinGame("admin", room: 0, "Gorilla");
            MapDAO.global.MovePlayer("admin", room: 0, x: 0, y: 1, range: 0, diagonal: false);
        }

        public void OccupationTest()
        {
            // Move Player Error: Tile already occupied
            PlayerDAO.global.JoinGame("Ghostie", room: 0, "Eagle");
            MapDAO.global.MovePlayer("Ghostie", room: 0, x: 0, y: 1, range: 1, diagonal: true);
            MapDAO.global.MovePlayer("admin", room: 0, x: 0, y: 1, range: 1, diagonal: true);
        }

    }
}
