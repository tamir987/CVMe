using CVMe.DataObjects.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Services.ResponseBuilder
{
    public class UnsuccessfulResponseBuilder
    {
        public static T BuildUnsuccessfulResponse<T>() where T : ServiceResponse
        {
            //ToDo : log
            var response = (T)Activator.CreateInstance(typeof(T));
            response.IsSuccess = false;
            return response;
        }
    }
}
