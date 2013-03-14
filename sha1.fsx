open System.IO
open System.Text
open System.Security.Cryptography

let toBytes (s:string) = Encoding.UTF8.GetBytes(s)

let toBeHash = 
    match fsi.CommandLineArgs with
    |[|_;"-f";path|] -> File.ReadAllBytes(path)
    |[|_;"-h"|] -> failwith("Usage: sha1 [{-f <path>|<string to be hash>}]")
    |[|_;str|] -> str |> toBytes
    |[|_|] -> System.Console.In.ReadToEnd() |> toBytes
    |_ -> failwith("Usage: sha1 [{-f <path>|<string to be hash>}]")

let sha1 (b:byte[]) = SHA1.Create().ComputeHash(b)

let toHexStr (b:byte[]) = 
    b 
    |> Seq.map (sprintf "%02x")
    |> Seq.reduce (+)

toBeHash |> sha1 |>  toHexStr |> printf "%s"
