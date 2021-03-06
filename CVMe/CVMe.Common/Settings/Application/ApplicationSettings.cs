﻿using CVMe.Common.Enums;
using CVMe.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Common.Settings.Application
{
    public interface IApplicationSettings
    {
        SystemEnvironment ThisEnvironment { get; }
        string CVGeneratorOutputFolderRootPath { get; }
        string CVTemplatesFolderFolderName { get; }
    }

    public class ApplicationSettings : IApplicationSettings
    {
        private readonly IConfigurationHelper _configurationHelper;

        public ApplicationSettings(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public SystemEnvironment ThisEnvironment => EnumUtils.Parse<SystemEnvironment>(_configurationHelper.GetString("ThisEnvironment"));

        public string CVGeneratorOutputFolderRootPath => _configurationHelper.GetString("CVGeneratorOutputFolderRootPath");

        public string CVTemplatesFolderFolderName => _configurationHelper.GetString("CVTemplatesFolderFolderName");
    }
}
