using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.CV;
using CVMe.Services.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CVMe.Controllers
{
    public class CVController : ApiController
    {
        private readonly IXmlGeneratorService _xmlGeneratorService;
        private readonly ICVGeneratorService _cvGeneratorService;

        public CVController(IXmlGeneratorService xmlGeneratorService, ICVGeneratorService cvGeneratorService)
        {
            _xmlGeneratorService = xmlGeneratorService;
            _cvGeneratorService = cvGeneratorService;
        }

        public CVResponse GenerateCV ([FromUri]CVRequest request)
        {
            var xmlGeneratorRequest = new XmlGeneratorRequest
            {
                Name = request.Name
            };
            var xmlResult = _xmlGeneratorService.GenerateXml(xmlGeneratorRequest);

            if(!xmlResult.IsSuccess) return new CVResponse { IsSuccess = false };

            var cvGeneratorRequest = new CVGeneratorRequest
            {

            };

            var cvResult = _cvGeneratorService.GenerateCV(cvGeneratorRequest);

            if (!cvResult.IsSuccess) return new CVResponse { IsSuccess = false };

            return new CVResponse { IsSuccess = true };
        }
    }
}
