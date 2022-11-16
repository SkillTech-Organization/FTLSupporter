using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLApiService
{
    public class FTLApiServiceClientBase
    {
        private readonly IConfiguration _configuration;

        protected string BaseUrl;

        public FTLApiServiceClientBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
