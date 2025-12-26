
namespace Game
{
    public struct LobbyState
    {
        public LobbyState()
        {
            Players = [];
        }

        public List<LobbyPlayerState> Players { get; set; }
    }

    public struct LobbyPlayerState
    {
        public string Name { get; set; }
        public bool IsReady { get; set; }
    }


}