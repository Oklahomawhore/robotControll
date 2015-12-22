// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Net;
using UIKit;

namespace robotControll
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton backward_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton confirm_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton forward_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton left_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel myLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton next_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton prev_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton reset_Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton right_Button { get; set; }

		[Action ("Backward_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Backward_Button_TouchUpInside (UIButton sender);

		[Action ("Confirm_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Confirm_Button_TouchUpInside (UIButton sender);

		[Action ("Forward_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Forward_Button_TouchUpInside (UIButton sender);

		[Action ("Left_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Left_Button_TouchUpInside (UIButton sender);

		[Action ("Next_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Next_Button_TouchUpInside (UIButton sender);

		[Action ("Prev_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Prev_Button_TouchUpInside (UIButton sender);

		[Action ("Reset_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Reset_Button_TouchUpInside (UIButton sender);

		[Action ("Right_Button_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Right_Button_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (backward_Button != null) {
				backward_Button.Dispose ();
				backward_Button = null;
			}
			if (confirm_Button != null) {
				confirm_Button.Dispose ();
				confirm_Button = null;
			}
			if (forward_Button != null) {
				forward_Button.Dispose ();
				forward_Button = null;
			}
			if (left_Button != null) {
				left_Button.Dispose ();
				left_Button = null;
			}
			if (myLabel != null) {
				myLabel.Dispose ();
				myLabel = null;
			}
			if (next_Button != null) {
				next_Button.Dispose ();
				next_Button = null;
			}
			if (prev_Button != null) {
				prev_Button.Dispose ();
				prev_Button = null;
			}
			if (reset_Button != null) {
				reset_Button.Dispose ();
				reset_Button = null;
			}
			if (right_Button != null) {
				right_Button.Dispose ();
				right_Button = null;
			}
		}
	}
}
