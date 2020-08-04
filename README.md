![Create new chip](https://github.com/JuliProg/K9GAG08U0E/workflows/Create%20new%20chip/badge.svg?event=repository_dispatch)
# Join the development of the project ([list of tasks](https://github.com/users/JuliProg/projects/1))


# K9GAG08U0E
Implementation of the K9GAG08U0E chip for the JuliProg programmer

Dependency injection, DI based on MEF framework is used to connect the chip to the programmer.

<section class = "listing">

#
```c#

    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype();
```
# Chip parameters
```c#


        ChipAssembly()
        {
            myChip.devManuf = "SAMSUNG";
            myChip.name = "K9GAG08U0E";
            myChip.chipID = "ECD584725042";      // device ID - ECh D5h 84h 72h 50h 42h (k9gag08u0e.pdf page 52)

            myChip.width = Organization.x8;    // chip width - 8 bit
            myChip.bytesPP = 8192;             // page size - 2048 byte (2Kb)
            myChip.spareBytesPP = 436;          // size Spare Area - 64 byte
            myChip.pagesPB = 128;               // the number of pages per block - 64 
            myChip.bloksPLUN = 2076;           // number of blocks in CE - 1024
            myChip.LUNs = 1;                   // the amount of CE in the chip
            myChip.colAdrCycles = 2;           // cycles for column addressing
            myChip.rowAdrCycles = 3;           // cycles for row addressing 
            myChip.vcc = Vcc.v3_3;             // supply voltage

```
# Chip operations
```c#


            //------- Add chip operations ----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h_30h").
                   Operations("PageProgram_80h_10h");

```
# Chip registers (optional)
```c#


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

```
# Interpretation of ID-register values ​​(optional)
```c#


        public string ID_interpreting(Register register)   
        
```
</section>



footer
