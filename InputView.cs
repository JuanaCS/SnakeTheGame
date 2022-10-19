using System;

namespace SnakeTheGame
{
    internal class InputView : BaseView
    {
        InputPresenter presenter;

        public InputView(InputPresenter presenter)
        {
            this.presenter = presenter;
        }

        internal void init()
        {
            Console.WriteLine("Welcome to THE snake game! Move using the w,a,s,d keys. Press any key to start.");
            Console.ReadKey();
            Console.Clear();
            presenter.initField();
        }


        internal void showAteItselfGameoverScreen()
        {
            Console.Clear();
            Console.WriteLine("GameOver!\n" + "It looks like your too hungry for your own good..\n" + "Do you want to try again? -> press any key");
            Console.ReadKey();
            presenter.initField();
        }

        internal void showRanIntoWallGameoverScreen()
        {
            Console.Clear();
            Console.WriteLine("GameOver!\n" + "Escape isn't an option!\n" + "Do you want to try again? -> press any key");
            Console.ReadKey();
            presenter.initField();
        }
    }
}