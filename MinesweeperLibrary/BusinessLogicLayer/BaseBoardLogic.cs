using MinesweeperLibrary.Models;

namespace MinesweeperLibrary.BusinessLogicLayer
{
    public abstract class BaseBoardLogic
    {
        public virtual void PerformPostSetupActions(BoardModel board)
        {
            System.Console.WriteLine("Base post-setup actions completed.");
        }
    }
}