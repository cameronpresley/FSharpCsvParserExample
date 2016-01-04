namespace CsvParserExample

#if INTERACTIVE
#r "bin/Debug/FSharp.Data.dll"
#endif
module Models =
    open System
    
    type Payment = Visa | Mastercard | Amex | Diners | Unknown
    type Transaction = {
        date:DateTime; 
        product:string; 
        price:decimal; 
        paymentType:Payment; 
        name:string; 
        city:string; 
        state:string; 
        country:string; 
        accountCreated:DateTime
    }

module Library =
    open Models
    open FSharp.Data
    open FSharp.Data.CsvExtensions
    let private convertStringToPayment (input:string) =
        match input.ToLower() with
        | "mastercard" -> Mastercard
        | "visa" -> Visa
        | "amex" -> Amex
        | "diners" -> Diners
        | _ -> Unknown

    let private convertRowToTransaction (row:CsvRow) = {
        date=row?Transaction_date.AsDateTime();
        product=row?Product;
        price=row?Price.AsDecimal();
        paymentType=row?Payment_Type |> convertStringToPayment;
        name=row?Name;
        city=row?City;
        state=row?State;
        country=row?Country;
        accountCreated=row?Account_Created.AsDateTime()
    }

    let readFile () =
        let csv = CsvFile.Load("SalesJan2009.csv").Cache()
        csv.Rows |> Seq.map(fun x -> convertRowToTransaction x) |> Seq.toList
      
