﻿using KarveCommon.Services;
using System;
using System.Windows.Input;

namespace KarveCommon.Command
{
    /// <summary>
    ///  AbstractCommand is the generic view model command.
    /// </summary>
    public abstract class AbstractCommand: ICommand
    {

        /// <summary>
        /// this is relared to ICommand
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        ///  Execute of the command
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);

        /// <summary>
	/// Vetoes the undo
        /// </summary>
        /// <returns></returns>
        public virtual bool UnExecute()
        {
            // this vetoes the undo.
            return true;
        }
    }
}
