using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class PrintResponse
	{
		public int Status { get; set; }
        public string Message { get; set; }
        public byte[] FileByte { get; set; }
		public string PrintUrl { get; set; }
		public string FileName { get; set; }
	}
}
