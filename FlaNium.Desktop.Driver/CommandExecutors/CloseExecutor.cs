﻿using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!this.Automator.ActualCapabilities.DebugConnectToRunningApp)
            {
                DriverManager.CloseDriver(this.Automator.ActualCapabilities.App.StartsWith("#"));

                this.Automator.ElementsRegistry.Clear();
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
