﻿using NBi.Core.Injection;
using NBi.Core.Sequence.Resolver;
using NBi.Core.Variable;
using NBi.Xml;
using NBi.Xml.Settings;
using NBi.Xml.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.NUnit.Builder.Helper
{
    public class InstanceArgsBuilder
    {
        private readonly ServiceLocator serviceLocator;
        private readonly IDictionary<string, ITestVariable> globalVariables;

        private bool isSetup = false;
        private object obj = null;
        private SettingsXml settings = SettingsXml.Empty;
        private IInstanceArgs args = null;

        public InstanceArgsBuilder(ServiceLocator serviceLocator, IDictionary<string, ITestVariable> globalVariables)
        {
            this.serviceLocator = serviceLocator;
            this.globalVariables = globalVariables;
        }

        public void Setup(SettingsXml settings)
        {
            this.settings = settings;
        }

        public void Setup(InstanceXml definition)
        {
            obj = definition;
            isSetup = true;
        }

        public void Build()
        {
            if (!isSetup)
                throw new InvalidOperationException();

            if ((obj as InstanceXml).Variable != null)
            {
                var variable = (obj as InstanceXml).Variable;

                var argsBuilder = new SequenceResolverArgsBuilder(serviceLocator);
                argsBuilder.Setup(settings);
                argsBuilder.Setup(globalVariables);
                argsBuilder.Setup(variable.SentinelLoop);
                argsBuilder.Setup(variable.Type);
                argsBuilder.Build();
                var factory = new SequenceResolverFactory(serviceLocator);

                args = new SingleVariableInstanceArgs()
                {
                    Name = variable.Name,
                    Resolver = factory.Instantiate<object>(argsBuilder.GetArgs())
                };
            }
        }

        public IInstanceArgs GetArgs() => args ?? throw new InvalidOperationException();
    }
}
