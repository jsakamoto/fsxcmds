open System
open System.IO
open System.Net

// define parser for command line switches that returned parameters as a tuple.
let rec parseArg (defroot, defport, defhost) args =
    match args with
    | [] -> (defroot, defport, defhost)
    | "-root" :: root :: tail -> parseArg (root, defport, defhost) tail
    | "-port" :: port :: tail -> parseArg (defroot, port, defhost) tail
    | "-host" :: host :: tail -> parseArg (defroot, defport, host) tail
    | _ -> failwith ("Usage:\n" + "httpd [-root <path, default:current directory>] [-port <port number, default:8080>] [-host <host name, default:localhost>]")

// define http daemon.
let run = fun (root, port, host) ->
    let listener = new HttpListener()
    listener.Prefixes.Add ("http://" + host + ":" + port + "/")
    listener.Start ()

    printfn "\nStart %s at %s" (listener.Prefixes |> Seq.head) root
    printfn "Enter Ctrl + C to stop."

    // define main loop to process the request from http clinet such as web browser.
    let rec procreq ():(unit->unit) = 
        let context = listener.GetContext ()
        let url = context.Request.Url.LocalPath.TrimStart('/')
        let res = context.Response
        let path = Path.Combine (root, (url.Replace("/","\\")))
        match (File.Exists path) with
        | true -> path |> File.ReadAllBytes |> fun content -> res.OutputStream.Write(content, 0, content.Length)
        | false -> res.StatusCode <- 404
        res.Close ()
        procreq ()

    // launch main loop.
    procreq ()

// Call function to run the http daemon.
fsi.CommandLineArgs 
    |> Seq.skip 1
    |> Seq.toList
    |> parseArg (Directory.GetCurrentDirectory(), "8080", "localhost")
    |> run
