﻿using KarveCar.Commands.Generic;
using KarveCar.Logic.Generic;
using KarveCar.Model.Generic;
using KarveCar.Utility;
using System.Globalization;
using System.Threading;
using System.Windows.Input;

namespace KarveCar.ViewModel.GenericViewModel
{
    public class SetLanguagesViewModel : GenericPropertyChanged
    {
        private SetLanguagesCommand setlanguagescommand;

        public SetLanguagesViewModel()
        {
            this.setlanguagescommand = new SetLanguagesCommand(this);
        }

        public ICommand SetLanguagesCommand
        {
            get
            {
                return setlanguagescommand;
            }
        }

        /// <summary>
        /// Cambia el idioma según el param recibido del xaml, y guardamos el idioma en app.config
        /// </summary>
        /// <param name="parameter"></param>
        public void SetLanguages(object parameter)
        {
            //Cambia el idioma según el param recibido del xaml
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(parameter.ToString());
            ChangeLanguageLogic.ChangeCulture(Thread.CurrentThread.CurrentUICulture);

            // Se guarda el idioma según el param recibido del xaml en app.config
            UserAndDefaultConfig.SetCurrentUserLanguage("Language", parameter.ToString());
        }
    }
}
