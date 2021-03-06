﻿using CVMe.Common.Settings.Application;
using System;
using System.IO;

namespace CVMe.Services.FilePaths
{
    public interface IFilePathService
    {
        string DocFileName { get; }
        string XsltFileName { get; }
        string XmlFileName { get; }
        string CVOutputPath(string name);
        string TemplateFilePath(string templateName);
    }
    public class FilePathService : IFilePathService
    {
        private readonly IApplicationSettings _applicationSettings;

        public FilePathService(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public string DocFileName => ".doc";

        public string XsltFileName => "xslt";

        public string XmlFileName => "xml";

        public string CVOutputPath (string name)
        {
            return Path.Combine(
                _applicationSettings.CVGeneratorOutputFolderRootPath,
                DateTime.Now.ToString("_dd_MM_yyyy"),
                name);
        }

        public string TemplateFilePath(string templateName)
        {
            return Path.Combine(
                _applicationSettings.CVGeneratorOutputFolderRootPath,
                _applicationSettings.CVTemplatesFolderFolderName,
                templateName);
        }
    }
}
