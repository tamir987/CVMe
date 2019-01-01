using CVMe.Common.Settings.Application;
using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.FilePaths;
using CVMe.Services.ResponseBuilder;
using CVMe.Services.Templates;
using CVMe.Services.Xml;
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
        CVGeneratorResponse GenerateCV(CVRequest request);
    }

    public class CVGeneratorService : ICVGeneratorService
    {
        private readonly IApplicationSettings _applicationSettings;
        private readonly IFilePathService _filePathService;
        private readonly IXmlGeneratorService _xmlGeneratorService;
        private readonly ITemplateService _templateService;

        public CVGeneratorService(IApplicationSettings applicationSettings, IFilePathService filePathService,
            IXmlGeneratorService xmlGeneratorService, ITemplateService templateService)
        {
            _applicationSettings = applicationSettings;
            _filePathService = filePathService;
            _xmlGeneratorService = xmlGeneratorService;
            _templateService = templateService;
        }

        public CVGeneratorResponse GenerateCV(CVRequest request)
        {
            var xmlGeneratorRequest = new XmlGeneratorRequest
            {
                Name = request.Name
            };

            var xmlResult = _xmlGeneratorService.GenerateXml(xmlGeneratorRequest);
            if (!xmlResult.IsSuccess)
                return UnsuccessfulResponseBuilder.BuildUnsuccessfulResponse<CVGeneratorResponse>();

            var templateRequest = new TemplateRequest
            {
                Id = request.TemplateId
            };

            var templateResult = _templateService.GetTemplateById(templateRequest);
            if (!templateResult.IsSuccess)
                return UnsuccessfulResponseBuilder.BuildUnsuccessfulResponse<CVGeneratorResponse>();

            var cvGeneratorRequest = new CVGeneratorRequest
            {
                Name = request.Name,
                TemplateName = templateResult.Template.TemplateName,
                Xml = xmlResult.Xml
            };

            return BuildCV(cvGeneratorRequest);
        }

        private CVGeneratorResponse BuildCV(CVGeneratorRequest request)
        {
            try
            {
                var outputDocumentPath = _filePathService.CVOutputPath(request.Name);
                var templateFilePath = _filePathService.TemplateFilePath(request.TemplateName);
                var templateDocFilePath = templateFilePath + _filePathService.DocFileName;

                File.Copy(templateDocFilePath, outputDocumentPath, true);

                using (WordprocessingDocument output = WordprocessingDocument.Open(outputDocumentPath, true))
                {
                    var updatedBodyContent = new Body(request.Xml);
                    output.MainDocumentPart.Document.Body = updatedBodyContent;
                    output.MainDocumentPart.Document.Save();
                }

                return new CVGeneratorResponse { IsSuccess = true, CVFilePath = outputDocumentPath };
            }
            catch(Exception ex)
            {
                return new CVGeneratorResponse { IsSuccess = false };
            }
            
        }
    }
}
