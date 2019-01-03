using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace fx.Infra.Utility
{
    public class XmlParameterValidator
    {
        private readonly Dictionary<string, string> _commandParams;
        private readonly XmlDocument _xmlDoc;
        private readonly string _command;

        public XmlParameterValidator(string command, Dictionary<string, string> commandParams)
        {
            _command = command;
            try
            {
                var validateXmlFilePath = AppDomain.CurrentDomain.BaseDirectory + "/ValidateXml/" + command + ".xml";
                _xmlDoc = new XmlDocument();
                _xmlDoc.Load(validateXmlFilePath);
            }
            catch
            {
                throw new ValidateFileNotExistException(command + ".xml");
            }
            _commandParams = commandParams;

        }

        /// <summary>
        /// 以Xml文件验证字段循环验证。
        /// </summary>
        /// <returns></returns>
        public string Validate4Column()
        {
            var result = string.Empty;
            if (_xmlDoc.FirstChild == null)
                return string.Empty;

            var root = _xmlDoc.SelectSingleNode("/validateFields");
            if (root == null || root.ChildNodes.Count == 0)
                return string.Empty;

            foreach (var node in root.ChildNodes)
            {
                var xNode = (XmlNode)node;
                if (xNode.Attributes == null)
                    break;
                if (xNode.Attributes["desc"] == null)
                {
                    throw new ValidateAttrNotDefinedException("desc");
                }

                if (!_commandParams.ContainsKey(xNode.Name))
                {
                    return _command + "命令参数中[" + xNode.Attributes["desc"].Value + "]字段缺失";
                }
                var columnNodeDesc = xNode.Attributes["desc"].Value;
                result = ValidateEmpty(_commandParams[xNode.Name], xNode);
                if (result != string.Empty)
                {
                    result = columnNodeDesc + result;
                    break;
                }
                result = ValidateStringMinLenth(_commandParams[xNode.Name], xNode);
                if (result != string.Empty)
                {
                    result = columnNodeDesc + result;
                    break;
                }
                result = ValidateStringMaxLenth(_commandParams[xNode.Name], xNode);
                if (result != string.Empty)
                {
                    result = columnNodeDesc + result;
                    break;
                }
                result = ValidateStringFixedLength(_commandParams[xNode.Name], xNode);
                if (result != string.Empty)
                {
                    result = columnNodeDesc + result;
                    break;
                }
                result = ValidateDataType(_commandParams[xNode.Name], xNode);
                if (result != string.Empty)
                {
                    result = columnNodeDesc + result;
                    break;
                }
            }
            return result;
        }


        /// <summary>
        /// 以录入的参数循环验证。验证验证似乎不太妥当，验证次数较多，效率较低
        /// </summary>
        /// <returns></returns>
        public string Validate4Param()
        {
            var result = string.Empty;
            try
            {
                foreach (var kv in _commandParams)
                {
                    var columnXmlNode = _xmlDoc.SelectSingleNode("/validateFields/" + kv.Key);

                    if (columnXmlNode == null)
                    {
                        //result = "缺少" + kv.Key + "字段";
                        //break;
                        continue;
                    }

                    //判断是否为空
                    if (columnXmlNode.Attributes != null)
                    {
                        var columnNodeDesc = columnXmlNode.Attributes["desc"].Value;
                        result = ValidateEmpty(kv.Value, columnXmlNode);
                        if (result != string.Empty)
                        {
                            result = columnNodeDesc + result;
                            break;
                        }
                        result = ValidateStringMinLenth(kv.Value, columnXmlNode);
                        if (result != string.Empty)
                        {
                            result = columnNodeDesc + result;
                            break;
                        }
                        result = ValidateStringMaxLenth(kv.Value, columnXmlNode);
                        if (result != string.Empty)
                        {
                            result = columnNodeDesc + result;
                            break;
                        }
                        result = ValidateStringFixedLength(kv.Value, columnXmlNode);
                        if (result != string.Empty)
                        {
                            result = columnNodeDesc + result;
                            break;
                        }
                        result = ValidateDataType(kv.Value, columnXmlNode);
                        if (result != string.Empty)
                        {
                            result = columnNodeDesc + result;
                            break;
                        }
                    }
                    //验证长度
                }
            }
            catch (Exception ex)
            {
                throw new ValidateUnkownException(ex);
            }

            return result;
        }

        private string ValidateEmail(string val, XmlNode validNode)
        {
            return string.Empty;
        }
        private string ValidateMinValue(string val, XmlNode validNode)
        {
            if (validNode.Attributes == null)
                return string.Empty;

            if (validNode.Attributes["dataType"] == null)
                return string.Empty;

            var dataType = validNode.Attributes["dataType"].Value;
            if (dataType == "int")
            {
                int ir;
                var br = Int32.TryParse(val, out ir);
                if (!br)
                    return "必须为整数";

                if (ir > Convert.ToInt32(val))
                    return "值不能小于" + ir;
            }

            if (dataType == "decimal")
            {
                decimal d;
                var bd = decimal.TryParse(val, out d);
                if (!bd)
                    return "必须为数字";

                if (d > Convert.ToDecimal(val))
                    return "值不能小于" + d;
            }
            return string.Empty;
        }
        private string ValidateMaxValue(string val, XmlNode validNode)
        {
            return string.Empty;
        }

        private string ValidateDataType(string val, XmlNode validNode)
        {
            if (validNode.Attributes == null)
                return string.Empty;

            if (validNode.Attributes["dataType"] == null)
                return string.Empty;

            var dataType = validNode.Attributes["dataType"].Value;
            if (dataType == "int")
            {
                int ir;
                var br = Int32.TryParse(val, out ir);
                if (!br)
                    return "必须为整数";
            }
            if (dataType == "decimal")
            {
                decimal d;
                var bd = decimal.TryParse(val, out d);
                if (!bd)
                    return "必须为数字";
            }
            return string.Empty;
        }


        private string ValidateStringMinLenth(string val, XmlNode validNode)
        {

            if (validNode.Attributes == null)
                return string.Empty;

            if (validNode.Attributes["minLength"] == null)
                return string.Empty;

            var minLengthValue = validNode.Attributes["minLength"].Value;
            if (minLengthValue != null)
            {
                var iMinLength = Convert.ToInt32(minLengthValue);
                if (val.Length < iMinLength)
                {
                    return "长度不能小于" + minLengthValue;
                }
            }

            return string.Empty;
        }

        private string ValidateStringMaxLenth(string val, XmlNode validNode)
        {

            if (validNode.Attributes == null)
                return string.Empty;

            if (validNode.Attributes["maxLength"] == null)
                return string.Empty;


            var maxLengthValue = validNode.Attributes["maxLength"].Value;
            if (maxLengthValue != null)
            {
                var iMaxLength = Convert.ToInt32(maxLengthValue);
                if (val.Length > iMaxLength)
                {
                    return "长度不能大于" + maxLengthValue;
                }
            }

            return string.Empty;
        }

        private string ValidateStringFixedLength(string val, XmlNode validNode)
        {
            if (validNode.Attributes == null)
                return string.Empty;

            if (validNode.Attributes["fixedLength"] == null)
                return string.Empty;

            var fixedLengthValue = validNode.Attributes["fixedLength"].Value;
            if (fixedLengthValue != null)
            {
                var iFixedLength = Convert.ToInt32(fixedLengthValue);
                if (val.Length != iFixedLength)
                {
                    return "长度必须为" + fixedLengthValue + "位";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 验证是否可空。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="validNode"></param>
        /// <returns>返回空字符串表示不验证或者通过验证。</returns>
        private static string ValidateEmpty(string val, XmlNode validNode)
        {
            if (validNode.Attributes == null)
                return string.Empty;

            if (validNode.Attributes["isNullable"] == null)
                return string.Empty;

            var isNullableAttr = validNode.Attributes["isNullable"];
            if (isNullableAttr == null)
                return string.Empty;

            var isNullableValue = isNullableAttr.Value;

            if (isNullableValue == "False" && val == string.Empty)
                return "值为空";

            return string.Empty;
        }
    }
}
