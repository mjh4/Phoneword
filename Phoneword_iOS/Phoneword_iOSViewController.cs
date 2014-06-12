// Phoneword_iOSViewController.cs
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

// This class implements the main view and handles the connections
// between the elements on the storyboard and the appropriate actions
// All of the code for this is handled in ViewDidLoad, and two
// global variables holding the translated number and call history are 
// declared at the top of the class. 

using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{

		// translatedNumber was moved here from ViewDidLoad ()
		string translatedNumber = "";

		public List<String> PhoneNumbers { get; set; }

		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
	{
			//initialize list of phone numbers called for Call History screen
			PhoneNumbers = new List<String> ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		// PrepareForSegue is only used when a Segue is created in the storyboard
		// our final implementation handles this transition in code when
		// CallHistoryButton is pressed

//		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
//		{
//			base.PrepareForSegue (segue, sender);
//
//			// set the View Controller that’s powering the screen we’re
//			// transitioning to
//
//			var callHistoryContoller = segue.DestinationViewController as CallHistoryController;
//
//			//set the Table View Controller’s list of phone numbers to the
//			// list of dialed phone numbers
//
//			if (callHistoryContoller != null) {
//				callHistoryContoller.PhoneNumbers = PhoneNumbers;
//			}
//		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			// Start with the CallButton Disabled
			CallButton.Enabled = false;

			// Code for when TranslateButton is pressed
			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {

				// Convert the phone number with text to a number 
				// using PhoneTranslator.cs
				translatedNumber = Core.PhonewordTranslator.ToNumber(
					PhoneNumberText.Text);   

				// Dismiss the keyboard if text field was tapped
				PhoneNumberText.ResignFirstResponder ();


				if (translatedNumber == "") {
					// If the Translate Button was pressed with no input entered, set the Call Button
					// to its original state
					CallButton.SetTitle ("Call", UIControlState.Normal);
					CallButton.Enabled = false;
				} 
				else {
				
					// If the Translate Button was pressed with an input, add the translated number 
					// to the Call Button title and enable the Button to be pressed. 
					CallButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
					CallButton.Enabled = true;
				}
			};

			// Code for when CallHistoryButton is pressed 
			CallHistoryButton.TouchUpInside += (object sender, EventArgs e) =>{
				// Launches a new instance of CallHistoryController
				CallHistoryController callHistory = this.Storyboard.InstantiateViewController ("CallHistoryController") as CallHistoryController;
				if (callHistory != null) {

					// Set the PhoneNumbers List in the CallHistoryController to the PhoneNumbersList in this View Controller
					callHistory.PhoneNumbers = PhoneNumbers;
					// Push the callHistoryController onto the NavigationController's stack
					this.NavigationController.PushViewController (callHistory, true);
				}
			};

			// Code for when CallButton is pressed
			CallButton.TouchUpInside += (object sender, EventArgs e) => {

				//store the phone number that we're dialing in PhoneNumbers
				// only if a number has been entered 
				if(translatedNumber != ""){
				PhoneNumbers.Add(translatedNumber);
				}

				//build the new url 
				var url = new NSUrl ("tel:" + translatedNumber);

				// Use URL handler with tel: prefix to invoke Apple's Phone app, 
				// otherwise show an alert dialog                

				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				}
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

