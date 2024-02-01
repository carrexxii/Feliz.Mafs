namespace Feliz.Mafs

open Option

open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Geometry =
    let movablePoint (initial: Vec2) color = function
        | None       -> Verbatim.useMovablePoint (initial.array, color = color)
        | Horizontal -> Verbatim.useMovablePoint (initial.array, color = color, constrain = !^"horizontal")
        | Vertical   -> Verbatim.useMovablePoint (initial.array, color = color, constrain = !^"vertical")
        | Constrain fn ->
            let constrain p =
                fn (Vec2 p)
                |> _.array
            Verbatim.useMovablePoint (initial.array, color = color, constrain = !^constrain)

    module Point =
        type Props =
            { pos      : Vec2
              color    : Color
              opacity  : float
              constrain: Vec2 -> Vec2
              svgProps : SVGProps }
            static member Default =
                { pos       = Vec2 (0, 0)
                  color     = Theme.foreground
                  opacity   = 1.0
                  constrain = fun p -> p
                  svgProps  = null }

        let create pos = { Props.Default with pos = pos }
        let color     color     props = { props with color     = color     }
        let opacity   opacity   props = { props with opacity   = opacity   }
        let constrain constrain props = { props with constrain = constrain }
        let svgProps  svgProps  props = { props with svgProps  = svgProps  }

        [<ReactComponent>]
        let render props =
            Verbatim.Point (props.pos.x, props.pos.y, color = props.color,
                            opacity = props.opacity, svgCircleProps = props.svgProps)

    [<ReactComponent>]
    let Circle center radius =
        Verbatim.Circle (center = center, radius = radius)

    [<ReactComponent>]
    let Polygon points =
        Verbatim.Polygon (points = points)

    module Vector =
        type Props =
            { head     : Vec2
              tail     : Vec2
              color    : Color
              opacity  : float
              weight   : float
              style    : LineStyle
              lineProps: SVGProps }
            static member Default =
                { head      = Vec2 (0.0, 0.0)
                  tail      = Vec2 (0.0, 0.0)
                  color     = Theme.foreground
                  opacity   = 1.0
                  weight    = 2.0
                  style     = Solid
                  lineProps = null }

        let create head = { Props.Default with head = head }
        let tail     tail     props = { props with tail      = tail     }
        let color    color    props = { props with color     = color    }
        let opacity  opacity  props = { props with opacity   = opacity  }
        let weight   weight   props = { props with weight    = weight   }
        let style    style    props = { props with style     = style    }
        let svgProps svgProps props = { props with lineProps = svgProps }

        [<ReactComponent>]
        let render props =
            Verbatim.Vector (tip = props.head.array, tail = props.tail.array, color = props.color, opacity = props.opacity,
                             weight = props.weight, style = props.style, svgLineProps = props.lineProps)
