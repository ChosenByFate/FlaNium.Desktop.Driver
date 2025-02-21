﻿namespace FlaNium.Desktop.Driver
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FlaNium.Desktop.Driver.CommandExecutors;
    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class CommandExecutorDispatchTable
    {
        #region Fields

        private Dictionary<string, Type> commandExecutorsRepository;

        #endregion

        #region Constructors and Destructors

        public CommandExecutorDispatchTable()
        {
            this.ConstructDispatcherTable();
        }

        #endregion

        #region Public Methods and Operators

        public CommandExecutorBase GetExecutor(string commandName)
        {
            Type executorType;
            if (this.commandExecutorsRepository.TryGetValue(commandName, out executorType))
            {
            }
            else
            {
                executorType = typeof(NotImplementedExecutor);
            }

            return (CommandExecutorBase)Activator.CreateInstance(executorType);
        }

        #endregion

        #region Methods

        private void ConstructDispatcherTable()
        {
            this.commandExecutorsRepository = new Dictionary<string, Type>();

            const string ExecutorsNamespace = "FlaNium.Desktop.Driver.CommandExecutors";

            var q =
                (from t in Assembly.GetExecutingAssembly().GetTypes()
                 where t.IsClass && t.Namespace.Contains(ExecutorsNamespace)
                 select t).ToArray();

            var fields = typeof(DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var localField = field;
                var executor = q.FirstOrDefault(x => x.Name.Equals(localField.Name + "Executor"));
                if (executor != null)
                {
                    this.commandExecutorsRepository.Add(localField.GetValue(null).ToString(), executor);
                }
            }
        }

        #endregion
    }
}
