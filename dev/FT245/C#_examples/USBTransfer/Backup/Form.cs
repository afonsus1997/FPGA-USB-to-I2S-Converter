/****************************************************************************************************/
/* Copyright (c)      2003 KOPF GmbH, Germany (All rights reserved)                                 */
/*                                                                                                  */
/* module name:       USBTransfer/Form.cs                                                                       */
/*                                                                                                  */
/* version:           V1.00                                                                         */
/*                                                                                                  */
/* abstract:          Basic Communication via AID.DLL to a FTDI-USB-Chip                            */
/*                                                                                                  */
/****************************************************************************************************/

/****************************************************************************************************/
/* List Of Changes                                                                                  */
/****************************************************************************************************/
/*                                                                                                  */
/* classes of changes:                                                                              */
/*                                                                                                  */
/* N: new module                                            (V1.00)                                 */
/* F: functional development                                (V+.00)                                 */
/* E: error correction                                      (Vx.+0)                                 */
/* I: change because of changed interface to other modules  (Vx.+0)                                 */
/* O: optimization without functional change                (Vx.x+)                                 */
/*                                                                                                  */
/* list of changes (last change is above):                                                          */
/*                                                                                                  */
/* Class: N    V1.00   03.04.03 KOPF-M.S.                                                           */
/* descript.:  new created                                                                          */
/*                                                                                                  */
/****************************************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace USBTransfer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form : System.Windows.Forms.Form
	{
		[DllImport("AID.dll")]
		static extern uint FT_ListDevices();
		[DllImport("AID.dll")]
		static extern uint FT_Open();
		[DllImport("AID.dll")]
		static extern uint FT_Close();
		[DllImport("AID.dll")]
		static extern uint FT_Write([MarshalAs(UnmanagedType.LPArray)] byte[] p_data,ulong size);
		[DllImport("AID.dll")]
		static extern uint FT_GetStatus(ref ulong rxsize, ref ulong txsize);
		[DllImport("AID.dll")]
		static extern uint FT_SetBitMode(byte mask, byte enable);
		[DllImport("AID.dll")]
		static extern uint FT_Read([MarshalAs(UnmanagedType.LPArray)] byte[] p_data,ulong size);
		[DllImport("AID.dll")]
		static extern uint FT_EE_Read(ref ushort vid,ref ushort pid, ref ushort power);
		[DllImport("AID.dll")]
		static extern uint FT_EE_Program(ushort power);
		[DllImport("AID.dll")]
		static extern uint FT_EE_ProgramToDefault();
		[DllImport("AID.dll")]
		static extern uint KCAN_Send(uint channel, uint id, uint dlc, [MarshalAs(UnmanagedType.LPArray)] byte[] p_data);
		[DllImport("AID.dll")]
		static extern uint KCAN_Receive(ref uint channel, ref uint id, ref uint dlc, [MarshalAs(UnmanagedType.LPArray)] byte[] p_data);
		[DllImport("AID.dll")]
		static extern uint KCAN_Init(uint baudraute);

		private System.Windows.Forms.Button btn_ListDevices;
		private System.Windows.Forms.TextBox tbx_No_Devices;
		private System.Windows.Forms.Button btn_Open;
		private System.Windows.Forms.Button btn_Close;
		private System.Windows.Forms.Button btn_Write;
		private System.Windows.Forms.Button btn_Read;
		private System.Windows.Forms.Button btn_CAN_Send;
		private System.Windows.Forms.Button btn_SetBitMode;
		private System.Windows.Forms.TextBox tbx_Write;
		private System.Windows.Forms.TextBox tbx_Read;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbx_Status;
		private System.Windows.Forms.Button btn_GetStatus;
		private System.Windows.Forms.TextBox tbx_RXSize;
		private System.Windows.Forms.TextBox tbx_TXSize;
		private System.Windows.Forms.Button btn_ResetDevice;
		private System.Windows.Forms.Button btn_EERead;
		private System.Windows.Forms.TextBox tbx_VID;
		private System.Windows.Forms.TextBox tbx_PID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btn_EEWrite;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbx_Power;
		private System.Windows.Forms.TextBox tbx_CAN_TX_ID;
		private System.Windows.Forms.TextBox tbx_CAN_TX_DLC;
		private System.Windows.Forms.TextBox tbx_CAN_TX_0;
		private System.Windows.Forms.TextBox tbx_CAN_TX_1;
		private System.Windows.Forms.TextBox tbx_CAN_TX_2;
		private System.Windows.Forms.TextBox tbx_CAN_TX_3;
		private System.Windows.Forms.TextBox tbx_CAN_TX_4;
		private System.Windows.Forms.TextBox tbx_CAN_TX_5;
		private System.Windows.Forms.TextBox tbx_CAN_TX_6;
		private System.Windows.Forms.TextBox tbx_CAN_TX_7;
		private System.Windows.Forms.TextBox tbx_CAN_RX_ID;
		private System.Windows.Forms.Button btn_CAN_Receive;
		private System.Windows.Forms.TextBox tbx_CAN_RX_7;
		private System.Windows.Forms.TextBox tbx_CAN_RX_6;
		private System.Windows.Forms.TextBox tbx_CAN_RX_5;
		private System.Windows.Forms.TextBox tbx_CAN_RX_4;
		private System.Windows.Forms.TextBox tbx_CAN_RX_3;
		private System.Windows.Forms.TextBox tbx_CAN_RX_2;
		private System.Windows.Forms.TextBox tbx_CAN_RX_1;
		private System.Windows.Forms.TextBox tbx_CAN_RX_0;
		private System.Windows.Forms.TextBox tbx_CAN_RX_DLC;
		private System.Windows.Forms.Button btn_CAN_Init;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btn_EEWriteDef;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btn_ListDevices = new System.Windows.Forms.Button();
			this.tbx_No_Devices = new System.Windows.Forms.TextBox();
			this.btn_Open = new System.Windows.Forms.Button();
			this.btn_Close = new System.Windows.Forms.Button();
			this.btn_Write = new System.Windows.Forms.Button();
			this.btn_Read = new System.Windows.Forms.Button();
			this.btn_CAN_Send = new System.Windows.Forms.Button();
			this.btn_SetBitMode = new System.Windows.Forms.Button();
			this.tbx_Write = new System.Windows.Forms.TextBox();
			this.tbx_Read = new System.Windows.Forms.TextBox();
			this.tbx_Status = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_GetStatus = new System.Windows.Forms.Button();
			this.tbx_RXSize = new System.Windows.Forms.TextBox();
			this.tbx_TXSize = new System.Windows.Forms.TextBox();
			this.btn_ResetDevice = new System.Windows.Forms.Button();
			this.btn_EERead = new System.Windows.Forms.Button();
			this.tbx_VID = new System.Windows.Forms.TextBox();
			this.tbx_PID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btn_EEWrite = new System.Windows.Forms.Button();
			this.tbx_Power = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btn_EEWriteDef = new System.Windows.Forms.Button();
			this.tbx_CAN_TX_ID = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_DLC = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_0 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_1 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_2 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_3 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_4 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_5 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_6 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_TX_7 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_7 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_6 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_5 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_4 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_3 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_2 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_1 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_0 = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_DLC = new System.Windows.Forms.TextBox();
			this.tbx_CAN_RX_ID = new System.Windows.Forms.TextBox();
			this.btn_CAN_Receive = new System.Windows.Forms.Button();
			this.btn_CAN_Init = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btn_ListDevices
			// 
			this.btn_ListDevices.Location = new System.Drawing.Point(8, 8);
			this.btn_ListDevices.Name = "btn_ListDevices";
			this.btn_ListDevices.Size = new System.Drawing.Size(75, 20);
			this.btn_ListDevices.TabIndex = 0;
			this.btn_ListDevices.Text = "ListDevices";
			this.btn_ListDevices.Click += new System.EventHandler(this.btn_ListDevices_Click);
			// 
			// tbx_No_Devices
			// 
			this.tbx_No_Devices.Location = new System.Drawing.Point(92, 8);
			this.tbx_No_Devices.Name = "tbx_No_Devices";
			this.tbx_No_Devices.Size = new System.Drawing.Size(24, 20);
			this.tbx_No_Devices.TabIndex = 1;
			this.tbx_No_Devices.Text = "";
			// 
			// btn_Open
			// 
			this.btn_Open.Location = new System.Drawing.Point(8, 36);
			this.btn_Open.Name = "btn_Open";
			this.btn_Open.Size = new System.Drawing.Size(75, 20);
			this.btn_Open.TabIndex = 2;
			this.btn_Open.Text = "Open";
			this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
			// 
			// btn_Close
			// 
			this.btn_Close.Enabled = false;
			this.btn_Close.Location = new System.Drawing.Point(8, 64);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(75, 20);
			this.btn_Close.TabIndex = 3;
			this.btn_Close.Text = "Close";
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// btn_Write
			// 
			this.btn_Write.Enabled = false;
			this.btn_Write.Location = new System.Drawing.Point(8, 92);
			this.btn_Write.Name = "btn_Write";
			this.btn_Write.Size = new System.Drawing.Size(75, 20);
			this.btn_Write.TabIndex = 4;
			this.btn_Write.Text = "Write";
			this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
			// 
			// btn_Read
			// 
			this.btn_Read.Enabled = false;
			this.btn_Read.Location = new System.Drawing.Point(8, 120);
			this.btn_Read.Name = "btn_Read";
			this.btn_Read.Size = new System.Drawing.Size(75, 20);
			this.btn_Read.TabIndex = 5;
			this.btn_Read.Text = "Read";
			this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
			// 
			// btn_CAN_Send
			// 
			this.btn_CAN_Send.Location = new System.Drawing.Point(8, 384);
			this.btn_CAN_Send.Name = "btn_CAN_Send";
			this.btn_CAN_Send.Size = new System.Drawing.Size(75, 20);
			this.btn_CAN_Send.TabIndex = 6;
			this.btn_CAN_Send.Text = "CAN Send";
			this.btn_CAN_Send.Click += new System.EventHandler(this.btn_CAN_Send_Click);
			// 
			// btn_SetBitMode
			// 
			this.btn_SetBitMode.Enabled = false;
			this.btn_SetBitMode.Location = new System.Drawing.Point(8, 148);
			this.btn_SetBitMode.Name = "btn_SetBitMode";
			this.btn_SetBitMode.Size = new System.Drawing.Size(75, 20);
			this.btn_SetBitMode.TabIndex = 7;
			this.btn_SetBitMode.Text = "SetBitMode";
			this.btn_SetBitMode.Click += new System.EventHandler(this.btn_SetBitMode_Click);
			// 
			// tbx_Write
			// 
			this.tbx_Write.Location = new System.Drawing.Point(92, 92);
			this.tbx_Write.Name = "tbx_Write";
			this.tbx_Write.Size = new System.Drawing.Size(172, 20);
			this.tbx_Write.TabIndex = 9;
			this.tbx_Write.Text = "";
			// 
			// tbx_Read
			// 
			this.tbx_Read.Location = new System.Drawing.Point(92, 120);
			this.tbx_Read.Name = "tbx_Read";
			this.tbx_Read.Size = new System.Drawing.Size(172, 20);
			this.tbx_Read.TabIndex = 10;
			this.tbx_Read.Text = "";
			// 
			// tbx_Status
			// 
			this.tbx_Status.Location = new System.Drawing.Point(200, 8);
			this.tbx_Status.Name = "tbx_Status";
			this.tbx_Status.Size = new System.Drawing.Size(64, 20);
			this.tbx_Status.TabIndex = 11;
			this.tbx_Status.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(148, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 20);
			this.label1.TabIndex = 12;
			this.label1.Text = "Status";
			// 
			// btn_GetStatus
			// 
			this.btn_GetStatus.Enabled = false;
			this.btn_GetStatus.Location = new System.Drawing.Point(8, 176);
			this.btn_GetStatus.Name = "btn_GetStatus";
			this.btn_GetStatus.Size = new System.Drawing.Size(75, 20);
			this.btn_GetStatus.TabIndex = 13;
			this.btn_GetStatus.Text = "GetStatus";
			this.btn_GetStatus.Click += new System.EventHandler(this.btn_GetStatus_Click);
			// 
			// tbx_RXSize
			// 
			this.tbx_RXSize.Location = new System.Drawing.Point(92, 176);
			this.tbx_RXSize.Name = "tbx_RXSize";
			this.tbx_RXSize.Size = new System.Drawing.Size(44, 20);
			this.tbx_RXSize.TabIndex = 14;
			this.tbx_RXSize.Text = "";
			// 
			// tbx_TXSize
			// 
			this.tbx_TXSize.Location = new System.Drawing.Point(144, 176);
			this.tbx_TXSize.Name = "tbx_TXSize";
			this.tbx_TXSize.Size = new System.Drawing.Size(44, 20);
			this.tbx_TXSize.TabIndex = 15;
			this.tbx_TXSize.Text = "";
			// 
			// btn_ResetDevice
			// 
			this.btn_ResetDevice.Enabled = false;
			this.btn_ResetDevice.Location = new System.Drawing.Point(8, 204);
			this.btn_ResetDevice.Name = "btn_ResetDevice";
			this.btn_ResetDevice.TabIndex = 16;
			this.btn_ResetDevice.Text = "ResetDev";
			// 
			// btn_EERead
			// 
			this.btn_EERead.Enabled = false;
			this.btn_EERead.Location = new System.Drawing.Point(8, 236);
			this.btn_EERead.Name = "btn_EERead";
			this.btn_EERead.TabIndex = 17;
			this.btn_EERead.Text = "EERead";
			this.btn_EERead.Click += new System.EventHandler(this.btn_EERead_Click);
			// 
			// tbx_VID
			// 
			this.tbx_VID.Location = new System.Drawing.Point(144, 236);
			this.tbx_VID.Name = "tbx_VID";
			this.tbx_VID.TabIndex = 18;
			this.tbx_VID.Text = "";
			// 
			// tbx_PID
			// 
			this.tbx_PID.Location = new System.Drawing.Point(144, 260);
			this.tbx_PID.Name = "tbx_PID";
			this.tbx_PID.TabIndex = 19;
			this.tbx_PID.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(92, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 20;
			this.label2.Text = "VID";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(92, 264);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 21;
			this.label3.Text = "PID";
			// 
			// btn_EEWrite
			// 
			this.btn_EEWrite.Enabled = false;
			this.btn_EEWrite.Location = new System.Drawing.Point(8, 264);
			this.btn_EEWrite.Name = "btn_EEWrite";
			this.btn_EEWrite.TabIndex = 22;
			this.btn_EEWrite.Text = "EEWrite";
			this.btn_EEWrite.Click += new System.EventHandler(this.btn_EEWrite_Click);
			// 
			// tbx_Power
			// 
			this.tbx_Power.Location = new System.Drawing.Point(144, 284);
			this.tbx_Power.Name = "tbx_Power";
			this.tbx_Power.TabIndex = 23;
			this.tbx_Power.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(92, 288);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 24;
			this.label4.Text = "Power";
			// 
			// btn_EEWriteDef
			// 
			this.btn_EEWriteDef.Enabled = false;
			this.btn_EEWriteDef.Location = new System.Drawing.Point(8, 292);
			this.btn_EEWriteDef.Name = "btn_EEWriteDef";
			this.btn_EEWriteDef.TabIndex = 25;
			this.btn_EEWriteDef.Text = "EEWriteDef";
			this.btn_EEWriteDef.Click += new System.EventHandler(this.btn_WriteDef_Click);
			// 
			// tbx_CAN_TX_ID
			// 
			this.tbx_CAN_TX_ID.Location = new System.Drawing.Point(92, 384);
			this.tbx_CAN_TX_ID.Name = "tbx_CAN_TX_ID";
			this.tbx_CAN_TX_ID.Size = new System.Drawing.Size(44, 20);
			this.tbx_CAN_TX_ID.TabIndex = 27;
			this.tbx_CAN_TX_ID.Text = "0";
			// 
			// tbx_CAN_TX_DLC
			// 
			this.tbx_CAN_TX_DLC.Location = new System.Drawing.Point(144, 384);
			this.tbx_CAN_TX_DLC.Name = "tbx_CAN_TX_DLC";
			this.tbx_CAN_TX_DLC.Size = new System.Drawing.Size(44, 20);
			this.tbx_CAN_TX_DLC.TabIndex = 28;
			this.tbx_CAN_TX_DLC.Text = "0";
			// 
			// tbx_CAN_TX_0
			// 
			this.tbx_CAN_TX_0.Location = new System.Drawing.Point(196, 384);
			this.tbx_CAN_TX_0.Name = "tbx_CAN_TX_0";
			this.tbx_CAN_TX_0.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_0.TabIndex = 29;
			this.tbx_CAN_TX_0.Text = "0";
			// 
			// tbx_CAN_TX_1
			// 
			this.tbx_CAN_TX_1.Location = new System.Drawing.Point(228, 384);
			this.tbx_CAN_TX_1.Name = "tbx_CAN_TX_1";
			this.tbx_CAN_TX_1.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_1.TabIndex = 30;
			this.tbx_CAN_TX_1.Text = "0";
			// 
			// tbx_CAN_TX_2
			// 
			this.tbx_CAN_TX_2.Location = new System.Drawing.Point(260, 384);
			this.tbx_CAN_TX_2.Name = "tbx_CAN_TX_2";
			this.tbx_CAN_TX_2.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_2.TabIndex = 31;
			this.tbx_CAN_TX_2.Text = "0";
			// 
			// tbx_CAN_TX_3
			// 
			this.tbx_CAN_TX_3.Location = new System.Drawing.Point(292, 384);
			this.tbx_CAN_TX_3.Name = "tbx_CAN_TX_3";
			this.tbx_CAN_TX_3.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_3.TabIndex = 32;
			this.tbx_CAN_TX_3.Text = "0";
			// 
			// tbx_CAN_TX_4
			// 
			this.tbx_CAN_TX_4.Location = new System.Drawing.Point(324, 384);
			this.tbx_CAN_TX_4.Name = "tbx_CAN_TX_4";
			this.tbx_CAN_TX_4.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_4.TabIndex = 33;
			this.tbx_CAN_TX_4.Text = "0";
			// 
			// tbx_CAN_TX_5
			// 
			this.tbx_CAN_TX_5.Location = new System.Drawing.Point(356, 384);
			this.tbx_CAN_TX_5.Name = "tbx_CAN_TX_5";
			this.tbx_CAN_TX_5.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_5.TabIndex = 34;
			this.tbx_CAN_TX_5.Text = "0";
			// 
			// tbx_CAN_TX_6
			// 
			this.tbx_CAN_TX_6.Location = new System.Drawing.Point(388, 384);
			this.tbx_CAN_TX_6.Name = "tbx_CAN_TX_6";
			this.tbx_CAN_TX_6.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_6.TabIndex = 35;
			this.tbx_CAN_TX_6.Text = "0";
			// 
			// tbx_CAN_TX_7
			// 
			this.tbx_CAN_TX_7.Location = new System.Drawing.Point(420, 384);
			this.tbx_CAN_TX_7.Name = "tbx_CAN_TX_7";
			this.tbx_CAN_TX_7.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_TX_7.TabIndex = 36;
			this.tbx_CAN_TX_7.Text = "0";
			// 
			// tbx_CAN_RX_7
			// 
			this.tbx_CAN_RX_7.Location = new System.Drawing.Point(420, 412);
			this.tbx_CAN_RX_7.Name = "tbx_CAN_RX_7";
			this.tbx_CAN_RX_7.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_7.TabIndex = 47;
			this.tbx_CAN_RX_7.Text = "";
			// 
			// tbx_CAN_RX_6
			// 
			this.tbx_CAN_RX_6.Location = new System.Drawing.Point(388, 412);
			this.tbx_CAN_RX_6.Name = "tbx_CAN_RX_6";
			this.tbx_CAN_RX_6.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_6.TabIndex = 46;
			this.tbx_CAN_RX_6.Text = "";
			// 
			// tbx_CAN_RX_5
			// 
			this.tbx_CAN_RX_5.Location = new System.Drawing.Point(356, 412);
			this.tbx_CAN_RX_5.Name = "tbx_CAN_RX_5";
			this.tbx_CAN_RX_5.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_5.TabIndex = 45;
			this.tbx_CAN_RX_5.Text = "";
			// 
			// tbx_CAN_RX_4
			// 
			this.tbx_CAN_RX_4.Location = new System.Drawing.Point(324, 412);
			this.tbx_CAN_RX_4.Name = "tbx_CAN_RX_4";
			this.tbx_CAN_RX_4.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_4.TabIndex = 44;
			this.tbx_CAN_RX_4.Text = "";
			// 
			// tbx_CAN_RX_3
			// 
			this.tbx_CAN_RX_3.Location = new System.Drawing.Point(292, 412);
			this.tbx_CAN_RX_3.Name = "tbx_CAN_RX_3";
			this.tbx_CAN_RX_3.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_3.TabIndex = 43;
			this.tbx_CAN_RX_3.Text = "";
			// 
			// tbx_CAN_RX_2
			// 
			this.tbx_CAN_RX_2.Location = new System.Drawing.Point(260, 412);
			this.tbx_CAN_RX_2.Name = "tbx_CAN_RX_2";
			this.tbx_CAN_RX_2.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_2.TabIndex = 42;
			this.tbx_CAN_RX_2.Text = "";
			// 
			// tbx_CAN_RX_1
			// 
			this.tbx_CAN_RX_1.Location = new System.Drawing.Point(228, 412);
			this.tbx_CAN_RX_1.Name = "tbx_CAN_RX_1";
			this.tbx_CAN_RX_1.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_1.TabIndex = 41;
			this.tbx_CAN_RX_1.Text = "";
			// 
			// tbx_CAN_RX_0
			// 
			this.tbx_CAN_RX_0.Location = new System.Drawing.Point(196, 412);
			this.tbx_CAN_RX_0.Name = "tbx_CAN_RX_0";
			this.tbx_CAN_RX_0.Size = new System.Drawing.Size(28, 20);
			this.tbx_CAN_RX_0.TabIndex = 40;
			this.tbx_CAN_RX_0.Text = "";
			// 
			// tbx_CAN_RX_DLC
			// 
			this.tbx_CAN_RX_DLC.Location = new System.Drawing.Point(144, 412);
			this.tbx_CAN_RX_DLC.Name = "tbx_CAN_RX_DLC";
			this.tbx_CAN_RX_DLC.Size = new System.Drawing.Size(44, 20);
			this.tbx_CAN_RX_DLC.TabIndex = 39;
			this.tbx_CAN_RX_DLC.Text = "";
			// 
			// tbx_CAN_RX_ID
			// 
			this.tbx_CAN_RX_ID.Location = new System.Drawing.Point(92, 412);
			this.tbx_CAN_RX_ID.Name = "tbx_CAN_RX_ID";
			this.tbx_CAN_RX_ID.Size = new System.Drawing.Size(44, 20);
			this.tbx_CAN_RX_ID.TabIndex = 38;
			this.tbx_CAN_RX_ID.Text = "";
			// 
			// btn_CAN_Receive
			// 
			this.btn_CAN_Receive.Location = new System.Drawing.Point(8, 412);
			this.btn_CAN_Receive.Name = "btn_CAN_Receive";
			this.btn_CAN_Receive.Size = new System.Drawing.Size(75, 20);
			this.btn_CAN_Receive.TabIndex = 37;
			this.btn_CAN_Receive.Text = "CAN Recv";
			this.btn_CAN_Receive.Click += new System.EventHandler(this.btn_CAN_Receive_Click);
			// 
			// btn_CAN_Init
			// 
			this.btn_CAN_Init.Location = new System.Drawing.Point(8, 356);
			this.btn_CAN_Init.Name = "btn_CAN_Init";
			this.btn_CAN_Init.Size = new System.Drawing.Size(75, 20);
			this.btn_CAN_Init.TabIndex = 48;
			this.btn_CAN_Init.Text = "CAN Init";
			this.btn_CAN_Init.Click += new System.EventHandler(this.btn_CAN_Init_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 332);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(304, 23);
			this.label5.TabIndex = 49;
			this.label5.Text = "KOPF CAN Commands - no standard commands";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(272, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 23);
			this.label6.TabIndex = 50;
			this.label6.Text = "A1,1F,D7";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(372, 28);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(100, 16);
			this.linkLabel1.TabIndex = 51;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "www.kopfweb.de";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(372, 8);
			this.label7.Name = "label7";
			this.label7.TabIndex = 52;
			this.label7.Text = "KOPF GmbH";
			// 
			// Form
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 473);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label7,
																		  this.linkLabel1,
																		  this.label6,
																		  this.label5,
																		  this.btn_CAN_Init,
																		  this.tbx_CAN_RX_7,
																		  this.tbx_CAN_RX_6,
																		  this.tbx_CAN_RX_5,
																		  this.tbx_CAN_RX_4,
																		  this.tbx_CAN_RX_3,
																		  this.tbx_CAN_RX_2,
																		  this.tbx_CAN_RX_1,
																		  this.tbx_CAN_RX_0,
																		  this.tbx_CAN_RX_DLC,
																		  this.tbx_CAN_RX_ID,
																		  this.btn_CAN_Receive,
																		  this.tbx_CAN_TX_7,
																		  this.tbx_CAN_TX_6,
																		  this.tbx_CAN_TX_5,
																		  this.tbx_CAN_TX_4,
																		  this.tbx_CAN_TX_3,
																		  this.tbx_CAN_TX_2,
																		  this.tbx_CAN_TX_1,
																		  this.tbx_CAN_TX_0,
																		  this.tbx_CAN_TX_DLC,
																		  this.tbx_CAN_TX_ID,
																		  this.btn_EEWriteDef,
																		  this.label4,
																		  this.tbx_Power,
																		  this.btn_EEWrite,
																		  this.label3,
																		  this.label2,
																		  this.tbx_PID,
																		  this.tbx_VID,
																		  this.btn_EERead,
																		  this.btn_ResetDevice,
																		  this.tbx_TXSize,
																		  this.tbx_RXSize,
																		  this.btn_GetStatus,
																		  this.label1,
																		  this.tbx_Status,
																		  this.tbx_Read,
																		  this.tbx_Write,
																		  this.btn_SetBitMode,
																		  this.btn_CAN_Send,
																		  this.btn_Read,
																		  this.btn_Write,
																		  this.btn_Close,
																		  this.btn_Open,
																		  this.tbx_No_Devices,
																		  this.btn_ListDevices});
			this.Name = "Form";
			this.Text = "USBTransfer";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form());
		}

		private void btn_ListDevices_Click(object sender, System.EventArgs e)
		{
			tbx_No_Devices.Text=Convert.ToString(FT_ListDevices());
		}

		private void btn_SetBitMode_Click(object sender, System.EventArgs e)
		{
			FT_SetBitMode(0xFF,0xFF);
		}

		private void btn_Write_Click(object sender, System.EventArgs e)
		{
			byte[] data=new Byte[64];
			char[] delimiter=new Char[1];
			string[] a_str_data=new String[64];
			uint i;

			delimiter[0]=',';
			a_str_data=tbx_Write.Text.Split(delimiter,64);
			for(i=0;i<a_str_data.Length;i++)
			{
				if (a_str_data[i].Length>0) data[i]=Convert.ToByte(a_str_data[i],16);
				else                        data[i]=0;
			}
			FT_Write(data,i);
		}

		private void btn_Open_Click(object sender, System.EventArgs e)
		{
			uint status;
			status=FT_Open();

			tbx_Status.Text=Convert.ToString(status);
			if(status==0)
			{
				btn_Close.Enabled=true;
				btn_Write.Enabled=true;
				btn_Read.Enabled=true;
				btn_SetBitMode.Enabled=true;
				btn_GetStatus.Enabled=true;
				btn_ResetDevice.Enabled=true;
				btn_EERead.Enabled=true;
				btn_EEWrite.Enabled=true;
				btn_EEWriteDef.Enabled=true;
			}
		}

		private void btn_Read_Click(object sender, System.EventArgs e)
		{
			byte[] data=new Byte[64];
			uint i,length;

			tbx_Read.Text="";
			length=FT_Read(data, 1);
			for(i=0;i<length;i++)
			{
				tbx_Read.Text+=Convert.ToString(data[i],16);
				if (i<(length-1)) tbx_Read.Text+=",";
			}
		}

		private void btn_Close_Click(object sender, System.EventArgs e)
		{
			FT_Close();
			btn_Close.Enabled=false;
			btn_Write.Enabled=false;
			btn_Read.Enabled=false;
			btn_SetBitMode.Enabled=false;
			btn_GetStatus.Enabled=false;
			btn_ResetDevice.Enabled=false;
			btn_EERead.Enabled=false;
			btn_EEWrite.Enabled=false;
			btn_EEWriteDef.Enabled=false;
		}

		private void btn_CAN_Send_Click(object sender, System.EventArgs e)
		{
			byte[] data=new Byte[8];
			uint  id;
			uint   dlc;

			id=Convert.ToUInt32(tbx_CAN_TX_ID.Text,16);
			dlc=Convert.ToByte(tbx_CAN_TX_DLC.Text,16);
			data[0]=Convert.ToByte(tbx_CAN_TX_0.Text,16);
			data[1]=Convert.ToByte(tbx_CAN_TX_1.Text,16);
			data[2]=Convert.ToByte(tbx_CAN_TX_2.Text,16);
			data[3]=Convert.ToByte(tbx_CAN_TX_3.Text,16);
			data[4]=Convert.ToByte(tbx_CAN_TX_4.Text,16);
			data[5]=Convert.ToByte(tbx_CAN_TX_5.Text,16);
			data[6]=Convert.ToByte(tbx_CAN_TX_6.Text,16);
			data[7]=Convert.ToByte(tbx_CAN_TX_7.Text,16);

			KCAN_Send(0,id,dlc,data);
		}

		private void btn_GetStatus_Click(object sender, System.EventArgs e)
		{
			ulong rxsize=0,txsize=0;
			FT_GetStatus(ref rxsize,ref txsize);
			tbx_RXSize.Text=Convert.ToString(rxsize);
			tbx_TXSize.Text=Convert.ToString(txsize);
		}

		private void btn_EERead_Click(object sender, System.EventArgs e)
		{
			byte[] data=new Byte[128];
			ushort vid=0,pid=0,power=0;

			FT_EE_Read(ref vid, ref pid,ref power);

			tbx_VID.Text=Convert.ToString(vid,16);
			tbx_PID.Text=Convert.ToString(pid,16);
			tbx_Power.Text=Convert.ToString(power);
		}

		private void btn_EEWrite_Click(object sender, System.EventArgs e)
		{
			byte[] data=new Byte[128];
			uint vid,pid;
			ushort power_l;

			power_l=Convert.ToUInt16(tbx_Power.Text);

			FT_EE_Program(power_l);
		}

		private void btn_WriteDef_Click(object sender, System.EventArgs e)
		{
			FT_EE_ProgramToDefault();
		}

		private void btn_RespTest_Click(object sender, System.EventArgs e)
		{
			byte[] data=new Byte[64];
			ulong rxsize=0,txsize=0;
    
			data[0]=1;
			FT_Write(data,1);
			do 
			{
				FT_GetStatus(ref rxsize,ref txsize);
			}
			while(rxsize==0);
			FT_Read(data, 1);
			FT_Write(data,2);
			do 
			{
				FT_GetStatus(ref rxsize,ref txsize);
			}
			while(rxsize==0);
			FT_Read(data, 1);
		}

		private void btn_CAN_Receive_Click(object sender, System.EventArgs e)
		{
			uint id,dlc,channel;
			uint result;
			byte[] data=new Byte[8];
			id=0; dlc=0; channel=0;
			result=KCAN_Receive(ref channel, ref id, ref dlc, data);
			if (result==0)
			{
				tbx_CAN_RX_ID.Text=Convert.ToString(id,16);
				tbx_CAN_RX_DLC.Text=Convert.ToString(dlc,16);
				tbx_CAN_RX_0.Text=Convert.ToString(data[0],16);
				tbx_CAN_RX_1.Text=Convert.ToString(data[1],16);
				tbx_CAN_RX_2.Text=Convert.ToString(data[2],16);
				tbx_CAN_RX_3.Text=Convert.ToString(data[3],16);
				tbx_CAN_RX_4.Text=Convert.ToString(data[4],16);
				tbx_CAN_RX_5.Text=Convert.ToString(data[5],16);
				tbx_CAN_RX_6.Text=Convert.ToString(data[6],16);
				tbx_CAN_RX_7.Text=Convert.ToString(data[7],16);
			}
			else 
			{
				tbx_CAN_RX_ID.Text="X";
			}
		}

		private void btn_CAN_Init_Click(object sender, System.EventArgs e)
		{
			KCAN_Init(100000);
		}
	}
}
