using CVMe.Common.Settings.Application;
using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.FilePaths;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace CVMe.Services.CV
{
    public interface ICVGeneratorService
    {
        CVGeneratorResponse GenerateCV(CVGeneratorRequest request);
    }

    public class CVGeneratorService : ICVGeneratorService
    {
        private readonly IApplicationSettings _applicationSettings;
        private readonly IFilePathService _filePathService;

        public CVGeneratorService(IApplicationSettings applicationSettings, IFilePathService filePathService)
        {
            _applicationSettings = applicationSettings;
            _filePathService = filePathService;
        }

        public CVGeneratorResponse GenerateCV(CVGeneratorRequest request)
        {
            var outputDocumentPath = _filePathService.CVOutputPath(request.Name);
            var templateFilePath = cvGeneratorOutputFolderRootPath +
                "\\" + _applicationSettings.CVTemplatesFolderFolderName + "\\" + "TemplateName";
            var templateDocFilePath = templateFilePath + ".doc";
            var templateXsltFilePath = templateFilePath + ".xslt";
            var templateXmtFilePath = templateFilePath + ".xml";

            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter);

            //Create the Xsl Transformation object.
            var transform = new XslCompiledTransform();
            transform.Load(templateXsltFilePath);

            //Transform the xml data into Open XML 2.0 Wordprocessing format.
            transform.Transform(templateXmtFilePath, xmlWriter);

            //Create an Xml Document of the new content.
            XmlDocument newWordContent = new XmlDocument();
            newWordContent.LoadXml(stringWriter.ToString());

            File.Copy(templateDocFilePath, outputDocumentPath, true);


            using (WordprocessingDocument output = WordprocessingDocument.Open(outputDocumentPath, true))
            {
                var updatedBodyContent = new Body(request.Xml);
                output.MainDocumentPart.Document.Body = updatedBodyContent;
                output.MainDocumentPart.Document.Save();
            }

            return new CVGeneratorResponse { IsSuccess = false };
        }
    }
}
