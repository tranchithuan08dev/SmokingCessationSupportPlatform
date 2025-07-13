using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories.Interfaces
{
    public interface IQuitProcessRepository
    {
       void  CreateQuitProcess(QuitProgress quitProgress);

        List<QuitProgress> GetListQuitProgresses(int userId);
    }
}
