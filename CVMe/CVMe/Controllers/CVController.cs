using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.CV;
using CVMe.Services.ResponseBuilder;
using CVMe.Services.Templates;
using CVMe.Services.Xml;
using System;
using System.Web.Http;

namespace CVMe.Controllers
{
    public class CVController : ApiController
    {
        private readonly ICVGeneratorService _cvGeneratorService;

        public CVController(ICVGeneratorService cvGeneratorService )
        {
            _cvGeneratorService = cvGeneratorService;
        }

        public CVResponse GenerateCV ([FromUri]CVRequest request)
        {
            try
            {
                //ToDo : validate request
                var result = _cvGeneratorService.GenerateCV(request);
                if(!result.IsSuccess)
                {
                    //logger
                    return UnsuccessfulResponseBuilder.BuildUnsuccessfulResponse<CVResponse>();
                }
                return new CVResponse { IsSuccess = true };

            }
            catch(Exception ex)
            {
                return UnsuccessfulResponseBuilder.BuildUnsuccessfulResponse<CVResponse>();
            }
        }
    }
}
