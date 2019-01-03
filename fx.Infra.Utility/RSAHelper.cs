using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace fx.Infra.Utility

{        /// <summary>
         /// RSA帮助类，平台公钥，商户私钥已确定。
         /// </summary>
    public class RSAHelper
    {
        /// <summary>
        /// 利用平台商户公钥加密。
        /// </summary>
        /// <param name="encData"></param>
        public static string EncryptByPubKey(string encData)
        {
            string rStr = string.Empty;

            char[] charArrs = encData.ToCharArray();

            byte[] bEncData = Encoding.GetEncoding(CommonConst.CHARSET).GetBytes(charArrs);

            var RSA = new RSACryptoServiceProvider();
            var bytes_Public_Key = Convert.FromBase64String(CommonConst.GATEWAY_PUBLICKEY);

            try
            {
                var bytes_Plain_Text = RSA.Encrypt(bEncData, false);
                rStr = Convert.ToBase64String(bytes_Plain_Text);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }

            return rStr;
        }

        public static string EncryptByPriKey(string encData)
        {
            string rStr = string.Empty;

            char[] charArrs = encData.ToCharArray();

            byte[] bEncData = Encoding.GetEncoding(CommonConst.CHARSET).GetBytes(charArrs);

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            byte[] bytes_Public_Key = Convert.FromBase64String(CommonConst.PRIVATEKEY);

            try
            {
                byte[] bytes_Plain_Text = RSA.Encrypt(bEncData, false);
                rStr = Convert.ToBase64String(bytes_Plain_Text);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }

            return rStr;
        }

        /// <summary>
        /// 利用商户的私钥进行签名。
        /// </summary>
        /// <param name="signData"></param>
        /// <returns></returns>
        //public static string SignData(string signData)
        //{
        //    string rStr = string.Empty;
        //    byte[] signedHash;

        //    char[] charArrs = signData.ToCharArray();

        //    byte[] bSignData = Encoding.GetEncoding(CommonConst.CHARSET).GetBytes(EncryptByPriKey(signData));

        //    SHA256 sha256 = SHA256.Create();

        //     byte[] bHash = sha256.ComputeHash(bSignData);
        //     try {
        //         System.Security.Cryptography.RSA rsa = new RSACryptoServiceProvider();

        //         //将pem私钥初始化成XML字符串格式的私钥
        //         //string privatekeyConent = privateKeyContent("c:/rsa_private_key_pkcs8.pem");
        //         //从XML私钥字符初始化RSA
        //         rsa.FromXmlString(ConvertToXmlPrivateKey(CommonConst.PRIVATEKEY));

        //         RSAPKCS1SignatureFormatter signProvider = new RSAPKCS1SignatureFormatter(rsa);

        //         signProvider.SetHashAlgorithm("SHA256");
        //         signedHash = signProvider.CreateSignature(bHash);
        //     }
        //     catch(Exception ex){
        //         Console.WriteLine(ex.StackTrace);
        //        throw ex;
        //    }
        //    return Convert.ToBase64String(signedHash);
        //}


        /// <summary>
        /// 把java的私钥转换成.net的xml格式
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="privateJavaKey"></param>
        /// <returns></returns>
        //private  static string ConvertToXmlPrivateKey( string privateJavaKey)
        //{
        //    RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateJavaKey));
        //    string xmlPrivateKey = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
        //    Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
        //    Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        //    return xmlPrivateKey;
        //}

        //private static string privateKeyContent(string filePath)
        //{

        //    string content = File.ReadAllText(filePath, Encoding.ASCII);//获取pem证书完整内容
        //    if (string.IsNullOrEmpty(content))
        //    {
        //        throw new ArgumentNullException("pemFileConent", "This arg cann't be empty.");
        //    }
        //    string privatekeyConent = content.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "").Replace("\n", "").Replace("\r", "");//去掉证书的头部和尾部
        //    return privatekeyConent;
        //}


        /// <summary>  
        /// 签名  
        /// </summary>  
        /// <param name="content">待签名字符串</param>  
        /// <param name="privateKey">私钥</param>  
        /// <param name="input_charset">编码格式</param>  
        /// <returns>签名后字符串</returns>  
        public static string Sign(string content, string privateKey, string input_charset)
        {
            byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(content);
            RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
            if (rsa != null)
            {
                SHA1 sh = new SHA1CryptoServiceProvider();
                byte[] signData = rsa.SignData(Data, sh);
                return Convert.ToBase64String(signData);
            }
            else
            {
                // Logger.wirte("DecodePemPrivateKey 返回 空值");
                return string.Empty;
            }
        }

        /// <summary>  
        /// 验签  
        /// </summary>  
        /// <param name="content">待验签字符串</param>  
        /// <param name="signedString">签名</param>  
        /// <param name="publicKey">公钥</param>  
        /// <param name="input_charset">编码格式</param>  
        /// <returns>true(通过)，false(不通过)</returns>  
        public static bool Verify(string content, string signedString, string publicKey, string input_charset)
        {
            bool result = false;
            byte[] signedBase64 = Convert.FromBase64String(signedString);
            byte[] orgin = Encoding.GetEncoding(input_charset).GetBytes(content);
            // RSAParameters paraPub = ConvertFromPublicKey(publicKey);
            RSACryptoServiceProvider rsaPub = DecodePemPublicKey(publicKey);
            // rsaPub.ImportParameters(paraPub);  
            SHA1 sh = new SHA1CryptoServiceProvider();
            result = rsaPub.VerifyData(orgin, sh, signedBase64);
            return result;
        }

        /// <summary>  
        /// 加密  
        /// </summary>  
        /// <param name="resData">需要加密的字符串</param>  
        /// <param name="publicKey">公钥</param>  
        /// <param name="input_charset">编码格式</param>  
        /// <returns>明文</returns>  
        public static string EncryptData(string resData, string publicKey, string input_charset)
        {
            byte[] DataToEncrypt = Encoding.GetEncoding(input_charset).GetBytes(resData);
            string result = Encrypt(DataToEncrypt, publicKey, input_charset);
            return result;
        }


        /// <summary>  
        /// 解密  
        /// </summary>  
        /// <param name="resData">加密字符串</param>  
        /// <param name="privateKey">私钥</param>  
        /// <param name="input_charset">编码格式</param>  
        /// <returns>明文</returns>  
        public static string DecryptData(string resData, string privateKey, string input_charset)
        {
            byte[] DataToDecrypt = Convert.FromBase64String(resData);
            string result = "";
            for (int j = 0; j < DataToDecrypt.Length / 128; j++)
            {
                byte[] buf = new byte[128];
                for (int i = 0; i < 128; i++)
                {

                    buf[i] = DataToDecrypt[i + 128 * j];
                }
                result += Decrypt(buf, privateKey, input_charset);
            }
            return result;
        }

        #region 内部方法

        private static string Encrypt(byte[] data, string publicKey, string input_charset)
        {
            var rsa = DecodePemPublicKey(publicKey);
            var sh = new SHA1CryptoServiceProvider();
            var result = rsa.Encrypt(data, false);

            return Convert.ToBase64String(result);
        }

        private static string Decrypt(byte[] data, string privateKey, string input_charset)
        {
            string result = "";
            var rsa = DecodePemPrivateKey(privateKey);
            var sh = new SHA1CryptoServiceProvider();
            var source = rsa.Decrypt(data, false);
            var asciiChars = new char[Encoding.GetEncoding(input_charset).GetCharCount(source, 0, source.Length)];
            Encoding.GetEncoding(input_charset).GetChars(source, 0, source.Length, asciiChars, 0);
            result = new string(asciiChars);
            //result = ASCIIEncoding.ASCII.GetString(source);  
            return result;
        }

        private static RSACryptoServiceProvider DecodePemPublicKey(String pemstr)
        {
            byte[] pkcs8publickkey;
            pkcs8publickkey = Convert.FromBase64String(pemstr);
            if (pkcs8publickkey != null)
            {
                var rsa = DecodeRSAPublicKey(pkcs8publickkey);
                return rsa;
            }
            else
                return null;
        }

        private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
        {
            byte[] pkcs8privatekey;
            pkcs8privatekey = Convert.FromBase64String(pemstr);
            if (pkcs8privatekey != null)
            {
                var rsa = DecodePrivateKeyInfo(pkcs8privatekey);
                return rsa;
            }
            else
            {
                // Logger.wirte(pemstr + "->FromBase64String 为空");
                return null;
            }
        }

        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            var seq = new byte[15];

            var mem = new MemoryStream(pkcs8);
            var lenstream = (int)mem.Length;
            var binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;

            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes  
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);        //read the Sequence OID  
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct  
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)    //expect an Octet string  
                    return null;

                bt = binr.ReadByte();        //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count  
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key  

                var rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                var rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch (Exception ex)
            {
                //  Logger.wirte(ex);
                return null;
            }

            finally { binr.Close(); }

        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        private static RSACryptoServiceProvider DecodeRSAPublicKey(byte[] publickey)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"  
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------  
            var mem = new MemoryStream(publickey);
            var binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID  
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct  
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next  
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)  
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus  
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes  
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order  
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it  
                    binr.ReadByte();    //skip this null byte  
                    modsize -= 1;   //reduce modulus buffer size by 1  
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes  

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data  
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)  
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----  
                var RSA = new RSACryptoServiceProvider();
                var RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }

        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------  
            var mem = new MemoryStream(privkey);
            var binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes  
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)    //version number  
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----  
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----  


                var cSAParams = new CspParameters
                {
                    Flags = CspProviderFlags.UseMachineKeyStore
                };
                System.Security.Cryptography.RSACryptoServiceProvider provider = new RSACryptoServiceProvider(1024, cSAParams);


                var RSAparams = new RSAParameters
                {
                    Modulus = MODULUS,
                    Exponent = E,
                    D = D,
                    P = P,
                    Q = Q,
                    DP = DP,
                    DQ = DQ,
                    InverseQ = IQ
                };
                provider.ImportParameters(RSAparams);
                return provider;
            }
            catch (Exception ex)
            {
                //Logger.wirte(ex);
                return null;
            }
            finally { binr.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)        //expect integer  
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte  
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();    // data size in next 2 bytes  
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;        // we already have the data size  
            }



            while (binr.ReadByte() == 0x00)
            {    //remove high order zeros in data  
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte  
            return count;
        }

        #endregion

        #region 解析.net 生成的Pem
        private static RSAParameters ConvertFromPublicKey(string pemFileConent)
        {

            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            byte[] pemModulus = new byte[128];
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }

        private static RSAParameters ConvertFromPrivateKey(string pemFileConent)
        {
            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 609)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }

            int index = 11;
            byte[] pemModulus = new byte[128];
            Array.Copy(keyData, index, pemModulus, 0, 128);

            index += 128;
            index += 2;//141  
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, index, pemPublicExponent, 0, 3);

            index += 3;
            index += 4;//148  
            byte[] pemPrivateExponent = new byte[128];
            Array.Copy(keyData, index, pemPrivateExponent, 0, 128);

            index += 128;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//279  
            byte[] pemPrime1 = new byte[64];
            Array.Copy(keyData, index, pemPrime1, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//346  
            byte[] pemPrime2 = new byte[64];
            Array.Copy(keyData, index, pemPrime2, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//412/413  
            byte[] pemExponent1 = new byte[64];
            Array.Copy(keyData, index, pemExponent1, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//479/480  
            byte[] pemExponent2 = new byte[64];
            Array.Copy(keyData, index, pemExponent2, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//545/546  
            byte[] pemCoefficient = new byte[64];
            Array.Copy(keyData, index, pemCoefficient, 0, 64);

            var para = new RSAParameters
            {
                Modulus = pemModulus,
                Exponent = pemPublicExponent,
                D = pemPrivateExponent,
                P = pemPrime1,
                Q = pemPrime2,
                DP = pemExponent1,
                DQ = pemExponent2,
                InverseQ = pemCoefficient
            };
            return para;
        }
        #endregion

        public static string GetSortParams(SortedDictionary<string, string> sArray)
        {
            StringBuilder _sb = new StringBuilder();
            IEnumerator<KeyValuePair<string, string>> dem = sArray.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    _sb.Append(name);
                    _sb.Append("=");
                    _sb.Append(value);
                    _sb.Append("&");
                }
            }

            return _sb.ToString().Substring(0, _sb.Length - 1);

        }

        public static string GetSortParamsUrlEnCode(SortedDictionary<string, string> sArray)
        {
            StringBuilder _sb = new StringBuilder();
            IEnumerator<KeyValuePair<string, string>> dem = sArray.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    _sb.Append(name);
                    _sb.Append("=");

                    _sb.Append(HttpUtility.UrlEncode(HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8), System.Text.Encoding.UTF8));

                    _sb.Append("&");
                }
            }

            return _sb.ToString().Substring(0, _sb.Length - 1);

        }


        public static string GetSortParamsUrlEnCode1(SortedDictionary<string, string> sArray)
        {
            StringBuilder _sb = new StringBuilder();
            IEnumerator<KeyValuePair<string, string>> dem = sArray.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    _sb.Append(name);
                    _sb.Append("=");

                    _sb.Append(HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8));

                    _sb.Append("&");
                }
            }

            return _sb.ToString().Substring(0, _sb.Length - 1);

        }
    }
}

