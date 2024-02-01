namespace Feliz.Mafs

open System

open Feliz

module Text =
    type Props =
        { text      : string
          pos       : float array
          attachDir : Direction
          attachDist: float
          size      : float
          color     : Color
          svgProps  : obj }
        static member Default =
            { text       = ""
              pos        = [| 0.0; 0.0 |]
              attachDir  = North
              attachDist = 0.0
              size       = 24.0
              color      = "#FFF"
              svgProps   = SVGProps }

    let create text pos = { Props.Default with text = text; pos = pos }
    let attach dir dist props = { props with attachDir = dir; attachDist = dist }: Props
    let size   size     props = { props with size  = size  }: Props
    let color  color    props = { props with color = color }: Props

    [<ReactComponent>]
    let render props =
        Verbatim.Text (props.text, x = props.pos[0], y = props.pos[1], attach = props.attachDir,
                       attachDistance = props.attachDist, size = props.size, color = props.color,
                       svgTextProps = props.svgProps)

    [<ReactComponent>]
    let Katex text =
        Html.span [
            prop.innerHtml (Verbatim.Katex.renderToString text)
        ]

    let point (dp: int) (pt: float array) pos =
        let x = Math.Round (pt[0], dp)
        let y = Math.Round (pt[1], dp)
        create $"({x}, {y})" pos
        |> attach South -12
