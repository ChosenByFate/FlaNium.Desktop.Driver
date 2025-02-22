﻿namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class ElementEqualsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var otherRegisteredKey = this.ExecutedCommand.Parameters["other"].ToString();

            return this.JsonResponse(ResponseStatus.Success, registeredKey == otherRegisteredKey);
        }

        #endregion
    }
}
