using DevGames.API.Entities;

namespace DevGames.API.Persistance
{
    public class DevGameContext
    {
        public DevGameContext()
        {
            Boards = new List<Board>();

        }

        public List<Board> Boards{ get; private set; }
    }
}
