using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLApiTester.Services
{
    internal class ApiTesterService : IApiTesterService
    {
        public async Task DoWork(CancellationToken cancellationToken = default)
        {
            var args = Environment.GetCommandLineArgs();
            ;
        }
    }
}
