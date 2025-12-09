using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChef.Services
{
    public class Logger
    {
        private static readonly string logPath = Path.Combine(AppContext.BaseDirectory, $"logs/{DateTime.Now:yyyy-MM-dd HH_mm_ss}-consolelog.log");

        public static void WriteLog(string message)
        {
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(logPath, logMessage + Environment.NewLine);
                Console.WriteLine(logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}
