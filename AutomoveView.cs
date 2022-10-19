using System;

namespace SnakeTheGame
{
    internal class AutomoveView : BaseView
    {
        AutomovePresenter presenter;
        public AutomoveView(AutomovePresenter presenter)
        {
            this.presenter = presenter;
        }
        internal void init()
        {
            Console.WriteLine("Have you ever wondered what a snake trying to find food looks like? Press any key to find out!");
            Console.ReadKey();
            Console.Clear();
            presenter.initField();
        }

        internal void showAteItselfGameoverScreen()
        {
            Console.Clear();
            Console.WriteLine("This snake isn't smart! It ate itself! To restart press any key");
            Console.ReadKey();
            presenter.initField();
        }
        internal void showRanIntoWallGameoverScreen()
        {
            Console.Clear();
            Console.WriteLine("This snake isn't smart! It ate itself! To restart press any key");
            Console.ReadKey();
            presenter.initField();
        }
    }
}