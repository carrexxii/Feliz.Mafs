module Feliz.Mafs

open Fable.Core
open Fable.Core.JsInterop
open Browser.Types
open Feliz

[<AutoOpen>]
module Types =
    [<StringEnum>] type Auto    = Auto
    [<StringEnum>] type Contain = Contain
    
    type Vec2 = float array
    type Matrix = float array

    type ViewBox =
        { x      : Vec2
          y      : Vec2
          padding: float }
        static member Default =
            { x       = [| -3; 3 |]
              y       = [| -3; 3 |]
              padding = 0.5 }

    type Range =
        { min: float
          max: float }

    [<StringEnum>]
    type Constrain =
        | Horizontal
        | Vertical

    type SVGProps = obj

    [<StringEnum>]
    type StrokeStyle =
        | Solid
        | Dashed

    module Labels =
        [<Import("labelPi", "mafs")>]
        let Pi = jsNative<int -> string>

    [<Emit("UseMovablePoint")>]
    type MovablePoint =
        { element : ReactElement
          point   : Vec2
          setPoint: Vec2 -> unit
          x       : float
          y       : float }

    type UseMovablePointArguments =
        { color    : string
          constrain: Constrain }

    [<StringEnum>]
    type Direction =
        | [<CompiledName("n")>] North | [<CompiledName("ne")>] NorthEast
        | [<CompiledName("e")>] East  | [<CompiledName("se")>] SouthEast
        | [<CompiledName("s")>] South | [<CompiledName("sw")>] SouthWest
        | [<CompiledName("w")>] West  | [<CompiledName("nw")>] NorthWest

    type MafsProps =
        { width  : U2<float, Auto>
          height : float
          pan    : bool
          zoom   : U2<bool, Range>
          viewBox: ViewBox
          preserveAspectRatio: U2<bool, Contain>
          ssr    : bool
          onClick: Vec2 -> MouseEvent -> unit }
        static member Default =
            { width   = !^Auto
              height  = 500
              pan     = true
              zoom    = !^false
              viewBox = ViewBox.Default
              onClick = fun _ _ -> ()
              ssr     = false
              preserveAspectRatio = !^Contain }

    type Axis =
        { lines : float
          labels: int -> string }
        static member Default =
            { lines  = 1.0
              labels = fun x -> $"{x}" }
    type CartesianProps =
        { xAxis : U2<bool, Axis>
          yAxis : U2<bool, Axis>
          subDiv: float }
        static member Default =
            { xAxis  = !^Axis.Default
              yAxis  = !^Axis.Default
              subDiv = 1 }
    type PolarProps =
        { xAxis : U2<bool, Axis>
          yAxis : U2<bool, Axis>
          lines : float
          subDiv: float }
        static member Default =
            { xAxis  = !^Axis.Default
              yAxis  = !^Axis.Default
              lines  = 1
              subDiv = 1 }

    type TextProps =
        { x             : float
          y             : float
          attach        : Direction
          attachDistance: float
          size          : float
          color         : string
          svgTextProps  : obj }
        static member Default =
            { x              = 0.0
              y              = 0.0
              attach         = North
              attachDistance = 0.0
              size           = 1.0
              color          = "#FFF"
              svgTextProps   = SVGProps }
    
    [<StringEnum>]
    type LineStyle =
        | Solid
        | Dashed
    
    type Color = string

    [<StringEnum>]
    type Inequality =
        | [<CompiledName(">" )>] GT
        | [<CompiledName(">=")>] GTEQ
        | [<CompiledName("<" )>] LT
        | [<CompiledName("<=")>] LTEQ

    type InequalityPair =
        { lower: Inequality * (float -> float)
          upper: Inequality * (float -> float) }
        static member Default =
            { lower = GT, (fun x -> x)
              upper = LTEQ, (fun y -> 5) }

    type KatexOptions = obj

    type XYAxis =
        | XAxis
        | YAxis

    type PlotProps =
        { fn              : float -> float
          color           : Color
          opacity         : float
          weight          : float
          style           : LineStyle
          minSamplingDepth: float
          maxSamplingDepth: float }
        static member Default =
            { fn               = fun x -> x
              color            = "blue"
              opacity          = 1.0
              weight           = 2.0
              style            = Solid
              minSamplingDepth = 8
              maxSamplingDepth = 15 }

type ITheme =
    abstract foreground: Color
    abstract background: Color
    abstract red       : Color
    abstract orange    : Color
    abstract green     : Color
    abstract blue      : Color
    abstract indigo    : Color
    abstract violet    : Color
    abstract pink      : Color
    abstract yellow    : Color
let Theme: ITheme = import "Theme" "mafs"

type Verbatim =
    [<ReactComponent(import="Mafs", from="mafs")>]
    static member Mafs (children: IReactProperty list,
                        ?width  : U2<float, Auto>,
                        ?height : float,
                        ?pan    : bool,
                        ?zoom   : U2<bool, Range>,
                        ?viewBox: ViewBox,
                        ?preserveAspectRatio: U2<bool, Contain>,
                        ?ssr    : bool,
                        ?onClick: Vec2 -> MouseEvent -> unit) = React.imported ()
    [<ReactComponent>]
    static member Mafs (children: ReactElement list,
                        props   : MafsProps) =
        Verbatim.Mafs (children = (prop.key 1)::[prop.children children], width = props.width, height = props.height,
                       pan = props.pan, zoom = props.zoom, viewBox = props.viewBox,
                       preserveAspectRatio = props.preserveAspectRatio, ssr = props.ssr, onClick = props.onClick)

    [<ReactComponent(import="Coordinates.Cartesian", from="mafs")>]
    static member Cartesian (?xAxis       : U2<bool, Axis>,
                             ?yAxis       : U2<bool, Axis>,
                             ?subdivisions: float) = React.imported ()

    [<ReactComponent(import="Coordinates.Polar", from="mafs")>]
    static member Polar (?xAxis       : U2<bool, Axis>,
                         ?yAxis       : U2<bool, Axis>,
                         ?lines       : float,
                         ?subdivisions: float) = React.imported ()

    [<ReactComponent(import="Point", from="mafs")>]
    static member Point (x              : float,
                         y              : float,
                         ?color         : Color,
                         ?opacity       : float,
                         ?svgCircleProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Vector", from="mafs")>]
    static member Vector (tip          : Vec2,
                          ?tail        : Vec2,
                          ?color       : Color,
                          ?opacity     : float,
                          ?weight      : float,
                          ?style       : LineStyle,
                          ?svgLineProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Line.ThroughPoints", from="mafs")>]
    static member ThroughPoints (point1  : Vec2,
                                 point2  : Vec2,
                                 ?color  : Color,
                                 ?opacity: float,
                                 ?weight : float,
                                 ?style  : LineStyle) = React.imported ()

    [<ReactComponent(import="Line.Segment", from="mafs")>]
    static member LineSegment (point1  : Vec2,
                               point2  : Vec2,
                               ?color  : Color,
                               ?opacity: float,
                               ?weight : float,
                               ?style  : LineStyle) = React.imported ()

    [<ReactComponent(import="Line.PointSlope", from="mafs")>]
    static member PointSlope (point   : Vec2,
                              slope   : float,
                              ?color  : Color,
                              ?opacity: float,
                              ?weight : float,
                              ?style  : LineStyle) = React.imported ()

    [<ReactComponent(import="Line.PointAngle", from="mafs")>]
    static member PointAngle (point   : Vec2,
                              angle   : float,
                              ?color  : Color,
                              ?opacity: float,
                              ?weight : float,
                              ?style  : LineStyle) = React.imported ()

    [<ReactComponent(import="Circle", from="mafs")>]
    static member Circle (center          : Vec2,
                          radius          : float,
                          ?angle          : float,
                          ?color          : Color,
                          ?weight         : float,
                          ?fillOpacity    : float,
                          ?strokeOpacity  : float,
                          ?strokeStyle    : LineStyle,
                          ?svgEllipseProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Ellipse", from="mafs")>]
    static member Ellipse (center          : Vec2,
                           radius          : Vec2,
                           ?color          : Color,
                           ?weight         : float,
                           ?fillOpacity    : float,
                           ?strokeOpacity  : float,
                           ?strokeStyle    : LineStyle,
                           ?svgEllipseProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Polygon", from="mafs")>]
    static member Polygon (points          : Vec2 array,
                           ?color          : Color,
                           ?weight         : float,
                           ?fillOpacity    : float,
                           ?strokeOpacity  : float,
                           ?strokeStyle    : StrokeStyle,
                           ?svgPolygonProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Polyline", from="mafs")>]
    static member Polyline (points          : Vec2 array,
                            ?color          : Color,
                            ?weight         : float,
                            ?fillOpacity    : float,
                            ?strokeOpacity  : float,
                            ?strokeStyle    : StrokeStyle,
                            ?svgPolygonProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Text", from="mafs")>]
    static member Text (children       : string,
                        x              : float,
                        y              : float,
                        ?attach        : Direction,
                        ?attachDistance: float,
                        ?size          : float,
                        ?color         : Color,
                        ?svgTextProps  : SVGProps) = React.imported ()

    [<ReactComponent(import="LaTeX", from="mafs")>]
    static member LaTeX (tex         : string,
                         at          : Vec2,
                         color       : Color,
                         katexOptions: KatexOptions) = React.imported ()

    [<ReactComponent(import="Plot.OfX", from="mafs")>]
    static member OfX (y                : float -> float,
                       ?color           : Color,
                       ?opacity         : float,
                       ?weight          : float,
                       ?style           : LineStyle,
                       ?minSamplingDepth: float,
                       ?maxSamplingDepth: float) = React.imported ()

    [<ReactComponent(import="Plot.OfY", from="mafs")>]
    static member OfY (x                : float -> float,
                       ?color           : Color,
                       ?opacity         : float,
                       ?weight          : float,
                       ?style           : LineStyle,
                       ?minSamplingDepth: float,
                       ?maxSamplingDepth: float) = React.imported ()

    // x / y -> { ">"?: FnX; "<="?: FnX; "<"?: FnX | undefined; ">="?: FnX | undefined; } | undefined
    [<ReactComponent(import="Plot.Inequality", from="mafs")>]
    static member Inequality (?y                : obj,
                              ?x                : obj,
                              ?color            : Color,
                              ?weight           : float,
                              ?strokeColor      : Color,
                              ?strokeOpacity    : float,
                              ?fillColor        : Color,
                              ?fillOpacity      : float,
                              ?minSamplingDepth : float,
                              ?maxSamplingDepth : float,
                              ?upperColor       : Color,
                              ?upperOpacity     : float,
                              ?upperWeight      : float,
                              ?lowerColor       : Color,
                              ?lowerOpacity     : float,
                              ?lowerWeight      : float,
                              ?svgUpperPathProps: SVGProps,
                              ?svgLowerPathProps: SVGProps,
                              ?svgFillPathProps : SVGProps) = React.imported ()

    [<ReactComponent(import="Plot.Parametric", from="mafs")>]
    static member Parametric (xy               : float -> Vec2,
                              t                : Vec2,
                              ?color           : Color,
                              ?opacity         : float,
                              ?weight          : float,
                              ?style           : LineStyle,
                              ?minSamplingDepth: float,
                              ?maxSamplingDepth: float,
                              ?svgPathProps    : SVGProps) = React.imported ()

    [<ReactComponent(import="Plot.VectorField", from="mafs")>]
    static member VectorField (xy         : Vec2 -> Vec2,
                               xyOpacity  : Vec2 -> float,
                               step       : float,
                               opacityStep: float,
                               color      : Color) = React.imported ()

    [<ReactComponent(import="Transform", from="mafs")>]
    static member Transform (children  : ReactElement list,
                             ?matrix   : Matrix,
                             ?translate: Vec2,
                             ?scale    : U2<float, Vec2>,
                             ?rotate   : float,
                             ?shear    : Vec2) = React.imported ()
    
    [<ReactComponent(import="Debug.TransformWidget", from="mafs")>]
    static member DebugTransformWidget (children: ReactElement list) = React.imported ()
    
    [<ReactComponent(import="Debug.ViewportInfo", from="mafs")>]
    static member DebugViewPortInfo (precision: int) = React.imported ()
    
    [<ReactComponent(import="MovablePoint", from="mafs")>]
    static member DebugViewPortInfo (point    : Vec2,
                                     onMove   : Vec2 -> unit,
                                     constrain: U2<Constrain, Vec2 -> Vec2>,
                                     color    : Color) = React.imported ()

    [<Import("useMovablePoint", "mafs")>]
    [<Emit("useMovablePoint($1, {constrain: $2, color: $3})")>]
    static member useMovablePoint (initialPoint: Vec2,
                                   ?constrain  : U2<Constrain, Vec2 -> Vec2>,
                                   ?color      : Color) = jsNative<MovablePoint>

[<AutoOpen>]
module Functions =
    let Mafs props children  = Verbatim.Mafs (children, props)
    let MafsDefault children = Verbatim.Mafs (children, MafsProps.Default)

    [<AutoOpen>]
    module Coordinates =
        [<ReactComponent>]
        let Cartesian (props: CartesianProps) = 
            Verbatim.Cartesian (xAxis = props.xAxis,
                                yAxis = props.yAxis,
                                subdivisions = props.subDiv)
        [<ReactComponent>]
        let Polar (props: PolarProps) = 
            Verbatim.Polar (xAxis = props.xAxis,
                            yAxis = props.yAxis,
                            lines = props.lines,
                            subdivisions = props.subDiv)

    [<ReactComponent>]
    let Point (vec: Vec2) = Verbatim.Point (vec[0], vec[1])
    [<ReactComponent>]
    let Circle center radius = Verbatim.Circle (center = center, radius = radius)
    [<ReactComponent>]
    let Polygon points = Verbatim.Polygon (points = points)

    [<ReactComponent>]
    let Text text (pos: Vec2) =
        Verbatim.Text (text, pos[0], pos[1])
    [<ReactComponent>]
    let TextAttach text (pos: Vec2) dir =
        Verbatim.Text (text, pos[0], pos[1], attach = dir)

    let useMovablePoint initial =
        Verbatim.useMovablePoint initial
    let useConstrainedPoint initial constrain =
        Verbatim.useMovablePoint (initial, constrain = constrain)

    module Plot =
        [<ReactComponent>]
        let OfX fn = Verbatim.OfX (y = fn)

        let create fn = { PlotProps.Default with fn = fn }
        let color    color   (props: PlotProps) = { props with color   = color   }
        let weight   weight  (props: PlotProps) = { props with weight  = weight  }
        let opacity  opacity (props: PlotProps) = { props with opacity = opacity }
        let style    style   (props: PlotProps) = { props with style   = style   }
        let minDepth depth   (props: PlotProps) = { props with minSamplingDepth = depth }
        let maxDepth depth   (props: PlotProps) = { props with maxSamplingDepth = depth }
        [<ReactComponent>]
        let render axis props =
            match axis with
            | XAxis -> Verbatim.OfX (props.fn, color = props.color, weight = props.weight, opacity = props.opacity, style = props.style,
                                     minSamplingDepth = props.minSamplingDepth, maxSamplingDepth = props.maxSamplingDepth)
            | YAxis -> Verbatim.OfY (props.fn, color = props.color, weight = props.weight, opacity = props.opacity, style = props.style,
                                     minSamplingDepth = props.minSamplingDepth, maxSamplingDepth = props.maxSamplingDepth)

        let renderInequality axis leq ueq lprop uprop =
            match axis with
            | XAxis -> Verbatim.Inequality (y = createObj [ leq.ToString () ==> lprop.fn; ueq.ToString () ==> uprop.fn ],
                                            lowerColor = lprop.color, lowerWeight = lprop.weight,lowerOpacity = lprop.opacity,
                                            upperColor = uprop.color, upperWeight = uprop.weight, upperOpacity = uprop.opacity)
            | YAxis -> Verbatim.Inequality (x = createObj [ leq.ToString () ==> lprop.fn; ueq.ToString () ==> uprop.fn ],
                                            lowerColor = lprop.color, lowerWeight = lprop.weight,lowerOpacity = lprop.opacity,
                                            upperColor = uprop.color, upperWeight = uprop.weight, upperOpacity = uprop.opacity)
