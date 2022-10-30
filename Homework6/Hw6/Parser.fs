module Hw6.Parser

open System
open System.Globalization
open Hw6.Calculator
open Hw6.Message
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let convertMessage message =
    match message with
    | Message.SuccessfulExecution -> "SuccessfulExecution"
    | Message.WrongArgLength -> "WrongArgLength"
    | Message.WrongArgFormat -> "WrongArgFormat"
    | Message.WrongArgFormatOperation -> "WrongArgFormatOperation"
    | Message.DivideByZero -> "DivideByZero"
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]    
let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match args.Length with
        | 3 -> Ok args
        | _ -> Error ("You must enter 3 args")
    
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), string> =
    match operation with
        |Calculator.plus -> Ok (arg1,CalculatorOperation.Plus,arg2)
        |Calculator.minus -> Ok (arg1,CalculatorOperation.Minus,arg2)
        |Calculator.multiply -> Ok (arg1,CalculatorOperation.Multiply,arg2)
        |Calculator.divide -> Ok (arg1,CalculatorOperation.Divide,arg2)
        | _ -> Error $"Could not parse value '{operation}'"
        
        

let parseDouble (str:string):Result<Double,string> =
        match Double.TryParse (str, NumberStyles.Any, CultureInfo.InvariantCulture) with
            | true,value -> Ok value
            | _ -> Error $"Could not parse value '{str}'"
       
let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), string> =
    match isOperationSupported (args[0], args[1], args[2]) with
        | Ok (arg1,operation,arg2) -> 
            match parseDouble arg1 with
                | Ok arg1 ->
                    match parseDouble arg2 with
                        | Ok arg2 -> Ok (arg1, operation, arg2)
                        | Error er -> Error er
                |Error er -> Error er
        |Error er -> Error er
    
        
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), string> =
    match operation,arg2 with
        | (CalculatorOperation.Divide,0.0) -> Error (convertMessage Message.DivideByZero)
        | _ -> Ok (arg1,operation,arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe{
        let! checkLength = isArgLengthSupported args
        let! parsingArgs = parseArgs checkLength
        let! checkDivideByZero = isDividingByZero parsingArgs
        return checkDivideByZero
    } 