#region Using ...
using System.Threading.Tasks;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISMSNotification
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="number"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		Task<bool> SendSMS(string number, string subject, string body);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="number"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		Task<bool> SendSMS(string[] number, string subject, string body);
	}
}
