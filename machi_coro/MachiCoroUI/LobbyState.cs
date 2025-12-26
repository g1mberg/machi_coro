
namespace MachiCoroUI
{
    class LobbyState
    {
        public List<LobbyPlayerState> Players;
    }

    class LobbyPlayerState
    {
        public int PlayerId;
        public string Name;
        public bool IsReady;
    }

}