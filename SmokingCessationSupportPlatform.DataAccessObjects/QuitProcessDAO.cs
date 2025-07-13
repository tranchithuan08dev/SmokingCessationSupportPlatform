using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
      public class QuitProcessDAO
    {
        // Ghi lại quá trình bỏ thuốc lá của người dùng 
        public static void CreateQuitProcess(QuitProgress quitProgress)
        {
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                context.QuitProgresses.Add(quitProgress);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating quit process: {ex.Message}");
            }
        }


        public static List<QuitProgress> GetQuitProgresses(int UserID)
        {
            var listQuitProgresses = new List<QuitProgress>();

            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                listQuitProgresses = context.QuitProgresses
                                      .Where(qp => qp.UserId == UserID)
                                      .ToList();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listQuitProgresses;
        }

    }
}
