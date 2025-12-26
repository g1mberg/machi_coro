namespace ProtocolFramework;

public enum XPacketType : byte
{
    Unknown = 0,
    Handshake = 1,

    PlayerConnected = 10,
    Welcome = 11,
    PlayerJoined = 12,
    GameStart = 13,
    PlayerReady =14,
    LobbyState,
    YouArePlayer=18,



    Roll = 20,
    Reroll = 21,
    Build = 24,
    Change = 25,
    Confirm = 26,
    Steal = 27,
    
    GameStateUpdate = 30,
    
    Error = 40
}
