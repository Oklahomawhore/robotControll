using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Foundation;
using ObjCRuntime;
using UIKit;
using SystemConfiguration;


namespace robotControll
{


	public class StateObject{
		public Socket workSocket = null;

		public const int BufferSize = 267;

		public byte[] buffer = new byte[BufferSize];

		public StringBuilder sb = new StringBuilder ();

	}
		

	public class song{

		public string songName { get; set;}

		public int songNum { get; set;}

		public override string ToString ()
		{
			return "No." + songNum + " " + songName;
		}

		public override bool Equals (object obj)
		{
			if (obj == null) return false;
			song objAsSong = obj as song;
			if (objAsSong == null)
				return false;
			else
				return Equals (objAsSong);
		}

		public override int GetHashCode ()
		{
			return songNum;
		}

		public bool Equals(song other){
			if (other == null)
				return false;
			return (this.songName.Equals (other.songName));
		}
	}

	public class robotclient: IDisposable
	{
		public static List<song> songList = new List<song> ();

		public static int songID = 0;

		private static ManualResetEvent connectDone = 
			new ManualResetEvent (false);
		private static ManualResetEvent sendDone = 
			new ManualResetEvent (false);
		private static ManualResetEvent receiveDone =
			new ManualResetEvent (false);

		private static String response = String.Empty;

		List<String> songTitles = new List<String> ();

		private static Int32 backMID;

		#region command constants

		byte sync = 0x55;

		byte stx = 0xe2;

		byte etx = 0xe3;

		byte dlen = 0x00;

		Int32 next = 0x00000010;

		Int32 prev = 0x00000012;

		Int32 confirm = 0x00000014;

		Int32 stop = 0x00000016;

		Int32 forward = 0x00000018;

		Int32 backward = 0x0000001A;

		Int32 left = 0x0000001c;

		Int32 right = 0x0000001e;

		Int32 reset = 0x00000020;

		Int32 play = 0x00000022;

		int maximumLength = 267;
		#endregion



		public robotclient ()
		{
			 

		}



		public string sendAction(string command){



			switch (command){
			case "forward":
				
				return connectSocket (forward);

			case "backward":

				return connectSocket (backward);


			case "left":

				return connectSocket (left);


			case "right":

				return connectSocket (right);
					

			case "next":
					
				return connectSocket (next);

			case "prev":	

				return connectSocket (prev);

			case "reset":
				
				return connectSocket (reset);

			case "confirm":

				return connectSocket (confirm);
			
			case "stop":

				return connectSocket (stop);
			
			case "play":

				return connectSocket (play);
			
			default:
				{return null;}
			}

		}


		internal static IPAddress GetDeviceAddress(){


			var nFace = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces ();

			foreach (var f in nFace) {



				if (f.Name == "en0") {

					var properties = f.GetIPProperties ();

					var addresses = properties.GatewayAddresses;

					if (addresses.Count > 0) {

						foreach (var addr in addresses) {

							return addr.Address;
						}
						
						}
				
					}
				}
			return null;

			}

			



		public string connectSocket(Int32 command)
		{
			
			/*
			Socket cocoBegin = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			cocoBegin.ExclusiveAddressUse = false;

			cocoBegin.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			*/
			//get ipv4 addresses

			IPAddress ifString = GetDeviceAddress ();

			TcpClient cocoClient = new TcpClient ();

			cocoClient.Connect (new IPEndPoint(ifString, 8101));

			NetworkStream abc = 
				cocoClient.GetStream ();
			
				
			Console.WriteLine (ifString.ToString());


			//send message buffer to robot

			try{
			


				//async connect to remote Endpoint
				cocoBegin.BeginConnect(new IPEndPoint(ifString, 8101), new AsyncCallback(ConnectCallback), cocoBegin);

				connectDone.WaitOne();
				//cocoBegin.Connect(new IPEndPoint(ifString, 8081), new AsyncCallback(ConnectCallback), cocoBegin);


				byte[] buffer = prepBuffer (command);

				//send buffer to robot
				cocoBegin.BeginSend (buffer, 0, buffer.Length, 0, new AsyncCallback (SendCallback), cocoBegin);
				
				sendDone.WaitOne();

				//receive reply message
				Receive(cocoBegin);
				receiveDone.WaitOne();

				/*if ((backMID - 1) == command){
					
					if (response != String.Empty){

					songList.Add(new song(){songNum = songID++, songName = response});

					return response;
					}
					 else {

						songID = 0;

						return songList[0].songName;

					}



				}*/

				Console.WriteLine("response received! ");

				return response;
			}
			catch (SocketException ex){


				return ex.ToString ();

			}
			finally {

				if (cocoBegin.Connected) {

					cocoBegin.Shutdown (SocketShutdown.Both);

					cocoBegin.Close ();
				}
			}
		}



		public static void ConnectCallback(IAsyncResult ar){
			try{
				Socket client = (Socket) ar.AsyncState;

				client.EndConnect(ar);

				Console.WriteLine("Socket successfuly connected!");

				connectDone.Set();

			}catch(Exception ex){
				Console.WriteLine (ex.ToString ());
			}

		
		}

		public static void Receive(Socket client){

			try{
				//create the state object
				StateObject state = new StateObject();
				state.workSocket = client;

				//begin receive from robot
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
			}
			catch(Exception ex){
				Console.WriteLine (ex.ToString ());
			}
		
		}

		public static void ReceiveCallback(IAsyncResult ar){
			try{
			
				StateObject state = (StateObject)ar.AsyncState;

				Socket client = state.workSocket;

				int bytesRead = client.EndReceive (ar);

				if (bytesRead > 0) {
				
					state.sb.Append (Encoding.UTF8.GetString (state.buffer, 0, bytesRead));

					if (state.sb.Length > 1) {
						response = state.sb.ToString ();

						Console.WriteLine(response);

						if (state.buffer[8] == 0x00){

							backMID = BitConverter.ToInt32(state.buffer, 4);
						 
						} else{


							response = Encoding.UTF8.GetString(state.buffer, 9, state.buffer[8]);

						}


					}

				receiveDone.Set ();
				}
			} catch (Exception ex){
				Console.WriteLine (ex.ToString ());
			}

		}

		public static void SendCallback(IAsyncResult ar){

			try{

				Socket client = (Socket)ar.AsyncState;

				int byteSent = client.EndSend(ar);

				sendDone.Set();

			}
			catch(Exception ex){

				Console.WriteLine (ex.ToString ());

			}
		
		}
			
		public byte[] prepBuffer(Int32 mid)
		{

				byte[] buffer = new byte[maximumLength];

				byte[] data = new byte[dlen];

				byte[] syncHeader = new byte[3];

				for (int i = 0; i < 3; i++) {
					syncHeader [i] = sync;

					Console.Write (syncHeader [i] + " \n");
				}


				byte[] midByte = BitConverter.GetBytes (mid);

			Array.Reverse (midByte);
		
				List<byte> bufferList = new List<byte> ();

				bufferList.AddRange (syncHeader);

				bufferList.Add (stx);

				Array.Reverse (midByte);

				bufferList.AddRange (midByte);

				bufferList.Add (dlen);

				if (dlen != 0)
				bufferList.AddRange (data);

				int sum = 0;

				byte[] sumCalc = bufferList.GetRange (4, 5 + data.Length).ToArray ();

				for (int i = 0; i < sumCalc.Length; i++) {
					sum = (sum + sumCalc [i]) % 0xffff;
				}

				byte[] sumBytes = BitConverter.GetBytes (sum);

				bufferList.Add (sumBytes [0]);

				bufferList.Add (sumBytes [1]);

				bufferList.Add (etx);

				buffer = bufferList.ToArray ();

				return buffer;
		}

		public void Dispose(){
		}

}
}




