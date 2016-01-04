// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open CsvParserExample
[<EntryPoint>]
let main argv = 
    let transactions = Library.readFile ()
    let allVisaUsers = transactions |> List.filter(fun x -> x.paymentType = Models.Visa)
    allVisaUsers |> List.length |> printfn "There are %d Visa card users"
    0 // return an integer exit code
