namespace Feliz.Mafs

open System

open Feliz

[<AutoOpen>]
module Util =
    [<ReactComponent>]
    let Katex text =
        Html.span [
            prop.innerHtml (Verbatim.Katex.renderToString text)
        ]

module Text =
    type Props =
        { text      : string
          pos       : Vec2
          attachDir : Direction
          attachDist: float
          size      : float
          color     : Color
          svgProps  : obj }
        static member Default =
            { text       = ""
              pos        = vec 0 0
              attachDir  = North
              attachDist = 0.0
              size       = 24.0
              color      = "#FFF"
              svgProps   = SVGProps }

    let create text = { Props.Default with text = text }
    let pos    pos      props = { props with pos       = pos   }: Props
    let size   size     props = { props with size      = size  }: Props
    let color  color    props = { props with color     = color }: Props
    let attach dir dist props = { props with attachDir = dir; attachDist = dist }: Props

    [<ReactComponent>]
    let render props =
        Verbatim.Text (props.text, x = props.pos.x, y = props.pos.y, attach = props.attachDir,
                       attachDistance = props.attachDist, size = props.size, color = props.color,
                       svgTextProps = props.svgProps)

    let point (dp: int) (pt: Vec2) textPos =
        let x = Math.Round (pt.x, dp)
        let y = Math.Round (pt.y, dp)
        create $"({x}, {y})"
        |> pos textPos
        |> attach South -12

module Latex =
    type Props =
        { tex         : string
          pos         : Vec2
          color       : Color
          katexOptions: KatexOptions }
        static member Default =
            { tex          = ""
              pos          = vec 0 0
              color        = Theme.foreground
              katexOptions = obj }

    let create tex = { Props.Default with tex = tex }
    let pos          pos          props = { props with pos          = pos          }
    let color        color        props = { props with color        = color        }
    let katexOptions katexOptions props = { props with katexOptions = katexOptions }

    [<ReactComponent>]
    let render props =
        Verbatim.LaTeX (props.tex, at = props.pos.array, color = props.color, katexOptions = props.katexOptions)
