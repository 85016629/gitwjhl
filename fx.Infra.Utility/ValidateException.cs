using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Utility
{
    public class ValidateFileNotExistException : Exception
    {
        public ValidateFileNotExistException(string validateFileName)
        {
            ValidateFileName = validateFileName;
        }

        public string ValidateFileName { get; private set; }

        public override string Message
        {
            get { return "参数验证文件" + ValidateFileName + "不存在，对此交易不做验证"; }
        }
    }

    public sealed class ValidateUnkownException : Exception
    {
        public string ErrMsg { get; set; }
        public Exception RealException { get; set; }
        public ValidateUnkownException(Exception ex)
        {
            ErrMsg = ex.Message;
            RealException = ex;
        }
    }

    public sealed class ValidateAttrNotDefinedException : Exception
    {
        private readonly string _attrName;
        public ValidateAttrNotDefinedException(string attrName)
        {
            _attrName = attrName;
        }

        public string ErrMsg
        {
            get { return _attrName + "未定义"; }
        }
    }
}
