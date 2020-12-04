using NAND_Prog;
using System;
using System.ComponentModel.Composition;

namespace K9GAG08U0E
{
    /*
     use the design :

      # region
         <some code>
      # endregion

    for automatically include <some code> in the READMY.md file in the repository
    */

    #region
    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype();
        #endregion


        #region Chip parameters

        ChipAssembly()
        {
            myChip.devManuf = "SAMSUNG";
            myChip.name = "K9GAG08U0E";
            myChip.chipID = "ECD584725042";     // device ID - ECh D5h 84h 72h 50h 42h (k9gag08u0e.pdf page 52)

            myChip.width = Organization.x8;     // chip width - 8 bit                  (k9gag08u0e.pdf page 9)
            myChip.bytesPP = 8192;              // page size - 8192 byte (8Kb)         (k9gag08u0e.pdf page 9)
            myChip.spareBytesPP = 436;          // size Spare Area - 436 byte          (k9gag08u0e.pdf page 9)
            myChip.pagesPB = 128;               // the number of pages per block - 128 (k9gag08u0e.pdf page 9)
            myChip.bloksPLUN = 2076;            // number of blocks in CE - 2076       (k9gag08u0e.pdf page 9)
            myChip.LUNs = 1;                    // the amount of CE in the chip        (k9gag08u0e.pdf page 9)
            myChip.colAdrCycles = 2;            // cycles for column addressing        (k9gag08u0e.pdf page 9)
            myChip.rowAdrCycles = 3;            // cycles for row addressing           (k9gag08u0e.pdf page 9)
            myChip.vcc = Vcc.v3_3;              // supply voltage                      (k9gag08u0e.pdf page 5)

            #endregion


            #region Chip operations

            //------- Add chip operations ----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h_30h").
                   Operations("PageProgram_80h_10h");

            #endregion

            #region Initial Invalid Block (s)

            //------- Select the Initial Invalid Block (s) algorithm    https://github.com/JuliProg/Wiki/wiki/InitialInvalidBlock-----------

            myChip.InitialInvalidBlock = "InitInvalidBlock_v2";

            #endregion

            #region Chip registers (optional)

            //------- Add chip registers (optional)----------------------------------------------------

            myChip.registers.Add(
                "Status Register").
                Size(1).
                Operations("ReadStatus_70h").
                Interpretation("SR_Interpreted").   //From ChipPart\SR_Interpreted.dll (https://github.com/JuliProg/Wiki/wiki/Status-Register-Interpretation)
                UseAsStatusRegister();



            myChip.registers.Add(
                "Id Register").
                Size(6).
                Operations("ReadId_90h");              
                //Interpretation(ID_interpreting);          // From here

            #endregion


        }

        //#region Interpretation of ID-register values ​​(optional)

        //public string ID_interpreting(Register register)   
        
        //#endregion
        //{
        //    byte[] content = register.GetContent();


        //    //BitConverter.ToString(register.GetContent(), 0, 1)
        //    //BitConverter.ToString(register.GetContent(), 1, 1)
        //    string messsage = "1st Byte    Maker Code = " + content[0].ToString("X2") + Environment.NewLine;
        //    messsage += ID_decoding(content[0],0) + Environment.NewLine;

        //    messsage += "2nd Byte    Device Code = " + content[1].ToString("X2") + Environment.NewLine;
        //    messsage += ID_decoding(content[1], 1) + Environment.NewLine;

        //    messsage += "3rd ID Data = " + content[2].ToString("X2") + Environment.NewLine;
        //    messsage += ID_decoding(content[2], 2) + Environment.NewLine;

        //    messsage += "4th ID Data = " + content[3].ToString("X2") + Environment.NewLine;
        //    messsage += ID_decoding(content[3], 3) + Environment.NewLine;

        //    messsage += "5th ID Data = " + content[4].ToString("X2") + Environment.NewLine;
        //    messsage += ID_decoding(content[4], 4) + Environment.NewLine;

        //    return messsage;
        //}  
        //private string ID_decoding(byte bt, int pos)
        //{
        //    string str_result = String.Empty;

        //    var IO = new System.Collections.BitArray(new[] { bt });

        //    switch (pos)
        //    {
        //        case 0:
        //            str_result += "Maker ";
        //            if (bt == 0xEC)
        //                str_result += "is Samsung";
        //            else
        //                str_result += "is not Samsung";
        //            str_result += Environment.NewLine;
        //            break;

        //        case 1:
        //            str_result += "Device ";
        //            if (bt == 0xF1)
        //                str_result += "is K9GAG08U0E";
        //            else
        //                str_result += "is not K9GAG08U0E";
        //            str_result += Environment.NewLine;
        //            break;

        //        case 2:
        //            str_result += " Internal Chip Number = ";
        //            if (IO[1] == false && IO[0] == false)
        //                str_result += "1";
        //            if (IO[1] == false && IO[0] == true)
        //                str_result += "2";
        //            if (IO[1] == true && IO[0] == false)
        //                str_result += "4";
        //            if (IO[1] == true && IO[0] == true)
        //                str_result += "8";
        //            str_result += Environment.NewLine;


        //            str_result += " Cell Type = ";
        //            if (IO[3] == false && IO[2] == false)
        //                str_result += "2 Level Cell";
        //            if (IO[3] == false && IO[2] == true)
        //                str_result += "4 Level Cell";
        //            if (IO[3] == true && IO[2] == false)
        //                str_result += "8 Level Cell";
        //            if (IO[3] == true && IO[2] == true)
        //                str_result += "16 Level Cell";
        //            str_result += Environment.NewLine;


        //            str_result += " Number of Simultaneously Programmed Pages = ";
        //            if (IO[5] == false && IO[4] == false)
        //                str_result += "1";
        //            if (IO[5] == false && IO[4] == true)
        //                str_result += "2";
        //            if (IO[5] == true && IO[4] == false)
        //                str_result += "4";
        //            if (IO[5] == true && IO[4] == true)
        //                str_result += "8";
        //            str_result += Environment.NewLine;


        //            str_result += " Interleave Program Between multiple chips = ";
        //            if (IO[6] == false)
        //                str_result += "Not Support";
        //            if (IO[6] == true)
        //                str_result += "Support";
        //            str_result += Environment.NewLine;

        //            str_result += " Cache Program = ";
        //            if (IO[7] == false)
        //                str_result += "Not Support";
        //            if (IO[7] == true)
        //                str_result += "Support";
        //            str_result += Environment.NewLine;
        //            break;

        //        case 3:

        //            str_result += " Page Size (w/o redundant area ) = ";
        //            if (IO[1] == false && IO[0] == false)
        //                str_result += "1KB";
        //            if (IO[1] == false && IO[0] == true)
        //                str_result += "2KB";
        //            if (IO[1] == true && IO[0] == false)
        //                str_result += "4KB";
        //            if (IO[1] == true && IO[0] == true)
        //                str_result += "8KB";
        //            str_result += Environment.NewLine;


        //            str_result += " Block Size (w/o redundant area ) = ";
        //            if (IO[5] == false && IO[4] == false)
        //                str_result += "64KB";
        //            if (IO[5] == false && IO[4] == true)
        //                str_result += "128KB";
        //            if (IO[5] == true && IO[4] == false)
        //                str_result += "256KB";
        //            if (IO[5] == true && IO[4] == true)
        //                str_result += "512KB";
        //            str_result += Environment.NewLine;


        //            str_result += " Redundant Area Size ( byte/512byte) = ";
        //            if (IO[2] == false)
        //                str_result += "8";
        //            if (IO[2] == true)
        //                str_result += "16";
        //            str_result += Environment.NewLine;


        //            str_result += " Organization = ";
        //            if (IO[6] == false)
        //                str_result += "x8";
        //            if (IO[6] == true)
        //                str_result += "x16";
        //            str_result += Environment.NewLine;

        //            str_result += " Serial Access Minimum = ";
        //            if (IO[7] == false && IO[3] == false)
        //                str_result += "50ns/30ns";
        //            if (IO[7] == true && IO[3] == false)
        //                str_result += "25ns";
        //            if (IO[7] == false && IO[3] == true)
        //                str_result += "Reserved";
        //            if (IO[7] == true && IO[3] == true)
        //                str_result += "Reserved";
        //            str_result += Environment.NewLine;
        //            break;

        //        case 4:

        //            str_result += " Plane Number = ";
        //            if (IO[3] == false && IO[2] == false)
        //                str_result += "1";
        //            if (IO[3] == false && IO[2] == true)
        //                str_result += "2";
        //            if (IO[3] == true && IO[2] == false)
        //                str_result += "4";
        //            if (IO[3] == true && IO[2] == true)
        //                str_result += "8";
        //            str_result += Environment.NewLine;


        //            str_result += " Plane Size (w/o redundant area ) = ";
        //            if (IO[6] == false && IO[5] == false && IO[4] == false)
        //                str_result += "64Mb";
        //            if (IO[6] == false && IO[5] == false && IO[4] == true)
        //                str_result += "128Mb";
        //            if (IO[6] == false && IO[5] == true && IO[4] == false)
        //                str_result += "256Mb";
        //            if (IO[6] == false && IO[5] == true && IO[4] == true)
        //                str_result += "512Mb";
        //            if (IO[6] == true && IO[5] == false && IO[4] == false)
        //                str_result += "1Gb";
        //            if (IO[6] == true && IO[5] == false && IO[4] == true)
        //                str_result += "2Gb";
        //            if (IO[6] == true && IO[5] == true && IO[4] == false)
        //                str_result += "4Gb";
        //            if (IO[6] == true && IO[5] == true && IO[4] == true)
        //                str_result += "8Gb";
        //            str_result += Environment.NewLine;


        //            break;
        //    }
        //    return str_result;
        //}

       
    }

}
