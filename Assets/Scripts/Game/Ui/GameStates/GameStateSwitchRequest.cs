﻿namespace Game.Ui.GameStates {
    public enum GameStates {
        Start,
        Pause,
        Restart,
        Exit,
    }

    public struct GameStateSwitchRequest {
        public GameStates state;
    }
}