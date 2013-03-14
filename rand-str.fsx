open System.Linq
open System.Security.Cryptography

//let charset = (['0'..'9'] @ ['A'..'Z'] @ ['a'..'z']).ToArray()
let charset = (['0'..'9'] @ ['a'..'z']).ToArray()

let genBytes len =
    let (buff:byte[]) = Array.zeroCreate(len)
    (new RNGCryptoServiceProvider()).GetBytes(buff)
    buff

let index (charset:char[]) (b:byte) = 
    int(floor(double(charset.Length-1) * double(b) / double(System.Byte.MaxValue)))

let randStr len = 
    genBytes len 
    |> Seq.map (index charset) 
    |> Seq.map (fun i -> charset.[i].ToString())
    |> Seq.reduce (+)

let len = 
    if fsi.CommandLineArgs.Length > 1
    then int(fsi.CommandLineArgs.[1])
    else failwith("no command line argument")

printf "%s" (randStr len)