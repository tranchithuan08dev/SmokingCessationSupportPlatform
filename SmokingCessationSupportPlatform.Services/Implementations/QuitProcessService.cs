using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories.Implementations;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services.Implementations
{
    public class QuitProcessService : IQuitProcessService
    {
        private readonly QuitProcessRepository _quitProcess;
        public QuitProcessService(QuitProcessRepository quitProcessRepository)
        {
            _quitProcess = quitProcessRepository; 
        }


        public void CreateQuitProcess(QuitProgress quitProcess)
        {
          _quitProcess.CreateQuitProcess(quitProcess);
        }
    }
}
