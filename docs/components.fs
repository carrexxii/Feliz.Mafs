namespace Feliz.Mafs.Docs

open Feliz

[<AutoOpen>]
module Components =
    [<ReactComponent>]
    let Heading text =
        Html.h1 [
            prop.className "head"
            prop.text (string text)
        ]

    [<ReactComponent>]
    let SubHeading text =
        Html.h2 [
            prop.className "subhead"
            prop.text (string text)
        ]

    [<ReactComponent>]
    let CodeBlock (text: string) =
        Html.code [
            prop.text text
        ]
