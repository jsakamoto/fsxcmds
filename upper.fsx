open System.Windows.Forms

let toBeCopy = 
    if fsi.CommandLineArgs.Length > 1
    then fsi.CommandLineArgs.[1]
    else System.Console.In.ReadToEnd()

printf "%s" <| toBeCopy.ToUpper()
