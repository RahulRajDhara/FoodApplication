using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;


namespace Common
{
    public static class GlobalSetting
    {
        private static string _rootPath = "";
        private static string _jsonPath = "";
        private static string _jsonString = "";
        private static JObject _jObject = null;

        static GlobalSetting()
        {
            
            _jsonPath = Path.Combine(_rootPath, "globalsettings.json");
            _jsonString = File.ReadAllText(_jsonPath);
            _jObject = JObject.Parse(_jsonString);

        }

        public static string ConnectionString
        {
            get
            {
                return _jObject.SelectToken("ConnectionString").Value<string>();
            }

        }
        public static string DataProvider
        {
            get
            {
                return _jObject.SelectToken("DataProvider").Value<string>();
            }

        }
        public static string SAPAPIENDPOINT
        {
            get
            {
                return _jObject.SelectToken("SAPAPIENDPOINT").Value<string>();
            }

        }
        public static string SAPUid
        {
            get
            {
                return _jObject.SelectToken("SAPUid").Value<string>();
            }

        }
        public static string SAPPwd
        {
            get
            {
                return _jObject.SelectToken("SAPPwd").Value<string>();
            }

        }
        public static string RECSAPAPIENDPOINT
        {
            get
            {
                return _jObject.SelectToken("RECSAPAPIENDPOINT").Value<string>();
            }

        }
        public static string RECSAPUid
        {
            get
            {
                return _jObject.SelectToken("RECSAPUid").Value<string>();
            }

        }
        public static string RECSAPPwd
        {
            get
            {
                return _jObject.SelectToken("RECSAPPwd").Value<string>();
            }

        }
        public static string ADJSAPAPIENDPOINT
        {
            get
            {
                return _jObject.SelectToken("ADJSAPAPIENDPOINT").Value<string>();
            }

        }
        public static string ADJSAPUid
        {
            get
            {
                return _jObject.SelectToken("ADJSAPUid").Value<string>();
            }

        }
        public static string ADJSAPPwd
        {
            get
            {
                return _jObject.SelectToken("ADJSAPPwd").Value<string>();
            }

        }
        public static string REVSAPAPIENDPOINT
        {
            get
            {
                return _jObject.SelectToken("REVSAPAPIENDPOINT").Value<string>();
            }

        }
        public static string REVSAPUid
        {
            get
            {
                return _jObject.SelectToken("REVSAPUid").Value<string>();
            }

        }
        public static string REVSAPPwd
        {
            get
            {
                return _jObject.SelectToken("REVSAPPwd").Value<string>();
            }

        }
        //---------------------------------------------------------------------------
        public static string RFIDENDPOINT
        {
            get
            {
                return _jObject.SelectToken("RFIDENDPOINT").Value<string>();
            }

        }
        public static string CFSID
        {
            get
            {
                return _jObject.SelectToken("CFSID").Value<string>();
            }

        }

        public static string RFIDSECRETKEY
        {
            get
            {
                return _jObject.SelectToken("RFIDSECRETKEY").Value<string>();
            }

        }
        //--------------------------------------------------------------------------
        public static string Environment
        {
            get
            {
                return _jObject.SelectToken("Environment").Value<string>();
            }

        }
        public static string CookiesAuthentnication
        {
            get
            {
                return _jObject.SelectToken("CookiesAuthentnication").Value<string>();
            }

        }

        public static string CCAVNWorkingKey
		{
			get
			{
				return _jObject.SelectToken("CCAVNWorkingKey").Value<string>();
			}

		}

		public static string CCAVNAccessCode
		{
			get
			{
				return _jObject.SelectToken("CCAVNAccessCode").Value<string>();
			}

		}

		public static string CCAVNWorkingKeyBqr
		{
			get
			{
				return _jObject.SelectToken("CCAVNWorkingKeyBqr").Value<string>();
			}

		}

		public static string CCAVNAccessCodeBqr
		{
			get
			{
				return _jObject.SelectToken("CCAVNAccessCodeBqr").Value<string>();
			}

		}

		public static string SAPJSONPATH
		{
			get
			{
				return _jObject.SelectToken("SAPJSONPATH").Value<string>();
			}

		}

		public static string BranchName
		{
			get
			{
				return _jObject.SelectToken("BranchName").Value<string>();
			}

		}
        public static string Dpe_ReportfileCIMSF
        {
            get
            {
                return _jObject.SelectToken("Dpe_ReportfileCIMSF").Value<string>();
            }

        }
        public static string DscApiUrl
        {
            get
            {
                return _jObject.SelectToken("DscApiUrl").Value<string>();
            }

        }
        public static string InJsonFile
        {
            get
            {
                return _jObject.SelectToken("InJsonFile").Value<string>();
            }

        }
        public static string OUTJsonFile
        {
            get
            {
                return _jObject.SelectToken("OUTJsonFile").Value<string>();
            }

        }
        public static string ArchiveInJsonFilePath
        {
            get
            {
                return _jObject.SelectToken("ArchiveInJsonFilePath").Value<string>();
            }

        }
        public static string DSCPATH
        {
            get
            {
                return _jObject.SelectToken("DSCPATH").Value<string>();
            }

        }
        public static string DSCPASSWORD
        {
            get
            {
                return _jObject.SelectToken("DSCPASSWORD").Value<string>();
            }

        }


    }
}
