// PhoneTranslator.cs
//
// Author:
//			Xamarin 
//
// Copyright (c) 2014 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

// The PhonewordTranslator class defines the method ToNumber,
// as well as helper methods, that takes an alphanumeric phone input
// and returns the corresponding phone number

using System.Text;
using System;

namespace Core
{
	public static class PhonewordTranslator
	{

		// Takes an alphanumeric phone input
		// and returns the corresponding phone number as a string

		public static string ToNumber(string raw)
		{
			if (string.IsNullOrWhiteSpace(raw))
				return "";
			else
				// Make sure all non-numeric characters are
				// uppercase to allow for easier mapping
				raw = raw.ToUpperInvariant();

			// Create a string to hold the new number 
			var newNumber = new StringBuilder();

			// Iterate through raw string, add translated
			// characters onto newNumber
			foreach (var c in raw)
			{
				// Append numbers and dashes to newNumber
				if (" -0123456789".Contains(c))
					newNumber.Append(c);
				else {
					// Translate all other characters, 
					// then append to newNumber
					var result = TranslateToNumber(c);
					if (result != null)
						newNumber.Append(result);
				}
				// otherwise we've skipped a non-numeric char
			}
			return newNumber.ToString();
		}
			
		// Returns true if c is in keyString
		static bool Contains (this string keyString, char c)
		{
			return keyString.IndexOf(c) >= 0;
		}

		// Returns the number corresponding with each
		// letter as entered on a telephone 

		static int? TranslateToNumber(char c)
		{
			if ("ABC".Contains(c))
				return 2;
			else if ("DEF".Contains(c))
				return 3;
			else if ("GHI".Contains(c))
				return 4;
			else if ("JKL".Contains(c))
				return 5;
			else if ("MNO".Contains(c))
				return 6;
			else if ("PQRS".Contains(c))
				return 7;
			else if ("TUV".Contains(c))
				return 8;
			else if ("WXYZ".Contains(c))
				return 9;
			return null;
		}
	}
}