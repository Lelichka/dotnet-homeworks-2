module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
        | "+" -> CalculatorOperation.Plus
        | "-" -> CalculatorOperation.Minus
        | "*" -> CalculatorOperation.Multiply
        | "/" -> CalculatorOperation.Divide
        | _ -> raise(ArgumentException "Unknown operation")
        
let parseArgument(str: string)(argNumb:string)  =
    match Double.TryParse str with
    | true,arg -> arg
    | _ -> raise(ArgumentException(argNumb+" argument could not be converted to the double type"))
    
let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then raise(ArgumentException "You have to pass 3 arguments")

    { arg1 = parseArgument args[0] "First"
      arg2 = parseArgument args[2] "Second"
      operation = parseOperation args[1] }
    
    

    
    
    
    

