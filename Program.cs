using System;
using System.Reflection;

namespace SnakeTheGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //0 = Snake bewegt sich automatisch, 1 = klassisches Snake
            int programToUse = 0;

            if (programToUse == 0)
            {
                Model model = new Model();
                AutomovePresenter presenter = new AutomovePresenter();
                AutomoveView view = new AutomoveView(presenter);

                presenter.setView(view);
                presenter.setModel(model);

                view.init();

                while (true)
                {
                    presenter.updateField();
                }
            }

            else if (programToUse == 1)
            {
                Model model = new Model();
                InputPresenter presenter = new InputPresenter();
                InputView view = new InputView(presenter);

                presenter.setView(view);
                presenter.setModel(model);

                Console.CursorVisible = false;
                view.init();
                while (true)
                {

                    if (Console.KeyAvailable)
                    {
                        presenter.handleInput(Console.ReadKey().Key);
                        continue;
                    }
                    presenter.updateField();
                }
            }
        }
    }
}
