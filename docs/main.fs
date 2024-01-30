namespace Feliz.Mafs.Docs

open Browser.Dom
open Feliz
open Feliz.Router

module Main =
    [<ReactComponent>]
    let Router () =
        let url, setUrl = React.useState (Router.currentUrl ())
        React.router [
            router.onUrlChanged setUrl
            router.children [
                match url with
                | []                -> IndexPage ()
                | [ "getstarted" ]  -> GetStartedPage ()
                | [ "text" ]        -> TextPage ()
                | [ "coordinates" ] -> CoordinatesPage ()
                | [ "camera" ]      -> CameraPage ()
                | [ "plots" ]       -> PlotsPage ()
                | _ -> Html.h1 "Not found"
            ]
        ]

    [<EntryPoint>]
    let main args =
        let root = ReactDOM.createRoot (document.getElementById "root")
        root.render (Router ())
        0
