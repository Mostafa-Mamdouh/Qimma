using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lg = PRJ.GlobalComponents.Logger;

namespace PRJ.GlobalComponents.Trace
{
	public static class TraceManager
	{
		public static string TraceMessage(string exceptionMessage, string message, bool doLog,
											[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
											[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
											[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string _traceMsg = "class => (" + message + ") function => (" + memberName + ") error => (" + exceptionMessage + ")";
			
			if(doLog) 
				lg.LoggerManager.Log(_traceMsg);

			return _traceMsg;
		}



		public static string TraceFunction(string message, bool doLog,
									[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
									[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
									[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string _traceMsg = "class => (" + message + ") function => (" + memberName + ")";

			if (doLog)
				lg.LoggerManager.Log(_traceMsg);


			return _traceMsg;
		}
	}
}


//Trace.WriteLine("message: " + message);
//Trace.WriteLine("member name: " + memberName);
//Trace.WriteLine("source file path: " + sourceFilePath);
//Trace.WriteLine("source line number: " + sourceLineNumber);