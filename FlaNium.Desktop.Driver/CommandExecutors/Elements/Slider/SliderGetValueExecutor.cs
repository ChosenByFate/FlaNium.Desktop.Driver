﻿
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.Common;

    class SliderGetValueExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            Slider slider = element.FlaUIElement.AsSlider();

            double value = slider.Value;

            return this.JsonResponse(ResponseStatus.Success, value.ToString());
        }

        #endregion
    }
}
