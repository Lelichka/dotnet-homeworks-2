open System
open Hw5

let createMessage message =
    match message with
        | Message.WrongArgLength -> "You have to pass 3 arguments"
        | Message.WrongArgFormat -> "Argument could not be converted to the double type"
        | Message.WrongArgFormatOperation -> "Unknown operation"
        | Message.DivideByZero -> "Divide by zero"
        
let printResult message status =
    printfn $"{message}"
    (int)status
    
[<EntryPoint>]
let Main args =
    match Parser.parseCalcArguments args with
        | Ok (arg1,operation,arg2) -> printResult (Calculator.calculate arg1 operation arg2) Message.SuccessfulExecution 
        | Error message -> printResult (createMessage message) message