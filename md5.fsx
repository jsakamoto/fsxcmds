open System.IO
open System.Text
open System.Security.Cryptography

let toBytes (s:string) = Encoding.UTF8.GetBytes(s)

let toBeHash = 
    match fsi.CommandLineArgs with
    |[|_;"-f";path|] -> File.ReadAllBytes(path)
    |[|_;"-h"|] -> failwith("Usage: md5 [{-f <path>|<string to be hash>}]")
    |[|_;str|] -> str |> toBytes
    |[|_|] -> System.Console.In.ReadToEnd() |> toBytes
    |_ -> failwith("Usage: md5 [{-f <path>|<string to be hash>}]")

let md5 (b:byte[]) = MD5.Create().ComputeHash(b)

let toHexStr (b:byte[]) = 
    b 
    |> Seq.map (sprintf "%02x")
    |> Seq.reduce (+)

toBeHash |> md5 |>  toHexStr |> printf "%s"

