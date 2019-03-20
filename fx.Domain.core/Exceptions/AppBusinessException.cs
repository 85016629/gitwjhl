namespace fx.Domain.core
{
    using System;
    public class AppBusinessException : Exception
    {
        public AppBusinessException(){}
        public AppBusinessException(string errorCode, string errorMsg){
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; } 
    }
}
