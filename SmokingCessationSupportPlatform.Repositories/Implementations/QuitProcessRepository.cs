using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories.Implementations
{
    public class QuitProcessRepository : IQuitProcessRepository
    {
      
        // Ghi lại quá trình bỏ thuốc lá của người dùng
        public void CreateQuitProcess(QuitProgress quitProgress)
        {
           QuitProcessDAO.CreateQuitProcess(quitProgress);
        }

        public List<QuitProgress> GetListQuitProgresses(int userId)
       => QuitProcessDAO.GetQuitProgresses(userId);
    }
}
