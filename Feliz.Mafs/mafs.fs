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

    type Axis =
        { lines : float
          labels: int -> string }
        static member Default =
            { lines  = 1.0
              labels = fun x -> $"{x}" }

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

    type CartesianProps =
        { xAxis : Axis
          yAxis : Axis
          subDiv: float }
        static member Default =
            { xAxis  = Axis.Default
              yAxis  = Axis.Default
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
    
    type PointProps =
        { initial  : Vec2
          constrain: U2<Constrain, Vec2 -> Vec2> }
        static member Default =
            { initial   = [| 0; 0 |]
              constrain = !^(fun v -> v) }

type Binds =
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
        Binds.Mafs (children = (prop.key 1)::[prop.children children], width = props.width, height = props.height,
                    pan = props.pan, zoom = props.zoom, viewBox = props.viewBox,
                    preserveAspectRatio = props.preserveAspectRatio, ssr = props.ssr, onClick = props.onClick)

    [<ReactComponent(import="Coordinates.Cartesian", from="mafs")>]
    static member Cartesian (?xAxis       : Axis,
                             ?yAxis       : Axis,
                             ?subdivisions: float) = React.imported ()

    [<ReactComponent(import="Circle", from="mafs")>]
    static member Circle (radius: float,
                          center: Vec2) = React.imported ()

    [<ReactComponent(import="Polygon", from="mafs")>]
    static member Polygon (points          : Vec2 array,
                           ?color          : string,
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
                        ?color         : string,
                        ?svgTextProps  : SVGProps) = React.imported ()

    [<ReactComponent(import="Plot.OfX", from="mafs")>]
    static member OfX (props: {| y: float -> float |}) = React.imported ()

    [<Import("useMovablePoint", "mafs")>]
    [<Emit("useMovablePoint($1, {constrain: $2, color: $3})")>]
    static member useMovablePoint (initialPoint: Vec2,
                                   ?constrain  : U2<Constrain, Vec2 -> Vec2>,
                                   ?color      : string) = jsNative<MovablePoint>

[<AutoOpen>]
module Functions =
    let vec2 x y: float array = [| x; y |]

    let Mafs props children  = Binds.Mafs (children, props)
    let MafsDefault children = Binds.Mafs (children, MafsProps.Default)

    [<AutoOpen>]
    module Coordinates =
        [<ReactComponent>]
        let Cartesian props = 
            Binds.Cartesian (xAxis = props.xAxis,
                             yAxis = props.yAxis,
                             subdivisions = props.subDiv)

    [<ReactComponent>]
    let Circle center radius =
        Binds.Circle (center = center, radius = radius)
    [<ReactComponent>]
    let Polygon points =
        Binds.Polygon (points = points)

    [<ReactComponent>]
    let Text text (pos: Vec2) =
        Binds.Text (text, pos[0], pos[1])
    [<ReactComponent>]
    let TextAttach text (pos: Vec2) dir =
        Binds.Text (text, pos[0], pos[1], attach = dir)

    let useMovablePoint initial =
        Binds.useMovablePoint initial
    let useConstrainedPoint initial constrain =
        Binds.useMovablePoint (initial, constrain = constrain)

    module Plot =
        [<ReactComponent>]
        let OfX fn = Binds.OfX {| y = fn |}
