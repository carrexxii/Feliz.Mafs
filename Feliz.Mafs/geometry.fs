namespace Feliz.Mafs

open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Geometry =
    let movablePoint (initial: Vec2) color = function
        | None       -> Verbatim.useMovablePoint (initial.array, color = color)
        | Horizontal -> Verbatim.useMovablePoint (initial.array, color = color, constrain = !^"horizontal")
        | Vertical   -> Verbatim.useMovablePoint (initial.array, color = color, constrain = !^"vertical")
        | Constrain fn ->
            let constrain (p: float array) =
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

    module Line =
        type Type =
            | ThroughPoints
            | Segment
            | PointSlope
            | PointAngle
        type Props =
            { type'  : Type
              p1     : Vec2
              p2     : Vec2
              slope  : float
              angle  : Angle
              color  : Color
              opacity: float
              weight : float
              style  : LineStyle }
            static member Default =
                { type'   = ThroughPoints
                  p1      = vec 0 0
                  p2      = vec 0 0
                  slope   = 1.0
                  angle   = Radians 0.0
                  color   = Theme.foreground
                  opacity = 1.0
                  weight  = 2.0
                  style   = Solid }

        let create type' = { Props.Default with type' = type' }
        let point1  pt      props = { props with p1      = pt      }
        let point2  pt      props = { props with p2      = pt      }
        let angle   angle   props = { props with angle   = angle   }
        let color   color   props = { props with color   = color   }
        let opacity opacity props = { props with opacity = opacity }
        let weight  weight  props = { props with weight  = weight  }
        let style   style   props = { props with style   = style   }

        [<ReactComponent>]
        let render props =
            let color, opacity, weight, style = props.color, props.opacity, props.weight, props.style
            let p1 = props.p1.array
            let p2 = props.p2.array
            let slope = props.slope
            let angle = Angle.toRad props.angle
            match props.type' with
            | ThroughPoints -> Verbatim.Line.ThroughPoints (p1, p2   , color = color, opacity = opacity, weight = weight, style = style)
            | Segment       -> Verbatim.Line.LineSegment   (p1, p2   , color = color, opacity = opacity, weight = weight, style = style)
            | PointSlope    -> Verbatim.Line.PointSlope    (p1, slope, color = color, opacity = opacity, weight = weight, style = style)
            | PointAngle    -> Verbatim.Line.PointAngle    (p1, angle, color = color, opacity = opacity, weight = weight, style = style)

    [<ReactComponent>]
    let Circle center radius =
        Verbatim.Circle (center = center, radius = radius)

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

    module Polygon =
        type Props =
            { points     : Vec2 list
              color      : Color
              weight     : float
              opacity    : float
              lineOpacity: float
              lineStyle  : LineStyle
              svgProps   : SVGProps }
            static member Default =
                { points      = []
                  color       = Theme.foreground
                  weight      = 2.0
                  opacity     = 1.0
                  lineOpacity = 1.0
                  lineStyle   = Solid
                  svgProps    = null }

        let create points = { Props.Default with points = points }
        let color       color       props = { props with color       = color       }
        let weight      weight      props = { props with weight      = weight      }
        let opacity     opacity     props = { props with opacity     = opacity     }
        let lineOpacity lineOpacity props = { props with lineOpacity = lineOpacity }
        let lineStyle   lineStyle   props = { props with lineStyle   = lineStyle   }

        [<ReactComponent>]
        let render fill props =
            let points = List.map (fun (p: Vec2) -> p.array) props.points |> Array.ofList
            match fill with
            | true -> Verbatim.Polygon (points, color = props.color, weight = props.weight, fillOpacity = props.opacity,
                                        strokeOpacity = props.lineOpacity, strokeStyle = props.lineStyle, svgPolygonProps = props.svgProps)
            | false -> Verbatim.Polyline (points, color = props.color, weight = props.weight, fillOpacity = props.opacity,
                                          strokeOpacity = props.lineOpacity, strokeStyle = props.lineStyle, svgPolygonProps = props.svgProps)
