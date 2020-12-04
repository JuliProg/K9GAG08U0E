![Create new chip](https://github.com/JuliProg/K9GAG08U0E/workflows/Create%20new%20chip/badge.svg?event=repository_dispatch) 
![ChipUpdate](https://github.com/JuliProg/K9GAG08U0E/workflows/ChipUpdate/badge.svg)
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

```
# Chip operations
```c#


            //------- Add chip operations    https://github.com/JuliProg/Wiki#command-set----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h_30h").
                   Operations("PageProgram_80h_10h");

```
# Initial Invalid Block (s)
```c#


            //------- Select the Initial Invalid Block (s) algorithm    https://github.com/JuliProg/Wiki/wiki/InitialInvalidBlock-----------

            myChip.InitialInvalidBlock = "InitInvalidBlock_v2";

```
# Chip registers (optional)
```c#


            //------- Add chip registers (optional)----------------------------------------------------

            myChip.registers.Add(                   // https://github.com/JuliProg/Wiki/wiki/StatusRegister
                "Status Register").
                Size(1).
                Operations("ReadStatus_70h").
                Interpretation("SR_Interpreted").
                UseAsStatusRegister();



            myChip.registers.Add(                  // https://github.com/JuliProg/Wiki/wiki/ID-Register
                "Id Register").
                Size(6).
                Operations("ReadId_90h");              
                //Interpretation(ID_interpreting);

```
# Interpretation of ID-register values ​​(optional)
```c#


        //public string ID_interpreting(Register register)   
        
```
</section>









footer
