namespace Feliz.Mafs

open System

open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Geometry =
    type MovablePoint =
        { element : ReactElement
          pos     : Vec2
          setPoint: Vec2 -> unit
          x       : float
          y       : float }
        static member Default =
            { element  = Html.div []
              pos      = vec 0 0
              setPoint = fun p -> ()
              x        = 0.0
              y        = 0.0 }

    let movablePoint (initial: Vec2) color constrain =
        match constrain with
        | None            -> Verbatim.useMovablePoint (initial.array, color = color)
        | Some Horizontal -> Verbatim.useMovablePoint (initial.array, color = color, constrain = !^"horizontal")
        | Some Vertical   -> Verbatim.useMovablePoint (initial.array, color = color, constrain = !^"vertical")
        | Some (Constrain fn) ->
            let constrain (p: float array) =
                fn (Vec2 p)
                |> _.array
            Verbatim.useMovablePoint (initial.array, color = color, constrain = !^constrain)
        |> fun mvp -> { pos      = Vec2 mvp.point
                        element  = mvp.element
                        setPoint = fun p -> mvp.setPoint p.array
                        x        = mvp.x
                        y        = mvp.y }

    module Arc =
        type Props =
            { pos  : Vec2
              v1   : Vec2
              v2   : Vec2
              r    : float
              color: Color }
            static member Default =
                { pos   = vec 0 0
                  v1    = vec 1 0
                  v2    = vec 0 1
                  r     = 0.5
                  color = Theme.foreground }

        let create pos = { Props.Default with pos = pos }
        let targets targets props = { props with v1 = fst targets; v2 = snd targets }
        let color   color   props = { props with color = color }

        [<ReactComponent>]
        let render props =
            Parametric.create <| fun t -> vec (props.pos.x + props.r*cos t)
                                              (props.pos.y + props.r*sin t)
            |> Parametric.color props.color
            |> Parametric.render (vec
                (props.v1 |> fun p -> -Math.Atan2 (-p.x, -p.y) + Math.PI + Math.PI/2.0)
                (props.v2 |> fun p -> -Math.Atan2 (-p.x, -p.y) + Math.PI + Math.PI/2.0))

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

    module Circle =
        type Props =
            { center     : Vec2
              radius     : float
              angle      : Angle
              color      : Color
              weight     : float
              opacity    : float
              lineOpacity: float
              lineStyle  : LineStyle
              svgProps   : SVGProps }
            static member Default =
                { center      = vec 0 0
                  radius      = 1.0
                  angle       = Radians 0.0
                  color       = Theme.foreground
                  weight      = 2.0
                  opacity     = 0.7
                  lineOpacity = 1.0
                  lineStyle   = Solid
                  svgProps    = null }

        let create radius = { Props.Default with radius = radius }
        let center      center      props = { props with center      = center      }
        let angle       angle       props = { props with angle       = angle       }
        let color       color       props = { props with color       = color       }
        let weight      weight      props = { props with weight      = weight      }
        let opacity     opacity     props = { props with opacity     = opacity     }
        let lineOpacity lineOpacity props = { props with lineOpacity = lineOpacity }
        let lineStyle   lineStyle   props = { props with lineStyle   = lineStyle   }
        let svgProps    svgProps    props = { props with svgProps    = svgProps    }

        [<ReactComponent>]
        let render props =
            Verbatim.Circle (props.center.array, props.radius, angle = Angle.toRad props.angle, color = props.color,
                             weight = props.weight, fillOpacity = props.opacity, strokeOpacity = props.lineOpacity,
                             strokeStyle = props.lineStyle, svgEllipseProps = props.svgProps)

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
        let render joinLast props =
            let points = List.map (fun (p: Vec2) -> p.array) props.points |> Array.ofList
            match joinLast with
            | true -> Verbatim.Polygon (points, color = props.color, weight = props.weight, fillOpacity = props.opacity,
                                        strokeOpacity = props.lineOpacity, strokeStyle = props.lineStyle, svgPolygonProps = props.svgProps)
            | false -> Verbatim.Polyline (points, color = props.color, weight = props.weight, fillOpacity = props.opacity,
                                          strokeOpacity = props.lineOpacity, strokeStyle = props.lineStyle, svgPolygonProps = props.svgProps)
