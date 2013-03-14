open System
open System.IO
open System.Text
open System.Text.RegularExpressions

let toBytes (s:string) = Encoding.UTF8.GetBytes(s)
let toBytesFromHexStr s =
    s
    |> fun s -> Regex.Replace(s, "[^0-9a-fA-F]", "")
    |> fun s -> ([|0..(s.Length-1)/2|] |> Seq.map (fun n -> s.Substring(n*2,2)))
    |> Seq.map (fun s -> Convert.ToByte(s, 16))
    |> Seq.toArray

let toBeBase64 = 
    match fsi.CommandLineArgs with
    |[|_;"-f";path|] -> File.ReadAllBytes(path)
    |[|_;"-x";hex|] -> toBytesFromHexStr hex
    |[|_;"-x"|] -> System.Console.In.ReadToEnd() |> toBytesFromHexStr
    |[|_;"-h"|] -> failwith("\nUsage: tobase64 [{-f <path>|-x <hex>|<string>}]")
    |[|_;str|] -> str |> toBytes
    |[|_|] -> System.Console.In.ReadToEnd() |> toBytes
    |_ -> failwith("\nUsage: tobase64 [{-f <path>|-h <hex>|<string>}]")

toBeBase64
    |> Convert.ToBase64String
    |> printf "%s"
