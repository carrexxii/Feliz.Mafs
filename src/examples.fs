module Examples

open Feliz

open SharpMafs

[<ReactComponent>]
let GetStarted () =
    Html.div [
        Html.h1 [
            prop.className "page-header"
            prop.text "Get Started Examples"
        ]
        Mafs [
            prop.key 1
            prop.children [
                Cartesian (true, true)
            ]
        ]
    ]
