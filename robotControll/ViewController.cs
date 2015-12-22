using System;
using System.Net;
using UIKit;
using System.Net.Sockets;
using System.Collections.Generic;

namespace robotControll
{


	public partial class ViewController : UIViewController
	{
		
		robotclient robTom = new robotclient ();

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();



			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void Forward_Button_TouchUpInside (UIButton sender)
		{

		    	myLabel.Text = robTom.sendAction("forward");


		}
				
		partial void Backward_Button_TouchUpInside (UIButton sender)
		{
			
				myLabel.Text = robTom.sendAction("backward");


		}

		partial void Right_Button_TouchUpInside (UIButton sender)
		{
			

				myLabel.Text = robTom.sendAction("right");

		}

		partial void Left_Button_TouchUpInside (UIButton sender)
		{
			

				myLabel.Text = robTom.sendAction("left");

		}

		partial void Reset_Button_TouchUpInside (UIButton sender)
		{

				myLabel.Text = robTom.sendAction("reset");


		}

		partial void Confirm_Button_TouchUpInside (UIButton sender)
		{

				myLabel.Text = robTom.sendAction("play");



		}

		partial void Next_Button_TouchUpInside (UIButton sender)
		{

				myLabel.Text = robTom.sendAction("next");


		}

		partial void Prev_Button_TouchUpInside (UIButton sender)
		{

				myLabel.Text = robTom.sendAction("prev");



		}
	}
}

