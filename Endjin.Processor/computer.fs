module Endjin.Computer 

(* THIS IS YOUR PROGRAM *)
let programInstructionMemory = [| 
    "load r0 1" 
    "add r0 1" 
    "write 0 r0"
    "read r1 0" 
    "load r2 4" 
    "compare r0 r2"
    "jumplt 2"
    "exit"
    "load r1 4"
    "exit" 
    |]

(* THIS IS THE END OF YOUR PROGRAM *)

(* THIS IS THE START OF THE CODE THAT 'RUNS' THE COMPUTER *)

let outputInHex = false

let debug = true

// Our computer has 10 bytes of memory
let memory : byte array = Array.zeroCreate 10
// 3 general purpose 1 byte registers + a flags register (R3)
let registers : byte array = Array.zeroCreate 5
// 3 16 bit registers (WR0-WR2) + a program counter
let wide_registers : uint16 array = Array.zeroCreate 4

type Token = | Load | Add | Sub | Mul | Div | Write | Read | Compare | Jump | JumpLT | JumpLTE | JumpGT | JumpGTE | JumpEQ | JumpNE | ShiftL | ShiftR | And | Or | XOr | Not | Exit | Byte of byte | Word of uint16 | R0 | R1 | R2 | PC | FL | WR0 | WR1 | WR2 

let split (value : System.String) = value.ToLower().Split([|' '|])

let numericFormatString = 
    if outputInHex then "{0:X2} " else "{0} "

let convertWordToUint16 (ok, i) =
    if ok then
        if i > 0 then
            uint16 i
        else if i < 0 then
            uint16 (i + 65536)
        else
            0us
    else
        0us

let parseDec s = 
    System.Int32.TryParse(s)

let parseHex s = 
    System.Int32.TryParse(s,System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture)

let parseBin s = 
    try
        let result = System.Convert.ToInt32(s,2)
        (true, result)
    with
        | _ -> (false,0)


let stow (s : string) = 
    if s.Length > 2 && s.[0..1] = "0x" then
         s.[2..] |> parseHex |> convertWordToUint16
    else if s.Length > 2 && s.[0..1] = "0b" then
         s.[2..] |> parseBin |> convertWordToUint16
    else if s.Length > 1 && s.[0] = '#' then
        s.[1..] |> parseHex |> convertWordToUint16
    else
        s |> parseDec |> convertWordToUint16

let convert s =
    let w = stow s
    if w > 255us then
        Word(w)
    else
        Byte(byte w)

let parseToken string =
    match string with
        | "load" -> Load
        | "add" -> Add
        | "sub" -> Sub
        | "mul" -> Mul
        | "div" -> Div
        | "write" -> Write
        | "read" -> Read
        | "compare" -> Compare
        | "shiftl" -> ShiftL
        | "shiftr" -> ShiftR
        | "and" -> And
        | "or" -> Or
        | "xor" -> XOr
        | "not" -> Not
        | "jump" -> Jump
        | "jumplt" -> JumpLT
        | "jumplte" -> JumpLTE
        | "jumpeq" -> JumpEQ
        | "jumpne" -> JumpNE
        | "jumpgt" -> JumpGT
        | "jumpgte" -> JumpGTE
        | "exit" -> Exit
        | "r0" -> R0
        | "r1" -> R1
        | "r2" -> R2
        | "wr0" -> WR0
        | "wr1" -> WR1
        | "wr2" -> WR2
        | "pc" -> PC
        | "fl" -> FL
        | value -> convert value

let tokenize (value : System.String) =
   // printf "%s" value
    value |> split |> Array.map parseToken

exception ParseError of string

let getByte token =
    match token with
    | R0 -> registers.[0]
    | R1 -> registers.[1]
    | R2 -> registers.[2]
    | FL -> registers.[3]
    | WR0 -> byte wide_registers.[0]
    | WR1 -> byte wide_registers.[1]
    | WR2 -> byte wide_registers.[2]
    | PC -> byte wide_registers.[3]
    | Byte x -> x
    | Word x -> byte x
    | _ -> raise (ParseError("Unable to evaluate expression"))

let getWord token =
    match token with
    | R0 -> uint16 registers.[0]
    | R1 -> uint16 registers.[1]
    | R2 -> uint16 registers.[2]
    | FL -> uint16 registers.[3]
    | WR0 -> wide_registers.[0]
    | WR1 -> wide_registers.[1]
    | WR2 -> wide_registers.[2]
    | PC -> wide_registers.[3]
    | Byte x -> uint16 x
    | Word x -> x
    | _ -> raise (ParseError("Unable to evaluate expression"))

let extend (word : uint16) =
    if (word &&& 0b0000000010000000us <> 0us) then
        word ||| 0xff00us
    else
        word &&& 0x00ffus

let getWordExtend token =
    match token with
    | R0 -> uint16 registers.[0] |> extend
    | R1 -> uint16 registers.[1] |> extend
    | R2 -> uint16 registers.[2] |> extend
    | FL -> uint16 registers.[3] |> extend
    | WR0 -> wide_registers.[0]
    | WR1 -> wide_registers.[1]
    | WR2 -> wide_registers.[2]
    | PC -> wide_registers.[3]
    | Byte x -> uint16 x |> extend
    | Word x -> x
    | _ -> raise (ParseError("Unable to evaluate expression"))

let load token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- getByte token2
    | R1 -> registers.[1] <- getByte token2
    | R2 -> registers.[2]  <- getByte token2
    | WR0 -> wide_registers.[0] <- getWord token2
    | WR1 -> wide_registers.[1] <- getWord token2
    | WR2 -> wide_registers.[2]  <- getWord token2
    | _ -> raise (ParseError("The first operand must be a register label"))

let add token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) + (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) + (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) + (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) + (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) + (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) + (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))

let binand token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) &&& (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) &&& (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) &&& (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) &&& (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) &&& (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) &&& (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))

let binor token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) ||| (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) ||| (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) ||| (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) ||| (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) ||| (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) ||| (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))

let binxor token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) ^^^ (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) ^^^ (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) ^^^ (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) ^^^ (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) ^^^ (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) ^^^ (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))

let binnot token1 =
    match token1 with
    | R0 -> registers.[0] <- ~~~(getByte token1)
    | R1 -> registers.[1] <- ~~~(getByte token1)
    | R2 -> registers.[2]  <- ~~~(getByte token1)
    | WR0 -> wide_registers.[0] <- ~~~(getWord token1)
    | WR1 -> wide_registers.[1] <- ~~~(getWord token1)
    | WR2 -> wide_registers.[2]  <- ~~~(getWord token1)
    | _ -> raise (ParseError("The first operand must be a register label"))

let sub token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) - (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) - (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) - (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) - (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) - (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) - (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))

let mul token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) * (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) * (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) * (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) * (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) * (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) * (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))

let div token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- (getByte token1) / (getByte token2)
    | R1 -> registers.[1] <- (getByte token1) / (getByte token2)
    | R2 -> registers.[2]  <- (getByte token1) / (getByte token2)
    | WR0 -> wide_registers.[0] <- (getWord token1) / (getWord token2)
    | WR1 -> wide_registers.[1] <- (getWord token1) / (getWord token2)
    | WR2 -> wide_registers.[2]  <- (getWord token1) / (getWord token2)
    | _ -> raise (ParseError("The first operand must be a register label"))


let writeword token1 value =
    let address = int (getWord token1)
    memory.[address + 1] <- byte (value >>> 8)
    memory.[address] <- byte (value)

let readword token1 =
    let address = int (getWord token1)
    let hibyte = memory.[address + 1] <<< 8
    let lobyte = memory.[address]
    uint16 (hibyte + lobyte)


let read token1 token2 =
    match token1 with
    | R0 -> registers.[0] <- memory.[int (getWord token2)]
    | R1 -> registers.[1] <- memory.[int (getWord token2)]
    | R2 -> registers.[2]  <- memory.[int (getWord token2)]
    | WR0 -> wide_registers.[0] <- readword token2
    | WR1 -> wide_registers.[1] <- readword token2
    | WR2 -> wide_registers.[2]  <- readword token2
    | _ -> raise (ParseError("The first operand must be a register label"))

let write token1 token2 =
    match token2 with
    | R0 -> memory.[int (getWord token1)] <- registers.[0]
    | R1 -> memory.[int (getWord token1)] <- registers.[1]
    | R2 -> memory.[int (getWord token1)] <- registers.[2]
    | WR0 -> writeword token1 wide_registers.[0]
    | WR1 -> writeword token1 wide_registers.[1]
    | WR2 -> writeword token1 wide_registers.[2]    
    | _ -> raise (ParseError("The second operand must be a register label"))

let comparison first second =
    if first = second then (registers.[3] &&& 248uy) ||| 0uy
    else if first < second then (registers.[3] &&& 248uy) ||| 1uy
    else (registers.[3] &&& 248uy) ||| 2uy

let compare token1 token2 =
    match token1 with
    | R0 -> registers.[3] <- comparison registers.[0] (getByte token2)
    | R1 -> registers.[3] <- comparison registers.[1] (getByte token2)
    | R2 -> registers.[3] <- comparison registers.[2] (getByte token2)
    | WR0 -> registers.[3] <- comparison wide_registers.[0] (getWord token2)
    | WR1 -> registers.[3] <- comparison wide_registers.[1] (getWord token2)
    | WR2 -> registers.[3] <- comparison wide_registers.[2] (getWord token2)    
    | _ -> raise (ParseError("The first operand must be a register label"))

let jump offset =
    wide_registers.[3] <- wide_registers.[3] + (offset - 1us)

let jumplt offset =
    if ((getByte FL) &&& 7uy) = 1uy then
        jump offset

let jumpgt offset =
    if ((getByte FL) &&& 7uy) = 2uy then
        jump offset

let jumpgte offset =
    let flval = ((getByte FL) &&& 7uy)
    if flval = 2uy || flval = 0uy then
        jump offset

let jumplte offset =
    let flval = ((getByte FL) &&& 7uy)
    if flval = 1uy || flval = 0uy then
        jump offset

let jumpeq offset =
    if ((getByte FL) &&& 7uy) = 0uy then
        jump offset

let jumpne offset =
    if ((getByte FL) &&& 7uy) <> 0uy then
        jump offset



let shiftl token offset = 
    match token with
    | R0 -> registers.[0] <- (getByte token) <<< offset
    | R1 -> registers.[1] <- (getByte token) <<< offset
    | R2 -> registers.[2]  <- (getByte token) <<< offset
    | WR0 -> wide_registers.[0] <- (getWord token) <<< offset
    | WR1 -> wide_registers.[1] <- (getWord token) <<< offset
    | WR2 -> wide_registers.[2]  <- (getWord token) <<< offset
    | _ -> raise (ParseError("The first operand must be a register label"))    

let shiftr token offset = 
    match token with
    | R0 -> registers.[0] <- (getByte token) >>> offset
    | R1 -> registers.[1] <- (getByte token) >>> offset
    | R2 -> registers.[2]  <- (getByte token) >>> offset
    | WR0 -> wide_registers.[0] <- (getWord token) >>> offset
    | WR1 -> wide_registers.[1] <- (getWord token) >>> offset
    | WR2 -> wide_registers.[2]  <- (getWord token) >>> offset
    | _ -> raise (ParseError("The first operand must be a register label"))    

let getOpCode (instruction : Token array) = 
    instruction.[0]

let evaluate (instruction : Token array) =
    match getOpCode instruction with
    | Load -> load instruction.[1] instruction.[2]
    | Add -> add instruction.[1] instruction.[2]
    | Sub -> sub instruction.[1] instruction.[2]
    | Mul -> mul instruction.[1] instruction.[2]
    | Div -> div instruction.[1] instruction.[2]
    | Write -> write instruction.[1] instruction.[2]
    | Read -> read instruction.[1] instruction.[2]
    | Compare -> compare instruction.[1] instruction.[2]
    | Jump -> jump (getWord instruction.[1])
    | JumpLT -> jumplt (getWord instruction.[1])
    | JumpGT -> jumpgt (getWord instruction.[1])
    | JumpLTE -> jumplte (getWord instruction.[1])
    | JumpGTE -> jumpgte (getWord instruction.[1])
    | JumpEQ -> jumpeq (getWord instruction.[1])
    | JumpNE -> jumpne (getWord instruction.[1])
    | ShiftL -> shiftl instruction.[1] (int (getWord instruction.[2]))
    | ShiftR -> shiftr instruction.[1] (int (getWord instruction.[2]))
    | And -> binand instruction.[1] instruction.[2]
    | Or -> binor instruction.[1] instruction.[2]
    | XOr -> binxor instruction.[1] instruction.[2]
    | Not -> binnot instruction.[1]
    | Exit -> wide_registers.[3] <- 65534us (* We're one less than 65535, as one will get added to the PC *)
    | _ -> raise (ParseError("Instruction not recognized"))

let HexFormat (h : byte[]) =
  let sb = System.Text.StringBuilder(h.Length * 2)
  let rec HexFormat' = function
    | _ as currIndex when currIndex = h.Length -> sb.ToString()
    | _ as currIndex when currIndex % 16 = 0 && currIndex > 0 -> 
        sb.AppendFormat("\n" + numericFormatString, h.[currIndex]) |> ignore
        HexFormat' (currIndex + 1)
    | _ as currIndex ->
        sb.AppendFormat(numericFormatString, h.[currIndex]) |> ignore
        HexFormat' (currIndex + 1)
  HexFormat' 0

let byteToString (h : byte) =
    let sb = System.Text.StringBuilder(2)
    if outputInHex then 
        sb.AppendFormat("{0:X2}",h) |> ignore
    else
        sb.AppendFormat("{0}", h) |> ignore
    sb.ToString()

let wordToString (h : uint16) =
    let sb = System.Text.StringBuilder(2)
    if outputInHex then 
        sb.AppendFormat("{0:X4}",h) |> ignore
    else 
        sb.AppendFormat("{0}", h) |> ignore
    sb.ToString()

let tokenToByteOutput (token : Token) =
    let byte = getByte token
    byteToString byte

let tokenToWordOutput (token : Token) =
    let word = getWord token
    wordToString word

let PrintMemory memstr =
    printfn "%s\n" memstr

let executeCurrentInstruction (program : System.String array) =
    let instruction = program.[int wide_registers.[3]]
    printfn "%s\n" instruction
    instruction |> tokenize |> evaluate
    wide_registers.[3] <- wide_registers.[3] + 1us


let run (program : System.String array) =
    let rec run' = function
    | _ as pc when int pc < program.Length -> 
        executeCurrentInstruction program
        if debug then
            printfn "R0=%s  R1=%s  R2=%s, WR0=%s, WR1=%s, WR2=%s, PC=%s, FL=%s" (tokenToByteOutput R0) (tokenToByteOutput R1) (tokenToByteOutput R2) (tokenToWordOutput WR0) (tokenToWordOutput WR1) (tokenToWordOutput WR2) (tokenToWordOutput PC) (tokenToByteOutput FL)
            memory |> HexFormat |> PrintMemory  
        run'(getWord PC)
    | _ as pc -> 
        printfn "\nCompleted successfully"
    run'(getWord PC)
    if (getWord PC) < 65535us then
        printfn "!!Unexpected end of program!!"


(* THIS IS THE END OF THE CODE THAT 'RUNS' THE COMPUTER *)

(* THIS RUNS YOUR PROGRAM *)

run programInstructionMemory