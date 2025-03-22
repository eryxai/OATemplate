#region Using ...
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMailNotification
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<bool> SendMail(string to, string cc, string bcc, string subject, string body);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<bool> SendMail(List<string> to, List<string> cc, List<string> bcc, string subject, string body);
    }
}
