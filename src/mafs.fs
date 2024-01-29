module SharpMafs

open Fable.Core
open Feliz

type Vec2 = float array
let vec2 x y = [| x; y |]

type ViewBox =
    { x      : Vec2
      y      : Vec2
      padding: float option }
    static member Default =
        { x = [| 5.0; 5.0 |]
          y = [| 5.0; 5.0 |]
          padding = None }

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
    { color    : string option
      constrain: Constrain }

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
    static member pi = 3.1415926535

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

    [<ReactComponent(import="Coordinates.Cartesian", from="mafs")>]
    static member Cartesian (?xAxis       : Axis,
                             ?yAxis       : Axis,
                             ?subdivisions: float) = React.imported ()
