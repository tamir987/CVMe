using CVMe.Common.Enums;
using CVMe.Common.Helpers;
using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.CV;
using CVMe.Services.ResponseBuilder;
using System;
using System.Web.Http;

namespace CVMe.Controllers
{
    public class CVController : ApiController
    {
        private readonly ICVGeneratorService _cvGeneratorService;
        private readonly ILoggerHelper _loggerHelper;

        public CVController(ICVGeneratorService cvGeneratorService, ILoggerHelper loggerHelper)
        {
            _cvGeneratorService = cvGeneratorService;
            _loggerHelper = loggerHelper;
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
                    _loggerHelper.LogObject("CVController - GenerateCV : Couldn't generate cv.", LoggerOption.Error);
                    return UnsuccessfulResponseBuilder.BuildUnsuccessfulResponse<CVResponse>();
                }
                return new CVResponse { IsSuccess = true };

            }
            catch(Exception ex)
            {
                _loggerHelper.LogObject("CVController - GenerateCV: " + ex.Message, LoggerOption.Error);
                return UnsuccessfulResponseBuilder.BuildUnsuccessfulResponse<CVResponse>();
            }
        }
    }
}
