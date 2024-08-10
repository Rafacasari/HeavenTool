﻿//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace ACNH_BCSV.FileTypes
//{
//    //Decode based off of thakis's and shevious's python code, recoded in C#
//    //Encode was re-researched and programmed by Shadsterwolf
//    public static class Yaz0
//    {

//        public static string lerror = ""; // Gets the last error

//        #region Conversions
//        private static uint ReadSize(this BinaryFileReader br)
//        {
//            return ((uint) br.ReadByte() << 24) | ( (uint) br.ReadByte() << 16) | ((uint)br.ReadByte() << 8) | (br.ReadByte());
//        }

//        private static uint Makeu32(byte b1, byte b2, byte b3, byte b4) //32-bit change (uint, 0xFFFFFFFF)
//        {
//            return ((uint)b1 << 24) | ((uint)b2 << 16) | ((uint)b3 << 8) | (uint)b4;
//        }


//        private static byte[] Breaku32(uint u32) //Byte change from 32-bits (byte, 0xFF, 0xFF, 0xFF, 0xFF)
//        {
//            return new byte[] { (byte)(u32 >> 24), (byte)((u32 >> 16) & 0xFF), (byte)((u32 >> 8) & 0xFF), (byte)(u32 & 0xFF) };
//        }
//        #endregion



//        #region Decode

//        public static string Decompress(string inFilePath, string outFilePath)
//        {
//            using (var inFile = new BinaryFileReader(File.OpenRead(inFilePath)))
//            {
//                if (inFile.ReadString(4, Encoding.ASCII) != "Yaz0")
//                    return "This is not Yaz0 compressed!";

//                uint uncompressedSize = inFile.ReadSize();
//                uint fixedOffset = inFile.ReadSize();
//                MessageBox.Show($"{uncompressedSize} | {fixedOffset}");

//                uint writePos = 0;
//                byte[] decodedData = new byte[uncompressedSize];

//                inFile.BaseStream.Seek(0x10, SeekOrigin.Begin);

//                byte groupHead = 0;
//                uint validBitCount = 0;

//                while (writePos < uncompressedSize)
//                {
//                    if (validBitCount == 0)
//                    {
//                        groupHead = inFile.ReadByte();
//                        validBitCount = 8;
//                    }

//                    if ((groupHead & 0x80) != 0)
//                    {
//                        decodedData[writePos++] = inFile.ReadByte();
//                    }
//                    else
//                    {
//                        byte b1 = inFile.ReadByte();
//                        byte b2 = inFile.ReadByte();
//                        uint dist = (uint)((b1 & 0xF) << 8 | b2);
//                        uint copySource = writePos - (dist + 1);
//                        uint numBytes = (uint)b1 >> 4;

//                        if (numBytes == 0)
//                        {
//                            numBytes = (uint)inFile.ReadByte() + 0x12;
//                        }
//                        else
//                        {
//                            numBytes += 2;
//                        }

//                        for (int i = 0; i < numBytes; i++)
//                        {
//                            decodedData[writePos++] = decodedData[copySource++];
//                        }
//                    }

//                    groupHead <<= 1;
//                    validBitCount--;
//                }

//                File.WriteAllBytes(outFilePath, decodedData);
//            }

//            return "";
//        }
//        #endregion

//        #region Encode
//        public static string Encode(string inFile, string outFile) //Encode ---------------------------------------------------------
//        {
//            try
//            {
//                return Encode(System.IO.File.ReadAllBytes(inFile), outFile);
//            }
//            catch (System.Exception e)
//            {
//                return e.ToString();
//            }
//        }

//        public static string Encode(byte[] inFile, string outFile)
//        {
//            //Yaz0 check
//            if (inFile[0] == 'Y' || inFile[1] == 'a' || inFile[2] == 'z' || inFile[3] == '0')
//            {
//                return "File already compressed!";
//            }

//            uint uncompressedSize = (uint)inFile.Length; //0x04
//            uint dataOffset = Makeu32(inFile[0x0C], inFile[0x0D], inFile[0x0E], inFile[0x0F]);

//            //Encode Logic
//            uint sourcePos = 0; //start of data after header
//            uint writePos = 0;
//            uint groupPos;
//            uint copyPos;
//            byte[] encodedData = new byte[uncompressedSize + (uncompressedSize / 2)];
//            byte[] groupData = new byte[24]; //24 bytes at most in special cases, 16 if all normal pairs, 8 if all straight copy...
//            byte groupHeader;
//            string groupHeaderFlag; //To build the binary flags to clearly know what is being flagged, then convert to a byte.
//            uint numBytes = 0;
//            uint rleNumBytes;
//            uint copyNumBytes;
//            uint predictNumBytes;
//            uint predictCopyPos;
//            bool predictHit = false;
//            uint bufferPos;
//            uint seekPos;
//            ushort dataCalc;
//            uint fixedOffset;

//            if (dataOffset == 0x2000)
//                fixedOffset = dataOffset;
//            else
//                fixedOffset = 0x00;

//            //for (int i = 0; i < 190; i++) //debug
//            while (sourcePos < uncompressedSize)
//            {
//                Array.Clear(groupData, 0, groupData.Length);
//                groupHeaderFlag = "";
//                groupPos = 0; //first byte
//                while (groupHeaderFlag.Length < 8) //ensure number of Header Flags is less than 8, as group can be between 8-16 bytes
//                {
//                    rleNumBytes = 3; copyNumBytes = 3; predictNumBytes = 3; // reset
//                    copyPos = 0; predictCopyPos = 0;
//                    bool match = false;

//                    if (sourcePos != 0 && (sourcePos + 3) < uncompressedSize)
//                    {
//                        //RLE check
//                        if ((inFile[sourcePos] == inFile[sourcePos - 1]) && (inFile[sourcePos + 1] == inFile[sourcePos - 1]) && (inFile[sourcePos + 2] == inFile[sourcePos - 1]) && predictHit != true) //Match found for RLE/overlap
//                        {
//                            match = true;
//                            bufferPos = sourcePos + 3; //buffer source ahead
//                            copyPos = sourcePos - 1;
//                            if (bufferPos < uncompressedSize)
//                            {
//                                while (bufferPos < uncompressedSize && inFile[bufferPos] == inFile[sourcePos - 1] && rleNumBytes < (0xFF + 0xF + 3)) //while there is more data matching from that one byte... (don't ask about the math plz, even I am confused)
//                                {
//                                    rleNumBytes++;
//                                    bufferPos++;
//                                }
//                            }
//                        }
//                        //Copy check
//                        for (uint backPos = sourcePos - 1; backPos > 0 && (sourcePos - backPos) < 0xFFF; backPos--) //go backwards into the inFile data from current position and search for a matching pattern
//                        {
//                            if (inFile[sourcePos] == inFile[backPos] && inFile[sourcePos + 1] == inFile[backPos + 1] && inFile[sourcePos + 2] == inFile[backPos + 2]) //Match found for copy
//                            {
//                                match = true;
//                                seekPos = backPos + 3; //search ahead
//                                bufferPos = sourcePos + 3; //buffer source ahead

//                                if (copyPos == 0) //if there is no copy position recorded...
//                                    copyPos = (uint)backPos;

//                                uint instanceNumBytes = 4;
//                                if (bufferPos < uncompressedSize && seekPos < uncompressedSize)
//                                {
//                                    while (bufferPos < uncompressedSize && seekPos < uncompressedSize && inFile[bufferPos] == inFile[seekPos] && copyNumBytes < (0xFF + 0xF + 3)) //while there is more data matched, and the seek position is less than the source position...
//                                    {
//                                        if (copyPos != backPos) //if new potential position is found
//                                        {
//                                            if (copyNumBytes < instanceNumBytes) //if current numBytes is less than new instance, take new position and increment
//                                            {
//                                                copyPos = (uint)backPos;
//                                                copyNumBytes++;
//                                            }
//                                        }
//                                        else
//                                            copyNumBytes++;
//                                        instanceNumBytes++;
//                                        seekPos++;
//                                        bufferPos++;
//                                    }
//                                }
//                            }
//                            if (inFile[sourcePos + 1] == inFile[backPos] && inFile[sourcePos + 2] == inFile[backPos + 1] && inFile[sourcePos + 3] == inFile[backPos + 2]) //Predict
//                            {
//                                seekPos = backPos + 3; //search ahead
//                                bufferPos = sourcePos + 4; //buffer source ahead, predicted

//                                if (predictCopyPos == 0) //if there is no copy position recorded...
//                                    predictCopyPos = (uint)backPos;

//                                uint instanceNumBytes = 4;
//                                if (bufferPos < uncompressedSize && seekPos < uncompressedSize)
//                                {
//                                    while (bufferPos < uncompressedSize && seekPos < uncompressedSize && inFile[bufferPos] == inFile[seekPos] && predictNumBytes < (0xFF + 0xF + 3))
//                                    {
//                                        if (predictCopyPos != backPos) //if new potential position is found
//                                        {
//                                            if (predictNumBytes < instanceNumBytes) //if current numBytes is less than new instance, take new position and increment numBytes
//                                            {
//                                                predictCopyPos = (uint)backPos;
//                                                predictNumBytes++;
//                                            }
//                                        }
//                                        else
//                                            predictNumBytes++;
//                                        instanceNumBytes++;
//                                        seekPos++;
//                                        bufferPos++;
//                                    }
//                                }
//                            }

//                            //if (sourcePos >= 0x3DA9 && (sourcePos - backPos) > 3835) //debug encode
//                            //System.Windows.Forms.MessageBox.Show("SourcePos: 0x" + sourcePos.ToString("X") + "\n" + "searchPos: " + "0x" + backPos.ToString("X") + "\n" + "copyPos: 0x" + copyPos.ToString("X") + "\n" + "predictCopyPos: 0x" + predictCopyPos.ToString("X") + "\n" + "dist: " + (sourcePos - backPos) + "\n" + "copyNumBytes: " + copyNumBytes + "\n" + "predictNumBytes: " + predictNumBytes);
//                        }
//                        predictHit = false; //reset prediction
//                        if (rleNumBytes >= copyNumBytes) //use RLE number of bytes unless copyNumBytes found a better match
//                            numBytes = rleNumBytes;
//                        else
//                            numBytes = copyNumBytes;
//                        if (predictNumBytes > numBytes)
//                        {
//                            match = false; //flag the next byte as straight copy because the next one will solve one copy instead of two. (End up using 3 bytes instead of 4)
//                            predictHit = true;
//                        }
//                    }
//                    if (match) //Flag for RLE/copy
//                    {
//                        if (numBytes > 18)
//                            dataCalc = (ushort)(((0x0) << 12) | (sourcePos - copyPos) - 1); //Mark the 4-bits all 0 to reference the 3rd byte to copy
//                        else
//                            dataCalc = (ushort)(((numBytes - 2) << 12) | (sourcePos - copyPos) - 1); //Calculate the pair
//                        groupData[groupPos] = (byte)(dataCalc >> 8); //b1
//                        groupData[groupPos + 1] = (byte)(dataCalc & 0xFF); //b2
//                        groupPos += 2;
//                        sourcePos += numBytes; //add by how many copies
//                        groupHeaderFlag += "0";
//                        if (numBytes >= 18) //if numBytes is greater than 18, but it will be used to accomodate the large number of bytes to copy, do not flag nor increment as it's part of the pair
//                        {
//                            groupData[groupPos] = (byte)(numBytes - 18);
//                            groupPos++;
//                        }
//                    }
//                    else if (sourcePos + 1 > uncompressedSize) //End of encryption
//                    {
//                        groupHeaderFlag += "0";
//                        sourcePos++;
//                    }
//                    else //Flag for Straight copy
//                    {
//                        groupData[groupPos] = inFile[sourcePos];
//                        groupPos++;
//                        sourcePos++;
//                        groupHeaderFlag += "1";
//                    }

//                }//end while

//                groupHeader = (byte)Convert.ToInt32(groupHeaderFlag, 2);

//                encodedData[writePos] = groupHeader;
//                writePos++;
//                for (int k = 0; k < groupPos; k++)
//                {
//                    encodedData[writePos] = groupData[k];
//                    writePos++;
//                }

//            }//end while
//             //Write all of the encoded data
//            System.IO.StreamWriter stream = new System.IO.StreamWriter(outFile);
//            stream.BaseStream.Write(new byte[] { 89, 97, 122, 48 }, 0, 4); //Yaz0 
//            stream.BaseStream.Write(Breaku32(uncompressedSize), 0, 4); //uncompressed size
//            stream.BaseStream.Write(Breaku32(fixedOffset), 0, 4); //fixedOffset
//            stream.BaseStream.Write(new byte[] { 00, 00, 00, 00 }, 0, 4); //End Header
//            stream.BaseStream.Write(encodedData, 0, ((int)writePos));

//            stream.Close();
//            stream.Dispose();
//            GC.Collect();

//            return "";
//        }
//        #endregion

//    }
//}