#region Using ...
#endregion

/*
 
 
 */

/*
 
 
 */
namespace TemplateService.Common.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ErrorCode
	{
		NotFound = 1,
		CodeAlreadyExist = 3,
        MobileVersionAlreadyExit=4,
		EmailAlreadyExist = 5,
		PhoneAlreadyExist = 6,
        EmptyList = 7,
		UserNameAlreadyExist = 8,
        AccountNumberAlreadyExist = 9,
        LocationNotExist = 15,
		PasswordIncorrect = 17,
		DataDuplicate = 20,
		MaxYear=23,
		MaximumData = 25,
        WrongEquation =35,
		MaximumBannerInPTemplate= 50,
		InActiveUser = 55,
		InvalidOperationException=70,
        SerialDuplicate = 71,
        DataLicense = 72,
		InvalidDate = 73,
		SerialNotFound = 74,
		DraftExist = 75,
    }
}
