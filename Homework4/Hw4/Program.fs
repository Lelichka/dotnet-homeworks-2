open System
open Hw4
open Hw4.Calculator

[<EntryPoint>]
let main args =
    let parseArgs = Parser.parseCalcArguments args
    if (parseArgs.operation = CalculatorOperation.Divide & parseArgs.arg2 = 0) then raise(ArgumentException "You can't divide by zero")
    printfn $"{Calculator.calculate parseArgs.arg1 parseArgs.operation parseArgs.arg2}"
    0