module Main

open Browser.Dom
open Feliz
open Feliz.Router

[<ReactComponent>]
let Router () =
    let (url, setUrl) = React.useState (Router.currentUrl ())
    React.router [
        router.onUrlChanged setUrl
        router.children [
            match url with
            | [] -> Examples.GetStarted ()
            | _ -> Html.h1 "Not found"
        ]
    ]

[<EntryPoint>]
let main args =
    let root = ReactDOM.createRoot (document.getElementById "root")
    root.render (Router ())
    0
