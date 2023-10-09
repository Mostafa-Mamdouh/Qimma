using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.GlobalComponents.Logger
{
	public static class LoggerManager
	{
        #region Constants

        /// <summary>
        /// The short name to identify the current project
        /// </summary>
        const string _SHORT_NAME = "QK";
        const string _DIRECTORY = @"c:\log_QKServices\{0}Log";
        #endregion

        #region Public Methods
        /// <summary>
        /// Log exception in Txt file.
        /// </summary>
        /// <param name="exception">The exception object</param>
        public static void Log(Exception exception)
        {
            var filePath = "";
            try
            {
                var directory = string.Format(_DIRECTORY, _SHORT_NAME);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentUICulture = Thread.CurrentThread.CurrentUICulture;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");



                filePath = string.Format(@"{0}\{1}-{2}.txt", directory, _SHORT_NAME, DateTime.Now.ToString("yyyy-MM-dd"));
                Thread.CurrentThread.CurrentCulture = currentCulture;
                Thread.CurrentThread.CurrentUICulture = currentUICulture;

                if (!File.Exists(filePath))
                {
                    string content = exception.ToString() + "\n" + DateTime.Now.ToLongTimeString();
                    content += "\n" + "====================================================================";
                    File.WriteAllText(filePath, content);
                }

                var exceptionContent = exception.ToString() + "\n" + DateTime.Now.ToLongTimeString();
                exceptionContent += "\n" + "====================================================================";
                try
                {
                    exceptionContent += "\n" + exception.InnerException.ToString();
                }
                catch (Exception)
                {

                }
                var allText = File.ReadAllLines(filePath).ToList();
                allText.Insert(0, exceptionContent);
                File.WriteAllLines(filePath, allText.ToArray());
            }
            catch
            {
                /* no need to implement it */
            }
            finally
            {

            }
        }
        public static void Log(string text)
        {
            var filePath = "";
            try
            {
                var directory = string.Format(_DIRECTORY, _SHORT_NAME);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentUICulture = Thread.CurrentThread.CurrentUICulture;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");



                filePath = string.Format(@"{0}\{1}-{2}.txt", directory, _SHORT_NAME, DateTime.Now.ToString("yyyy-MM-dd"));
                Thread.CurrentThread.CurrentCulture = currentCulture;
                Thread.CurrentThread.CurrentUICulture = currentUICulture;

                if (!File.Exists(filePath))
                {
                    string content = text + "\n" + DateTime.Now.ToLongTimeString();
                    content += "\n" + "====================================================================";
                    File.WriteAllText(filePath, content);
                }

                var exceptionContent = text + "\n" + DateTime.Now.ToLongTimeString();
                exceptionContent += "\n" + "====================================================================";

                var allText = File.ReadAllLines(filePath).ToList();
                allText.Insert(0, exceptionContent);
                File.WriteAllLines(filePath, allText.ToArray());
            }
            catch (Exception ex)
            {

                LoggerManager.Log(ex);

            }
        }
        
        #endregion
    }
}
