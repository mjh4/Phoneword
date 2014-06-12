// MainActivity.cs
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

// The Main Activity is launched when Phoneword is opened.
// This class connects the layout to the correct actions.
// It handles actions for when the call, translate and call history
// buttons are pressed. 

using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Phoneword_Droid
{
	[Activity (Label = "Phoneword", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : Activity
	{
		static readonly List<string> phoneNumbers = new List<string>();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


			// Get our UI controls from the loaded layout
			EditText PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			Button TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			Button CallButton = FindViewById<Button>(Resource.Id.CallButton);
			Button CallHistoryButton = FindViewById<Button> (Resource.Id.CallHistoryButton);


			// Disable the "Call" button
			CallButton.Enabled = false;

			// Start with translatedNumber as empty string
			string translatedNumber = string.Empty;

			// Code for when translatedButton is pressed
			TranslateButton.Click += (object sender, EventArgs e) =>
			{
				// Translate user’s alphanumeric phone number to numeric
				translatedNumber = Core.PhonewordTranslator.ToNumber(PhoneNumberText.Text);
				if (String.IsNullOrWhiteSpace(translatedNumber))
				{
					// If the translated number is empty or invalid
					// keep CallButton in disabled state
					CallButton.Text = "Call";
					CallButton.Enabled = false;
				}
				else
				{
					// Otherwise update the CallButton text and 
					// enable the button to be pressed 
					CallButton.Text = "Call " + translatedNumber;
					CallButton.Enabled = true;
				}
			};

			// Code for when CallButton is pressed 
			CallButton.Click += (object sender, EventArgs e) => {
				// On "Call" button click, try to dial phone number.
				var callDialog = new AlertDialog.Builder (this);
				callDialog.SetMessage ("Call " + translatedNumber + "?");

				callDialog.SetNeutralButton ("Call", delegate {

					// add dialed number to list of called numbers.
					phoneNumbers.Add(translatedNumber);
					// enable the Call History button
					CallHistoryButton.Enabled = true;

					// Create intent to dial phone
					var callIntent = new Intent (Intent.ActionCall);
					callIntent.SetData (Android.Net.Uri.Parse ("tel:" + translatedNumber));
					StartActivity (callIntent);
				});

				callDialog.SetNegativeButton ("Cancel", delegate {
				});

				// Show the alert dialog to the user and wait for response.
				callDialog.Show ();
			};

			// Code for when CallHistoryButton is pressed 
			CallHistoryButton.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(CallHistoryActivity));
				intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
				StartActivity(intent);
			};
		}
	}
}

