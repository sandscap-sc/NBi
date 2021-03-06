﻿using System;
using System.Linq;
using NBi.UI.Genbi.Presenter;

namespace NBi.UI.Genbi.Command.TestCases
{
    abstract class MoveVariableCommand : CommandBase
    {
        protected readonly TestCasesPresenter presenter;


        protected MoveVariableCommand(TestCasesPresenter presenter)
        {
            this.presenter = presenter;
        }
    }

    class MoveLeftVariableCommand : MoveVariableCommand
    {

        public MoveLeftVariableCommand(TestCasesPresenter presenter) : base(presenter)
        {}

        /// <summary>
        /// Refreshes the command state.
        /// </summary>
        public override void Refresh()
        {
            this.IsEnabled = !presenter.IsFirst();
        }

        /// <summary>
        /// Executes the command logics.
        /// </summary>
        public override void Invoke()
        {
            presenter.Move(presenter.VariableSelectedIndex, presenter.VariableSelectedIndex-1);
        }
    }


    class MoveRightVariableCommand : MoveVariableCommand
    {

        public MoveRightVariableCommand(TestCasesPresenter presenter) : base(presenter)
        {}

        /// <summary>
        /// Refreshes the command state.
        /// </summary>
        public override void Refresh()
        {
            this.IsEnabled = !presenter.IsLast();
        }

        /// <summary>
        /// Executes the command logics.
        /// </summary>
        public override void Invoke()
        {
            presenter.Move(presenter.VariableSelectedIndex, presenter.VariableSelectedIndex + 1);
        }
    }
}
