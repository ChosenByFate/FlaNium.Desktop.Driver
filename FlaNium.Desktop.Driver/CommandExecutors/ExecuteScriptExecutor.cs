﻿namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Newtonsoft.Json.Linq;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    #endregion

    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        #region Constants

        internal const string HelpArgumentsErrorMsg = "Arguments error.";

        internal const string HelpUnknownScriptMsg = "Unknown script command '{0} {1}'.";

        #endregion

        #region Methods

        protected override string DoImpl()
        {
            var script = this.ExecutedCommand.Parameters["script"].ToString();

            var prefix = string.Empty;
            string command;

            var index = script.IndexOf(':');
            if (index == -1)
            {
                command = script;
            }
            else
            {
                prefix = script.Substring(0, index);
                command = script.Substring(++index).Trim();
            }

            switch (prefix)
            {
                case "input":
                    this.ExecuteInputScript(command);
                    break;
                case "automation":
                    this.ExecuteAutomationScript(command);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, prefix, command);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            return this.JsonResponse();
        }

        private void ExecuteAutomationScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

            switch (command)
            {
                case "ValuePattern.SetValue":
                    this.ValuePatternSetValue(element, args);
                    break;
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "automation:", command);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
        }

        private void ExecuteInputScript(string command)
        {
            var args = (JArray)this.ExecutedCommand.Parameters["args"];
            var elementId = args[0]["ELEMENT"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(elementId);
            element.Click();
            switch (command)
            {
                case "ctrl_click":
                    throw new NotImplementedException("not implemented yet");
                   
                case "brc_click":
                  
                    throw new AutomationException(string.Format("Unknown script command '{0} {1}'.", (object)"input:", (object)command), ResponseStatus.JavaScriptError);
                    
                default:
                    var msg = string.Format(HelpUnknownScriptMsg, "input:", command);
                    throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }
            
        }

        private void ValuePatternSetValue(FlaUIDriverElement element, IEnumerable<JToken> args)
        {
            var value = args.ElementAtOrDefault(1);
            if (value == null)
            {
                var msg = HelpArgumentsErrorMsg;
                throw new AutomationException(msg, ResponseStatus.JavaScriptError);
            }

            element.FlaUIElement.Patterns.Value.Pattern.SetValue(value.ToString());
        }

        #endregion
    }
}
