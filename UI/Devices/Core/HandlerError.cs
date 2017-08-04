namespace Devices.Core
{
    public static class HandlerError
    {
        public static void ClearErrors()
        {
            ErrorOccurred = false;
            ErrorCode = 0;
            ErrorMsg = "";
        }
        public static bool ErrorOccurred { get; set; }

        public static int ErrorCode { get; set; }

        public static string ErrorMsg { get; set; }
       
    }
}
