module Feliz.Mafs

open Fable.Core
open Feliz

[<AutoOpen>]
module Types =
    type Vec2 = float array

    type ViewBox =
        { x      : Vec2
          y      : Vec2
          padding: float }
        static member Default =
            { x       = [| 5.0; 5.0 |]
              y       = [| 5.0; 5.0 |]
              padding = 0.0 }

    [<StringEnum>]
    type Constrain =
        | Horizontal
        | Vertical

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

    type Direction =
        | [<CompiledName("n")>] North | [<CompiledName("ne")>] NorthEast
        | [<CompiledName("e")>] East  | [<CompiledName("se")>] SouthEast
        | [<CompiledName("s")>] South | [<CompiledName("sw")>] SouthWest
        | [<CompiledName("w")>] West  | [<CompiledName("nw")>] NorthWest

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
              svgTextProps   = null }

[<AutoOpen>]
module Plot =
    [<Import("labelPi", "mafs")>]
    let Pi = jsNative<string>

    type Axis =
        { lines : float
          labels: string }

    type Plot =
        [<ReactComponent(import="Plot.OfX", from="mafs")>]
        static member OfX (props: {| y: float -> float |}) = React.imported ()

        [<Import("useMovablePoint", "mafs")>]
        [<Emit("$0($1, {constrain: $2, color: $3})")>]
        static member useMovablePoint (initialPoint: Vec2,
                                       ?constrain  : U2<Constrain, Vec2 -> Vec2>,
                                       ?color      : string) = jsNative<MovablePoint>

[<AutoOpen>]
type Mafs =
    [<ReactComponent(import="Mafs", from="mafs")>]
    static member Mafs (children: IReactProperty list,
                        ?width  : U2<string, string>,
                        ?height : float,
                        ?pan    : bool,
                        ?zoom   : U2<bool, {| min: float
                                              max: float |}>,
                        ?viewBox: ViewBox,
                        ?preserveAspectRatio: U2<string, bool>,
                        ?ssr    : bool,
                        ?onClick: Vec2 -> obj -> int) = React.imported ()

[<AutoOpen>]
type Coordinates =
    [<ReactComponent(import="Coordinates.Cartesian", from="mafs")>]
    static member Cartesian (?xAxis       : Axis,
                             ?yAxis       : Axis,
                             ?subdivisions: float) = React.imported ()

[<AutoOpen>]
type Text =
    [<ReactComponent(import="Text", from="mafs")>]
    static member Text (children       : string,
                        x              : float,
                        y              : float,
                        ?attach        : Direction,
                        ?attachDistance: float,
                        ?size          : float,
                        ?color         : string,
                        ?svgTextProps  : obj) = React.imported ()

[<AutoOpen>]
module Util =
    let vec2 x y = [| x; y |]

    let Text (pos: Vec2) (text: string) = Text (text, pos[0], pos[1])
