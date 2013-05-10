open System.IO
open System.Text
open System.Security.Cryptography

let toBytes (s:string) = Encoding.UTF8.GetBytes(s)

let toBeHash = function
    |[|_;"-f";path|] -> path |> File.ReadAllBytes
    |[|_;"-h"|] -> failwith("Usage: sha1 [{-f <path>|<string to be hash>}]")
    |[|_;str|] -> str |> toBytes
    |[|_|] -> System.Console.In.ReadToEnd() |> toBytes
    |_ -> failwith("Usage: sha1 [{-f <path>|<string to be hash>}]")

let sha256 (b:byte[]) = SHA256.Create().ComputeHash(b)

let toHexStr = Seq.map (sprintf "%02x") >> Seq.reduce (+)

fsi.CommandLineArgs |> toBeHash |> sha256 |>  toHexStr |> printf "%s"
