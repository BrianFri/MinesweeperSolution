using MinesweeperLibrary.Models;

namespace MinesweeperLibrary.BusinessLogicLayer
{
    public interface IBoardLogic
    {
        void SetupBombs(BoardModel board, int difficultyLevel);
        void CountBombsNearby(BoardModel board);
        void PlaceReward(BoardModel board);           
        GameState DetermineGameState(BoardModel board); 
    }
}
