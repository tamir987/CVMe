using CVMe.Common.Settings.Application;
using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace CVMe.Services.CV
{
    public interface ICVGeneratorService
    {
        CVGeneratorResponse GenerateCV(CVGeneratorRequest request);
    }

    public class CVGeneratorService : ICVGeneratorService
    {
        private readonly IApplicationSettings _applicationSettings;

        public CVGeneratorService(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public CVGeneratorResponse GenerateCV(CVGeneratorRequest request)
        {
            var cvGeneratorOutputFolderRootPath = _applicationSettings.CVGeneratorOutputFolderRootPath;
            var cvGeneratorOutputFolderPath = cvGeneratorOutputFolderRootPath +
                "\\" + DateTime.Now.ToString("_dd_MM_yyyy");
            var outputDocumentPath = cvGeneratorOutputFolderPath + "\\" + request.Name;
            var templateDocumentPath = cvGeneratorOutputFolderRootPath +
                "\\" + _applicationSettings.CVTemplatesFolderFolderName + "\\" + "TemplateName";

            System.IO.File.Copy(templateDocumentPath, outputDocumentPath, true);


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
