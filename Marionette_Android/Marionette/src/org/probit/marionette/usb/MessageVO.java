package org.probit.marionette.usb;

public class MessageVO {
   public int arg1;
   public int arg2;
   public int arg3;
   public int arg4;
   public int arg5;
   public byte[] buf;
   
   public MessageVO(byte[] buf)
   {
	   this.buf = buf;
   }
   
   public MessageVO(int arg1)
   {
      this.arg1 = arg1;
   }

   public MessageVO(int arg1, int arg2, int arg3, int arg4, int arg5) {
      this.arg1 = arg1;
      this.arg2 = arg2;
      this.arg3 = arg3;
      this.arg4 = arg4;
      this.arg5 = arg5;
   }

   public MessageVO(int arg1, int arg2, int arg3, int arg4) {
      this.arg1 = arg1;
      this.arg2 = arg2;
      this.arg3 = arg3;
      this.arg4 = arg4;
   }

   public MessageVO(int arg1, int arg2, int arg3) {
      this.arg1 = arg1;
      this.arg2 = arg2;
      this.arg3 = arg3;
   }

   public String getMessageType(int a) {
      String[] msg_type_arr = new String[2];
      msg_type_arr[0] = "Keyboard Event";
      msg_type_arr[1] = "Mouse Event";

      return msg_type_arr[a];
   }

   public String getKeyCode(int a)
   {
      String[] msg_type_arr = new String[256];
      msg_type_arr[0] = "None";
      msg_type_arr[1] = "LButton";
      msg_type_arr[2] = "RButton";
      msg_type_arr[3] = "Cancel";
      msg_type_arr[4] = "MButton";
      msg_type_arr[5] = "XButton1";
      msg_type_arr[6] = "XButton2";
      msg_type_arr[7] = "";
      msg_type_arr[8] = "Back";
      msg_type_arr[9] = "Tab";
      msg_type_arr[10] = "LineFeed";
      msg_type_arr[11] = "";
      msg_type_arr[12] = "Clear";
      msg_type_arr[13] = "Enter";
      msg_type_arr[14] = "";
      msg_type_arr[15] = "";
      msg_type_arr[16] = "ShiftKey";
      msg_type_arr[17] = "ControlKey";
      msg_type_arr[18] = "Menu";
      msg_type_arr[19] = "Pause";
      msg_type_arr[20] = "CapsLock";
      msg_type_arr[21] = "KanaMode";
      msg_type_arr[22] = "";
      msg_type_arr[23] = "JunjaMode";
      msg_type_arr[24] = "FinalMode";
      msg_type_arr[25] = "HanjaMode";
      msg_type_arr[26] = "";
      msg_type_arr[27] = "Escape";
      msg_type_arr[28] = "IMEConvert";
      msg_type_arr[29] = "IMENonconvert";
      msg_type_arr[30] = "IMEAceept";
      msg_type_arr[31] = "IMEModeChange";
      msg_type_arr[32] = "Space";
      msg_type_arr[33] = "PageUp";
      msg_type_arr[34] = "PageDown";
      msg_type_arr[35] = "End";
      msg_type_arr[36] = "Home";
      msg_type_arr[37] = "Left";
      msg_type_arr[38] = "Up";
      msg_type_arr[39] = "Right";
      msg_type_arr[40] = "Down";
      msg_type_arr[41] = "Select";
      msg_type_arr[42] = "Print";
      msg_type_arr[43] = "Execute";
      msg_type_arr[44] = "PrintScreen";
      msg_type_arr[45] = "Insert";
      msg_type_arr[46] = "Delete";
      msg_type_arr[47] = "Help";
      msg_type_arr[48] = "D0";
      msg_type_arr[49] = "D1";
      msg_type_arr[50] = "D2";
      msg_type_arr[51] = "D3";
      msg_type_arr[52] = "D4";
      msg_type_arr[53] = "D5";
      msg_type_arr[54] = "D6";
      msg_type_arr[55] = "D7";
      msg_type_arr[56] = "D8";
      msg_type_arr[57] = "D9";
      msg_type_arr[58] = "";
      msg_type_arr[59] = "";
      msg_type_arr[60] = "";
      msg_type_arr[61] = "";
      msg_type_arr[62] = "";
      msg_type_arr[63] = "";
      msg_type_arr[64] = "";
      msg_type_arr[65] = "A";
      msg_type_arr[66] = "B";
      msg_type_arr[67] = "C";
      msg_type_arr[68] = "D";
      msg_type_arr[69] = "E";
      msg_type_arr[70] = "F";
      msg_type_arr[71] = "G";
      msg_type_arr[72] = "H";
      msg_type_arr[73] = "I";
      msg_type_arr[74] = "J";
      msg_type_arr[75] = "K";
      msg_type_arr[76] = "L";
      msg_type_arr[77] = "M";
      msg_type_arr[78] = "N";
      msg_type_arr[79] = "O";
      msg_type_arr[80] = "P";
      msg_type_arr[81] = "Q";
      msg_type_arr[82] = "R";
      msg_type_arr[83] = "S";
      msg_type_arr[84] = "T";
      msg_type_arr[85] = "U";
      msg_type_arr[86] = "V";
      msg_type_arr[87] = "W";
      msg_type_arr[88] = "X";
      msg_type_arr[89] = "Y";
      msg_type_arr[90] = "Z";
      msg_type_arr[91] = "LWin";
      msg_type_arr[92] = "RWin";
      msg_type_arr[93] = "Apps";
      msg_type_arr[94] = "";
      msg_type_arr[95] = "Sleep";
      msg_type_arr[96] = "NumPad0";
      msg_type_arr[97] = "NumPad1";
      msg_type_arr[98] = "NumPad2";
      msg_type_arr[99] = "NumPad3";
      msg_type_arr[100] = "NumPad4";
      msg_type_arr[101] = "NumPad5";
      msg_type_arr[102] = "NumPad6";
      msg_type_arr[103] = "NumPad7";
      msg_type_arr[104] = "NumPad8";
      msg_type_arr[105] = "NumPad9";
      msg_type_arr[106] = "Multiply";
      msg_type_arr[107] = "Add";
      msg_type_arr[108] = "Separator";
      msg_type_arr[109] = "Subtract";
      msg_type_arr[110] = "Decimal";
      msg_type_arr[111] = "Divide";
      msg_type_arr[112] = "F1";
      msg_type_arr[113] = "F2";
      msg_type_arr[114] = "F3";
      msg_type_arr[115] = "F4";
      msg_type_arr[116] = "F5";
      msg_type_arr[117] = "F6";
      msg_type_arr[118] = "F7";
      msg_type_arr[119] = "F8";
      msg_type_arr[120] = "F9";
      msg_type_arr[121] = "F10";
      msg_type_arr[122] = "F11";
      msg_type_arr[123] = "F12";
      msg_type_arr[124] = "F13";
      msg_type_arr[125] = "F14";
      msg_type_arr[126] = "F15";
      msg_type_arr[127] = "F16";
      msg_type_arr[128] = "F17";
      msg_type_arr[129] = "F18";
      msg_type_arr[130] = "F19";
      msg_type_arr[131] = "F20";
      msg_type_arr[132] = "F21";
      msg_type_arr[133] = "F22";
      msg_type_arr[134] = "F23";
      msg_type_arr[135] = "F24";
      msg_type_arr[136] = "";
      msg_type_arr[137] = "";
      msg_type_arr[138] = "";
      msg_type_arr[139] = "";
      msg_type_arr[140] = "";
      msg_type_arr[141] = "";
      msg_type_arr[142] = "";
      msg_type_arr[143] = "";
      msg_type_arr[144] = "NumLock";
      msg_type_arr[145] = "Scroll";
      msg_type_arr[146] = "";
      msg_type_arr[147] = "";
      msg_type_arr[148] = "";
      msg_type_arr[149] = "";
      msg_type_arr[150] = "";
      msg_type_arr[151] = "";
      msg_type_arr[152] = "";
      msg_type_arr[153] = "";
      msg_type_arr[154] = "";
      msg_type_arr[155] = "";
      msg_type_arr[156] = "";
      msg_type_arr[157] = "";
      msg_type_arr[158] = "";
      msg_type_arr[159] = "";
      msg_type_arr[160] = "LShiftKey";
      msg_type_arr[161] = "RShiftKey";
      msg_type_arr[162] = "LControlKey";
      msg_type_arr[163] = "RControlKey";
      msg_type_arr[164] = "LMenu";
      msg_type_arr[165] = "RMenu";
      msg_type_arr[166] = "BrowserBack";
      msg_type_arr[167] = "BrowserForward";
      msg_type_arr[168] = "BrowserRefresh";
      msg_type_arr[169] = "BrowserStop";
      msg_type_arr[170] = "BrowserSearch";
      msg_type_arr[171] = "BrowserFavorites";
      msg_type_arr[172] = "BrowserHome";
      msg_type_arr[173] = "VolumeMute";
      msg_type_arr[174] = "VolumeDown";
      msg_type_arr[175] = "VolumeUp";
      msg_type_arr[176] = "MediaNextTrack";
      msg_type_arr[177] = "MediaPreviousTrack";
      msg_type_arr[178] = "MediaStop";
      msg_type_arr[179] = "MediaPlayPause";
      msg_type_arr[180] = "LaunchMail";
      msg_type_arr[181] = "SelectMedia";
      msg_type_arr[182] = "LaunchApplication1";
      msg_type_arr[183] = "LaunchApplication2";
      msg_type_arr[184] = "";
      msg_type_arr[185] = "";
      msg_type_arr[186] = "OemSemicolon";
      msg_type_arr[187] = "Oemplus";
      msg_type_arr[188] = "Oemcomma";
      msg_type_arr[189] = "OemMinus";
      msg_type_arr[190] = "OemPeriod";
      msg_type_arr[191] = "OemQuestion";
      msg_type_arr[192] = "Oemtilde";
      msg_type_arr[193] = "";
      msg_type_arr[194] = "";
      msg_type_arr[195] = "";
      msg_type_arr[196] = "";
      msg_type_arr[197] = "";
      msg_type_arr[198] = "";
      msg_type_arr[199] = "";
      msg_type_arr[200] = "";
      msg_type_arr[201] = "";
      msg_type_arr[202] = "";
      msg_type_arr[203] = "";
      msg_type_arr[204] = "";
      msg_type_arr[205] = "";
      msg_type_arr[206] = "";
      msg_type_arr[207] = "";
      msg_type_arr[208] = "";
      msg_type_arr[209] = "";
      msg_type_arr[210] = "";
      msg_type_arr[211] = "";
      msg_type_arr[212] = "";
      msg_type_arr[213] = "";
      msg_type_arr[214] = "";
      msg_type_arr[215] = "";
      msg_type_arr[216] = "";
      msg_type_arr[217] = "";
      msg_type_arr[218] = "";
      msg_type_arr[219] = "OemOpenBrackets";
      msg_type_arr[220] = "OemPipe";
      msg_type_arr[221] = "OemCloseBrackets";
      msg_type_arr[222] = "OemQuotes";
      msg_type_arr[223] = "Oem8";
      msg_type_arr[224] = "";
      msg_type_arr[225] = "";
      msg_type_arr[226] = "OemBackslash";
      msg_type_arr[227] = "";
      msg_type_arr[228] = "";
      msg_type_arr[229] = "ProcessKey";
      msg_type_arr[230] = "";
      msg_type_arr[231] = "Packet";
      msg_type_arr[232] = "";
      msg_type_arr[233] = "";
      msg_type_arr[234] = "";
      msg_type_arr[235] = "";
      msg_type_arr[236] = "";
      msg_type_arr[237] = "";
      msg_type_arr[238] = "";
      msg_type_arr[239] = "";
      msg_type_arr[240] = "";
      msg_type_arr[241] = "";
      msg_type_arr[242] = "";
      msg_type_arr[243] = "";
      msg_type_arr[244] = "";
      msg_type_arr[245] = "";
      msg_type_arr[246] = "Attn";
      msg_type_arr[247] = "Crsel";
      msg_type_arr[248] = "Exsel";
      msg_type_arr[249] = "EraseEof";
      msg_type_arr[250] = "Play";
      msg_type_arr[251] = "Zoom";
      msg_type_arr[252] = "NoName";
      msg_type_arr[253] = "Pa1";
      msg_type_arr[254] = "OemClear";
      msg_type_arr[255] = "";
      
      return msg_type_arr[a];
   }
   public String getKeyData(int a)
   {
      if ( a == 256 || a == 257){
         String[] msg_type_arr = new String[2];
         msg_type_arr[0] = "KEYDOWN";
         msg_type_arr[1] = "KEYUP";
         return msg_type_arr[a];
      }
      else
         return null;
      
   }
}