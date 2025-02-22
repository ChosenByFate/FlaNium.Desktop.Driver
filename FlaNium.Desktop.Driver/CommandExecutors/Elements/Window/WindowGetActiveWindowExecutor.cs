﻿
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window
{
    class WindowGetActiveWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {            
            var activeWindow = DriverManager.GetActiveWindow();

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(activeWindow));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
