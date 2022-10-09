open Hw4

[<EntryPoint>]
let main args =
    let parseArgs = Parser.parseCalcArguments args
    printfn $"{Calculator.calculate parseArgs.arg1 parseArgs.operation parseArgs.arg2}"
    0